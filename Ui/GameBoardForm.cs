using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Logic;

namespace Ui
{
    public class GameBoardForm : Form
    {
        private MenuForm m_MenuForm;
        private Button[,] m_ButtonsGameBoard;
        private Label labelFirstPlayerScore;
        private Label labelSecondPlayerScore;
        private Label labelWhichPlayerTurn;
        private PictureBox m_PictureBoxWhichPlayerTurn;
        private PictureBox m_PictureBoxFirstPlayer;
        private PictureBox m_PictureBoxSecondPlayer;
        private Gameplay m_Gameplay;
        private Position m_CellMoveFrom;
        private Position m_CellMoveTo;
        private bool m_IsFirstClick = true;
        private bool m_IsFirstPlayerMove = true;
        private bool m_IsEat = false;
        private bool m_GotPlayersNames = false;

        public GameBoardForm()
        {
            initializeComponent();
        }

        private void initializeComponent()
        {
            int sizeOfBoard;

            m_CellMoveFrom = new Position();
            m_CellMoveTo = new Position();

            m_MenuForm = new MenuForm();

            if (ensurePlayersNames())
            {
                sizeOfBoard = m_MenuForm.SizeOfBoard / 2;

                m_ButtonsGameBoard = new Button[m_MenuForm.SizeOfBoard, m_MenuForm.SizeOfBoard];
                labelFirstPlayerScore = new Label();
                labelSecondPlayerScore = new Label();
                labelWhichPlayerTurn = new Label();
                m_PictureBoxWhichPlayerTurn = new PictureBox();
                m_PictureBoxFirstPlayer = new PictureBox();
                m_PictureBoxSecondPlayer = new PictureBox();


                for (int i = 0; i < m_MenuForm.SizeOfBoard; i++)
                {
                    for (int j = 0; j < m_MenuForm.SizeOfBoard; j++)
                    {
                        m_ButtonsGameBoard[i, j] = new Button();
                        m_ButtonsGameBoard[i, j].Size = new System.Drawing.Size(70, 70);

                        enableButtonAndDraw(this.m_ButtonsGameBoard[i, j], i, j);

                        locateButtonOnForm(this.m_ButtonsGameBoard[i, j], i, j);

                        putSymbolOnButton(i, j);

                        this.m_ButtonsGameBoard[i, j].Click += new System.EventHandler(this.buttonMButtonsGameBoard_Click);

                        this.Controls.Add(this.m_ButtonsGameBoard[i, j]);
                    }
                }

                m_Gameplay = new Gameplay(m_MenuForm.TextBoxPlayer1Name, m_MenuForm.TextBoxPlayer2Name, (uint)m_MenuForm.SizeOfBoard, m_MenuForm.IsPlayingAgainstFriend);
                m_Gameplay.OnMove += M_Gameplay_OnMove;
                labelFirstPlayerScore.Text = m_MenuForm.TextBoxPlayer1Name + ": " + m_Gameplay.FirstPlayerScore;
                this.labelFirstPlayerScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
                this.labelFirstPlayerScore.AutoSize = true;
                this.labelFirstPlayerScore.BackColor = System.Drawing.Color.White;
                this.labelFirstPlayerScore.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, sizeOfBoard - 3].Left + 22, 21);
                labelSecondPlayerScore.Text = m_MenuForm.TextBoxPlayer2Name + ": " + m_Gameplay.SecondPlayerScore;
                this.labelSecondPlayerScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
                this.labelSecondPlayerScore.AutoSize = true;
                this.labelSecondPlayerScore.BackColor = System.Drawing.Color.White;
                this.labelSecondPlayerScore.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, sizeOfBoard + 2].Left + 17, 21);
                this.labelWhichPlayerTurn.Text = "'s Turn!";
                this.labelWhichPlayerTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
                this.labelWhichPlayerTurn.AutoSize = true;
                this.labelWhichPlayerTurn.BackColor = System.Drawing.Color.White;
                this.labelWhichPlayerTurn.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, sizeOfBoard - 1].Left + 22, 21);

                this.m_PictureBoxWhichPlayerTurn.Height = this.labelWhichPlayerTurn.Height + 1;
                this.m_PictureBoxWhichPlayerTurn.Width = this.labelWhichPlayerTurn.Height;
                this.m_PictureBoxWhichPlayerTurn.BackColor = System.Drawing.Color.White;
                this.m_PictureBoxWhichPlayerTurn.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, sizeOfBoard - 1].Left, 21);
                this.m_PictureBoxWhichPlayerTurn.Image = global::Ui.Properties.Resources.O_removebg_preview__1_;
                this.m_PictureBoxWhichPlayerTurn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.m_PictureBoxFirstPlayer.Height = this.labelFirstPlayerScore.Height + 1;
                this.m_PictureBoxFirstPlayer.Width = this.labelFirstPlayerScore.Height;
                this.m_PictureBoxFirstPlayer.BackColor = System.Drawing.Color.White;
                this.m_PictureBoxFirstPlayer.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, sizeOfBoard - 3].Left, 21);
                this.m_PictureBoxFirstPlayer.Image = global::Ui.Properties.Resources.O_removebg_preview__1_;
                this.m_PictureBoxFirstPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.m_PictureBoxSecondPlayer.Height = this.labelSecondPlayerScore.Height + 1;
                this.m_PictureBoxSecondPlayer.Width = this.labelSecondPlayerScore.Height;
                this.m_PictureBoxSecondPlayer.BackColor = System.Drawing.Color.White;
                this.m_PictureBoxSecondPlayer.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, sizeOfBoard + 2].Left-5, 21);
                this.m_PictureBoxSecondPlayer.Image = global::Ui.Properties.Resources.x;
                this.m_PictureBoxSecondPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.Controls.Add(this.labelFirstPlayerScore);
                this.Controls.Add(this.labelSecondPlayerScore);
                this.Controls.Add(this.labelWhichPlayerTurn);
                this.Controls.Add(this.m_PictureBoxWhichPlayerTurn);
                this.Controls.Add(this.m_PictureBoxFirstPlayer);
                this.Controls.Add(this.m_PictureBoxSecondPlayer);
                this.ClientSize = new System.Drawing.Size(m_ButtonsGameBoard[m_MenuForm.SizeOfBoard - 1, m_MenuForm.SizeOfBoard - 1].Right + 20, m_ButtonsGameBoard[m_MenuForm.SizeOfBoard - 1, m_MenuForm.SizeOfBoard - 1].Bottom + 20);
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "English Checkers";
                this.BackgroundImage = global::Ui.Properties.Resources.checkersLogo;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
                this.MaximizeBox = false;
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            }
        }

        private bool ensurePlayersNames()
        {
            if (!m_GotPlayersNames)
            {
                if (m_MenuForm.ShowDialog() == DialogResult.OK)
                {
                    if (m_MenuForm.ArePlayersNamesNotEmpty())
                    {
                        m_GotPlayersNames = true;
                    }
                    else
                    {
                        MessageBox.Show("You inserted empty name!", "Name Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ensurePlayersNames();
                    }
                }
            }
            return m_GotPlayersNames;
        }

        public Form MenuForm
        {
            get
            {
                return m_MenuForm;
            }
        }

        private void enableButtonAndDraw(Button i_CurrentButton, int i_IndexRow, int i_IndexColoum)
        {
            if ((i_IndexRow + i_IndexColoum) % 2 != 0)
            {
                i_CurrentButton.BackColor = System.Drawing.Color.White;
                i_CurrentButton.Enabled = true;
            }
            else
            {
                i_CurrentButton.BackColor = System.Drawing.Color.Gray;
                i_CurrentButton.Enabled = false;
            }
        }

        private void locateButtonOnForm(Button i_CurrentButton, int i_IndexRow, int i_IndexColoum)
        {
            if (i_IndexRow + i_IndexColoum == 0)
            {
                i_CurrentButton.Location = new System.Drawing.Point(20, 100);
            }
            else
            {
                if (i_IndexColoum == 0)
                {
                    i_CurrentButton.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, i_IndexColoum].Left, this.m_ButtonsGameBoard[i_IndexRow - 1, i_IndexColoum].Bottom);
                }
                else if (i_IndexRow == 0)
                {
                    i_CurrentButton.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum - 1].Right, this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum - 1].Top);
                }
                else
                {
                    i_CurrentButton.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[i_IndexRow - 1, i_IndexColoum].Left, this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum - 1].Top);
                }
            }
        }

        private void putSymbolOnButton(int i_IndexRow, int i_IndexColoum)
        {
            if ((i_IndexRow + i_IndexColoum) % 2 != 0)
            {
                if (i_IndexRow < (m_MenuForm.SizeOfBoard / 2) - 1)
                {
                    //O
                    this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum].BackgroundImage = global::Ui.Properties.Resources.O_removebg_preview__1_;
                }
                else if (m_MenuForm.SizeOfBoard / 2 < i_IndexRow && i_IndexRow < m_MenuForm.SizeOfBoard)
                {
                    //X
                    this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum].BackgroundImage = global::Ui.Properties.Resources.x;
                }
                else
                {
                    this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum].BackgroundImage = null;
                }

                this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
        }

        private void buttonMButtonsGameBoard_Click(object sender, EventArgs e)
        {
            bool isGameOver;
            Button clickedButton = sender as Button;
            Position clickedPosition = getClickedPosition(clickedButton);

            if (m_IsFirstClick)// boolean
            {
                firstClickOperation(clickedButton, clickedPosition);
            }
            else
            {
                secondClickOperation(clickedPosition);
            }

            isGameOver = m_Gameplay.IsGameOver();

            if (isGameOver)
            {
                gameOverOperation();
            }
        }

        private void firstClickOperation(Button i_ClickedButton, Position i_ClickedPosition)
        {
            if (m_Gameplay.IsOwnTheCell(i_ClickedPosition, m_IsFirstPlayerMove))// get the currnt position and and call to the is control the cell in logicBoard(isOwntheCell is in the gameplay class)
            {
                paintClickedButtonInColor(i_ClickedButton);
                if (m_CellMoveFrom.PositionInDefulteValue())
                {
                    m_CellMoveFrom = i_ClickedPosition;
                    m_IsFirstClick = !m_IsFirstClick;
                }
                else
                {
                    if (!m_CellMoveFrom.IsTheSamePositionValue(i_ClickedPosition))
                    {
                        MessageBox.Show(" you can eat, choose the correct button !", "Error Pick", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        m_IsFirstClick = !m_IsFirstClick;
                    }
                }
            }
            else
            {
                MessageBox.Show("You don't own the cell !", "Error Pick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void secondClickOperation(Position i_ClickedPosition)
        {
            // set the CellMoveTo with the currentPosition
            m_CellMoveTo = i_ClickedPosition;
            if (m_CellMoveFrom.IsTheSamePositionValue(m_CellMoveTo))
            {
                paintButtonsInWhite();
                m_IsFirstClick = !m_IsFirstClick;
                m_CellMoveFrom.ResetPosition();
                m_CellMoveTo.ResetPosition();
            }
            else
            {
                //new method in gameplay that checks the positions
                if (m_Gameplay.IsLegalMove(ref m_CellMoveFrom, ref m_CellMoveTo, m_IsFirstPlayerMove, ref m_IsEat))
                {
                    // we have more move
                    m_Gameplay.RunGame(m_CellMoveFrom, m_CellMoveTo, ref m_IsEat, m_IsFirstPlayerMove);
                }
                else
                {
                    MessageBox.Show("Your move is illegal!", "Error Move", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void reinitGame()
        {
            m_Gameplay.ReinitGame();
            for (int i = 0; i < m_MenuForm.SizeOfBoard; i++)
            {
                for (int j = 0; j < m_MenuForm.SizeOfBoard; j++)
                {
                    putSymbolOnButton(i, j);
                }
            }

            labelFirstPlayerScore.Text = m_MenuForm.TextBoxPlayer1Name + ": " + m_Gameplay.FirstPlayerScore;
            labelSecondPlayerScore.Text = m_MenuForm.TextBoxPlayer2Name + ": " + m_Gameplay.SecondPlayerScore;
            m_IsFirstClick = true;
            m_IsFirstPlayerMove = true;
            changePictureTurn();
        }

        private void M_Gameplay_OnMove(Position From, Position To, bool i_IsEatingAvailable, bool IsEat, bool IsCanEatAgain, Position ExpectenMove)
        {
            m_IsEat = IsEat;
            if (i_IsEatingAvailable)
            {
                if (IsEat)
                {
                    changePictureOnButton(From, To, IsEat);
                    if (IsCanEatAgain)
                    {
                        m_CellMoveFrom = ExpectenMove;
                        m_CellMoveTo.ResetPosition();
                    }
                    else
                    {
                        doWhenTurnIsChanged(); 
                    }
                }
                else
                {
                    // messageBox iligale move need to do an eat Move
                     MessageBox.Show("You must eat your enemy !","Error Pick",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                changePictureOnButton(From, To, IsEat);
                doWhenTurnIsChanged();
            }

            if (m_Gameplay.IsSecondPlayerIsCPU && !m_IsFirstPlayerMove)
            {
                m_Gameplay.RunGame(m_CellMoveFrom, m_CellMoveTo, ref m_IsEat, m_IsFirstPlayerMove);
                if (!IsCanEatAgain)
                {
                    m_IsFirstClick = !m_IsFirstClick;
                }
            }
        }

        private void doWhenTurnIsChanged()
        {
            m_IsFirstClick = !m_IsFirstClick;
            m_IsFirstPlayerMove = !m_IsFirstPlayerMove;
            changePictureTurn();
            m_CellMoveFrom.ResetPosition();
            m_CellMoveTo.ResetPosition();
        }

        private void changePictureOnButton(Position i_CellMoveFrom, Position i_CellMoveTo, bool i_IsEat)
        {
            if (i_IsEat)
            {
                Position middleCell = new Position();
                middleCell.FindEatenPosition(i_CellMoveFrom, i_CellMoveTo);
                this.m_ButtonsGameBoard[middleCell.Row, middleCell.Column].BackgroundImage = getSymbol(m_Gameplay.MovedCells[0]);
                this.m_ButtonsGameBoard[i_CellMoveFrom.Row, i_CellMoveFrom.Column].BackgroundImage = getSymbol(m_Gameplay.MovedCells[2]);
                this.m_ButtonsGameBoard[i_CellMoveTo.Row, i_CellMoveTo.Column].BackgroundImage = getSymbol(m_Gameplay.MovedCells[1]);
            }
            else
            {
                this.m_ButtonsGameBoard[i_CellMoveTo.Row, i_CellMoveTo.Column].BackgroundImage = getSymbol(m_Gameplay.MovedCells[0]);
                this.m_ButtonsGameBoard[i_CellMoveFrom.Row, i_CellMoveFrom.Column].BackgroundImage = getSymbol(m_Gameplay.MovedCells[1]);
            }

            paintButtonsInWhite();
        }

        private void paintClickedButtonInColor(Button io_ClickedButton)
        {
            if (io_ClickedButton.BackColor == Color.White)
            {
                io_ClickedButton.BackColor = Color.LightBlue;
            }
            else
            {
                io_ClickedButton.BackColor = Color.White;
            }

            foreach (Button button in this.m_ButtonsGameBoard)
            {
                if (button.Enabled && button != io_ClickedButton)
                {
                    button.BackColor = Color.White;
                }
            }
        }

        private void paintButtonsInWhite()
        {
            foreach (Button button in this.m_ButtonsGameBoard)
            {
                if (button.Enabled)
                {
                    button.BackColor = Color.White;
                }
            }
        }

        private void changePictureTurn()
        {
            if (m_IsFirstPlayerMove)
            {
                this.m_PictureBoxWhichPlayerTurn.Image = global::Ui.Properties.Resources.O_removebg_preview__1_;
            }
            else
            {
                this.m_PictureBoxWhichPlayerTurn.Image = global::Ui.Properties.Resources.x;
            }
        }

        private Position getClickedPosition(Button i_ClickedButton)
        {
            uint countPosition = 0;
            foreach (Button button in this.m_ButtonsGameBoard)
            {
                if (button == i_ClickedButton)
                {
                    break;
                }
                else
                {
                    countPosition++;
                }
            }

            Position clickedPosition = new Position();
            clickedPosition.Row = (uint)(countPosition / m_MenuForm.SizeOfBoard);
            clickedPosition.Column = (uint)(countPosition % m_MenuForm.SizeOfBoard);
            return clickedPosition;
        }

        private static Image getSymbol(Cell i_currentCell)
        {
            Image symbol = null;
            if (i_currentCell.IsActiveCell)
            {
                if (i_currentCell.cellState == CellState.eCellState.BelongToFirstPlayer)
                {
                    if (i_currentCell.IsKing)
                    {
                        symbol = global::Ui.Properties.Resources.goldCrown;
                    }
                    else
                    {
                        symbol = global::Ui.Properties.Resources.O_removebg_preview__1_;
                    }
                }

                if (i_currentCell.cellState == CellState.eCellState.BelongToSecondPlayer)
                {
                    if (i_currentCell.IsKing)
                    {
                        symbol = global::Ui.Properties.Resources.redCrown;
                    }
                    else
                    {
                        symbol = global::Ui.Properties.Resources.x;
                    }
                }
            }

            return symbol;
        }

        private void gameOverOperation()
        {
            declareOnWinner(out string winDeclaration);
            // winnig message 
            DialogResult userAnswerAboutRestart = MessageBox.Show(string.Format(@"{0}
Do you want to play another round?", winDeclaration), "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (userAnswerAboutRestart == DialogResult.Yes)
            {
                reinitGame();
            }
            else
            {
                this.Close();
            }
        }

        private void declareOnWinner(out string o_WinDeclaration)
        {
            int differenceResult = m_Gameplay.CalcDifferencesBetweenPlayersSoldiers();
            if (differenceResult > 0)
            {
                o_WinDeclaration = string.Format("{0} won this game!", m_MenuForm.TextBoxPlayer1Name);
            }
            else if (differenceResult < 0)
            {
                o_WinDeclaration = string.Format("{0} won this game!", m_MenuForm.TextBoxPlayer2Name);
            }
            else
            {
                o_WinDeclaration = "There is a tie!";
            }
        }
    }
}
