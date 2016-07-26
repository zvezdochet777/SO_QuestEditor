using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using UMD.HCIL.Piccolo;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{
    //! Словарь <DialogID, CDialog>
    using NPCDialogDict = Dictionary<int, CDialog>;
    //! Словарь <NPCName, <DialogID, CDialog>>
    using NPCDicts = Dictionary<string, Dictionary<int, CDialog>>;
    //! Словарь <LocaleName, <NPCName, <DialogID, CDialog>>>
    using NPCLocales = Dictionary<string, Dictionary<string, Dictionary<int, CDialog>>>;
    //! Словарь <NPCName, <DialogID, CDifference>>
    using DifferenceDict = Dictionary<string, Dictionary<int, CDifference>>;
    //! Словарь <Имя NPC, < ID диалога, Структура NodeCoordinates > 
    using CoordinatesDict = Dictionary<string, Dictionary<int, NodeCoordinates>>;

    //! Класс обработки диалогов
    public class CDialogs
    {
        private MainForm parent;
        //! XML файл диалогов для хранения информации
        XDocument doc = new XDocument();
        //! Словарь диалогов: < Имя NPC, <DialogID,  CDialog> >
        public NPCDicts dialogs = new NPCDicts();
        //! Словарь локалей
        public NPCLocales locales = new NPCLocales();
        private CoordinatesDict tempCoordinates = new CoordinatesDict();
        private CManagerNPC ManagerNPC;

        //! Конструктор - парсит текущий файл диалогов, ищет локализации и парсит их тоже
        public CDialogs(MainForm parent, CManagerNPC managerNPC)
        {
            this.parent = parent;
            ManagerNPC = managerNPC;
            ParseNodeCoordinates("NodeCoordinates.xml");

            ParseDialogsData(parent.settings.GetDialogDataPath(), this.dialogs);
            ParseDialogsTexts(parent.settings.GetDialogTextPath(), this.dialogs);

            foreach (var locale in parent.settings.getListLocales())
            {
                if (!locales.Keys.Contains(locale))
                    locales.Add(locale, new NPCDicts());
                ParseDialogsData(parent.settings.GetDialogDataPath(), this.locales[locale]);
                ParseDialogsTexts(parent.settings.GetDialogLocaleTextPath(), this.locales[locale]);
            }
        }

        //! Парсер xml - файла данных диалогов, записывает результат в target
        private void ParseDialogsData(String DialogsXMLFile, NPCDicts target)
        {
            if (!File.Exists(DialogsXMLFile))
                return;

            doc = XDocument.Load(DialogsXMLFile);
            foreach (XElement npc in doc.Root.Elements())
            {
                string npc_name = npc.Element("Name").Value.ToString();
                target.Add(npc_name, new Dictionary<int, CDialog>());

                foreach (XElement dialog in npc.Elements("Dialog"))
                {
                    int DialogID = int.Parse(dialog.Element("ID").Value);
                    List<int> Nodes = new List<int>();
                    Actions Actions = new Actions();
                    CDialogPrecondition Precondition = new CDialogPrecondition();

                    if (dialog.Element("Nodes").Value != "")
                        foreach (string node in dialog.Element("Nodes").Value.Split(','))
                            if (node != "")
                                Nodes.Add(int.Parse(node));

                    Actions.Exit = dialog.Element("Actions").Element("Exit").Value == "1";
                    Actions.ToDialog = ParseIntIfNotEmpty(dialog, "Actions", "ToDialog", 0);
                    Actions.Event = parent.dialogEvents.GetEventFromID(ParseIntIfNotEmpty(dialog, "Actions", "Event", 0));
                    Actions.Data = dialog.Element("Actions").Element("Data").Value;

                    AddDataToList(dialog, "Actions", "GetQuest", Actions.GetQuests);
                    AddDataToList(dialog, "Actions", "CompleteQuest", Actions.CompleteQuests);
                    if (dialog.Element("Actions").Descendants().Any(item => item.Name =="CancelQuest"))
                    {
                        AddDataToList(dialog, "Actions", "CancelQuest", Actions.CancelQuests);
                        AddDataToList(dialog, "Actions", "FailQuest", Actions.FailQuests);
                    }

                    AddPreconditionQuests(dialog, "ListOfNecessaryQuests", "listOfOpenedQuests", Precondition.ListOfNecessaryQuests.ListOfOpenedQuests);
                    AddPreconditionQuests(dialog, "ListOfNecessaryQuests", "listOfOnTestQuests", Precondition.ListOfNecessaryQuests.ListOfOnTestQuests);
                    AddPreconditionQuests(dialog, "ListOfNecessaryQuests", "listOfCompletedQuests", Precondition.ListOfNecessaryQuests.ListOfCompletedQuests);
                    AddPreconditionQuests(dialog, "ListOfNecessaryQuests", "listOfFailedQuests", Precondition.ListOfNecessaryQuests.ListOfFailedQuests);

                    AddPreconditionQuests(dialog, "ListOfMustNoQuests", "listOfOpenedQuests", Precondition.ListOfMustNoQuests.ListOfOpenedQuests);
                    AddPreconditionQuests(dialog, "ListOfMustNoQuests", "listOfOnTestQuests", Precondition.ListOfMustNoQuests.ListOfOnTestQuests);
                    AddPreconditionQuests(dialog, "ListOfMustNoQuests", "listOfCompletedQuests", Precondition.ListOfMustNoQuests.ListOfCompletedQuests);
                    AddPreconditionQuests(dialog, "ListOfMustNoQuests", "listOfFailedQuests", Precondition.ListOfMustNoQuests.ListOfFailedQuests);

                    Precondition.KarmaPK = new List<int>();
                    AddDataToList(dialog, "Precondition", "KarmaPK", Precondition.KarmaPK);
                    AddDataToList(dialog, "Precondition", "tests", Precondition.tests);

                    if (dialog.Element("Precondition").Descendants().Any(itm2 => itm2.Name == "PlayerLevel"))
                        if (!dialog.Element("Precondition").Element("PlayerLevel").Value.Equals(""))
                            Precondition.PlayerLevel = int.Parse(dialog.Element("Precondition").Element("PlayerLevel").Value);

                    NodeCoordinates nodeCoord = new NodeCoordinates();
                    nodeCoord.RootDialog = dialog.Element("RootDialog").Value.Equals("1");
                    nodeCoord.Active = dialog.Element("Active").Value.Equals("1");
                    if (tempCoordinates.ContainsKey(npc_name) && tempCoordinates[npc_name].ContainsKey(DialogID))
                    {
                        nodeCoord.X = tempCoordinates[npc_name][DialogID].X;
                        nodeCoord.Y = tempCoordinates[npc_name][DialogID].Y;
                    }

                    foreach (string el in dialog.Element("Precondition").Element("Reputation").Value.Split(';'))
                    {
                        if (el == "")
                            continue;
                        string[] fr = el.Split(':');
                        int fractionID = int.Parse(fr[0]);
                        Precondition.Reputation.Add(fractionID, new List<double>());
                        double A = double.Parse(fr[1], System.Globalization.CultureInfo.InvariantCulture);
                        double B = double.Parse(fr[2], System.Globalization.CultureInfo.InvariantCulture);
                        Precondition.Reputation[fractionID].Add(A);
                        Precondition.Reputation[fractionID].Add(B);
                    }

                    if (!target[npc_name].Keys.Contains(DialogID))
                        target[npc_name].Add(DialogID, new CDialog(npc_name, "", "", Precondition, Actions, Nodes, DialogID, 0, nodeCoord));
                }
            }
        }

        private void AddPreconditionQuests(XElement Element, String Name1, String Name2, List<int> list)
        {
            if (Element.Element("Precondition").Element(Name1).Element(Name2).Value != "")
                foreach (string quest in Element.Element("Precondition").Element(Name1).Element(Name2).Value.Split(','))
                    list.Add(int.Parse(quest));
        }

        private void AddDataToList(XElement Element, String Name1, String Name2, List<int> list)
        {
            if (Element.Element(Name1).Element(Name2).Value != "")
                foreach (string quest in Element.Element(Name1).Element(Name2).Value.Split(','))
                    list.Add(int.Parse(quest));
        }

        private int ParseIntIfNotEmpty(XElement Element, String Name1, String Name2, int defaultValue)
        {
            if (!Element.Element(Name1).Element(Name2).Value.Equals(""))
                return int.Parse(Element.Element(Name1).Element(Name2).Value);
            return defaultValue;
        }

        private void ParseDialogsTexts(String DialogsXMLFile, NPCDicts target)
        { 
            doc = XDocument.Load(DialogsXMLFile);
            foreach (XElement npc in doc.Root.Elements())
            {
                string npc_name = npc.Element("Name").Value.ToString();

                foreach (XElement dialog in npc.Elements("Dialog"))
                {
                    int DialogID = int.Parse(dialog.Element("ID").Value);
                    target[npc_name][DialogID].Title = dialog.Element("Title").Value.Trim();
                    target[npc_name][DialogID].Text = dialog.Element("Text").Value.Trim();
                    int Version = 0;
                    if (!dialog.Element("Version").Value.Equals(""))
                        Version = int.Parse(dialog.Element("Version").Value);
                    target[npc_name][DialogID].version = Version;
                }
            }
        }

        //! Сохранить все диалоги в xml файл
        public void SaveDialogs()
        {
            SaveNodeCoordinates("NodeCoordinates.xml",this.dialogs);
            SaveDialogsTexts(parent.settings.GetDialogTextPath(), this.dialogs);
            SaveDialogsData(parent.settings.GetDialogDataPath(), this.dialogs);
        }

        //! Сохраняет текущую локализацию диалогов в файл
        public void SaveLocales()
        {
            SaveDialogsTexts(parent.settings.GetDialogLocaleTextPath(), this.locales[parent.settings.getCurrentLocale()]);
        }

        private void SaveDialogsTexts(string fileName, NPCDicts target)
        {
            XDocument resultDoc = new XDocument(new XElement("root"));
            XElement element;
            XElement npcElement;

            foreach (string npcName in target.Keys)
            {
                npcElement = new XElement("NPC", new XElement("Name", npcName));
                NPCDialogDict Dictdialog = target[npcName];
                foreach (CDialog dialog in Dictdialog.Values)
                {
                    element = new XElement("Dialog",
                       new XElement("ID", dialog.DialogID.ToString()),
                       new XElement("Version", dialog.version.ToString()),
                       new XElement("Title", dialog.Title),
                       new XElement("Text", dialog.Text));
                    npcElement.Add(element);
                }
                resultDoc.Root.Add(npcElement);
            }
            System.Xml.XmlWriterSettings settings = Global.GetXmlSettings();
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(fileName, settings))
            {
                resultDoc.Save(w);
            }
        }

        private void SaveDialogsData(string fileName, NPCDicts target)
        {
            XDocument resultDoc = new XDocument(new XElement("root"));
            XElement element;
            XElement npcElement;

            foreach (string npcName in target.Keys)
            {
                npcElement = new XElement("NPC", new XElement("Name", npcName));
                NPCDialogDict Dictdialog = target[npcName];
                foreach (CDialog dialog in Dictdialog.Values)
                {
                    element = new XElement("Dialog",
                       new XElement("ID", dialog.DialogID.ToString()),
                       new XElement("Precondition",
                           new XElement("ListOfNecessaryQuests",
                               new XElement("listOfCompletedQuests",
                                       Global.GetListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfCompletedQuests)),
                               new XElement("listOfOpenedQuests",
                                       Global.GetListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfOpenedQuests)),
                               new XElement("listOfOnTestQuests",
                                       Global.GetListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfOnTestQuests)),
                                new XElement("listOfFailedQuests",
                                       Global.GetListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfFailedQuests))),
                           new XElement("ListOfMustNoQuests",
                               new XElement("listOfCompletedQuests",
                                       Global.GetListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfCompletedQuests)),
                               new XElement("listOfOpenedQuests",
                                       Global.GetListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfOpenedQuests)),
                               new XElement("listOfOnTestQuests",
                                       Global.GetListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfOnTestQuests)),
                               new XElement("listOfFailedQuests",
                                       Global.GetListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfFailedQuests))),
                           new XElement("tests", Global.GetListAsString(dialog.Precondition.tests)),
                           new XElement("Reputation", dialog.Precondition.getReputation()),
                           new XElement("PlayerLevel", Global.GetIntAsString(dialog.Precondition.PlayerLevel)),
                           new XElement("KarmaPK", Global.GetListAsString(dialog.Precondition.KarmaPK))),
                       new XElement("Actions",
                           new XElement("Exit", Global.GetBoolAsString(dialog.Actions.Exit)),
                           new XElement("ToDialog", Global.GetIntAsString(dialog.Actions.ToDialog)),
                           new XElement("Data", dialog.Actions.Data),
                           new XElement("Event", dialog.Actions.Event.Value.ToString()),
                           new XElement("GetQuest", Global.GetListAsString(dialog.Actions.GetQuests)),
                           new XElement("CompleteQuest", Global.GetListAsString(dialog.Actions.CompleteQuests)),
                           new XElement("CancelQuest", Global.GetListAsString(dialog.Actions.CancelQuests)),
                           new XElement("FailQuest", Global.GetListAsString(dialog.Actions.FailQuests))),
                       new XElement("Nodes", Global.GetListAsString(dialog.Nodes)),
                       new XElement("RootDialog", Global.GetBoolAsString(dialog.coordinates.RootDialog)),
                       new XElement("Active", Global.GetBoolAsString(dialog.coordinates.Active))
                           );

                    npcElement.Add(element);
                }
                resultDoc.Root.Add(npcElement);
            }
            System.Xml.XmlWriterSettings settings = Global.GetXmlSettings();
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(fileName, settings))
            {
                resultDoc.Save(w);
            }        
        }

        private void SaveNodeCoordinates(string fileName, NPCDicts target)
        {
            XDocument resultDoc = new XDocument(new XElement("root"));
            XElement npc_element;
            foreach (String NPC_Name in target.Keys)
            {
                npc_element = new XElement("NPC", new XAttribute("NPC_Name", NPC_Name));
                foreach (CDialog dialog in target[NPC_Name].Values)
                {
                    npc_element.Add(new XElement("Dialog", 
                        new XAttribute("ID", dialog.DialogID.ToString()),
                        new XElement("X", dialog.coordinates.X.ToString()),
                        new XElement("Y", dialog.coordinates.Y.ToString())));                  
                }
                resultDoc.Root.Add(npc_element);
            }
            System.Xml.XmlWriterSettings settings = Global.GetXmlSettings();
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(fileName, settings))
            {
                resultDoc.Save(w);
            }
        }

        private void ParseNodeCoordinates(string filename)
        {
            if (!File.Exists(filename))
                return;

            doc = XDocument.Load(filename);
            foreach (XElement item in doc.Root.Elements())
            {
                string npc_name = item.Attribute("NPC_Name").Value.ToString();
                if (!tempCoordinates.ContainsKey(npc_name))
                    tempCoordinates.Add(npc_name, new Dictionary<int,NodeCoordinates>());

                foreach (XElement dialog in item.Elements())
                {
                    int id = int.Parse(dialog.Attribute("ID").Value);
                    int x = int.Parse(dialog.Element("X").Value);
                    int y = int.Parse(dialog.Element("Y").Value);
                    tempCoordinates[npc_name].Add(id, new NodeCoordinates(x,y,false,false));
                }
            }
        }

        //! Возвращает список всех NPC
        public List<string> getListOfNPC()
        {
            List<string> npc = new List<string>();
            foreach (string key in dialogs.Keys)
                npc.Add(key);
            return npc;
        }

        //--------------------------locale dialogs-------------------------------------------------------

        //! Возвращает CDialog по заданной локали, имени NPC и ID диалога
        public CDialog getLocaleDialog(int dialogID, string locale, string npcName)
        {
            if (this.locales.Keys.Contains(locale))
                if (this.locales[locale].Keys.Contains(npcName))
                    if (this.locales[locale][npcName].Keys.Contains(dialogID))
                        return locales[locale][npcName][dialogID];
            return null;
        }

        //! Добавить диалог к локали
        public void addLocaleDialog(CDialog dialog, string locale)
        {
            if (!this.locales.Keys.Contains(locale))
            {
                this.locales.Add(locale, new NPCDicts());
            }
            if (!this.locales[locale].Keys.Contains(dialog.Holder))
            {
                this.locales[locale].Add(dialog.Holder, new Dictionary<int, CDialog>());
            }

            if (this.locales[locale][dialog.Holder].Keys.Contains(dialog.DialogID))
                this.locales[locale][dialog.Holder].Remove(dialog.DialogID);
            this.locales[locale][dialog.Holder].Add(dialog.DialogID, dialog);
        }

        //! Возвращает словарь из диалогов для локализации (устаревшие, актуальные или все)
        public DifferenceDict getDialogDifference(string locale, FindType findType)
        {
            //System.Console.WriteLine("CDialogs::getDialogDifference");
            DifferenceDict ret = new DifferenceDict();
            if (this.locales.Keys.Contains(locale))
            {
                var cur_locale_info = this.locales[locale];
                foreach (var npc_name in dialogs.Keys)
                {
                    if (!cur_locale_info.Keys.Contains(npc_name))
                    {
                        //NPCDialogDict dict = parent.getDialogDictionary(npc_name);
                        NPCDialogDict dict = new NPCDialogDict();
                        dict.Add(dialogs[npc_name].First().Key, new CDialog() );
                        cur_locale_info.Add(npc_name, dict);
                    }
                    var locale_dialogs = cur_locale_info[npc_name];
                    foreach (var dialog in dialogs[npc_name].Values)
                    {
                        if (!dialog.coordinates.Active)
                            continue;
                        if (!ManagerNPC.NpcData.ContainsKey(npc_name) || ManagerNPC.NpcData[npc_name].location == "notfound")
                            continue;

                        var locale_version = 0;
                        if (locale_dialogs.Keys.Contains(dialog.DialogID))
                            locale_version = locale_dialogs[dialog.DialogID].version;
                        
                        if (!ret.Keys.Contains(npc_name))
                            ret.Add(npc_name, new Dictionary<int, CDifference>());
                        switch (findType)
                        {
                            case FindType.all:
                                ret[npc_name].Add(dialog.DialogID, new CDifference(dialog.version, locale_version));
                                break;
                            case FindType.outdatedOnly:
                                if (dialog.version != locale_version)
                                    ret[npc_name].Add(dialog.DialogID, new CDifference(dialog.version, locale_version));
                                break;
                            case FindType.actualOnly:
                                if (dialog.version == locale_version)
                                    ret[npc_name].Add(dialog.DialogID, new CDifference(dialog.version, locale_version));
                                break;
                        }
                    }
                }
            }
            return ret;
        }

    }
}