using System;
using System.Collections.Generic;
using Reppertum.Crypto;

namespace Reppertum.Core
{
    public class Block : IBlock
    {
        public BlockHeader Header { get; set; }
        public List<Transaction> Data { get; set; }
        public string MerkleRoot { get; set; }
        public Block(BlockHeader header, List<Transaction> data)
        {
            if (data == null) data = new List<Transaction>();
            Header = header;
            Data = data;
            MerkleRoot = (data.Count != 0) ? Merkle.GetMerkleRoot(Data) : "Not available due to transaction count.";
        }

        public string GetHash()
        {
            return Header.Hash;
        }
    }
}