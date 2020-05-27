using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniTween.Core {
    [CreateAssetMenu(menuName = "Tween Data/Sequence Collection",order = -100)]
    public class UniTweenSequenceCollection : ScriptableObject {
        [NonSerialized, ShowInInspector]
        private Dictionary<string, UniTweenReferenceableSequence> data = null;
        [NonSerialized]
        private bool isInitialized = false;

        internal void Add(UniTweenReferenceableSequence tween) {
            InitializeData();
            if (data.ContainsKey(tween.ID)) {
                Debug.LogException(new Exception("A tween with same ID is already registrered. ID=" + tween.ID), tween);
            } else {
                data.Add(tween.ID, tween);
            }
            
        }

        internal bool Remove(UniTweenReferenceableSequence tween) {
            InitializeData();
            return data.Remove(tween.ID);
        }

        /// <summary>
        /// Plays a UniTween Sequence based on an ID (defined on the UniTween Sequence component via inspector). 
        /// </summary>
        /// <param name="id">The ID defined on the UniTween Referencable Sequence component</param>
        public void Play(string id) {
            InitializeData();
            if (data.ContainsKey(id)) {
                data[id].TweenSequence.Play();
            } else {
                Debug.LogWarning("Referenceable tween not found: " + id);
            }
        }

        void InitializeData() {
            if (!isInitialized) {
                isInitialized = true;
                data = new Dictionary<string, UniTweenReferenceableSequence>();
            }
        }
    }
}