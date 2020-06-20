namespace UniTween.Tweens
{

    using DG.Tweening;
    using System;
    using UniTween.Core;
    using UnityEngine;
    using DG.Tweening.Core;
#if UNITY_EDITOR
    using UnityEditor;
    using Funbites.UnityUtils.Editor;
#endif

    public class SetGradientTweenData : TweenData
    {
        public enum SpecificType
        {
            Material_GradientProperty,
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.Material_GradientProperty;
        [SerializeField]
        private Gradient m_targetValue = default;
        [SerializeField]
        private string m_variableName = "";


        public override Type RequestedType {
            get {
                switch (m_type)
                {
                    case SpecificType.Material_GradientProperty:
                        return typeof(Material);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target)
        {
            TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> result;
            switch (m_type)
            {
                case SpecificType.Material_GradientProperty:
                    {
                        var material = (Material)target;
                        return material.DOGradientColor(m_targetValue, m_variableName, duration);
                    }
                default:
                    result = null;
                break;
            }
            return result;
        }

        internal override bool ValidateType(Type type)
        {
            switch (m_type)
            {
                case SpecificType.Material_GradientProperty:
                    return type == typeof(Material);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR

        [MenuItem("Assets/Create/Tween Data/Material/Gradient Property")]
        private static void CreateMaterialGradientPropertyAsset()
        {
            var newInstance = CreateInstance<SetGradientTweenData>();
            newInstance.m_type = SpecificType.Material_GradientProperty;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif
    }
}