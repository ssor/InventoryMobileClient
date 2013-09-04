using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace CommonSerialPort
{
    public delegate void delevoid_bytes(byte[] bytes);
    /// <summary>
    /// 提供一个统一的串口，防止多串口冲突
    /// 当一个页面只有一个串口时，一定要在页面关闭时关闭串口
    /// 当一个页面使用名义上的多个串口时，用完之后及时关闭
    /// </summary>
    public class StaticSerialPort
    {
        static SerialPort comport = null;
        static List<delevoid_bytes> delegateList = new List<delevoid_bytes>();

        public static void AddParser(delevoid_bytes parser)
        {
            if (!StaticSerialPort.delegateList.Contains(parser))
            {
                StaticSerialPort.delegateList.Add(parser);
            }
        }
        public static void removeParser(delevoid_bytes parser)
        {
            if (StaticSerialPort.delegateList.Contains(parser))
            {
                StaticSerialPort.delegateList.Remove(parser);
            }
        }
        //关闭串口的时候必须考虑死锁问题
        public static void closeStaticSerialPort()
        {
            StaticSerialPort.comport.Close();
        }
        private static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = comport.BytesToRead;//n为返回的字节数
                byte[] buf = new byte[n];//初始化buf 长度为n
                comport.Read(buf, 0, n);//读取返回数据并赋值到数组
                //_RFIDHelper.Parse(buf);
                foreach (delevoid_bytes parser in StaticSerialPort.delegateList)
                {
                    parser(buf);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static SerialPort getStaticSerialPort()
        {
            if (StaticSerialPort.comport == null)
            {
                StaticSerialPort.comport = new SerialPort();
                comport.DataReceived += StaticSerialPort.port_DataReceived;
                StaticSerialPort.resetStaticSerialPort();//使用统一配置参数
            }
            return StaticSerialPort.comport;
        }
        /// <summary>
        /// 这个函数使用统一的ConfigManager来配置串口参数，如果项目中没有ConfigManager类，需要将此函数注释
        /// </summary>
        public static void resetStaticSerialPort()
        {
            SerialPort sp = getStaticSerialPort();
            if (sp == null)
            {
                return;
            }
            bool biniOpened = sp.IsOpen;
            if (biniOpened)
            {
                sp.Close();
            }
            //try
            //{
            //    serialPortConfig config = serialPortConfig.getDefaultConfig();
            //    if (config != null)
            //    {
            //        sp.PortName = config.portName;
            //        sp.BaudRate = int.Parse(config.baudRate);
            //        sp.DataBits = int.Parse(config.dataBits);
            //        sp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), config.stopBits);
            //        sp.Parity = (Parity)Enum.Parse(typeof(Parity), config.parity);
            //    }
            //    //sp.PortName = ConfigManager.GetItemValue("PortName");
            //    //sp.BaudRate = int.Parse(ConfigManager.GetItemValue("BaudRate"));
            //    //sp.DataBits = int.Parse(ConfigManager.GetItemValue("DataBits"));
            //    //sp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), ConfigManager.GetItemValue("StopBits"));
            //    //sp.Parity = (Parity)Enum.Parse(typeof(Parity), ConfigManager.GetItemValue("Parity"));

            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show("配置文件出现错误！" + ex.Message);
            //}
        }
        /// <summary>
        /// 串口关闭时可能引起线程死锁，因此这里要求首先安全关闭串口
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        public static void resetStaticSerialPort(string portName, string baudRate, string parity, string dataBits, string stopBits)
        {
            SerialPort sp = StaticSerialPort.getStaticSerialPort();
            bool biniOpened = sp.IsOpen;
            if (biniOpened)
            {
                //sp.Close();
                MessageBox.Show("请先关闭串口！");
                return;
            }
            try
            {
                sp.PortName = portName;
                sp.BaudRate = int.Parse(baudRate);
                sp.DataBits = int.Parse(dataBits);
                sp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits,true);
                sp.Parity = (Parity)Enum.Parse(typeof(Parity), parity,true);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("设置串口时出现异常错误！" + ex.Message);
            }
        }
    }
}
