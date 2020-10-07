using CodeBlaze.Library.Collections.Pools;
using CodeBlaze.Library.Collections.Random;
using CodeBlaze.Voxel.Renderer;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeController : MonoBehaviour {

        [SerializeField] private Material _material;

        [SerializeField] private ShapeBehaviour.Config _shapeConfig;

        private LazyObjectPool<ShapeBehaviour> _shapePool;

        private void Awake() {
            _shapePool = new LazyObjectPool<ShapeBehaviour>(
                5,
                index => {
                    var shape = new GameObject("Shape", typeof(ChunkRenderer), typeof(ShapeBehaviour));

                    shape.SetActive(false);
                    shape.transform.parent = transform;
                    shape.transform.position = Vector3.up * _shapeConfig.SpawnHeight;

                    shape.GetComponent<MeshRenderer>().material = _material;
                    shape.GetComponent<ShapeBehaviour>().Initialize(
                        _shapeConfig,
                        shape.GetComponent<ChunkRenderer>()
                    );

                    return shape.GetComponent<ShapeBehaviour>();
                },
                sb => sb.gameObject.SetActive(true),
                sb => sb.gameObject.SetActive(false)
            );
        }

        private void Start() {
            var bag = new RandomBag<Shape>(new[] {
                new Shape(ShapeType.I, new Color32(220, 10, 10, 255)),
                new Shape(ShapeType.L, new Color32(10, 220, 10, 255)),
                new Shape(ShapeType.T, new Color32(10, 10, 220, 255)),
                new Shape(ShapeType.Z, new Color32(220, 220, 220, 255))
            });

            _shapePool.Claim().UpdateShape(bag.GetItem());
        }

    }

}