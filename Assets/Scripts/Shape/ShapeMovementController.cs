using System;

using CodeBlaze.Detris.Input;
using CodeBlaze.Detris.Settings;

using DG.Tweening;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeMovementController : MonoBehaviour {
        
        public void Movement(Shape shape, SwipeDirection swipeDirection) {
            var pivot = shape.Behaviour.transform.parent;

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
            
            var newPosition = shape.Position + mov;
            var newCrossPosition = shape.CrossPosition + mov;

            pivot.DOMove(pivot.position + mov, SettingsProvider.Current.Settings.TweenDuration);
        }

    }

}