﻿using CodeBlaze.Detris.Input;
using CodeBlaze.Detris.Settings;
using CodeBlaze.Detris.Util;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes.Input {

    public class ShapeInputController : MonoBehaviour {

        private ShapeMovementController _shapeMovementController;
        private ShapeRotationController _shapeRotationController;

        private TweenQueue _tweenQueue;

        private void Awake() {
            _tweenQueue = new TweenQueue();

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
            _tweenQueue.Clear();

            _shapeRotationController.UpdateShape(currentShape);
            _shapeMovementController.UpdateShape(currentShape);
        }

        private void OnSwipe(object sender, SwipeInputDetector.SwipeEventArgs e) {
            if (SwipeHelpers.MeanY(e) / Screen.height > SettingsProvider.Current.Settings.ScreenSplit) {
                var swipeDirection = SwipeHelpers.GetOctalDirection(e);

                _shapeMovementController.Movement(swipeDirection, _tweenQueue);
            } else {
                var swipeDirection = SwipeHelpers.GetHorizontalDirection(e);

                _shapeRotationController.Rotation(swipeDirection, _tweenQueue);
            }
        }

    }

}