using System;

using CodeBlaze.Detris.Input;
using CodeBlaze.Detris.Settings;

using DG.Tweening;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeRotationController : MonoBehaviour {
        private Vector3 _rotation;

        private void Start() {
            _rotation = Vector3.zero;
        }

        public void Rotation(Shape shape, SwipeDirection swipeDirection) {
            var pivot = shape.Behaviour.transform.parent;
            
            switch (swipeDirection) {
                case SwipeDirection.WEST:
                    _rotation += Vector3.up * 90;
                    if (Math.Abs(_rotation.y - 360) < float.Epsilon) _rotation = Vector3.zero;
                    pivot.DORotate(_rotation, SettingsProvider.Current.Settings.TweenDuration);

                    break;
                case SwipeDirection.EAST:
                    _rotation += Vector3.up * -90;
                    if (Math.Abs(_rotation.y + 360) < float.Epsilon) _rotation = Vector3.zero;
                    pivot.DORotate(_rotation, SettingsProvider.Current.Settings.TweenDuration);

                    break;
            }
        }

    }

}