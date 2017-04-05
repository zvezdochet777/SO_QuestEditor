using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{
    //! Класс, содержащий данные о территориях, которые нужно посетить по квестам
    public class CZoneConstants
    {
        //! Словарь ID территории (mark в xml файле) - Имя территории (по-русски, для геймдевов)
        protected Dictionary<string, CZoneDescription> zones;

        //! Конструктор, создает словарь на основе xml файлов areas и AllAreas
        public CZoneConstants()
        {
            zones = new Dictionary<string, CZoneDescription>();
            XDocument doc = XDocument.Load("source/Areas.xml");
            foreach (XElement item in doc.Root.Elements())            
            {
                string id = item.Element("mark").Value.ToString().Trim();
                string name = item.Element("description").Value.ToString().Trim();

                zones.Add(id, new CZoneDescription(name));
            }

            // добавление неназванных зон из AllAreas.xml - создается парсером по всем chunk
            XDocument allAreas = XDocument.Load("source/AllAreas.xml");
            foreach(XElement item in allAreas.Root.Elements())
            {
                string id = item.Element("mark").Value.ToString().Trim();
                List<int> quests = new List<int>();
                string[] q;
                if (item.Element("quests") != null)
                {
                    q = item.Element("quests").Value.ToString().Trim().Split(' ');
                    foreach (string i in q)
                    {
                        if (i != "0")
                            quests.Add(Convert.ToInt32(i));
                    }
                }
                if (!zones.ContainsKey(id))
                    zones.Add(id, new CZoneDescription(id, quests));
                else { zones[id].setQuests(quests); }

            }

        }

        public bool checkAreaGiveQuestByID(int quest_id)
        {
            foreach (CZoneDescription area in zones.Values)
            {
                List<int> area_quest;
                area_quest = area.getQuests();
                if (area_quest == null) continue;
                if (area_quest.Contains(quest_id))
                    return true;
            }
            return false;
        }

        public bool checkHaveArea(string key)
        {
            return zones.ContainsKey(key.Trim());
        }

        //! Возвращает словарь всех территорий
        public Dictionary<string, CZoneDescription> getAllZones()
        {
            return zones;
        }
        //! Возвращает описание территории по-русски по ее ключу mark
        public CZoneDescription getDescriptionOnKey(string key)
        {
            //System.Console.WriteLine("key:" + key);
            return zones[key.Trim()];
        }
        //! Возвращает ключ по описанию территории
        public string getKeyOnDescription(string description)
        {
            foreach (string key in zones.Keys)
                if (zones[key].getName().Equals(description))
                    return key;
            return "";
        }
    }

    public class CZoneMobConstants : CZoneConstants
    {
        public CZoneMobConstants()
        {
            zones = new Dictionary<string, CZoneDescription>();
            if (!File.Exists("source/MobAreas.xml"))
                return;

            XDocument mobAreas = XDocument.Load("source/MobAreas.xml");
            foreach (XElement item in mobAreas.Root.Elements())
            {
                string id = item.Element("mark").Value.ToString().Trim();
                List<int> quests = new List<int>();
                string[] q;
                if (item.Element("quests") != null)
                {
                    q = item.Element("quests").Value.ToString().Trim().Split(' ');
                    foreach (string i in q)
                    {
                        if (i != "0")
                            quests.Add(Convert.ToInt32(i));
                    }
                }
                if (!zones.ContainsKey(id))
                    zones.Add(id, new CZoneDescription(id, quests));
                else { zones[id].setQuests(quests); }

            }

        }
    }
    //! Класс описания территории для посещения. Имеет только поле имя. Что за пиздец???
    public class CZoneDescription
    {
        string Name;
        List<int> quests;

        public CZoneDescription(string name, List<int> quests = null)
        {
            this.Name = name;
            this.quests = quests;
        }

        public string getName()
        {
            return this.Name;
        }

        public List<int> getQuests()
        {
            return quests;
        }

        public void setQuests(List<int> value)
        {
            quests = value;
        }
    }
}
