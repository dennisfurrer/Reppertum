using System;
using Reppertum.Crypto;

namespace Reppertum.Core
{
    public class Consensus : IConsensus
    {

        public bool ConsensusAchieved(Config config, Block prevB, Block newB, UInt16 difficulty = 5)
        {
            switch (config.ConsensusType)
            {
                case ("pow"):
                    return ProofOfWork(config, prevB, newB, difficulty);
                default:
                    return ProofOfWork(config, prevB, newB, difficulty);
            }
        }
        private bool ProofOfWork(Config config, Block prevB, Block newB, UInt16 difficulty = 5)
        {
            Console.Clear();
            Console.WriteLine("Computing Proof-of-Work...");
            bool valid = false;
            UInt32 nonce = 0;
            string Header = newB.Header.Index + newB.Header.PreviousHash;
            string hash;
            string diff = string.Empty;
            for (int i = 0; i < difficulty - 1; i++)
            {
                diff += "0";
            }
            do
            {
                hash = Cryptography.CalculateHash(config, Header + nonce);
                nonce++;
            }
            while (hash.Substring(0, difficulty - 1) != diff);

            valid = (hash.Substring(0, difficulty - 1) == diff);

            Console.WriteLine("Successfully computed!");

            return valid;
        }
    }
}