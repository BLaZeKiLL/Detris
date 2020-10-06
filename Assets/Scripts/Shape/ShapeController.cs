using CodeBlaze.Library.Collections.Pools;
using CodeBlaze.Library.Collections.Random;
using CodeBlaze.Voxel;
using CodeBlaze.Voxel.Renderer;

using UnityEngine;

namespace CodeBlaze.Detris.Shape {

    public class ShapeController : MonoBehaviour {

        [SerializeField] private Material _material;

        [SerializeField] private ShapeBehaviour.Config _shapeConfig;

        private LazyObjectPool<GameObject> _shapePool;

        private void Awake() {
            _shapePool = new LazyObjectPool<GameObject>(
                5,
                index => {
                    var go = new GameObject("Shape", typeof(ShapeRenderer), typeof(ShapeBehaviour));

                    go.SetActive(false);
                    go.transform.parent = transform;
                    go.transform.position = Vector3.up * _shapeConfig.SpawnHeight;

                    go.GetComponent<MeshRenderer>().material = _material;
                    go.GetComponent<ShapeBehaviour>().UpdateConfig(_shapeConfig);

                    return go;
                },
                go => go.SetActive(true),
                go => go.SetActive(false)
            );
        }

        private void Start() {
            var bag = new RandomBag<Chunk>(new[] {
                ShapeBuilder.Build(ShapeType.I, new Color32(220, 10, 10, 255)),
                ShapeBuilder.Build(ShapeType.L, new Color32(10, 220, 10, 255)),
                ShapeBuilder.Build(ShapeType.T, new Color32(10, 10, 220, 255)),
                ShapeBuilder.Build(ShapeType.Z, new Color32(220, 220, 220, 255))
            });

            _shapePool.Claim().GetComponent<ShapeRenderer>().Render(bag.GetItem());
        }

    }

}