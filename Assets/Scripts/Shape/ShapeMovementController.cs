using System;

using CodeBlaze.Detris.Input;

using DG.Tweening;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeMovementController : MonoBehaviour {

        [SerializeField] [Range(0.5f, 5f)] private float _movementDuration = 1f;
        
        public void Movement(Shape shape, SwipeDirection swipeDirection) {
            var pivot = shape.Behaviour.transform.parent;
            
            switch (swipeDirection) {
                case SwipeDirection.NORTH:
                    pivot.DOMove(pivot.position + new Vector3(-1, 0, -1), _movementDuration);
                    break;
                case SwipeDirection.NORTH_EAST:
                    pivot.DOMove(pivot.position + new Vector3(-1, 0, 0), _movementDuration);
                    break;
                case SwipeDirection.EAST:
                    pivot.DOMove(pivot.position + new Vector3(-1, 0, 1), _movementDuration);
                    break;
                case SwipeDirection.SOUTH_EAST:
                    pivot.DOMove(pivot.position + new Vector3(0, 0, 1), _movementDuration);
                    break;
                case SwipeDirection.SOUTH:
                    pivot.DOMove(pivot.position + new Vector3(1, 0, 1), _movementDuration);
                    break;
                case SwipeDirection.SOUTH_WEST:
                    pivot.DOMove(pivot.position + new Vector3(1, 0, 0), _movementDuration);
                    break;
                case SwipeDirection.WEST:
                    pivot.DOMove(pivot.position + new Vector3(1, 0, -1), _movementDuration);
                    break;
                case SwipeDirection.NORTH_WEST:
                    pivot.DOMove(pivot.position + new Vector3(0, 0, -1), _movementDuration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(swipeDirection), swipeDirection, null);
            }
        }

    }

}