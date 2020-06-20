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

    public class SetVector2TweenData : TweenData
    {
        public enum SpecificType
        {
            Material_OffsetProperty,
            Material_TilingProperty,
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.Material_OffsetProperty;
        [SerializeField]
        private Vector2 m_targetValue = default;
        [SerializeField]
        private string m_variableName = "";


        public override Type RequestedType {
            get {
                switch (m_type)
                {
                    case SpecificType.Material_OffsetProperty:
                    case SpecificType.Material_TilingProperty:
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
                case SpecificType.Material_OffsetProperty:
                    {
                        var material = (Material)target;
                        return material.DOOffset(m_targetValue, m_variableName, duration);
                    }
                case SpecificType.Material_TilingProperty:
                    {
                        var material = (Material)target;
                        return material.DOTiling(m_targetValue, m_variableName, duration);
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
                case SpecificType.Material_OffsetProperty:
                case SpecificType.Material_TilingProperty:
                    return type == typeof(Material);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR

        [MenuItem("Assets/Create/Tween Data/Material/Offset Property")]
        private static void CreateMaterialOffsetPropertyAsset()
        {
            var newInstance = CreateInstance<SetVector2TweenData>();
            newInstance.m_type = SpecificType.Material_OffsetProperty;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Material/Tiling Property")]
        private static void CreateMaterialTilingPropertyAsset()
        {
            var newInstance = CreateInstance<SetVector2TweenData>();
            newInstance.m_type = SpecificType.Material_TilingProperty;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif
    }
}