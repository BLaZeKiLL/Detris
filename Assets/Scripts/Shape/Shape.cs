using System;

using CodeBlaze.Voxel;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class Shape {

        public Shape(ShapeType type, Color32 color, Vector3 position) {
            Type = type;
            Chunk = ShapeBuilder.Build(type, color);
            Position = position;
            CrossPosition = this.GetCrossPosition();
        }

        public ShapeType Type { get; }
        public Chunk Chunk { get; }
        
        public Vector3 Position { get; set; }
        public Vector3 CrossPosition { get; set; }

    }

    public static class ShapeExtensions {

        public static Vector3 GetCrossPosition(this Shape shape) {
            return shape.Position + new Vector3(shape.Chunk.Size.x, 0, shape.Chunk.Size.z);
        }

    }

}