using System;

using CodeBlaze.Detris.Settings;
using CodeBlaze.Detris.Shapes;
using CodeBlaze.Voxel;
using CodeBlaze.Voxel.Renderer;

using UnityEngine;

namespace CodeBlaze.Detris.DetrisChunk {

    [RequireComponent(typeof(ChunkRenderer))]
    public class DetrisChunkBehaviour : MonoBehaviour {

        private Chunk _chunk;

        private ChunkRenderer _renderer;

        private void Awake() {
            _renderer = GetComponent<ChunkRenderer>();
            _chunk = new Chunk(new Vector3Int(
                SettingsProvider.Current.Settings.GridSize,
                SettingsProvider.Current.Settings.ShapeConfig.SpawnHeight,
                SettingsProvider.Current.Settings.GridSize
            ));
        }

        public void AddShape(Shape shape) {
            
        }

    }

}