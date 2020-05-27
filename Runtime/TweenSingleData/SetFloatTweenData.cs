using DG.Tweening;
using System;
using UniTween.Core;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening.Core;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
using Funbites.UnityUtils.Editor;
#endif

namespace UniTween.Tweens
{
    public class SetFloatTweenData : TweenData
    {
        public enum SpecificType
        {
            AudioMixer_SetFloat
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.AudioMixer_SetFloat;
        [SerializeField]
        private float m_targetValue = 0;
        [SerializeField]
        private bool m_useFrom = false;
        [SerializeField, ShowIf("m_useFrom")]
        private float m_fromValue = 0;
        [SerializeField]
        private string m_floatName;


        public override Type RequestedType {
            get {
                switch (m_type)
                {
                    case SpecificType.AudioMixer_SetFloat:
                        return typeof(AudioMixer);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target)
        {
            TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> result;
            switch (m_type)
            {
                case SpecificType.AudioMixer_SetFloat:
                        AudioMixer mixer = (AudioMixer)target;
                        result = mixer.DOSetFloat(m_floatName, m_targetValue, duration);
                break;
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
                case SpecificType.AudioMixer_SetFloat:
                    return type == typeof(AudioMixer);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tween Data/Audio/Mixer/SetFloat")]
        private static void CreateAudioMixerSetFloatAsset()
        {
            var newInstance = CreateInstance<SetFloatTweenData>();
            newInstance.m_type = SpecificType.AudioMixer_SetFloat;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif
    }
}