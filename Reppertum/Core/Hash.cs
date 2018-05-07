using System;
using System.Collections.Generic;
using Reppertum.Crypto;

namespace Reppertum.Core
{
    public class Hash
    {
        public static string GetHash(UInt16 index, string prevHash, Int64 timestamp)
        {
            return Cryptography.Sha256(index + prevHash + timestamp);
        }
    }
}
