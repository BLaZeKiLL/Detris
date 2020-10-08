using CodeBlaze.Detris.Input;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeInputController : MonoBehaviour {

        [SerializeField] [Range(0.2f, 0.5f)] private float _screenYSplit = 0.3f;
        private ShapeMovementController _shapeMovementController;

        private ShapeRotationController _shapeRotationController;

        public Shape CurrentShape { get; private set; }

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
            _shapeMovementController.UpdateCurrentShape(CurrentShape);
            _shapeRotationController.UpdateCurrentShape(CurrentShape);
        }

        private void OnSwipe(object sender, SwipeInputDetector.SwipeEventArgs e) {
            if (SwipeHelpers.MeanY(e) / Screen.height > _screenYSplit)
                _shapeMovementController.Movement(e);
            else
                _shapeRotationController.Rotation(e);
        }

    }

}