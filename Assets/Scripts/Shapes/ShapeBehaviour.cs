using System;

using CodeBlaze.Voxel.Renderer;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeBehaviour : MonoBehaviour {

        private ChunkRenderer _chunkRenderer;

        private Config _config;
        private Shape _shape;

        private void Awake() {
            _chunkRenderer = GetComponent<ChunkRenderer>();
        }

        private void Update() {
            transform.position += Vector3.down * (_config.FallSpeed * Time.deltaTime);
        }

        public static ShapeBehaviour Instantiate(Transform parent, Vector3 pivotPosition, Config config) {
            var shape = new GameObject("Shape", typeof(ChunkRenderer), typeof(ShapeBehaviour));
            var pivot = new GameObject("Pivot");

            pivot.SetActive(false);
            
            pivot.transform.position = pivotPosition;
            pivot.transform.parent = parent;
            shape.transform.parent = pivot.transform;

            var shapeBehaviour = shape.GetComponent<ShapeBehaviour>();

            shapeBehaviour.Initialize(config);

            return shapeBehaviour;
        }

        private void Initialize(Config config) {
            _config = config;
            _chunkRenderer.SetMaterial(config.Material);
        }

        public void UpdateShape(Shape shape) {
            _shape = shape;
            _shape.Behaviour = this;
            _chunkRenderer.Render(_shape.Chunk);
            transform.position = new Vector3(_shape.Position.x, _config.SpawnHeight, _shape.Position.z);
        }

        [Serializable]
        public class Config {

            [SerializeField] public Material Material;
            [SerializeField] public int SpawnHeight = 10;
            [SerializeField] public float FallSpeed = 9.81f;

        }

    }

}