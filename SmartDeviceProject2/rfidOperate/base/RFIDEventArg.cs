using System;
using System.Collections.Generic;
using System.Text;

namespace RfidReader
{
    public class RFIDEventArg
    {
        public object _arg;
        public RFIDEventType _type;
        public RFIDEventArg(RFIDEventType type, object arg)
        {
            this._type = type;
            this._arg = arg;
        }
    }
}
