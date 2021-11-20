using System.Globalization;
using UnityEngine;

namespace CellularAutomaton
{
    public class CellularAutomatonSimulation : MonoBehaviour
    {
        [SerializeField]
        private int gridSize;

        [SerializeField]
        private int seed;

        [SerializeField]
        [Range(0,100)]
        private int floorRatio;

        [SerializeField]
        private CellularAutomatonVisualizer cellularAutomatonVisualizer;
        
        private int[,] cells; 
    
        void Start()
        {
            RetrieveSeed();
            InitializeCellGrid();
            cellularAutomatonVisualizer.VisualizeAutomaton(cells);
        }

        private void InitializeCellGrid()
        {
            cells = new int[gridSize, gridSize];
            Random.InitState(seed);
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    cells[x, y] = Random.Range(0, 100) <= floorRatio ? 0 : 1;
                }
            }
            
            Debug.Log("Cells initialized!");
            Debug.Log(cells);
        }

        private void RetrieveSeed()
        {
            if (seed == 0)
            {
                seed = Time.time.ToString(CultureInfo.CurrentCulture).GetHashCode();
                Debug.Log($"Current seed is {seed}");
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
