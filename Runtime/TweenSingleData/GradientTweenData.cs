namespace UniTween.Tweens
{
    using DG.Tweening;
    using System;
    using UniTween.Core;
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
    using Funbites.UnityUtils.Editor;
    using UnityEngine.UI;
#endif

    public class GradientTweenData : TweenData
    {
        public enum SpecificType
        {
            Image_GradientColor

        }
        [SerializeField]
        private SpecificType m_type = SpecificType.Image_GradientColor;
        [SerializeField]
        private Gradient m_targetValue = default;


        public override Type RequestedType {
            get {
                switch (m_type)
                {
                    case SpecificType.Image_GradientColor:
                        return typeof(Image);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target)
        {
            switch (m_type)
            {
                case SpecificType.Image_GradientColor:
                    {    
                        var image = (Image)target;
                        return image.DOGradientColor(m_targetValue, duration);
                    }
                default:
                        return null;
            }
        }

        internal override bool ValidateType(Type type)
        {
            switch (m_type)
            {
                case SpecificType.Image_GradientColor:
                    return type == typeof(Image);
                default:
                    return false;
            }
        }

    #if UNITY_EDITOR
        [MenuItem("Assets/Create/Tween Data/Image/Gradient Color")]
        private static void CreateImageGradientColorAsset()
        {
            var newInstance = CreateInstance<GradientTweenData>();
            newInstance.m_type = SpecificType.Image_GradientColor;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
    #endif
    }
}