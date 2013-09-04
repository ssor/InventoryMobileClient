using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Diagnostics;
using CommonSerialPort;

namespace RfidReader
{
    public class rfidOperateUnitBase : IRfidOperateUnit
    {
        deleRfidOperateCallback callback = null;
        public List<operateAction> actionList = new List<operateAction>();
        IRFIDHelper _RFIDHelper = null;
        //Rmu900RFIDHelper _RFIDHelper = null;
        int ActionIndex = 0;//标记执行到的Action的索引,从0开始
        bool bAutoRemoveParser = false;
        IDataTransfer dataTransfer = null;

        public rfidOperateUnitBase(IDataTransfer _dataTransfer, enumRFIDType type)
        {
            this.dataTransfer = _dataTransfer;
            switch ((int)type)
            {
                case (int)enumRFIDType.RMU900:
                    //_RFIDHelper = new Rmu900RFIDHelper();
                    break;
                case (int)enumRFIDType.RFID2600:
                    _RFIDHelper = new RFID2600Helper();
                    break;


            }
            //使得Helper类可以向串口中写入数据
            //_RFIDHelper.evtWriteToSerialPort += new deleVoid_Byte_Func(RFIDHelper_evtWriteToSerialPort);
            //_RFIDHelper.evtWriteToSerialPort = this.dataTransfer.writeData;
            _RFIDHelper.registerWriteDataFunc(this.dataTransfer.writeData);
            // 处理当前操作的状态
            //_RFIDHelper.evtCardState += new deleVoid_RFIDEventType_Object_Func(_RFIDHelper_evtCardState);
            _RFIDHelper.registerStateCallbackFunc(_RFIDHelper_evtCardState);
        }
        // 打开与外界的数据通道
        public void linkOpen()
        {
            if (this.dataTransfer != null)
            {
                this.dataTransfer.AddParser(_RFIDHelper.Parse);
            }
        }
        //关闭与外界的数据通道
        public void linkClose()
        {
            if (this.dataTransfer != null)
            {
                this.dataTransfer.removeParser(_RFIDHelper.Parse);
            }
        }

        void _RFIDHelper_evtCardState(RFIDEventType eventType, object o)
        {
            if ((this.ActionIndex + 1) <= actionList.Count)
            {
                operateAction action = actionList[ActionIndex];
                if (!action.bLoop)
                {
                    ActionIndex++;
                }
                if (eventType == action.invokeEvent)
                {
                    string value = null;
                    value = action.getProcessedData(o);
                    if (value != null)
                    {
                        if (null != this.callback)
                        {
                            this.callback(new operateMessage("success", value));
                        }
                    }
                    if ((ActionIndex <= (actionList.Count - 1)) && (action.nextCommandType != RFIDEventType.RMU_Unknown))
                    {
                        _RFIDHelper.SendCommand(action.nextCommand, action.nextCommandType, false);
                    }

                    //如果已经是最后一个action，选择是否自动关闭数据解析回调
                    if ((ActionIndex > (actionList.Count - 1)) && this.bAutoRemoveParser)
                    {
                        this.linkClose();
                    }
                }
                else
                {
                    //异常出现
                    this.clearException(eventType, o);
                }
            }
        }

        public void registeCallback(deleRfidOperateCallback callback)
        {
            this.callback = callback;
        }
        public void OperateStart(bool autoClose)
        {
            this.bAutoRemoveParser = autoClose;
            this.OperateStart();
        }
        public void OperateStart()
        {
            this.linkOpen();
            this.bInventoryStoped = false;
            ActionIndex = 0;
            if (actionList.Count > 0)
            {
                operateAction action = actionList[ActionIndex];

                if (!action.bLoop && actionList.Count > 1)
                {
                    ActionIndex++;
                }
                _RFIDHelper.SendCommand(action.nextCommand, action.nextCommandType, true);
            }
        }

        bool bInventoryStoped = false;
        /// <summary>
        /// 当读写器处于识别标签状态时，只能发送停止识别的命令才能被接收
        /// 因此如果接收到RMU_Exception事件，也可能是因为命令没有被接收
        /// 这里尝试发送 停止识别命令以确认这种情况
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="o"></param>
        public virtual void clearException(RFIDEventType eventType, object o)
        {
            if (eventType == RFIDEventType.RMU_Unknown)
            {
                return;
            }
            List<string> list = null;
            switch ((int)eventType)
            {
                case (int)RFIDEventType.RMU_Exception:
                    if (!bInventoryStoped)
                    {
                        bInventoryStoped = true;
                        list = new List<string>();
                        //list.Add(Rmu900RFIDHelper.RFIDCommand_RMU_StopGet);
                        _RFIDHelper.SendCommand(
                           list, RFIDEventType.RMU_StopGet, true);

                        //_RFIDHelper.SendCommand(Rmu900RFIDHelper.RFIDCommand_RMU_StopGet, RFIDEventType.RMU_StopGet,true);
                        break;
                    }
                    if (null != this.callback)
                    {
                        this.callback(new operateMessage("fail", "设备异常"));
                    }
                    break;
                case (int)RFIDEventType.RMU_Inventory:
                case (int)RFIDEventType.RMU_InventoryAnti:
                    list = new List<string>();
                    //list.Add(Rmu900RFIDHelper.RFIDCommand_RMU_StopGet);
                    _RFIDHelper.SendCommand(
                       list, RFIDEventType.RMU_StopGet, true);
                    this.OperateStart();
                    break;
                case (int)RFIDEventType.RMU_StopGet:
                    this.OperateStart();
                    break;
            }
        }
    }
}
