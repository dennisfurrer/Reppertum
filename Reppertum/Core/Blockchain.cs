﻿using System;
using System.Collections.Generic;
using Reppertum.Crypto;

namespace Reppertum.Core
{
    public class Blockchain
    {
        public readonly List<Block> Chain = new List<Block>();

        private UInt16 _current = 0;

        public Blockchain()
        {
            Hash hash = new Hash();
            Block genesisBlock = new Block(new BlockHeader(0, hash.GetHash(0, "", DateTime.UtcNow.Ticks), "0", DateTime.UtcNow.Ticks), null);
            Chain.Add(genesisBlock);
            _current++;
        }

        public Block AddBlock(string prevHash, List<Transaction> data, Int64 timestamp)
        {
            Block prevB = Chain[_current - 1];
            Hash hash = new Hash();
            Block newB = new Block(new BlockHeader(_current, hash.GetHash(_current, prevHash, timestamp), prevHash, timestamp), data);
            Consensus consensus = new Consensus();
            if (consensus.ProofOfWork(prevB, newB))
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
            Cryptography crypto = new Cryptography();
            Int64 timestamp = DateTime.UtcNow.Ticks;
            string hashable = index + from + to + data + timestamp;
            return new Transaction(index, crypto.Sha256(hashable), from, to, data, timestamp);
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