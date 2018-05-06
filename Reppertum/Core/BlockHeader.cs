using System;

namespace Reppertum.Core
{
    public class BlockHeader : IBlockHeader
    {
        public UInt16 index { get; set; }
        public string previousHash { get; set; }
        public string hash { get; set; }
        public Int64 timestamp { get; set; }

        public BlockHeader(UInt16 _index, string _hash, string _previousHash, Int64 _timestamp)
        {
            index = _index;
            hash = _hash;
            previousHash = _previousHash;
            timestamp = _timestamp;
        }
    }
}