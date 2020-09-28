using System;

using CodeBlaze.Detris.Voxel.Renderer;
using CodeBlaze.Detris.Voxel.Shape;
using CodeBlaze.Library.Collections.Pools;

using UnityEngine;

namespace CodeBlaze.Detris.Shape {

    public class ShapeController : MonoBehaviour {

        [SerializeField]
        private Material _material;

        private LazyObjectPool<ShapeRenderer> _shapePool;

        private void Awake() {
            _shapePool = new LazyObjectPool<ShapeRenderer>(5, index => {
                var go = new GameObject("Shape", typeof(ShapeRenderer));
                go.transform.parent = transform;
                
                go.GetComponent<MeshRenderer>().material = _material;

                return go.GetComponent<ShapeRenderer>();
            });
        }

        private void Start() {
            _shapePool.Claim().Render(Shapes.Z(new Color32(220,10,10,255)));
        }

    }

}