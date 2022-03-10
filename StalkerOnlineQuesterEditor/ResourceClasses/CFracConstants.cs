using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    public class CFracConstants
    {
        protected Dictionary<int, string> fractions;

        public CFracConstants()
        {
            this.fractions = new Dictionary<int, string>();
            parseFractions("source/fractions.xml");
        }

        void parseFractions(string path)
        {
            XDocument doc = XDocument.Load(path);
            foreach (XElement item in doc.Root.Elements())
            {
                int id = int.Parse(item.Element("id").Value);
                string name = item.Element("name").Value.ToString();
                fractions.Add(id, name);
            }
        }

        public int getFractionIDByDescr(string frac_name)
        {
            foreach (KeyValuePair<int, string> pair in this.fractions)
            {
                if (pair.Value == frac_name) return pair.Key;
            }
            return 0;
        }

        public string getFractionDesctByID(int frac_id)
        {
            foreach (KeyValuePair<int, string> pair in this.fractions)
            {
                if (pair.Key == frac_id) return pair.Value;
            }
            return "нет";
        }

        public Dictionary<int, string> getListOfFractions()
        {
            return this.fractions;
        }

        public int genLenListOfFractions()
        {
            return this.fractions.Count;
        }

    }

    public class CFracConstants2: CFracConstants
    {
        public CFracConstants2()
        {
            this.fractions = new Dictionary<int, string>();
            parseFractions("source/fraction_groups.json");
        }

        void parseFractions(string path)
        {
            JsonTextReader reader = new JsonTextReader(new StreamReader(path, Encoding.UTF8));
            string name = "";
            int id = 0;
            bool inName = false;

            while (reader.Read())
            {
                if ((reader.TokenType == JsonToken.EndArray) && (id != 0)) 
                {
                    fractions.Add(id, name);
                    id = 0;
                    continue;
                }
                if (reader.TokenType == JsonToken.Integer)
                   id = Convert.ToInt32(reader.Value);
                else if (reader.TokenType == JsonToken.String)
                    name = reader.Value.ToString();

            }
            
            reader.Close();
        }

        public Dictionary<int, bool> getFractions2()
        {
            Dictionary<int, bool> result = new Dictionary<int, bool>();
            result.Add(0, true);
            foreach (var i in fractions.Keys)
            {
                result.Add(i, true);
            }
            return result;
        }
            
    }


    public static class CFracBonuses
    {
        static string PATH = "../../../res/scripts/common/rep_groups/events_data.pyson";
        static Dictionary<int, string> bonuses = new Dictionary<int, string>();

        public static void readBonuses()
        {
            if (!File.Exists(PATH))
                return;

            string line;
            StreamReader reader = new StreamReader(PATH);
            int id = 0;
            string name = "";

            bonuses.Add(id, "нет");

            while ((line = reader.ReadLine()) != null)
            {

                if (!line.Any()) continue;
                if (line.Contains("\"id\" :"))
                {
                    id = Convert.ToInt32(line.Replace("\"id\" :", "").Replace(',', ' '));
                    continue;
                }
                if (line.Contains("\"name\" :"))
                {
                    name = line.Replace("\"name\" :", "").Replace(',', ' ').Replace('"', ' ').Replace('u', ' ').Trim();
                    bonuses.Add(id, name);
                }
            }
            reader.Close();
        }

        public static int getIDByName(string name)
        {
            foreach (KeyValuePair<int, string> pair in bonuses)
            {
                if (pair.Value == name) return pair.Key;
            }
            return 0;
        }

        public static string getNameByID(int frac_id)
        {
            foreach (KeyValuePair<int, string> pair in bonuses)
            {
                if (pair.Key == frac_id) return pair.Value;
            }
            return "";
        }

        public static string[] getListNames()
        {
            return bonuses.Values.ToArray();
        }
    }
}
