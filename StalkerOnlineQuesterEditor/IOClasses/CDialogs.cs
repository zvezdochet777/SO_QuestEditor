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
            List<int> tests;
            doc = XDocument.Load(DialogsXMLFile);
            foreach (XElement npc in doc.Root.Elements())
            {
                string npc_name = npc.Element("Name").Value.ToString().Trim();
                target.Add(npc_name, new Dictionary<int, CDialog>());

                foreach (XElement dialog in npc.Elements("Dialog"))
                {
                    int DialogID = int.Parse(dialog.Element("ID").Value);
                    string DebugData = "";
                    bool isAutoNode = false;
                    string defaultNode = "";
                    List<int> Nodes = new List<int>();
                    List<int> CheckNodes = new List<int>();
                    Actions Actions = new Actions();
                    CDialogPrecondition Precondition = new CDialogPrecondition();

                    if ((dialog.Element("Nodes") != null) && (dialog.Element("Nodes").Value != ""))
                        foreach (string node in dialog.Element("Nodes").Value.Split(','))
                            if (node != "")
                                Nodes.Add(int.Parse(node));
                    if ((dialog.Element("CheckNodes") != null) && (dialog.Element("CheckNodes").Value != ""))
                        foreach (string node in dialog.Element("CheckNodes").Value.Split(','))
                            if (node != "")
                                CheckNodes.Add(int.Parse(node));
                    if (dialog.Element("Actions") != null)
                    {
                        if (dialog.Element("Actions").Element("GoToCamera") != null)
                           {
                            Actions.actionCamera = dialog.Element("Actions").Element("GoToCamera").Value;
                            if (dialog.Element("Actions").Element("CameraSmoothly") == null)
                                Actions.actionCameraSmoothly = false;
                            }
                        if (dialog.Element("Actions").Element("AnimationPlayer") != null)
                            Actions.actionAnimationPlayer = dialog.Element("Actions").Element("AnimationPlayer").Value;
                        if (dialog.Element("Actions").Element("AnimationNPC") != null)
                            Actions.actionAnimationNPC = dialog.Element("Actions").Element("AnimationNPC").Value;
                        if (dialog.Element("Actions").Element("ItemNPC") != null)
                            Actions.actionItemNPC = dialog.Element("Actions").Element("ItemNPC").Value;
                        if (dialog.Element("Actions").Element("AvatarPoint") != null)
                            Actions.actionAvatarPoint = dialog.Element("Actions").Element("AvatarPoint").Value;
                        if (dialog.Element("Actions").Element("PlaySound") != null)
                            Actions.actionPlaySound = dialog.Element("Actions").Element("PlaySound").Value;

                    if (dialog.Element("Actions").Element("Exit")!= null)
                        Actions.Exit = dialog.Element("Actions").Element("Exit").Value == "1";
                    Actions.ToDialog = ParseIntIfNotEmpty(dialog, "Actions", "ToDialog", 0);
                    Actions.Event = parent.dialogEvents.GetEventFromID(ParseIntIfNotEmpty(dialog, "Actions", "Event", 0));
                    if (dialog.Element("Actions").Element("Data") != null)
                        Actions.Data = dialog.Element("Actions").Element("Data").Value;

                    AddDataToList(dialog, "Actions", "GetQuest", Actions.GetQuests);
                    AddDataToList(dialog, "Actions", "CompleteQuest", Actions.CompleteQuests);
                    if (dialog.Element("Actions").Descendants().Any(item => item.Name =="CancelQuest"))
                    {
                        AddDataToList(dialog, "Actions", "CancelQuest", Actions.CancelQuests);
                        AddDataToList(dialog, "Actions", "FailQuest", Actions.FailQuests);
                    }
                    }
                    if (dialog.Element("Precondition") != null)
                    {

                        if (dialog.Element("Precondition").Element("ListOfNecessaryQuests") != null)
                        {
                            AddPreconditionQuests(dialog, "ListOfNecessaryQuests", "listOfOpenedQuests", Precondition.ListOfNecessaryQuests.ListOfOpenedQuests, ref Precondition.ListOfNecessaryQuests.conditionOfOpenedQuests);
                            AddPreconditionQuests(dialog, "ListOfNecessaryQuests", "listOfFailQuests", Precondition.ListOfNecessaryQuests.ListOfFailQuests, ref Precondition.ListOfNecessaryQuests.conditionOfFailQuests);
                            AddPreconditionQuests(dialog, "ListOfNecessaryQuests", "listOfOnTestQuests", Precondition.ListOfNecessaryQuests.ListOfOnTestQuests, ref Precondition.ListOfNecessaryQuests.conditionOfOnTestQuest);
                            AddPreconditionQuests(dialog, "ListOfNecessaryQuests", "listOfCompletedQuests", Precondition.ListOfNecessaryQuests.ListOfCompletedQuests, ref Precondition.ListOfNecessaryQuests.conditionOfCompletedQuests);
                            AddPreconditionQuests(dialog, "ListOfNecessaryQuests", "listOfOmniCounters", Precondition.ListOfNecessaryQuests.ListOfCounters, ref Precondition.ListOfNecessaryQuests.conditionOfCounterss);
                            AddPreconditionQuests(dialog, "ListOfNecessaryQuests", "listOfRepeat", Precondition.ListOfNecessaryQuests.ListOfRepeat, ref Precondition.ListOfNecessaryQuests.conditionOfRepeat);
                            
                            if (dialog.Element("Precondition").Element("ListOfNecessaryQuests").Element("listOfMassQuests") != null)
                            {
                                if (dialog.Element("Precondition").Element("ListOfNecessaryQuests").Element("listOfMassQuests").Value.Contains('|'))
                                    Precondition.ListOfNecessaryQuests.conditionOfMassQuests = '|';
                                else
                                    Precondition.ListOfNecessaryQuests.conditionOfMassQuests = '&';
                                Precondition.ListOfNecessaryQuests.ListOfMassQuests = dialog.Element("Precondition").Element("ListOfNecessaryQuests").Element("listOfMassQuests").Value;
                                Precondition.ListOfNecessaryQuests.ListOfMassQuests.Replace(Precondition.ListOfNecessaryQuests.conditionOfMassQuests, ',');
                            }
                        }
                        if (dialog.Element("Precondition").Element("ListOfMustNoQuests") != null)
                        {
                            AddPreconditionQuests(dialog, "ListOfMustNoQuests", "listOfOpenedQuests", Precondition.ListOfMustNoQuests.ListOfOpenedQuests, ref Precondition.ListOfMustNoQuests.conditionOfOpenedQuests);
                            AddPreconditionQuests(dialog, "ListOfMustNoQuests", "listOfFailQuests", Precondition.ListOfMustNoQuests.ListOfFailQuests, ref Precondition.ListOfMustNoQuests.conditionOfFailQuests);
                            AddPreconditionQuests(dialog, "ListOfMustNoQuests", "listOfOnTestQuests", Precondition.ListOfMustNoQuests.ListOfOnTestQuests, ref Precondition.ListOfMustNoQuests.conditionOfOnTestQuest);
                            AddPreconditionQuests(dialog, "ListOfMustNoQuests", "listOfCompletedQuests", Precondition.ListOfMustNoQuests.ListOfCompletedQuests, ref Precondition.ListOfMustNoQuests.conditionOfCompletedQuests);
                            AddPreconditionQuests(dialog, "ListOfMustNoQuests", "listOfOmniCounters", Precondition.ListOfMustNoQuests.ListOfCounters, ref Precondition.ListOfMustNoQuests.conditionOfCounterss);
                            AddPreconditionQuests(dialog, "ListOfMustNoQuests", "listOfRepeat", Precondition.ListOfMustNoQuests.ListOfRepeat, ref Precondition.ListOfMustNoQuests.conditionOfRepeat);
                        
                            if (dialog.Element("Precondition").Element("ListOfMustNoQuests").Element("listOfMassQuests") != null)
                            {
                                if (dialog.Element("Precondition").Element("ListOfMustNoQuests").Element("listOfMassQuests").Value.Contains('|'))
                                    Precondition.ListOfMustNoQuests.conditionOfMassQuests = '|';
                                else
                                    Precondition.ListOfMustNoQuests.conditionOfMassQuests = '&';
                                Precondition.ListOfMustNoQuests.ListOfMassQuests = dialog.Element("Precondition").Element("ListOfMustNoQuests").Element("listOfMassQuests").Value;
                                Precondition.ListOfMustNoQuests.ListOfMassQuests.Replace(Precondition.ListOfMustNoQuests.conditionOfMassQuests, ',');
                            }
                        }
                        if (dialog.Element("Precondition").Element("NecessaryEffects") != null)
                            AddDialogEffectsToList(dialog, "Precondition", "NecessaryEffects", Precondition.NecessaryEffects);
                        if (dialog.Element("Precondition").Element("MustNoEffects") != null)
                            AddDialogEffectsToList(dialog, "Precondition", "MustNoEffects", Precondition.MustNoEffects);

                        if (dialog.Element("Precondition").Element("Skills") != null)
                            AddDialogSkillsToListSkills(dialog, "Precondition", "Skills", Precondition.Skills);
                        Precondition.KarmaPK = new List<int>();

                        AddDataToList(dialog, "Precondition", "KarmaPK", Precondition.KarmaPK);
                        if (dialog.Element("Precondition").Element("forDev") != null)
                            Precondition.forDev = true;
                        if (dialog.Element("Precondition").Element("clanOptions") != null)
                        {
                            Precondition.clanOptions = dialog.Element("Precondition").Element("clanOptions").Value.ToString();
                        }
                        if (dialog.Element("Precondition").Element("tests") != null)
                        {
                            tests = new List<int>();
                            AddDataToList(dialog, "Precondition", "tests", tests);
                            for (int i = 0; i < tests.Count; i++)
                            {
                                if (tests[i] == 0) tests[i] = 7;
                                if (tests[i] == 2) tests[i] = 8;
                            }
                            if (Precondition.clanOptions == "")
                                Precondition.clanOptions = Global.GetListAsString(tests);
                            else
                                Precondition.clanOptions += "," + Global.GetListAsString(tests);
                        }
                        if (dialog.Element("Precondition").Element("PlayerLevel") != null)
                        {
                            Precondition.PlayerLevel = dialog.Element("Precondition").Element("PlayerLevel").Value;
                        }
                        if (dialog.Element("Precondition").Element("playerCombatLvl") != null)
                        {
                            Precondition.playerCombatLvl = dialog.Element("Precondition").Element("playerCombatLvl").Value;
                        }
                        if (dialog.Element("Precondition").Element("playerSurvLvl") != null)
                        {
                            Precondition.playerSurvLvl = dialog.Element("Precondition").Element("playerSurvLvl").Value;
                        }
                        if (dialog.Element("Precondition").Element("playerOtherLvl") != null)
                        {
                            Precondition.playerOtherLvl = dialog.Element("Precondition").Element("playerOtherLvl").Value;
                        }
                        if (dialog.Element("Precondition").Element("Reputation") != null)
                        {
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
                        }

                        if (dialog.Element("Precondition").Element("items") != null)
                        {
                            if (dialog.Element("Precondition").Element("items").Element("itemCategory") != null)
                                Precondition.items.itemCategory = int.Parse(dialog.Element("Precondition").Element("items").Element("itemCategory").Value);
                            else if (dialog.Element("Precondition").Element("items").Element("typeOfItems") != null)
                            {
                                AddDataToList(dialog.Element("Precondition"), "items", "typeOfItems", Precondition.items.typeOfItems);
                                AddDataToList(dialog.Element("Precondition"), "items", "numOfItems", Precondition.items.numOfItems);
                                AddDataToList(dialog.Element("Precondition"), "items", "attrOfItems", Precondition.items.attrOfItems);
                            }
                        }

                    }
                    NodeCoordinates nodeCoord = new NodeCoordinates();
                    if (dialog.Element("RootDialog") != null)
                        nodeCoord.RootDialog = dialog.Element("RootDialog").Value.Trim().Equals("1");
                    if (dialog.Element("Active") != null)
                        nodeCoord.Active = dialog.Element("Active").Value.Trim().Equals("1");
                    else nodeCoord.Active = false;
                    if (tempCoordinates.ContainsKey(npc_name) && tempCoordinates[npc_name].ContainsKey(DialogID))
                    {
                        nodeCoord.X = tempCoordinates[npc_name][DialogID].X;
                        nodeCoord.Y = tempCoordinates[npc_name][DialogID].Y;
                    }


                    if (dialog.Element("DebugData") != null)
                        DebugData = dialog.Element("DebugData").Value.ToString();
                    if (dialog.Element("isAutoNode") != null)
                    {
                        isAutoNode = dialog.Element("isAutoNode").Value.Trim().Equals("1");
                        if (dialog.Element("defaultNode") != null) defaultNode = dialog.Element("defaultNode").Value; 

                    }
                    if (!target[npc_name].Keys.Contains(DialogID))
                        target[npc_name].Add(DialogID, new CDialog(npc_name, "", "", Precondition, Actions, Nodes, CheckNodes, DialogID, 0, nodeCoord, DebugData, isAutoNode, defaultNode));
                }
            }
        }

        private void AddPreconditionQuests(XElement Element, String Name1, String Name2, List<int> list, ref char condition)
        {
            if (Element.Element("Precondition").Element(Name1).Element(Name2) == null)
                return;
            if (Element.Element("Precondition").Element(Name1).Element(Name2).Value != "")

                if (Element.Element("Precondition").Element(Name1).Element(Name2).Value.Contains('|'))
                    condition = '|';
                else if (Element.Element("Precondition").Element(Name1).Element(Name2).Value.Contains('&'))
                    condition = '&';
                else
                    condition = ',';
                foreach (string quest in Element.Element("Precondition").Element(Name1).Element(Name2).Value.Split(condition))
                    list.Add(int.Parse(quest));
        }
        private void AddDialogEffectsToList(XElement Element, String Name1, String Name2, List<DialogEffect> list)
        {
            if (Element.Element(Name1).Element(Name2) == null)
                return;
          
                foreach (XElement effect in Element.Element(Name1).Element(Name2).Elements())
                {
                    int ID = int.Parse(effect.Element("id").Value);
                    string stack = effect.Element("stack").Value;
                    list.Add(new DialogEffect(ID, stack));
                }
        }
        private void AddDialogSkillsToListSkills(XElement Element, String Name1, String Name2, ListDialogSkills list)
        {
            if (Element.Element(Name1).Element(Name2) == null)
                return;

            foreach (XElement effect in Element.Element(Name1).Element(Name2).Elements())
            {
                string name = effect.Element("id").Value;
                string[] val = effect.Element("value").Value.Split(':');
                list.Add(name, val[0], val[1]);
            }
        }

        private void AddDataToList(XElement Element, String Name1, String Name2, List<int> list)
        {
            if (Element.Element(Name1).Element(Name2) == null)
                return;
            if (Element.Element(Name1).Element(Name2).Value != "")
                foreach (string quest in Element.Element(Name1).Element(Name2).Value.Split(','))
                    list.Add(int.Parse(quest));
        }

        private int ParseIntIfNotEmpty(XElement Element, String Name1, String Name2, int defaultValue)
        {
            if (Element.Element(Name1).Element(Name2) == null)
                return defaultValue;
            if (!Element.Element(Name1).Element(Name2).Value.Equals(""))
                return int.Parse(Element.Element(Name1).Element(Name2).Value);
            return defaultValue;
        }

        private void ParseDialogsTexts(String DialogsXMLFile, NPCDicts target)
        { 
            doc = XDocument.Load(DialogsXMLFile);
            foreach (XElement npc in doc.Root.Elements())
            {
                string npc_name = npc.Element("Name").Value.ToString().Trim();
                if (!target.ContainsKey(npc_name))
                {
                    MessageBox.Show("В DialogsData отсутствует NPC:" + npc_name, "Ошибка парсинга текстов");
                    continue;
                }
                foreach (XElement dialog in npc.Elements("Dialog"))
                {
                    int DialogID = int.Parse(dialog.Element("ID").Value);

                    if (!target[npc_name].ContainsKey(DialogID))
                    {
                        string msg = "Проблема с диалогом ID:" + DialogID.ToString() + ". У NPC в DialogData отсутствует диалог с таким ID";
                        if (dialog.Element("Title") != null)
                            msg += " Title:" + dialog.Element("Title").Value.Trim();
                        MessageBox.Show(msg, "Ошибка парсинга текстов");
                        continue;
                    }
                   
                    if (dialog.Element("Title") != null)
                    {
                       
                        target[npc_name][DialogID].Title = dialog.Element("Title").Value.Trim();
                    }
                    if (dialog.Element("Text") != null)
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
                       new XElement("Version", dialog.version.ToString()));
                    if (dialog.Title != "")
                        element.Add(new XElement("Title", dialog.Title));
                    if (dialog.Text != "")
                        element.Add(new XElement("Text", dialog.Text));

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
                       new XElement("ID", dialog.DialogID.ToString()));
                    if (dialog.Precondition.Exists())
                    {
                         element.Add(new XElement("Precondition"));
                    
                    if (dialog.Precondition.ListOfNecessaryQuests.Any())
                    {
                        element.Element("Precondition").Add(new XElement("ListOfNecessaryQuests"));
                        if (dialog.Precondition.ListOfNecessaryQuests.ListOfCompletedQuests.Any())
                            element.Element("Precondition").Element("ListOfNecessaryQuests").Add(new XElement("listOfCompletedQuests",
                                          Global.GetListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfCompletedQuests, 
                                                                dialog.Precondition.ListOfNecessaryQuests.conditionOfCompletedQuests)));
                        if (dialog.Precondition.ListOfNecessaryQuests.ListOfOpenedQuests.Any())
                            element.Element("Precondition").Element("ListOfNecessaryQuests").Add(new XElement("listOfOpenedQuests",
                                          Global.GetListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfOpenedQuests,
                                                                dialog.Precondition.ListOfNecessaryQuests.conditionOfOpenedQuests)));
                        if (dialog.Precondition.ListOfNecessaryQuests.ListOfFailQuests.Any())
                            element.Element("Precondition").Element("ListOfNecessaryQuests").Add(new XElement("listOfFailQuests",
                                          Global.GetListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfFailQuests,
                                                                dialog.Precondition.ListOfNecessaryQuests.conditionOfFailQuests)));
                        if (dialog.Precondition.ListOfNecessaryQuests.ListOfOnTestQuests.Any())
                            element.Element("Precondition").Element("ListOfNecessaryQuests").Add(new XElement("listOfOnTestQuests",
                                          Global.GetListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfOnTestQuests,
                                                                dialog.Precondition.ListOfNecessaryQuests.conditionOfOnTestQuest)));
                        if (dialog.Precondition.ListOfNecessaryQuests.ListOfCounters.Any())
                            element.Element("Precondition").Element("ListOfNecessaryQuests").Add(new XElement("listOfOmniCounters",
                                          Global.GetListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfCounters,
                                                                dialog.Precondition.ListOfNecessaryQuests.conditionOfCounterss)));
                        if (dialog.Precondition.ListOfNecessaryQuests.ListOfRepeat.Any())
                            element.Element("Precondition").Element("ListOfNecessaryQuests").Add(new XElement("listOfRepeat",
                                          Global.GetListAsString(dialog.Precondition.ListOfNecessaryQuests.ListOfRepeat,
                                                                dialog.Precondition.ListOfNecessaryQuests.conditionOfRepeat)));
                        if (dialog.Precondition.ListOfNecessaryQuests.ListOfMassQuests != "")
                            element.Element("Precondition").Element("ListOfNecessaryQuests").Add(new XElement("listOfMassQuests",
                                                                     dialog.Precondition.ListOfNecessaryQuests.ListOfMassQuests.Replace(',', dialog.Precondition.ListOfNecessaryQuests.conditionOfMassQuests)));
                    }
                    if (dialog.Precondition.ListOfMustNoQuests.Any())
                    {
                        element.Element("Precondition").Add(new XElement("ListOfMustNoQuests"));
                        if (dialog.Precondition.ListOfMustNoQuests.ListOfCompletedQuests.Any())
                            element.Element("Precondition").Element("ListOfMustNoQuests").Add(new XElement("listOfCompletedQuests",
                                          Global.GetListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfCompletedQuests,
                                                                dialog.Precondition.ListOfMustNoQuests.conditionOfCompletedQuests)));
                        if (dialog.Precondition.ListOfMustNoQuests.ListOfOpenedQuests.Any())
                            element.Element("Precondition").Element("ListOfMustNoQuests").Add(new XElement("listOfOpenedQuests",
                                          Global.GetListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfOpenedQuests,
                                                                dialog.Precondition.ListOfMustNoQuests.conditionOfOpenedQuests)));
                        if (dialog.Precondition.ListOfMustNoQuests.ListOfFailQuests.Any())
                            element.Element("Precondition").Element("ListOfMustNoQuests").Add(new XElement("listOfFailQuests",
                                          Global.GetListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfFailQuests,
                                                                dialog.Precondition.ListOfMustNoQuests.conditionOfFailQuests)));
                        if (dialog.Precondition.ListOfMustNoQuests.ListOfOnTestQuests.Any())
                            element.Element("Precondition").Element("ListOfMustNoQuests").Add(new XElement("listOfOnTestQuests",
                                          Global.GetListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfOnTestQuests,
                                                                dialog.Precondition.ListOfMustNoQuests.conditionOfOnTestQuest)));
                        if (dialog.Precondition.ListOfMustNoQuests.ListOfCounters.Any())
                            element.Element("Precondition").Element("ListOfMustNoQuests").Add(new XElement("listOfOmniCounters",
                                          Global.GetListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfCounters,
                                                                dialog.Precondition.ListOfMustNoQuests.conditionOfCounterss)));
                        if (dialog.Precondition.ListOfMustNoQuests.ListOfRepeat.Any())
                            element.Element("Precondition").Element("ListOfMustNoQuests").Add(new XElement("listOfRepeat",
                                          Global.GetListAsString(dialog.Precondition.ListOfMustNoQuests.ListOfRepeat,
                                                                dialog.Precondition.ListOfMustNoQuests.conditionOfRepeat)));
                        if (dialog.Precondition.ListOfMustNoQuests.ListOfMassQuests != "")
                            element.Element("Precondition").Element("ListOfMustNoQuests").Add(new XElement("listOfMassQuests",
                                                                     dialog.Precondition.ListOfMustNoQuests.ListOfMassQuests.Replace(',', dialog.Precondition.ListOfMustNoQuests.conditionOfMassQuests)));
                    }
                    if (dialog.Precondition.clanOptions != "")
                        element.Element("Precondition").Add(new XElement("clanOptions", dialog.Precondition.clanOptions));
                   
                   
                    if (dialog.Precondition.MustNoEffects.Any())
                        element.Element("Precondition").Add(dialog.Precondition.getMustNoEffects());
                    if (dialog.Precondition.NecessaryEffects.Any())
                        element.Element("Precondition").Add(dialog.Precondition.getNecessaryEffects());
                    if (dialog.Precondition.Skills.Any())
                        element.Element("Precondition").Add(dialog.Precondition.Skills.getSkills());
                    if (dialog.Precondition.PlayerLevel != "" && dialog.Precondition.PlayerLevel != ":")
                        element.Element("Precondition").Add(new XElement("PlayerLevel", dialog.Precondition.PlayerLevel));
                    if (dialog.Precondition.playerCombatLvl != "" && dialog.Precondition.playerCombatLvl != ":")
                        element.Element("Precondition").Add(new XElement("playerCombatLvl", dialog.Precondition.playerCombatLvl));
                    if (dialog.Precondition.playerSurvLvl != "" && dialog.Precondition.playerSurvLvl != ":")
                        element.Element("Precondition").Add(new XElement("playerSurvLvl", dialog.Precondition.playerSurvLvl));
                    if (dialog.Precondition.playerOtherLvl != "" && dialog.Precondition.playerOtherLvl != ":")
                        element.Element("Precondition").Add(new XElement("playerOtherLvl", dialog.Precondition.playerOtherLvl));
                    if (dialog.Precondition.getReputation() != "")
                        element.Element("Precondition").Add(new XElement("Reputation", dialog.Precondition.getReputation()));
                    if (dialog.Precondition.KarmaPK.Any())
                        element.Element("Precondition").Add(new XElement("KarmaPK", Global.GetListAsString(dialog.Precondition.KarmaPK)));
                    }
                    if (dialog.Precondition.forDev)
                        element.Element("Precondition").Add(new XElement("forDev", Global.GetBoolAsString(dialog.Precondition.forDev)));

                    if (dialog.Precondition.items.itemCategory != -1)
                    {
                        element.Element("Precondition").Add(new XElement("items", new XElement("itemCategory", dialog.Precondition.items.itemCategory.ToString())));
                    }
                    else if (dialog.Precondition.items.typeOfItems.Any())
                    {
                        element.Element("Precondition").Add(new XElement("items", ""));
                        element.Element("Precondition").Element("items").Add(new XElement("typeOfItems", Global.GetListAsString(dialog.Precondition.items.typeOfItems)));
                        element.Element("Precondition").Element("items").Add(new XElement("numOfItems", Global.GetListAsString(dialog.Precondition.items.numOfItems)));
                        element.Element("Precondition").Element("items").Add(new XElement("attrOfItems", Global.GetListAsString(dialog.Precondition.items.attrOfItems)));
                    }
                    if (dialog.Actions.Any())
                    {
                        element.Add(new XElement("Actions", ""));
                    
                        if (dialog.Actions.Exit)
                            element.Element("Actions").Add(new XElement("Exit", Global.GetBoolAsString(dialog.Actions.Exit)));
                        if (dialog.Actions.ToDialog != 0)
                            element.Element("Actions").Add(new XElement("ToDialog", Global.GetIntAsString(dialog.Actions.ToDialog)));
                        if (dialog.Actions.Data != "")
                            element.Element("Actions").Add(new XElement("Data", dialog.Actions.Data));
                        if (dialog.Actions.Event.Value != 0)
                            element.Element("Actions").Add(new XElement("Event", dialog.Actions.Event.Value.ToString()));
                        if (dialog.Actions.GetQuests.Any())
                            element.Element("Actions").Add(new XElement("GetQuest", Global.GetListAsString(dialog.Actions.GetQuests)));
                        if (dialog.Actions.CompleteQuests.Any())
                            element.Element("Actions").Add(new XElement("CompleteQuest", Global.GetListAsString(dialog.Actions.CompleteQuests)));
                        if (dialog.Actions.CancelQuests.Any())
                            element.Element("Actions").Add(new XElement("CancelQuest", Global.GetListAsString(dialog.Actions.CancelQuests)));
                        if (dialog.Actions.FailQuests.Any())
                            element.Element("Actions").Add( new XElement("FailQuest", Global.GetListAsString(dialog.Actions.FailQuests)));
                        if (dialog.Actions.actionCamera.Any())
                        {
                            element.Element("Actions").Add(new XElement("GoToCamera", dialog.Actions.actionCamera));
                            if (dialog.Actions.actionCameraSmoothly)
                                element.Element("Actions").Add(new XElement("CameraSmoothly", Global.GetBoolAsString(dialog.Actions.actionCameraSmoothly)));
                        }
                        if (dialog.Actions.actionAnimationPlayer.Any())
                            element.Element("Actions").Add(new XElement("AnimationPlayer", dialog.Actions.actionAnimationPlayer));
                        if (dialog.Actions.actionAnimationNPC.Any())
                            element.Element("Actions").Add(new XElement("AnimationNPC", dialog.Actions.actionAnimationNPC));
                        if (dialog.Actions.actionItemNPC.Any())
                            element.Element("Actions").Add(new XElement("ItemNPC", dialog.Actions.actionItemNPC));
                        if (dialog.Actions.actionAvatarPoint.Any())
                            element.Element("Actions").Add(new XElement("AvatarPoint", dialog.Actions.actionAvatarPoint));
                        if (dialog.Actions.actionPlaySound.Any())
                            element.Element("Actions").Add(new XElement("PlaySound", dialog.Actions.actionPlaySound));

                    }

                    if (dialog.Nodes.Any())
                        element.Add(new XElement("Nodes", Global.GetListAsString(dialog.Nodes)));
                    if (dialog.CheckNodes.Any())
                        element.Add(new XElement("CheckNodes", Global.GetListAsString(dialog.CheckNodes)));
                    if (dialog.coordinates.RootDialog)
                        element.Add(new XElement("RootDialog", Global.GetBoolAsString(dialog.coordinates.RootDialog)));
                    if (dialog.coordinates.Active)
                        element.Add(new XElement("Active", Global.GetBoolAsString(dialog.coordinates.Active)));
                    if (dialog.DebugData != "") element.Add(new XElement("DebugData",dialog.DebugData ));
                    if (dialog.isAutoNode)
                    {
                        element.Add(new XElement("isAutoNode", Global.GetBoolAsString(dialog.isAutoNode)));
                        if (dialog.defaultNode.Any()) element.Add(new XElement("defaultNode", dialog.defaultNode));
                    }

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
                        new XElement("X", Convert.ToString(dialog.coordinates.X)),
                        new XElement("Y", Convert.ToString(dialog.coordinates.Y))));                  
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
                    float x = float.Parse(dialog.Element("X").Value);
                    float y = float.Parse(dialog.Element("Y").Value);
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