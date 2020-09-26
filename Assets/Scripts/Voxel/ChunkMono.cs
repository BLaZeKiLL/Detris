using System;

using UnityEngine;

namespace CodeBlaze.Detris.Voxel {

    public class ChunkMono : MonoBehaviour {

        private Chunk _chunk;
        private MeshBuilder _meshBuilder;
        
        private void Start() {
            _chunk = new Chunk(Vector3Int.zero);
            var position = transform.position;
            
            for (int x = 0; x < Chunk.SIZE.x; x++) {
                for (int z = 0; z < Chunk.SIZE.z; z++) {
                    var height = Mathf.FloorToInt(
                        Mathf.PerlinNoise((position.x + x) * 0.15f, (position.z + z) * 0.15f) * Chunk.SIZE.y
                    );

                    for (int y = 0; y < height; y++) {
                        _chunk.SetBlock(BlockTypes.Green(), x,y,z);
                    }
                    
                    for (int y = height; y < Chunk.SIZE.y; y++) {
                        _chunk.SetBlock(BlockTypes.Air(), x,y,z);
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