using UnityEngine;
using UnityEngine.UI;

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

        public uint ToInt() {
            uint x = R;
            x <<= 8;
            x += G;
            x <<= 8;
            x += B;
            x <<= 8;
            x += A;
            return x;
        }
        
        public Color32 GetColor() => new Color32(R, G, B, A);

    }

    public static class BlockTypes {

        public static Block Air() => new Block(0,0,0,0);
        
        public static Block Red() => new Block(255, 0, 0, 255);
        
        public static Block Green() => new Block(0, 255, 0, 255);
        
        public static Block Blue() => new Block(0, 0, 255, 255);

    }

}