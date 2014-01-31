using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml;


namespace StalkerOnlineQuesterEditor
{
    public class CEffectConstants
    {
        string JSON_PATH = "source/Effects.json";
        JsonTextReader reader;
        public Dictionary<int, string> effects = new Dictionary<int, string>();


        public CEffectConstants()
        {
            System.Console.WriteLine("CEffectConstants.__init__");
            
            reader = new JsonTextReader(new StreamReader(JSON_PATH, Encoding.UTF8));
            int id = 0;
            bool inName = false;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    int n;
                    if (int.TryParse(reader.Value.ToString(), out n))
                    {
                        id = n;
                    }

                    if (reader.Value.ToString().Equals("name"))
                    {
                        inName = true;
                    }
                }
                if (reader.TokenType == JsonToken.String)
                    if (inName)
                    {
                        inName = false;
                        effects.Add(id, reader.Value.ToString());

                    }
            }

            //foreach (int tid in effects.Keys)
            //    System.Console.WriteLine(tid.ToString() + ":" + effects[tid]);

        }

        public List<string> getAllDescriptions()
        {
            return effects.Values.ToList<string>();
        }

        public string getDescriptionOnID(int id)
        {
            string n = "";
            effects.TryGetValue(id, out n);
            return n;
        }

        public int getIDOnDescription(string description)
        {
            foreach (KeyValuePair<int, string> pair in effects)
                if (pair.Value == description)
                    return pair.Key;
            return 0;
        }
    }


}
