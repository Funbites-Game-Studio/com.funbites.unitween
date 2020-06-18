namespace UniTween.Tweens
{
    using DG.Tweening;
    using System;
    using UniTween.Core;
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
    using Funbites.UnityUtils.Editor;
#endif

    [CreateAssetMenu(menuName = "Tween Data/Line Renderer")]
    public class LineRendererTweenData : TweenData
    {
        [SerializeField]
        private Color m_startColorA = Color.white;
        [SerializeField]
        private Color m_startColorB = Color.white;
        [SerializeField]
        private Color m_endColorA = Color.black;
        [SerializeField]
        private Color m_endColorB = Color.black;

        public override Type RequestedType {
            get {
                return typeof(LineRenderer);
            }
        }

        public override Tween GetTween(UnityEngine.Object target)
        {
            LineRenderer line = (LineRenderer)target;
            return line.DOColor(new Color2(m_startColorA, m_startColorB), new Color2(m_endColorA, m_endColorB), duration);
        }

        internal override bool ValidateType(Type type)
        {
            return type == typeof(LineRenderer);
        }
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tween Data/Line Renderer/Colors")]
        private static void CreateLineRendererColorsAsset()
        {
            var newInstance = CreateInstance<LineRendererTweenData>();
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, "LineRenderer_Colors");
        }
#endif
    }
}