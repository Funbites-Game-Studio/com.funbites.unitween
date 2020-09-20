namespace UniTween.DOTweenModules
{
    using DG.Tweening;
    using DG.Tweening.Core;
    using DG.Tweening.Plugins.Options;
    using System;
    using TMPro;
    using UnityEngine.UI;

    public static class DOTweenModuleExtension

    {
        public static TweenerCore<int, int, NoOptions> DOFormatNumericToString(
            this TMP_Text target, int fromValue, int endValue, float duration, Func<int, string> toString)
        {
            int v = fromValue;
            TweenerCore<int, int, NoOptions> t = DOTween.To(() => v, x => {
                v = x;
                target.text = toString.Invoke(v);
            }, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        public static TweenerCore<int, int, NoOptions> DOFormatNumericToString(
            this Text target, int fromValue, int endValue, float duration, Func<int, string> toString)
        {
            int v = fromValue;
            TweenerCore<int, int, NoOptions> t = DOTween.To(() => v, x => {
                v = x;
                target.text = toString.Invoke(v);
            }, endValue, duration);
            t.SetTarget(target);
            return t;
        }

    }
}