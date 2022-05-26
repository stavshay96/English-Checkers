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
        public MenuForm()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

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

        private void radioButtonMediumSize_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPlayer1Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPlayer2Name_TextChanged(object sender, EventArgs e)
        {

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
    }
}
