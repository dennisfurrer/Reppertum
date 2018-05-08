using System;
using System.Collections.Generic;
using Reppertum.Crypto;

namespace Reppertum.Core
{
    public class Blockchain
    {
        public readonly List<Block> Chain = new List<Block>();
        public readonly string FirstHash;
        private UInt16 _current = 0;

        public Blockchain(Config config)
        {
            string hash = Cryptography.CalculateHash(config, 0 + "" + DateTime.UtcNow.Ticks);
            FirstHash = hash;
            Block genesisBlock = new Block(config, new BlockHeader(0, hash, "0", DateTime.UtcNow.Ticks), null);
            Chain.Add(genesisBlock);
            _current++;
        }

        public Block AddBlock(Config config, string prevHash, List<Transaction> data, Int64 timestamp)
        {
            Block prevB = Chain[_current - 1];
            
            Consensus consensus = new Consensus();
            Block newB = new Block(config, new BlockHeader(_current, Cryptography.CalculateHash(config, _current + prevHash + timestamp), prevHash, timestamp), data);
            {
                if (consensus.ConsensusCalculation(config, prevB, newB))
                {
                    Chain.Add(newB);
                    _current++;
                }
                else if (prevB.Header.PreviousHash != prevHash && prevB.Header.Index != 0)
                {
                    throw new Exception("Hashes do not match");
                }
                else
                {
                    throw new Exception("Chain not valid");
                }
            }
            return newB;
        }

        public Transaction AddTransaction(Config config, UInt16 index, string from, string to, string data)
        {
            Int64 timestamp = DateTime.UtcNow.Ticks;
            string hashable = index + from + to + data + timestamp;
            return new Transaction(index, Cryptography.CalculateHash(config, hashable), from, to, data, timestamp);
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