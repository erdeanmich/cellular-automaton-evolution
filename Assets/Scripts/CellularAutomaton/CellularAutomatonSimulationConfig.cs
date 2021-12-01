using System;

namespace CellularAutomaton
{
    [Serializable]
    public struct CellularAutomatonSimulationConfig
    {
        public int Seed;
        public int GridSize;
        public float R;
        public int N;
        public int T;
        public int M;

        public CellularAutomatonSimulationConfig(int seed, int gridSize, float r, int n, int t, int m)
        {
            Seed = seed;
            GridSize = gridSize;
            R = r;
            N = n;
            T = t;
            M = m;
        }
    }
}