using System;
using System.Collections.Generic;
using System.Text;

namespace RfidReader
{
    public delegate void deleVoid_Byte_Func(byte[] value);
    public delegate void deleVoid_RFIDEventType_Object_Func(RFIDEventType eventType, object o);
    public interface IRFIDHelper
    {
        void SendCommand(List<string> commands, RFIDEventType type, bool bReturnInstance);
        void Parse(byte[] value);
        void registerWriteDataFunc(deleVoid_Byte_Func func);
        void registerStateCallbackFunc(deleVoid_RFIDEventType_Object_Func func);
    }
}
