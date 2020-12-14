using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
namespace UniTween.Core {
    public class UniTweenSequencePlayer : MonoBehaviour, IUniTweenSequencePlayer {
        [SerializeField, InlineProperty, HideLabel]
        private UniTweenSequence m_tweenSequence = null;
        public UniTweenSequence TweenSequence {
            get {
                if (!m_tweenSequence.HasInitialized)
                    InitializeSequence(); 
                return m_tweenSequence;
            }
        }
        [FoldoutGroup("Settings", true)]
        public bool playOnStart;
        [FoldoutGroup("Settings", true)]
        public bool playOnEnable;
        //[FoldoutGroup("Settings", true)]
        //public bool playBackwardsOnStart;
        //[FoldoutGroup("Settings", true)]
        //public bool playBackwardsOnEnable;
        [FoldoutGroup("Settings", true)]
        public bool resumeOnEnable;
        [FoldoutGroup("Settings", true)]
        public bool rewindOnDisable;
        [FoldoutGroup("Settings", true)]
        public bool killOnDisable;

        [Tooltip("Set this to -1 for infinite loops.")]
        [FoldoutGroup("Settings", true)]
        public int loops;
        [FoldoutGroup("Settings", true)]
        [ShowIf("IsLoopSequence")]
        public LoopType loopType;
        [FoldoutGroup("Settings", true)]
        public float timeScale = 1;

        private void Awake() {
            if (!m_tweenSequence.HasInitialized)
                InitializeSequence();
        }

        private void InitializeSequence()
        {
            m_tweenSequence.Initialize(this, "seq", loops, loopType, timeScale);
        }

        public void Kill() {
            m_tweenSequence.Kill();
        }

        private void Start() {
            if (playOnStart)
                m_tweenSequence.Play();

            //if (playBackwardsOnStart)
            //    m_tweenSequence.PlayBackwards();
        }

        public void Play() {
            if (TweenSequence.IsPlaying) {
                m_tweenSequence.Rewind();
            }
            m_tweenSequence.Play();
        }

        public void Stop() {
            m_tweenSequence.Pause();
            m_tweenSequence.Rewind();
        }

        protected void OnEnable() {


            if (playOnEnable)
                m_tweenSequence.Play();

            if (resumeOnEnable)
                m_tweenSequence.Resume();

            //if (playBackwardsOnEnable)
            //    m_tweenSequence.PlayBackwards();
        }

        protected void OnDisable() {

            if (rewindOnDisable)
                m_tweenSequence.Rewind();

            if (killOnDisable)
                m_tweenSequence.Kill();
        }

        public string DisplayName {
            get {
                return name;
            }
        }

        public IEnumerable<UniTweenSequence> Tweens {
            get {
                return new UniTweenSequence[] { m_tweenSequence };
            }
        }

        public bool IsPlaying => m_tweenSequence.IsPlaying;

        private bool IsLoopSequence() {
            return loops != 0;
        }

        private bool isApplicationQuitting = false;
        private void OnApplicationQuit() {
            isApplicationQuitting = true;
        }

        private void OnDestroy() {
            if (!isApplicationQuitting) {
                m_tweenSequence.Kill();
            }
        }
    }
}