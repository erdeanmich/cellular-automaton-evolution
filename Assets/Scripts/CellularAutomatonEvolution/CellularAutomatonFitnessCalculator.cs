using System.Linq;
using CellularAutomaton;

namespace CellularAutomatonEvolution
{
    public class CellularAutomatonFitnessCalculator : IFitnessEvaluator
    {
        public int DetermineFitness(int[,] cells)
        {
            // amount of rocks - the more walls the better
            int wallPoints = DetermineAmountOfWalls(cells);
            int sizeOfBiggestCave;
            int minusCavesThatAreUnreachableFromBiggestOne;
            
            // place start and end tile on floor tile - the longer the way the better 
            return 1;
        }

        private int DetermineAmountOfWalls(int[,] cells)
        {
            return cells.Cast<int>().Count(cell => cell == (int)CellType.Wall);
        }

        private void DetermineAllCaves()
        {
            // have a floodarray
            // loop through all cells 
            // 
        }
}