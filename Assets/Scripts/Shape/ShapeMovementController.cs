using CodeBlaze.Detris.Input;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeMovementController : MonoBehaviour {

        public void Movement(SwipeDirection swipeDirection) {
            UnityEngine.Debug.Log($"Movement Direction : {swipeDirection}");
        }

    }

}