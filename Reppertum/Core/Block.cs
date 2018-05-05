using System;
using System.Collections.Generic;
using Reppertum.Crypto;

namespace Reppertum.Core 
{
    public class Block 
    {
        public readonly BlockHeader Header;
        public readonly List<Transaction> Data;
        public readonly string MerkleRoot;

        public Block(BlockHeader header, List<Transaction> data)
        {
            if (data == null) data = new List<Transaction>();
            Header = header;
            Data = data;
            MerkleRoot = (data.Count != 0) ? Merkle.GetMerkleRoot(Data) : "Not available due to transaction count.";
        }
        
        public static string GetHash(UInt16 index, string prevHash, Int64 timestamp) 
        {
            return Cryptography.Sha256(index + prevHash + timestamp);
        }
    }
}