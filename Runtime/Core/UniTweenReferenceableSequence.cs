using Sirenix.OdinInspector;
using UnityEngine;

namespace UniTween.Core {
    public class UniTweenReferenceableSequence : UniTweenSequencePlayer {
        [FoldoutGroup("Settings", true)]
        [Tooltip("To play this Sequence using Play(string id) in UniTweenSequenceCollection.")]
        [LabelText("ID")]
        [SerializeField]
        [Required]
        private string m_id = "TWEEN_SEQUENCE_ID";

        public string ID
        {
            get
            {
                return m_id;
            }
        }

        [FoldoutGroup("Settings", true)]
        [Required]
        [AssetsOnly]
        [SerializeField]
        private UniTweenSequenceCollection m_collection = null;


        private new void OnEnable() {
            m_collection.Add(this);
            base.OnEnable();
        }

        private new void OnDisable() {
            m_collection.Remove(this);
            base.OnDisable();

        }
        private static string DisplayNameFormat = "{0} : {1}";
        public new string DisplayName {
            get
            {
                return string.Format(DisplayNameFormat, ID, name);
            }
        }

    }
}
