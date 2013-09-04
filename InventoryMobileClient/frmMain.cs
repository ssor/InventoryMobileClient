using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InventoryMobileClient
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            frmInventory frm = new frmInventory();
            frm.ShowDialog();
        }
    }
}