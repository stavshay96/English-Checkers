using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Ui
{
    public class GameBoardForm : Form
    {
        private MenuForm m_MenuForm;
        private Button[,] m_ButtonsGameBoard;
        private Label m_LabelFirstPlayerScore;
        private Label m_LabelSecondPlayerScore;
        //private Gameplay m_Gameplay;

        public GameBoardForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            m_MenuForm = new MenuForm();
            m_MenuForm.ShowDialog();
            m_ButtonsGameBoard = new Button[m_MenuForm.SizeOfBoard, m_MenuForm.SizeOfBoard];
            m_LabelFirstPlayerScore = new Label();
            m_LabelFirstPlayerScore.Text = m_MenuForm.TextBoxPlayer1Name;
            m_LabelSecondPlayerScore = new Label();
            m_LabelSecondPlayerScore.Text = m_MenuForm.TextBoxPlayer2Name;

            for (int i = 0; i < m_MenuForm.SizeOfBoard; i++)
            {
                for (int j = 0; j < m_MenuForm.SizeOfBoard; j++)
                {
                    m_ButtonsGameBoard[i,j] = new Button();
                    m_ButtonsGameBoard[i, j].Size = new System.Drawing.Size(70, 70);
                    if ((i+j)%2 != 0)
                    {
                        this.m_ButtonsGameBoard[i, j].BackColor = System.Drawing.Color.White;
                        this.m_ButtonsGameBoard[i, j].Enabled = true;

                    }
                    else
                    {
                        this.m_ButtonsGameBoard[i, j].BackColor = System.Drawing.Color.Gray;
                        this.m_ButtonsGameBoard[i, j].Enabled = false;
                    }
                    if (i + j == 0)
                    {
                        this.m_ButtonsGameBoard[i, j].Location = new System.Drawing.Point(20, 60);
                    }
                    else
                    {
                        if (j == 0)
                        {
                            this.m_ButtonsGameBoard[i, j].Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0, j].Left, this.m_ButtonsGameBoard[i - 1, j].Bottom);
                        }
                        else if (i == 0)
                        {
                            this.m_ButtonsGameBoard[i, j].Location = new System.Drawing.Point(this.m_ButtonsGameBoard[i, j - 1].Right, this.m_ButtonsGameBoard[i, j - 1].Top);
                        }
                        else
                        {
                            this.m_ButtonsGameBoard[i, j].Location = new System.Drawing.Point(this.m_ButtonsGameBoard[i-1, j].Left, this.m_ButtonsGameBoard[i , j-1].Top);
                        }

                    }

                    this.Controls.Add(this.m_ButtonsGameBoard[i, j]);
                }
            }

            this.m_LabelFirstPlayerScore.AutoSize = true;
            this.m_LabelFirstPlayerScore.BackColor = System.Drawing.Color.LightBlue;
            this.m_LabelFirstPlayerScore.Location = new System.Drawing.Point(20, 21);

            this.m_LabelSecondPlayerScore.AutoSize = true;
            this.m_LabelSecondPlayerScore.BackColor = System.Drawing.Color.Orange;
            this.m_LabelSecondPlayerScore.Location = new System.Drawing.Point(100, 21);
            

            this.Controls.Add(this.m_LabelFirstPlayerScore);
            this.Controls.Add(this.m_LabelSecondPlayerScore);
            this.ClientSize = new System.Drawing.Size(m_ButtonsGameBoard[m_MenuForm.SizeOfBoard-1, m_MenuForm.SizeOfBoard-1].Right + 20, m_ButtonsGameBoard[m_MenuForm.SizeOfBoard - 1, m_MenuForm.SizeOfBoard - 1].Bottom + 20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}
