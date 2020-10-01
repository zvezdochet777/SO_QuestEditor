using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    public static class CPVPConstans
    {
        static Dictionary<int, string> _modes = new Dictionary<int, string>();

        public static void Load()
        {
            XDocument doc_modes = XDocument.Load("source/PVP_modes.xml");
            foreach (XElement item in doc_modes.Root.Elements())
            {
                int id = Convert.ToInt32(item.Element("id").Value.ToString().Trim());
                string name = item.Element("name").Value.ToString().Trim();
                _modes.Add(id, name);

            }
        }

        public static List<string> getAllDescriptions()
        {
            return _modes.Values.ToList();
        }

        public static string getPVPModeNameByID(int id)
        {
            if (_modes.ContainsKey(id))
                return _modes[id];
            return "нет";
        }

        public static int getPVPModeIDByName(string name)
        {
            foreach (int key in _modes.Keys)
                if (_modes[key].Equals(name))
                    return key;
            return -1;
        }

    }


    public class PvPRanks : Constants
    {

        public PvPRanks()
        {
            string path = "../../../res/scripts/common/pvp_rating_config.py";

            string name = "";
            string rating = "0";

            string local_name_path = "../../../res/local/Russian/pvp_rating_config.xml";
            Dictionary<string, string> names = new Dictionary<string, string>();
            doc = XDocument.Load(local_name_path);
            foreach (XElement item in doc.Root.Elements("rank").Elements())
            {
                names.Add(item.Name.ToString(), item.Value);
            }

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("\"name\":"))
                    {
                        name = line.Split(':')[1].Split(',')[0].Replace("\"", "");
                        name = names[name.Split('.').Last()];
                    }
                    else if (line.Contains("\"rating\":"))
                    {
                        rating = line.Split(':')[1].Split(',')[0].Replace("\"", "");
                        _constants.Add(name, rating);
                    }
                }
            }
        }
    }
}
