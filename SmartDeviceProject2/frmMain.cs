using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmInventory frm = new frmInventory();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmInstorage frm = new frmInstorage();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmOutstorage frm = new frmOutstorage();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmSysSetting frm = new frmSysSetting();
            frm.ShowDialog();
        }


    }
}