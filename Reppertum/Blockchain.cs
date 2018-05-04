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
            string hash = GetHash(_current, prevHash, data, timestamp);
            FirstHash = hash;
            Block b = new Block(_current, prevHash, hash, data, timestamp);
            Chain.Add(b);
            _current++;
        }

        public Block AddBlock(string prevHash, List<Transaction> data, Int64 timestamp) 
        {
            Block prevB = Chain[_current-1];
            string hash = GetHash(_current, prevHash, data, timestamp);
            Block newB = new Block(_current, prevHash, hash, data, timestamp);

            if (ProofOfWork((Block)prevB, newB)) 
            {
                Chain.Add(newB);
                _current++;
            }
            else if (prevB.PreviousHash != prevHash && prevB.Index != 0) 
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

        public string GetHash(UInt16 i, string prevHash, List<Transaction> data, Int64 timestamp) 
        {
            return Cryptography.Sha256(i.ToString() + prevHash + data + timestamp);
        }

        public Int32 GetNumberOfBlocks() 
        {
            return Chain.Count;
        }

        private bool ProofOfWork(Block prevB, Block newB, UInt16 difficulty = 5)
        {
            Console.Clear();
            Console.WriteLine("Computing Proof-of-Work...");
            bool valid = false;
            UInt32 nonce = 0;
            string header = newB.Index + newB.PreviousHash;
            string hash;
            
            do 
            {
                hash = Cryptography.Sha256(header + nonce);
                nonce++;
            } 
            while (hash.Substring(0, difficulty - 1) != "0000");
            
            valid = (hash.Substring(0, difficulty-1) == "0000");
            
            Console.WriteLine("Successfully computed!");
            
            return valid;
        }
    }
}