using System;

namespace Reppertum.Core
{
    public class Transaction : ITransaction
    {
        public UInt16 Index { get; set; }
        public string Hash { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Data { get; set; }
        public Int64 Timestamp { get; set; }

        public Transaction(UInt16 index, string hash, string fromAddress, string toAddress, string data, Int64 timestamp)
        {
            Index = index;
            Hash = hash;
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Data = data;
            Timestamp = timestamp;
        }
    }
}