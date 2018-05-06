using System;

namespace Reppertum.Core 
{
    public class Transaction 
    {
        public UInt16 index { get; set; }
        public string hash { get; set; }
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string data { get; set; }
        public Int64 timestamp { get; set; }

        public Transaction(UInt16 _index, string _hash, string _fromAddress, string _toAddress, string _data, Int64 _timestamp) 
        {
            index = _index;
            hash = _hash;
            fromAddress = _fromAddress;
            toAddress = _toAddress;
            data = _data;
            timestamp = _timestamp;
        }
    }
}