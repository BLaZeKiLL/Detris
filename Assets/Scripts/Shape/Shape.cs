using System;

using CodeBlaze.Detris.Settings;
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
        public ShapeBehaviour Behaviour { get; set; }
        public Vector3 Position { get;  set; }
        public Vector3 CrossPosition { get; set; }

    }

    public static class ShapeExtensions {

        public static bool BoundCheck(Vector3 newPosition, Vector3 newCrossPosition) {
            var _gridSize = SettingsProvider.Current.Settings.GridSize;
            
            if (newPosition.x > _gridSize || newPosition.z > _gridSize) return false;
            if (newPosition.x < 0 || newPosition.z < 0) return false;
            if (newCrossPosition.x > _gridSize || newCrossPosition.z > _gridSize) return false;
            if (newCrossPosition.x < 0 || newCrossPosition.z < 0) return false;
            
            return true;
        }

    }

}