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
            {
                try
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
            if (typeID == -1) return "";
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

        public virtual string get_file_path()
        {
            return "source/ItemStrings.xml";
        }

        public CItemConstants()
        {
            items = new Dictionary<int, CItem>();
            if (!File.Exists(get_file_path()))
            {
                System.Windows.Forms.MessageBox.Show("Отсуствует файл ItemStrings.xml, нужно распарсить предметы", "Ошибка");
                return;
            }
            XDocument doc = XDocument.Load(get_file_path());
            foreach (XElement item in doc.Root.Elements())
            {
                bool deleted = false, converted = false, is_ingredient = false;
                if (item.Element("deleted") != null)
                    deleted = true;
                if (item.Element("converted") != null)
                    converted = true;
                if (item.Element("is_ingredient") != null)
                    is_ingredient = true;
                try
                {
                    int type_id = int.Parse(item.Element("id").Value.ToString());
                    items.Add(type_id, new CItem(type_id.ToString() + " " + item.Element("Name").Value.ToString() , item.Element("Description").Value.ToString(), deleted, converted, is_ingredient));
                }
                catch
                {
                    System.Console.WriteLine("Error with item id:" + item.Element("id").Value.ToString());
                }


            }

        }

        public string getItemName(int typeID)
        {
            if (!items.ContainsKey(typeID))
            {
                System.Windows.Forms.MessageBox.Show("Предмета " + typeID.ToString() + " не существует", "Ошибка предмета");
                return "";
            }
            return items[typeID].getName();
        }

        public Dictionary<int, CItem> getCookItems()
        {
            Dictionary<int, CItem> result = new Dictionary<int, CItem>();
            foreach (var i in items)
            {
                if (i.Value.is_ingredient) result.Add(i.Key, i.Value);
            }
            return result;

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
            return 0;
        }

        public int getIDOnName(string name)
        {
            foreach (int key in items.Keys)
                if (items[key].getName().Equals(name))
                    return key;
            return 0;
        }
    }


    public class CItemRecipes:CItemConstants
        {
        public override string get_file_path()
        {
            return "source/ItemRecipes.xml";
        }
    }

    public class CItem
    {
        public string Name;
        public string Description;
        public bool deleted;
        public bool converted;
        public bool is_ingredient;

        public CItem(string Name, string Description, bool deleted = false, bool converted = false, bool is_ingredient = false)
        {
            this.Name = Name;
            this.Description = Description;
            this.deleted = deleted;
            this.converted = converted;
            this.is_ingredient = is_ingredient;
        }
        public string getDescription()
        {
            return Description;
        }

        public string getName()
        {
            return Name;
        }
    }
}
