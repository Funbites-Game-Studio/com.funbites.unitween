namespace UniTween.Tweens {
    using DG.Tweening;
    using DG.Tweening.Core;
    using Sirenix.OdinInspector;
    using System;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.UI;

#if UNITY_EDITOR
    using UnityEditor;
    using Funbites.UnityUtils.Editor;
#endif

    public class Vector2TweenData : TweenData {
        public enum SpecificType {
            LayoutElement_FlexibleSize,
            LayoutElement_MinSize,
            LayoutElement_PreferredSize
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.LayoutElement_FlexibleSize;
        [SerializeField]
        private Vector2 m_targetValue = Vector2.zero;
        [SerializeField]
        private bool m_useFrom = false;
        [SerializeField, ShowIf("m_useFrom")]
        private Vector2 m_fromValue = Vector2.zero;

        public override Type RequestedType {
            get {
                switch (m_type) {
                    case SpecificType.LayoutElement_FlexibleSize:
                    case SpecificType.LayoutElement_MinSize:
                    case SpecificType.LayoutElement_PreferredSize:
                        return typeof(LayoutElement);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target) {
            TweenerCore<Vector2, Vector2, DG.Tweening.Plugins.Options.VectorOptions> result;
            switch (m_type) {
                case SpecificType.LayoutElement_FlexibleSize: {
                        var layoutElement = (LayoutElement)target;
                        result = layoutElement.DOFlexibleSize(m_targetValue, duration);
                        break;
                    }
                case SpecificType.LayoutElement_MinSize:
                    {
                        var layoutElement = (LayoutElement)target;
                        result = layoutElement.DOMinSize(m_targetValue, duration);
                        break;
                    }
                case SpecificType.LayoutElement_PreferredSize:
                    {
                        var layoutElement = (LayoutElement)target;
                        result = layoutElement.DOPreferredSize(m_targetValue, duration);
                        break;
                    }
                default:
                    result = null;
                    break;
            }
            if (m_useFrom) {
                result.From(m_fromValue, false);
            }
            return result;
        }

        internal override bool ValidateType(Type type) {
            switch (m_type) {
                case SpecificType.LayoutElement_FlexibleSize:
                case SpecificType.LayoutElement_MinSize:
                case SpecificType.LayoutElement_PreferredSize:
                    return type == typeof(LayoutElement);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tween Data/Canvas/Layout Element/Flexible Size")]
        private static void CreateLayoutFlexibleSizeAsset() {
            var newInstance = CreateInstance<Vector2TweenData>();
            newInstance.m_type = SpecificType.LayoutElement_FlexibleSize;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Canvas/Layout Element/Flexible Size")]
        private static void CreateLayoutMinSizeAsset()
        {
            var newInstance = CreateInstance<Vector2TweenData>();
            newInstance.m_type = SpecificType.LayoutElement_MinSize;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Canvas/Layout Element/Flexible Size")]
        private static void CreateLayoutPreferredSizeAsset()
        {
            var newInstance = CreateInstance<Vector2TweenData>();
            newInstance.m_type = SpecificType.LayoutElement_PreferredSize;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif

    }
}