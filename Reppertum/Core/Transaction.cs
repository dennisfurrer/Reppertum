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

        public Transaction(UInt16 _index, string _hash, string _fromAddress, string _toAddress, string _data, Int64 _timestamp)
        {
            Index = _index;
            Hash = _hash;
            FromAddress = _fromAddress;
            ToAddress = _toAddress;
            Data = _data;
            Timestamp = _timestamp;
        }
    }
}