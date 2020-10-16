using System;

using CodeBlaze.Detris.Settings;
using CodeBlaze.Detris.Shapes;
using CodeBlaze.Detris.Util;
using CodeBlaze.Voxel;
using CodeBlaze.Voxel.Renderer;

using UnityEngine;

namespace CodeBlaze.Detris.DetrisChunk {

    [RequireComponent(typeof(ChunkRenderer))]
    public class DetrisChunkBehaviour : Singleton<DetrisChunkBehaviour> {

        private Chunk _chunk;

        private ChunkRenderer _renderer;

        protected override void Awake() {
            base.Awake();
            _renderer = GetComponent<ChunkRenderer>();
            _chunk = new Chunk(new Vector3Int(
                SettingsProvider.Current.Settings.GridSize,
                SettingsProvider.Current.Settings.ShapeConfig.SpawnHeight,
                SettingsProvider.Current.Settings.GridSize
            ));
        }

        private void Start() {
            _renderer.SetMaterial(SettingsProvider.Current.Settings.ShapeConfig.Material);
            _renderer.Render(_chunk);
        }

        public void AddShape(Shape shape) {
            ShapeChunkBuilder.Fill(_chunk, shape);
            _renderer.Render(_chunk);
        }

    }

}