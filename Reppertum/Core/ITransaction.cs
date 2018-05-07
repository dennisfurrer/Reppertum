namespace Reppertum.Core
{
    public interface ITransaction
    {
        string Data { get; set; }
        string FromAddress { get; set; }
        string Hash { get; set; }
        ushort Index { get; set; }
        long Timestamp { get; set; }
        string ToAddress { get; set; }
    }
}