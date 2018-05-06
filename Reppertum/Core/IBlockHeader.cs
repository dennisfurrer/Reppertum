namespace Reppertum.Core
{
    public interface IBlockHeader
    {
        string hash { get; set; }
        ushort index { get; set; }
        string previousHash { get; set; }
        long timestamp { get; set; }
    }
}