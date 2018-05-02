using System;

namespace Reppertum
{
    public class Block
    {
        public readonly UInt16 Index;
        public readonly string PreviousHash, Hash, Data;
        public readonly Int64 Timestamp;

        public Block(UInt16 i, string prevHash, string hash, string data, Int64 timestamp)
        {
            Index = i;
            PreviousHash = prevHash;
            Hash = hash;
            Data = data;
            Timestamp = timestamp;
        }
    }
}