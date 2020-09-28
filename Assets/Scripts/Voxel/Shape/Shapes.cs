using UnityEngine;

namespace CodeBlaze.Detris.Voxel.Shape {

    public static class Shapes {

        public static Chunk I(Vector3Int position, Color32 color) {
            var shape = new Chunk(new Vector3Int(3, 1, 1), position);
            var block = new Block(color);

            shape.SetBlock(block, 0, 0, 0);
            shape.SetBlock(block, 1, 0, 0);
            shape.SetBlock(block, 2, 0, 0);

            return shape;
        }

    }

}