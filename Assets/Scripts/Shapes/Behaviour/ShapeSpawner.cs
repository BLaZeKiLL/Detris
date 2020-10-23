using System;

using CodeBlaze.Detris.Settings;
using CodeBlaze.Detris.Shapes.Input;
using CodeBlaze.Library.Collections.Pools;
using CodeBlaze.Library.Collections.Random;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes.Behaviour {

    public class ShapeSpawner : MonoBehaviour {
        
        private IObjectPool<ShapeBehaviour> _shapeBehaviourPool;

        private ShapeInputController _shapeInputController;

        private RandomBag<ShapeType> _bag;
        
        private void Awake() {
            var gridSize = SettingsProvider.Current.Settings.GridSize;
            var pivotPosition = new Vector3((float) gridSize / 2, 0, (float) gridSize / 2);

            _shapeInputController = GetComponent<ShapeInputController>();
            
            _shapeBehaviourPool = new LazyObjectPool<ShapeBehaviour>(
                5, // max number of objects if required the pool can create
                index => ShapeBehaviour.Instantiate(this, transform, pivotPosition, SettingsProvider.Current.Settings.ShapeConfig),
                sb => sb.transform.parent.gameObject.SetActive(true),
                sb => sb.transform.parent.gameObject.SetActive(false)
            );

            _bag = new RandomBag<ShapeType>(new [] {
                ShapeType.I,
                ShapeType.L,
                ShapeType.T,
                ShapeType.Z
            });
        }

        private void Start() {
            SpawnShape(CreateShape(_bag.GetItem()), Vector3.zero, Orientation.ZERO.Euler());
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
            
            SpawnShape(CreateShape(_bag.GetItem()), Vector3.zero, Orientation.ZERO.Euler());
        }

        private Shape CreateShape(ShapeType type) {
            switch (type) {
                case ShapeType.I:
                    return new Shape(ShapeType.I, new Color32(220, 10, 10, 255));
                case ShapeType.T:
                    return new Shape(ShapeType.L, new Color32(10, 220, 10, 255));
                case ShapeType.L:
                    return new Shape(ShapeType.T, new Color32(10, 10, 220, 255));
                case ShapeType.Z:
                    return new Shape(ShapeType.Z, new Color32(220, 220, 220, 255));
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

}