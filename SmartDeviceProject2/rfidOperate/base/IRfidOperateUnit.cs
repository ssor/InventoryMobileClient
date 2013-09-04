using System;
using System.Collections.Generic;
using System.Text;

namespace RfidReader
{
    public delegate void deleRfidOperateCallback(object o);

    public interface IRfidOperateUnit
    {
        void registeCallback(deleRfidOperateCallback callback);
        void OperateStart();
    }
}
