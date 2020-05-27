using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UniTweens.SpriteAnimation {
    [RequireComponent(typeof(Image))]
    public class ImageSpriteAnimation : MonoBehaviour {
        private Image image;
        [ReadOnly]
        public float IndexAcc;

        public Sprite Sprite {
            get => image.sprite;
            set {
                image.sprite = value;
            }
        }


        private void Awake() {
            image = GetComponent<Image>();
        }


    }
}