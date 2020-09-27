using UnityEngine;

namespace CodeBlaze.Detris.Voxel {

    public class ChunkRenderer : MonoBehaviour {

        [SerializeField] [Range(0.01f, 0.99f)] private float _frequency = 0.1f;

        private Chunk _chunk;
        private MeshBuilder _meshBuilder;

        private void Awake() {
            transform.position = new Vector3(-(float) Chunk.SIZE.x / 2, 0f, -(float) Chunk.SIZE.z / 2);
        }

        private void Start() {
            _chunk = new Chunk(Vector3Int.zero);
            var position = transform.position;

            for (int x = 0; x < Chunk.SIZE.x; x++) {
                for (int z = 0; z < Chunk.SIZE.z; z++) {
                    var height = Mathf.FloorToInt(
                        Mathf.PerlinNoise((position.x + x) * _frequency, (position.z + z) * _frequency) * Chunk.SIZE.y
                    );

                    for (int y = 0; y < height; y++) {
                        _chunk.SetBlock(BlockTypes.RandomSolid(), x, y, z);
                    }

                    for (int y = height; y < Chunk.SIZE.y; y++) {
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