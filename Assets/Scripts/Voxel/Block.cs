using UnityEngine;

namespace CodeBlaze.Detris.Voxel {

    public struct Block {

        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        public byte A { get; }

        public Block(byte r, byte g, byte b, byte a) {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public bool IsSolid() => A == byte.MaxValue;
        public bool IsEmpty() => A == byte.MinValue;
        public Color32 GetColor() => new Color32(R, G, B, A);

        public static Block Air() => new Block(0,0,0,0);
        
    }

}