namespace CellularAutomaton
{
    public struct CellularAutomatonSimulationConfig
    {
        public int Seed { get; }
        public float R { get; }
        public int N { get; }
        public int T { get; }
        public int M { get; }

        public CellularAutomatonSimulationConfig(int seed, float r, int n, int t, int m)
        {
            Seed = seed;
            R = r;
            N = n;
            T = t;
            M = m;
        }
    }
}