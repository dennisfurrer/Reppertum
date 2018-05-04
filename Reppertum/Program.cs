using System;
using System.Collections.Generic;

namespace Reppertum 
{
    class Program 
    {
        private static Blockchain _chain;
        private static string _currHash = string.Empty;

        private static void Main(string[] args) 
        {
            Console.WriteLine("Welcome! To quit -> Control + c \n");
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

            List<Transaction> genTx = new List<Transaction>{new Transaction(0, "0", "Network", "Network", "Genesis Block", DateTime.UtcNow.Ticks)};
            _chain = new Blockchain("0", genTx, DateTime.UtcNow.Ticks);
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
                        Console.WriteLine("Exiting");
                        ok = false;
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
                if (execType == "1" || execType == "1") 
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

            Block block = _chain.AddBlock(_currHash, transactions, DateTime.UtcNow.Ticks);

            Console.WriteLine($"Added Block {block.Header.Index} with Hash: {block.Header.Hash})\n");

            _currHash = block.Header.Hash;
        }

        private static Transaction AddTransaction(UInt16 index) 
        {
            Console.Clear();
            Console.Write("Transaction Data: ");
            string data = Console.ReadLine();
            return _chain.AddTransaction(index, "Network", "Network", data);
        }
        
        private static void ViewChain() 
        {
            Console.Clear();
            
            foreach (Block currBlock in _chain.Chain) 
            {
                Console.WriteLine($"\nBlock {currBlock.Header.Index}\n---------------\nHash: {currBlock.Header.Hash}\nPrevious Hash: {currBlock.Header.PreviousHash}\nTimestamp: {currBlock.Header.Timestamp}");
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
            UInt16 index = UInt16.Parse(Console.ReadLine());
            Int32 chainSize = _chain.GetNumberOfBlocks() - 1;
            Int32 txIndex = 0;
            if (index <= chainSize) 
            {
                Block block = _chain.GetBlock(index);
                Console.WriteLine($"\nBlock {block.Header.Index}\n---------------\nHash: {block.Header.Hash}\nPrevious Hash: {block.Header.PreviousHash}\nTimestamp: {block.Header.Timestamp}");
                foreach (Transaction t in block.Data) 
                {
                    Console.WriteLine($"Transaction #{t.Index}: \n\tHash: {t.Hash}\n\tFrom: {t.FromAddress}\n\tTo: {t.ToAddress}\n\tData: {block.Data[t.Index].Data}\n\tTimestamp: {t.Timestamp}\n");
                }
            }
            else 
            {
                Console.WriteLine($"Block { index } does not exist\n");
            }
        }
    }
}