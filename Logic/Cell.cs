namespace Logic
{
    public class Cell
    {
        private bool                 m_IsActiveCell = false;
        private CellState.eCellState m_CellState = CellState.eCellState.Empty;
        private bool                 m_IsKing = false;

        public bool IsActiveCell
        {
            get
            {
                return m_IsActiveCell;
            }

            set
            {
                m_IsActiveCell = value;
            }
        }

        public CellState.eCellState cellState 
        {
            get
            {
                return m_CellState;
            }

            set
            {
                m_CellState = value;
            }
        }

        public bool IsKing
        {
            get
            {
                return m_IsKing;
            }

            set
            {
                m_IsKing = value;
            }
        }

        public void ChangeSourceCell()
        {
            this.cellState = CellState.eCellState.Empty;
            this.IsKing = false;
        }

        public void ChangeDestinationCell(Cell i_SourceCell)
        {
            this.cellState = i_SourceCell.cellState;
            this.IsKing = i_SourceCell.IsKing;
        }
    }
}
