using System;

using CodeBlaze.Detris.Input;

using DG.Tweening;

using UnityEngine;

namespace CodeBlaze.Detris {

    public class SceneRootController : MonoBehaviour {

        [SerializeField] [Range(0.5f, 5f)] private float _rotationDuration = 1f;
        [SerializeField] [Range(0.2f, 0.5f)] private float _screenYSplit = 0.3f;
        
        private Vector3 _rotation;

        private void Start() {
            _rotation = Vector3.zero;
            SwipeInputDetector.Swipe += OnSwipe;
        }

        private void OnDestroy() {
            SwipeInputDetector.Swipe -= OnSwipe;
        }

        private void OnSwipe(object sender, SwipeInputDetector.SwipeEventArgs e) {
            if (SwipeHelpers.MeanY(e) / Screen.height > _screenYSplit) ShapeMovement(e);
            else SceneRotation(e);
        }

        private void ShapeMovement(SwipeInputDetector.SwipeEventArgs e) {
            Debug.Log($"Movement Direction : {SwipeHelpers.GetOctalDirection(e)}");
        }

        private void SceneRotation(SwipeInputDetector.SwipeEventArgs e) {
            switch (SwipeHelpers.GetHorizontalDirection(e)) {
                case SwipeDirection.WEST:
                    _rotation += Vector3.up * 90;
                    if (Math.Abs(_rotation.y - 360) < float.Epsilon) _rotation = Vector3.zero;
                    transform.DORotate(_rotation, _rotationDuration);

                    break;
                case SwipeDirection.EAST:
                    _rotation += Vector3.up * -90;
                    if (Math.Abs(_rotation.y + 360) < float.Epsilon) _rotation = Vector3.zero;
                    transform.DORotate(_rotation, _rotationDuration);

                    break;
            }
        }

    }

}