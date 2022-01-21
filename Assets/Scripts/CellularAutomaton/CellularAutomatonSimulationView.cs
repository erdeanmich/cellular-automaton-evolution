using FitnessEvaluation;
using UnityEngine;

namespace CellularAutomaton
{
    public class CellularAutomatonSimulationView : MonoBehaviour
    {
        [SerializeField]
        private CellularAutomatonVisualizer cellularAutomatonVisualizer;

        private readonly CellularAutomatonSimulation cellularAutomatonSimulation = new CellularAutomatonSimulation();
        private FitnessEvaluator _fitnessEvaluator =  new FitnessEvaluator();


        public void StartSimulation()
        {
            cellularAutomatonSimulation.Simulate();
        }

        public void StartSimulationSlow()
        {
            StartCoroutine(cellularAutomatonSimulation.SimulateSlow(VisualizeAutomaton));
        }

        private void VisualizeAutomaton()
        {
            if (cellularAutomatonVisualizer != null)
            {
                cellularAutomatonVisualizer.VisualizeAutomaton(cellularAutomatonSimulation.GetCells());
            }

            double fitness = _fitnessEvaluator.DetermineFitness(cellularAutomatonSimulation.GetCells());
            Debug.Log(fitness);
        }
        
        public void SetConfig(CellularAutomatonSimulationConfig cellularAutomatonSimulationConfig)
        {
            cellularAutomatonSimulation.SetConfig(cellularAutomatonSimulationConfig);
        }
    }
}
