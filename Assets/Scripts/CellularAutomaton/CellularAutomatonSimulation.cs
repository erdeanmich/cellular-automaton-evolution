using System.Collections;
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
        private int initialWallChance;

        [SerializeField] 
        private int caIterations;

        [SerializeField] 
        private int wallNeighborhoodThreshhold;

        [SerializeField]
        private int mooreNeighborHoodSize;

        [SerializeField]
        private CellularAutomatonVisualizer cellularAutomatonVisualizer;
        
        private int[,] cells; 
    
        void Start()
        {
            StartSimulationSlow();
        }

        public void StartSimulation()
        {
            InitializeAutomaton();
            for (int i = 0; i < caIterations; i++)
            {
                UpdateAutomaton();
            }
            VisualizeAutomaton();
        }

        public void StartSimulationSlow()
        {
            InitializeAutomaton();
            StartCoroutine(SimulateAutomatonStepByStep());
        }

        private IEnumerator SimulateAutomatonStepByStep()
        {
            for (int i = 0; i < caIterations; i++)
            {
                UpdateAutomaton();
                VisualizeAutomaton();
                yield return new WaitForSeconds(1);
            }
        }

        private void InitializeAutomaton()
        {
            RetrieveSeed();
            InitializeCellGrid();
            VisualizeAutomaton();
        }

        private void VisualizeAutomaton()
        {
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
                    cells[x, y] = Random.Range(0, 100) <= initialWallChance ? 1 : 0;
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

        private void UpdateAutomaton()
        {
            int[,] nextCells = new int[gridSize,gridSize];
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    int wallCount = GetWallCountOfNeighborhood(x, y);
                    CellType cellType = GetNextCellState(wallCount);
                    nextCells[x, y] = (int) cellType;
                }
            }

            cells = nextCells;
        }

        private CellType GetNextCellState(int neighborhoodWallCount)
        {
            return neighborhoodWallCount >= wallNeighborhoodThreshhold ? CellType.Wall : CellType.Floor;
        }

        private int GetWallCountOfNeighborhood(int x, int y)
        {
            int wallCount = 0;
            for (int xOffset = x - mooreNeighborHoodSize; xOffset <= x + mooreNeighborHoodSize; xOffset++)
            {
                if (IsOffsetOutOfBounds(xOffset))
                {
                    continue;
                }
                
                for (int yOffset = y - mooreNeighborHoodSize; yOffset <= y + mooreNeighborHoodSize; yOffset++)
                {
                    if (IsOffsetOutOfBounds(yOffset))
                    {
                        continue;
                    }

                    if (cells[xOffset, yOffset] == (int) CellType.Wall)
                    {
                        wallCount++;
                    }
                }
            }

            return wallCount;
        }

        private bool IsOffsetOutOfBounds(int offset)
        {
            return offset < 0 || offset > gridSize - 1;
        }
    }
}
