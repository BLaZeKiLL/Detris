using System;

using CodeBlaze.Detris.Input;
using CodeBlaze.Detris.Settings;
using CodeBlaze.Detris.Util;

using DG.Tweening;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes.Input {

    public class ShapeRotationController : MonoBehaviour {

        private Shape _currentShape;
        
        private Vector3 _newCrossPosition;
        private Vector3 _newPosition;
        private Vector3 _origin;
        
        private void Start() {
            var _gridSize = SettingsProvider.Current.Settings.GridSize;
            _origin = new Vector3((float) _gridSize / 2, 0f, (float) _gridSize / 2);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            if (!Application.isPlaying) return;
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
#endif

        public void Rotation(SwipeDirection direction, TweenQueue tweenQueue) {
            var pivot = _currentShape.Behaviour.transform.parent;

            if (!CheckRotation(direction)) return;

            switch (direction) {
                case SwipeDirection.WEST:
                    _currentShape.Rotation += Vector3.up * 90;
                    if (Mathf.Approximately(_currentShape.Rotation.y, 360f)) _currentShape.Rotation = Vector3.zero;

                    break;
                case SwipeDirection.EAST:
                    _currentShape.Rotation += Vector3.up * -90;
                    if (Mathf.Approximately(_currentShape.Rotation.y, -360f)) _currentShape.Rotation = Vector3.zero;

                    break;
                default:
                    throw new ArgumentOutOfRangeException($"This should not happen : {direction}");
            }

            _currentShape.Position = _newPosition;
            _currentShape.CrossPosition = _newCrossPosition;

            tweenQueue.Add(pivot.DORotate(_currentShape.Rotation, SettingsProvider.Current.Settings.TweenDuration));
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