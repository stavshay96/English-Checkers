namespace Logic
{
    public class Position
    {
        private uint m_Row = 0;
        private uint m_Column = 0;

        public uint Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }

        public uint Column
        {
            get
            {
                return m_Column;
            }

            set
            {
                m_Column = value;
            }
        }

        public void ResetPosition()
        {
            this.m_Row = 0;
            this.m_Column = 0;
        }

        public void FindEatenPosition(Position i_CellMoveFrom, Position i_CellMoveTo)
        {
            this.Row = (i_CellMoveFrom.Row + i_CellMoveTo.Row) / 2;
            this.Column = (i_CellMoveFrom.Column + i_CellMoveTo.Column) / 2;
        }
    }
}
