using System;

using UnityEngine;
using UnityEngine.UI;

namespace CodeBlaze.Detris {

    [RequireComponent(typeof(RawImage))]
    public class GradientTexture : MonoBehaviour {

        [SerializeField]
        private Color[] _colors;
        
        private Texture2D _bg;
        private RawImage _img;

        private void Awake() {
            _img = GetComponent<RawImage>();
            _bg = new Texture2D(1, _colors.Length) {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Bilinear
            };
            SetColor();
        }

        private void SetColor() {
            _bg.SetPixels(_colors);
            _bg.Apply();
            _img.texture = _bg;
        }

    }

}