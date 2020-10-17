using System;
using System.Linq;

using CodeBlaze.Detris.Settings;
using CodeBlaze.Detris.Shapes;
using CodeBlaze.Detris.Util;
using CodeBlaze.Voxel;
using CodeBlaze.Voxel.Renderer;

using UnityEngine;
using UnityEngine.SceneManagement;

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

        public bool Check(Shape shape) {
            var indexes = shape.GetIndexes().ToArray();
            
            var result = false;
            
            foreach (var index in indexes) {
                try {
                    var temp = _heightMap[index.x, index.z] >= shape.Position.y;
                    result |= temp;
                    if (temp)
                        if(++_heightMap[index.x, index.z] >= SettingsProvider.Current.Settings.ShapeConfig.SpawnHeight)
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                } catch (IndexOutOfRangeException) {
                    throw new IndexOutOfRangeException($"Height Map Index Out Of Range : {index}");
                }
            }
            
            if (result) {
                _chunk.Fill(indexes, new Block(shape.Color));
                _renderer.Render(_chunk);
            }

            return result;
        }

    }

}