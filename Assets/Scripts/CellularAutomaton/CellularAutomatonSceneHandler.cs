using System;
using System.Collections;
using System.Globalization;
using System.IO;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.UI;

namespace CellularAutomaton
{
    public class CellularAutomatonSceneHandler : MonoBehaviour
    {
        [SerializeField] 
        private CellularAutomatonSimulation cellularAutomatonSimulation;

        [Header("Buttons")] 
        [SerializeField] private Button startCAButton;

        [SerializeField] 
        private Button exportCAButton;

        [SerializeField] 
        private Button importCAButton;

        [Header("Simulation Inputs")] 
        [SerializeField]
        private InputField seedInput;

        [SerializeField] 
        private InputField gridSizeInput;

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
            exportCAButton.onClick.AddListener(OnClickExport);
            importCAButton.onClick.AddListener(OnClickImport);
        }

        private void OnClickStart()
        {
            CellularAutomatonSimulationConfig cellularAutomatonSimulationConfig =
                CreateCellularAutomatonSimulationConfig();
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

        private void OnClickImport()
        {
            StartCoroutine(ShowImportAutomatonDialog());
        }

        private void OnClickExport()
        {
            StartCoroutine(ShowExportAutomatonToDialog());
        }

        private IEnumerator ShowImportAutomatonDialog()
        {
            yield break;
        }

        private IEnumerator ShowExportAutomatonToDialog()
        {
            FileBrowser.SetFilters(true, CellularAutomataConstants.ExportFileFilter);
            FileBrowser.SetDefaultFilter(CellularAutomataConstants.DefaultExportFileFilter);
            yield return FileBrowser.WaitForSaveDialog(
                FileBrowser.PickMode.Files,
                false,
                null, 
                CellularAutomataConstants.InitialExportFilename, 
                CellularAutomataConstants.ExportDialogTitle,
                CellularAutomataConstants.ExportSaveButtonText
            );

            if (!FileBrowser.Success)
            {
                yield break;
            }
            
            // only one possible path here
            var pathToFile = FileBrowser.Result[0];
            ExportCurrentCaConfigToFile(pathToFile);
        }

        private void ExportCurrentCaConfigToFile(string pathToFile)
        {
            var filename = Path.GetFileName(pathToFile);
            var directoryPath = Path.GetDirectoryName(pathToFile);
            var simulationConfig = CreateCellularAutomatonSimulationConfig();
            var json = JsonUtility.ToJson(simulationConfig, true);
            var pathToJson = FileBrowserHelpers.CreateFileInDirectory(directoryPath, filename);
            FileBrowserHelpers.WriteTextToFile(pathToJson, json);
        }

        private CellularAutomatonSimulationConfig CreateCellularAutomatonSimulationConfig()
        {
            // add error handling 
            return new CellularAutomatonSimulationConfig(
                RetrieveSeed(),
                RetrieveGridSize(),
                RetrieveR(),
                RetrieveN(),
                RetrieveT(),
                RetrieveM()
            );
        }

        private int RetrieveGridSize()
        {
            return Convert.ToInt32(gridSizeInput.text);
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
                Debug.Log($"Current seed is {_seed}");
            }

            seedInput.text = _seed.ToString();
            return _seed;
        }

        private void OnDestroy()
        {
            startCAButton.onClick.RemoveAllListeners();
            importCAButton.onClick.RemoveAllListeners();
            exportCAButton.onClick.RemoveAllListeners();
        }
    }
}