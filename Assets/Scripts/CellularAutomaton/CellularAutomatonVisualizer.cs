using UnityEngine;
using UnityEngine.UIElements;

namespace CellularAutomaton
{
    public class CellularAutomatonVisualizer : MonoBehaviour
    {
        [SerializeField] 
        private GameObject cellPrefab;

        [SerializeField]
        private RectTransform gridParent;

        [SerializeField] 
        private RectTransform targetFrame;
        
        public void VisualizeAutomaton(int[,] cells)
        {
            DestroyExistingCells();
            ScaleGridToTargetFrame(cells);

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    SpawnCell(x, y, cells[x, y]);
                }
            }
        }

        private void ScaleGridToTargetFrame(int[,] cells)
        {
            var gridLength = cells.GetLength(0);
            var rect = targetFrame.rect;
            var sizeDelta = targetFrame.sizeDelta;
            
            gridParent.position = new Vector3(rect.xMin, rect.yMin);
            gridParent.sizeDelta = new Vector2(gridLength, gridLength);
            var scaleRatioX = sizeDelta.x / gridLength;
            var scaleRatioY = sizeDelta.y / gridLength;
            gridParent.localScale = new Vector3(scaleRatioX, scaleRatioY, 1);
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