namespace Logic
{
    using System;
    using System.Collections.Generic;

    public class LogicBoard
    {
        private static int[,] s_FirstPlayerAbiltyMove = { { 1, -1 }, { 1, 1 } };
        private static int[,] s_SecondPlayerAbiltyMove = { { -1, -1 }, { -1, 1 } };

        private Cell[,] m_Board;
        private uint    m_SizeOfBoard;

        public LogicBoard(uint i_BoardSize)
        {
            m_Board = new Cell[i_BoardSize, i_BoardSize];
            m_SizeOfBoard = i_BoardSize;
            InitBoard();
        }

        public Cell[,] Board
        {
            get
            {
                return m_Board;
            }

            set
            {
                m_Board = value;
            }
        }

        public uint SizeOfBoard
        {
            get
            {
                return m_SizeOfBoard;
            }

            set
            {
                m_SizeOfBoard = value;
            }
        }

        public void InitBoard()
        {
            CreateBaseBoard();
            FillBaseBoard();
        }

        //public void ReadMove(ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_IsFirstPlayerMove, ref bool o_isEat, out bool o_Quit, string i_ExpectedMove)
        //{
        //    string currentMove;
        //    bool isLegalMove;

        //   // TheWholeTestOfTheInput(out currentMove, ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove, out isLegalMove, ref o_isEat, out o_Quit, i_ExpectedMove);
        //    while (!isLegalMove)
        //    {
        //        //UI_Utils.PrintErrorMessage("legal move");
        //        //TheWholeTestOfTheInput(out currentMove, ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove, out isLegalMove, ref o_isEat, out o_Quit, i_ExpectedMove);
        //    }
        //}

        //private void TheWholeTestOfTheInput(out string io_CurrentMove, ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_IsFirstPlayerMove, out bool o_IsLegalMove, ref bool o_isEat, out bool io_Quit, string i_ExpectedMove)
        //{
        //    io_CellMoveFrom.ResetPosition();
        //    io_CellMoveTo.ResetPosition();


        //    //UI_Utils.GetCurrentMove(out io_CurrentMove, m_SizeOfBoard, out io_Quit, i_ExpectedMove);
        //    if (!io_Quit)
        //    {
        //        ConvertCurrentMoveToPositions(ref io_CellMoveFrom, ref io_CellMoveTo, io_CurrentMove);
        //        o_IsLegalMove = this.CheckLegalMove(ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove, ref o_isEat);
        //    }
        //    else
        //    {
        //        o_isEat = false;
        //        o_IsLegalMove = true;
        //    }
        //}

        public static void ConvertCurrentMoveToPositions(ref Position io_CellMoveFrom, ref Position io_CellMoveTo, string i_CurrentMove)
        {
            char little_a = 'a', big_a = 'A';
            io_CellMoveFrom.Column = (uint)(i_CurrentMove[0] - big_a);
            io_CellMoveFrom.Row = (uint)(i_CurrentMove[1] - little_a);
            io_CellMoveTo.Column = (uint)(i_CurrentMove[3] - big_a);
            io_CellMoveTo.Row = (uint)(i_CurrentMove[4] - little_a);
        }

        public bool CheckLegalMove(ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_IsFirstPlayerMove, ref bool o_isEat)
        {
            bool isLegalMove = false;
            o_isEat = false;
            // check from belong to the corret player
            // if cell is empty
            // if the move is eating enemy
            if (m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].IsActiveCell && m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column].IsActiveCell
                && IsControllingTheCell(m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].cellState, i_IsFirstPlayerMove))
            {
                if (CheckValidDirection(IndicationForEating.eIndicationForEating.NotEating, ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove))
                {
                    // not eat
                    if (m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column].cellState == CellState.eCellState.Empty)
                    {
                        isLegalMove = true;
                    }

                }
                else if (CheckValidDirection(IndicationForEating.eIndicationForEating.Eating, ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove))
                {
                    // eat
                    if (m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column].cellState == CellState.eCellState.Empty
                       && IsControlledByEnemy(ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove))
                    {
                        isLegalMove = true;
                        o_isEat = true;
                    }
                }
            }

            return isLegalMove;
        }

        private bool CheckValidDirection(IndicationForEating.eIndicationForEating i_IndicationForEating, ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_IsFirstPlayerMove)
        {
            bool validDirection;
            if (m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].IsKing)
            {
                CheckValidDirectionToKing(i_IndicationForEating, ref io_CellMoveFrom, ref io_CellMoveTo, out validDirection);
            }
            else
            {
                CheckValidDirectionToSoldier(i_IndicationForEating, ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove, out validDirection);
            }

            return validDirection;
        }

        private static void CheckValidDirectionToKing(IndicationForEating.eIndicationForEating i_IndicationForEating, ref Position io_CellMoveFrom, ref Position io_CellMoveTo, out bool o_ValidDirection)
        {
            int indication = (int)i_IndicationForEating;

            if (IsMoveLeftUpOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
            {
                o_ValidDirection = true;
            }
            else if (IsMoveRightUpOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
            {
                o_ValidDirection = true;
            }
            else if (IsMoveLeftDownOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
            {
                o_ValidDirection = true;
            }
            else if (IsMoveRightDownOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
            {
                o_ValidDirection = true;
            }
            else
            {
                o_ValidDirection = false;
            }
        }

        private static void CheckValidDirectionToSoldier(IndicationForEating.eIndicationForEating i_IndicationForEating, ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_IsFirstPlayerMove, out bool o_ValidDirection)
        {
            int indication = (int)i_IndicationForEating;
            if (i_IsFirstPlayerMove)
            {
                if (IsMoveLeftUpOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
                {
                    o_ValidDirection = true;
                }
                else if (IsMoveRightUpOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
                {
                    o_ValidDirection = true;
                }
                else
                {
                    o_ValidDirection = false;
                }
            }
            else
            {
                if (IsMoveLeftDownOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
                {
                    o_ValidDirection = true;
                }
                else if (IsMoveRightDownOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
                {
                    o_ValidDirection = true;
                }
                else
                {
                    o_ValidDirection = false;
                }
            }
        }

        private static bool IsMoveLeftUpOnTheBoard(uint i_RowCellFrom, uint i_RowCellTo, uint i_ColumnCellFrom, uint i_ColumnCellTo, int i_Indication)
        {
            return (i_RowCellFrom + (s_FirstPlayerAbiltyMove[0, 0] * i_Indication) == i_RowCellTo) &&
                   (i_ColumnCellFrom + (s_FirstPlayerAbiltyMove[0, 1] * i_Indication) == i_ColumnCellTo);
        }

        private static bool IsMoveRightUpOnTheBoard(uint i_RowCellFrom, uint i_RowCellTo, uint i_ColumnCellFrom, uint i_ColumnCellTo, int i_Indication)
        {
            return (i_RowCellFrom + (s_FirstPlayerAbiltyMove[1, 0] * i_Indication) == i_RowCellTo) &&
                             (i_ColumnCellFrom + (s_FirstPlayerAbiltyMove[1, 1] * i_Indication) == i_ColumnCellTo);
        }

        private static bool IsMoveLeftDownOnTheBoard(uint i_RowCellFrom, uint i_RowCellTo, uint i_ColumnCellFrom, uint i_ColumnCellTo, int i_Indication)
        {
            return (i_RowCellFrom + (s_SecondPlayerAbiltyMove[0, 0] * i_Indication) == i_RowCellTo) &&
                (i_ColumnCellFrom + (s_SecondPlayerAbiltyMove[0, 1] * i_Indication) == i_ColumnCellTo);
        }

        private static bool IsMoveRightDownOnTheBoard(uint i_RowCellFrom, uint i_RowCellTo, uint i_ColumnCellFrom, uint i_ColumnCellTo, int i_Indication)
        {
            return (i_RowCellFrom + (s_SecondPlayerAbiltyMove[1, 0] * i_Indication) == i_RowCellTo) &&
                (i_ColumnCellFrom + (s_SecondPlayerAbiltyMove[1, 1] * i_Indication) == i_ColumnCellTo);
        }

        public void MakeLegalMove(ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_isEat, out bool o_BecameAKing)
        {
            bool isKingMoveNow = false;
            if (i_isEat)
            {
                Position eatenPosition = new Position();
                eatenPosition.FindEatenPosition(io_CellMoveFrom, io_CellMoveTo);
                m_Board[eatenPosition.Row, eatenPosition.Column].ChangeSourceCell();
            }

            if (m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].IsKing == true)
            {
                isKingMoveNow = true;
            }

            m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column].ChangeDestinationCell(m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column]);
            m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].ChangeSourceCell();

            if (!isKingMoveNow)
            {
                MakeKing(ref io_CellMoveTo, out o_BecameAKing);
            }
            else
            {
                o_BecameAKing = false;
            }
        }

        private void MakeKing(ref Position io_CellMoveTo, out bool o_BecameAKing)
        {
            o_BecameAKing = false;
            if (m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column].cellState == CellState.eCellState.BelongToFirstPlayer
                && io_CellMoveTo.Row == (m_SizeOfBoard - 1))
            {
                m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column].IsKing = true;
                o_BecameAKing = true;
            }
            else if (m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column].cellState == CellState.eCellState.BelongToSecondPlayer
                     && (io_CellMoveTo.Row == 0))
            {
                m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column].IsKing = true;
                o_BecameAKing = true;
            }
        }

        public static bool IsControllingTheCell(CellState.eCellState i_CellStateFrom, bool i_IsFirstPlayerMove)
        {
            bool isControlling;
            if (i_IsFirstPlayerMove && i_CellStateFrom == CellState.eCellState.BelongToFirstPlayer)
            {
                isControlling = true;
            }
            else if (!i_IsFirstPlayerMove && i_CellStateFrom == CellState.eCellState.BelongToSecondPlayer)
            {
                isControlling = true;
            }
            else
            {
                isControlling = false;
            }

            return isControlling;
        }

        private bool IsControlledByEnemy(ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_IsFirstPlayerMove)
        {
            //check if enemy conrtol the middle cell between form to.
            bool isControlled = false;
            Position eatenPosition = new Position();
            eatenPosition.FindEatenPosition(io_CellMoveFrom, io_CellMoveTo);
            if (m_Board[eatenPosition.Row, eatenPosition.Column].cellState == CellState.eCellState.BelongToFirstPlayer && !i_IsFirstPlayerMove)
            {
                isControlled = true;
            }
            else if (m_Board[eatenPosition.Row, eatenPosition.Column].cellState == CellState.eCellState.BelongToSecondPlayer && i_IsFirstPlayerMove)
            {
                isControlled = true;
            }

            return isControlled;
        }

        public void CheckEatenPosition(ref Position i_EatenPosition, out bool o_IsEatenAKing, out CellState.eCellState o_EatenPositionBelonging)
        {
            o_IsEatenAKing = false;
            if (m_Board[i_EatenPosition.Row, i_EatenPosition.Column].IsKing)
            {
                o_IsEatenAKing = true;
            }

            o_EatenPositionBelonging = m_Board[i_EatenPosition.Row, i_EatenPosition.Column].cellState;
        }

        public bool IsMovingAvailable(bool i_IsFirstPlayerMove)
        {
            bool isMovingAvailable = false;
            Position currPositionToCheck = new Position();

            for (int i = 0; i < m_SizeOfBoard; ++i)
            {
                for (int j = 0; j < m_SizeOfBoard; ++j)
                {
                    if (m_Board[i, j].IsActiveCell)
                    {
                        if (IsControllingTheCell(m_Board[i, j].cellState, i_IsFirstPlayerMove))
                        {
                            currPositionToCheck.Row = (uint)i;
                            currPositionToCheck.Column = (uint)j;
                            if (IsCanMove(ref currPositionToCheck))
                            {
                                isMovingAvailable = true;
                                break;
                            }
                        }
                    }
                }

                if (isMovingAvailable)
                {
                    break;
                }
            }

            return isMovingAvailable;
        }

        public bool IsCanMove(ref Position i_CellToCheck)
        {
            bool canMove = false;
            int currentRow = (int)i_CellToCheck.Row, currentColumn = (int)i_CellToCheck.Column;
            if (IsLegalPosition(currentRow, currentColumn))
            {
                if (m_Board[currentRow, currentColumn].IsKing)
                {
                    if (IsPlayerCanMove(currentRow, currentColumn, s_FirstPlayerAbiltyMove) || IsPlayerCanMove(currentRow, currentColumn, s_SecondPlayerAbiltyMove))
                    {
                        canMove = true;
                    }
                }
                else
                {
                    // have 2 check for eche player
                    if (m_Board[currentRow, currentColumn].cellState == CellState.eCellState.BelongToFirstPlayer)
                    {
                        canMove = IsPlayerCanMove(currentRow, currentColumn, s_FirstPlayerAbiltyMove);
                    }
                    else if (m_Board[i_CellToCheck.Row, i_CellToCheck.Column].cellState == CellState.eCellState.BelongToSecondPlayer)
                    {
                        canMove = IsPlayerCanMove(currentRow, currentColumn, s_SecondPlayerAbiltyMove);
                    }
                }
            }

            return canMove;
        }

        private bool CheckingPlayerOnHisMatrixMove(int i_CurrentRow, int i_CurrentColumn, int i_IndicationRowMatrix, int i_IndicationFirstColumnMatrix, int i_IndicationSecondColumnMatrix, int[,] i_ArrayPlayerAbiltyMove)
        {
            bool isPlayerCanMove = false;
            if (IsLegalPosition(i_CurrentRow + i_ArrayPlayerAbiltyMove[i_IndicationRowMatrix, i_IndicationFirstColumnMatrix],
                i_CurrentColumn + i_ArrayPlayerAbiltyMove[i_IndicationRowMatrix, i_IndicationSecondColumnMatrix])
               && m_Board[i_CurrentRow + i_ArrayPlayerAbiltyMove[i_IndicationRowMatrix, i_IndicationFirstColumnMatrix],
               i_CurrentColumn + i_ArrayPlayerAbiltyMove[i_IndicationRowMatrix, i_IndicationSecondColumnMatrix]].cellState == CellState.eCellState.Empty)
            {
                isPlayerCanMove = true;
            }

            return isPlayerCanMove;
        }

        private bool IsPlayerCanMove(int i_CurrentRow, int i_CurrentColumn, int[,] i_ArrayPlayerAbiltyMove)
        {
            bool isPlayerCanMove = false;
            if (CheckingPlayerOnHisMatrixMove(i_CurrentRow, i_CurrentColumn, 0, 0, 1, i_ArrayPlayerAbiltyMove)
                || CheckingPlayerOnHisMatrixMove(i_CurrentRow, i_CurrentColumn, 1, 0, 1, i_ArrayPlayerAbiltyMove))
            {
                isPlayerCanMove = true;
            }

            return isPlayerCanMove;
        }

        //It is known that we have a code duplication with the function IsMovingAvailable.
        //We have not yet learned how to send methods as a parameter.
        public bool IsEatingAvailable(bool i_IsFirstPlayerMove)
        {
            bool isEatingAvailable = false;
            Position currPositionToCheck = new Position();

            for (int i = 0; i < m_SizeOfBoard; ++i)
            {
                for (int j = 0; j < m_SizeOfBoard; ++j)
                {
                    if (m_Board[i, j].IsActiveCell)
                    {
                        if (IsControllingTheCell(m_Board[i, j].cellState, i_IsFirstPlayerMove))
                        {
                            currPositionToCheck.Row = (uint)i;
                            currPositionToCheck.Column = (uint)j;
                            if (IsCanEat(ref currPositionToCheck))
                            {
                                isEatingAvailable = true;
                                break;
                            }
                        }
                    }
                }

                if (isEatingAvailable)
                {
                    break;
                }
            }

            return isEatingAvailable;
        }

        public bool IsCanEat(ref Position i_CellToCheck)
        {
            bool canEat = false;
            int currentRow = (int)i_CellToCheck.Row, currentColumn = (int)i_CellToCheck.Column;
            if (IsLegalPosition(currentRow, currentColumn))
            {
                if (m_Board[currentRow, currentColumn].IsKing)
                {
                    if (m_Board[currentRow, currentColumn].cellState == CellState.eCellState.BelongToFirstPlayer)
                    {
                        if (IsPlayerCanEat(currentRow, currentColumn, s_FirstPlayerAbiltyMove, CellState.eCellState.BelongToSecondPlayer)
                            || IsPlayerCanEat(currentRow, currentColumn, s_SecondPlayerAbiltyMove, CellState.eCellState.BelongToSecondPlayer))
                        {
                            canEat = true;
                        }
                    }
                    else
                    {
                        if (IsPlayerCanEat(currentRow, currentColumn, s_FirstPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer)
                               || IsPlayerCanEat(currentRow, currentColumn, s_SecondPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer))
                        {
                            canEat = true;
                        }
                    }
                }
                else
                {
                    // have 2 check for eche player
                    if (m_Board[currentRow, currentColumn].cellState == CellState.eCellState.BelongToFirstPlayer)
                    {
                        canEat = IsPlayerCanEat(currentRow, currentColumn, s_FirstPlayerAbiltyMove, CellState.eCellState.BelongToSecondPlayer);
                    }
                    else if (m_Board[i_CellToCheck.Row, i_CellToCheck.Column].cellState == CellState.eCellState.BelongToSecondPlayer)
                    {
                        canEat = IsPlayerCanEat(currentRow, currentColumn, s_SecondPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer);
                    }
                }
            }

            return canEat;
        }

        public bool IsCanEatAgain(ref Position i_CellToCheck, out string o_ExpectedMove)
        {
            bool canEatAgain = false;
            o_ExpectedMove = null;
            char row, col;
            if (IsCanEat(ref i_CellToCheck))
            {
                row = (char)(i_CellToCheck.Row + 'a');
                col = (char)(i_CellToCheck.Column + 'A');
                o_ExpectedMove = col.ToString() + row.ToString();
                canEatAgain = true;
            }

            return canEatAgain;
        }

        private bool IsLegalPosition(int i_XPositio,int i_YPosition)
        {
            return i_XPositio >= 0 && i_XPositio < m_SizeOfBoard && i_YPosition >= 0 && i_YPosition < m_SizeOfBoard;
        }

        private bool IsPlayerCanEat(int i_CurrentRow, int i_CurrentColumn, int[,] i_ArrayPlayerAbiltyMove, CellState.eCellState i_EnemyECellState)
        {
            bool canEat = false;

            // can eat in left up direction
            if (CanEatLeftDirection(i_CurrentRow, i_CurrentColumn, i_ArrayPlayerAbiltyMove, i_EnemyECellState))
            {
                {
                    canEat = true;
                }
            }

            // can eat in right up direction
            if (CanEatRightDirection(i_CurrentRow, i_CurrentColumn, i_ArrayPlayerAbiltyMove, i_EnemyECellState))
            {
                    canEat = true;
            }

            return canEat;
        }

        private bool CanEatLeftDirection(int i_CurrentRow, int i_CurrentColumn, int[,] i_ArrayPlayerAbiltyMove, CellState.eCellState i_EnemyECellState)
        {
            return IsLegalPosition(i_CurrentRow + i_ArrayPlayerAbiltyMove[0, 0], i_CurrentColumn + i_ArrayPlayerAbiltyMove[0, 1])
                 && m_Board[i_CurrentRow + i_ArrayPlayerAbiltyMove[0, 0], i_CurrentColumn + i_ArrayPlayerAbiltyMove[0, 1]].cellState == i_EnemyECellState
             && IsLegalPosition(i_CurrentRow + (i_ArrayPlayerAbiltyMove[0, 0] * 2), i_CurrentColumn + (i_ArrayPlayerAbiltyMove[0, 1] * 2))
                && m_Board[i_CurrentRow + (i_ArrayPlayerAbiltyMove[0, 0] * 2), i_CurrentColumn + (i_ArrayPlayerAbiltyMove[0, 1] * 2)].cellState == CellState.eCellState.Empty;
        }

        private bool CanEatRightDirection(int i_CurrentRow, int i_CurrentColumn, int[,] i_ArrayPlayerAbiltyMove, CellState.eCellState i_EnemyECellState)
        {
            return IsLegalPosition(i_CurrentRow + i_ArrayPlayerAbiltyMove[1, 0], i_CurrentColumn + i_ArrayPlayerAbiltyMove[1, 1])
                && m_Board[i_CurrentRow + i_ArrayPlayerAbiltyMove[1, 0], i_CurrentColumn + i_ArrayPlayerAbiltyMove[1, 1]].cellState == i_EnemyECellState
                && IsLegalPosition(i_CurrentRow + (i_ArrayPlayerAbiltyMove[1, 0] * 2), i_CurrentColumn + (i_ArrayPlayerAbiltyMove[1, 1] * 2))
                    && m_Board[i_CurrentRow + (i_ArrayPlayerAbiltyMove[1, 0] * 2), i_CurrentColumn + (i_ArrayPlayerAbiltyMove[1, 1] * 2)].cellState == CellState.eCellState.Empty;
        }

        public void CpuEat(ref Position io_CellMoveFrom, List<string> io_EatingOptions, string i_ExpectedMove)
        {
            if (i_ExpectedMove != null)
            {
                IsLegalPosition(i_ExpectedMove[1] - 'a', i_ExpectedMove[0] - 'A');
                io_CellMoveFrom.Column = (uint)(i_ExpectedMove[0] - 'A');
                io_CellMoveFrom.Row = (uint)(i_ExpectedMove[1] - 'a');
            }

            if (m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].IsKing)
            {
                // king Eat move up

                // left eat
                // leagl position and position empty
                if (IsLegalPosition((int)io_CellMoveFrom.Row + (s_FirstPlayerAbiltyMove[0,0]*2),(int)io_CellMoveFrom.Column+(s_FirstPlayerAbiltyMove[0,1]*2))
                    && CanEatLeftDirection((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, s_FirstPlayerAbiltyMove,CellState.eCellState.BelongToFirstPlayer))
                {
                    io_EatingOptions.Add(CreateValidStringForCpuPath(ref io_CellMoveFrom,(int)(io_CellMoveFrom.Row + (s_FirstPlayerAbiltyMove[0, 0] * 2)),(int)(io_CellMoveFrom.Column + (s_FirstPlayerAbiltyMove[0, 1] * 2)))); 
                }

                // right eat
                if (IsLegalPosition((int)io_CellMoveFrom.Row + (s_FirstPlayerAbiltyMove[1, 0] * 2), (int)io_CellMoveFrom.Column + (s_FirstPlayerAbiltyMove[1, 1] * 2))
                    && CanEatRightDirection((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, s_FirstPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer))
                {
                    io_EatingOptions.Add(CreateValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + (s_FirstPlayerAbiltyMove[1, 0] * 2)), (int)(io_CellMoveFrom.Column + (s_FirstPlayerAbiltyMove[1, 1] * 2))));
                }
            }

            // player eat down
            //left
            if (IsLegalPosition((int)io_CellMoveFrom.Row + (s_SecondPlayerAbiltyMove[0, 0] * 2), (int)io_CellMoveFrom.Column + (s_SecondPlayerAbiltyMove[0, 1] * 2))
                    && CanEatLeftDirection((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, s_SecondPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer))
            {
                io_EatingOptions.Add(CreateValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + (s_SecondPlayerAbiltyMove[0, 0] * 2)), (int)(io_CellMoveFrom.Column + (s_SecondPlayerAbiltyMove[0, 1] * 2))));
            }

            //right
            if (IsLegalPosition((int)io_CellMoveFrom.Row + (s_SecondPlayerAbiltyMove[1, 0] * 2), (int)io_CellMoveFrom.Column + (s_SecondPlayerAbiltyMove[1, 1] * 2))
                   && CanEatRightDirection((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, s_SecondPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer))
            {
                io_EatingOptions.Add(CreateValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + (s_SecondPlayerAbiltyMove[1, 0] * 2)), (int)(io_CellMoveFrom.Column + (s_SecondPlayerAbiltyMove[1, 1] * 2))));
            }
        }

        private void CpuMove(ref Position io_CellMoveFrom, List<string> io_MovingOptions)
        {
            if (m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].IsKing)
            {
                //left king
                if(CheckingPlayerOnHisMatrixMove((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, 0, 0, 1, s_FirstPlayerAbiltyMove))
                {
                    io_MovingOptions.Add(CreateValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + s_FirstPlayerAbiltyMove[0, 0]), (int)(io_CellMoveFrom.Column + s_FirstPlayerAbiltyMove[0, 1])));
                }

                //right king
                if (CheckingPlayerOnHisMatrixMove((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, 1, 0, 1, s_FirstPlayerAbiltyMove))
                {
                    io_MovingOptions.Add(CreateValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + s_FirstPlayerAbiltyMove[1, 0]), (int)(io_CellMoveFrom.Column + s_FirstPlayerAbiltyMove[1, 1])));
                }
            }

            //player move down
            //left
            if (CheckingPlayerOnHisMatrixMove((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, 0, 0, 1, s_SecondPlayerAbiltyMove))
            {
                io_MovingOptions.Add(CreateValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + s_SecondPlayerAbiltyMove[0, 0]), (int)(io_CellMoveFrom.Column + s_SecondPlayerAbiltyMove[0, 1])));
            }

            //right
            if (CheckingPlayerOnHisMatrixMove((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, 1, 0, 1, s_SecondPlayerAbiltyMove))
            {
                io_MovingOptions.Add(CreateValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + s_SecondPlayerAbiltyMove[1, 0]), (int)(io_CellMoveFrom.Column + s_SecondPlayerAbiltyMove[1, 1])));
            }
        }

        public void AddRelevantPath(List<string> io_EatingOptions, List<string> io_MovingOptions, string i_ExpectedMove)
        {
            bool isFirstPlayerMove = false; // the method work only for CPU( he is the second player ).
            Position currPositionToCheck = new Position();

            for (int i = 0; i < m_SizeOfBoard; ++i)
            {
                for (int j = 0; j < m_SizeOfBoard; ++j)
                {
                    if (m_Board[i, j].IsActiveCell)
                    {
                        if (IsControllingTheCell(m_Board[i, j].cellState, isFirstPlayerMove))
                        {
                            currPositionToCheck.Row = (uint)i;
                            currPositionToCheck.Column = (uint)j;
                            if (IsCanEat(ref currPositionToCheck))
                            {
                                CpuEat(ref currPositionToCheck, io_EatingOptions, i_ExpectedMove);
                            }
                            else if (IsCanMove(ref currPositionToCheck))
                            {
                                CpuMove(ref currPositionToCheck, io_MovingOptions);
                            }
                        }
                    }
                }
            }
        }

        private static string CreateValidStringForCpuPath(ref Position i_CellMoveFrom, int i_CellMoveToRow, int i_CellMoveToCol)
        {
            char moveSign = '>';

            return ((char)(i_CellMoveFrom.Column + 'A')).ToString() + ((char)(i_CellMoveFrom.Row + 'a')).ToString()
                                + moveSign.ToString() + ((char)(i_CellMoveToCol + 'A')).ToString() + ((char)(i_CellMoveToRow + 'a')).ToString();
        }

        private void CreateBaseBoard()
        {
            for (int i = 0; i < m_SizeOfBoard; i++)
            {
                for (int j = 0; j < m_SizeOfBoard; j++)
                {
                    m_Board[i, j] = new Cell();
                }
            }
        }

        private void FillBaseBoard()
        {
            for (int i = 0; i < m_SizeOfBoard; i++)
            {
                for (int j = 0; j < m_SizeOfBoard; j++)
                {

                    if ((i + j) % 2 != 0)
                    {
                        if (i < m_SizeOfBoard / 2 - 1)
                        {
                            m_Board[i, j].cellState = CellState.eCellState.BelongToFirstPlayer;
                        }

                        if (m_SizeOfBoard / 2 < i && i < m_SizeOfBoard)
                        {
                            m_Board[i, j].cellState = CellState.eCellState.BelongToSecondPlayer;
                        }

                        if (m_SizeOfBoard / 2 - 1 <= i && i < m_SizeOfBoard / 2 + 1)
                        {
                            m_Board[i, j].cellState = CellState.eCellState.Empty;
                        }

                        m_Board[i, j].IsActiveCell = true;
                    }
                    else
                    {
                        m_Board[i, j].IsActiveCell = false;
                    }
                }
            }
        }
    }
}
