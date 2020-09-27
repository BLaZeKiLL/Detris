using System;

using UnityEngine;

namespace CodeBlaze.Detris.Camera {

    public class CameraController : MonoBehaviour {

        private void Start() {
            TouchInputManager.SwipeUp += SwipeUp;
            TouchInputManager.SwipeDown += SwipeDown;
            TouchInputManager.SwipeRight += SwipeRight;
            TouchInputManager.SwipeLeft += SwipeLeft;
        }

        private void OnDestroy() {
            TouchInputManager.SwipeUp -= SwipeUp;
            TouchInputManager.SwipeDown -= SwipeDown;
            TouchInputManager.SwipeRight -= SwipeRight;
            TouchInputManager.SwipeLeft -= SwipeLeft;
        }

        private void SwipeLeft(object sender, TouchInputManager.SwipeEventArgs e) {
            throw new NotImplementedException();
        }

        private void SwipeRight(object sender, TouchInputManager.SwipeEventArgs e) {
            throw new NotImplementedException();
        }

        private void SwipeDown(object sender, TouchInputManager.SwipeEventArgs e) {
            throw new NotImplementedException();
        }

        private void SwipeUp(object sender, TouchInputManager.SwipeEventArgs e) {
            throw new NotImplementedException();
        }

    }

}