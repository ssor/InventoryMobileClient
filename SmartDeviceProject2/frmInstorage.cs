using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using httpHelper;
using Inventory;
using System.Collections;
using RfidReader;
using System.IO.Ports;
using invokePhpRestDemo;
using rfidCheck;

namespace SmartDeviceProject2
{
    public partial class frmInstorage : Form
    {

        Timer __timer;
        rfidOperateUnitBase operaterGetTag = null;
        IDataTransfer dataTransfer = null;
        SerialPort comport = null;

        public frmInstorage()
        {
            InitializeComponent();
            this.label1.Text = string.Empty;

            __timer = new Timer();
            __timer.Interval = 1000;
            __timer.Tick += new EventHandler(__timer_Tick);

            dataTransfer = new SerialPortDataTransfer();
            comport = new SerialPort(sysConfig.comportName, 57600, Parity.None, 8, StopBits.One);
            ((SerialPortDataTransfer)dataTransfer).Comport = comport;
            operaterGetTag = new rfidOperateUnit2600InventoryTag(dataTransfer);
            operaterGetTag.registeCallback(new deleRfidOperateCallback(UpdateEpcList));

            this.Closing += new CancelEventHandler(frmInstorage_Closing);
        }

        void frmInstorage_Closing(object sender, CancelEventArgs e)
        {
            if (this.comport != null && this.comport.IsOpen == true)
            {
                this.comport.Close();
            }
        }

        void __timer_Tick(object sender, EventArgs e)
        {
            operaterGetTag.linkOpen();
            operaterGetTag.OperateStart(true);
        }
        void UpdateEpcList(object o)
        {
            //把读取到的标签epc与产品的进行关联
            deleControlInvoke dele = delegate(object oOperateMessage)
            {

                operateMessage msg = (operateMessage)oOperateMessage;
                if (msg.status == "fail")
                {
                    this.label1.Text = "出现错误：" + msg.message;
                    return;
                }
                string value = msg.message;
                //this.label1.Text = "读取到标签 " + value + " " + DateTime.Now.ToString("");
                this.label1.Text = value;

                foreach (System.Windows.Forms.ListViewItem lvi in this.listView1.Items)
                {
                    if (lvi.Text == value)
                    {
                        lvi.Checked = true;
                        break;
                    }
                }
            };
            this.Invoke(dele, o);
        }


        private void menuItem2_Click(object sender, EventArgs e)
        {
            HttpWebConnect helper = new HttpWebConnect();
            string url = RestUrl.getPreProListToStorage;
            helper.RequestCompleted += new deleGetRequestObject(helper_RequestCompleted_getPreProListToStorage);
            helper.TryRequest(url);
            return;
        }
        void helper_RequestCompleted_getPreProListToStorage(object o)
        {
            deleControlInvoke dele = delegate(object oProductList)
            {
                string strProduct = (string)oProductList;
                object olist = fastJSON.JSON.Instance.ToObjectListDic(strProduct);
                this.listView1.Items.Clear();
                ArrayList array = (ArrayList)olist;
                for (int i = 0; i < array.Count; i++)
                {
                    Dictionary<string, object> dicTemp = (Dictionary<string, object>)array[i];
                    Product p = Product.createInstance(dicTemp);
                    System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
                    listViewItem1.Text = p.productID;
                    listViewItem1.SubItems.Add(p.productName);
                    this.listView1.Items.Add(listViewItem1);
                }
            };
            this.Invoke(dele, o);
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            if (this.menuItem5.Checked == true)//正在循环读取标签
            {
                __timer.Enabled = false;
                this.menuItem5.Checked = false;
            }
            else
            {
                __timer.Enabled = true;
                this.menuItem5.Checked = true;

            }
        }
        //发送盘点结果
        private void menuItem4_Click(object sender, EventArgs e)
        {
            //发送前先将读取标签的操作停止
            if (this.menuItem5.Checked == true)//正在循环读取标签
            {
                __timer.Enabled = false;
                this.menuItem5.Checked = false;
            }

            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            List<tagID> list = new List<tagID>();
            foreach (System.Windows.Forms.ListViewItem lvi in this.listView1.Items)
            {
                if (lvi.Checked == true)
                {
                    tagID tag1 = new tagID(lvi.Text, strTime, InventoryCommand.扫描入库);
                    list.Add(tag1);
                }
            }

            string jsonString = tagID.toJSONFromList(list);
            HttpWebConnect helper = new HttpWebConnect();
            helper.RequestCompleted += new deleGetRequestObject(helper_RequestCompleted);
            string url = RestUrl.addScanedTags;
            helper.TryPostData(url, jsonString);
        }

        void helper_RequestCompleted(object o)
        {
            deleControlInvoke dele = delegate(object ol)
            {
                string tags = (string)ol;
                object olist = fastJSON.JSON.Instance.ToObjectListDic(tags);
                ArrayList array = (ArrayList)olist;
                bool bOK = true;
                for (int i = 0; i < array.Count; i++)
                {
                    Dictionary<string, object> dicTemp = (Dictionary<string, object>)array[i];
                    tagID t = tagID.createInstance(dicTemp);
                    if (t.state != "ok")
                    {
                        bOK = false;
                        MessageBox.Show("发送扫描数据失败，请重试！", "信息提示");
                        break;
                    }
                }

                if (bOK == true)
                {
                    MessageBox.Show("发送扫描数据成功！", "信息提示");
                }
            };
            this.Invoke(dele, o);
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}