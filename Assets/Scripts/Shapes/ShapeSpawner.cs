using CodeBlaze.Detris.DetrisChunk;
using CodeBlaze.Detris.Settings;
using CodeBlaze.Library.Collections.Pools;
using CodeBlaze.Library.Collections.Random;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeSpawner : MonoBehaviour {
        
        private IObjectPool<ShapeBehaviour> _shapeBehaviourPool;

        private ShapeInputController _shapeInputController;

        private RandomBag<Shape> _bag;
        
        private void Awake() {
            var gridSize = SettingsProvider.Current.Settings.GridSize;
            var pivotPosition = new Vector3((float) gridSize / 2, 0, (float) gridSize / 2);

            _shapeInputController = GetComponent<ShapeInputController>();
            
            _shapeBehaviourPool = new ObjectPool<ShapeBehaviour>(
                5,
                index => ShapeBehaviour.Instantiate(this, transform, pivotPosition, SettingsProvider.Current.Settings.ShapeConfig),
                sb => {
                    var pivot = sb.transform.parent;
                    
                    pivot.transform.position = pivotPosition;

                    pivot.gameObject.SetActive(true);
                },
                sb => sb.transform.parent.gameObject.SetActive(false)
            );
            
            _bag = new RandomBag<Shape>(new[] {
                new Shape(ShapeType.I, new Color32(220, 10, 10, 255)),
                new Shape(ShapeType.L, new Color32(10, 220, 10, 255)),
                new Shape(ShapeType.T, new Color32(10, 10, 220, 255)),
                new Shape(ShapeType.Z, new Color32(220, 220, 220, 255))
            });
        }

        private void Start() {
            SpawnShape(_bag.GetItem(), Vector3.zero, Orientation.ZERO.Euler());
        }

        private void SpawnShape(Shape shape, Vector3 position, Vector3 rotation) {
            shape.Rotation = rotation;
            shape.Position = position;
            shape.CrossPosition = position + new Vector3(shape.Chunk.Size.x, 0, shape.Chunk.Size.z);
            
            _shapeBehaviourPool.Claim().UpdateShape(shape);
            _shapeInputController.UpdateCurrentShape(shape);
        }

        public void DeSpawnShape(ShapeBehaviour shapeBehaviour) {
            _shapeBehaviourPool.Reclaim(shapeBehaviour);
            
            SpawnShape(_bag.GetItem(), Vector3.zero, Orientation.ZERO.Euler());
        }

    }

}