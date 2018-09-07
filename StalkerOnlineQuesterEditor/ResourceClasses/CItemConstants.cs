using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{

    public class CItemCategories
    {
        Dictionary<int, string> items;

        public CItemCategories()
        {
            items = new Dictionary<int, string>();
            if (!File.Exists("source/ItemCategories.xml"))
            {
                System.Windows.Forms.MessageBox.Show("Отсуствует файл ItemCategories.xml, нужно распарсить предметы", "Ошибка");
                return;
            }
            XDocument doc = XDocument.Load("source/ItemCategories.xml");
            foreach (XElement item in doc.Root.Elements())
            {                try
                {
                    items.Add(int.Parse(item.Element("id").Value.ToString()), item.Element("Name").Value.ToString());
                }
                catch
                {
                    System.Console.WriteLine("Error with item category:" + item.Element("id").Value.ToString());
                }
            }

        }
        public string getNameOnID(int typeID)
        {
            return items[typeID];
        }

        public Dictionary<int, string> getAllItems()
        {
            return items;
        }

        public int getID(string name)
        {
            foreach (int key in items.Keys)
                if (items[key].Equals(name))
                    return key;
            return -1;
        }
    }
    public class CItemConstants
    {
        Dictionary<int, CItem> items;

        public CItemConstants()
        {
            items = new Dictionary<int, CItem>();
            if (!File.Exists("source/ItemStrings.xml"))
            {
                System.Windows.Forms.MessageBox.Show("Отсуствует файл ItemStrings.xml, нужно распарсить предметы", "Ошибка");
                return;
            }
            XDocument doc = XDocument.Load("source/ItemStrings.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                bool deleted = false, converted = false;
                if (item.Element("deleted") != null)
                    deleted = true;
                if (item.Element("converted") != null)
                    converted = true;
                try
                {
                    int type_id = int.Parse(item.Element("id").Value.ToString());
                    items.Add(type_id, new CItem(type_id.ToString() + " " + item.Element("Name").Value.ToString(), deleted, converted));
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
        public bool deleted;
        public bool converted;

        public CItem(string Description, bool deleted = false, bool converted = false)
        {
            this.Description = Description;
            this.deleted = deleted;
            this.converted = converted;
        }
        public string getDescription()
        {
            return Description;
        }
    }
}
