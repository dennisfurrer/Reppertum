namespace Reppertum.Core
{
    public interface IConsensus
    {
        bool ProofOfWork(Block prevB, Block newB, ushort difficulty = 5);
    }
}