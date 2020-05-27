namespace UniTween.Core
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using UnityEngine;
    public abstract class TweenData : ScriptableObject
    {
        public float duration = 1;
        public float delay;
        public bool customEase;
        [HideIf("customEase")]
        public Ease ease;
        [ShowIf("customEase")]
        public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        /// <summary>
        /// This method should be overriden to return the Tween the TweenData 
        /// is configured to perform.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public virtual Tween GetTween(UnityEngine.Object target)
        {
            return null;
        }
        public abstract System.Type RequestedType { get; }

        internal abstract bool ValidateType(System.Type type);
    }
}
