using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    public class CItemConstants
    {
        Dictionary<int, CItem> items;

        public CItemConstants()
        {
            items = new Dictionary<int, CItem>();
            XDocument doc = XDocument.Load("source/ItemStrings.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                try
                {
                    items.Add(int.Parse(item.Element("id").Value.ToString()), new CItem(item.Element("Name").Value.ToString()));
                }
                catch
                {
                    System.Console.WriteLine("Error with item id:" + item.Element("id").Value.ToString());
                }
            }

        }


            public string getDescriptionOnID(int typeID)
            {
                return items[typeID].getDescription();
            }


            public Dictionary<int, CItem> getAllItems()
            {
                return items;
            }

            public int getIDOnDescription(string description)
            {
                foreach (int key in items.Keys)
                    if (items[key].getDescription().Equals(description))
                        return key;
                return 1000000;
            }
    }


    public class CItem
    {
        public string Description;
        public CItem(string Description)
        {
            this.Description = Description;
        }
        public string getDescription()
        {
            return Description;
        }
    }
}
