using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTest
{
    static class Program
    {
        





        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            
        


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BasicForm basicForm = new BasicForm();
            ServerController svController = new ServerController(basicForm);
            basicForm.AddController(svController);
            Debug.Print("프린터 연결됨");
            Application.Run(basicForm);


        }
    }
}
