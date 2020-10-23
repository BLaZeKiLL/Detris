using System;

using CodeBlaze.Detris.DetrisChunk;
using CodeBlaze.Voxel.Renderer;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes.Behaviour {

    public class ShapeBehaviour : MonoBehaviour {

        private ShapeSpawner _spawner;
        private ChunkRenderer _chunkRenderer;
        private Transform _pivot;
        private Config _config;
        private Shape _shape;

        private void Awake() {
            _chunkRenderer = GetComponent<ChunkRenderer>();
        }

        private void Update() {
            var position = transform.position;
            position += Vector3.down * (_config.FallSpeed * Time.deltaTime);

            _shape.Position = new Vector3(_shape.Position.x, position.y, _shape.Position.z);
            _shape.CrossPosition = new Vector3(_shape.CrossPosition.x, position.y, _shape.CrossPosition.z);

            transform.position = position;

            if (!DetrisChunkBehaviour.Current.Check(_shape)) return;

            _spawner.DeSpawnShape(this);
        }

        private void Initialize(ShapeSpawner spawner, Config config) {
            _spawner = spawner;
            _config = config;
            _pivot = transform.parent;
            _chunkRenderer.SetMaterial(config.Material);
        }

        public void UpdateShape(Shape shape) {
            _shape = shape;
            _shape.Behaviour = this;
            
            _chunkRenderer.Render(_shape.Chunk);
            
            // TODO find out why setting rotation first avoids position issue
            // maybe
            // since pivot is a parent transform changes to it propogate to children
            _pivot.rotation = Quaternion.Euler(_shape.Rotation);
            transform.position = new Vector3(_shape.Position.x, _config.SpawnHeight, _shape.Position.z);
        }

        [Serializable]
        public class Config {

            [SerializeField] public Material Material;
            [SerializeField] public int SpawnHeight = 10;
            [SerializeField] public float FallSpeed = 9.81f;

        }
        
        public static ShapeBehaviour Instantiate(ShapeSpawner spawner, Transform parent, Vector3 pivotPosition, Config config) {
            var shape = new GameObject("Shape", typeof(ChunkRenderer), typeof(ShapeBehaviour));
            var pivot = new GameObject("Pivot");

            pivot.SetActive(false);
            pivot.transform.parent = parent;
            pivot.transform.position = pivotPosition;
            shape.transform.parent = pivot.transform;

            var shapeBehaviour = shape.GetComponent<ShapeBehaviour>();

            shapeBehaviour.Initialize(spawner, config);

            return shapeBehaviour;
        }

    }

}