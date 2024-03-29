﻿namespace Logic
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
            createBaseBoard();
            fillBaseBoard();
        }

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
                if (checkValidDirection(IndicationForEating.eIndicationForEating.NotEating, ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove))
                {
                    // not eat
                    if (m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column].cellState == CellState.eCellState.Empty)
                    {
                        isLegalMove = true;
                    }

                }
                else if (checkValidDirection(IndicationForEating.eIndicationForEating.Eating, ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove))
                {
                    // eat
                    if (m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column].cellState == CellState.eCellState.Empty
                       && isControlledByEnemy(ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove))
                    {
                        isLegalMove = true;
                        o_isEat = true;
                    }
                }
            }

            return isLegalMove;
        }

        private bool checkValidDirection(IndicationForEating.eIndicationForEating i_IndicationForEating, ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_IsFirstPlayerMove)
        {
            bool validDirection;
            if (m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].IsKing)
            {
                checkValidDirectionToKing(i_IndicationForEating, ref io_CellMoveFrom, ref io_CellMoveTo, out validDirection);
            }
            else
            {
                checkValidDirectionToSoldier(i_IndicationForEating, ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove, out validDirection);
            }

            return validDirection;
        }

        private static void checkValidDirectionToKing(IndicationForEating.eIndicationForEating i_IndicationForEating, ref Position io_CellMoveFrom, ref Position io_CellMoveTo, out bool o_ValidDirection)
        {
            int indication = (int)i_IndicationForEating;

            if (isMoveLeftUpOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
            {
                o_ValidDirection = true;
            }
            else if (isMoveRightUpOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
            {
                o_ValidDirection = true;
            }
            else if (isMoveLeftDownOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
            {
                o_ValidDirection = true;
            }
            else if (isMoveRightDownOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
            {
                o_ValidDirection = true;
            }
            else
            {
                o_ValidDirection = false;
            }
        }

        private static void checkValidDirectionToSoldier(IndicationForEating.eIndicationForEating i_IndicationForEating, ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_IsFirstPlayerMove, out bool o_ValidDirection)
        {
            int indication = (int)i_IndicationForEating;
            if (i_IsFirstPlayerMove)
            {
                if (isMoveLeftUpOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
                {
                    o_ValidDirection = true;
                }
                else if (isMoveRightUpOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
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
                if (isMoveLeftDownOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
                {
                    o_ValidDirection = true;
                }
                else if (isMoveRightDownOnTheBoard(io_CellMoveFrom.Row, io_CellMoveTo.Row, io_CellMoveFrom.Column, io_CellMoveTo.Column, indication))
                {
                    o_ValidDirection = true;
                }
                else
                {
                    o_ValidDirection = false;
                }
            }
        }

        private static bool isMoveLeftUpOnTheBoard(uint i_RowCellFrom, uint i_RowCellTo, uint i_ColumnCellFrom, uint i_ColumnCellTo, int i_Indication)
        {
            return (i_RowCellFrom + (s_FirstPlayerAbiltyMove[0, 0] * i_Indication) == i_RowCellTo) &&
                   (i_ColumnCellFrom + (s_FirstPlayerAbiltyMove[0, 1] * i_Indication) == i_ColumnCellTo);
        }

        private static bool isMoveRightUpOnTheBoard(uint i_RowCellFrom, uint i_RowCellTo, uint i_ColumnCellFrom, uint i_ColumnCellTo, int i_Indication)
        {
            return (i_RowCellFrom + (s_FirstPlayerAbiltyMove[1, 0] * i_Indication) == i_RowCellTo) &&
                             (i_ColumnCellFrom + (s_FirstPlayerAbiltyMove[1, 1] * i_Indication) == i_ColumnCellTo);
        }

        private static bool isMoveLeftDownOnTheBoard(uint i_RowCellFrom, uint i_RowCellTo, uint i_ColumnCellFrom, uint i_ColumnCellTo, int i_Indication)
        {
            return (i_RowCellFrom + (s_SecondPlayerAbiltyMove[0, 0] * i_Indication) == i_RowCellTo) &&
                (i_ColumnCellFrom + (s_SecondPlayerAbiltyMove[0, 1] * i_Indication) == i_ColumnCellTo);
        }

        private static bool isMoveRightDownOnTheBoard(uint i_RowCellFrom, uint i_RowCellTo, uint i_ColumnCellFrom, uint i_ColumnCellTo, int i_Indication)
        {
            return (i_RowCellFrom + (s_SecondPlayerAbiltyMove[1, 0] * i_Indication) == i_RowCellTo) &&
                (i_ColumnCellFrom + (s_SecondPlayerAbiltyMove[1, 1] * i_Indication) == i_ColumnCellTo);
        }

        public void MakeLegalMove(ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_isEat, out bool o_BecameAKing, List <Cell> o_MovedCells)
        {
            bool isKingMoveNow = false;
            o_MovedCells.Clear();
            if (i_isEat)
            {
                Position eatenPosition = new Position();
                eatenPosition.FindEatenPosition(io_CellMoveFrom, io_CellMoveTo);
                m_Board[eatenPosition.Row, eatenPosition.Column].ChangeSourceCell();
                o_MovedCells.Add(m_Board[eatenPosition.Row, eatenPosition.Column]);
            }

            if (m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].IsKing == true)
            {
                isKingMoveNow = true;
            }

            m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column].ChangeDestinationCell(m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column]);
            m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].ChangeSourceCell();

            if (!isKingMoveNow)
            {
                makeKing(ref io_CellMoveTo, out o_BecameAKing);
            }
            else
            {
                o_BecameAKing = false;
            }

            o_MovedCells.Add(m_Board[io_CellMoveTo.Row, io_CellMoveTo.Column]);
            o_MovedCells.Add(m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column]);
        }

        private void makeKing(ref Position io_CellMoveTo, out bool o_BecameAKing)
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

        private bool isControlledByEnemy(ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_IsFirstPlayerMove)
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
            if (isLegalPosition(currentRow, currentColumn))
            {
                if (m_Board[currentRow, currentColumn].IsKing)
                {
                    if (isPlayerCanMove(currentRow, currentColumn, s_FirstPlayerAbiltyMove) || isPlayerCanMove(currentRow, currentColumn, s_SecondPlayerAbiltyMove))
                    {
                        canMove = true;
                    }
                }
                else
                {
                    // have 2 check for eche player
                    if (m_Board[currentRow, currentColumn].cellState == CellState.eCellState.BelongToFirstPlayer)
                    {
                        canMove = isPlayerCanMove(currentRow, currentColumn, s_FirstPlayerAbiltyMove);
                    }
                    else if (m_Board[i_CellToCheck.Row, i_CellToCheck.Column].cellState == CellState.eCellState.BelongToSecondPlayer)
                    {
                        canMove = isPlayerCanMove(currentRow, currentColumn, s_SecondPlayerAbiltyMove);
                    }
                }
            }

            return canMove;
        }

        private bool checkingPlayerOnHisMatrixMove(int i_CurrentRow, int i_CurrentColumn, int i_IndicationRowMatrix, int i_IndicationFirstColumnMatrix, int i_IndicationSecondColumnMatrix, int[,] i_ArrayPlayerAbiltyMove)
        {
            bool isPlayerCanMove = false;
            if (isLegalPosition(i_CurrentRow + i_ArrayPlayerAbiltyMove[i_IndicationRowMatrix, i_IndicationFirstColumnMatrix],
                i_CurrentColumn + i_ArrayPlayerAbiltyMove[i_IndicationRowMatrix, i_IndicationSecondColumnMatrix])
               && m_Board[i_CurrentRow + i_ArrayPlayerAbiltyMove[i_IndicationRowMatrix, i_IndicationFirstColumnMatrix],
               i_CurrentColumn + i_ArrayPlayerAbiltyMove[i_IndicationRowMatrix, i_IndicationSecondColumnMatrix]].cellState == CellState.eCellState.Empty)
            {
                isPlayerCanMove = true;
            }

            return isPlayerCanMove;
        }

        private bool isPlayerCanMove(int i_CurrentRow, int i_CurrentColumn, int[,] i_ArrayPlayerAbiltyMove)
        {
            bool isPlayerCanMove = false;
            if (checkingPlayerOnHisMatrixMove(i_CurrentRow, i_CurrentColumn, 0, 0, 1, i_ArrayPlayerAbiltyMove)
                || checkingPlayerOnHisMatrixMove(i_CurrentRow, i_CurrentColumn, 1, 0, 1, i_ArrayPlayerAbiltyMove))
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
            if (isLegalPosition(currentRow, currentColumn))
            {
                if (m_Board[currentRow, currentColumn].IsKing)
                {
                    if (m_Board[currentRow, currentColumn].cellState == CellState.eCellState.BelongToFirstPlayer)
                    {
                        if (isPlayerCanEat(currentRow, currentColumn, s_FirstPlayerAbiltyMove, CellState.eCellState.BelongToSecondPlayer)
                            || isPlayerCanEat(currentRow, currentColumn, s_SecondPlayerAbiltyMove, CellState.eCellState.BelongToSecondPlayer))
                        {
                            canEat = true;
                        }
                    }
                    else
                    {
                        if (isPlayerCanEat(currentRow, currentColumn, s_FirstPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer)
                               || isPlayerCanEat(currentRow, currentColumn, s_SecondPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer))
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
                        canEat = isPlayerCanEat(currentRow, currentColumn, s_FirstPlayerAbiltyMove, CellState.eCellState.BelongToSecondPlayer);
                    }
                    else if (m_Board[i_CellToCheck.Row, i_CellToCheck.Column].cellState == CellState.eCellState.BelongToSecondPlayer)
                    {
                        canEat = isPlayerCanEat(currentRow, currentColumn, s_SecondPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer);
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

        private bool isLegalPosition(int i_XPositio,int i_YPosition)
        {
            return i_XPositio >= 0 && i_XPositio < m_SizeOfBoard && i_YPosition >= 0 && i_YPosition < m_SizeOfBoard;
        }

        private bool isPlayerCanEat(int i_CurrentRow, int i_CurrentColumn, int[,] i_ArrayPlayerAbiltyMove, CellState.eCellState i_EnemyECellState)
        {
            bool canEat = false;

            // can eat in left up direction
            if (canEatLeftDirection(i_CurrentRow, i_CurrentColumn, i_ArrayPlayerAbiltyMove, i_EnemyECellState))
            {
                {
                    canEat = true;
                }
            }

            // can eat in right up direction
            if (canEatRightDirection(i_CurrentRow, i_CurrentColumn, i_ArrayPlayerAbiltyMove, i_EnemyECellState))
            {
                    canEat = true;
            }

            return canEat;
        }

        private bool canEatLeftDirection(int i_CurrentRow, int i_CurrentColumn, int[,] i_ArrayPlayerAbiltyMove, CellState.eCellState i_EnemyECellState)
        {
            return isLegalPosition(i_CurrentRow + i_ArrayPlayerAbiltyMove[0, 0], i_CurrentColumn + i_ArrayPlayerAbiltyMove[0, 1])
                 && m_Board[i_CurrentRow + i_ArrayPlayerAbiltyMove[0, 0], i_CurrentColumn + i_ArrayPlayerAbiltyMove[0, 1]].cellState == i_EnemyECellState
             && isLegalPosition(i_CurrentRow + (i_ArrayPlayerAbiltyMove[0, 0] * 2), i_CurrentColumn + (i_ArrayPlayerAbiltyMove[0, 1] * 2))
                && m_Board[i_CurrentRow + (i_ArrayPlayerAbiltyMove[0, 0] * 2), i_CurrentColumn + (i_ArrayPlayerAbiltyMove[0, 1] * 2)].cellState == CellState.eCellState.Empty;
        }

        private bool canEatRightDirection(int i_CurrentRow, int i_CurrentColumn, int[,] i_ArrayPlayerAbiltyMove, CellState.eCellState i_EnemyECellState)
        {
            return isLegalPosition(i_CurrentRow + i_ArrayPlayerAbiltyMove[1, 0], i_CurrentColumn + i_ArrayPlayerAbiltyMove[1, 1])
                && m_Board[i_CurrentRow + i_ArrayPlayerAbiltyMove[1, 0], i_CurrentColumn + i_ArrayPlayerAbiltyMove[1, 1]].cellState == i_EnemyECellState
                && isLegalPosition(i_CurrentRow + (i_ArrayPlayerAbiltyMove[1, 0] * 2), i_CurrentColumn + (i_ArrayPlayerAbiltyMove[1, 1] * 2))
                    && m_Board[i_CurrentRow + (i_ArrayPlayerAbiltyMove[1, 0] * 2), i_CurrentColumn + (i_ArrayPlayerAbiltyMove[1, 1] * 2)].cellState == CellState.eCellState.Empty;
        }

        public void CpuEat(ref Position io_CellMoveFrom, List<string> io_EatingOptions, string i_ExpectedMove)
        {
            if (i_ExpectedMove != null)
            {
                isLegalPosition(i_ExpectedMove[1] - 'a', i_ExpectedMove[0] - 'A');
                io_CellMoveFrom.Column = (uint)(i_ExpectedMove[0] - 'A');
                io_CellMoveFrom.Row = (uint)(i_ExpectedMove[1] - 'a');
            }

            if (m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].IsKing)
            {
                // king Eat move up

                // left eat
                // leagl position and position empty
                if (isLegalPosition((int)io_CellMoveFrom.Row + (s_FirstPlayerAbiltyMove[0,0]*2),(int)io_CellMoveFrom.Column+(s_FirstPlayerAbiltyMove[0,1]*2))
                    && canEatLeftDirection((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, s_FirstPlayerAbiltyMove,CellState.eCellState.BelongToFirstPlayer))
                {
                    io_EatingOptions.Add(createValidStringForCpuPath(ref io_CellMoveFrom,(int)(io_CellMoveFrom.Row + (s_FirstPlayerAbiltyMove[0, 0] * 2)),(int)(io_CellMoveFrom.Column + (s_FirstPlayerAbiltyMove[0, 1] * 2)))); 
                }

                // right eat
                if (isLegalPosition((int)io_CellMoveFrom.Row + (s_FirstPlayerAbiltyMove[1, 0] * 2), (int)io_CellMoveFrom.Column + (s_FirstPlayerAbiltyMove[1, 1] * 2))
                    && canEatRightDirection((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, s_FirstPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer))
                {
                    io_EatingOptions.Add(createValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + (s_FirstPlayerAbiltyMove[1, 0] * 2)), (int)(io_CellMoveFrom.Column + (s_FirstPlayerAbiltyMove[1, 1] * 2))));
                }
            }

            // player eat down
            //left
            if (isLegalPosition((int)io_CellMoveFrom.Row + (s_SecondPlayerAbiltyMove[0, 0] * 2), (int)io_CellMoveFrom.Column + (s_SecondPlayerAbiltyMove[0, 1] * 2))
                    && canEatLeftDirection((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, s_SecondPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer))
            {
                io_EatingOptions.Add(createValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + (s_SecondPlayerAbiltyMove[0, 0] * 2)), (int)(io_CellMoveFrom.Column + (s_SecondPlayerAbiltyMove[0, 1] * 2))));
            }

            //right
            if (isLegalPosition((int)io_CellMoveFrom.Row + (s_SecondPlayerAbiltyMove[1, 0] * 2), (int)io_CellMoveFrom.Column + (s_SecondPlayerAbiltyMove[1, 1] * 2))
                   && canEatRightDirection((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, s_SecondPlayerAbiltyMove, CellState.eCellState.BelongToFirstPlayer))
            {
                io_EatingOptions.Add(createValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + (s_SecondPlayerAbiltyMove[1, 0] * 2)), (int)(io_CellMoveFrom.Column + (s_SecondPlayerAbiltyMove[1, 1] * 2))));
            }
        }

        private void cpuMove(ref Position io_CellMoveFrom, List<string> io_MovingOptions)
        {
            if (m_Board[io_CellMoveFrom.Row, io_CellMoveFrom.Column].IsKing)
            {
                //left king
                if(checkingPlayerOnHisMatrixMove((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, 0, 0, 1, s_FirstPlayerAbiltyMove))
                {
                    io_MovingOptions.Add(createValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + s_FirstPlayerAbiltyMove[0, 0]), (int)(io_CellMoveFrom.Column + s_FirstPlayerAbiltyMove[0, 1])));
                }

                //right king
                if (checkingPlayerOnHisMatrixMove((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, 1, 0, 1, s_FirstPlayerAbiltyMove))
                {
                    io_MovingOptions.Add(createValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + s_FirstPlayerAbiltyMove[1, 0]), (int)(io_CellMoveFrom.Column + s_FirstPlayerAbiltyMove[1, 1])));
                }
            }

            //player move down
            //left
            if (checkingPlayerOnHisMatrixMove((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, 0, 0, 1, s_SecondPlayerAbiltyMove))
            {
                io_MovingOptions.Add(createValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + s_SecondPlayerAbiltyMove[0, 0]), (int)(io_CellMoveFrom.Column + s_SecondPlayerAbiltyMove[0, 1])));
            }

            //right
            if (checkingPlayerOnHisMatrixMove((int)io_CellMoveFrom.Row, (int)io_CellMoveFrom.Column, 1, 0, 1, s_SecondPlayerAbiltyMove))
            {
                io_MovingOptions.Add(createValidStringForCpuPath(ref io_CellMoveFrom, (int)(io_CellMoveFrom.Row + s_SecondPlayerAbiltyMove[1, 0]), (int)(io_CellMoveFrom.Column + s_SecondPlayerAbiltyMove[1, 1])));
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
                                cpuMove(ref currPositionToCheck, io_MovingOptions);
                            }
                        }
                    }
                }
            }
        }

        private static string createValidStringForCpuPath(ref Position i_CellMoveFrom, int i_CellMoveToRow, int i_CellMoveToCol)
        {
            char moveSign = '>';

            return ((char)(i_CellMoveFrom.Column + 'A')).ToString() + ((char)(i_CellMoveFrom.Row + 'a')).ToString()
                                + moveSign.ToString() + ((char)(i_CellMoveToCol + 'A')).ToString() + ((char)(i_CellMoveToRow + 'a')).ToString();
        }

        private void createBaseBoard()
        {
            for (int i = 0; i < m_SizeOfBoard; i++)
            {
                for (int j = 0; j < m_SizeOfBoard; j++)
                {
                    m_Board[i, j] = new Cell();
                }
            }
        }

        private void fillBaseBoard()
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
