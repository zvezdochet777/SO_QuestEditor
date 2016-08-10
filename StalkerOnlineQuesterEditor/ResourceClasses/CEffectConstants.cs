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

    public class CEffect
    {
        int id;
        int stack;

        public CEffect(int id, int stack)
        {
            this.id = id;
            this.stack = stack;
        }

        public int getID()
        {
            return this.id;
        }

        public int getStack()
        {
            return this.stack;
        }
    }

    public class CEffectConstants
    {
        string JSON_PATH = "source/Effects.json";
        JsonTextReader reader;
        public Dictionary<int, string> effects = new Dictionary<int, string>();

        public CEffectConstants()
        {
            reader = new JsonTextReader(new StreamReader(JSON_PATH, Encoding.UTF8));
            int id = 0;
            bool inName = false;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    int n;
                    if (int.TryParse(reader.Value.ToString(), out n))
                        id = n;

                    if (reader.Value.ToString().Equals("name"))
                        inName = true;
                }
                if (reader.TokenType == JsonToken.String)
                    if (inName)
                    {
                        inName = false;
                        effects.Add(id, reader.Value.ToString());
                    }
            }
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
