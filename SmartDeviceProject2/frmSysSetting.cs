using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using nsConfigDB;
using rfidCheck;
using System.Net;

namespace SmartDeviceProject2
{
    public partial class frmSysSetting : Form
    {
        public frmSysSetting()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            for (int i = 0; i < ports.Length;i++ )
            {
                cmbPortName.Items.Add(ports[i]);
            }

            this.Load += new EventHandler(frmSysSetting_Load);
        }

        void frmSysSetting_Load(object sender, EventArgs e)
        {
            string portName = string.Empty;
            string ip = string.Empty;
            string tcpPort = string.Empty;

            object o = ConfigDB.getConfig("comportName");
            if (o != null)
            {
                portName = (string)o;
            }
            o = ConfigDB.getConfig("ip");
            if (o != null)
            {
                ip = (string)o;
            }
            o = ConfigDB.getConfig("tcp_port");
            if (o != null)
            {
                tcpPort = (string)o;
            }
            this.cmbPortName.Text = portName;
            this.txtPort.Text = tcpPort;
            this.txtIP.Text = ip;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.checkValidation())
            {
                string portName = this.cmbPortName.Text;
                string ip = this.txtIP.Text;
                string tcpPort = this.txtPort.Text;
                ConfigDB.saveConfig("comportName", portName);
                ConfigDB.saveConfig("ip", ip);
                ConfigDB.saveConfig("tcp_port", tcpPort);

                sysConfig.ip = ip;
                sysConfig.comportName = portName;
                sysConfig.tcp_port = int.Parse(tcpPort);

                this.Close();
            }
        }
        bool checkValidation()
        {
            bool bR = true;
            int tcpPort = 5000;
            try
            {
                tcpPort = int.Parse(this.txtPort.Text);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("端口设置不正确，请重新设置！");
                return false;
            }
            string ip = string.Empty;
            try
            {
                ip = txtIP.Text;
                IPAddress address = IPAddress.Parse(ip);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("IP设置不正确，请重新设置！");
                return false;
            }
            return bR;
        }

    }
}