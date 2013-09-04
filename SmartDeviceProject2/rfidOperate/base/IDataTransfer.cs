using System;
using System.Collections.Generic;
using System.Text;

namespace RfidReader
{
    /// <summary>
    /// 通用的数据传输接口，无论是通过串口或者网络
    /// </summary>
    public interface IDataTransfer
    {
        void writeData(byte[] data);
        byte[] readData();
        void AddParser(deleVoid_Byte_Func parser);
        void removeParser(deleVoid_Byte_Func parser);
    }
 
}
