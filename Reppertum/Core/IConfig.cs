namespace Reppertum.Core
{
    interface IConfig
    {
        string ConsensusType { get; set; }
        string HashType { get; set; }
        string NetworkType { get; set; }
    }
}