using System;

using CodeBlaze.Voxel;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class Shape {

        public Shape(ShapeType type, Color32 color) {
            Type = type;
            Chunk = ShapeChunkBuilder.Build(type, color);
        }

        public ShapeType Type { get; }
        public Chunk Chunk { get; }
        
        public Vector2 Position { get; set; }
        public Vector2 CrossPosition { get; set; }

    }

}