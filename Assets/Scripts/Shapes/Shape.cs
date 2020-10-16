using System;

using CodeBlaze.Detris.Settings;
using CodeBlaze.Voxel;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public class Shape {

        public Shape(ShapeType type, Color32 color) {
            Type = type;
            Color = color;
            Chunk = ShapeBuilder.Build(type, color);
        }

        public ShapeType Type { get; }
        public Chunk Chunk { get; }
        public Color32 Color { get; }
        public ShapeBehaviour Behaviour { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Position { get;  set; }
        public Vector3 CrossPosition { get; set; }

    }

    public static class ShapeExtensions {

        public static Orientation Orientation(this Shape shape) {
            if (shape.Rotation.y - 0 <= float.Epsilon) return Shapes.Orientation.ZERO;
            if (shape.Rotation.y - 90 <= float.Epsilon) return Shapes.Orientation.NINETY;
            if (shape.Rotation.y - 180 <= float.Epsilon) return Shapes.Orientation.ONE_EIGHTY;
            return Shapes.Orientation.TWO_SEVENTY;
        }
        
        public static bool BoundCheck(Vector3 newPosition, Vector3 newCrossPosition) {
            var _gridSize = SettingsProvider.Current.Settings.GridSize;
            
            if (newPosition.x > _gridSize || newPosition.z > _gridSize) return false;
            if (newPosition.x < 0 || newPosition.z < 0) return false;
            if (newCrossPosition.x > _gridSize || newCrossPosition.z > _gridSize) return false;
            if (newCrossPosition.x < 0 || newCrossPosition.z < 0) return false;
            
            return true;
        }

    }
    
    public enum Orientation {

        ZERO,
        NINETY,
        ONE_EIGHTY,
        TWO_SEVENTY

    }

    public static class OrientationExtensions {

        public static Vector3 Euler(this Orientation orientation) {
            switch (orientation) {
                case Orientation.ZERO:
                    return new Vector3(0,0,0);
                case Orientation.NINETY:
                    return new Vector3(0,90,0);
                case Orientation.ONE_EIGHTY:
                    return new Vector3(0,180,0);
                case Orientation.TWO_SEVENTY:
                    return new Vector3(0,270,0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

    }

}