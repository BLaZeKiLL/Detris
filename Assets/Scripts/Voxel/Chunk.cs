using System;

using UnityEngine;

namespace CodeBlaze.Detris.Voxel {

    public class Chunk {

        public static readonly Vector3Int SIZE = new Vector3Int(3,3,3);

        public Vector3Int Position { get; }
        
        private Block[] _blocks;

        public Chunk(Vector3Int position) {
            Position = position;
            _blocks = new Block[SIZE.x * SIZE.y * SIZE.z];
        }

        public void SetBlock(Block block, Vector3Int index) => SetBlock(block, index.x, index.y, index.z);

        public void SetBlock(Block block, int x, int y, int z) {
            if (!ContainsIndex(x,y,z)) {
                throw new IndexOutOfRangeException($"Chunk does not contain index: ({x},{y},{z})");
            }

            _blocks[FlattenIndex(x,y,z)] = block;
        }

        public Block GetBlock(Vector3Int index) => GetBlock(index.x, index.y, index.z);

        public Block GetBlock(int x, int y, int z) {
            return !ContainsIndex(x,y,z) ? 
                Block.Air() : 
                _blocks[FlattenIndex(x,y,z)];
        }
        
        private int FlattenIndex(int x, int y, int z) =>
            y * SIZE.x * SIZE.z +
            z * SIZE.x +
            x;
        
        private bool ContainsIndex(int x, int y, int z) =>
            x >= 0 && x < SIZE.x &&
            y >= 0 && y < SIZE.y &&
            z >= 0 && z < SIZE.z;

    }

}