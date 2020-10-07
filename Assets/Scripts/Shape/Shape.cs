using CodeBlaze.Voxel;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class Shape {

        public Shape(ShapeType type, Color32 color) {
            Type = type;
            Chunk = ShapeBuilder.Build(type, color);
        }

        public ShapeType Type { get; }
        public Chunk Chunk { get; }

    }

}