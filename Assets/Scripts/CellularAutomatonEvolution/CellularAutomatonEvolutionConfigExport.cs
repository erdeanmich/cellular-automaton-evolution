using System;
using System.Collections.Generic;
using CellularAutomaton;

namespace CellularAutomatonEvolution
{
    [Serializable]
    public struct CellularAutomatonEvolutionConfigExport
    {
        public CellularAutomatonEvolutionConfig cellularAutomatonEvolutionConfig;
        public List<CellularAutomatonSimulationConfig> population;
    }
}