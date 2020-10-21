using System;
using System.Linq;

using CodeBlaze.Detris.Shapes;
using CodeBlaze.Library.Behaviour;
using CodeBlaze.Voxel;
using CodeBlaze.Voxel.Renderer;

using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;

using SettingsProvider = CodeBlaze.Detris.Settings.SettingsProvider;

namespace CodeBlaze.Detris.DetrisChunk {

    [RequireComponent(typeof(ChunkRenderer))]
    public class DetrisChunkBehaviour : Singleton<DetrisChunkBehaviour> {

        private Chunk _chunk;

        private ChunkRenderer _renderer;

        private int[,] _heightMap;

        protected override void Awake() {
            base.Awake();
            _renderer = GetComponent<ChunkRenderer>();
            
            _chunk = new Chunk(new Vector3Int(
                SettingsProvider.Current.Settings.GridSize,
                SettingsProvider.Current.Settings.ShapeConfig.SpawnHeight,
                SettingsProvider.Current.Settings.GridSize
            ));
            
            _heightMap = new int[SettingsProvider.Current.Settings.GridSize, SettingsProvider.Current.Settings.GridSize];
        }

        private void Start() {
            _renderer.SetMaterial(SettingsProvider.Current.Settings.ShapeConfig.Material);
            _renderer.Render(_chunk);
        }

        private void OnDrawGizmos() {
            if (!Application.isPlaying) return;
            for (int x = 0; x < _heightMap.GetLength(0); x++) {
                for (int z = 0; z < _heightMap.GetLength(1); z++) {
                    var y = _heightMap[x, z];
                    Handles.Label(new Vector3(x + 0.5f, y, z + 0.5f), $"{y}");
                }
            }
        }

        public bool Check(Shape shape) {
            var indexes = shape.GetIndexes().ToArray();
            
            var result = false;
            
            foreach (var index in indexes) {
                try {
                    result = _heightMap[index.x, index.z] >= shape.Position.y;
                    if (result) break;
                } catch (IndexOutOfRangeException) {
                    throw new IndexOutOfRangeException($"Height Map Index Out Of Range : {index}");
                }
            }

            if (!result) return false;
            
            foreach (var index in indexes) {
                _heightMap[index.x, index.z] = index.y + 1;

                if (_heightMap[index.x, index.z] >= SettingsProvider.Current.Settings.ShapeConfig.SpawnHeight) {
                    // TODO Pause here and show the glorious chunk
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            
            _chunk.Fill(indexes, new Block(shape.Color));
            _renderer.Render(_chunk);
            
            return true;
        }

    }

}