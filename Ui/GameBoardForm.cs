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
        private Label m_LabelFirstPlayerScore;
        private Label m_LabelSecondPlayerScore;
        private Label m_LabelWhichPlayerTurn;
        private PictureBox m_PictureBoxWhichPlayerTurn;
        private Gameplay m_Gameplay;
        private Position m_CellMoveFrom;
        private Position m_CellMoveTo;
        private bool m_IsFirstClick = true;
        private bool m_IsFirstPlayerMove = true;
        private bool m_IsEat = false;
        private bool m_IsQuit = false;

        public GameBoardForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            int sizeOfBoard;

            m_CellMoveFrom = new Position();
            m_CellMoveTo = new Position();

            m_MenuForm = new MenuForm();

            do
            {
                m_MenuForm.ShowDialog();
            } while (m_MenuForm.DialogResult != DialogResult.OK);

            sizeOfBoard = m_MenuForm.SizeOfBoard / 2;

            m_ButtonsGameBoard = new Button[m_MenuForm.SizeOfBoard, m_MenuForm.SizeOfBoard];
            m_LabelFirstPlayerScore = new Label();
            m_LabelSecondPlayerScore = new Label();
            m_LabelWhichPlayerTurn = new Label();
            m_PictureBoxWhichPlayerTurn = new PictureBox();


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

            m_LabelFirstPlayerScore.Text = m_MenuForm.TextBoxPlayer1Name + ": " + m_Gameplay.FirstPlayerScore;
            this.m_LabelFirstPlayerScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_LabelFirstPlayerScore.AutoSize = true;
            this.m_LabelFirstPlayerScore.BackColor = System.Drawing.Color.LightBlue;
            this.m_LabelFirstPlayerScore.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, sizeOfBoard - 3].Left, 21);

            m_LabelSecondPlayerScore.Text = m_MenuForm.TextBoxPlayer2Name + ": " + m_Gameplay.SecondPlayerScore;
            this.m_LabelSecondPlayerScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_LabelSecondPlayerScore.AutoSize = true;
            this.m_LabelSecondPlayerScore.BackColor = System.Drawing.Color.Orange;
            this.m_LabelSecondPlayerScore.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, sizeOfBoard + 2].Left, 21);

            //this.m_LabelWhichPlayerTurn.Image = global::Ui.Properties.Resources.x;
            this.m_LabelWhichPlayerTurn.Text = "'s Turn!";
            this.m_LabelWhichPlayerTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_LabelWhichPlayerTurn.AutoSize = true;
            this.m_LabelWhichPlayerTurn.BackColor = System.Drawing.Color.White;
            this.m_LabelWhichPlayerTurn.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, sizeOfBoard - 1].Left + 22, 21);

            this.m_PictureBoxWhichPlayerTurn.Height = this.m_LabelWhichPlayerTurn.Height + 1;
            this.m_PictureBoxWhichPlayerTurn.Width = this.m_LabelWhichPlayerTurn.Height;
            this.m_PictureBoxWhichPlayerTurn.BackColor = System.Drawing.Color.White;
            //this.m_PictureBoxWhichPlayerTurn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_PictureBoxWhichPlayerTurn.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, sizeOfBoard - 1].Left, 21);
            this.m_PictureBoxWhichPlayerTurn.Image = global::Ui.Properties.Resources.x;
            this.m_PictureBoxWhichPlayerTurn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            this.Controls.Add(this.m_LabelFirstPlayerScore);
            this.Controls.Add(this.m_LabelSecondPlayerScore);
            this.Controls.Add(this.m_LabelWhichPlayerTurn);
            this.Controls.Add(this.m_PictureBoxWhichPlayerTurn);
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
                i_CurrentButton.Location = new System.Drawing.Point(20, 60);
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
                if (i_IndexRow < m_MenuForm.SizeOfBoard / 2 - 1)
                {
                    // X
                    this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum].BackgroundImage = global::Ui.Properties.Resources.x;
                }
                else if (m_MenuForm.SizeOfBoard / 2 < i_IndexRow && i_IndexRow < m_MenuForm.SizeOfBoard)
                {
                    //O
                    this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum].BackgroundImage = global::Ui.Properties.Resources.O_removebg_preview__1_;
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
            bool isGameOver = false;
            Button clickedButton = sender as Button;

            // send to method and get the current position to the click button in  the matrix
            Position clickedPosition = GetClickedPosition(clickedButton);

            if (m_IsFirstClick)// boolean
            {
                if (m_Gameplay.IsOwnTheCell(clickedPosition,m_IsFirstPlayerMove))// get the currnt position and and call to the is control the cell in logicBoard(isOwntheCell is in the gameplay class)
                {
                    paintClickedButtonInColor(clickedButton);
                    if (m_CellMoveFrom.PositionInDefulteValue())
                    {
                        m_CellMoveFrom = clickedPosition;
                        m_IsFirstClick = !m_IsFirstClick;

                    }
                    else
                    {
                        if (!m_CellMoveFrom.IsTheSamePositionValue(clickedPosition))
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
                    MessageBox.Show("You not own the cell !","Error Pick",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                // set the CellMoveTo with the currentPosition
                m_CellMoveTo = clickedPosition;
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
            isGameOver = m_Gameplay.IsGameOver();

            if (isGameOver)
            {
                declareOnWinner();
                // winnig message 
               DialogResult userAnswerAboutRestart = MessageBox.Show("Do you want to play another game?", "Restart Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (userAnswerAboutRestart == DialogResult.Yes)
                {
                    reinitGame();
                }
                else
                {
                    this.Close();
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
                    putSymbolOnButton(i,j);
                }
            }
            m_LabelFirstPlayerScore.Text = m_MenuForm.TextBoxPlayer1Name + ": " + m_Gameplay.FirstPlayerScore;
            m_LabelSecondPlayerScore.Text = m_MenuForm.TextBoxPlayer2Name + ": " + m_Gameplay.SecondPlayerScore;
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
                        m_IsFirstClick = !m_IsFirstClick;
                        m_IsFirstPlayerMove = !m_IsFirstPlayerMove;
                        changePictureTurn();
                        //m_IsEat = false;
                        m_CellMoveFrom.ResetPosition();
                        m_CellMoveTo.ResetPosition();
                       
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
                m_IsFirstClick = !m_IsFirstClick;
                m_IsFirstPlayerMove = !m_IsFirstPlayerMove;
                m_CellMoveFrom.ResetPosition();
                m_CellMoveTo.ResetPosition();

                changePictureTurn();
            }
        }

        private void changePictureOnButton(Position i_CellMoveFrom, Position i_CellMoveTo,bool i_IsEat)
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
            if(m_IsFirstPlayerMove)
            {
                this.m_PictureBoxWhichPlayerTurn.Image = global::Ui.Properties.Resources.x;
            }
            else
            {
                this.m_PictureBoxWhichPlayerTurn.Image = global::Ui.Properties.Resources.O_removebg_preview__1_;
            }
        }

        private Position GetClickedPosition(Button i_ClickedButton)
        {
            uint countPosition = 0;
            foreach(Button button in this.m_ButtonsGameBoard)
            {
                if(button == i_ClickedButton)
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
                        symbol = global::Ui.Properties.Resources.redCrown;
                    }
                    else
                    {
                        symbol = global::Ui.Properties.Resources.x;
                    }
                }

                if (i_currentCell.cellState == CellState.eCellState.BelongToSecondPlayer)
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
            }

            return symbol;
        }

        private void declareOnWinner()
        {
            int differenceResult = m_Gameplay.CalcDifferencesBetweenPlayersSoldiers()
            if (differenceResult > 0)
            {
                // first player won
            }
            else if (differenceResult < 0)
            {
                // secound player won 
            }
            else
            {
                //tie
            }
        }
    }
}
