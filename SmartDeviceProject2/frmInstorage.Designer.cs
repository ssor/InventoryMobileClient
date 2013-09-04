namespace SmartDeviceProject2
{
    partial class frmInstorage
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
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnID = new System.Windows.Forms.ColumnHeader();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem5);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.Text = "文件(&F)";
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "读取标签(&R)";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "发送入库结果(&C)";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "退出(&X)";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.Add(this.columnID);
            this.listView1.Columns.Add(this.columnName);
            this.listView1.Location = new System.Drawing.Point(2, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(234, 217);
            this.listView1.TabIndex = 0;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnID
            // 
            this.columnID.Text = "产品编号";
            this.columnID.Width = 142;
            // 
            // columnName
            // 
            this.columnName.Text = "产品名称";
            this.columnName.Width = 92;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 20);
            this.label1.Text = "111";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "获取入库单(&G)";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // frmInstorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 275);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "frmInstorage";
            this.Text = " 入库";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.ColumnHeader columnID;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}