namespace CellularAutomaton
{
    public struct CellularAutomatonSimulationConfig
    {
        public int Seed { get; }
        public int GridSize { get; }
        public float R { get; }
        public int N { get; }
        public int T { get; }
        public int M { get; }

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