﻿using CodeBlaze.Detris.Voxel.Mesh;
using CodeBlaze.Detris.Voxel.Shape;

using UnityEngine;

namespace CodeBlaze.Detris.Voxel.Renderer {

    public class ChunkRenderer : MonoBehaviour {

        // For Height Map
        [SerializeField] [Range(0.01f, 0.99f)] private float _frequency = 0.1f;

        private Chunk _chunk;
        private MeshBuilder _meshBuilder;

        private void Start() {
            _chunk = new Chunk(new Vector3Int(4, 10, 4));
            transform.position = new Vector3(-(float) Chunk.SIZE.x / 2, 0f, -(float) Chunk.SIZE.z / 2);
            var position = transform.position;

            // Height Map
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