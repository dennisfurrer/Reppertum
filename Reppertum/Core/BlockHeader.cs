using System;

namespace Reppertum.Core
{
    public class BlockHeader : IBlockHeader
    {
        public UInt16 Index { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public Int64 Timestamp { get; set; }

        public BlockHeader(UInt16 index, string hash, string previousHash, Int64 timestamp)
        {
            Index = index;
            Hash = hash;
            PreviousHash = previousHash;
            Timestamp = timestamp;
        }
    }
}