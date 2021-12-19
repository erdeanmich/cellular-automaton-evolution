using UI;
using UnityEngine;
using UnityEngine.UI;

namespace CellularAutomatonEvolution
{
    public class CellularAutomatonEvolutionSceneHandler : MonoBehaviour
    {
        [SerializeField]
        private InputField rInput;
        
        [SerializeField]
        private InputField nInput;
        
        [SerializeField]
        private InputField tInput;
        
        [SerializeField]
        private InputField mInput;

        [SerializeField] 
        private InputField gridSizeInput;
        
        [SerializeField] 
        private InputField populationSizeInput;

        [SerializeField]
        private Button startEvolutionButton;

        [SerializeField]
        private Button importPopulationButton;

        [SerializeField] 
        private Button exportPopulationButton;

        private void InitNewPopulation(CellularAutomatonEvolutionConfig cellularAutomatonEvolutionConfig)
        {
            // init array of size pop
            // init caconfig with random values within ranges 
            // put in array
            
        }
    }
}