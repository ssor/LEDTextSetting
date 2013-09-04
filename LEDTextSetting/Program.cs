using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LEDTextSetting
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
            resetConfig();

            Application.Run(new Form1());
        }
        public static void resetConfig()
        {
            //初始化一些设置

            object orestIP = nsConfigDB.ConfigDB.getConfig("restIP");
            if (orestIP != null)
            {
                staticClass.restIP = (string)orestIP;
            }
            object orestPort = nsConfigDB.ConfigDB.getConfig("restPort");
            if (orestPort != null)
            {
                staticClass.restPort = (string)orestPort;
            }
        }
    }
}
