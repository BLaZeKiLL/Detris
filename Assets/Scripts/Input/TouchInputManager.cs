using System;

using UnityEngine;

namespace CodeBlaze.Detris.Input {

    public class TouchInputManager : MonoBehaviour {

        [SerializeField] private float _swipeThreshold = 10f;

        private Vector2 _fingerDown;
        private Vector2 _fingerUp;

        private bool _swiping;

        private void Update() {
            foreach (var touch in UnityEngine.Input.touches) {
                switch (touch.phase) {
                    case TouchPhase.Began:
                        _fingerDown = touch.position;

                        break;
                    case TouchPhase.Moved:
                        _fingerUp = touch.position;
                        CheckSwipe();

                        break;
                    case TouchPhase.Ended:
                        _swiping = false;

                        break;
                }
            }
        }

        public static event EventHandler<SwipeEventArgs> SwipeUp;
        public static event EventHandler<SwipeEventArgs> SwipeDown;
        public static event EventHandler<SwipeEventArgs> SwipeLeft;
        public static event EventHandler<SwipeEventArgs> SwipeRight;

        private void CheckSwipe() {
            if (_swiping) return;

            var verticalMovement = Mathf.Abs(_fingerDown.y - _fingerUp.y);
            var horizontalMovement = Mathf.Abs(_fingerDown.x - _fingerUp.x);

            if (verticalMovement > _swipeThreshold && verticalMovement > horizontalMovement) {
                if (_fingerDown.y - _fingerUp.y > 0) {
                    SwipeUp?.Invoke(this, new SwipeEventArgs {
                        StartPosition = _fingerDown,
                        EndPosition = _fingerUp
                    });
                } else if (_fingerDown.y - _fingerUp.y < 0) {
                    SwipeDown?.Invoke(this, new SwipeEventArgs {
                        StartPosition = _fingerDown,
                        EndPosition = _fingerUp
                    });
                }

                _swiping = true;
            } else if (horizontalMovement > _swipeThreshold && horizontalMovement > verticalMovement) {
                if (_fingerDown.x - _fingerUp.x > 0) {
                    SwipeRight?.Invoke(this, new SwipeEventArgs {
                        StartPosition = _fingerDown,
                        EndPosition = _fingerUp
                    });
                } else if (_fingerDown.x - _fingerUp.x < 0) {
                    SwipeLeft?.Invoke(this, new SwipeEventArgs {
                        StartPosition = _fingerDown,
                        EndPosition = _fingerUp
                    });
                }

                _swiping = true;
            }
        }

        public class SwipeEventArgs : EventArgs {

            public Vector2 StartPosition { get; set; }
            public Vector2 EndPosition { get; set; }

        }

    }

}