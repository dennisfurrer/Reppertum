namespace Reppertum.Core
{
    public interface IConsensus
    {
        bool ConsensusCalculation(Config config, Block prevB, Block newB, ushort difficulty = 5);
    }
}