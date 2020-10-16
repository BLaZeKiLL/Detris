using System;

using CodeBlaze.Detris.Input;
using CodeBlaze.Detris.Settings;
using CodeBlaze.Detris.Util;

using DG.Tweening;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeMovementController : MonoBehaviour {

        private Shape _currentShape;

        public void Movement(SwipeDirection swipeDirection, TweenQueue tweenQueue) {
            var pivot = _currentShape.Behaviour.transform;

            Vector3 mov;

            switch (swipeDirection) {
                case SwipeDirection.NORTH:
                    mov = new Vector3(-1, 0, -1);

                    break;
                case SwipeDirection.NORTH_EAST:
                    mov = new Vector3(-1, 0, 0);

                    break;
                case SwipeDirection.EAST:
                    mov = new Vector3(-1, 0, 1);

                    break;
                case SwipeDirection.SOUTH_EAST:
                    mov = new Vector3(0, 0, 1);

                    break;
                case SwipeDirection.SOUTH:
                    mov = new Vector3(1, 0, 1);

                    break;
                case SwipeDirection.SOUTH_WEST:
                    mov = new Vector3(1, 0, 0);

                    break;
                case SwipeDirection.WEST:
                    mov = new Vector3(1, 0, -1);

                    break;
                case SwipeDirection.NORTH_WEST:
                    mov = new Vector3(0, 0, -1);

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(swipeDirection), swipeDirection, null);
            }

            var newPosition = _currentShape.Position + mov;
            var newCrossPosition = _currentShape.CrossPosition + mov;

            if (!ShapeExtensions.BoundCheck(newPosition, newCrossPosition)) {
                tweenQueue.Add(pivot.DOShakePosition(SettingsProvider.Current.Settings.TweenDuration, 0.1f));

                return;
            }

            _currentShape.Position = newPosition;
            _currentShape.CrossPosition = newCrossPosition;

            tweenQueue.Add(pivot.DOMove(pivot.position + mov, SettingsProvider.Current.Settings.TweenDuration));
        }

        public void UpdateShape(Shape shape) {
            _currentShape = shape;
        }

    }

}