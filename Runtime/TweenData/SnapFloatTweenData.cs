using DG.Tweening;
using System;
using UniTween.Core;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening.Core;
#if UNITY_EDITOR
using UnityEditor;
using Funbites.UnityUtils.Editor;
#endif

namespace UniTween.Tweens {
    public class SnapFloatTweenData : TweenData {
        public enum SpecificType {
            RectTransform_AnchorPosX,
            RectTransform_AnchorPosY
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.RectTransform_AnchorPosY;
        [SerializeField]
        private float m_targetValue = 0;
        [SerializeField]
        private bool m_useFrom = false;
        [SerializeField, ShowIf("m_useFrom")]
        private float m_fromValue = 0;
        [SerializeField]
        private bool m_snapping = false;

        public override Type RequestedType {
            get {
                switch (m_type) {
                    case SpecificType.RectTransform_AnchorPosX:
                    case SpecificType.RectTransform_AnchorPosY:
                        return typeof(RectTransform);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target) {
            TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> result;
            switch (m_type) {
                case SpecificType.RectTransform_AnchorPosX:
                    {
                        var rectTransform = (RectTransform)target;
                        return rectTransform.DOAnchorPosX(m_targetValue, duration, m_snapping);
                    }
                case SpecificType.RectTransform_AnchorPosY:
                    {
                        var rectTransform = (RectTransform)target;
                        return rectTransform.DOAnchorPosY(m_targetValue, duration, m_snapping);
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
                case SpecificType.RectTransform_AnchorPosX:
                case SpecificType.RectTransform_AnchorPosY:
                    return type == typeof(RectTransform);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        [Button]
        private void BuildCustomEaseFromTimeAndPosList(Vector2[] timeAndPos)
        {
            customEase = true;
            AnimationCurve newCurve = new AnimationCurve();
            m_useFrom = true;
            m_fromValue = timeAndPos[0].y;
            m_targetValue = timeAndPos[timeAndPos.Length - 1].y;
            foreach (var val in timeAndPos)
            {
                newCurve.AddKey(new Keyframe(val.x, Mathf.InverseLerp(m_fromValue, m_targetValue, val.y)));
            }
            curve = newCurve;
        }

        [MenuItem("Assets/Create/Tween Data/Canvas/RectTransform/Anchor Pos X")]
        private static void CreateRectTransformAnchorPosXAsset()
        {
            var newInstance = CreateInstance<SnapFloatTweenData>();
            newInstance.m_type = SpecificType.RectTransform_AnchorPosX;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Canvas/RectTransform/Anchor Pos Y")]
        private static void CreateRectTransformAnchorPosYAsset()
        {
            var newInstance = CreateInstance<SnapFloatTweenData>();
            newInstance.m_type = SpecificType.RectTransform_AnchorPosY;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif
    }
}