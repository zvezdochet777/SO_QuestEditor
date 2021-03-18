using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    public class CTriggerConstants
    {
        Dictionary<string, CTriggerDescription> triggers;
        public CTriggerConstants()
        {
            triggers = new Dictionary<string, CTriggerDescription>();
            XDocument doc = XDocument.Load("source/Triggers.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                try
                {
                    int triggerID = int.Parse(item.Element("id").Value.ToString());
                    triggers.Add(item.Element("name").Value.ToString() + " " + triggerID.ToString(), new CTriggerDescription(triggerID));
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Error with trigger id:" + item.Element("name").Value.ToString() + ". " + e.Message);
                }
            }
        }

        public List<string> getTriggersDescription()
        {
            List<string> ret = new List<string>();
            foreach (string key in this.triggers.Keys)
                ret.Add(key);
            return ret;
        }

        public int getIdOnKey(string key)
        {
            if (this.triggers.Keys.Contains(key))
                return this.triggers[key].getID();
            else
                return 0;
        }

        public string getDescriptionOnId(int id)
        {
            foreach (KeyValuePair<string, CTriggerDescription> element in this.triggers)
                if (element.Value.getID().Equals(id))
                    return element.Key;
            return "";
        }
             
    }

    public class CTriggerDescription
    {
        public int id;
        public CTriggerDescription()
        {
            this.id = 0;
        }

        public CTriggerDescription(int id)
        {
            this.id = id;
        }

        public int getID()
        {
            return this.id;
        }
    }

}

