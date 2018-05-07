using System;
using System.Collections.Generic;
using Reppertum.Crypto;

namespace Reppertum.Core
{
    public class Hash : IHash
    {
        public string GetHash(UInt16 index, string prevHash, Int64 timestamp)
        {
            Cryptography cryptography = new Cryptography();
            return cryptography.Sha256(index + prevHash + timestamp);
        }
    }
}
