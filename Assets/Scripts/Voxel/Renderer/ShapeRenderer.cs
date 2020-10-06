using CodeBlaze.Voxel.Mesh;

using UnityEngine;

namespace CodeBlaze.Voxel.Renderer {

    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class ShapeRenderer : MonoBehaviour {

        private MeshFilter _filter;
        private MeshBuilder _meshBuilder;

        public Chunk Chunk { get; private set; }

        private void Awake() {
            _filter = GetComponent<MeshFilter>();
            _meshBuilder = new MeshBuilder();
        }

        public void Render(Chunk chunk) {
            Chunk = chunk;
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