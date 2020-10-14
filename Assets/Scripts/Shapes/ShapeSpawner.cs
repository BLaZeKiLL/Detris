using CodeBlaze.Detris.Settings;
using CodeBlaze.Library.Collections.Pools;
using CodeBlaze.Library.Collections.Random;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeSpawner : MonoBehaviour {
        
        private LazyObjectPool<ShapeBehaviour> _shapeBehaviourPool;

        private ShapeInputController _shapeInputController;

        private void Awake() {
            var gridSize = SettingsProvider.Current.Settings.GridSize;
            var pivot = new Vector3((float) gridSize / 2, 0, (float) gridSize / 2);

            _shapeInputController = GetComponent<ShapeInputController>();
            
            _shapeBehaviourPool = new LazyObjectPool<ShapeBehaviour>(
                5,
                index => ShapeBehaviour.Instantiate(transform, pivot, SettingsProvider.Current.Settings.ShapeConfig),
                sb => sb.transform.parent.gameObject.SetActive(true),
                sb => sb.transform.parent.gameObject.SetActive(false)
            );
        }

        private void Start() {
            var bag = new RandomBag<Shape>(new[] {
                new Shape(ShapeType.I, new Color32(220, 10, 10, 255)),
                new Shape(ShapeType.L, new Color32(10, 220, 10, 255)),
                new Shape(ShapeType.T, new Color32(10, 10, 220, 255)),
                new Shape(ShapeType.Z, new Color32(220, 220, 220, 255))
            });

            SpawnShape(bag.GetItem(), Vector3.zero);
        }

        private void SpawnShape(Shape shape, Vector3 position) {
            shape.Position = position;
            shape.CrossPosition = position + new Vector3(shape.Chunk.Size.x, 0, shape.Chunk.Size.z);
            
            _shapeBehaviourPool.Claim().UpdateShape(shape);
            _shapeInputController.UpdateCurrentShape(shape);
        }

        private void DeSpawnShape(ShapeBehaviour shapeBehaviour) {
            _shapeBehaviourPool.Reclaim(shapeBehaviour);
        }

    }

}