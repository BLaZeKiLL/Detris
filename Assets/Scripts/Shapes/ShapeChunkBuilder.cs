using System;

using CodeBlaze.Voxel;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

    public static class ShapeChunkBuilder {

        public static Chunk Build(ShapeType type, Color32 color) {
            switch (type) {
                case ShapeType.I:
                    return I(color);
                case ShapeType.T:
                    return T(color);
                case ShapeType.L:
                    return L(color);
                case ShapeType.Z:
                    return Z(color);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static void Fill(Chunk chunk, Shape shape) {
            switch (shape.Type) {
                case ShapeType.I:
                    FillI(chunk, Vector3Int.FloorToInt(shape.Position), shape.Orientation(), shape.Color);
                    break;
                case ShapeType.T:
                    FillT(chunk, Vector3Int.FloorToInt(shape.Position), shape.Orientation(), shape.Color);
                    break;
                case ShapeType.L:
                    FillL(chunk, Vector3Int.FloorToInt(shape.Position), shape.Orientation(), shape.Color);
                    break;
                case ShapeType.Z:
                    FillZ(chunk, Vector3Int.FloorToInt(shape.Position), shape.Orientation(), shape.Color);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static Chunk I(Color32 color) {
            var chunk = new Chunk(new Vector3Int(3, 1, 1));

            chunk.Fill(BlockTypes.Air());

            FillI(chunk, Vector3Int.zero, Orientation.ZERO, color);

            return chunk;
        }

        private static void FillI(Chunk chunk, Vector3Int position, Orientation orientation, Color32 color) {
            var block = new Block(color);

            switch (orientation) {
                case Orientation.ZERO:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(1, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(2, 0, 0));
                    break;
                case Orientation.ONE_EIGHTY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(-1, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(-2, 0, 0));
                    break;
                case Orientation.NINETY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 1));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 2));
                    break;
                case Orientation.TWO_SEVENTY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, -1));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, -2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        private static Chunk T(Color32 color) {
            var chunk = new Chunk(new Vector3Int(3, 2, 1));

            chunk.Fill(BlockTypes.Air());

            FillT(chunk, Vector3Int.zero, Orientation.ZERO, color);

            return chunk;
        }

        private static void FillT(Chunk chunk, Vector3Int position, Orientation orientation, Color32 color) {
            var block = new Block(color);

            switch (orientation) {
                case Orientation.ZERO:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(1, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(1, 1, 0));
                    chunk.SetBlock(block, position + new Vector3Int(2, 0, 0));
                    break;
                case Orientation.ONE_EIGHTY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(-1, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(-1, 1, 0));
                    chunk.SetBlock(block, position + new Vector3Int(-2, 0, 0));
                    break;
                case Orientation.NINETY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 1));
                    chunk.SetBlock(block, position + new Vector3Int(0, 1, 1));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 2));
                    break;
                case Orientation.TWO_SEVENTY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, -1));
                    chunk.SetBlock(block, position + new Vector3Int(0, 1, -1));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, -2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        private static Chunk L(Color32 color) {
            var chunk = new Chunk(new Vector3Int(3, 1, 2));

            chunk.Fill(BlockTypes.Air());

            FillL(chunk, Vector3Int.zero, Orientation.ZERO, color);

            return chunk;
        }

        private static void FillL(Chunk chunk, Vector3Int position, Orientation orientation, Color32 color) {
            var block = new Block(color);

            switch (orientation) {
                case Orientation.ZERO:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(1, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(2, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(2, 0, 1));
                    break;
                case Orientation.NINETY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 1));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 2));
                    chunk.SetBlock(block, position + new Vector3Int(-1, 0, 2));
                    break;
                case Orientation.ONE_EIGHTY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(-1, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(-2, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(-2, 0, -1));
                    break;
                case Orientation.TWO_SEVENTY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, -1));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, -2));
                    chunk.SetBlock(block, position + new Vector3Int(1, 0, -2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        private static Chunk Z(Color32 color) {
            var chunk = new Chunk(new Vector3Int(3, 1, 2));

            chunk.Fill(BlockTypes.Air());

            FillZ(chunk, Vector3Int.zero, Orientation.ZERO, color);

            return chunk;
        }

        private static void FillZ(Chunk chunk, Vector3Int position, Orientation orientation, Color32 color) {
            var block = new Block(color);

            switch (orientation) {
                case Orientation.ZERO:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(1, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(1, 0, 1));
                    chunk.SetBlock(block, position + new Vector3Int(2, 0, 1));
                    break;
                case Orientation.NINETY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 1));
                    chunk.SetBlock(block, position + new Vector3Int(-1, 0, 1));
                    chunk.SetBlock(block, position + new Vector3Int(-1, 0, 2));
                    break;
                case Orientation.ONE_EIGHTY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(-1, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(-1, 0, -1));
                    chunk.SetBlock(block, position + new Vector3Int(-2, 0, -1));
                    break;
                case Orientation.TWO_SEVENTY:
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, 0));
                    chunk.SetBlock(block, position + new Vector3Int(0, 0, -1));
                    chunk.SetBlock(block, position + new Vector3Int(1, 0, -1));
                    chunk.SetBlock(block, position + new Vector3Int(1, 0, -2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

    }

}