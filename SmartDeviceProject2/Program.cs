using System;

using System.Collections.Generic;
using System.Windows.Forms;
using nsConfigDB;
using rfidCheck;

namespace SmartDeviceProject2
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            initialSystem();
            Application.Run(new frmMain());
        }

        static void initialSystem()
        {
            object oPortName = ConfigDB.getConfig("comportName");
            if (oPortName != null)
            {
                sysConfig.comportName = (string)oPortName;
            }
            object oip = ConfigDB.getConfig("ip");
            if (oip != null)
            {
                sysConfig.ip = (string)oip;
            }
            object oTcpPort = ConfigDB.getConfig("tcp_port");
            if (oTcpPort != null)
            {
                sysConfig.tcp_port = int.Parse((string)oTcpPort);
            }
        }
    }
}