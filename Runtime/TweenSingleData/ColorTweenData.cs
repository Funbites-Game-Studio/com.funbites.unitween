namespace UniTween.Tweens
{
    using DG.Tweening;
    using System;
    using UniTween.Core;
    using UnityEngine;
    using Sirenix.OdinInspector;
    using DG.Tweening.Core;
    using UnityEngine.UI;
#if UNITY_EDITOR
    using UnityEditor;
    using Funbites.UnityUtils.Editor;
#endif

    public class ColorTweenData : TweenData
    {
        public enum SpecificType
        {
            Camera_Color,
            Graphic_Color,
            Graphic_BlendableColor,
            Image_Color,
            Image_BlendableColor,
            Light_Color,
            Light_BlendableColor,
            Material_Color,
            Material_BlendableColor,
            Renderer_Color,
            Renderer_BlendableColor,
            Outline_Color
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.Camera_Color;
        [SerializeField]
        private Color m_targetValue = Color.black;
        [SerializeField]
        private bool m_useFrom = false;
        [SerializeField, ShowIf("m_useFrom")]
        private Color m_fromValue = Color.black;


        public override Type RequestedType {
            get {
                switch (m_type)
                {
                    case SpecificType.Camera_Color:
                        return typeof(Camera);
                    case SpecificType.Graphic_Color:
                    case SpecificType.Graphic_BlendableColor:
                        return typeof(Graphic);
                    case SpecificType.Image_Color:
                    case SpecificType.Image_BlendableColor:
                        return typeof(Image);
                    case SpecificType.Light_Color:
                    case SpecificType.Light_BlendableColor:
                        return typeof(Light);
                    case SpecificType.Material_Color:
                    case SpecificType.Material_BlendableColor:
                    case SpecificType.Renderer_Color:
                    case SpecificType.Renderer_BlendableColor:
                        return typeof(Renderer);
                    case SpecificType.Outline_Color:
                        return typeof(Outline);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target)
        {
            TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> result;
            switch (m_type)
            {
                case SpecificType.Camera_Color:
                    {
                        Camera camera = (Camera)target;
                        result = camera.DOColor(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Graphic_Color:
                    {
                        Graphic graphic = (Graphic)target;
                        result = graphic.DOColor(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Graphic_BlendableColor:
                    {
                        Graphic graphic = (Graphic)target;
                        return graphic.DOBlendableColor(m_targetValue, duration);
                    }
                case SpecificType.Image_Color:
                    {
                        var image = (Image)target;
                        result = image.DOColor(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Image_BlendableColor:
                    {
                        var image = (Image)target;
                        return image.DOBlendableColor(m_targetValue, duration);
                    }
                case SpecificType.Light_Color:
                    {
                        var light = (Light)target;
                        result = light.DOColor(m_targetValue, duration);
                        break;
                    }
                case SpecificType.Light_BlendableColor:
                    {
                        var light = (Light)target;
                        return light.DOBlendableColor(m_targetValue, duration);
                    }
                case SpecificType.Material_Color:
                    {
                        var renderer = (Renderer)target;
                        return renderer.sharedMaterial.DOColor(m_targetValue, duration);
                    }
                case SpecificType.Material_BlendableColor:
                    {
                        var renderer = (Renderer)target;
                        return renderer.sharedMaterial.DOBlendableColor(m_targetValue, duration);
                    }
                case SpecificType.Renderer_Color:
                    {
                        var renderer = (Renderer)target;
                        return renderer.material.DOColor(m_targetValue, duration);
                    }
                case SpecificType.Renderer_BlendableColor:
                    {
                        var renderer = (Renderer)target;
                        return renderer.material.DOBlendableColor(m_targetValue, duration);
                    }
                case SpecificType.Outline_Color:
                    {
                        var outline = (Outline)target;
                        result = outline.DOColor(m_targetValue, duration);
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
                case SpecificType.Camera_Color:
                    return type == typeof(Camera);
                case SpecificType.Graphic_Color:
                case SpecificType.Graphic_BlendableColor:
                    return type == typeof(Graphic);
                case SpecificType.Image_Color:
                case SpecificType.Image_BlendableColor:
                    return type == typeof(Image);
                case SpecificType.Light_Color:
                case SpecificType.Light_BlendableColor:
                    return type == typeof(Light);
                case SpecificType.Material_Color:
                case SpecificType.Material_BlendableColor:
                case SpecificType.Renderer_Color:
                case SpecificType.Renderer_BlendableColor:
                    return type == typeof(MeshRenderer) || type == typeof(SpriteRenderer);
                case SpecificType.Outline_Color:
                    return type == typeof(Outline);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tween Data/Camera/Color")]
        private static void CreateCameraColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Camera_Color;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Canvas/Graphic/Color")]
        private static void CreateGraphicColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Graphic_Color;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Canvas/Graphic/Blendable Color")]
        private static void CreateGraphicBlendableColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Graphic_BlendableColor;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Canvas/Image/Color")]
        private static void CreateImageColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Image_Color;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Canvas/Image/Blendable Color")]
        private static void CreateImageBlendableColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Image_BlendableColor;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Light/Color")]
        private static void CreateLightColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Light_Color;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Light/Blendable Color")]
        private static void CreateLightBlendableColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Light_BlendableColor;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Renderer/SharedMaterial Color")]
        private static void CreateRendererSharedMaterialColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Material_Color;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Renderer/SharedMaterial Blendable Color")]
        private static void CreateSharedMaterialBlendableColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Material_BlendableColor;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Renderer/Material Color")]
        private static void CreateMaterialColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Renderer_Color;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        [MenuItem("Assets/Create/Tween Data/Renderer/SharedMaterial Blendable Color")]
        private static void CreateMaterialBlendableColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Renderer_BlendableColor;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/Outline/Color")]
        private static void CreateOutlineColorAsset()
        {
            var newInstance = CreateInstance<ColorTweenData>();
            newInstance.m_type = SpecificType.Outline_Color;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
#endif
    }
}