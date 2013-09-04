using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Inventory;

namespace InventoryMobileClient
{
    public partial class frmInventory : Form
    {
        public frmInventory()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmInventory_Load);
        }

        void frmInventory_Load(object sender, EventArgs e)
        {

        }
 
        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void miGetPreInventoryItems_Click(object sender, EventArgs e)
        {
            //HttpWebConnect helper = new HttpWebConnect();
            //string url = RestUrl.getProductInfoForInventoryList;
            //helper.RequestCompleted += new deleGetRequestObject(helper_RequestCompleted_getProductInfoForInventoryList);
            //helper.TryRequest(url);
            return;
        }
        void helper_RequestCompleted_getProductInfoForInventoryList(object o)
        {
            deleControlInvoke dele = delegate(object oProductList)
            {
                string strProduct = (string)oProductList;
                object olist = fastJSON.JSON.Instance.ToObjectList(strProduct, typeof(List<Product>), typeof(Product));
                this.listView1.Items.Clear();

                foreach (Product p in (List<Product>)olist)
                {
                    System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
                    listViewItem1.Text = p.productID;
                    listViewItem1.SubItems.Add(p.productName);
                    this.listView1.Items.Add(listViewItem1);
                }
            };
            this.Invoke(dele, o);
        }
    }
}