using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    using ListOfQuests = List<int>;

    public class CDialogPreconditionQuests : ICloneable
    {
        public ListOfQuests ListOfCompletedQuests;
        public char conditionOfCompletedQuests;
        public ListOfQuests ListOfOpenedQuests;
        public char conditionOfOpenedQuests;
        public ListOfQuests ListOfOnTestQuests;
        public char conditionOfOnTestQuest;
        public ListOfQuests ListOfCounters;
        public char conditionOfCounterss;
        public string ListOfMassQuests;
        public char conditionOfMassQuests;
        public ListOfQuests ListOfRepeat;
        public char conditionOfRepeat;
        public CDialogPreconditionQuests()
        {
            this.ListOfCompletedQuests = new ListOfQuests();
            this.ListOfOpenedQuests = new ListOfQuests();
            this.ListOfOnTestQuests = new ListOfQuests();
            this.ListOfCounters = new ListOfQuests();
            this.ListOfRepeat = new ListOfQuests();
            this.ListOfMassQuests = "";
            this.conditionOfCompletedQuests = ',';
            this.conditionOfOpenedQuests = ',';
            this.conditionOfOnTestQuest = ',';
            this.conditionOfCounterss = ',';
            this.conditionOfMassQuests = ',';
            this.conditionOfRepeat = ',';
        }

        public bool Any()
        {
            if (ListOfCompletedQuests.Any() || ListOfOnTestQuests.Any() || ListOfOpenedQuests.Any() ||
                ListOfCounters.Any() || ListOfMassQuests != "" || ListOfRepeat.Any())
                return true;
            else
                return false;
        }

        public object Clone()
        {
            CDialogPreconditionQuests copy = new CDialogPreconditionQuests();
            copy.ListOfCompletedQuests = this.ListOfCompletedQuests;
            copy.ListOfOpenedQuests = this.ListOfOpenedQuests;
            copy.ListOfOnTestQuests = this.ListOfOnTestQuests;
            copy.ListOfMassQuests = this.ListOfMassQuests;
            copy.ListOfCounters = this.ListOfCounters;
            copy.ListOfRepeat = this.ListOfRepeat;
            copy.conditionOfCompletedQuests = this.conditionOfCompletedQuests;
            copy.conditionOfOpenedQuests = this.conditionOfOpenedQuests;
            copy.conditionOfOnTestQuest = this.conditionOfOnTestQuest;
            copy.conditionOfCounterss = this.conditionOfCounterss;
            copy.conditionOfMassQuests = this.conditionOfMassQuests;
            copy.conditionOfRepeat = this.conditionOfRepeat;
            return copy;
        }

        public string GetAsString()
        {
            if (Any())
            {
                string result = "\n";
                result += Global.GetNamedList(" Открыто: ", ListOfOpenedQuests);
                result += Global.GetNamedList(" На проверке: ", ListOfOnTestQuests);
                result += Global.GetNamedList(" Закрыто: ", ListOfCompletedQuests);
                result += Global.GetNamedList(" Счётчики: ", ListOfCounters);
                result += " Массовые: " + ListOfMassQuests;
                return result;
            }
            else
                return "";
        }
    }

    //! Условия появления диалога в игре - открытые/закрытые/проваленные квесты, урвоень игрока, репутация у фракций, карма ПК
    public class CDialogPrecondition : ICloneable
    {
        public CDialogPreconditionQuests ListOfNecessaryQuests;
        public CDialogPreconditionQuests ListOfMustNoQuests;
        public string clanOptions;
        public string PlayerLevel;
        public string playerCombatLvl;
        public string playerSurvLvl;
        public string playerOtherLvl;
        public ListDialogSkills Skills = new ListDialogSkills();
        public Dictionary<int, List<double>> Reputation = new Dictionary<int, List<double>>();
        public List<int> KarmaPK = new List<int>();
        public List<DialogEffect> NecessaryEffects = new List<DialogEffect>();
        public List<DialogEffect> MustNoEffects = new List<DialogEffect>();
        public bool forDev;
       

        public object Clone()
        {
            CDialogPrecondition copy = new CDialogPrecondition();
            copy.clanOptions = this.clanOptions;
            copy.ListOfNecessaryQuests = (CDialogPreconditionQuests)this.ListOfNecessaryQuests.Clone();
            copy.ListOfMustNoQuests = (CDialogPreconditionQuests)this.ListOfMustNoQuests.Clone();
            copy.Reputation = this.Reputation;
            copy.Skills = this.Skills;
            copy.KarmaPK = this.KarmaPK;
            copy.PlayerLevel = this.PlayerLevel;
            copy.playerCombatLvl = this.playerCombatLvl;
            copy.playerSurvLvl = this.playerSurvLvl;
            copy.playerOtherLvl = this.playerOtherLvl;
            copy.NecessaryEffects = this.NecessaryEffects;
            copy.MustNoEffects = this.MustNoEffects;
            copy.forDev = this.forDev;
            return copy;
        }

        public CDialogPrecondition()
        {
            this.ListOfNecessaryQuests = new CDialogPreconditionQuests();
            this.ListOfMustNoQuests = new CDialogPreconditionQuests();
            this.clanOptions = "";
            this.Reputation = new Dictionary<int, List<double>>();
            this.Skills = new ListDialogSkills();
            this.KarmaPK = new List<int>();
            this.PlayerLevel = "";
            this.playerCombatLvl = "";
            this.playerSurvLvl = "";
            this.playerOtherLvl = "";
            this.NecessaryEffects = new List<DialogEffect>();
            this.MustNoEffects = new List<DialogEffect>();
            this.forDev = false;

        }

        public bool Exists()
        {
            return this.Any() || KarmaPK.Any() || PlayerLevel != "" || playerCombatLvl != "" ||
                playerSurvLvl != "" || playerOtherLvl != "" || this.clanOptions != "" || Skills.Any() || forDev;
        }

        public bool Any()
        {
            if (ListOfMustNoQuests.Any() || ListOfNecessaryQuests.Any() || NecessaryEffects.Any() || MustNoEffects.Any() || Reputation.Any() ||
                PlayerLevel != "" || playerCombatLvl != "" || playerSurvLvl != "" || playerOtherLvl != "" || Skills.Any())
                return true;
            else
                return false;
        }

        public string GetAsString()
        {
            if (Any())
            {
                string result = "";
                result += (ListOfNecessaryQuests.Any()) ? ("\nДолжно быть:" + ListOfNecessaryQuests.GetAsString()) : ("");
                result += (ListOfMustNoQuests.Any()) ? ("\nНе должно быть: " + ListOfMustNoQuests.GetAsString()) : ("");
                return result;
            }
            else
                return "";
        }

        public string getReputation()
        {
            string result = "";
            foreach (int key in this.Reputation.Keys)
            {
                if (this.Reputation[key].Count == 2)
                {
                    if (result != "")
                        result += ";";
                    result += key.ToString() + ":";
                    result += this.Reputation[key][0].ToString(System.Globalization.CultureInfo.InvariantCulture) + ":";
                    result += this.Reputation[key][1].ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                else if (this.Reputation[key].Count == 3)  //костыль для плавного перехода между старой и новой версией
                {
                    if (result != "")
                        result += ";";
                    double A = this.Reputation[key][1];
                    double B = this.Reputation[key][2];
                    if (A == 0.0)
                        A = double.NegativeInfinity;
                    if (B == 0.0)
                        B = double.PositiveInfinity;
                    result = key.ToString() + ":";
                    result += A.ToString(System.Globalization.CultureInfo.InvariantCulture) + ":";
                    result += B.ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            return result;
        }

        public XElement getNecessaryEffects()
        {
            XElement effects = null;
            if (!NecessaryEffects.Any())
                return null;

            foreach (DialogEffect effect in this.NecessaryEffects)
            {
                if (effects == null)
                    effects = new XElement("NecessaryEffects",new XElement("Effect",
                                                                    new XElement("id", effect.getID()),
                                                                    new XElement("stack", effect.getStacks())));
                else
                    effects.Add(new XElement("Effect",
                                                                    new XElement("id", effect.getID()),
                                                                    new XElement("stack", effect.getStacks())));
            }

            return effects;

        }
        public XElement getMustNoEffects()
        {
            XElement effects = null;
            if (!MustNoEffects.Any())
                return null;
            
            foreach (DialogEffect effect in this.MustNoEffects)
            {
                if (effects == null)
                    effects = new XElement("MustNoEffects", new XElement("Effect",
                                                      new XElement("id", effect.getID()),
                                                      new XElement("stack", effect.getStacks())));
                else
                    effects.Add(new XElement("Effect",
                                                      new XElement("id", effect.getID()),
                                                      new XElement("stack", effect.getStacks())));
            }
            return effects;

        }
    }


    //! Действия диалога - торговля, починка, телепортация, окончание разговора и т.д.
    public class Actions
    {
        public bool Exit;
        public DialogEvent Event;
        public int ToDialog;
        public ListOfQuests CompleteQuests;
        public ListOfQuests GetQuests;
        public ListOfQuests CancelQuests;
        public ListOfQuests FailQuests;
        public string Data;
        public string actionCamera;
        public bool actionCameraSmoothly;
        public string actionAnimationPlayer;
        public string actionAnimationNPC;
        public string actionItemNPC;
        public string actionAvatarPoint;
        public string actionPlaySound;

        public Actions()
        {
            this.ToDialog = new int();
            //this.Event = new DialogEvent();
            this.Exit = new bool();
            this.CompleteQuests = new ListOfQuests();
            this.GetQuests = new ListOfQuests();
            this.CancelQuests = new ListOfQuests();
            this.FailQuests = new ListOfQuests();
            this.Data = "";
            this.actionCamera = "";
            this.actionCameraSmoothly = true;
            this.actionAnimationPlayer = "";
            this.actionAnimationNPC = "";
            this.actionAvatarPoint = "";
            this.actionPlaySound = "";
            this.actionItemNPC = "";
        }


        public bool Any()
        {
            return GetQuests.Any() || CompleteQuests.Any() || CancelQuests.Any() || FailQuests.Any() || (Event != null && Event.Value != 0) || (ToDialog != 0) || Exit ||
                actionCamera != "" || actionAnimationPlayer != "" || actionAnimationNPC != "" || actionAvatarPoint != "" || actionPlaySound != "" ;
        }

        public bool Exists()
        {
            return (GetQuests.Count > 0 || CompleteQuests.Count > 0 || CancelQuests.Count > 0 || FailQuests.Count > 0 || (Event!= null && Event.Value != 0));
        }

        public bool CheckAndGetString(out string ActionString)
        {
            ActionString = "";
            if (Exists())
            {
                string tooltip = (Event.Value != 0) ? ("\n" + Event.Display) : ("");
                tooltip += Global.GetNamedList("\nВзять: ", GetQuests);
                tooltip += Global.GetNamedList("\nЗакрыть: ", CompleteQuests);
                tooltip += Global.GetNamedList("\nОтменить: ", CancelQuests);
                tooltip += Global.GetNamedList("\nПровалить: ", FailQuests);
                ActionString = tooltip;
                return true;
            }
            else
                return false;
        }

        public string GetAsString()
        {
            string action = "";
            action += Global.GetNamedList(" Взять: ", GetQuests);
            action += Global.GetNamedList(" Закрыть: ", CompleteQuests);
            action += Global.GetNamedList(" Отменить: ", CancelQuests);
            action += Global.GetNamedList(" Провалить: ", FailQuests);
            if (action.Length > 0)
                action = "(" + action + ")";
            return action;
        }
    }


    //! Класс параметров узла диалога на графе - координаты и флаг "корневой"
    public class NodeCoordinates
    {
        public float X;
        public float Y;
        public bool RootDialog;
        public bool Active;
        public NodeCoordinates()
        {
            X = 0;
            Y = 0;
            RootDialog = false;
            Active = true;
        }
        public NodeCoordinates(float x, float y, bool root, bool active)
        {
            X = x;
            Y = y;
            RootDialog = root;
            Active = active;
        }
    }


    //! Класс диалога (одна ветка в xml файле)
    public class CDialog : ICloneable
    {
        public int DialogID;
        public string Holder; //имя NPC
        public string Title;
        public string Text;
        public CDialogPrecondition Precondition; //Условия
        public Actions Actions; //Действия диалога
        public List<int> Nodes;
        public int version; //Версия для перевода
        public NodeCoordinates coordinates;
        public string DebugData; //Для теста, пока фича не реализована в эдиторе, пользуются этой нодой
        public bool isAutoNode; //Диалог автоматически перебрасывает на рандомную ноду
        public string defaultNode; //Диалог по-умолчанию, если другие не подходят по условиям(для автопереходилки)

        public CDialog(string Holder, string Title, string Text, CDialogPrecondition Precondition,
                    Actions Actions, List<int> Nodes, int DialogID, int version, NodeCoordinates Coordinates, string DebugData = "", bool isAutoNode = false, string defaultNode = "")
        {
            this.Holder = Holder;
            this.Title = Title;
            this.Text = Text;
            this.Precondition = Precondition;
            this.Actions = Actions;
            this.Nodes = Nodes;
            this.DialogID = DialogID;
            this.version = version;
            this.coordinates = Coordinates;
            this.DebugData = DebugData;
            this.isAutoNode = isAutoNode;
            this.defaultNode = defaultNode;
        }
        public CDialog()
        {
            this.Holder = "";
            this.Title = "";
            this.Text = "";
            this.Precondition = new CDialogPrecondition();
            this.Actions = new Actions();
            this.Nodes = new List<int>();
            this.DialogID = new int();
            this.version = new int();
            coordinates = new NodeCoordinates();
            this.isAutoNode = false;
            this.DebugData = "";
            this.defaultNode = "";
        }
        public object Clone()
        {
            CDialog copy = new CDialog();
            copy.Actions = this.Actions;
            copy.coordinates = this.coordinates;
            copy.DialogID = this.DialogID;
            copy.Holder = this.Holder;
            copy.Nodes = new List<int>(this.Nodes);
            copy.Precondition = this.Precondition;
            copy.Text = this.Text;
            copy.Title = this.Title;
            copy.version = this.version;
            copy.DebugData = this.DebugData;
            copy.isAutoNode = this.isAutoNode;
            copy.defaultNode = this.defaultNode;
            return copy;
        }
        // Копирование всех нетекстовых полей (сделано для синхронизации данных, не изменяя перевода)
        public void InsertNonTextData(CDialog source)
        {
            this.Precondition = source.Precondition;
            this.Actions = source.Actions;
            this.Nodes = new List<int>(source.Nodes);
            this.coordinates.Active = source.coordinates.Active;
            this.coordinates.RootDialog = source.coordinates.RootDialog;
        }

        public string GetNodeTooltip()
        {
            string condition = Precondition.GetAsString();
            string actionString;
            Actions.CheckAndGetString(out actionString);
            string result = (condition + actionString).TrimStart();
            return result;
        }
    }

}
