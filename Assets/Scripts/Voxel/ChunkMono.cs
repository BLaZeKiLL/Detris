using System;

using UnityEngine;

namespace CodeBlaze.Detris.Voxel {

    public class ChunkMono : MonoBehaviour {

        private Chunk _chunk;
        private MeshBuilder _meshBuilder;
        
        private void Start() {
            _chunk = new Chunk(Vector3Int.zero);

            for (int i = 0; i < Chunk.SIZE.x; i++) {
                for (int j = 0; j < Chunk.SIZE.y; j++) {
                    for (int k = 0; k < Chunk.SIZE.z; k++) {
                        if (k == 1 && i == 1 && j == 9) {
                            _chunk.SetBlock(BlockTypes.Air(), i, j, k);
                        } else if (j < 5) {
                            _chunk.SetBlock(BlockTypes.Green(), i, j, k);
                        } else {
                            _chunk.SetBlock(BlockTypes.Red(), i, j, k);
                        }
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
            _filter.mesh.RecalculateNormals();
        }

    }

}