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
        public ListOfQuests ListOfOpenedQuests;
        public ListOfQuests ListOfOnTestQuests;
        public ListOfQuests ListOfFailedQuests;
        public string ListOfMassQuests;

        public CDialogPreconditionQuests()
        {
            this.ListOfCompletedQuests = new ListOfQuests();
            this.ListOfOpenedQuests = new ListOfQuests();
            this.ListOfOnTestQuests = new ListOfQuests();
            this.ListOfFailedQuests = new ListOfQuests();
            this.ListOfMassQuests = "";
        }

        public bool Any()
        {
            if (ListOfCompletedQuests.Any() || ListOfOnTestQuests.Any() || ListOfOpenedQuests.Any() || ListOfFailedQuests.Any() || ListOfMassQuests != "")
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
            copy.ListOfFailedQuests = this.ListOfFailedQuests;
            copy.ListOfMassQuests = this.ListOfMassQuests;
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
                result += Global.GetNamedList(" Провалено: ", ListOfFailedQuests);
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
        public int PlayerLevel;
        public string playerCombatLvl;
        public string playerSurvLvl;
        public string playerOtherLvl;
        public Dictionary<int, List<double>> Reputation = new Dictionary<int, List<double>>();
        public List<int> KarmaPK = new List<int>();
        public List<DialogEffect> NecessaryEffects = new List<DialogEffect>();
        public List<DialogEffect> MustNoEffects = new List<DialogEffect>();

        public object Clone()
        {
            CDialogPrecondition copy = new CDialogPrecondition();
            copy.clanOptions = this.clanOptions;
            copy.ListOfNecessaryQuests = (CDialogPreconditionQuests)this.ListOfNecessaryQuests.Clone();
            copy.ListOfMustNoQuests = (CDialogPreconditionQuests)this.ListOfMustNoQuests.Clone();
            copy.Reputation = this.Reputation;
            copy.KarmaPK = this.KarmaPK;
            copy.PlayerLevel = this.PlayerLevel;
            copy.playerCombatLvl = this.playerCombatLvl;
            copy.playerSurvLvl = this.playerSurvLvl;
            copy.playerOtherLvl = this.playerOtherLvl;
            copy.NecessaryEffects = this.NecessaryEffects;
            copy.MustNoEffects = this.MustNoEffects;
            return copy;
        }

        public CDialogPrecondition()
        {
            this.ListOfNecessaryQuests = new CDialogPreconditionQuests();
            this.ListOfMustNoQuests = new CDialogPreconditionQuests();
            this.clanOptions = "";
            this.Reputation = new Dictionary<int, List<double>>();
            this.KarmaPK = new List<int>();
            this.PlayerLevel = 0;
            this.playerCombatLvl = "";
            this.playerSurvLvl = "";
            this.playerOtherLvl = "";
            this.NecessaryEffects = new List<DialogEffect>();
            this.MustNoEffects = new List<DialogEffect>();
        }
        public bool Any()
        {
            if (ListOfMustNoQuests.Any() || ListOfNecessaryQuests.Any() || NecessaryEffects.Any() || MustNoEffects.Any() || Reputation.Any())
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

        public Actions()
        {
            this.Exit = new bool();
            this.CompleteQuests = new ListOfQuests();
            this.GetQuests = new ListOfQuests();
            this.CancelQuests = new ListOfQuests();
            this.FailQuests = new ListOfQuests();
            this.Data = "";
        }

        public bool Exists()
        {
            return (GetQuests.Count > 0 || CompleteQuests.Count > 0 || CancelQuests.Count > 0 || FailQuests.Count > 0 || Event.Value != 0);
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
        public int X;
        public int Y;
        public bool RootDialog;
        public bool Active;
        public NodeCoordinates()
        {
            X = 0;
            Y = 0;
            RootDialog = false;
            Active = true;
        }
        public NodeCoordinates(int x, int y, bool root, bool active)
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
        public string Holder;
        public string Title;
        public string Text;
        public CDialogPrecondition Precondition;
        public Actions Actions;
        public List<int> Nodes;
        public int version;
        public NodeCoordinates coordinates;
        public string DebugData;

        public CDialog(string Holder, string Title, string Text, CDialogPrecondition Precondition,
                    Actions Actions, List<int> Nodes, int DialogID, int version, NodeCoordinates Coordinates, string DebugData = "")
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
            this.DebugData = "";
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
