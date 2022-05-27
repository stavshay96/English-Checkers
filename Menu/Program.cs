using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ui
{
    internal static class Program
    {
        //[STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            GameBoardForm gameBoardForm = new GameBoardForm();
            gameBoardForm.ShowDialog();
            ////Application.Run(menuForm);

        }
    }
}
