using System;

using CodeBlaze.Detris.Settings;

using UnityEngine;

namespace CodeBlaze.Detris.Input {

    public class SwipeInputDetector : MonoBehaviour {

        private Vector2 _fingerDown;
        private Vector2 _fingerUp;

        private bool _swiping;

        public bool Active { get; set; } = true;

        private void Update() {
            if (!Active) return;

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

        public static event EventHandler<SwipeEventArgs> Swipe;

        private void CheckSwipe() {
            if (_swiping) return;

            var swipe = _fingerUp - _fingerDown;

            if (!(swipe.magnitude >= SettingsProvider.Current.Settings.SwipeThreshold)) return;

            _swiping = true;

            Swipe?.Invoke(this, new SwipeEventArgs {
                StartPosition = _fingerDown,
                EndPosition = _fingerUp
            });
        }

        public class SwipeEventArgs : EventArgs {

            public Vector2 StartPosition { get; set; }
            public Vector2 EndPosition { get; set; }

        }

    }

    public enum SwipeDirection : byte {

        NORTH,
        NORTH_EAST,
        EAST,
        SOUTH_EAST,
        SOUTH,
        SOUTH_WEST,
        WEST,
        NORTH_WEST

    }

    public static class SwipeHelpers {

        public static SwipeDirection GetOctalDirection(SwipeInputDetector.SwipeEventArgs e) {
            var angle = Vector2.SignedAngle(Vector2.right, e.EndPosition - e.StartPosition);

            return GetOctalDirection(angle);
        }

        public static SwipeDirection GetOctalDirection(float angle) {
            if (angle < 22.5f && angle >= -22.5f) return SwipeDirection.EAST;

            if (angle < 67.5f && angle >= 22.5f) return SwipeDirection.NORTH_EAST;
            if (angle < 112.5f && angle >= 67.5f) return SwipeDirection.NORTH;
            if (angle < 157.5f && angle >= 112.5f) return SwipeDirection.NORTH_WEST;

            if (angle < -22.5f && angle >= -67.5f) return SwipeDirection.SOUTH_EAST;
            if (angle < -67.5f && angle >= -112.5f) return SwipeDirection.SOUTH;
            if (angle < -112.5f && angle >= -157.5f) return SwipeDirection.SOUTH_WEST;

            if ((angle < -157.5f && angle >= -180f) || (angle <= 180f && angle >= 157.5f)) return SwipeDirection.WEST;

            throw new ArgumentException($"Invalid swipe angle : {angle}");
        }

        public static SwipeDirection GetHorizontalDirection(SwipeInputDetector.SwipeEventArgs e) {
            var angle = Vector2.SignedAngle(Vector2.right, e.EndPosition - e.StartPosition);

            return GetHorizontalDirection(angle);
        }

        public static SwipeDirection GetHorizontalDirection(float angle) {
            if (angle < 90f && angle >= -90f) return SwipeDirection.EAST;
            if ((angle >= 90f && angle <= 180f) || (angle <= -90f && angle >= -180f)) return SwipeDirection.WEST;

            throw new ArgumentException($"Invalid swipe angle : {angle}");
        }

        public static float MeanX(SwipeInputDetector.SwipeEventArgs e) {
            return (e.EndPosition.x + e.StartPosition.x) / 2;
        }

        public static float MeanY(SwipeInputDetector.SwipeEventArgs e) {
            return (e.EndPosition.y + e.StartPosition.y) / 2;
        }

    }

}