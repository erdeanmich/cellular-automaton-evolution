using System;
using System.Collections.Generic;

namespace FitnessEvaluation.FloodFill
{
    public class Cave : IComparable
    {
        private List<Tuple<int, int>> cells = new List<Tuple<int, int>>();

        public void AddCell(int x, int y)
        {
            cells.Add(new Tuple<int, int>(x, y));
        }

        public int GetSize()
        {
            return cells.Count;
        }


        public int CompareTo(object obj)
        {
            return GetSize().CompareTo(((Cave) obj).GetSize());
        }
    }
}