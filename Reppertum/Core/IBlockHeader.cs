namespace Reppertum.Core
{
    public interface IBlockHeader
    {
        string Hash { get; set; }
        ushort Index { get; set; }
        string PreviousHash { get; set; }
        long Timestamp { get; set; }
    }
}