using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    public class CZoneConstatnts
    {
        Dictionary<string, CZoneDescription> zones;

        public CZoneConstatnts()
        {
                zones = new Dictionary<string, CZoneDescription>();
                XDocument doc = XDocument.Load("source/Areas.xml");
                foreach (XElement item in doc.Root.Elements())
                    zones.Add(item.Element("mark").Value.ToString().Trim(), new CZoneDescription(item.Element("description").Value.ToString().Trim()));


/*            zones = new Dictionary<string, CZoneDescription>();
            zones.Add("abandoned_house", new CZoneDescription("Заброшенный дом."));
            zones.Add("swamp_house", new CZoneDescription("Дом на болоте."));
            zones.Add("chemical_cemetery", new CZoneDescription("Кладбище химических отходов."));
            zones.Add("swamp_exit", new CZoneDescription("Выход с болота."));
            zones.Add("road_exit", new CZoneDescription("Выход с дороги."));
            zones.Add("trainstation", new CZoneDescription("Железнодорожная станция."));
            zones.Add("stadium", new CZoneDescription("Стадион."));
            zones.Add("new_district", new CZoneDescription("Новый район."));
            zones.Add("kpp", new CZoneDescription("КПП."));
            zones.Add("safety_zone3", new CZoneDescription("Безопасная зона 3."));
            zones.Add("barrels1", new CZoneDescription("barrels1"));
            zones.Add("barrels2", new CZoneDescription("barrels2"));
            zones.Add("psycho_clinic", new CZoneDescription("Псих. клиника."));
            zones.Add("sanatorium", new CZoneDescription("Санаторий."));
            zones.Add("barrels3", new CZoneDescription("barrels3"));
            zones.Add("swamp", new CZoneDescription("Болота."));
            zones.Add("Ryabinushka", new CZoneDescription("Рябинушка."));*/
        }

        public Dictionary<string, CZoneDescription> getAllZones()
        {
            return zones;
        }

        public CZoneDescription getDescriptionOnKey(string key)
        {
            System.Console.WriteLine("key:" + key);
            return zones[key.Trim()];
        }

        public string getKeyOnDescription(string description)
        {
            foreach (string key in zones.Keys)
                if (zones[key].getName().Equals(description))
                    return key;
            return "";
        }
    }

    public class CZoneDescription
    {
        string Name;

        public CZoneDescription(string name)
        {
            this.Name = name;
        }

        public string getName()
        {
            return this.Name;
        }
    }
}
