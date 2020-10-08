using System;

using CodeBlaze.Detris.Input;

using DG.Tweening;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeRotationController : MonoBehaviour {

        [SerializeField] [Range(0.5f, 5f)] private float _rotationDuration = 1f;

        private Vector3 _rotation;

        public Shape CurrentShape { get; private set; }

        private void Start() {
            _rotation = Vector3.zero;
        }

        public void UpdateCurrentShape(Shape currentShape) {
            CurrentShape = currentShape;
        }

        public void Rotation(SwipeInputDetector.SwipeEventArgs e) {
            switch (SwipeHelpers.GetHorizontalDirection(e)) {
                case SwipeDirection.WEST:
                    _rotation += Vector3.up * 90;
                    if (Math.Abs(_rotation.y - 360) < float.Epsilon) _rotation = Vector3.zero;
                    transform.DORotate(_rotation, _rotationDuration);

                    break;
                case SwipeDirection.EAST:
                    _rotation += Vector3.up * -90;
                    if (Math.Abs(_rotation.y + 360) < float.Epsilon) _rotation = Vector3.zero;
                    transform.DORotate(_rotation, _rotationDuration);

                    break;
            }
        }

    }

}