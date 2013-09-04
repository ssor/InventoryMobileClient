namespace InventoryMobileClient
{
    partial class frmInventory
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miGetPreInventoryItems = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnID = new System.Windows.Forms.ColumnHeader();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.miGetPreInventoryItems);
            this.menuItem1.Text = "功能";
            // 
            // miGetPreInventoryItems
            // 
            this.miGetPreInventoryItems.Text = "获取盘点列表";
            this.miGetPreInventoryItems.Click += new System.EventHandler(this.miGetPreInventoryItems_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "退出";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.Add(this.columnID);
            this.listView1.Columns.Add(this.columnName);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(240, 268);
            this.listView1.TabIndex = 0;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnID
            // 
            this.columnID.Text = "产品编号";
            this.columnID.Width = 121;
            // 
            // columnName
            // 
            this.columnName.Text = "产品名称";
            this.columnName.Width = 116;
            // 
            // frmInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.listView1);
            this.Menu = this.mainMenu1;
            this.Name = "frmInventory";
            this.Text = "仓库盘点";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miGetPreInventoryItems;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnID;
        private System.Windows.Forms.ColumnHeader columnName;
    }
}