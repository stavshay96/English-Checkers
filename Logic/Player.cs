namespace Logic
{
    public class Player
    {
        private string m_PlayerName;
        private uint   m_AmountOfSoldiers;
        private uint   m_AmountOfKings = 0;
        private uint   m_CountWins = 0;

        public Player(string i_Name, uint i_SizeOfBoard)
        {
            PlayerName = i_Name;
            InitAmountSoldiersAndKings(i_SizeOfBoard);
        }

        public string PlayerName
        {
            get
            {
                return m_PlayerName;
            }

            set
            {
                m_PlayerName = value;
            }
        }

        public uint AmountOfSoldiers
        {
            get
            {
                return m_AmountOfSoldiers;
            }

            set
            {
                m_AmountOfSoldiers = value;
            }
        }

        public uint AmountOfKings
        {
            get
            {
                return m_AmountOfKings;
            }

            set
            {
                m_AmountOfKings = value;
            }
        }

        public uint CountWins
        {
            get
            {
                return m_CountWins;
            }

            set
            {
                m_CountWins = value;
            }
        }

        public void InitAmountSoldiersAndKings(uint i_SizeOfBoard)
        {
            AmountOfSoldiers = ((i_SizeOfBoard / 2) - 1) * (i_SizeOfBoard / 2);
            AmountOfKings = 0;
        }

        public void WonTheGame()
        {
            m_CountWins++;
        }

        public void DecreaseAmounts(bool i_IsEatenAKing)
        {
            if (i_IsEatenAKing)
            {
                this.AmountOfKings--;
            }
            else
            {
                this.AmountOfSoldiers--;
            }
        }

        public void SoldierBecomeKing()
        {
            m_AmountOfSoldiers--;
            m_AmountOfKings++;
        }

        public uint CalcSumOfSoldiers()
        {
            return this.AmountOfSoldiers + (this.AmountOfKings * 4);
        }

        //public static void WhichPlayerPlayNow(bool i_FirstPlayerMove, Player i_FisrtPlayer, Player i_SecondPlayer)
        //{
        //    string playerNameHisTurnNow;

        //    if (i_FirstPlayerMove)
        //    {
        //        playerNameHisTurnNow = i_FisrtPlayer.PlayerName;
        //    }
        //    else
        //    {
        //        playerNameHisTurnNow = i_SecondPlayer.PlayerName;
        //    }

        //    //UI_Utils.PrintScoreMessage(i_FisrtPlayer.PlayerName, i_FisrtPlayer.CountWins, i_SecondPlayer.PlayerName, i_SecondPlayer.CountWins);
        //    //UI_Utils.PrintMoveMessage(playerNameHisTurnNow, i_FirstPlayerMove);
        //}
    }
}
