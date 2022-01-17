using System;
using System.Collections.Generic;
using CellularAutomaton;

namespace FitnessEvaluation.FloodFill
{
    public class Cave : IComparable
    {
        private CellType type;
        private List<Tuple<int, int>> cells = new List<Tuple<int, int>>();

        public Cave(CellType type)
        {
            this.type = type;
        }
        
        public void AddCell(int x, int y)
        {
            cells.Add(new Tuple<int, int>(x, y));
        }

        public int GetSize()
        {
            return cells.Count;
        }

        public CellType GetType()
        {
            return type;
        }

        public int CompareTo(object obj)
        {
            return GetSize().CompareTo(((Cave) obj).GetSize());
        }
    }
}