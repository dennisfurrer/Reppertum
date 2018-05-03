using System;
using System.Collections.Generic;

namespace Reppertum {
    public class Block {
        public readonly UInt16 Index;
        public readonly string PreviousHash, Hash;
        public readonly List<Transaction> Data;
        public readonly Int64 Timestamp;

        public Block(UInt16 index, string prevHash, string hash, List<Transaction> data, Int64 timestamp)
        {
            if (data == null) data = new List<Transaction>();
            Index = index;
            PreviousHash = prevHash;
            Hash = hash;
            Data = data;
            Timestamp = timestamp;
        }
    }
}