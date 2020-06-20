
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

    public class Vector3TweenData : TweenData {
        public enum SpecificType {
            Transform_Move,
            Transform_Rotate,
            Transform_Rotate_Beyond360
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.Transform_Move;
        [SerializeField]
        private Vector3 m_targetValue = Vector3.zero;
        [SerializeField]
        private bool m_useFrom = false;
        [SerializeField, ShowIf("m_useFrom")]
        private Vector3 m_fromValue = Vector3.zero;

        public override Type RequestedType {
            get {
                switch (m_type) {
                    case SpecificType.Transform_Move:
                    case SpecificType.Transform_Rotate:
                    case SpecificType.Transform_Rotate_Beyond360:
                        return typeof(Transform);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target) {
            TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> result;
            switch (m_type) {
                case SpecificType.Transform_Move: {
                        Transform transform = (Transform)target;
                        result = transform.DOMove(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Transform_Rotate: {
                        Transform transform = (Transform)target;
                        return TweenToQuaternionRotation(transform, RotateMode.Fast);
                    }
                case SpecificType.Transform_Rotate_Beyond360:
                    {
                        Transform transform = (Transform)target;
                        return TweenToQuaternionRotation(transform, RotateMode.FastBeyond360);
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

        private TweenerCore<Quaternion, Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> TweenToQuaternionRotation(Transform transform, RotateMode rotateMode)
        {
            var rotateResult = transform.DORotate(m_targetValue, duration, rotateMode);
            if (m_useFrom)
            {
                rotateResult.From(m_fromValue, false);
            }
            return rotateResult;
        }


        internal override bool ValidateType(Type type) {
            switch (m_type) {
                case SpecificType.Transform_Move:
                case SpecificType.Transform_Rotate:
                case SpecificType.Transform_Rotate_Beyond360:
                    return type == typeof(Transform) || type == typeof(RectTransform);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tween Data/Transform/Move")]
        private static void CreateTransformMoveAsset() {
            var newInstance = CreateInstance<Vector3TweenData>();
            newInstance.m_type = SpecificType.Transform_Move;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Transform/Rotate")]
        private static void CreateTransformRotateAsset() {
            var newInstance = CreateInstance<Vector3TweenData>();
            newInstance.m_type = SpecificType.Transform_Rotate;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif

    }
}