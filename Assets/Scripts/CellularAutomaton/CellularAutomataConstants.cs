using System.Collections.Generic;
using UnityEngine;

namespace CellularAutomaton
{
    public class CellularAutomataConstants
    {
        public static Dictionary<CellType, Color> CELL_COLORS = new Dictionary<CellType, Color>
        {
            { CellType.Floor, Color.white },
            { CellType.Wall, Color.black },
            { CellType.Start, Color.green },
            { CellType.End, Color.red }
        };
    }
}