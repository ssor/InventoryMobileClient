using System;
using System.Collections.Generic;
using System.Text;

namespace RfidReader
{
    public class reader2600OperateAction : operateAction
    {
        public reader2600OperateAction()
        {
            this.nextCommand.Add("4006EE01000000CB");
            //this.nextCommand = RFIDHelper.RFIDCommand_RMU_GetStatus;
            this.nextCommandType = RFIDEventType.RMU_InventorySingle;
            this.invokeEvent = RFIDEventType.RMU_InventorySingle;
            this.exceptionMsg = "设备尚未准备就绪";
        }
        public override string getProcessedData(object inData)
        {
            string value = null;
            if (inData != null && (string)inData != "ok")
            {
                //value = Rmu900RFIDHelper.GetEPCFormUII((string)inData);
                value = (string)inData;
            }
            return value;
        }
    }
}
