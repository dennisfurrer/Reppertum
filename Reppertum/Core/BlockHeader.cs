using System;

namespace Reppertum.Core
{
    public class BlockHeader : IBlockHeader
    {
        public UInt16 Index { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public Int64 Timestamp { get; set; }

        public BlockHeader(UInt16 _index, string _hash, string _previousHash, Int64 _timestamp)
        {
            Index = _index;
            Hash = _hash;
            PreviousHash = _previousHash;
            Timestamp = _timestamp;
        }
    }
}