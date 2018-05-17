using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Reppertum.Core;
using Reppertum.Network;

namespace Reppertum
{
    internal static class Program
    {
        private static Blockchain _chain;
        private static string _currHash = string.Empty;
        static Config config = new Config();

        private static void Main(string[] args)
        {
            Regex regex = new Regex("(?<name>-{1,2}\\S*)(?:[=:]?|\\s+)(?<value>[^-\\s].*?)?(?=\\s+[-\\/]|$)");
            List<KeyValuePair<string, string>> matches = (from match in regex.Matches(string.Join(" ", args)).Cast<Match>()
                                                          select new KeyValuePair<string, string>(match.Groups["name"].Value, match.Groups["value"].Value)).ToList();        
            foreach (KeyValuePair<string, string> _match in matches)
            {
                switch (_match.Key.Replace("-", string.Empty).ToLower())
                {
                    case "c":
                        string consensusType = _match.Value.ToLower();
                        config.ConsensusType = consensusType;
                        break;
                    case "h":
                        string hashType = _match.Value.ToLower();
                        config.HashType = hashType;
                        break;
                    case "n":
                        string networkType = _match.Value.ToLower();
                        config.NetworkType = networkType;
                        break;
                    default:
                        break;
                }
            }
            Setup();
            Console.ReadKey();
        }

        private static void Setup()
        {
            Console.Write("(1) New blockchain\n(2) Test UPnP\n");
            string execType = Console.ReadLine();

            if (execType == "1")
            {
                NewChain();
            }
            else if (execType == "2")
            {
                UPnP();
            }
            else
            {
                Console.WriteLine("Exiting");
            }
        }

        private static void NewChain()
        {
            Console.Clear();
            _chain = new Blockchain(config);
            _currHash = _chain.FirstHash;
            Console.WriteLine("Initialised Genesis Block with default values.");

            bool ok = true;

            while (ok)
            {
                Console.WriteLine("(1) Add new block\n(2) View blockchain\n(3) View block\n(4) Quit\n");
                string execType = Console.ReadLine();

                switch (execType)
                {
                    case "1":
                        AddBlock();
                        break;
                    case "2":
                        ViewChain();
                        break;
                    case "3":
                        ViewBlock();
                        break;
                    case "4":
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Unknown command\n");
                        break;
                }
            }
        }

        private static void AddBlock()
        {
            string execType = string.Empty;
            UInt16 txCount = 0;
            List<Transaction> transactions = new List<Transaction>();

            do
            {
                Console.Clear();
                Console.WriteLine("(1) Add transaction\n(2) Mine block\n");
                execType = Console.ReadLine();
                if (execType == "1")
                {
                    transactions.Add(AddTransaction(txCount));
                }
                else
                {
                    break;
                }
                txCount++;
            }
            while (execType == "1");

            Block block = _chain.AddBlock(config, _currHash, transactions, DateTime.UtcNow.Ticks);

            Console.WriteLine($"Added Block {block.Header.Index} with Hash: {block.Header.Hash})\n");

            _currHash = block.Header.Hash;
        }

        private static Transaction AddTransaction(UInt16 Index)
        {
            Console.Clear();
            Console.Write("Transaction Data: ");
            string Data = Console.ReadLine();
            return _chain.AddTransaction(config, Index, "Network", "Network", Data);
        }

        private static void ViewChain()
        {
            Console.Clear();

            foreach (Block currBlock in _chain.Chain)
            {
                Console.WriteLine($"\nBlock {currBlock.Header.Index}\n---------------\nHash: {currBlock.Header.Hash}\nPrevious Hash: {currBlock.Header.PreviousHash}\nMerkle Root: {currBlock.MerkleRoot}\nTimestamp: {currBlock.Header.Timestamp}");
                foreach (Transaction t in currBlock.Data)
                {
                    Console.WriteLine($"Transaction #{t.Index}: \n\tHash: {t.Hash}\n\tFrom: {t.FromAddress}\n\tTo: {t.ToAddress}\n\tData: {currBlock.Data[t.Index].Data}\n\tTimestamp: {t.Timestamp}\n");
                }
            }
        }

        private static void ViewBlock()
        {
            Console.Clear();
            Console.WriteLine("Enter Block Index: ");
            UInt16 Index = UInt16.Parse(Console.ReadLine());
            Int32 chainSize = _chain.GetNumberOfBlocks() - 1;
            // Int32 txIndex = 0;
            if (Index <= chainSize)
            {
                Block block = _chain.GetBlock(Index);
                Console.WriteLine($"\nBlock {block.Header.Index}\n---------------\nHash: {block.Header.Hash}\nPrevious Hash: {block.Header.PreviousHash}\nMerkle Root: {block.MerkleRoot}\nTimestamp: {block.Header.Timestamp}");
                foreach (Transaction t in block.Data)
                {
                    Console.WriteLine($"Transaction #{t.Index}: \n\tHash: {t.Hash}\n\tFrom: {t.FromAddress}\n\tTo: {t.ToAddress}\n\tData: {block.Data[t.Index].Data}\n\tTimestamp: {t.Timestamp}\n");
                }
            }
            else
            {
                Console.WriteLine($"Block { Index } does not exist\n");
            }
        }

        private static void UPnP()
        {
            Console.Clear();
            Console.WriteLine("Checking if you have a UPnP-enabled router...");
            try
            {
                Console.Clear();
                Network.UPnP.Discover();
                Console.WriteLine("You have an UPnP-enabled router and your IP is: " + Network.UPnP.GetExternalIP());
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("You do not have an UPnP-enabled router.");
            }
        }
    }
}
