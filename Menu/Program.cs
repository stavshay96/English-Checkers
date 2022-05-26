using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menu
{
    internal static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MenuForm menuForm = new MenuForm();
            Application.Run(menuForm);

            string First = menuForm.FirstPlayerName;
            string second = menuForm.SecondPlayerName;
            bool HaveSecondPlayer = menuForm.IsPlayerAgainstFriend;
            int size = menuForm.SizeOfBoard;

        }
    }
}
