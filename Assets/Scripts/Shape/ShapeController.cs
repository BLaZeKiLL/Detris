using CodeBlaze.Library.Collections.Pools;
using CodeBlaze.Library.Collections.Random;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class ShapeController : MonoBehaviour {

        [SerializeField] private ShapeBehaviour.Config _shapeConfig;

        private LazyObjectPool<ShapeBehaviour> _shapePool;

        private void Awake() {
            _shapePool = new LazyObjectPool<ShapeBehaviour>(
                5,
                index => ShapeBehaviour.Instantiate(transform, _shapeConfig),
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