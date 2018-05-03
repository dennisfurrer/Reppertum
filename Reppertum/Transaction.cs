using System;

namespace Reppertum {
    public class Transaction {
        public readonly UInt16 Index;
        public readonly string Hash, FromAddress, ToAddress, Data;
        public readonly Int64 Timestamp;

        public Transaction(UInt16 index, string hash, string fromAddress, string toAddress, string data, Int64 timestamp) {
            Index = index;
            Hash = hash;
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Data = data;
            Timestamp = timestamp;
        }
    }
}