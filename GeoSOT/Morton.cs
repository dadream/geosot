using System;
using System.Collections.Generic;
using System.Text;

namespace GeoSOT
{
    /// <summary>
    /// 二维莫顿码
    /// Magicbits masks (2D encode)
    /// 实现方法引用自libmorton库
    /// </summary>
    public class Morton2D
    {
        // public static List<UInt32> magicbit2D_masks32 = new List<UInt32> { 0xFFFFFFFF, 0x0000FFFF, 0x00FF00FF, 0x0F0F0F0F, 0x33333333, 0x55555555 };
        private List<UInt64> magicbit2D_masks64 = new List<UInt64> { 0x00000000FFFFFFFF, 0x0000FFFF0000FFFF, 0x00FF00FF00FF00FF, 0x0F0F0F0F0F0F0F0F, 0x3333333333333333, 0x5555555555555555 };

        // HELPER METHOD for Magic bits encoding - split by 2
        private UInt64 SplitBy2Bits(UInt32 a)
        {
            List<UInt64> masks = magicbit2D_masks64;
            UInt64 x = a;
            x = (x | x << 32) & masks[0];
            x = (x | x << 16) & masks[1];
            x = (x | x << 8) & masks[2];
            x = (x | x << 4) & masks[3];
            x = (x | x << 2) & masks[4];
            x = (x | x << 1) & masks[5];
            return x;
        }

        // HELPER method for Magicbits decoding
        private UInt32 GetSecondBits(UInt64 m)
        {
            List<UInt64> masks = magicbit2D_masks64;
            UInt64 x = m & masks[5];
            x = (x ^ (x >> 1)) & masks[4];
            x = (x ^ (x >> 2)) & masks[3];
            x = (x ^ (x >> 4)) & masks[2];
            x = (x ^ (x >> 8)) & masks[1];
            x = (x ^ (x >> 16)) & masks[0];
            return (UInt32)x;
        }

        public UInt64 Magicbits(uint l, UInt32 b)
        {
            return SplitBy2Bits(l) | (SplitBy2Bits(b) << 1);
        }

        public void Magicbits(UInt64 m, ref UInt32 l, ref UInt32 b)
        {
            l = GetSecondBits(m);
            b = GetSecondBits(m >> 1);
        }
    }
}
