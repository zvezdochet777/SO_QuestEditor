using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    public class CMobConstants
    {
        Dictionary<int, CMobDescription> mobs;

        public CMobConstants()
        {
            mobs = new Dictionary<int, CMobDescription>();

            XDocument doc = XDocument.Load("source/Mobs.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                try
                {
                    int iType = int.Parse(item.Element("id").Value.ToString());
                    string sDescription = item.Element("Name").Value.ToString();
                    List<int> lLevels = new List<int>();
                    
                    foreach(string sLevel in item.Element("Levels").Value.ToString().Split(','))
                        lLevels.Add(int.Parse(sLevel));

                    CMobDescription mobDescr = new CMobDescription(item.Element("Name").Value.ToString(), lLevels);
                    mobs.Add(iType, mobDescr);
                }
                catch
                {
                    System.Console.WriteLine("Error with item id:" + item.Element("id").Value.ToString());
                }
            }
        }

        public Dictionary<int, CMobDescription> getAllDescriptions()
        {
            return mobs;
        }

        public CMobDescription getDescriptionOnType(int type)
        {
            return mobs[type];
        }

        public int getTypeOnDescription(string description)
        {
            foreach (int key in mobs.Keys)
                if (mobs[key].getName().Equals(description))
                    return key;
            return 1000;
        }

        public List<int> getLevelsOnDescription(string description)
        {
            foreach (int key in mobs.Keys)
                if (mobs[key].getName().Equals(description))
                    return mobs[key].getLevels();
            return null;
        }
    }

    public class CMobDescription
    {
        string Name;
        List<int> iLevels;

        public CMobDescription(string name, List<int> iLevels)
        {
            this.Name = name;
            this.iLevels = iLevels;
        }

        public string getName()
        {
            return this.Name;
        }
        public List<int> getLevels()
        {
            return this.iLevels;
        }

        public int getLevelOnIndex(int index)
        {
            if (index - 1 < 0)
                return 0;
            else
                return this.iLevels[index - 1];
        }

        public int getIndexOnLevel(int ilevel)
        {

                if (iLevels.Contains(ilevel))
                    return this.iLevels.IndexOf(ilevel) + 1;
                else
                    return 0;
        }

    }
}
