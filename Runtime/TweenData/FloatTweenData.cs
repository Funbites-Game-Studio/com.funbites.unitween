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
    public class FloatTweenData : TweenData {
        public enum SpecificType {
            Image_Fade,
            Image_FillAmount,
            CanvasGroup_Fade,
            Camera_Aspect,
            Camera_FarClipPlane,
            Camera_FieldOfView,
            Camera_NearClipPlane,
            Camera_OrthoSize,
            AudioSource_Fade,
            AudioSource_Pitch,
            Graphic_Fade,
            Light_Intensity,
            Light_ShadowStrength,
            Material_Fade,
            Outline_Fade
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.Image_Fade;
        [SerializeField]
        private float m_targetValue = 0;
        [SerializeField]
        private bool m_useFrom = false;
        [SerializeField, ShowIf("m_useFrom")]
        private float m_fromValue = 0;


        public override Type RequestedType {
            get {
                switch (m_type) {
                    case SpecificType.CanvasGroup_Fade:
                        return typeof(CanvasGroup);
                    case SpecificType.Image_Fade:
                    case SpecificType.Image_FillAmount:
                        return typeof(Image);
                    case SpecificType.Camera_Aspect:
                    case SpecificType.Camera_FarClipPlane:
                    case SpecificType.Camera_FieldOfView:
                    case SpecificType.Camera_OrthoSize:
                    case SpecificType.Camera_NearClipPlane:
                        return typeof(Camera);
                    case SpecificType.AudioSource_Fade:
                    case SpecificType.AudioSource_Pitch:
                        return typeof(AudioSource);
                    case SpecificType.Graphic_Fade:
                        return typeof(Graphic);
                    case SpecificType.Light_Intensity:
                    case SpecificType.Light_ShadowStrength:
                        return typeof(Light);
                    case SpecificType.Material_Fade:
                        return typeof(Material);
                    case SpecificType.Outline_Fade:
                        return typeof(Outline);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target) {
            TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> result;
            switch (m_type) {
                case SpecificType.Image_Fade: {
                        Image img = (Image)target;
                        var colorResult = img.DOFade(m_targetValue, duration);
                        if (m_useFrom) {
                            colorResult.From(new Color(img.color.r, img.color.g, img.color.b, m_fromValue), false);
                        }
                        return colorResult;
                    }
                case SpecificType.Image_FillAmount: {
                        Image img = (Image)target;
                        result = img.DOFillAmount(m_targetValue, duration);
                        break;
                    }
                case SpecificType.CanvasGroup_Fade: {
                        CanvasGroup canvasGroup = (CanvasGroup)target;
                        result = canvasGroup.DOFade(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Camera_Aspect:
                    {
                        Camera camera = (Camera)target;
                        result = camera.DOAspect(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Camera_FarClipPlane:
                    {
                        Camera camera = (Camera)target;
                        result = camera.DOFarClipPlane(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Camera_FieldOfView:
                    {
                        Camera camera = (Camera)target;
                        result = camera.DOFieldOfView(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Camera_NearClipPlane:
                    {
                        Camera camera = (Camera)target;
                        result = camera.DONearClipPlane(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Camera_OrthoSize:
                    {
                        Camera camera = (Camera)target;
                        result = camera.DOOrthoSize(m_targetValue, duration);
                        break;
                    }
                case SpecificType.AudioSource_Fade:
                    {
                        AudioSource audioSource = (AudioSource)target;
                        result = audioSource.DOFade(m_targetValue, duration);
                        break;
                    }
                case SpecificType.AudioSource_Pitch:
                    {
                        AudioSource audioSource = (AudioSource)target;
                        result = audioSource.DOPitch(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Graphic_Fade:
                    {
                        var graphic = (Graphic)target;
                        return graphic.DOFade(m_targetValue, duration);
                    }
                case SpecificType.Light_Intensity:
                    {
                        var light = (Light)target;
                        return light.DOIntensity(m_targetValue, duration);
                    }
                case SpecificType.Light_ShadowStrength:
                    {
                        var light = (Light)target;
                        return light.DOShadowStrength(m_targetValue, duration);
                    }
                case SpecificType.Material_Fade:
                    {
                        var material = (Material)target;
                        return material.DOFade(m_targetValue, duration);
                    }
                case SpecificType.Outline_Fade:
                    {
                        var outline = (Outline)target;
                        return outline.DOFade(m_targetValue, duration);
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
                case SpecificType.Image_Fade:
                case SpecificType.Image_FillAmount:
                    return type == typeof(Image);
                case SpecificType.CanvasGroup_Fade:
                    return type == typeof(CanvasGroup);
                case SpecificType.Camera_Aspect:
                case SpecificType.Camera_FarClipPlane:
                case SpecificType.Camera_FieldOfView:
                case SpecificType.Camera_OrthoSize:
                case SpecificType.Camera_NearClipPlane:
                    return type == typeof(Camera);
                case SpecificType.AudioSource_Fade:
                case SpecificType.AudioSource_Pitch:
                    return type == typeof(Camera);
                case SpecificType.Graphic_Fade:
                    return type == typeof(Graphic);
                case SpecificType.Light_Intensity:
                case SpecificType.Light_ShadowStrength:
                    return type == typeof(Light);
                case SpecificType.Material_Fade:
                    return type == typeof(Material);
                case SpecificType.Outline_Fade:
                    return type == typeof(Outline);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tween Data/Canvas/Image/Fade")]
        private static void CreateImageFadeAsset() {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Image_Fade;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Canvas/Image/FillAmount")]
        private static void CreateImageFillAmountAsset() {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Image_FillAmount;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Canvas/CanvasGroup/Fade")]
        private static void CreateCanvasGroupFadeAsset() {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.CanvasGroup_Fade;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Camera/Aspect")]
        private static void CreateCameraAspectAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Camera_Aspect;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Camera/Far Clip Plane")]
        private static void CreateCameraFarClipPlaneAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Camera_FarClipPlane;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Camera/Near Clip Plane")]
        private static void CreateCameraNearClipPlaneAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Camera_NearClipPlane;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Camera/Field Of View")]
        private static void CreateCameraFieldOfViewAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Camera_FieldOfView;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Camera/Orthografic Size")]
        private static void CreateCameraOrthoSizeAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Camera_OrthoSize;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Audio/Source/Fade")]
        private static void CreateAudioSourceFadeAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.AudioSource_Fade;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Audio/Source/Pitch")]
        private static void CreateAudioPitchFadeAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.AudioSource_Pitch;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Canvas/Graphic/Fade")]
        private static void CreateGraphicFadeAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Graphic_Fade;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Light/Intensity")]
        private static void CreateLightIntensityAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Light_Intensity;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Light/ShadowStrenght")]
        private static void CreateLightShadowStrenghtAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Light_ShadowStrength;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Material/Fade")]
        private static void CreateMaterialFadeAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Material_Fade;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Outline/Fade")]
        private static void CreateOutlineFadeAsset()
        {
            var newInstance = CreateInstance<FloatTweenData>();
            newInstance.m_type = SpecificType.Outline_Fade;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif
    }
}