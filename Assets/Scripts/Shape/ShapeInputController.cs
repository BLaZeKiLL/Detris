using System;

using CodeBlaze.Detris.Input;
using CodeBlaze.Detris.Settings;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeInputController : MonoBehaviour {

        private ShapeMovementController _shapeMovementController;
        private ShapeRotationController _shapeRotationController;

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
            _shapeRotationController.UpdateShape(currentShape);
            _shapeMovementController.UpdateShape(currentShape);
        }

        private void OnSwipe(object sender, SwipeInputDetector.SwipeEventArgs e) {
            if (SwipeHelpers.MeanY(e) / Screen.height > SettingsProvider.Current.Settings.ScreenSplit) {
                var swipeDirection = SwipeHelpers.GetOctalDirection(e);
                
                _shapeMovementController.Movement(swipeDirection);
            } else {
                var swipeDirection = SwipeHelpers.GetHorizontalDirection(e);
                
                _shapeRotationController.Rotation(swipeDirection);
            }
        }

    }

}