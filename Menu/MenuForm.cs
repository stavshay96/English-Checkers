using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menu
{
    public partial class MenuForm : Form
    {
        private string firstPlayerName = "";
        private string secondPlayerName = "";
        private bool isPlayerAgainstFriend = false;
        private int sizeOfBoard = 0;
        public MenuForm()
        {
            InitializeComponent();
        }

        public string FirstPlayerName
        {
            get
            {
                return firstPlayerName;
            }
            set
            {
                firstPlayerName = value;
            }
        }

        public string SecondPlayerName
        {
            get
            {
                return secondPlayerName;
            }
            set
            {
                secondPlayerName = value;
            }
        }

        public bool IsPlayerAgainstFriend
        {
            get
            {
                return isPlayerAgainstFriend;
            }
            set
            {
                isPlayerAgainstFriend = value;
            }
        }

        public int SizeOfBoard
        { 
            get
            {
                return sizeOfBoard;
            }
            set
            {
                sizeOfBoard = value;
            }
        }


        private void buttonDoneConfiguration_Click(object sender, EventArgs e)
        {
            if(arePlayersNamesNotEmpty())
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("You inserted empty name!", "Name Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool arePlayersNamesNotEmpty()
        {
            return !(string.IsNullOrWhiteSpace(textBoxPlayer1Name.Text) || string.IsNullOrWhiteSpace(textBoxPlayer2Name.Text));
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }


        private void textBoxPlayer1Name_TextChanged(object sender, EventArgs e)
        {
            TextBox Name = sender as TextBox;
            FirstPlayerName = Name.Text;
        }

        private void textBoxPlayer2Name_TextChanged(object sender, EventArgs e)
        {
            TextBox Name = sender as TextBox;
            if(!IsPlayerAgainstFriend)
            {
                SecondPlayerName = "CPU";
            }
            else
            {
                SecondPlayerName = Name.Text;
            }

        }

        private void checkBoxIsPlayingAgainstFriend_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as CheckBox).Checked)
            {
                textBoxPlayer2Name.Enabled = true;
                textBoxPlayer2Name.Text = "";
                isPlayerAgainstFriend = true;
            }
            else
            {
                textBoxPlayer2Name.Enabled = false;
                textBoxPlayer2Name.Text = "CPU";
                isPlayerAgainstFriend = false;
            }
        }

        private void radioButtonLargeSize_CheckedChanged(object sender, EventArgs e)
        {
            this.sizeOfBoard = 10;
        }

        private void radioButtonMediumSize_CheckedChanged(object sender, EventArgs e)
        {
            this.sizeOfBoard = 8;
        }
        private void radioButtonSmallSize_CheckedChanged(object sender, EventArgs e)
        {
            this.sizeOfBoard = 6;
        }

    }
}
