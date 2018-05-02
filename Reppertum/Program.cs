using System;

namespace Reppertum
{
    class Program
    {
        private static Blockchain _chain;
        private static string _currHash = string.Empty;

        private static void Main(string[] args) {
            Console.WriteLine("Welcome! To quit -> Control + c \n");
            Setup();
            Console.ReadKey();
        }

        private static void Setup() {
            Console.Write("(1) New blockchain: ");
            string execType = Console.ReadLine();

            if (execType == "1") {
                NewChain();
            }
            else {
                Console.WriteLine("Exiting");
                return;
            }
        }

        private static void NewChain() {
            Console.Clear();

            _chain = new Blockchain("0", "Genesis Block", DateTime.UtcNow.Ticks);
            _currHash = _chain.FirstHash;
            Console.WriteLine("Initialised Genesis Block with default values.");

            bool ok = true;

            while (ok) {
                Console.WriteLine("(1) Add new block\n(2) View blockchain\n(3) View block\n(4) Quit\n");
                string execType = Console.ReadLine();

                switch (execType)
                {
                    case "1":
                        Add();
                        break;
                    case "2":
                        ViewChain();
                        break;
                    case "3":
                        ViewBlock();
                        break;
                    case "4":
                        Console.WriteLine("Exiting");
                        ok = false;
                        break;
                    default:
                        Console.WriteLine("Unknown command\n");
                        break;
                }
            }
        }

        private static void ViewBlock()
        {
            Console.Clear();
            Console.WriteLine("Block Index: ");
            UInt16 index = UInt16.Parse(Console.ReadLine());
            Int32 chainSize = _chain.GetNumberOfBlocks() - 1;
            if (index <= chainSize) {
                Block block = _chain.GetBlock(index);
                Console.WriteLine($"Block { block.Index }\nHash: { block.Hash }\nPrevious Hash: { block.PreviousHash }\nData: { block.Data }\nTimestamp: { block.Timestamp }\n");             
            }
            else {
                Console.WriteLine($"Block { index } does not exist\n");
            }
        }
        
        private static void ViewChain() {
            Console.Clear();
            
            foreach (Block currBlock in _chain.Chain) {
                Console.WriteLine($"Block { currBlock.Index }\nHash: { currBlock.Hash }\nPrevious Hash: { currBlock.PreviousHash }\nData: { currBlock.Data }\nTimestamp: { currBlock.Timestamp }\n");
            }
        }

        private static void Add() {
            Console.Clear();
            Console.Write("Data: ");
            string data = Console.ReadLine();
            
            Block c = _chain.AddBlock(_currHash, data, DateTime.UtcNow.Ticks);

            Console.WriteLine($"Added Block { c.Index } with Hash: { c.Hash })\n");

            _currHash = c.Hash;
        }
    }
}