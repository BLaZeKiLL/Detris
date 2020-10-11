using CodeBlaze.Detris.Input;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeInputController : MonoBehaviour {

        [SerializeField] [Range(0.2f, 0.5f)] private float _screenYSplit = 0.3f;
        
        public Shape CurrentShape { get; private set; }
        
        private ShapeMovementController _shapeMovementController;
        private ShapeRotationController _shapeRotationController;

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

        public void UpdateCurrentShape(Shape currentShape) {
            CurrentShape = currentShape;
            
            UnityEngine.Debug.Log($"Position : {CurrentShape.Position} : Cross Position : {CurrentShape.CrossPosition}");
        }

        public void setGridSize(int gridSize) => _gridSize = gridSize;

        private void OnSwipe(object sender, SwipeInputDetector.SwipeEventArgs e) {
            if (SwipeHelpers.MeanY(e) / Screen.height > _screenYSplit) {
                var swipeDirection = SwipeHelpers.GetOctalDirection(e);
                
                _shapeMovementController.Movement(swipeDirection);
            } else {
                var swipeDirection = SwipeHelpers.GetHorizontalDirection(e);
                
                _shapeRotationController.Rotation(swipeDirection);
            }
        }

    }

}