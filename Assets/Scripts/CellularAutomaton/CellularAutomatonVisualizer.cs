using System.Threading;
using UnityEngine;

namespace CellularAutomaton
{
    public class CellularAutomatonVisualizer : MonoBehaviour
    {
        [SerializeField]
        private GameObject cellPrefab;

        [SerializeField]
        private Transform gridParent;
        
        public void VisualizeAutomaton(int[,] cells)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for(int y = 0; y < cells.GetLength(1); y++)
                {
                    SpawnCell(x,y, cells[x,y]);
                }
            }
        }

        private void SpawnCell(int x, int y, int cellType)
        {
            GameObject cellObject = Instantiate(cellPrefab, gridParent);
            Cell cell = cellObject.GetComponent<Cell>();
            cell.SetPosition(x,y);
            CellType type = (CellType)cellType;
            cell.SetCellType(type);
        } 
    }
}