using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    public static class QAutogenDatacs
    {
        public static Dictionary<string, AutogenQuestData> data_quests = new Dictionary<string, AutogenQuestData>();
        public static AutogetDialogData data_dialogs = new AutogetDialogData();
        public static Dictionary<string, AGDialogLocals> locals_dialogs = new Dictionary<string, AGDialogLocals>();
        public static Dictionary<string, AGQuestLocals> locals_quests = new Dictionary<string, AGQuestLocals>();

        public static Dictionary<int, string> QuestTypes = new Dictionary<int, string>() { { 1, "Убить мобов" }, { 2, "Принести предметы" } };

        static string QUEST_JSON_PATH = "../../../res/scripts/common/data/Quests/QuestsAutogenerator/";
        static string QUEST_LOCAL_PATH = "/AutoGenQuestTexts.xml";
        static string DIALOGS_DATA_JSON_PATH = "../../../res/scripts/common/data/DialogsAutogenerator.json";
        static string DIALOGS_LOCAL_PATH = "/DialogTexts/AutogenDialogs/AutogenDialogs.xml";

        public static void Load()
        {
            string[] files = Directory.GetFiles(QUEST_JSON_PATH, "*.json");
            foreach (string dialogFile in files)
            {
                string npc_name = Path.GetFileNameWithoutExtension(dialogFile);
                using (StreamReader reader = new StreamReader(dialogFile))
                {
                    string json_string = reader.ReadToEnd();
                    AutogenQuestData myDeserializedClass = JsonConvert.DeserializeObject<AutogenQuestData>(json_string);
                    QAutogenDatacs.data_quests.Add(npc_name, myDeserializedClass);
                    reader.Close();
                }
            }

            using (StreamReader reader = new StreamReader(DIALOGS_DATA_JSON_PATH))
            {
                string json_string = reader.ReadToEnd();
                data_dialogs = JsonConvert.DeserializeObject<AutogetDialogData>(json_string);
            }

            foreach (var a in CSettings.getFullListLocales())
            {
                string path = (CSettings.pathToLocalFiles + a + QUEST_LOCAL_PATH);
                ParseQuestTexts(path, a);
            }

            foreach (var a in CSettings.getFullListLocales())
            {
                string path = (CSettings.pathToLocalFiles + a + DIALOGS_LOCAL_PATH);
                ParseDialogsTexts(path, a);
            }

            AGNPCMeta.init();
        }

        private static void ParseQuestTexts(string dialogFile, string locale)
        {
            XDocument doc;
            try
            {
                doc = XDocument.Load(dialogFile);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Произошла ошибка при чтении файла: " + dialogFile + "\n" + e.Message, "Ошибка чтения");
                return;
            }

            locals_quests.Add(locale, new AGQuestLocals());
            foreach(XElement npc in doc.Root.Elements())
            {
                string npcName = npc.Name.ToString();
                locals_quests[locale].data.Add(npcName, new Dictionary<string, QuestLocal>());
                foreach(XElement quest in npc.Elements("quest"))
                {
                    string str_id = quest.Element("id").Value.ToString();
                    int version = int.Parse(quest.Element("Version").Value);
                    locals_quests[locale].data[npcName].Add(str_id, new QuestLocal(quest.Element("text").Value.ToString(), version));
                }
            }
        }

        public static void SaveQuestTexts(string dialogFile, string locale)
        {
            XDocument resultDoc = new XDocument(new XElement("root"));
            foreach(var npcName in locals_quests[locale].data.Keys)
            {
                XElement npc_element = new XElement(npcName);
                foreach(var quest in locals_quests[locale].data[npcName])
                {
                    string str_id = quest.Key;
                    npc_element.Add(new XElement("quest", new XElement("id", str_id),
                        new XElement("text", quest.Value.text),
                        new XElement("Version", quest.Value.version)));
                }
                resultDoc.Root.Add(npc_element);
            }
            System.Xml.XmlWriterSettings settings = Global.GetXmlSettings();
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(dialogFile, settings))
            {
                resultDoc.Save(w);
            }
        }
       
        private static void ParseDialogsTexts(string dialogFile, string locale)
        {
            XDocument doc;
            try
            {
                doc = XDocument.Load(dialogFile);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Произошла ошибка при чтении файла: " + dialogFile + "\n" + e.Message, "Ошибка чтения");
                return;
            }

            locals_dialogs.Add(locale, new AGDialogLocals());

            foreach (XElement dialog in doc.Root.Elements("Title"))
            {

                int DialogID = int.Parse(dialog.Element("ID").Value);

                int Version = 0;
                if ((!dialog.Element("Version").Value.Equals("")))
                    Version = int.Parse(dialog.Element("Version").Value);
                if (dialog.Element("Text") != null)
                    locals_dialogs[locale].titles.Add(new DialogLocal(DialogID, dialog.Element("Text").Value.Trim(), Version));
            }
            foreach (XElement dialog in doc.Root.Elements("Text"))
            {

                int DialogID = int.Parse(dialog.Element("ID").Value);

                int Version = 0;
                if ((!dialog.Element("Version").Value.Equals("")))
                    Version = int.Parse(dialog.Element("Version").Value);
                if (dialog.Element("Text") != null)
                    locals_dialogs[locale].texts.Add(new DialogLocal(DialogID, dialog.Element("Text").Value.Trim(), Version));
            }
        }

        public static void SaveDialogsTexts(string dialogFile, string locale)
        {
            XElement element;
            XDocument resultDoc = new XDocument(new XElement("root"));

            foreach (var dialog in locals_dialogs[locale].titles)
            {
                element = new XElement("Title",
                   new XElement("ID", dialog.id.ToString()),
                   new XElement("Version", dialog.version.ToString()));
                if (dialog.text != "")
                    element.Add(new XElement("Text", dialog.text));

                resultDoc.Root.Add(element);
            }

            foreach (var dialog in locals_dialogs[locale].texts)
            {
                element = new XElement("Text",
                   new XElement("ID", dialog.id.ToString()),
                   new XElement("Version", dialog.version.ToString()));
                if (dialog.text != "")
                    element.Add(new XElement("Text", dialog.text));

                resultDoc.Root.Add(element);
            }
            System.Xml.XmlWriterSettings settings = Global.GetXmlSettings();
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(dialogFile, settings))
            {
                resultDoc.Save(w);
            }
        }
        

        public static void Save()
        {
            foreach(var i in data_quests)
            {
                string npc_name = i.Key;
                string path = QUEST_JSON_PATH + npc_name + ".json";
                if (!i.Value.data.Any()) continue;
                using (StreamWriter writer = new StreamWriter(path))
                {
                    string json = JsonConvert.SerializeObject(i.Value, Formatting.Indented);
                    writer.Write(json);
                    writer.Close();
                }
            }

            using (StreamWriter writer = new StreamWriter(DIALOGS_DATA_JSON_PATH))
            {
                string json = JsonConvert.SerializeObject(data_dialogs, Formatting.Indented);
                writer.Write(json);
                writer.Close();
            }
            foreach (var locale in CSettings.getFullListLocales())
            {
                string path = (CSettings.pathToLocalFiles + locale + QUEST_LOCAL_PATH);
                SaveQuestTexts(path, locale);
            }

            foreach (var locale in CSettings.getFullListLocales())
            {
                string path = (CSettings.pathToLocalFiles + locale + DIALOGS_LOCAL_PATH);
                SaveDialogsTexts(path, locale);
            }
            AGNPCMeta.save();
        }

        public static int getNewFraseID(bool isTitle)
        {
            List<int> ids = new List<int>();
            foreach(var nature in data_dialogs.nature)
            {
                foreach (var dialogs in new List<AGDialogs>() { nature.dialogs_closed, nature.dialogs_ontest, nature.dialogs_opened })
                {
                    List<int> data = isTitle ? dialogs.titles : dialogs.texts;
                    foreach (var dialog in data)
                    {
                        ids.Add(dialog);
                    }
                }
            }
            if (isTitle)
                foreach (var dialogs in new List<List<int>>() { data_dialogs.netral.no, data_dialogs.netral.yes, data_dialogs.netral.work })
                {
                    ids.AddRange(dialogs);
                }
            
            return ids.Max() + 1;

        }

        public static int getFraseIDByText(string text, bool isTitle)
        {
            int frase_id = -1;
            List<DialogLocal> dialogs;
            if (isTitle)
                dialogs = QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].titles;
            else
                dialogs = QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].texts;

            foreach (var i in dialogs)
            {
                if (i.text == text) { frase_id = i.id; break; }
            }
            return frase_id;
        }

        public static bool changeQuestTarget(string npcName, int quest_type, int target_type, string text)
        {
            if (!text.Any())
            {
                System.Windows.Forms.MessageBox.Show("Произошла ошибка. Не указано имя цели: " + text + "\n", "Ошибка чтения");
                return false;
            }
            if (find_dublicate_target_names(text))
            {
                System.Windows.Forms.MessageBox.Show("Произошла ошибка. Цели с таким названием уже существуют: " + text + "\n", "Ошибка чтения");
                return false;
            }

            string str_id = quest_type.ToString() + "_" + target_type.ToString();
            foreach (var local in CSettings.getFullListLocales())
            {
                int version = CSettings.ORIGINAL_PATH == local ? 1 : 0;
                if (!locals_quests[local].data.ContainsKey(npcName))
                    locals_quests[local].data.Add(npcName, new Dictionary<string, QuestLocal>());
                if (!locals_quests[local].data[npcName].ContainsKey(str_id))
                {
                    locals_quests[local].data[npcName].Add(str_id, new QuestLocal(text, version));
                    continue;
                }
                locals_quests[local].data[npcName][str_id].text = text;
                locals_quests[local].data[npcName][str_id].version = version;
            }
            return true;
        }

        private static bool find_dublicate_target_names(string text)
        {
            foreach(var npc in locals_quests[CSettings.ORIGINAL_PATH].data)
            {
                foreach(var quest in npc.Value)
                {
                    if (quest.Value.text == text) return true;
                }
            }
            return false;
        }

        public static bool addQuestTarget(string npcName, int quest_type, string text, AutogenTarget target)
        {
            if (!text.Any())
            {
                System.Windows.Forms.MessageBox.Show("Произошла ошибка. Не указано имя цели: " + text + "\n", "Ошибка чтения");
                return false;
            }
            if (find_dublicate_target_names(text))
            {
                System.Windows.Forms.MessageBox.Show("Произошла ошибка. Цели с таким названием уже существуют: " + text + "\n" , "Ошибка чтения");
                return false;
            }
            AutogenQuestType quest = data_quests[npcName].getQuestTypeByID(quest_type);
            target.id = quest.getNewTargetID();
            quest.targets.Add(target);
            string str_id = quest_type.ToString() + "_" + target.id.ToString();
            foreach (var local in CSettings.getFullListLocales())
            {
                int version = CSettings.ORIGINAL_PATH == local ? 1 : 0;
                if (!locals_quests[local].data.ContainsKey(npcName))
                {
                    locals_quests[local].data.Add(npcName, new Dictionary<string, QuestLocal>());
                    AGNPCMeta.addNpc(npcName);
                }
                if (locals_quests[local].data[npcName].ContainsKey(str_id))
                    return false;
                locals_quests[local].data[npcName].Add(str_id, new QuestLocal(text, version));
            }
            AGNPCMeta.changeNPC(npcName);
            return true;
        }

        public static bool deleteQuestTarget(string npcName, int quest_type, int target_type)
        {
            AutogenQuestType quest = data_quests[npcName].getQuestTypeByID(quest_type);
            quest.deleteTarget(target_type);
            string str_id = quest_type.ToString() + "_" + target_type.ToString();
            foreach(var local in CSettings.getFullListLocales())
            {
                if (locals_quests[local].data[npcName].ContainsKey(str_id))
                    locals_quests[local].data[npcName].Remove(str_id);
            }
            AGNPCMeta.changeNPC(npcName);
            return true;
        }

        public static bool addDialog(int natureID, string text, string type, bool isTitle)
        {
            int fraseID = 0;
            if (natureID == 0)
            {
                fraseID = getNewFraseID(true);
                switch (type)
                {
                    case "yes":
                        QAutogenDatacs.data_dialogs.netral.yes.Add(fraseID);
                        break;
                    case "no":
                        QAutogenDatacs.data_dialogs.netral.no.Add(fraseID);
                        break;
                    case "get":
                        QAutogenDatacs.data_dialogs.netral.work.Add(fraseID);
                        break;
                }
            }
            else
            {
                Nature dialog_data;

                dialog_data = QAutogenDatacs.data_dialogs.get_nature(natureID);

                if (dialog_data == null) return false;

                fraseID = getNewFraseID(isTitle);

                switch (type)
                {
                    case "on_test":
                        if (isTitle)
                            dialog_data.dialogs_ontest.titles.Add(fraseID);
                        else dialog_data.dialogs_ontest.texts.Add(fraseID);
                        break;
                    case "opened":
                        if (isTitle)
                            dialog_data.dialogs_opened.titles.Add(fraseID);
                        else dialog_data.dialogs_opened.texts.Add(fraseID);
                        break;
                    case "closed":
                        if (isTitle)
                            dialog_data.dialogs_closed.titles.Add(fraseID);
                        else dialog_data.dialogs_closed.texts.Add(fraseID);
                        break;
                }
            }
            foreach (var locale in CSettings.getFullListLocales())
            {
                DialogLocal new_dialog = new DialogLocal(fraseID, text, locale == CSettings.ORIGINAL_PATH ? 1 : 0);

                if (isTitle)
                    QAutogenDatacs.locals_dialogs[locale].titles.Add(new_dialog);
                else
                    QAutogenDatacs.locals_dialogs[locale].texts.Add(new_dialog); ;
            }

            return true;

        }

        public static bool removeDialog(int natureID, int fraseID, string type, bool isTitle)
        {
            if (natureID == 0)
            {
                switch (type)
                {
                    case "yes":
                        QAutogenDatacs.data_dialogs.netral.yes.Remove(fraseID);
                        break;
                    case "no":
                        QAutogenDatacs.data_dialogs.netral.no.Remove(fraseID);
                        break;
                    case "get":
                        QAutogenDatacs.data_dialogs.netral.work.Remove(fraseID);
                        break;
                }
            }
            else
            {
                var dialog_data = QAutogenDatacs.data_dialogs.get_nature(natureID);
                if (dialog_data == null) return false;
                switch (type)
                {
                    case "on_test":
                        if (isTitle)
                            dialog_data.dialogs_ontest.titles.Remove(fraseID);
                        else dialog_data.dialogs_ontest.texts.Remove(fraseID);
                        break;
                    case "opened":
                        if (isTitle)
                            dialog_data.dialogs_opened.titles.Remove(fraseID);
                        else dialog_data.dialogs_opened.texts.Remove(fraseID);
                        break;
                    case "closed":
                        if (isTitle)
                            dialog_data.dialogs_closed.titles.Remove(fraseID);
                        else dialog_data.dialogs_closed.texts.Remove(fraseID);
                        break;
                }
            }
            List<DialogLocal> dialog_text;
            foreach (var locale in CSettings.getFullListLocales())
            {
                if (isTitle)
                    dialog_text = QAutogenDatacs.locals_dialogs[locale].titles;
                else
                    dialog_text = QAutogenDatacs.locals_dialogs[locale].texts;
                foreach (var i in dialog_text)
                {
                    if (i.id == fraseID) { dialog_text.Remove(i); break; }
                }
            }
            return true;
        }
    }

    public class Reward
    {
        public int money;
        public int exp;
    }

    public class AutogenTarget
    {
        public int id { get; set; }
        public int int_param { get; set; }
        public string str_param { get; set; }
        public List<int> counts { get; set; }
        public List<Reward> rewards;

        public AutogenTarget()
        {
            counts = new List<int>();
            rewards = new List<Reward>();
        }
    }

    public class AutogenQuestType
    {
        public int id { get; set; }
        public List<AutogenTarget> targets { get; set; }

        public AutogenQuestType()
        {
            targets = new List<AutogenTarget>();
        }

        public AutogenTarget getTargetByType(int target_type)
        {
            foreach (var i in targets)
                if (i.id == target_type) return i;
            return null;
        }

        public void setTargetByType(int target_type, AutogenTarget new_target)
        {
            foreach (var i in targets)
                if (i.id == target_type)
                {
                    i.int_param = new_target.int_param;
                    i.str_param = new_target.str_param;
                    i.counts = new_target.counts;
                    return;
                };

            targets.Add(new_target);
        }

        public int getNewTargetID()
        {
            int result = 0;
            foreach (var i in targets)
                result = Math.Max(i.id, result);
            return result + 1;
        }

        public void deleteTarget(int target_type)
        {
            foreach (var i in targets)
                if (i.id == target_type)
                {
                    targets.Remove(i);
                    return;
                }
        }

    }

    public class AutogenQuestData
    {
        public List<AutogenQuestType> data { get; set; }

        public AutogenQuestData()
        {
            data = new List<AutogenQuestType>();
        }

        public AutogenQuestType getQuestTypeByID(int id)
        {
            foreach (var i in data)
                if (i.id == id) return i;
            return null;
        }

        public int getNewTargetID(int quest_type)
        {
            return data[quest_type].getNewTargetID();
        }

    }

    public class QuestLocal
    {
        public string text = "";
        public int version = 0;

        public QuestLocal(string _text, int _version)
        {
            this.text = _text;
            this.version = _version;
        }
    }

    public class DialogLocal
    {
        public int id = 0;
        public string text = "";
        public int version = 0;

        public DialogLocal(int _id, string _text, int _version)
        {
            this.id = _id;
            this.text = _text;
            this.version = _version;
        }
    }

    public class AGQuestLocals
    {
        public Dictionary<string, Dictionary<string, QuestLocal>> data = new Dictionary<string, Dictionary<string, QuestLocal>>();

        public string getTextByID(string npc_name, int quest_id, int target_id)
        {
            if (!data.ContainsKey(npc_name))
                return "NPC ERROR";
            string str_id = quest_id.ToString() + "_" + target_id.ToString();
            if (!data[npc_name].ContainsKey(str_id))
                return "QUEST ERROR";
            
            return data[npc_name][str_id].text;
        }
    }
    public class AGDialogLocals

    {
        public List<DialogLocal> texts = new List<DialogLocal>();
        public List<DialogLocal> titles = new List<DialogLocal>();

        public string getTextByID(int id)
        {
            foreach (var item in texts)
            {
                if (item.id == id) return item.text;
            }
            return "NONE ERR";
        }
        public string getTitleByID(int id)
        { 
            foreach (var item in titles)
            {
                if (item.id == id) return item.text;
            }
            return "NONE ERR";
        }
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class AGDialogs
    {
        public List<int> titles { get; set; }
        public List<int> texts { get; set; }
    }

    public class Nature
    {
        public int id { get; set; }
        public AGDialogs dialogs_opened { get; set; }
        public AGDialogs dialogs_ontest { get; set; }
        public AGDialogs dialogs_closed { get; set; }
    }
    
    public class Netural
    {
        public List<int> work { get; set; }
        public List<int> yes { get; set; }
        public List<int> no { get; set; }
    }

    public class AutogetDialogData
    {
        public List<Nature> nature { get; set; }
        public Netural netral { get; set; }

        public Nature get_nature(int id)
        {
            foreach(var a in nature)
            {
                if (id == a.id) return a;

            }
            return null;
        }
    }

    public static class AGNPCMeta
    {
        public static string JSON_PATH = "../../../res/scripts/common/data/Quests/QuestsAutogenerator/_meta.json";
        static JsonTextReader reader;
        static List<string> changedNPC = new List<string>();
        static Dictionary<string, int[]> data = new Dictionary<string, int[]>();

        public static void addNpc(string npcName)
        {
            if (data.ContainsKey(npcName))
                return;

            int new_index = AGNPCMeta.getMaxID() + 1;
            data.Add(npcName, new int[] { new_index, 0 });

        }

        public static void changeNPC(string npcName)
        {
            if (!data.ContainsKey(npcName))
                return;
            if (changedNPC.Contains(npcName)) return;
            changedNPC.Add(npcName);
        }

        public static int getMaxID()
        {
            int max = -9999;
            foreach (var i in data.Values)
            {
                if (i[0] > max) max = i[0];
            }
            return max;
        }

        public static void init()
        {
            reader = new JsonTextReader(new StreamReader(JSON_PATH, Encoding.UTF8));
            string npc_name = "";
            bool flag = false;
            int id = 0;
            int ver = 0;
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.StartArray)
                {
                    flag = true;
                    continue;
                }

                if (reader.TokenType == JsonToken.Integer)
                {
                    if (flag)
                    {
                        id = Convert.ToInt32(reader.Value.ToString());
                        flag = false;
                    }
                    else
                    {
                        ver = Convert.ToInt32(reader.Value.ToString());
                        data.Add(npc_name, new int[2] { id, ver });
                    }

                }
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    npc_name = reader.Value.ToString();
                }
            }
            reader.Close();
        }

        public static void save()
        {
            foreach(var name in changedNPC)
                data[name][1]++;


            using (JsonWriter writer = new JsonTextWriter(new StreamWriter(JSON_PATH)))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                foreach (var val in data)
                {
                    writer.WritePropertyName(val.Key);
                    writer.WriteStartArray();
                    foreach (int id in val.Value)
                    {
                        writer.WriteValue(id);
                    }
                    writer.WriteEnd();
                }
                writer.WriteEndObject();
            }
        }
    }

}
