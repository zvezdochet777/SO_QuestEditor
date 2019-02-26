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
            string path = CBlackBoxConstants.getPath();

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
            reader.Close();
        }

        public static string getPath()
        {
            if (File.Exists(CEffectConstants.JSON_PATH))
                return CBlackBoxConstants.JSON_PATH;
            return CBlackBoxConstants.OTHER_JSON_PATH;
        }

        public List<string> getAll()
        {
            return blackBoxes;
        }
    }
}
