using System;

using CodeBlaze.Voxel;

using UnityEngine;

namespace CodeBlaze.Detris.Shape {

    public static class ShapeBuilder {

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
        
        private static Chunk I(Color32 color) {
            var shape = new Chunk(new Vector3Int(3, 1, 1));

            shape.Fill(BlockTypes.Air());

            var block = new Block(color);

            shape.SetBlock(block, 0, 0, 0);
            shape.SetBlock(block, 1, 0, 0);
            shape.SetBlock(block, 2, 0, 0);

            return shape;
        }

        private static Chunk T(Color32 color) {
            var shape = new Chunk(new Vector3Int(3, 2, 1));

            shape.Fill(BlockTypes.Air());

            var block = new Block(color);

            shape.SetBlock(block, 0, 0, 0);

            shape.SetBlock(block, 1, 0, 0);
            shape.SetBlock(block, 1, 1, 0);

            shape.SetBlock(block, 2, 0, 0);

            return shape;
        }

        private static Chunk L(Color32 color) {
            var shape = new Chunk(new Vector3Int(3, 1, 2));

            shape.Fill(BlockTypes.Air());

            var block = new Block(color);

            shape.SetBlock(block, 0, 0, 0);

            shape.SetBlock(block, 1, 0, 0);

            shape.SetBlock(block, 2, 0, 0);
            shape.SetBlock(block, 2, 0, 1);

            return shape;
        }

        private static Chunk Z(Color32 color) {
            var shape = new Chunk(new Vector3Int(3, 1, 2));

            shape.Fill(BlockTypes.Air());

            var block = new Block(color);

            shape.SetBlock(block, 0, 0, 0);

            shape.SetBlock(block, 1, 0, 0);
            shape.SetBlock(block, 1, 0, 1);

            shape.SetBlock(block, 2, 0, 1);

            return shape;
        }

    }

}