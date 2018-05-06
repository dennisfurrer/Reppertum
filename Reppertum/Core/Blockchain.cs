using System;
using System.Collections.Generic;
using Reppertum.Crypto;

namespace Reppertum.Core
{
    public class Blockchain : IBlockchain
    {
        public readonly List<Block> Chain = new List<Block>();

        private UInt16 _current = 0;

        public Blockchain()
        {
            string hash = Hash.GetHash(0, "", DateTime.UtcNow.Ticks);
            Block genesisBlock = new Block(new BlockHeader(0, hash, "0", DateTime.UtcNow.Ticks), null);
            Chain.Add(genesisBlock);
            _current++;
        }

        public Block AddBlock(string prevHash, List<Transaction> data, Int64 timestamp)
        {
            Block prevB = Chain[_current - 1];
            string hash = Hash.GetHash(_current, prevHash, timestamp);
            Block newB = new Block(new BlockHeader(_current, hash, prevHash, timestamp), data);
            if (Consensus.ProofOfWork(prevB, newB))
            {
                Chain.Add(newB);
                _current++;
            }
            else if (prevB.header.previousHash != prevHash && prevB.header.index != 0)
            {
                throw new Exception("Hashes do not match");
            }
            else
            {
                throw new Exception("Chain not valid");
            }

            return newB;
        }

        public Transaction AddTransaction(UInt16 index, string from, string to, string data)
        {
            Int64 timestamp = DateTime.UtcNow.Ticks;
            string hashable = index + from + to + data + timestamp;
            return new Transaction(index, Cryptography.Sha256(hashable), from, to, data, timestamp);
        }

        public Block GetBlock(UInt16 index)
        {
            return Chain[index];
        }

        public Int32 GetNumberOfBlocks()
        {
            return Chain.Count;
        }
    }
}