namespace Logic
{
    using System;
    using System.Collections.Generic;

    public class Gameplay
    {
        private Player     m_FirstPlayer;
        private Player     m_SecondPlayer;
        private bool       m_IsSecondPlayerCPU;
        private LogicBoard m_LogicBoard;
        private List<Cell> m_MovedCells;

        public event Action<Position,Position,bool,bool,bool,Position> OnMove;

        public Gameplay(string i_FirstPlayerName, string i_SecondPlayerName, uint i_BoardSize, bool i_PlayingAgainstFriend)
        {
            m_FirstPlayer = new Player(i_FirstPlayerName, i_BoardSize);
            m_SecondPlayer = new Player(i_SecondPlayerName, i_BoardSize);
            m_IsSecondPlayerCPU = !i_PlayingAgainstFriend;
            m_LogicBoard = new LogicBoard(i_BoardSize);
            m_MovedCells = new List<Cell>();
        }

        public uint FirstPlayerScore
        {
            get { return m_FirstPlayer.CountWins; }
        }

        public uint SecondPlayerScore
        {
            get { return m_SecondPlayer.CountWins; }
        }

        public List<Cell> MovedCells
        {
            get { return m_MovedCells; }
            set { m_MovedCells = value; }
        }

        public void ReinitGame()
        {
            m_FirstPlayer.InitAmountSoldiersAndKings(m_LogicBoard.SizeOfBoard);
            m_SecondPlayer.InitAmountSoldiersAndKings(m_LogicBoard.SizeOfBoard);
            m_LogicBoard.InitBoard();
        }


        public void RunGame(Position i_CellMoveFrom, Position i_CellMoveTo,ref bool i_IsEat, bool i_IsFirstPlayerMove)
        {
            bool isCanEatAgain = false;
            string expectedMove = null;
            //need to fix this
            if (m_IsSecondPlayerCPU && !i_IsFirstPlayerMove)
            {
                CpuTurn(ref i_CellMoveFrom, ref i_CellMoveTo, ref i_IsEat, expectedMove);
            }
            else
            {
                 this.m_LogicBoard.CheckLegalMove(ref i_CellMoveFrom,ref i_CellMoveTo,i_IsFirstPlayerMove,ref i_IsEat);
            }

            bool IsEatingAvailable = m_LogicBoard.IsEatingAvailable(i_IsFirstPlayerMove);
            if (IsEatingAvailable)
            {
                if (i_IsEat)
                {
                    MakeEatOperation(i_CellMoveFrom, i_CellMoveTo, i_IsEat, i_IsFirstPlayerMove);
                }
                // check if he can eat again
                isCanEatAgain = m_LogicBoard.IsCanEatAgain(ref i_CellMoveTo, out expectedMove);
            }
            else
            {
                m_LogicBoard.MakeLegalMove(ref i_CellMoveFrom, ref i_CellMoveTo, i_IsEat, out bool becameAKing, m_MovedCells);
                ChangeKingStateIfBecomeAKing(becameAKing, i_IsFirstPlayerMove);
            }
            OnMove?.Invoke(i_CellMoveFrom, i_CellMoveTo, IsEatingAvailable,i_IsEat, isCanEatAgain,Position.ConvertStringToPosition(expectedMove));
        }

        private bool NoMoreMoves()
        {
            bool noMoreMoves = true;
            bool isFirstPlayerMoveCheckDummy = true;
            bool isFirstPlayerCanMoveOrEat = m_LogicBoard.IsEatingAvailable(isFirstPlayerMoveCheckDummy) || m_LogicBoard.IsMovingAvailable(isFirstPlayerMoveCheckDummy);
            bool isSecondPlayerCanMoveOrEat = m_LogicBoard.IsEatingAvailable(!isFirstPlayerMoveCheckDummy) || m_LogicBoard.IsMovingAvailable(!isFirstPlayerMoveCheckDummy);
            if (isFirstPlayerCanMoveOrEat && isSecondPlayerCanMoveOrEat)
            {
                noMoreMoves = false;
            }

            if (noMoreMoves)
            {
                uint sumFirstPlayerSoldiers = m_FirstPlayer.CalcSumOfSoldiers();
                uint sumSecondPlayerSoldiers = m_SecondPlayer.CalcSumOfSoldiers();

                if (sumFirstPlayerSoldiers > sumSecondPlayerSoldiers)
                {
                    m_FirstPlayer.WonTheGame();
                }
                else if (sumFirstPlayerSoldiers < sumSecondPlayerSoldiers)
                {
                    m_SecondPlayer.WonTheGame();
                }
                else
                {
                    /// Tie
                }
            }

            return noMoreMoves;
        }

        public void CpuTurn(ref Position io_CellMoveFrom, ref Position io_CellMoveTo, ref bool io_IsEat, string i_ExpectedMove)
        {
            List<string> eatingOptions = new List<string>();
            List<string> movingOptions = new List<string>();

            if (i_ExpectedMove == null)
            {
                m_LogicBoard.AddRelevantPath(eatingOptions, movingOptions, i_ExpectedMove);

                if (eatingOptions.Count != 0)
                {
                    // we have a eating option
                    ConvertTheSeletedString(ref io_CellMoveFrom, ref io_CellMoveTo, eatingOptions);
                    io_IsEat = true;
                }
                else if (movingOptions.Count != 0)
                {
                    ConvertTheSeletedString(ref io_CellMoveFrom, ref io_CellMoveTo, movingOptions);
                    io_IsEat = false;
                }
            }
            else
            {
                m_LogicBoard.CpuEat(ref io_CellMoveFrom, eatingOptions, i_ExpectedMove);
                ConvertTheSeletedString(ref io_CellMoveFrom, ref io_CellMoveTo, eatingOptions);
                io_IsEat = true;
            }

            eatingOptions.Clear();
            movingOptions.Clear();
        }

        private static void ConvertTheSeletedString(ref Position io_CellMoveFrom, ref Position io_CellMoveTo, List<string> i_SeletedList)
        {
            Random randIndex = new Random();
            int selectedIndex;
            selectedIndex = randIndex.Next(0, i_SeletedList.Count);
            LogicBoard.ConvertCurrentMoveToPositions(ref io_CellMoveFrom, ref io_CellMoveTo, i_SeletedList[selectedIndex]);
        }

        private void MakeEatOperation(Position i_CellMoveFrom, Position i_CellMoveTo, bool i_IsEat, bool i_IsFirstPlayerMove)
        {
            Position eatenPosition = new Position();
            eatenPosition.FindEatenPosition(i_CellMoveFrom, i_CellMoveTo);
            ChangeSoldiersState(ref eatenPosition);
            m_LogicBoard.MakeLegalMove(ref i_CellMoveFrom, ref i_CellMoveTo, i_IsEat, out bool becameAKing, m_MovedCells);

            ChangeKingStateIfBecomeAKing(becameAKing, i_IsFirstPlayerMove);
        }

        private void ChangeSoldiersState(ref Position i_EatenPosition)
        {
            m_LogicBoard.CheckEatenPosition(ref i_EatenPosition, out bool isEatenAKing, out CellState.eCellState o_EatenPositionBelonging);

            if (o_EatenPositionBelonging == CellState.eCellState.BelongToSecondPlayer)
            {
                m_SecondPlayer.DecreaseAmounts(isEatenAKing);
            }
            else if (o_EatenPositionBelonging == CellState.eCellState.BelongToFirstPlayer)
            {
                m_FirstPlayer.DecreaseAmounts(isEatenAKing);
            }
        }

        private void ChangeKingStateIfBecomeAKing(bool i_BecameAKing, bool i_IsFirstPlayerMove)
        {
            if (i_BecameAKing)
            {
                if (i_IsFirstPlayerMove)
                {
                    m_FirstPlayer.SoldierBecomeKing();
                }
                else
                {
                    m_SecondPlayer.SoldierBecomeKing();
                }
            }
        }

        public bool IsGameOver()
        {
            bool isGameOver = false;

            if (CheckIfEnemyLostAllSoldiers())
            {
                isGameOver = true;
            }
            else if (NoMoreMoves())
            {
                isGameOver = true;
            }

            return isGameOver;
        }

        private bool CheckIfEnemyLostAllSoldiers()
        {
            bool isEnemyLostAllSoldiers = false;

            if (m_FirstPlayer.AmountOfSoldiers == 0 && m_FirstPlayer.AmountOfKings == 0)
            {
                DeclareWinWhenPlayerLostHisSoldiers(ref isEnemyLostAllSoldiers, m_SecondPlayer);
            }
            else if (m_SecondPlayer.AmountOfSoldiers == 0 && m_SecondPlayer.AmountOfKings == 0)
            {
                DeclareWinWhenPlayerLostHisSoldiers(ref isEnemyLostAllSoldiers, m_FirstPlayer);
            }

            return isEnemyLostAllSoldiers;
        }

        private void DeclareWinWhenPlayerLostHisSoldiers(ref bool io_IsEnemyLostAllSoldiers, Player i_WinnerPlayer)
        {
            i_WinnerPlayer.WonTheGame();
            io_IsEnemyLostAllSoldiers = true;
        }

        public bool IsOwnTheCell(Position i_ClickedPosition,bool i_IsFirstPlayerMove)
        {
            CellState.eCellState clickedCellState = m_LogicBoard.Board[i_ClickedPosition.Row, i_ClickedPosition.Column].cellState;
            return LogicBoard.IsControllingTheCell(clickedCellState, i_IsFirstPlayerMove);
        }

        public bool IsLegalMove(ref Position io_CellMoveFrom, ref Position io_CellMoveTo, bool i_IsFirstPlayerMove, ref bool io_isEat)
        {
            return m_LogicBoard.CheckLegalMove(ref io_CellMoveFrom, ref io_CellMoveTo, i_IsFirstPlayerMove, ref io_isEat);
        }

        public int CalcDifferencesBetweenPlayersSoldiers()
        {
            return m_FirstPlayer.CalcSumOfSoldiers() - m_SecondPlayer.CalcSumOfSoldiers();
        }
    }
}
