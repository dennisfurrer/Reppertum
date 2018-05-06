using System;
using System.Collections.Generic;
using System.Text;

namespace Reppertum.Core
{
    interface IBlock
    {
        BlockHeader header { get; set; }
        List<Transaction> data { get; set; }
        string merkleRoot { get; set; }
    }
}
