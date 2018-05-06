using System.Collections.Generic;

namespace Reppertum.Core
{
    public interface IBlockchain
    {
        Block AddBlock(string prevHash, List<Transaction> data, long timestamp);
        Transaction AddTransaction(ushort index, string from, string to, string data);
        Block GetBlock(ushort index);
        int GetNumberOfBlocks();
    }
}