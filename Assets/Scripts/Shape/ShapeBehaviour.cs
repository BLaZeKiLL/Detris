using System;

using CodeBlaze.Voxel.Renderer;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeBehaviour : MonoBehaviour {

        private ChunkRenderer _chunkRenderer;

        private Config _config;
        private Shape _shape;

        private void Start() {
            transform.position = new Vector3(0, 10, 0);
        }

        private void Update() {
            transform.position += Vector3.down * (_config.FallSpeed * Time.deltaTime);
        }

        public static ShapeBehaviour Instantiate(Transform parent, Config config) {
            var shape = new GameObject("Shape", typeof(ChunkRenderer), typeof(ShapeBehaviour));

            shape.SetActive(false);
            shape.transform.parent = parent;
            shape.transform.position = Vector3.up * config.SpawnHeight;

            shape.GetComponent<MeshRenderer>().material = config.Material;
            shape.GetComponent<ShapeBehaviour>().Initialize(
                config,
                shape.GetComponent<ChunkRenderer>()
            );

            return shape.GetComponent<ShapeBehaviour>();
        }

        public void Initialize(Config config, ChunkRenderer chunkRenderer) {
            _config = config;
            _chunkRenderer = chunkRenderer;
        }

        public void UpdateShape(Shape shape) {
            _shape = shape;
            _chunkRenderer.Render(_shape.Chunk);
        }

        [Serializable]
        public class Config {

            [SerializeField] public Material Material;
            [SerializeField] public float SpawnHeight = 10f;
            [SerializeField] public float FallSpeed = 9.81f;

        }

    }

}