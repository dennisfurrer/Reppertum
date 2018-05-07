using Reppertum.Crypto;

namespace Reppertum.Core

{
    public interface IHash
    {
        string GetHash(ushort index, string prevHash, long timestamp);
    }
}