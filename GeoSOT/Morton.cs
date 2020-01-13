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
        // public static List<uint> magicbit2D_masks32 = new List<uint> { 0xFFFFFFFF, 0x0000FFFF, 0x00FF00FF, 0x0F0F0F0F, 0x33333333, 0x55555555 };
        private List<ulong> magicbit2D_masks64 = new List<ulong> { 0x00000000FFFFFFFF, 0x0000FFFF0000FFFF, 0x00FF00FF00FF00FF, 0x0F0F0F0F0F0F0F0F, 0x3333333333333333, 0x5555555555555555 };

        // HELPER METHOD for Magic bits encoding - split by 2
        private ulong SplitBy2Bits(uint a)
        {
            List<ulong> masks = magicbit2D_masks64;
            ulong x = a;
            x = (x | x << 32) & masks[0];
            x = (x | x << 16) & masks[1];
            x = (x | x << 8) & masks[2];
            x = (x | x << 4) & masks[3];
            x = (x | x << 2) & masks[4];
            x = (x | x << 1) & masks[5];
            return x;
        }

        // HELPER method for Magicbits decoding
        private uint GetSecondBits(ulong m)
        {
            List<ulong> masks = magicbit2D_masks64;
            ulong x = m & masks[5];
            x = (x ^ (x >> 1)) & masks[4];
            x = (x ^ (x >> 2)) & masks[3];
            x = (x ^ (x >> 4)) & masks[2];
            x = (x ^ (x >> 8)) & masks[1];
            x = (x ^ (x >> 16)) & masks[0];
            return (uint)x;
        }

        public ulong Magicbits(uint l, uint b)
        {
            return SplitBy2Bits(l) | (SplitBy2Bits(b) << 1);
        }

        public void Magicbits(ulong m, ref uint l, ref uint b)
        {
            l = GetSecondBits(m);
            b = GetSecondBits(m >> 1);
        }
    }
}
