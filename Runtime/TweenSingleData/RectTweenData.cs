using DG.Tweening;
using System;
using UniTween.Core;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening.Core;
#if UNITY_EDITOR
using UnityEditor;
using Funbites.UnityUtils.Editor;
#endif

namespace UniTween.Tweens
{
    public class RectTweenData : TweenData
    {
        public enum SpecificType
        {
            Camera_PixelRect,
            Camera_Rect

        }
        [SerializeField]
        private SpecificType m_type = SpecificType.Camera_PixelRect;
        [SerializeField]
        private Rect m_targetValue = default;
        [SerializeField]
        private bool m_useFrom = false;
        [SerializeField, ShowIf("m_useFrom")]
        private Rect m_fromValue = default;


        public override Type RequestedType {
            get {
                switch (m_type)
                {
                    case SpecificType.Camera_PixelRect:
                    case SpecificType.Camera_Rect:
                        return typeof(Camera);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target)
        {
            TweenerCore<Rect, Rect, DG.Tweening.Plugins.Options.RectOptions> result;
            switch (m_type)
            {
                case SpecificType.Camera_PixelRect:
                    {
                        Camera camera = (Camera)target;
                        result = camera.DOPixelRect(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Camera_Rect:
                    {
                        Camera camera = (Camera)target;
                        result = camera.DORect(m_targetValue, duration);
                        break;
                    }
                default:
                    result = null;
                    break;
            }
            if (m_useFrom)
            {
                result.From(m_fromValue, false);
            }
            return result;
        }

        internal override bool ValidateType(Type type)
        {
            switch (m_type)
            {
                case SpecificType.Camera_PixelRect:
                    return type == typeof(Camera);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tween Data/Camera/PixelRect")]
        private static void CreateCameraPixelRectAsset()
        {
            var newInstance = CreateInstance<RectTweenData>();
            newInstance.m_type = SpecificType.Camera_PixelRect;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Camera/Rect")]
        private static void CreateCameraRectAsset()
        {
            var newInstance = CreateInstance<RectTweenData>();
            newInstance.m_type = SpecificType.Camera_Rect;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif
    }
}