using System;

using UnityEngine;

namespace CodeBlaze.Detris {

    public class TouchInputManager : MonoBehaviour {

        [SerializeField] private float _swipeThreshold = 20f;

        private Vector2 _fingerDown;
        private Vector2 _fingerUp;

        private void Update() {
            foreach (var touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    _fingerDown = touch.position;
                }

                if (touch.phase == TouchPhase.Ended) {
                    _fingerUp = touch.position;
                    CheckSwipe();
                }
            }
        }

        public static event EventHandler<SwipeEventArgs> SwipeUp;
        public static event EventHandler<SwipeEventArgs> SwipeDown;
        public static event EventHandler<SwipeEventArgs> SwipeLeft;
        public static event EventHandler<SwipeEventArgs> SwipeRight;

        private void CheckSwipe() {
            var verticalMovement = Mathf.Abs(_fingerDown.y - _fingerUp.y);
            var horizontalMovement = Mathf.Abs(_fingerDown.x - _fingerUp.x);

            if (verticalMovement > _swipeThreshold && verticalMovement > horizontalMovement) {
                if (_fingerDown.y - _fingerUp.y > 0) //Up swipe
                {
                    Debug.Log($"Up Swipe : START : {_fingerDown} END : {_fingerUp}");
                    SwipeUp?.Invoke(this, new SwipeEventArgs {
                        StartPosition = _fingerDown,
                        EndPosition = _fingerUp
                    });
                } else if (_fingerDown.y - _fingerUp.y < 0) //Down swipe
                {
                    Debug.Log($"Down Swipe : START : {_fingerDown} END : {_fingerUp}");
                    SwipeDown?.Invoke(this, new SwipeEventArgs {
                        StartPosition = _fingerDown,
                        EndPosition = _fingerUp
                    });
                }
            } else if (horizontalMovement > _swipeThreshold && horizontalMovement > verticalMovement) {
                if (_fingerDown.x - _fingerUp.x > 0) //Right swipe
                {
                    Debug.Log($"Right Swipe : START : {_fingerDown} END : {_fingerUp}");
                    SwipeRight?.Invoke(this, new SwipeEventArgs {
                        StartPosition = _fingerDown,
                        EndPosition = _fingerUp
                    });
                } else if (_fingerDown.x - _fingerUp.x < 0) //Left swipe
                {
                    Debug.Log($"Left Swipe : START : {_fingerDown} END : {_fingerUp}");
                    SwipeLeft?.Invoke(this, new SwipeEventArgs {
                        StartPosition = _fingerDown,
                        EndPosition = _fingerUp
                    });
                }
            }
        }

        public class SwipeEventArgs : EventArgs {

            public Vector2 StartPosition { get; set; }
            public Vector2 EndPosition { get; set; }

        }

    }

}