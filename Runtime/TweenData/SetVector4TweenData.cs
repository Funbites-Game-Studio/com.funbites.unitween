namespace UniTween.Tweens {
    using DG.Tweening;
    using DG.Tweening.Core;
    using Sirenix.OdinInspector;
    using System;
    using UniTween.Core;
    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
    using Funbites.UnityUtils.Editor;
#endif

    public class SetVector4TweenData : TweenData {
        public enum SpecificType {
            Material_Vector
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.Material_Vector;
        [SerializeField]
        private Vector4 m_targetValue = Vector4.zero;
        [SerializeField]
        private string m_variableName = "";
        /*[SerializeField]
        private bool m_useFrom = false;
        [SerializeField, ShowIf("m_useFrom")]
        private Vector4 m_fromValue = Vector3.zero;*/

        public override Type RequestedType {
            get {
                switch (m_type) {
                    case SpecificType.Material_Vector:
                        return typeof(Material);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target) {
            TweenerCore<Vector4, Vector4, DG.Tweening.Plugins.Options.VectorOptions> result;
            switch (m_type) {
                case SpecificType.Material_Vector: {
                        var material = (Material)target;
                        result = material.DOVector(m_targetValue, m_variableName, duration);
                        break;
                    }
                default:
                    result = null;
                    break;
            }
            /*if (m_useFrom) {
                result.From(m_fromValue, false);
            }*/
            return result;
        }

        internal override bool ValidateType(Type type) {
            switch (m_type) {
                case SpecificType.Material_Vector:
                    return type == typeof(Material);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tween Data/Material/Vector Property")]
        private static void CreateTransformMoveAsset() {
            var newInstance = CreateInstance<SetVector4TweenData>();
            newInstance.m_type = SpecificType.Material_Vector;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif

    }
}