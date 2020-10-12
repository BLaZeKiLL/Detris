using CodeBlaze.Detris.Shapes;

using UnityEngine;

namespace CodeBlaze.Detris.Settings {

    [CreateAssetMenu(fileName = "GameSettings", menuName = "Detris/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject {

        [Header("Input Settings")]
        [Range(0.2f, 0.5f)] public float ScreenSplit = 0.3f;

        [Header("Shapes")]
        [Range(3, 5)] public int GridSize = 3;
        [Range(0.5f, 5f)] public float TweenDuration = 1f;
        public ShapeBehaviour.Config ShapeConfig;
        
    }

}