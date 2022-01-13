
using System;
using System.Collections;
using CellularAutomatonEvolution;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace CellularAutomaton
{
    public class CellularAutomatonSimulation
    {
        private CellularAutomatonSimulationConfig cellularAutomatonSimulationConfig;
        private int[,] cells; 
        

        public int[,] GetCells()
        {
            return cells;
        }

        public void SetConfig(CellularAutomatonSimulationConfig cellularAutomatonSimulationConfig)
        {
            this.cellularAutomatonSimulationConfig = cellularAutomatonSimulationConfig;
        }

        public void Simulate(Action visualizeAction = null)
        {
            InitializeCellGrid();
            visualizeAction?.Invoke();
            for (int i = 0; i < cellularAutomatonSimulationConfig.N; i++)
            {
                UpdateAutomaton();
            }
            visualizeAction?.Invoke();
        }

        public IEnumerator SimulateSlow(Action visualizeAction)
        {
            InitializeCellGrid();
            visualizeAction.Invoke();
            for (int i = 0; i < cellularAutomatonSimulationConfig.N; i++)
            {
                UpdateAutomaton();
                yield return new WaitForSeconds(1);
                visualizeAction.Invoke();
            }
        }
        
        private void InitializeCellGrid()
        {
            int gridSize = cellularAutomatonSimulationConfig.GridSize;
            int seed = cellularAutomatonSimulationConfig.Seed;
            double r = cellularAutomatonSimulationConfig.R;

            cells = new int[gridSize, gridSize];
            Random.InitState(seed);
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    var random = Random.Range(0, 100);
                    cells[x, y] = random <= r ? 1 : 0;
                }
            }
        }
        
        private int GetWallCountOfNeighborhood(int x, int y)
        {
            int wallCount = 0;
            int mooreNeighborHoodSize = cellularAutomatonSimulationConfig.M;
            for (int xOffset = x - mooreNeighborHoodSize; xOffset <= x + mooreNeighborHoodSize; xOffset++)
            {
                if (ArrayUtils.IsIndexOutOfBounds(xOffset, cellularAutomatonSimulationConfig.GridSize))
                {
                    continue;
                }
                
                for (int yOffset = y - mooreNeighborHoodSize; yOffset <= y + mooreNeighborHoodSize; yOffset++)
                {
                    if (ArrayUtils.IsIndexOutOfBounds(yOffset, cellularAutomatonSimulationConfig.GridSize))
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
        
        public void UpdateAutomaton()
        {
            int gridSize = cellularAutomatonSimulationConfig.GridSize;
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
            return neighborhoodWallCount >= cellularAutomatonSimulationConfig.T ? CellType.Wall : CellType.Floor;
        }
    }
}