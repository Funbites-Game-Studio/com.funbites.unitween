namespace UniTween.Tweens {
    using DG.Tweening;
    using System;
    using UniTween.Core;
    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
    using Funbites.UnityUtils.Editor;
#endif

    public class ShakeTweenData : TweenData {
        public enum SpecificType {
            RectTransform_AnchoredPosition,
            Camera_ShakePosition,
            Camera_ShakeRotation,
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.RectTransform_AnchoredPosition;
        [SerializeField]
        float m_strength = 100;
        [SerializeField]
        int m_vibrato = 10;
        [SerializeField]
        float m_randomness = 90;
        [SerializeField]
        bool m_snapping = false;
        [SerializeField]
        bool m_fadeOut = true;

        public override Type RequestedType {
            get {
                switch (m_type) {
                    case SpecificType.RectTransform_AnchoredPosition:
                        return typeof(RectTransform);
                    case SpecificType.Camera_ShakePosition:
                    case SpecificType.Camera_ShakeRotation:
                        return typeof(Camera);
                    default:
                        return null;
                }
            }
        }


        public override Tween GetTween(UnityEngine.Object target) {
            
            switch (m_type) {
                case SpecificType.RectTransform_AnchoredPosition: {
                        RectTransform rectTransform = (RectTransform)target;
                        return rectTransform.DOShakeAnchorPos(duration, m_strength, m_vibrato, m_randomness, m_snapping, m_fadeOut);
                    }
                case SpecificType.Camera_ShakePosition:
                    {
                        Camera cam = (Camera)target;
                        return cam.DOShakePosition(duration, m_strength, m_vibrato, m_randomness, m_fadeOut);
                    }
                case SpecificType.Camera_ShakeRotation:
                    {
                        Camera cam = (Camera)target;
                        return cam.DOShakeRotation(duration, m_strength, m_vibrato, m_randomness, m_fadeOut);
                    }
                default:
                    return null;
            }
        }

        internal override bool ValidateType(Type type) {
            switch (m_type) {
                case SpecificType.RectTransform_AnchoredPosition:
                    return type == typeof(RectTransform);
                case SpecificType.Camera_ShakePosition:
                case SpecificType.Camera_ShakeRotation:
                    return type == typeof(Camera);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tween Data/Canvas/Rect Transform/Shake Anchored Position")]
        private static void CreateRectTransformShakeAnchoredPositionSpriteAsset() {
            var newInstance = CreateInstance<ShakeTweenData>();
            newInstance.m_type = SpecificType.RectTransform_AnchoredPosition;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Canvas/Camera/Shake Position")]
        private static void CreateShakeCameraPositionAsset()
        {
            var newInstance = CreateInstance<ShakeTweenData>();
            newInstance.m_type = SpecificType.Camera_ShakePosition;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Canvas/Rect Transform/Shake Anchored Position")]
        private static void CreateShakeCameraRotationAsset()
        {
            var newInstance = CreateInstance<ShakeTweenData>();
            newInstance.m_type = SpecificType.Camera_ShakeRotation;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif
    }
}