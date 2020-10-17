using System;
using System.Collections.Generic;

using CodeBlaze.Voxel;

using UnityEngine;

namespace CodeBlaze.Detris.Shapes {

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
            var chunk = new Chunk(new Vector3Int(3, 1, 1));

            chunk.Fill(BlockTypes.Air());
            
            var block = new Block(color);

            chunk.SetBlock(block, 0,0,0);
            chunk.SetBlock(block, 1,0,0);
            chunk.SetBlock(block, 2,0,0);

            return chunk;
        }

        private static Chunk T(Color32 color) {
            var chunk = new Chunk(new Vector3Int(3, 2, 1));

            chunk.Fill(BlockTypes.Air());

            var block = new Block(color);
            
            chunk.SetBlock(block, 0, 0, 0);
            chunk.SetBlock(block, 1, 0, 0);
            chunk.SetBlock(block, 1, 1, 0);
            chunk.SetBlock(block, 2, 0, 0);

            return chunk;
        }

        private static Chunk L(Color32 color) {
            var chunk = new Chunk(new Vector3Int(3, 1, 2));

            chunk.Fill(BlockTypes.Air());
            
            var block = new Block(color);

            chunk.SetBlock(block, 0, 0, 0);
            chunk.SetBlock(block, 1, 0, 0);
            chunk.SetBlock(block, 2, 0, 0);
            chunk.SetBlock(block, 2, 0, 1);

            return chunk;
        }

        private static Chunk Z(Color32 color) {
            var chunk = new Chunk(new Vector3Int(3, 1, 2));

            chunk.Fill(BlockTypes.Air());

            var block = new Block(color);
            
            chunk.SetBlock(block, 0, 0, 0);
            chunk.SetBlock(block, 1, 0, 0);
            chunk.SetBlock(block, 1, 0, 1);
            chunk.SetBlock(block, 2, 0, 1);

            return chunk;
        }

    }

}