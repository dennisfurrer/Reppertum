namespace Reppertum.Core
{
    public interface IConsensus
    {
        bool ConsensusAchieved(Config config, Block prevB, Block newB, ushort difficulty = 5);
    }
}