using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UniTween.Core
{
    [HideMonoScript]
    [Serializable]
    [HelpURL("https://github.com/sampaiodias/UniTween")]
    public class UniTweenSequence
    {
        [Space(10)]
        [ListDrawerSettings(AlwaysAddDefaultValue = true)]
        [InlineProperty]
        [LabelText("Sequence")]
        [PropertyOrder(10)]
        [HideReferenceObjectPicker]
        [OnValueChanged("CreateNewUniTween")]
        public List<UniTweenObject> uniTweens = new List<UniTweenObject>();
        [HideInInspector]
        public Sequence SequenceInstance { get; private set; }
        [ToggleLeft]
        [InfoBox("This Sequence is disabled, it will not be played if you call it.", InfoMessageType = InfoMessageType.Warning, VisibleIf  = "Disabled")]
        public bool Enabled = true;

        public bool Disabled => !Enabled;
        public bool IsPlaying => SequenceInstance.IsPlaying();

        public int ElementsCount => uniTweens.Count;

        private float originalTimeScale;

        public IUniTweenSequencePlayer TweenPlayer { get; private set; }
        public bool HasInitialized => TweenPlayer != null;

        public void Initialize(IUniTweenSequencePlayer owner, string name, int loops = 0, LoopType loopType = LoopType.Incremental, float timeScale = 1)
        {
            if (HasInitialized) throw new Exception("Trying to initialize a Sequence that's already initialized.");
            TweenPlayer = owner;
            SequenceInstance = DOTween.Sequence();
            SequenceInstance.SetId($"{owner.DisplayName}_{name}");
            SequenceInstance.SetAutoKill(false);
            SequenceInstance.SetLoops(loops, loopType);
            SequenceInstance.timeScale = timeScale;
            originalTimeScale = timeScale;
            foreach (var uniTween in uniTweens)
            {
                switch (uniTween.operation)
                {
                    case UniTweenObject.TweenOperation.Append:
                        SequenceInstance.Append(GetTween(uniTween));
                        break;
                    case UniTweenObject.TweenOperation.AppendInterval:
                        SequenceInstance.AppendInterval(uniTween.GetInterval());
                        break;
                    case UniTweenObject.TweenOperation.AppendCallback:
                        SequenceInstance.AppendCallback(() => uniTween.unityEvent.Invoke());
                        break;
                    case UniTweenObject.TweenOperation.Join:
                        SequenceInstance.Join(GetTween(uniTween));
                        break;
                }
            }
        }

        /// <summary>
        /// Initializes and plays the Sequence. If you already called this once and didn't change 
        /// the Sequence, consider using Resume() for a performance boost.
        /// </summary>
        [ShowIf("IsApplicationPlaying")]
        [ButtonGroup("Player")]
        [PropertyOrder(-1)]
        [Button(ButtonSizes.Medium)]
        public void Play()
        {
            //Debug.Log($"Playing {TweenPlayer.DisplayName}", TweenPlayer.gameObject);
            Play(originalTimeScale);
        }

        public void Play(float timeScale)
        {
            SequenceInstance.timeScale = timeScale;
            JustPlay();
        }

        public void Stop()
        {
            Pause();
            Rewind();
        }

        private void JustPlay()
        {
            if (Enabled)
            {
                if (SequenceInstance.IsComplete())
                {
                    SequenceInstance.Rewind();
                }
                SequenceInstance.Play();
            } else
            {
                if (SequenceInstance.onComplete != null) SequenceInstance.onComplete.Invoke();
            }

        }

        /// <summary>
        /// Initializes and Sequence backwards and plays the Sequence.
        /// If you already called this once and didn't change 
        /// the Sequence, consider using Resume() for a small performance boost.
        /// </summary>
        /*[ShowIf("IsApplicationPlaying")]
        [ButtonGroup("Player")]
        [PropertyOrder(-1)]
        [Button(ButtonSizes.Medium)]
        public void PlayBackwards() {
            SequenceInstance.timeScale = -SequenceInstance.timeScale;
            SequenceInstance.Play();
        }*/

        /// <summary>
        /// Resumes the playing Sequence, playing it where it was paused. 
        /// Only works if the Sequence was initialized before (using Play() or PlayBackwards()).
        /// </summary>
        [ShowIf("CanShowResumeButton")]
        [ButtonGroup("Player")]
        [PropertyOrder(-1)]
        [Button(ButtonSizes.Medium)]
        public void Resume()
        {
            SequenceInstance.Play();
        }

        /// <summary>
        /// Pauses the Sequence.
        /// </summary>
        [ShowIf("CanShowPauseButton")]
        [ButtonGroup("Player")]
        [PropertyOrder(-1)]
        [Button(ButtonSizes.Medium)]
        public void Pause()
        {
            SequenceInstance.Pause();
        }

        /// <summary>
        /// Rewinds and pauses the Sequence.
        /// </summary>
        [ShowIf("IsApplicationPlaying")]
        [ButtonGroup("Player")]
        [PropertyOrder(-1)]
        [Button(ButtonSizes.Medium)]
        public void Rewind()
        {
            SequenceInstance.Rewind();
        }

        /// <summary>
        /// Kills the Sequence.
        /// </summary>
        [ShowIf("IsApplicationPlaying")]
        [ButtonGroup("Player")]
        [PropertyOrder(-1)]
        [Button(ButtonSizes.Medium)]
        public void Kill()
        {
            if (HasInitialized && SequenceInstance.IsPlaying())
            {
                SequenceInstance.Pause();
            }
            SequenceInstance.Kill();
        }

        private void CreateNewUniTween()
        {
            for (int i = 0; i < uniTweens.Count; i++)
            {
                if (uniTweens[i] == null)
                    uniTweens[i] = new UniTweenObject();
            }
        }

        private Tween GetTween(UniTweenObject uniTween)
        {
            try
            {
                if (uniTween.tweenData == null)
                {
                    Debug.LogError($"Tween sequence {SequenceInstance.stringId} found an empty tween data.", TweenPlayer.gameObject);
                    return null;
                }
                Tween t = uniTween.tweenData.GetTween(uniTween.target);
                if (uniTween.tweenData.customEase)
                    t.SetEase(uniTween.tweenData.curve);
                else
                    t.SetEase(uniTween.tweenData.ease);
                t.SetDelay(uniTween.tweenData.delay);
                return t;
            } catch (InvalidCastException ic)
            {
                Debug.LogError($"Tween target is not from the right type in {TweenPlayer.DisplayName}. {ic.Message}", TweenPlayer.gameObject);
                return null;
            }
        }
#if UNITY_EDITOR
        private bool CanShowPauseButton()
        {
            return Application.isPlaying && SequenceInstance.IsPlaying();
        }

        private bool CanShowResumeButton()
        {
            if (!Application.isPlaying) return false;
            if (SequenceInstance == null) return false;
            float elapsedPercentage = SequenceInstance.ElapsedPercentage();
            return !SequenceInstance.IsPlaying() && elapsedPercentage > 0 && elapsedPercentage < 1;
        }

        private bool IsApplicationPlaying()
        {
            return Application.isPlaying;
        }
#endif

    }
}