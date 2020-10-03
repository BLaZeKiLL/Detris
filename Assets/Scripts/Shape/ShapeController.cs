using System;

using CodeBlaze.Detris.Voxel.Renderer;
using CodeBlaze.Detris.Voxel.Shape;
using CodeBlaze.Library.Collections.Pools;
using CodeBlaze.Library.Collections.Random;

using UnityEngine;

namespace CodeBlaze.Detris.Shape {

    public class ShapeController : MonoBehaviour {

        [SerializeField]
        private Material _material;

        private LazyObjectPool<ShapeRenderer> _shapePool;

        private void Awake() {
            _shapePool = new LazyObjectPool<ShapeRenderer>(
                5, 
                index => {
                    var go = new GameObject("Shape", typeof(ShapeRenderer));
                    go.SetActive(false);
                    go.transform.parent = transform;
                    
                    go.GetComponent<MeshRenderer>().material = _material;

                    return go.GetComponent<ShapeRenderer>();
                },
                shapeRenderer => shapeRenderer.gameObject.SetActive(true),
                shapeRenderer => shapeRenderer.gameObject.SetActive(false)
            );
        }

        private void Start() {
            var bag = new RandomBag<Chunk>(new [] {
                Shapes.Z(new Color32(220,10,10,255)),
                Shapes.T(new Color32(10,220,10,255)),
                Shapes.L(new Color32(10,10,220,255)),
                Shapes.I(new Color32(220,220,220,255)),
            });
            
            _shapePool.Claim().Render(bag.GetItem());
        }

    }

}