using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{
    public class CTPConstants
    {
        Dictionary<string, string> tp = new Dictionary<string, string>();
        XDocument doc = new XDocument();

        public CTPConstants()
        {
            doc = XDocument.Load("source/TPoints.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                string tpID = item.Element("id").Value;
                string name = item.Element("name").Value;

                tp.Add(name, tpID);
            }
        }

        public string getTtID(string name)
        {
            return tp[name];
        }

        public string getName(string tpID)
        {
            string ret = "";
            foreach (KeyValuePair<string, string> value in tp)
                if (value.Value.Equals(tpID))
                    ret = value.Key;
            return ret;
        }

        public List<string> getKeys()
        {
            //System.Console.WriteLine("getKeys");
            List<string> ret = new List<string>();
            foreach (string key in tp.Keys)
            {
                //System.Console.WriteLine(key);
                ret.Add(key);
            }
            return ret;
        }
    }
}
