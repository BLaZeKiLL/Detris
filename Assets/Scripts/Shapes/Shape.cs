using System;
using System.Collections.Generic;

using CodeBlaze.Detris.Settings;
using CodeBlaze.Detris.Shapes.Behaviour;
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

    public enum ShapeType : byte {

        I,
        T,
        L,
        Z

    }
    
    public enum Orientation {

        ZERO,
        NINETY,
        ONE_EIGHTY,
        TWO_SEVENTY

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
        
        public static IEnumerable<Vector3Int> GetIndexes(this Shape shape) {
            switch (shape.Type) {
                case ShapeType.I:
                    return IndexI(shape.PositionToIndex(), shape.Orientation());
                case ShapeType.T:
                    return IndexT(shape.PositionToIndex(), shape.Orientation());
                case ShapeType.L:
                    return IndexL(shape.PositionToIndex(), shape.Orientation());
                case ShapeType.Z:
                    return IndexZ(shape.PositionToIndex(), shape.Orientation());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Vector3Int PositionToIndex(this Shape shape) {
            var x = Mathf.RoundToInt(shape.Position.x);
            var y = Mathf.RoundToInt(shape.Position.y);
            var z = Mathf.RoundToInt(shape.Position.z);
            
            var xdir = Mathf.Sign(shape.CrossPosition.x - shape.Position.x);
            var zdir = Mathf.Sign(shape.CrossPosition.z - shape.Position.z);

            if (Mathf.Approximately(xdir, -1f)) x -= 1;
            if (Mathf.Approximately(zdir, -1f)) z -= 1;
            
            return new Vector3Int(x,y,z);
        }
        
        public static Orientation Orientation(this Shape shape) {
            if (Mathf.Approximately(shape.Rotation.y, 0f)) 
                return Shapes.Orientation.ZERO;
            if (Mathf.Approximately(shape.Rotation.y, 270f) || Mathf.Approximately(shape.Rotation.y, -90f)) 
                return Shapes.Orientation.NINETY;
            if (Mathf.Approximately(shape.Rotation.y, 180f) || Mathf.Approximately(shape.Rotation.y, -180f)) 
                return Shapes.Orientation.ONE_EIGHTY;
            if (Mathf.Approximately(shape.Rotation.y, 90f) || Mathf.Approximately(shape.Rotation.y, -270f)) 
                return Shapes.Orientation.TWO_SEVENTY;
            
            throw new InvalidProgramException("This should not happen");
        }
        
        private static IEnumerable<Vector3Int> IndexI(Vector3Int position, Orientation orientation) {
            var indexes = new List<Vector3Int>();

            switch (orientation) {
                case Shapes.Orientation.ZERO:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(1, 0, 0));
                    indexes.Add(position + new Vector3Int(2, 0, 0));
                    break;
                case Shapes.Orientation.ONE_EIGHTY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(-1, 0, 0));
                    indexes.Add(position + new Vector3Int(-2, 0, 0));
                    break;
                case Shapes.Orientation.NINETY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(0, 0, 1));
                    indexes.Add(position + new Vector3Int(0, 0, 2));
                    break;
                case Shapes.Orientation.TWO_SEVENTY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(0, 0, -1));
                    indexes.Add(position + new Vector3Int(0, 0, -2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }

            return indexes;
        }
        
        private static IEnumerable<Vector3Int> IndexT(Vector3Int position, Orientation orientation) {
            var indexes = new List<Vector3Int>();

            switch (orientation) {
                case Shapes.Orientation.ZERO:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(1, 0, 0));
                    indexes.Add(position + new Vector3Int(1, 1, 0));
                    indexes.Add(position + new Vector3Int(2, 0, 0));
                    break;
                case Shapes.Orientation.ONE_EIGHTY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(-1, 0, 0));
                    indexes.Add(position + new Vector3Int(-1, 1, 0));
                    indexes.Add(position + new Vector3Int(-2, 0, 0));
                    break;
                case Shapes.Orientation.NINETY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(0, 0, 1));
                    indexes.Add(position + new Vector3Int(0, 1, 1));
                    indexes.Add(position + new Vector3Int(0, 0, 2));
                    break;
                case Shapes.Orientation.TWO_SEVENTY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(0, 0, -1));
                    indexes.Add(position + new Vector3Int(0, 1, -1));
                    indexes.Add(position + new Vector3Int(0, 0, -2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }

            return indexes;
        }

        private static IEnumerable<Vector3Int> IndexL(Vector3Int position, Orientation orientation) {
            var indexes = new List<Vector3Int>();

            switch (orientation) {
                case Shapes.Orientation.ZERO:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(1, 0, 0));
                    indexes.Add(position + new Vector3Int(2, 0, 0));
                    indexes.Add(position + new Vector3Int(2, 0, 1));
                    break;
                case Shapes.Orientation.NINETY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(0, 0, 1));
                    indexes.Add(position + new Vector3Int(0, 0, 2));
                    indexes.Add(position + new Vector3Int(-1, 0, 2));
                    break;
                case Shapes.Orientation.ONE_EIGHTY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(-1, 0, 0));
                    indexes.Add(position + new Vector3Int(-2, 0, 0));
                    indexes.Add(position + new Vector3Int(-2, 0, -1));
                    break;
                case Shapes.Orientation.TWO_SEVENTY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(0, 0, -1));
                    indexes.Add(position + new Vector3Int(0, 0, -2));
                    indexes.Add(position + new Vector3Int(1, 0, -2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }

            return indexes;
        }
        
        private static IEnumerable<Vector3Int> IndexZ(Vector3Int position, Orientation orientation) {
            var indexes = new List<Vector3Int>();

            switch (orientation) {
                case Shapes.Orientation.ZERO:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(1, 0, 0));
                    indexes.Add(position + new Vector3Int(1, 0, 1));
                    indexes.Add(position + new Vector3Int(2, 0, 1));
                    break;
                case Shapes.Orientation.NINETY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(0, 0, 1));
                    indexes.Add(position + new Vector3Int(-1, 0, 1));
                    indexes.Add(position + new Vector3Int(-1, 0, 2));
                    break;
                case Shapes.Orientation.ONE_EIGHTY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(-1, 0, 0));
                    indexes.Add(position + new Vector3Int(-1, 0, -1));
                    indexes.Add(position + new Vector3Int(-2, 0, -1));
                    break;
                case Shapes.Orientation.TWO_SEVENTY:
                    indexes.Add(position + new Vector3Int(0, 0, 0));
                    indexes.Add(position + new Vector3Int(0, 0, -1));
                    indexes.Add(position + new Vector3Int(1, 0, -1));
                    indexes.Add(position + new Vector3Int(1, 0, -2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }

            return indexes;
        }

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