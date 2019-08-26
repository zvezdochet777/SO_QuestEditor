using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{
    public struct npc_data
    {
        public string rusName;
        public string engName;
        public string location;
        public string coordinates;

        public npc_data(string rn, string en, string loc, string cd)
        {
            rusName = rn;
            engName = en;
            location = loc;
            coordinates = cd;
        }
    }

    public class CManagerNPC
    {
        //! Словарь информации об NPC - локализованное имя, карта, координаты
        public Dictionary<string, npc_data> NpcData = new Dictionary<string, npc_data>();
        //! Словарь соответствия русское имя - общее имя в игре (для поиска в комбобоксе NPCBox)
        public Dictionary<string, string> rusNamesToNPC = new Dictionary<string, string>();
        //! Словарь cоответствия англ имя - общее имя в игре (для поиска в комбобоксе NPCBox)
        public Dictionary<string, string> engNamesToNPC = new Dictionary<string, string>();
        public Dictionary<string, List<string>> mapToNPCList = new Dictionary<string, List<string>>();

        //! Список всех локаций
        public List<string> locationNames = new List<string>();

        public CManagerNPC()
        {
            parseNpcLocationFile("npc_stat.xml");
        }

        public Dictionary<string, bool> getSpaces()
        {
            Dictionary<string, bool> data = new Dictionary<string, bool>();
            foreach(string spaceName in locationNames)
            {
                data.Add(spaceName, true);
            }
            data.Add("no map", true);
            return data;
        }
        
        //! Парсит файл с местонахождением NPC
        private void parseNpcLocationFile(string fileName)
        {
            if (!File.Exists(fileName))
                return;
            XDocument doc;
            try
            {
                doc = XDocument.Load(fileName);
            }
            catch
            {
                MessageBox.Show("Не могу прочитать файл npc_stat.xml", caption: "Ошибка чтения");
                return;
            }
            foreach (XElement item in doc.Root.Elements())
            {
                string name = item.Element("Name").Value.ToString();
                string map = item.Element("map").Value.ToString();
                string rusName = item.Element("npcLocal").Value.ToString();
                string engName = item.Element("npcEngName").Value.ToString();
                string coord = item.Element("coord").Value.ToString();
                if (!NpcData.ContainsKey(name))
                    NpcData.Add(name, new npc_data(rusName, engName, map, coord));
                if (!mapToNPCList.ContainsKey(map))
                    mapToNPCList.Add(map, new List<string>());
                mapToNPCList[map].Add(name);

                if (!locationNames.Contains(map))
                    locationNames.Add(map);
                if (!rusNamesToNPC.ContainsKey(rusName))
                    rusNamesToNPC.Add(rusName, name);
                if (!engNamesToNPC.ContainsKey(engName))
                    engNamesToNPC.Add(engName, name);

            }
        }

    }

    //! Класс объекта, хранящий данные о русском и английском имени NPC 
    public class NPCNameDataSourceObject : IComparable
    {
        //! Имя NPC по-английски
        private string value;
        //! Имя NPC по-русски
        private string displayString;

        public NPCNameDataSourceObject(string _value, string _display)
        {
            value = _value;
            displayString = _display;
        }

        public string DisplayString
        {
            get { return displayString; }
        }

        public string Value
        {
            get { return value; }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            NPCNameDataSourceObject otherNPCName = obj as NPCNameDataSourceObject;
            if (otherNPCName != null)
                return this.Value.CompareTo(otherNPCName.Value);
            else
                throw new ArgumentException("Object is not an NPC name");
        }
    };

}
