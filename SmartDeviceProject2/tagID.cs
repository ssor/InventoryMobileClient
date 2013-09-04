using System;
using System.Collections.Generic;
using System.Text;

namespace invokePhpRestDemo
{
    public class InventoryCommand
    {
        public static string 扫描入库 = "inventory_input";
        public static string 扫描出库 = "inventory_output";
        public static string 盘点 = "inventory_pandian";
    }
    public class tagID
    {
        public string tag = string.Empty;
        public string startTime = string.Empty;
        public string cmd = string.Empty;
        public string state;
        public tagID() { }
        //public tagID(string _tag, string _time)
        public tagID(string _tag, string _time,string _cmd)
        {
            this.tag = _tag;
            this.startTime = _time;
            this.cmd = _cmd;
            this.state = string.Empty;
        }
        public tagID(string _tag, string _time, string _cmd, string _state)
        {
            this.tag = _tag;
            this.startTime = _time;
            this.cmd = _cmd;
            this.state = _state;
        }
        public string toJSON()
        {
            string json = string.Empty;
            json = "{\"tag\":\"" + this.tag + "\",\"startTime\":\"" + this.startTime + "\",\"cmd\":\"" + this.cmd + "\",\"state\":\"\"}";
            return json;
        }
        public static tagID createInstance(Dictionary<string, object> dic)
        {
            string tag = string.Empty;
            string startTime = string.Empty;
            string cmd = string.Empty;
            string state = string.Empty;
            object tmpTag = null;
            object tmpTime = null;
            object tmpCmd = null;
            object tmpState = null;
            if (dic.TryGetValue("tag", out tmpTag) == true)
            {
                tag = (string)tmpTag;
            }
            if (dic.TryGetValue("startTime", out tmpTime) == true)
            {

                startTime = (string)tmpTime;

            }
            if (dic.TryGetValue("cmd", out tmpCmd) == true)
            {
                cmd = (string)tmpCmd;
            }
            if (dic.TryGetValue("state", out tmpState) == true)
            {
                state = (string)tmpState;
            }
            tagID u = new tagID(tag, startTime, cmd, state);
            return u;
        }
        public static string toJSONFromList(List<tagID> list)
        {
            if (list == null || list.Count <= 0)
            {
                return "[]";
            }
            string strR = "[";
            for (int i = 0; i < list.Count; i++)
            {
                tagID t = list[i];
                if (i > 0)
                {
                    strR += "," + t.toJSON();
                }
                else
                {
                    strR += t.toJSON();
                }
            }
            strR += "]";
            return strR;
        }
    }
    public class scanTagPara
    {
        public string cmd = string.Empty;
        public string startTime = string.Empty;
        public scanTagPara(string _cmd, string _startTime)
        {
            this.cmd = _cmd;
            this.startTime = _startTime;
        }
    }

}
