using System;
using System.Collections.Generic;
using System.Text;

namespace RfidReader
{
    public enum RFIDEventType
    {
        RMU_Unknown = 0
      ,
        RMU_CardIsReady = 1
            ,
        RMU_GetPower
            ,
        RMU_SetPower
            ,
        RMU_GetFrequency
            ,
        RMU_SetFrequency
            ,
        RMU_Inventory
            ,
        RMU_InventoryAnti
            ,
        RMU_StopGet
            ,
        RMU_ReadData
            ,
        RMU_WriteData
            ,
        RMU_EraseData
            ,
        RMU_LockMem
            ,
        RMU_KillTag
            ,
        RMU_InventorySingle
            ,
        RMU_WeigandInvetory
            ,
        RMU_SingleReadData
            ,
        RMU_SingleWriteData//17
            ,
        RMU_Exception


            , WriteToSerialPort


    }
}
