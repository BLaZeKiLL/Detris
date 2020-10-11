using CodeBlaze.Library.Collections.Pools;
using CodeBlaze.Library.Collections.Random;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeSpawner : MonoBehaviour {

        [SerializeField] [Range(3, 5)] private int _gridSize = 3;
        [SerializeField] private ShapeBehaviour.Config _shapeConfig;

        private LazyObjectPool<ShapeBehaviour> _shapeBehaviourPool;

        private ShapeInputController _shapeInputController;

        private void Awake() {
            transform.position = new Vector3((float) _gridSize / 2, 0, (float) _gridSize / 2);

            _shapeInputController = GetComponent<ShapeInputController>();

            _shapeBehaviourPool = new LazyObjectPool<ShapeBehaviour>(
                5,
                index => ShapeBehaviour.Instantiate(transform, _shapeConfig),
                sb => sb.gameObject.SetActive(true),
                sb => sb.gameObject.SetActive(false)
            );
        }

        private void Start() {
            var bag = new RandomBag<Shape>(new[] {
                new Shape(ShapeType.I, new Color32(220, 10, 10, 255), Vector3.up),
                new Shape(ShapeType.L, new Color32(10, 220, 10, 255), Vector3.up),
                new Shape(ShapeType.T, new Color32(10, 10, 220, 255), Vector3.up),
                new Shape(ShapeType.Z, new Color32(220, 220, 220, 255), Vector3.up)
            });

            SpawnShape(bag.GetItem());
        }

        private void SpawnShape(Shape shape) {
            _shapeBehaviourPool.Claim().UpdateShape(shape);
            _shapeInputController.UpdateCurrentShape(shape);
        }

        private void DeSpawnShape(ShapeBehaviour shapeBehaviour) {
            _shapeBehaviourPool.Reclaim(shapeBehaviour);
        }

    }

}