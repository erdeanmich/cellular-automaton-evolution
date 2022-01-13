using System.Collections.Generic;
using System.Linq;
using CellularAutomaton;
using CellularAutomatonEvolution;
using FitnessEvaluation.FloodFill;
using Utility;

namespace FitnessEvaluation
{
    public class FitnessEvaluator : IFitnessEvaluator
    {
        private int[,] cells;

        public int DetermineFitness(int[,] cellsToInvestigate)
        {
            cells = cellsToInvestigate;

            // amount of rocks - the more walls the better
            int wallPoints = DetermineAmountOfWalls();
            int total = cellsToInvestigate.GetLength(0) * cellsToInvestigate.GetLength(0);
            float percentage =  wallPoints / (float) total;
            List<Cave> caves = DetermineAllCaves();
            int sizeOfBiggestCave;
            if (caves.Count == 0)
            {
                sizeOfBiggestCave = 0;
            }
            else
            {
                sizeOfBiggestCave = caves.Last()?.GetSize() ?? 0;
            }
   
            return (int) (percentage * sizeOfBiggestCave);
        }

        private int DetermineAmountOfWalls()
        {
            return cells.Cast<int>().Count(cell => cell == (int)CellType.Wall);
        }
        
        private List<Cave> DetermineAllCaves()
        {
            // have a floodarray
            int[,] floodedCells = new int[cells.GetLength(0), cells.GetLength(1)];
            int floodColor = 1;
            var cavesByFloodColor = new Dictionary<int, Cave>();

            // loop through all cells 
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    if (cells[x, y] == (int)CellType.Floor)
                    {
                        FloodCell(floodedCells, x, y, floodColor, cavesByFloodColor);
                        floodColor++;
                    }
                }
            }

            return cavesByFloodColor.Values.ToList();
        }

        private void FloodCell(int[,] floodedCells, int x, int y, int floodColor, Dictionary<int, Cave> cavesByFloodColor)
        {
            int cellLength = floodedCells.GetLength(0);
            if (ArrayUtils.IsIndexOutOfBounds(x, cellLength) || ArrayUtils.IsIndexOutOfBounds(y, cellLength))
            {
                // no valid cell index
                return;
            }
            
            if (floodedCells[x, y] != 0)
            {
                // cell was already visited
                return;
            }

            if (cells[x, y] == (int)CellType.Wall)
            {
                // stop flooding, this is a wall
                return;
            }

            floodedCells[x, y] = floodColor;
            if (!cavesByFloodColor.ContainsKey(floodColor))
            {
                cavesByFloodColor[floodColor] = new Cave();
            }
            
            cavesByFloodColor[floodColor].AddCell(x,y);
            
            FloodCell(floodedCells, x + 1, y, floodColor, cavesByFloodColor);
            FloodCell(floodedCells, x - 1, y, floodColor, cavesByFloodColor);
            FloodCell(floodedCells, x, y + 1, floodColor, cavesByFloodColor);
            FloodCell(floodedCells, x, y - 1, floodColor, cavesByFloodColor);
        }
        
        
    }
}