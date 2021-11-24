using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField]
        private Button manualCAButton;

        [SerializeField]
        private Button caEvolutionButton;

        private void Start()
        {
            manualCAButton.onClick.AddListener(OnClickManualCA);
            caEvolutionButton.onClick.AddListener(OnClickCAEvolution);
        }

        private void OnClickManualCA()
        {
            SceneManager.LoadScene("Scenes/CellularAutomaton");
        }

        private void OnClickCAEvolution()
        {
            //TODO create scene for CA
        }
        
        private void OnDestroy()
        {
            manualCAButton.onClick.RemoveAllListeners();
            caEvolutionButton.onClick.RemoveAllListeners();
        }
    }
}