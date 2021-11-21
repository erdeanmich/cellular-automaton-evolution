using UnityEngine;

namespace CellularAutomaton
{
    public class Cell : MonoBehaviour
    {
        [SerializeField]
        public CellType cellType;

        [SerializeField] 
        public SpriteRenderer spriteRenderer;

        public void SetPosition(int x, int y)
        {
            transform.localPosition = new Vector3(x, y);
        }

        public void SetCellType(CellType type)
        {
            cellType = type;
            spriteRenderer.color = CellularAutomataConstants.CELL_COLORS[cellType];
        }
    }
}