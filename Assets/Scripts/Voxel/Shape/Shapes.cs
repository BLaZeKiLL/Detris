using UnityEngine;

namespace CodeBlaze.Detris.Voxel.Shape {

    public static class Shapes {

        public static Chunk I(Color32 color) {
            var shape = new Chunk(new Vector3Int(3, 1, 1));
            var block = new Block(color);

            shape.SetBlock(block, 0, 0, 0);
            shape.SetBlock(block, 1, 0, 0);
            shape.SetBlock(block, 2, 0, 0);

            return shape;
        }
        
        public static Chunk T(Color32 color) {
            var shape = new Chunk(new Vector3Int(3, 2, 1));
            var block = new Block(color);

            shape.SetBlock(block, 0, 0, 0);
            
            shape.SetBlock(block, 1, 0, 0);
            shape.SetBlock(block, 1, 1, 0);
            
            shape.SetBlock(block, 2, 0, 0);

            return shape;
        }
        
        public static Chunk L(Color32 color) {
            var shape = new Chunk(new Vector3Int(3, 1, 2));
            var block = new Block(color);

            shape.SetBlock(block, 0, 0, 0);
            
            shape.SetBlock(block, 1, 0, 0);
            
            shape.SetBlock(block, 2, 0, 0);
            shape.SetBlock(block, 2, 1, 0);

            return shape;
        }
        
        public static Chunk Z(Color32 color) {
            var shape = new Chunk(new Vector3Int(3, 1, 2));
            var block = new Block(color);

            shape.SetBlock(block, 0, 0, 0);
            
            shape.SetBlock(block, 1, 0, 0);
            shape.SetBlock(block, 1, 0, 1);
            
            shape.SetBlock(block, 2, 0, 1);

            return shape;
        }

    }

}