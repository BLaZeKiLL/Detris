using CodeBlaze.Detris.Input;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeMovementController : MonoBehaviour {

        public Shape CurrentShape { get; private set; }

        public void UpdateCurrentShape(Shape currentShape) {
            CurrentShape = currentShape;
        }

        public void Movement(SwipeInputDetector.SwipeEventArgs e) {
            UnityEngine.Debug.Log($"Movement Direction : {SwipeHelpers.GetOctalDirection(e)}");
        }

    }

}