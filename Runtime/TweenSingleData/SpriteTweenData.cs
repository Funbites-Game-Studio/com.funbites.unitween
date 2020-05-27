using DG.Tweening;
using System;
using UniTween.Core;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
using Funbites.UnityUtils.Editor;
#endif

namespace UniTween.Tweens {
    [Obsolete]
    public class SpriteTweenData : TweenData {
        public enum SpecificType {
            Image_Sprite,
            SpriteRenderer_Sprite
        }
        [SerializeField]
        private SpecificType m_type = SpecificType.Image_Sprite;
        [PreviewField(80), AssetSelector, SerializeField]
        private Sprite m_targetValue = null;


        public override Type RequestedType {
            get {
                switch (m_type) {
                    case SpecificType.Image_Sprite:
                        return typeof(Image);
                    case SpecificType.SpriteRenderer_Sprite:
                        return typeof(SpriteRenderer);
                    default:
                        return null;
                }
            }
        }

        public override Tween GetTween(UnityEngine.Object target) {
            throw new NotImplementedException("This Tween is obsolete");
            /*switch (m_type) {
                case SpecificType.Image_Sprite: {
                        Image img = (Image)target;
                        Sequence s = DOTween.Sequence();
                        s.AppendCallback(() => img.sprite = m_targetValue);
                        s.AppendInterval(duration);
                        return s;
                    }
                case SpecificType.SpriteRenderer_Sprite: {
                        SpriteRenderer spriteRenderer = (SpriteRenderer)target;
                        Sequence s = DOTween.Sequence();
                        s.AppendCallback(() => spriteRenderer.sprite = m_targetValue);
                        s.AppendInterval(duration);
                        return s;
                    }
                default:
                    return null;
            }*/
        }

        internal override bool ValidateType(Type type) {
            switch (m_type) {
                case SpecificType.Image_Sprite:
                    return type == typeof(Image);
                case SpecificType.SpriteRenderer_Sprite:
                    return type == typeof(SpriteRenderer);
                default:
                    return false;
            }
        }

#if UNITY_EDITOR
        /*
        [MenuItem("Assets/Create/Tween Data/Canvas/Image/Sprite")]
        private static void CreateImageSpriteAsset() {
            var newInstance = CreateInstance<SpriteTweenData>();
            newInstance.m_type = SpecificType.Image_Sprite;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }

        [MenuItem("Assets/Create/Tween Data/SpriteRenderer/Sprite")]
        private static void CreateSpriteRendererSpriteAsset() {
            var newInstance = CreateInstance<SpriteTweenData>();
            newInstance.m_type = SpecificType.SpriteRenderer_Sprite;
            CustomCreateAsset.CreateScriptableAssetInCurrentSelection(newInstance, newInstance.m_type.ToString());
        }
        */
#endif
    }
}