using System;

namespace CellularAutomatonEvolution
{
    [Serializable]
    public struct CellularAutomatonEvolutionConfig
    {
        public int GridSize;
        public int PopulationSize;
        public double maxR;
        public int maxN;
        public int maxT;
        public int maxM;
    }
}