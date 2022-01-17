using System;
using System.Collections;
using System.Globalization;
using System.IO;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace CellularAutomaton
{
    public class CellularAutomatonSceneHandler : MonoBehaviour
    {
        [SerializeField] 
        private CellularAutomatonSimulationView cellularAutomatonSimulationView;

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
            SetFileBrowserFilters();

            startCAButton.onClick.AddListener(OnClickStart);
            exportCAButton.onClick.AddListener(OnClickExport);
            importCAButton.onClick.AddListener(OnClickImport);
        }

        private void OnClickStart()
        {
            CellularAutomatonSimulationConfig cellularAutomatonSimulationConfig =
                CreateCellularAutomatonSimulationConfig();
            cellularAutomatonSimulationView.SetConfig(cellularAutomatonSimulationConfig);

            if (stepByStep.isOn)
            {
                cellularAutomatonSimulationView.StartSimulationSlow();
            }
            else
            {
                cellularAutomatonSimulationView.StartSimulation();
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
            yield return FileBrowser.WaitForLoadDialog(
                FileBrowser.PickMode.Files,
                false,
                null,
                CellularAutomataConstants.ImportDialogTitle,
                CellularAutomataConstants.ImportButtonText
            );

            if (!FileBrowser.Success)
            {
                yield break;
            }

            var pathToFile = FileBrowser.Result[0];
            ImportAutomatonFromFile(pathToFile);
        }

        private void ImportAutomatonFromFile(string pathToFile)
        {
            var caJson = FileBrowserHelpers.ReadTextFromFile(pathToFile);

            try
            {
                var cellularAutomatonSimulationConfig = JsonUtility.FromJson<CellularAutomatonSimulationConfig>(caJson);
                rInput.text = cellularAutomatonSimulationConfig.R.ToString(CultureInfo.CurrentCulture);
                nInput.text = cellularAutomatonSimulationConfig.N.ToString(CultureInfo.CurrentCulture);
                tInput.text = cellularAutomatonSimulationConfig.T.ToString(CultureInfo.CurrentCulture);
                mInput.text = cellularAutomatonSimulationConfig.M.ToString(CultureInfo.CurrentCulture);
                gridSizeInput.text = cellularAutomatonSimulationConfig.GridSize.ToString(CultureInfo.CurrentCulture);
                seedInput.text = cellularAutomatonSimulationConfig.Seed.ToString(CultureInfo.CurrentCulture);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                Debug.LogError($"Not possible to parse cellular automaton json at {pathToFile}");
            }
        }

        private IEnumerator ShowExportAutomatonToDialog()
        {
            yield return FileBrowser.WaitForSaveDialog(
                FileBrowser.PickMode.Files,
                false,
                null, 
                CellularAutomataConstants.InitialExportFilename, 
                CellularAutomataConstants.ExportDialogTitle,
                CellularAutomataConstants.ExportButtonText
            );

            if (!FileBrowser.Success)
            {
                yield break;
            }
            
            // only one possible path here
            var pathToFile = FileBrowser.Result[0];
            ExportCurrentCaConfigToFile(pathToFile);
        }

        private static void SetFileBrowserFilters()
        {
            FileBrowser.SetFilters(true, CellularAutomataConstants.ExportFileFilter);
            FileBrowser.SetDefaultFilter(CellularAutomataConstants.DefaultExportFileFilter);
        }

        private void ExportCurrentCaConfigToFile(string pathToFile)
        {
            var simulationConfig = CreateCellularAutomatonSimulationConfig();
            FileUtils.WriteObjectAsJsonToFile(simulationConfig, pathToFile);
        }

        private CellularAutomatonSimulationConfig CreateCellularAutomatonSimulationConfig()
        {
            // TODO add error handling 
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