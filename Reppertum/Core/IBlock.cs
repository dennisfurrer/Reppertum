using System;
using System.Collections.Generic;
using System.Text;

namespace Reppertum.Core
{
    public interface IBlock
    {
        BlockHeader Header { get; set; }
        List<Transaction> Data { get; set; }
        string MerkleRoot { get; set; }
        string GetHash();
    }
}
