using System;
using System.Collections.Generic;
using System.Text;

namespace RfidReader
{
    public class operateActionRmu900CheckReady : operateAction
    {
        public operateActionRmu900CheckReady()
        {
            this.nextCommand.Add(Rmu900RFIDHelper.RFIDCommand_RMU_GetStatus);
            //this.nextCommand = RFIDHelper.RFIDCommand_RMU_GetStatus;
            this.nextCommandType = RFIDEventType.RMU_CardIsReady;
            this.exceptionMsg = "设备尚未准备就绪";
        }
    }
    public class operateActionRmu900InventoryAnti : operateAction
    {
        public operateActionRmu900InventoryAnti()
        {
            this.invokeEvent = RFIDEventType.RMU_CardIsReady;
            this.nextCommand.Add(Rmu900RFIDHelper.RFIDCommand_RMU_InventoryAnti3);
            //this.nextCommand = RFIDHelper.RFIDCommand_RMU_InventoryAnti3;
            this.nextCommandType = RFIDEventType.RMU_InventoryAnti;
            this.exceptionMsg = "读取标签异常";
        }
    }
    public class operateActionRmu900InventoryAntiNoNextCommand : operateAction
    {
        public operateActionRmu900InventoryAntiNoNextCommand()
        {
            this.invokeEvent = RFIDEventType.RMU_InventoryAnti;
        }
    }
    public class operateActionRmu900InventoryAntiNoStopGet : operateAction
    {
        public operateActionRmu900InventoryAntiNoStopGet()
        {
            this.invokeEvent = RFIDEventType.RMU_InventoryAnti;
        }
        public override string getProcessedData(object inData)
        {
            string value = null;
            if (inData != null && (string)inData != "ok")
            {
                value = Rmu900RFIDHelper.GetEPCFormUII((string)inData);
            }
            return value;
        }
    }
    public class operateActionRmu900JustStopGet : operateAction
    {
        public operateActionRmu900JustStopGet()
        {
            this.invokeEvent = RFIDEventType.RMU_InventoryAnti;
            this.nextCommand.Add(Rmu900RFIDHelper.RFIDCommand_RMU_StopGet);
            //this.nextCommand = RFIDHelper.RFIDCommand_RMU_StopGet;
            this.nextCommandType = RFIDEventType.RMU_StopGet;
            this.exceptionMsg = "读取标签异常";
        }
    }
    public class operateActionRmu900StopGet : operateAction
    {
        public operateActionRmu900StopGet()
        {
            this.invokeEvent = RFIDEventType.RMU_InventoryAnti;
            this.nextCommand.Add(Rmu900RFIDHelper.RFIDCommand_RMU_StopGet);
            //this.nextCommand = RFIDHelper.RFIDCommand_RMU_StopGet;
            this.nextCommandType = RFIDEventType.RMU_StopGet;
            this.exceptionMsg = "读取标签异常";
        }
        public override string getProcessedData(object inData)
        {
            string value = null;
            if (inData != null && (string)inData != "ok")
            {
                value = Rmu900RFIDHelper.GetEPCFormUII((string)inData);
            }
            return value;
        }
    }
    public class operateActionRmu900InventoryWriteEpc : operateAction
    {
        string epc = string.Empty;
        public void setEPC(string epc)
        {
            this.epc = epc;
            this.nextCommand =
                Rmu900RFIDHelper.RmuWriteDataCommandCompose(RMU_CommandType.RMU_SingleWriteData,
                                                                                null,
                                                                                1,
                                                                                2,
                                                                                this.epc,
                                                                                null);
            //这里因为不需要在命令里面注明要写入数据的标签的UII，因此可以把所有写入命令
            //放到一个字符串里面一次写入
            string strCommand = string.Empty;
            foreach (string s in this.nextCommand)
            {
                strCommand += s;
            }
            this.nextCommand.Clear();
            this.nextCommand.Add(strCommand);
        }
        public operateActionRmu900InventoryWriteEpc()
            : this(string.Empty)
        {

        }
        public operateActionRmu900InventoryWriteEpc(string epc)
        {
            this.epc = epc;
            this.invokeEvent = RFIDEventType.RMU_CardIsReady;
            this.nextCommand =
                Rmu900RFIDHelper.RmuWriteDataCommandCompose(RMU_CommandType.RMU_SingleWriteData,
                                                                                null,
                                                                                1,
                                                                                2,
                                                                                this.epc,
                                                                                null);
            //这里因为不需要在命令里面注明要写入数据的标签的UII，因此可以把所有写入命令
            //放到一个字符串里面一次写入
            string strCommand = string.Empty;
            foreach (string s in this.nextCommand)
            {
                strCommand += s;
            }
            this.nextCommand.Clear();
            this.nextCommand.Add(strCommand);
            this.nextCommandType = RFIDEventType.RMU_SingleWriteData;
            this.exceptionMsg = "写入标签异常";
        }
    }

}
