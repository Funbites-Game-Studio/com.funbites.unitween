namespace UniTween.Tweens
{
    using DG.Tweening;
    using DG.Tweening.Core;
    using Sirenix.OdinInspector;
    using System;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using UniTween.DOTweenModules;
#if UNITY_EDITOR
    using UnityEditor;
    using Funbites.UnityUtils.Editor;
    using System.Text;
    using Funbites.UnityUtils;
#endif

    public class IntegerTweenData : TweenData
    {
        public enum SpecificType
        {
            TMP_Text_TimeFormat,
            Text_TimeFormat
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.TMP_Text_TimeFormat;
        [SerializeField]
        private int m_targetValue = 0;
        [SerializeField]
        private bool m_useFrom = false;
        [SerializeField, ShowIf("m_useFrom")]
        private int m_fromValue = 0;
        public override Type RequestedType {
            get {
                switch (m_type)
                {
                    case SpecificType.TMP_Text_TimeFormat:
                        return typeof(TMP_Text);
                    case SpecificType.Text_TimeFormat:
                        return typeof(Text);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target)
        {
            TweenerCore<int, int, DG.Tweening.Plugins.Options.NoOptions> result;
            switch (m_type)
            {
                case SpecificType.TMP_Text_TimeFormat:
                    {
                        var text = (TMP_Text)target;
                        return text.DOFormatNumericToString(m_fromValue, m_targetValue, duration, StringUtils.FormatMinutesToClockTime);
                    }
                case SpecificType.Text_TimeFormat:
                    {
                        var text = (Text)target;
                        return text.DOFormatNumericToString(m_fromValue, m_targetValue, duration, StringUtils.FormatMinutesToClockTime);
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
                case SpecificType.TMP_Text_TimeFormat:
                    return type == typeof(TMP_Text);
                case SpecificType.Text_TimeFormat:
                    return type == typeof(Text);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tween Data/Canvas/Text/Time Format")]
        private static void CreateTextTimeFormatAsset()
        {
            var newInstance = CreateInstance<IntegerTweenData>();
            newInstance.m_type = SpecificType.Text_TimeFormat;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif

    }
}