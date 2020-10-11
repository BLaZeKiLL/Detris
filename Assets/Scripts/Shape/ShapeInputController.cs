using System;

using CodeBlaze.Detris.Input;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeInputController : MonoBehaviour {

        [SerializeField] [Range(0.2f, 0.5f)] private float _screenYSplit = 0.3f;
        
        public Shape CurrentShape { get; private set; }
        
        private ShapeMovementController _shapeMovementController;
        private ShapeRotationController _shapeRotationController;

        private Vector2 _origin;
        
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
            
            UnityEngine.Debug.DrawLine(
                new Vector3(_origin.x, 0 ,_origin.y), 
                new Vector3(CurrentShape.Position.x, 0f, CurrentShape.Position.y),
                Color.red
            );
            
            UnityEngine.Debug.DrawLine(
                new Vector3(_origin.x, 0 ,_origin.y), 
                new Vector3(CurrentShape.CrossPosition.x, 0f, CurrentShape.CrossPosition.y),
                Color.cyan
            );
        }

        public void UpdateCurrentShape(Shape currentShape) {
            CurrentShape = currentShape;
            
            UnityEngine.Debug.Log($"Position : {CurrentShape.Position} : Cross Position : {CurrentShape.CrossPosition}");
        }

        public void setGridSize(int gridSize) => _origin = new Vector2((float) gridSize/ 2, (float) gridSize/ 2);

        private void OnSwipe(object sender, SwipeInputDetector.SwipeEventArgs e) {
            if (SwipeHelpers.MeanY(e) / Screen.height > _screenYSplit) {
                var swipeDirection = SwipeHelpers.GetOctalDirection(e);
                
                _shapeMovementController.Movement(swipeDirection);
            } else {
                var swipeDirection = SwipeHelpers.GetHorizontalDirection(e);
                CheckRotation(swipeDirection);
                _shapeRotationController.Rotation(swipeDirection);
            }
        }

        private bool CheckRotation(SwipeDirection direction) {
            var positionVec = CurrentShape.Position - _origin;
            var crossPositionVec = CurrentShape.CrossPosition - _origin;
            
            switch (direction) {
                case SwipeDirection.EAST:
                    CurrentShape.Position = Quaternion.Euler(0f, 0f, 90) * positionVec;
                    CurrentShape.CrossPosition = Quaternion.Euler(0f, 0f, 90) * crossPositionVec;

                    UnityEngine.Debug.Log($"Position : {CurrentShape.Position} Cross Position : {CurrentShape.CrossPosition}");
                    return false;
                case SwipeDirection.WEST:
                    CurrentShape.Position = Quaternion.Euler(0f, 0f, -90) * positionVec;
                    CurrentShape.CrossPosition = Quaternion.Euler(0f, 0f, -90) * crossPositionVec;
                    
                    UnityEngine.Debug.Log($"Position : {CurrentShape.Position} Cross Position : {CurrentShape.CrossPosition}");
                    return false;
            }
            
            throw new InvalidProgramException($"This should not happen, Direction : {direction}");
        }

    }

}