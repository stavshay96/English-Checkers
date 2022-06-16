using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ui
{
    internal static class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            GameBoardForm gameBoardForm = new GameBoardForm();
            if (gameBoardForm.MenuForm.DialogResult != DialogResult.Cancel)
            {
                gameBoardForm.ShowDialog();
            }
        }
    }
}
