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
       protected int id;
       protected int stack;

        public CEffect()
        {
            this.id = 0;
            this.stack = 0;
        }

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

    public class DialogEffect : CEffect
    {

        protected string stack_before;
        protected string stack_from;

        public DialogEffect(int id, string stack_from, string stack_before)
            : base(id, 0)
        {
            this.stack_before = stack_before;
            this.stack_from = stack_from;
        }

        public DialogEffect(int id, string stack)
            : base(id, 0)
        {
            string[] tmp;
            tmp = stack.Split(':');
            this.stack_from = tmp[0];
            this.stack_before = tmp[1];   
        }

        public string getStacks()
        {
            return this.stack_from + ":" + this.stack_before;
        }

        public string getStackFrom()
        {
            return this.stack_from;
        }
        public string getStackBefore()
        {
            return this.stack_before;
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
