using System;

using UnityEngine;

namespace CodeBlaze.Detris.Voxel {

    public class ChunkMono : MonoBehaviour {

        private Chunk _chunk;
        private MeshBuilder _meshBuilder;
        
        private void Start() {
            _chunk = new Chunk(Vector3Int.zero);

            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    for (int k = 0; k < 3; k++) {
                        if (k == 1 && i == 1 && j == 2) {
                            _chunk.SetBlock(new Block(255, 255, 255, 0), i, j, k);
                        } else {
                            _chunk.SetBlock(new Block(255, 255, 255, 255), i, j, k);
                        }
                    }
                }
            }

            _meshBuilder = new MeshBuilder();

            var _filter = GetComponent<MeshFilter>();

            var data = _meshBuilder.GenerateMesh(_chunk);

            Debug.Log(data.Vertices.Length);
            Debug.Log(data.Triangles.Length);

            var mesh = _filter.mesh;
            mesh.vertices = data.Vertices;
            mesh.triangles = data.Triangles;
            _filter.mesh.RecalculateNormals();
        }

    }

}