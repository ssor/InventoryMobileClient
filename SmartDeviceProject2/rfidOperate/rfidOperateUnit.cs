using System;
using System.Collections.Generic;
using System.Text;

namespace RfidReader
{
    public class rfidOperateUnit2600InventoryTag : rfidOperateUnitBase
    {
        public rfidOperateUnit2600InventoryTag(IDataTransfer _dataTransfer)
            : base(_dataTransfer,enumRFIDType.RFID2600)
        {
            reader2600OperateAction op = new reader2600OperateAction();
            //op.bLoop = true;
            this.actionList.Add(op);
        }

    }
 
}
