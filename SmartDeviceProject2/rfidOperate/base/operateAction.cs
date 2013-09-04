using System;
using System.Collections.Generic;
using System.Text;

namespace RfidReader
{
    /// <summary>
    /// 基础操作动作，比如读版本号，比如读写数据
    /// </summary>
    public class operateAction
    {
        //当接收到此事件时触发动作
        public RFIDEventType invokeEvent = RFIDEventType.RMU_Unknown;
        public List<string> nextCommand = new List<string>();
        public RFIDEventType nextCommandType = RFIDEventType.RMU_Unknown;
        public string exceptionMsg = string.Empty;
        public bool bLoop = false;//标识是否执行到该action时是循环执行，例如循环读取标签直至停止
        public operateAction()
        {

        }
        /* 
        
        public operateAction(RFIDEventType invokeEvent, string nextCommand, RFIDEventType nextCommandType, string exMsg)
        {
            this.invokeEvent = invokeEvent;
            this.nextCommand = nextCommand;
            this.nextCommandType = nextCommandType;
            this.exceptionMsg = exMsg;
        }
        */
        public virtual string getProcessedData(object inData)
        {
            //子类应重写此方法
            string value = null;
            return value;
        }
        public override string ToString()
        {
            return invokeEvent.ToString() + " " + nextCommandType.ToString() + " " + nextCommand;
        }
    }

}
