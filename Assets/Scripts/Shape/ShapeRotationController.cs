using System;

using CodeBlaze.Detris.Input;
using CodeBlaze.Detris.Settings;
using CodeBlaze.Detris.Util;

using DG.Tweening;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeRotationController : MonoBehaviour {

        private Shape _currentShape;
        private Vector3 _newCrossPosition;

        private Vector3 _newPosition;
        private Vector3 _origin;

        private Vector3 _rotation;

        private void Start() {
            _rotation = Vector3.zero;

            var _gridSize = SettingsProvider.Current.Settings.GridSize;
            _origin = new Vector3((float) _gridSize / 2, 0f, (float) _gridSize / 2);
        }

        private void OnDrawGizmos() {
            if (_currentShape == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                _origin,
                _currentShape.Position
            );

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(
                _origin,
                _currentShape.CrossPosition
            );
        }

        public void Rotation(SwipeDirection direction, TweenQueue tweenQueue) {
            var pivot = _currentShape.Behaviour.transform.parent;

            if (!CheckRotation(direction)) return;

            switch (direction) {
                case SwipeDirection.WEST:
                    _rotation += Vector3.up * 90;
                    if (Math.Abs(_rotation.y - 360) < float.Epsilon) _rotation = Vector3.zero;

                    break;
                case SwipeDirection.EAST:
                    _rotation += Vector3.up * -90;
                    if (Math.Abs(_rotation.y + 360) < float.Epsilon) _rotation = Vector3.zero;

                    break;
                default:
                    throw new ArgumentOutOfRangeException($"This should not happen : {direction}");
            }

            _currentShape.Position = _newPosition;
            _currentShape.CrossPosition = _newCrossPosition;

            tweenQueue.Add(pivot.DORotate(_rotation, SettingsProvider.Current.Settings.TweenDuration));
        }

        public void UpdateShape(Shape shape) {
            _currentShape = shape;
        }

        private bool CheckRotation(SwipeDirection direction) {
            Quaternion rot;

            switch (direction) {
                case SwipeDirection.EAST:
                    rot = Quaternion.Euler(0, -90, 0);

                    break;
                case SwipeDirection.WEST:
                    rot = Quaternion.Euler(0, 90, 0);

                    break;
                default:
                    throw new ArgumentOutOfRangeException($"This should not happen : {direction}");
            }

            _newPosition = rot * (_currentShape.Position - _origin) + _origin;
            _newCrossPosition = rot * (_currentShape.CrossPosition - _origin) + _origin;

            return ShapeExtensions.BoundCheck(_newPosition, _newCrossPosition);
        }

    }

}