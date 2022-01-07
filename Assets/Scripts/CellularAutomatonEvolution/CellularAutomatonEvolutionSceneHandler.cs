using System;
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
        private Button stopEvolutionButton;

        [SerializeField]
        private Button resetAllValuesButton;

        [SerializeField]
        private Button importPopulationButton;

        [SerializeField] 
        private Button exportPopulationButton;

        [SerializeField]
        private CellularAutomatonStats cellularAutomatonStats;
        

        private GeneticEvolutionSimulation geneticEvolutionSimulation;
        private bool isRunning = false;
        private int iterationCount = 0;

        private void Start()
        {
            startEvolutionButton.onClick.AddListener(OnPressStart);
            stopEvolutionButton.onClick.AddListener(OnPressStop);
            resetAllValuesButton.onClick.AddListener(OnPressResetAll);
        }

        private void OnDestroy()
        {
            startEvolutionButton.onClick.RemoveAllListeners();
        }

        private void Update()
        {
            if (!isRunning)
            {
                return;
            }
            
            geneticEvolutionSimulation.ExecuteEvolutionStep();
            iterationCount++;
            cellularAutomatonStats.UpdateStats(iterationCount, geneticEvolutionSimulation.GetAverageFitness());

        }
        
        private void OnPressStart()
        {
            var cellularAutomatonEvolutionConfig = CreateCellularAutomatonEvolutionConfig();
            geneticEvolutionSimulation = new GeneticEvolutionSimulation(cellularAutomatonEvolutionConfig);
            iterationCount = 0;
            cellularAutomatonStats.StartEvolution();
            startEvolutionButton.interactable = false;
            stopEvolutionButton.interactable = true;
            isRunning = true;
        }

        private void OnPressStop()
        {
            startEvolutionButton.interactable = true;
            stopEvolutionButton.interactable = false;
            isRunning = false;
            cellularAutomatonStats.StopEvolution();
        }

        private void OnPressResetAll()
        {
            if (isRunning)
            {
                OnPressStop();
            }
            
            rInput.text = string.Empty;
            mInput.text = string.Empty;
            nInput.text = string.Empty;
            tInput.text = string.Empty;
            gridSizeInput.text = string.Empty;
            populationSizeInput.text = string.Empty;
        }
        
        private CellularAutomatonEvolutionConfig CreateCellularAutomatonEvolutionConfig()
        {
            return new CellularAutomatonEvolutionConfig
            {
                GridSize = RetrieveGridSizeFromInput(),
                PopulationSize = RetrievePopulationSizeFromInput(),
                maxR = RetrieveMaxRFromInput(),
                maxN = RetrieveMaxNFromInput(),
                maxT = RetrieveMaxTFromInput(),
                maxM = RetrieveMaxMFromInput()
            };
        }

        private int RetrieveMaxMFromInput()
        {
            return Convert.ToInt32(mInput.text);
        }

        private int RetrieveMaxTFromInput()
        {
            return Convert.ToInt32(tInput.text);
        }

        private int RetrieveMaxNFromInput()
        {
            return Convert.ToInt32(nInput.text);
        }

        private int RetrieveMaxRFromInput()
        {
            return Convert.ToInt32(rInput.text);
        }

        private int RetrievePopulationSizeFromInput()
        {
            return Convert.ToInt32(populationSizeInput.text);
        }

        private int RetrieveGridSizeFromInput()
        {
            return Convert.ToInt32(gridSizeInput.text);
        }
    }
}