using System;
using System.Collections.Generic;
using Reppertum.Crypto;

namespace Reppertum.Core
{
    public class Block : IBlock
    {
        public BlockHeader header { get; set; }
        public List<Transaction> data { get; set; }
        public string merkleRoot { get; set; }
        public Block(BlockHeader _header, List<Transaction> _data)
        {
            if (_data == null) _data = new List<Transaction>();
            header = _header;
            data = _data;
            merkleRoot = (_data.Count != 0) ? Merkle.GetMerkleRoot(data) : "Not available due to transaction count.";
        }

        public string GetHash()
        {
            return header.hash;
        }
    }
}