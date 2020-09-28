using System;

using CodeBlaze.Detris.Voxel.Mesh;
using CodeBlaze.Detris.Voxel.Shape;

using UnityEngine;

namespace CodeBlaze.Detris.Voxel.Renderer {

    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class ShapeRenderer : MonoBehaviour {

        private MeshFilter _filter;
        private MeshBuilder _meshBuilder;
        
        private void Awake() {
            _filter = GetComponent<MeshFilter>();
            _meshBuilder = new MeshBuilder();
        }

        public void Render(Chunk chunk) {
            var data = _meshBuilder.GenerateMesh(chunk);
            var mesh = _filter.mesh;
            
            mesh.Clear();
            
            mesh.vertices = data.Vertices;
            mesh.triangles = data.Triangles;
            mesh.colors32 = data.Colors;
            mesh.normals = data.Normals;
        }

    }

}