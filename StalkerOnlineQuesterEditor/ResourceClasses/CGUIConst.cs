using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StalkerOnlineQuesterEditor
{
    public class CGUIConst
    {
        public Dictionary<string, int> guiIDs = new Dictionary<string, int>();


        public CGUIConst()
        {
            guiIDs.Add("Журнал квестов", 16);
            guiIDs.Add("Инвентарь", 6);
            guiIDs.Add("Снаряжение", 10);
            guiIDs.Add("Карта", 12);
            guiIDs.Add("КПК", 11);
            guiIDs.Add("Хранилище", 19);
        }

        public int getIDOnDescription(string descr)
        {
            foreach (KeyValuePair<string, int> pair in guiIDs)
            {
                if (pair.Key == descr)
                    return pair.Value;
            }
            return 0;
        }

        public string getDescriptionOnID(int id)
        {
            foreach (KeyValuePair<string, int> pair in guiIDs)
            {
                if (pair.Value == id)
                    return pair.Key;
            }
            return "";
        }
    }
}
