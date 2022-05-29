using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ui
{
    public partial class MenuForm : Form
    {
        private int sizeOfBoard = 6;
        public MenuForm()
        {
            InitializeComponent();
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

        public string TextBoxPlayer1Name
        {
            get
            {
                return this.textBoxPlayer1Name.Text;
            }
        }
        public string TextBoxPlayer2Name
        {
            get
            {
                return this.textBoxPlayer2Name.Text;
            }
        }

        public bool IsPlayingAgainstFriend
        {
            get
            {
                return this.checkBoxIsPlayingAgainstFriend.Checked;
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
                this.DialogResult = DialogResult.Retry;
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
            textBoxPlayer1Name.Text = Name.Text;
        }

        private void textBoxPlayer2Name_TextChanged(object sender, EventArgs e)
        {
            TextBox Name = sender as TextBox;
            textBoxPlayer2Name.Text = Name.Text;
        }

        private void checkBoxIsPlayingAgainstFriend_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as CheckBox).Checked)
            {
                textBoxPlayer2Name.Enabled = true;
                textBoxPlayer2Name.Text = "";
            }
            else
            {
                textBoxPlayer2Name.Enabled = false;
                textBoxPlayer2Name.Text = "CPU";
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
