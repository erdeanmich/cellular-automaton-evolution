using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField]
        private Button manualCaButton;

        [SerializeField]
        private Button caEvolutionButton;

        private void Start()
        {
            manualCaButton.onClick.AddListener(OnClickManualCA);
            caEvolutionButton.onClick.AddListener(OnClickCAEvolution);
        }

        private void OnClickManualCA()
        {
            SceneManager.LoadScene("Scenes/CellularAutomaton");
        }

        private void OnClickCAEvolution()
        {
            SceneManager.LoadScene("Scenes/CellularAutomataEvolution");
        }
        
        private void OnDestroy()
        {
            manualCaButton.onClick.RemoveAllListeners();
            caEvolutionButton.onClick.RemoveAllListeners();
        }
    }
}