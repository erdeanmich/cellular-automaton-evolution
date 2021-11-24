using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace CellularAutomaton
{
    public class CellularAutomatonSceneHandler : MonoBehaviour
    {
        [SerializeField]
        private Button startCAButton;

        [SerializeField]
        private CellularAutomatonSimulation cellularAutomatonSimulation;

        [Header("Simulation Inputs")]
        [SerializeField]
        private InputField seedInput;

        [SerializeField]
        private InputField rInput;

        [SerializeField]
        private InputField nInput;

        [SerializeField]
        private InputField tInput;

        [SerializeField]
        private InputField mInput;
        
        [SerializeField]
        private Toggle stepByStep;
        
        private void Start()
        {
            
            startCAButton.onClick.AddListener(OnClickStart);
        }

        private void OnClickStart()
        {
            CellularAutomatonSimulationConfig cellularAutomatonSimulationConfig = CreateCellularAutomatonSimulationConfig();
            cellularAutomatonSimulation.SetConfig(cellularAutomatonSimulationConfig);
            
            if (stepByStep.isOn)
            {
                cellularAutomatonSimulation.StartSimulationSlow();
            }
            else
            {
                cellularAutomatonSimulation.StartSimulation();
            }
        }

        private CellularAutomatonSimulationConfig CreateCellularAutomatonSimulationConfig()
        {
            return new CellularAutomatonSimulationConfig(
                RetrieveSeed(),
                RetrieveR(),
                RetrieveN(),
                RetrieveT(),
                RetrieveM()
            );
        }

        private int RetrieveM()
        {
            return Convert.ToInt32(mInput.text);
        }

        private int RetrieveT()
        {
            return Convert.ToInt32(tInput.text);
        }

        private int RetrieveN()
        {
            return Convert.ToInt32(nInput.text);
        }

        private float RetrieveR()
        {
            return (float)Convert.ToDouble(rInput.text);
        }

        private int RetrieveSeed()
        {
            int _seed = Convert.ToInt32(seedInput.text);
            if (_seed == 0)
            {
                _seed = Time.time.ToString(CultureInfo.CurrentCulture).GetHashCode();
                Debug.Log($"Current seed is {seedInput}");
            }

            return _seed;
        }

        private void OnDestroy()
        {
            startCAButton.onClick.RemoveAllListeners();
        }
    }
}