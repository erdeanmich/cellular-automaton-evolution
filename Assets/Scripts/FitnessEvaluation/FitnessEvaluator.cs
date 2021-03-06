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

        public double DetermineFitness(int[,] cellsToInvestigate)
        {
            cells = cellsToInvestigate;
            List<Cave> caves = DetermineAllCaves();
            List<Cave> floorCaves = caves.FindAll(x => x.GetType() == CellType.Floor);
            floorCaves.Sort();
            List<Cave> wallCaves = caves.FindAll(x => x.GetType() == CellType.Wall && x.GetSize() > 50);
            wallCaves.Sort();
            int countOfWallIslands = wallCaves.Count;

            int sizeOfBiggestFloorCave = 0;
            if (floorCaves.Count > 0)
            {
                sizeOfBiggestFloorCave = floorCaves.Last().GetSize();
            }

            int sizeOfBiggestWallCave = 0;
            if (wallCaves.Count > 2)
            {
                sizeOfBiggestWallCave = wallCaves.Last().GetSize();
            }

            return (sizeOfBiggestFloorCave + sizeOfBiggestWallCave) * 0.1 * countOfWallIslands;
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
                    FloodCell(floodedCells, x, y, floodColor, cavesByFloodColor, cells[x, y]);
                    floodColor++;
                }
            }

            return cavesByFloodColor.Values.ToList();
        }

        private void FloodCell(int[,] floodedCells, int x, int y, int floodColor, Dictionary<int, Cave> cavesByFloodColor, int cellType)
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

            if (cells[x, y] != cellType)
            {
                // stop flooding, this is not the cellType we are currently flooding
                return;
            }

            floodedCells[x, y] = floodColor;
            if (!cavesByFloodColor.ContainsKey(floodColor))
            {
                cavesByFloodColor[floodColor] = new Cave((CellType)cellType);
            }
            
            cavesByFloodColor[floodColor].AddCell(x,y);
            
            FloodCell(floodedCells, x + 1, y, floodColor, cavesByFloodColor, cellType);
            FloodCell(floodedCells, x - 1, y, floodColor, cavesByFloodColor, cellType);
            FloodCell(floodedCells, x, y + 1, floodColor, cavesByFloodColor, cellType);
            FloodCell(floodedCells, x, y - 1, floodColor, cavesByFloodColor, cellType);
        }
    }
}