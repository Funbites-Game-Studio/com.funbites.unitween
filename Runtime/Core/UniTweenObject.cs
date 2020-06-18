namespace UniTween.Core
{
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    [System.Serializable]
    public class UniTweenObject
    {
        [OnValueChanged("NewOperation")]
        [Tooltip("Choose between one of four operations:\n\nAppend: adds a tween to play when the previous Append operation ends.\n\nAppendInterval: adds an interval (in seconds) between the previous and the next Append operation.\n\nAppendCallback: invokes the methods registered on the callback/UnityEvent when the previous Append operation ends.\n\nJoin: adds a tween to play at the same time of the previous Append operation.")]
        public TweenOperation operation;
        [ShowIf("IsTweenOperation")]
        //[OnValueChanged("NewTarget")]
        [Tooltip("To create a new TweenData right-click inside of any folder in your project, go to Create/TweenData and choose the kind of TweenData that modifies the component (or MonoBehaviour) you want to tween.")]
        public TweenData tweenData;
        [ShowIf("IsIntervalOperation")]
        public float interval;
        [ShowIf("IsIntervalOperation")]
        [Tooltip("A random value between these two specified values will be added to the value of \"Interval\".\n\nThe random value will NOT change between loops. It is set only when Play or Play Backwards is called.")]
        public Vector2 randomVariance;
        [ShowIf("IsCallbackOperation")]
        [HideReferenceObjectPicker]
        public UnityEvent unityEvent;
        [HideReferenceObjectPicker]
        [ShowIf("ShowTarget")]
        [HideLabel]
        [SerializeField]
        [OnValueChanged("OnTargetChange")]
        [ValidateInput("ValidateTarget", "Type of target must correspond to the Tween.", InfoMessageType.Error)]
        [HorizontalGroup("Target")]
        public UnityEngine.Object target;
        

        //private string tweenDataCurrentType;

        /// <summary>
        /// Calculates and returns a new interval based on the default interval plus its defined variance.
        /// </summary>
        /// <returns></returns>
        public float GetInterval()
        {
            return interval + Random.Range(randomVariance.x, randomVariance.y);
        }

        private bool ValidateTarget(UnityEngine.Object target)
        {
            if (target == null || tweenData == null) return true;
            return tweenData.ValidateType(target.GetType());
        }
        /*
        private void SetCurrentType()
        {
            if (tweenData != null)
                tweenDataCurrentType = tweenData.GetType().ToString();
        }
        */



#pragma warning disable IDE0051 // Remove unused private member. Note: These are used by Odin Attributes via strings.
        [Button]
        [ShowIf("IsTweenOperation")]
        [HorizontalGroup("Target")]
        private void UseSelectedGO()
        {
            target = UnityEditor.Selection.activeGameObject;
            OnTargetChange();
        }

        private void OnTargetChange()
        {
            if (!ValidateTarget(target))
            {
                if (target is GameObject)
                {
                    target = (target as GameObject).GetComponent(tweenData.RequestedType);
                }
            }
        }

        private bool ShowTarget()
        {
            return !IsTweenDataNull() && IsTweenOperation();
        }

        private bool IsTweenDataNull()
        {
            return tweenData == null;
        }

        private bool IsTweenOperation()
        {
            return operation != TweenOperation.AppendCallback && operation != TweenOperation.AppendInterval;
        }


        private bool IsCallbackOperation()
        {
            return operation == TweenOperation.AppendCallback;
        }

        private bool IsIntervalOperation()
        {
            return operation == TweenOperation.AppendInterval;
        }

        private void NewOperation()
        {
            if (operation == TweenOperation.AppendCallback && unityEvent == null)
            {
                unityEvent = new UnityEvent();
            }
        }
        /*
        private void NewTarget()
        {
            if (tweenDataCurrentType == null)
            {
                SetCurrentType();
            }

            if (target == null || (tweenData != null && tweenData.GetType().ToString() != tweenDataCurrentType))
            {
                SetCurrentType();
                SetNewTarget();
            }
        }
        */
#pragma warning restore IDE0051 // Remover membros privados não utilizados

        public enum TweenOperation
        {
            Append,
            AppendInterval,
            AppendCallback,
            Join
        }

        #region Wrapper
        public abstract class UniTweenTarget
        {
        }

        public class UniTweenTarget<T> : UniTweenTarget
        {
            [HideReferenceObjectPicker]
            [ListDrawerSettings(Expanded = true, AlwaysAddDefaultValue = true)]
            public List<T> components = new List<T>();
        }
        #endregion
    }
}
