using System;

using UnityEngine;

namespace CodeBlaze.Detris.Shape {

    public class ShapeBehaviour : MonoBehaviour {

        private Config _config;

        private void Start() {
            transform.position = new Vector3(0, 10, 0);
        }

        private void Update() {
            transform.position += Vector3.down * (_config.FallSpeed * Time.deltaTime);
        }

        public void UpdateConfig(Config config) {
            _config = config;
        }

        [Serializable]
        public class Config {

            [SerializeField] public float SpawnHeight = 10f;
            [SerializeField] public float FallSpeed = 9.81f;

        }

    }

}