using System;

using CodeBlaze.Detris.Input;

using DG.Tweening;

using UnityEngine;

namespace CodeBlaze.Detris.Camera {

    public class SceneRootController : MonoBehaviour {

        [SerializeField] [Range(0.5f, 5f)] private float _rotationDuration = 1f;

        private Vector3 _rotation;

        private void Start() {
            _rotation = Vector3.zero;
            TouchInputManager.SwipeRight += SwipeRight;
            TouchInputManager.SwipeLeft += SwipeLeft;
        }

        private void OnDestroy() {
            TouchInputManager.SwipeRight -= SwipeRight;
            TouchInputManager.SwipeLeft -= SwipeLeft;
        }

        private void SwipeLeft(object sender, TouchInputManager.SwipeEventArgs e) {
            _rotation += Vector3.up * -90;
            if (Math.Abs(_rotation.y + 360) < float.Epsilon) _rotation = Vector3.zero;
            transform.DORotate(_rotation, _rotationDuration);
        }

        private void SwipeRight(object sender, TouchInputManager.SwipeEventArgs e) {
            _rotation += Vector3.up * 90;
            if (Math.Abs(_rotation.y - 360) < float.Epsilon) _rotation = Vector3.zero;
            transform.DORotate(_rotation, _rotationDuration);
        }

    }

}