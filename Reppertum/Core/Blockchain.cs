using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Reppertum 
{
    public class Blockchain 
    {
        public readonly List<Block> Chain = new List<Block>();
        public readonly string FirstHash;
        
        private UInt16 _current = 0;

        public Blockchain(string prevHash, List<Transaction> data, Int64 timestamp) 
        {
            string hash = Block.GetHash(_current, prevHash, timestamp);
            FirstHash = hash;
            Block b = new Block(new BlockHeader(_current, hash, prevHash, timestamp), data);
            Chain.Add(b);
            _current++;
        }

        public Block AddBlock(string prevHash, List<Transaction> data, Int64 timestamp) 
        {
            Block prevB = Chain[_current-1];
            string hash = Block.GetHash(_current, prevHash, timestamp);
            Block newB = new Block(new BlockHeader(_current, hash, prevHash, timestamp), data);

            if (Consensus.ProofOfWork((Block)prevB, newB)) 
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

            return newB;
        }
        
        public Transaction AddTransaction(UInt16 index, string from, string to, string data) 
        {
            string hashable = index + from + to + data;
            return new Transaction(index, Cryptography.Sha256(hashable), from, to, data, DateTime.UtcNow.Ticks);
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