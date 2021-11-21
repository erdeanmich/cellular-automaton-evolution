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
            DestroyExistingCells();

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    SpawnCell(x, y, cells[x, y]);
                }
            }
        }

        private void DestroyExistingCells()
        {
            if (transform.childCount != 0)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
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