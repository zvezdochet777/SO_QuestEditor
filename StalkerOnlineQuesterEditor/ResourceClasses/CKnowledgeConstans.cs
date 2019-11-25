using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{
    public class CKnowledgeConstans
    {
        Dictionary<int, string> items;

        public CKnowledgeConstans()
        {
            items = new Dictionary<int, string>();
            if (!File.Exists("source/KnowledgeCategories.xml"))
            {
                System.Windows.Forms.MessageBox.Show("Отсуствует файл KnowledgeCategories.xml, нужно распарсить предметы", "Ошибка");
                return;
            }
            XDocument doc = XDocument.Load("source/KnowledgeCategories.xml");
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


}
