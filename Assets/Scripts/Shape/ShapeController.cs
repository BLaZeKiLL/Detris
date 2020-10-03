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

        [SerializeField]
        private Shape.Config _shapeConfig;
        
        private LazyObjectPool<GameObject> _shapePool;

        private void Awake() {
            _shapePool = new LazyObjectPool<GameObject>(
                5, 
                index => {
                    var go = new GameObject("Shape", typeof(ShapeRenderer), typeof(Shape));
                    
                    go.SetActive(false);
                    go.transform.parent = transform;
                    go.transform.position = Vector3.up * _shapeConfig.SpawnHeight;
                    
                    go.GetComponent<MeshRenderer>().material = _material;
                    go.GetComponent<Shape>().UpdateConfig(_shapeConfig);

                    return go;
                },
                go => go.SetActive(true),
                go => go.SetActive(false)
            );
        }

        private void Start() {
            var bag = new RandomBag<Chunk>(new [] {
                Shapes.Z(new Color32(220,10,10,255)),
                Shapes.T(new Color32(10,220,10,255)),
                Shapes.L(new Color32(10,10,220,255)),
                Shapes.I(new Color32(220,220,220,255)),
            });
            
            _shapePool.Claim().GetComponent<ShapeRenderer>().Render(bag.GetItem());
        }

    }

}