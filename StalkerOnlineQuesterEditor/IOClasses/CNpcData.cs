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
        //! Список всех локаций
        public List<string> locationNames = new List<string>();

        public CManagerNPC()
        {
            parseNpcLocationFile("npc_stat.xml");
        }
        
        //! Парсит файл с местонахождением NPC
        private void parseNpcLocationFile(string fileName)
        {
            if (!File.Exists(fileName))
                return;

            XDocument doc = XDocument.Load(fileName);
            foreach (XElement item in doc.Root.Elements())
            {
                string name = item.Element("Name").Value.ToString();
                string map = item.Element("map").Value.ToString();
                string rusName = item.Element("npcLocal").Value.ToString();
                string engName = item.Element("npcEngName").Value.ToString();
                string coord = item.Element("chunk").Value.ToString();
                if (!NpcData.ContainsKey(name))
                    NpcData.Add(name, new npc_data(rusName, engName, map, coord));
                if (!locationNames.Contains(map))
                    locationNames.Add(map);
                if (!rusNamesToNPC.ContainsKey(rusName))
                    rusNamesToNPC.Add(rusName, name);
                if (!engNamesToNPC.ContainsKey(engName))
                    engNamesToNPC.Add(engName, name);
            }
        }

    }

}
