using System;

using CodeBlaze.Detris.Input;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeInputController : MonoBehaviour {

        [SerializeField] [Range(0.2f, 0.5f)] private float _screenYSplit = 0.3f;
        
        public Shape CurrentShape { get; private set; }
        
        private ShapeMovementController _shapeMovementController;
        private ShapeRotationController _shapeRotationController;

        private Vector3 _origin;
        private int _gridSize;
        
        private void Awake() {
            _shapeRotationController = GetComponent<ShapeRotationController>();
            _shapeMovementController = GetComponent<ShapeMovementController>();
        }

        private void Start() {
            SwipeInputDetector.Swipe += OnSwipe;
        }

        private void OnDestroy() {
            SwipeInputDetector.Swipe -= OnSwipe;
        }
        
        private void OnDrawGizmos() {
            if (CurrentShape == null) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                _origin, 
                CurrentShape.Position
            );
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(
                _origin, 
                CurrentShape.CrossPosition
            );
        }

        public void UpdateCurrentShape(Shape currentShape) {
            CurrentShape = currentShape;
        }

        public void setGridSize(int gridSize) {
            _gridSize = gridSize;
            _origin = new Vector3((float) _gridSize/ 2, 0f, (float) _gridSize/ 2);
        }

        private void OnSwipe(object sender, SwipeInputDetector.SwipeEventArgs e) {
            if (SwipeHelpers.MeanY(e) / Screen.height > _screenYSplit) {
                var swipeDirection = SwipeHelpers.GetOctalDirection(e);
                
                _shapeMovementController.Movement(swipeDirection);
            } else {
                var swipeDirection = SwipeHelpers.GetHorizontalDirection(e);
                
                if (CheckRotation(swipeDirection)) _shapeRotationController.Rotation(swipeDirection);
            }
        }

        private bool CheckRotation(SwipeDirection direction) {
            var newPosition = CurrentShape.Position - _origin;
            var newCrossPosition = CurrentShape.CrossPosition - _origin;

            Quaternion rot;
            
            switch (direction) {
                case SwipeDirection.EAST:
                    rot = Quaternion.Euler(0, -90, 0);
                    break;
                case SwipeDirection.WEST:
                    rot = Quaternion.Euler(0, 90, 0);
                    break;
                default:
                    throw new InvalidProgramException($"This should not happen : {direction}");
            }
            
            newPosition = rot * newPosition;
            newCrossPosition = rot * newCrossPosition;
            
            newPosition += _origin;
            newCrossPosition += _origin;

            var check = BoundCheck(newPosition, newCrossPosition);

            if (!check) return false;

            CurrentShape.Position = newPosition;
            CurrentShape.CrossPosition = newCrossPosition;

            return true;
        }

        private bool BoundCheck(Vector3 newPosition, Vector3 newCrossPosition) {
            if (newPosition.x > _gridSize || newPosition.z > _gridSize) return false;
            if (newCrossPosition.x > _gridSize || newCrossPosition.z > _gridSize) return false;
            
            return true;
        }

    }

}