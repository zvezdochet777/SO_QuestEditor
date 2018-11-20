using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StalkerOnlineQuesterEditor
{
    class CBlackBoxConstants
    {
        public static string JSON_PATH = "../../../res/scripts/common/data/blackboxs.json";
        public static string OTHER_JSON_PATH = "source/blackboxs.json";
        JsonTextReader reader;
        protected List<string> blackBoxes = new List<string>();

        public CBlackBoxConstants()
        {
            string path;
            if (File.Exists(CEffectConstants.JSON_PATH))
                path = CBlackBoxConstants.JSON_PATH;
            else
                path = CBlackBoxConstants.OTHER_JSON_PATH;

            reader = new JsonTextReader(new StreamReader(path, Encoding.UTF8));
            string name = "";
            bool inName = false;

            while (reader.Read())
            {
                if ((reader.TokenType == JsonToken.String) && (inName))
                {
                    inName = false;
                    if (blackBoxes.Contains(reader.Value.ToString()))
                        System.Windows.Forms.MessageBox.Show("Ошибка парсинга Блекбоксов. Уже был такой блекбокс "+ reader.Value.ToString(), "Ошибка");
                    blackBoxes.Add(reader.Value.ToString());
                }

                if (reader.TokenType == JsonToken.PropertyName)
                {
                    name = reader.Value.ToString();
                    if (name == "Name") inName = true;
                }
                
               
            } 
        }

        public List<string> getAll()
        {
            return blackBoxes;
        }
    }
}
