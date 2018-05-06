namespace Reppertum.Core
{
    public interface ITransaction
    {
        string data { get; set; }
        string fromAddress { get; set; }
        string hash { get; set; }
        ushort index { get; set; }
        long timestamp { get; set; }
        string toAddress { get; set; }
    }
}