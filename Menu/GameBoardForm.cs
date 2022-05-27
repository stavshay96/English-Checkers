﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Menu.Properties;

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

                    enableButtonAndDraw(this.m_ButtonsGameBoard[i, j], i,j);

                    locateButtonOnForm(this.m_ButtonsGameBoard[i, j], i, j);

                    putSymbolOnButton(i, j);

                    this.Controls.Add(this.m_ButtonsGameBoard[i, j]);
                }
            }

            this.m_LabelFirstPlayerScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_LabelFirstPlayerScore.AutoSize = true;
            this.m_LabelFirstPlayerScore.BackColor = System.Drawing.Color.LightBlue;
            this.m_LabelFirstPlayerScore.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0,1].Left, 21);

            this.m_LabelSecondPlayerScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_LabelSecondPlayerScore.AutoSize = true;
            this.m_LabelSecondPlayerScore.BackColor = System.Drawing.Color.Orange;
            this.m_LabelSecondPlayerScore.Location = new System.Drawing.Point(this.m_ButtonsGameBoard[0,3].Left, 21);
            

            this.Controls.Add(this.m_LabelFirstPlayerScore);
            this.Controls.Add(this.m_LabelSecondPlayerScore);
            this.ClientSize = new System.Drawing.Size(m_ButtonsGameBoard[m_MenuForm.SizeOfBoard-1, m_MenuForm.SizeOfBoard-1].Right + 20, m_ButtonsGameBoard[m_MenuForm.SizeOfBoard - 1, m_MenuForm.SizeOfBoard - 1].Bottom + 20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "English Checkers";
            this.BackgroundImage = global::Menu.Properties.Resources.checkersBackground21;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            //this.Icon = ((System.Drawing.Icon)(global::Menu.Properties.Resources.checkersLogo);

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
                    this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum].BackgroundImage = global::Menu.Properties.Resources.x;
                    this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                }

                if (m_MenuForm.SizeOfBoard / 2 < i_IndexRow && i_IndexRow < m_MenuForm.SizeOfBoard)
                {
                    //O
                    this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum].BackgroundImage = global::Menu.Properties.Resources.O;
                    this.m_ButtonsGameBoard[i_IndexRow, i_IndexColoum].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                }
            }

        }
    }
}
