using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;

namespace RfidReader
{
    public class SerialPortDataTransfer : IDataTransfer
    {
        List<deleVoid_Byte_Func> delegateList = new List<deleVoid_Byte_Func>();

        SerialPort comport = null;
        public System.IO.Ports.SerialPort Comport
        {
            get { return comport; }
            set
            {
                //if (comport != null)
                //{
                //    bool bSame = false;
                //    if (comport.PortName == value.PortName &&
                //        comport.BaudRate == value.BaudRate &&
                //        comport.DataBits == value.DataBits &&
                //        comport.StopBits == value.StopBits &&
                //        comport.Parity == value.Parity)
                //    {
                //        bSame = true;
                //    }
                //    //如果新设定的串口和现在使用的不一致，则需要重新设定
                //    if(!bSame)
                //    {
                //        //if (comport.IsOpen)
                //        //{
                //        //    comport.Close();
                //        //}
                //        comport = value;
                //    }
                //}
                //else
                //{
                //    comport = value;
                //}
                comport = value;
                comport.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            }
        }
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = comport.BytesToRead;//n为返回的字节数
                byte[] buf = new byte[n];//初始化buf 长度为n
                comport.Read(buf, 0, n);//读取返回数据并赋值到数组
                foreach (deleVoid_Byte_Func parser in delegateList)
                {
                    parser(buf);
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(
                    string.Format("SerialPortDataTransfer.port_DataReceived  -> exception = {0}"
                    , ex.Message));
            }
        }
        #region IDataTransfer 成员
        public void removeParser(deleVoid_Byte_Func parser)
        {
            this.delegateList.Remove(parser);
        }

        public void AddParser(deleVoid_Byte_Func parser)
        {
            if (this.delegateList.Contains(parser) == false)
            {
                this.delegateList.Add(parser);
            }
        }

        //public void writeData(string data)
       public void writeData(byte[] data)
        {
            if (this.comport != null)
            {
                if (this.comport.IsOpen == false)
                {
                    this.comport.Open();
                }
                this.comport.Write(data, 0, data.Length);
                //this.comport.Write(data);
            }
        }

        public byte[] readData()
        {
            int n = 0;//n为返回的字节数
            n = comport.BytesToRead;//n为返回的字节数
            byte[] buf = new byte[n];//初始化buf 长度为n
            comport.Read(buf, 0, n);//读取返回数据并赋值到数组
            return buf;
        }

        #endregion
    }
}
