using System;

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


        public bool PositionInDefulteValue()
        {
            return this.Row == 0 && this.Column == 0;
        }

        public bool IsTheSamePositionValue(Position i_ClickedPosition)
        {
            return this.Row == i_ClickedPosition.Row && this.Column == i_ClickedPosition.Column;
        }

        public static Position ConvertStringToPosition(string i_ExpectedMove)
        {
            Position expectedPosition = new Position();
            if (i_ExpectedMove != null)
            {
                char little_a = 'a', big_a = 'A';
                expectedPosition.Column = (uint)(i_ExpectedMove[0] - big_a);
                expectedPosition.Row = (uint)(i_ExpectedMove[1] - little_a);
            }
            return expectedPosition;
        }
    }
}
