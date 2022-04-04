using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    public static class CAchivements
    {
        static string PATH = "../../../res/scripts/common/achivments/achivements.pyson";
        static string LOCAL_PATH = CSettings.pathToLocalFiles + CSettings.ORIGINAL_PATH + "/Achivements.xml";
        static Dictionary<int, string> achivements = new Dictionary<int, string>();

        public static void load()
        {
            if (!File.Exists(PATH))
                return;

            string line;
            StreamReader reader = new StreamReader(PATH);
            int id = 0;
            string name = "";



            while ((line = reader.ReadLine()) != null)
            {

                if (!line.Any()) continue;
                if (line.Contains("		\"id\":") && !line.Contains("			"))
                {
                    id = Convert.ToInt32(line.Replace("		\"id\":", "").Replace(',', ' '));
                    achivements.Add(id, name);
                    continue;
                }
                name = "";     
            }
            reader.Close();


            XDocument doc = XDocument.Load(LOCAL_PATH);
            foreach (XElement node in doc.Root.Element("name").Elements())
            {
                id = Convert.ToInt32(node.Name.ToString().Replace("id", ""));
                name = node.Value.ToString();
                if (achivements.ContainsKey(id))
                    achivements[id] = name;
            }
        }

        public static int getIDByName(string name)
        {
            foreach (KeyValuePair<int, string> pair in achivements)
            {
                if (pair.Value == name) return pair.Key;
            }
            return 0;
        }

        public static string getNameByID(int frac_id)
        {
            foreach (KeyValuePair<int, string> pair in achivements)
            {
                if (pair.Key == frac_id) return pair.Value;
            }
            return "";
        }

        public static string[] getListNames()
        {
            return achivements.Values.ToArray();
        }
    }
}
