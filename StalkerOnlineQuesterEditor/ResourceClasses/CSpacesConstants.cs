using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    public class CSpacesConstants
    {
        Dictionary<string, CSpaceDescription> spaces;
        public CSpacesConstants()
        {
            spaces = new Dictionary<string, CSpaceDescription>();
            XDocument doc = XDocument.Load("source/Spaces.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                try
                {
                    spaces.Add(item.Element("name").Value.ToString(), new CSpaceDescription(item.Element("dir").Value.ToString()));
                }
                catch
                {
                    System.Console.WriteLine("Error with item id:" + item.Element("name").Value.ToString());
                }
            }
        }

        public List<string> getSpacesDescription()
        {
            List<string> ret = new List<string>();
            foreach (string key in this.spaces.Keys)
                ret.Add(key);
            return ret;
        }

        public string getNameOnDir(string sDir)
        {
            foreach (KeyValuePair<string, CSpaceDescription> element in this.spaces)
                if (element.Value.getDir().Equals(sDir))
                    return element.Key;
            return "";
        }
    }

    public class CSpaceDescription
    {
        public string sDir;
        public CSpaceDescription()
        {
            this.sDir = "";
        }

        public CSpaceDescription(string sDir)
        {
            this.sDir = sDir;
        }

        public string getDir()
        {
            return this.sDir;
        }

    }
}
