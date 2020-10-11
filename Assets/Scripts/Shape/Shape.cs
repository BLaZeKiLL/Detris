using CodeBlaze.Voxel;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class Shape {

        public Shape(ShapeType type, Color32 color) {
            Type = type;
            Chunk = ShapeChunkBuilder.Build(type, color);
        }

        public ShapeType Type { get; }
        
        public Vector2 Position { get; set; }
        public Vector2 CrossPosition { get; set; }
        
        public Chunk Chunk { get; }

    }

}