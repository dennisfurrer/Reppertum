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
        public Block(BlockHeader _header, List<Transaction> _data)
        {
            if (_data == null) _data = new List<Transaction>();
            Header = _header;
            Data = _data;
            MerkleRoot = (_data.Count != 0) ? Merkle.GetMerkleRoot(Data) : "Not available due to transaction count.";
        }

        public string GetHash()
        {
            return Header.Hash;
        }
    }
}