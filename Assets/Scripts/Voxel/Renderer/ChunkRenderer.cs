using CodeBlaze.Voxel.Mesh;

using UnityEngine;

namespace CodeBlaze.Voxel.Renderer {

    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider))]
    public class ChunkRenderer : MonoBehaviour {

        private MeshBuilder _builder;

        private MeshFilter _filter;
        private MeshRenderer _renderer;
        private MeshCollider _collider;

        private void Awake() {
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<MeshCollider>();
            _builder = new MeshBuilder();
        }

        public void SetMaterial(Material material) {
            _renderer.material = material;
        }

        public void Render(Chunk chunk) {
            var data = _builder.GenerateMesh(chunk);
            var mesh = _filter.mesh;

            mesh.Clear();
            
            mesh.vertices = data.Vertices;
            mesh.triangles = data.Triangles;
            mesh.colors32 = data.Colors;
            mesh.normals = data.Normals;

            _collider.sharedMesh = mesh;
        }

    }

}