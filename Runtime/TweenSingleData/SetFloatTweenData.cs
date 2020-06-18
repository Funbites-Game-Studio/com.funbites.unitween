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
            AudioMixer_SetFloat,
            Material_FadeProperty,
            Material_FloatProperty,
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
        private string m_variableName = "";


        public override Type RequestedType {
            get {
                switch (m_type)
                {
                    case SpecificType.AudioMixer_SetFloat:
                        return typeof(AudioMixer);
                    case SpecificType.Material_FadeProperty:
                    case SpecificType.Material_FloatProperty:
                        return typeof(Material);
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
                    {
                        AudioMixer mixer = (AudioMixer)target;
                        result = mixer.DOSetFloat(m_variableName, m_targetValue, duration);
                        break;
                    }
                case SpecificType.Material_FadeProperty:
                    {
                        var material = (Material)target;
                        return material.DOFade(m_targetValue, m_variableName, duration);
                    }
                case SpecificType.Material_FloatProperty:
                    {
                        var material = (Material)target;
                        result = material.DOFloat(m_targetValue, m_variableName, duration);
                        break;
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
                case SpecificType.AudioMixer_SetFloat:
                    return type == typeof(AudioMixer);
                case SpecificType.Material_FadeProperty:
                case SpecificType.Material_FloatProperty:
                    return type == typeof(Material);
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

        [MenuItem("Assets/Create/Tween Data/Material/Fade Property")]
        private static void CreateMaterialFadePropertyAsset()
        {
            var newInstance = CreateInstance<SetFloatTweenData>();
            newInstance.m_type = SpecificType.Material_FadeProperty;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Material/Float Property")]
        private static void CreateMaterialFloatPropertyAsset()
        {
            var newInstance = CreateInstance<SetFloatTweenData>();
            newInstance.m_type = SpecificType.Material_FloatProperty;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif
    }
}