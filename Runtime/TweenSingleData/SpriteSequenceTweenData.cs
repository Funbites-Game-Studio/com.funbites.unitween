namespace UniTweens.SpriteAnimation {
    using Sirenix.OdinInspector;
    using System;
    using UnityEngine;
    using UniTween.Core;
    using DG.Tweening;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    [CreateAssetMenu(menuName ="Tween Data/Sprite Animation/Sprite Sequence")]
    public class SpriteSequenceTweenData : TweenData {
        [InfoBox("Duration and CustomEase/Curve are set automatically based on Frames. Please, don't change it.", InfoMessageType.Warning)]
        public enum SpecificType {
            Image_Sprite,
            SpriteRenderer_Sprite
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.Image_Sprite;
        [Serializable]
        public class Frame {
            [SerializeField]
            private float m_duration = 1;
            [SerializeField, Required]
            private Sprite m_sprite = null;
            
            public Sprite Sprite => m_sprite;
            public float Duration => m_duration;
        }
        [SerializeField, OnValueChanged("OnEditFrames", true)]
        private Frame[] m_frames = null;
        public int Length => m_frames.Length;
        public Frame this[int i] {
            get {
                return m_frames[i];
            }
        }

        


        public override Type RequestedType {
            get {
                switch (m_type) {
                    case SpecificType.Image_Sprite:
                        return typeof(ImageSpriteAnimation);
                    case SpecificType.SpriteRenderer_Sprite:
                        throw new NotImplementedException("Not implemented");
                    default:
                        return null;
                }
            }
        }
        public override Tween GetTween(UnityEngine.Object target) {
            switch (m_type) {
                case SpecificType.Image_Sprite: {
                        ImageSpriteAnimation imageSpriteAnimator = (ImageSpriteAnimation)target;
                        Tween t = DOTween.To(() => imageSpriteAnimator.IndexAcc, x => {
                            imageSpriteAnimator.IndexAcc = x;
                            imageSpriteAnimator.Sprite = m_frames[Mathf.CeilToInt(x)].Sprite;
                        }, Length, duration).From(0f,false);
                        return t;
                    }
                case SpecificType.SpriteRenderer_Sprite: {
                        throw new NotImplementedException("Not implemented");
                    }
                default:
                    return null;
            }
        }

        internal override bool ValidateType(Type type) {
            switch (m_type) {
                case SpecificType.Image_Sprite:
                    return type == typeof(ImageSpriteAnimation);
                case SpecificType.SpriteRenderer_Sprite:
                    throw new NotImplementedException("Not implemented");
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        private void OnEditFrames() {
            customEase = true;
            duration = 0;
            Frame frame;
            curve.keys = null;
            if (Length == 0) return;
            int i;
            for (i = 0; i < Length; i++) {
                frame = m_frames[i];
                duration += frame.Duration;
            }
            float curNormTime = 0;
            for (i = 0; i < Length; i++) {
                frame = m_frames[i];
                curve.AddKey(curNormTime, (float)i/Length);
                curNormTime += frame.Duration / duration;
            }
            curve.AddKey(1, ((float)i - 1)/Length);
            for (i = 0; i < curve.keys.Length; i++) {
                AnimationUtility.SetKeyLeftTangentMode(curve, i, AnimationUtility.TangentMode.Constant);
                AnimationUtility.SetKeyRightTangentMode(curve, i, AnimationUtility.TangentMode.Constant);
            }
        }
#endif
    }
}