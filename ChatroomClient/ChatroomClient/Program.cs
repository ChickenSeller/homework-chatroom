using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatroomClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program.client = new Client();
            Program.client.formLogin.Show();
            Program.client.formMain = new FormMain();
            //Program.client.formMain.Show();
            Program.client.formMain.Hide();
            Application.Run();
        }
        public static Client client;
        public delegate void ShowFormMessageBox(string msg, ref object form);
        public delegate void ShowFormDelegate(ref object form);
    }
}
