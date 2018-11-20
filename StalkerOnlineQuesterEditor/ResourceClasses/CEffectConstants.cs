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
        public static string JSON_PATH = "../../../res/scripts/common/data/Effects.json";
        public static string OTHER_JSON_PATH = "source/Effects.json";

        JsonTextReader reader;
        public Dictionary<int, string> effects = new Dictionary<int, string>();

        public CEffectConstants()
        {
            string path;
            if (File.Exists(CEffectConstants.JSON_PATH))
                path = CEffectConstants.JSON_PATH;
            else
                path = CEffectConstants.OTHER_JSON_PATH;

            reader = new JsonTextReader(new StreamReader(path, Encoding.UTF8));
            string name = "";
            bool inName = false;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    name = reader.Value.ToString();
                    inName = true;
                }
                if (reader.TokenType == JsonToken.Integer)
                    if (inName)
                    {
                        inName = false;
                        int id = Convert.ToInt32(reader.Value);
                        if (effects.ContainsKey(id))
                            System.Windows.Forms.MessageBox.Show("Ошибка парсинга эффектов. Повторяются ID в effects.json, ошибочный эффект - " + id.ToString(), "Ошибка");
                        effects.Add(id, name);
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

        public bool hasEffectById(int id)
        {
            return effects.ContainsKey(id);
        }
    }


}
