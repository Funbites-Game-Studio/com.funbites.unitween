using DG.Tweening;
using System;
using UniTween.Core;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening.Core;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
using Funbites.UnityUtils.Editor;
#endif

namespace UniTween.Tweens
{
    public class SetColorTweenData : TweenData
    {
        public enum SpecificType
        {
            Material_ColorProperty,
            Material_BlendableColorProperty
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.Material_ColorProperty;
        [SerializeField]
        private Color m_targetValue = Color.white;
        /*[SerializeField]
        private bool m_useFrom = false;
        [SerializeField, ShowIf("m_useFrom")]
        private float m_fromValue = 0;*/
        [SerializeField]
        private string m_variableName = "";


        public override Type RequestedType {
            get {
                switch (m_type)
                {
                    case SpecificType.Material_ColorProperty:
                    case SpecificType.Material_BlendableColorProperty:
                        return typeof(Material);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target)
        {
            TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> result;
            switch (m_type)
            {
                case SpecificType.Material_ColorProperty:
                    {
                        var material = (Material)target;
                        result = material.DOColor(m_targetValue, m_variableName, duration);
                        break;
                    }
                case SpecificType.Material_BlendableColorProperty:
                    {
                        var material = (Material)target;
                        return material.DOBlendableColor(m_targetValue, m_variableName, duration);
                    }
                default:
                    result = null;
                break;
            }
            /*if (m_useFrom)
            {
                result.From(m_fromValue, false);
            }*/
            return result;
        }

        internal override bool ValidateType(Type type)
        {
            switch (m_type)
            {
                case SpecificType.Material_ColorProperty:
                case SpecificType.Material_BlendableColorProperty:
                    return type == typeof(Material);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR

        [MenuItem("Assets/Create/Tween Data/Material/Color Property")]
        private static void CreateMaterialColorPropertyAsset()
        {
            var newInstance = CreateInstance<SetColorTweenData>();
            newInstance.m_type = SpecificType.Material_ColorProperty;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Material/Blendable Color Property")]
        private static void CreateMaterialBlendableColorPropertyAsset()
        {
            var newInstance = CreateInstance<SetColorTweenData>();
            newInstance.m_type = SpecificType.Material_BlendableColorProperty;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif
    }
}