using System;

namespace Reppertum.Core
{
    public class BlockHeader
    {
        public readonly UInt16 Index;
        public readonly string PreviousHash, Hash;
        public readonly Int64 Timestamp;

        public BlockHeader(UInt16 index, string hash, string prevHash, Int64 timestamp)
        {
            Index = index;
            Hash = hash;
            PreviousHash = prevHash;
            Timestamp = timestamp;
        }
    }
}