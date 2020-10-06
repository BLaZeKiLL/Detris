using CodeBlaze.Voxel.Mesh;

using UnityEngine;

namespace CodeBlaze.Voxel.Renderer {

    public class ChunkRenderer : MonoBehaviour {

        // For Height Map
        [SerializeField] [Range(0.01f, 0.99f)] private float _frequency = 0.1f;

        private Chunk _chunk;
        private MeshBuilder _meshBuilder;

        private void Start() {
            _chunk = new Chunk(new Vector3Int(4, 10, 4));
            var _transform = transform;
            _transform.position = new Vector3(-(float) _chunk.Size.x / 2, 0f, -(float) _chunk.Size.z / 2);
            var position = _transform.position;

            // Height Map
            for (int x = 0; x < _chunk.Size.x; x++) {
                for (int z = 0; z < _chunk.Size.z; z++) {
                    var height = Mathf.FloorToInt(
                        Mathf.PerlinNoise((position.x + x) * _frequency, (position.z + z) * _frequency) * _chunk.Size.y
                    );

                    for (int y = 0; y < height; y++) {
                        _chunk.SetBlock(BlockTypes.RandomSolid(), x, y, z);
                    }

                    for (int y = height; y < _chunk.Size.y; y++) {
                        _chunk.SetBlock(BlockTypes.Air(), x, y, z);
                    }
                }
            }

            _meshBuilder = new MeshBuilder();

            var _filter = GetComponent<MeshFilter>();

            var data = _meshBuilder.GenerateMesh(_chunk);

            Debug.Log($"Vertex Count : {data.Vertices.Length}");
            Debug.Log($"Triangle Count : {data.Triangles.Length}");

            var mesh = _filter.mesh;
            mesh.vertices = data.Vertices;
            mesh.triangles = data.Triangles;
            mesh.colors32 = data.Colors;
            mesh.normals = data.Normals;
        }

    }

}