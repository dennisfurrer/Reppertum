using System;
using System.Collections.Generic;
using Reppertum.Core;

namespace Reppertum
{
    internal static class Program
    {
        private static Blockchain _chain;
        private static string _currhash = string.Empty;

        private static void Main(string[] args)
        {
            //todo Create and start node 

            Console.WriteLine();
            Setup();
            Console.ReadKey();
        }

        private static void Setup()
        {
            Console.Write("(1) New blockchain: ");
            string execType = Console.ReadLine();

            if (execType == "1")
            {
                NewChain();
            }
            else
            {
                Console.WriteLine("Exiting");
            }
        }

        private static void NewChain()
        {
            Console.Clear();
            _chain = new Blockchain();
            _currhash = _chain.FirstHash;
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

            Block block = _chain.AddBlock(_currhash, transactions, DateTime.UtcNow.Ticks);

            Console.WriteLine($"Added Block {block.header.index} with hash: {block.header.hash})\n");

            _currhash = block.header.hash;
        }

        private static Transaction AddTransaction(UInt16 index)
        {
            Console.Clear();
            Console.Write("Transaction data: ");
            string data = Console.ReadLine();
            return _chain.AddTransaction(index, "Network", "Network", data);
        }

        private static void ViewChain()
        {
            Console.Clear();

            foreach (Block currBlock in _chain.Chain)
            {
                Console.WriteLine($"\nBlock {currBlock.header.index}\n---------------\nhash: {currBlock.header.hash}\nPrevious hash: {currBlock.header.previousHash}\nMerkle Root: {currBlock.merkleRoot}\ntimestamp: {currBlock.header.timestamp}");
                foreach (Transaction t in currBlock.data)
                {
                    Console.WriteLine($"Transaction #{t.index}: \n\thash: {t.hash}\n\tFrom: {t.fromAddress}\n\tTo: {t.toAddress}\n\tdata: {currBlock.data[t.index].data}\n\ttimestamp: {t.timestamp}\n");
                }
            }
        }

        private static void ViewBlock()
        {
            Console.Clear();
            Console.WriteLine("Enter Block index: ");
            UInt16 index = UInt16.Parse(Console.ReadLine());
            Int32 chainSize = _chain.GetNumberOfBlocks() - 1;
            // Int32 txindex = 0;
            if (index <= chainSize)
            {
                Block block = _chain.GetBlock(index);
                Console.WriteLine($"\nBlock {block.header.index}\n---------------\nhash: {block.header.hash}\nPrevious hash: {block.header.previousHash}\nMerkle Root: {block.merkleRoot}\ntimestamp: {block.header.timestamp}");
                foreach (Transaction t in block.data)
                {
                    Console.WriteLine($"Transaction #{t.index}: \n\thash: {t.hash}\n\tFrom: {t.fromAddress}\n\tTo: {t.toAddress}\n\tdata: {block.data[t.index].data}\n\ttimestamp: {t.timestamp}\n");
                }
            }
            else
            {
                Console.WriteLine($"Block { index } does not exist\n");
            }
        }
    }
}