using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StalkerOnlineQuesterEditor
{
    using ListOfQuests = List<int>;

    public class CDialogPreconditionQuests : ICloneable
    {
        public ListOfQuests ListOfCompletedQuests;
        public ListOfQuests ListOfOpenedQuests;
        public ListOfQuests ListOfOnTestQuests;
        public ListOfQuests ListOfFailedQuests;
        
        public CDialogPreconditionQuests()
        {
            this.ListOfCompletedQuests = new ListOfQuests();
            this.ListOfOpenedQuests = new ListOfQuests();
            this.ListOfOnTestQuests = new ListOfQuests();
            this.ListOfFailedQuests = new ListOfQuests();
        }

        public bool Any()
        {
            if (ListOfCompletedQuests.Any() || ListOfOnTestQuests.Any() || ListOfOpenedQuests.Any() || ListOfFailedQuests.Any())
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
            return copy;
        }
    }

    public class CDialogPrecondition : ICloneable
    {
        public CDialogPreconditionQuests ListOfNecessaryQuests;
        public CDialogPreconditionQuests ListOfMustNoQuests;
        //public bool CheckClanID;
        //public bool CheckClan;
        public List<int> tests;
        public int PlayerLevel;
        public Dictionary<int, List<double>> Reputation = new Dictionary<int, List<double>>();
        public List<int> KarmaPK = new List<int>();

        public object Clone()
        {
            CDialogPrecondition copy = new CDialogPrecondition();
            //copy.CheckClanID = this.CheckClanID;
            //copy.CheckClan = this.CheckClan;
            foreach (int test in this.tests)
                copy.tests.Add(test);
            copy.ListOfNecessaryQuests = (CDialogPreconditionQuests)this.ListOfNecessaryQuests.Clone();
            copy.ListOfMustNoQuests = (CDialogPreconditionQuests)this.ListOfMustNoQuests.Clone();
            copy.Reputation = this.Reputation;
            copy.KarmaPK = this.KarmaPK;
            copy.PlayerLevel = this.PlayerLevel;
            return copy;
        }

        public CDialogPrecondition()
        {
            this.ListOfNecessaryQuests = new CDialogPreconditionQuests();
            this.ListOfMustNoQuests = new CDialogPreconditionQuests();
            this.tests = new List<int>();
            //this.CheckClanID = false;
            //this.CheckClan = false;
            this.Reputation = new Dictionary<int, List<double>>();
            this.KarmaPK = new List<int>();
            this.PlayerLevel = 0;
        }
        public bool Any()
        {
            if (ListOfMustNoQuests.Any() || ListOfNecessaryQuests.Any())
                return true;
            else
                return false;
        }

        public string getReputation()
        {
            string result = "";
            //double.NegativeInfinity.ToString(System.Globalization.CultureInfo.InvariantCulture);
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
    }
    //! Действия диалога - торговля, починка, телепортация, окончание разговора и т.д.
    public class Actions //: ICloneable
    {
        public bool Exit;
        //public bool Trade;
        public int Event;
        public int ToDialog;
        public ListOfQuests CompleteQuests;
        public ListOfQuests GetQuests;
        public string Data;

        //public object Clone()
        //{
        //    Actions copy = new Actions();
        //    copy.Exit = this.Exit;
        //    copy.Event = this.Event;
        //    copy.ToDialog = this.ToDialog;
        //    copy.CompleteQuests = this.CompleteQuests;
        //    copy.GetQuests = this.GetQuests;
        //    copy.Data = this.Data;
        //    return copy;
        //}

        public Actions()
        {
            this.Exit = new bool();
            this.Event = new int();
            //this.Trade = new bool();
            this.CompleteQuests = new ListOfQuests();
            this.GetQuests = new ListOfQuests();
            this.Data = "";
        }

        public bool CheckAndGetString(out string ActionString)
        {
            ActionString = "";
            if (GetQuests.Count > 0 || CompleteQuests.Count > 0 || Event != 0)
            {
                //string action = dialogEvents.GetEventName(dialog.Actions.Event) + "\n";
                string tooltip = "Взять: " + Global.GetListAsString(GetQuests) + "\nЗакрыть: " + Global.GetListAsString(CompleteQuests);
                return true;
            }
            else
                return false;
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
        //public List<string> Holder;
        public string Holder;
        public string Title;
        public string Text;
        public CDialogPrecondition Precondition;
        //public List<int> Actions;
        public Actions Actions;
        public List<int> Nodes;
        public int version;
        public NodeCoordinates coordinates;

        public CDialog(string Holder, string Title, string Text , CDialogPrecondition Precondition, 
                    Actions Actions, List<int> Nodes, int DialogID, int version, NodeCoordinates Coordinates)
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
        }
        public CDialog()
        {
            //this.Holder = new List<string>();
            this.Holder = "";
            this.Title = "";
            this.Text = "";
            this.Precondition = new CDialogPrecondition();
            this.Actions = new Actions();
            this.Nodes = new List<int>();
            this.DialogID = new int();
            this.version = new int();
            coordinates = new NodeCoordinates();
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
            return copy;
        }
        // Копирование всех нетекстовых полей (сделано для синхронизации данных, не изменяя перевода)
        public void InsertNonTextData(CDialog source)
        {
            //this.Holder = Holder;
            //this.Title = Title;
            //this.Text = Text;
            this.Precondition = source.Precondition;
            this.Actions = source.Actions;
            this.Nodes = new List<int>(source.Nodes);
            //this.DialogID = DialogID;
            //this.version = source.version;
            this.coordinates.Active = source.coordinates.Active;
            this.coordinates.RootDialog = source.coordinates.RootDialog;
        }
    }
    //! Класс квест - все параметры получения, выполнения и вознаграждения за квест
    public class CQuest : ICloneable
    {
        public int QuestID;
        public int Version;
        public CQuestInformation QuestInformation;
        public CQuestTarget Target;
        public CQuestPrecondition Precondition;
        public CQuestRules QuestRules;
        public CQuestReward Reward;
        public CQuestPenalty QuestPenalty;
        public CQuestAdditional Additional;


        public object Clone()
        {
            CQuest copy = new CQuest();
            copy.QuestID = this.QuestID;
            copy.Version = 0;
            copy.QuestInformation = (CQuestInformation)this.QuestInformation.Clone();
            copy.Precondition = (CQuestPrecondition)this.Precondition.Clone();
            copy.QuestRules = (CQuestRules)this.QuestRules.Clone();
            copy.Reward = (CQuestReward)this.Reward.Clone();
            copy.QuestPenalty = (CQuestPenalty)this.QuestPenalty.Clone();
            copy.Additional = (CQuestAdditional)this.Additional.Clone();
            copy.Target = (CQuestTarget)this.Target.Clone();
            return copy;
        }

        public CQuest()
        {
            this.QuestID = new int();
            this.Version = new int();
            this.QuestInformation = new CQuestInformation();
            this.Precondition = new CQuestPrecondition();
            this.QuestRules = new CQuestRules();
            this.Reward = new CQuestReward();
            this.QuestPenalty = new CQuestPenalty();
            this.Additional = new CQuestAdditional();
            this.Target = new CQuestTarget();
        }

        public CQuest(int questID,int Version, CQuestInformation questInformation,CQuestPrecondition precondition, CQuestRules questRules, CQuestReward reward, CQuestAdditional additional,CQuestTarget target, CQuestPenalty penalty)
        {
            this.QuestID = questID;
            this.Version = Version;
            this.QuestInformation = questInformation;
            this.Precondition = precondition;
            this.QuestRules = questRules;
            this.Reward = reward;
            this.Additional = additional;
            this.Target = target;
            this.QuestPenalty = penalty;
        }

        public void InsertNonTextData(CQuest source)
        {
            //this.QuestID = questID;
            //this.Version = Version;
            //this.QuestInformation. = source.QuestInformation;
            this.Precondition = (CQuestPrecondition) source.Precondition.Clone();
            this.QuestRules = (CQuestRules) source.QuestRules.Clone();
            this.Reward = (CQuestReward) source.Reward.Clone();
            this.Additional = (CQuestAdditional) source.Additional.Clone();
            this.Target = (CQuestTarget) source.Target.Clone();
            this.QuestPenalty = (CQuestPenalty) source.QuestPenalty.Clone();        
        }
    }
    //! Класс текстовой информации о квесте - название, описание, надписи на победу и проигрыш
    public class CQuestInformation : ICloneable
    {
    	public string Title;
		public string Description;
        public string onWin;
        public string onFailed;
        public Dictionary<int,QuestItemInfo> Items;

        public object Clone()
        {
            CQuestInformation copy = new CQuestInformation();
            copy.Title = (string)this.Title.Clone();
            copy.Description = (string)this.Description.Clone();
            copy.onWin = (string)this.onWin.Clone();
            copy.onFailed = (string)this.onFailed.Clone();
            foreach (KeyValuePair<int, QuestItemInfo> item in this.Items)
                copy.Items.Add(item.Key, (QuestItemInfo)item.Value.Clone());
            return copy;
        }
        
        public CQuestInformation()
        {
            this.Title = "";
            this.Description = "";
            this.onFailed = "";
            this.onWin = "";
            this.Items = new Dictionary<int, QuestItemInfo>();
        }

        public Dictionary<int, QuestItemInfo> getItems()
        {
            return Items;
        }
        public void addItem(int itemID, string title, string description, string activation)
        {
            this.Items.Add(itemID, new QuestItemInfo(title, description, activation));
        }
    }

    public class CQuestTarget : ICloneable
    {
        public int QuestType;
		public int ObjectType;
		public int NumOfObjects;
        public int ObjectAttr;
        public List<int> AObjectAttrs;
        public string AreaName;
        public float Time;
        public string ObjectName;
        public int IsGroup;
        public bool IsClan;
        public int onFin;

        public object Clone()
        {
            CQuestTarget copy = new CQuestTarget();

            copy.QuestType = this.QuestType;
            copy.ObjectType = this.ObjectType;
            copy.NumOfObjects = this.NumOfObjects;
            copy.ObjectAttr = this.ObjectAttr;
            copy.AObjectAttrs = new List<int>(this.AObjectAttrs);
            copy.AreaName = (string)this.AreaName.Clone();
            copy.Time = this.Time;
            copy.ObjectName = (string)this.ObjectName.Clone();
            copy.IsGroup = this.IsGroup;
            copy.IsClan = this.IsClan;
            copy.onFin = this.onFin;
            return copy;
        }

        public CQuestTarget()
        {
            this.QuestType = new int();
            this.ObjectType = new int();
            this.NumOfObjects = new int();
            this.ObjectAttr = new int();
            this.AreaName = "";
            this.Time = 0.0f;
            this.IsGroup = new int();
            this.IsClan = new bool();
            this.ObjectName = "";
            this.AObjectAttrs = new List<int>();
            this.onFin = new int();
        }
        public CQuestTarget(int QuestType)
        {
            this.QuestType = QuestType;
            this.ObjectType = new int();
            this.NumOfObjects = new int();
            this.ObjectName = "";
            this.AObjectAttrs = new List<int>();
        }
    }

    public class CQuestPrecondition : CDialogPreconditionQuests
    {
       	public int Repeat;
        public double TakenPeriod;
        public CQuestPrecondition() : base()
        {
            this.Repeat = new int();
            this.TakenPeriod = new double();
        }

        public object Clone()
        {
            CQuestPrecondition copy = new CQuestPrecondition();
            copy.ListOfCompletedQuests = new ListOfQuests(this.ListOfCompletedQuests);
            copy.ListOfOpenedQuests = new ListOfQuests(this.ListOfOpenedQuests);
            copy.ListOfOnTestQuests = new ListOfQuests(copy.ListOfOnTestQuests);
            copy.Repeat = this.Repeat;
            copy.TakenPeriod = this.TakenPeriod;
            return copy;
        }
    }

    public class CQuestRules : ICloneable
    {
        public List<int> TypeOfItems;
        public List<int> NumOfItems;
        public List<int> AttrOfItems;
        public List<int> Scenarios;
        public string TeleportTo;
        public int FailedIf;
        public int MaxGroup;
        public int MinGroup;
        public int MaxMember;
        public int MinMember;

        public object Clone()
        {
            CQuestRules copy = new CQuestRules();

            copy.TypeOfItems = new List<int>(this.TypeOfItems);
            copy.NumOfItems = new List<int>(this.NumOfItems);
            copy.AttrOfItems = new List<int>(this.AttrOfItems);
            copy.Scenarios = new List<int>(this.Scenarios);
            copy.TeleportTo = (string)this.TeleportTo.Clone();
            copy.FailedIf = this.FailedIf;
            copy.MaxGroup = this.MaxGroup;
            copy.MinGroup = this.MinGroup;
            copy.MaxMember = this.MaxMember;
            copy.MinMember = this.MinMember;
            return copy;
        }

        public CQuestRules()
        {
            this.TypeOfItems = new List<int>();
            this.NumOfItems = new List<int>();
            this.AttrOfItems = new List<int>();
            this.Scenarios = new List<int>();
            this.FailedIf = new int();
            this.MaxGroup = new int();
            this.MinGroup = new int();
            this.TeleportTo = "";
        }
    }

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
    //! Награда за успешное выполнение квеста
    public class CQuestReward : ICloneable
    {
        //! Опыт: боевой, выживания, поддержки
        public List<int> Experience;
        //! Тип предмета: ID предметов
        public List<int> TypeOfItems;
        //! Кол-во предметов
        public List<int> NumOfItems;
        //! Атрибут предметов: 0 - обычный, 1 - квестовый
        public List<int> AttrOfItems;
        //! Вероятность выпадения предметов
        public List<float> Probability;
        //! Денежное вознаграждение
        public float Credits;
        //! Словарь репутаций в награду, выглядит так <id фракции>:<значение награды>;
        public Dictionary<int, int> Reputation;
        public int Difficulty;
        public int KarmaPK;
        public List<CEffect> Effects;

        public object Clone()
        {
            CQuestReward copy = new CQuestReward();
            copy.Experience = new List<int>(this.Experience);
            copy.TypeOfItems = new List<int>(this.TypeOfItems);
            copy.NumOfItems = new List<int>(this.NumOfItems);
            copy.AttrOfItems = new List<int>(this.AttrOfItems);
            copy.Probability = this.Probability;
            copy.Credits = this.Credits;
            copy.Reputation = this.Reputation;
            copy.KarmaPK = this.KarmaPK;
            copy.Effects = new List<CEffect>(this.Effects);
            copy.Difficulty = this.Difficulty;
            return copy;
        }

        public CQuestReward()
        {
            this.Experience = new List<int>();
            this.TypeOfItems = new List<int>();
            this.NumOfItems = new List<int>();
            this.AttrOfItems = new List<int>();
            this.Probability = new List<float>();
            this.Credits = new float();            
            this.Reputation = new Dictionary<int, int>();
            this.KarmaPK = new int();
            this.Effects = new List<CEffect>();
            this.Difficulty = 1;
        }

        public string getReputation()
        {
            string result = "";
            if (ReputationNotEmpty())
            {
                foreach (int key in this.Reputation.Keys)
                {
                    if (this.Reputation[key] == 0)
                        continue;
                    if (!result.Equals(""))
                        result += ";";
                    result += (key.ToString() + ":" + this.Reputation[key].ToString());
                }
            }
            return result;
        }

        public bool ReputationNotEmpty()
        {
            foreach (int Value in this.Reputation.Values)
                if (Value != 0)
                    return true;
            return false;
        }
    }

    public class CQuestPenalty : ICloneable
    {
        public List<int> Experience;
        public List<int> TypeOfItems;
        public List<int> NumOfItems;
        public float Credits;

        public object Clone()
        {
            CQuestPenalty copy = new CQuestPenalty();
            copy.Experience = new List<int>(this.Experience);
            copy.TypeOfItems = new List<int>(this.TypeOfItems);
            copy.NumOfItems = new List<int>(this.NumOfItems);
            copy.Credits = this.Credits;
            return copy;
        }

        public CQuestPenalty()
        {
            this.Experience = new List<int>();
            this.TypeOfItems = new List<int>();
            this.NumOfItems = new List<int>();
            this.Credits = new float();
        }
    }

    public class CQuestAdditional : ICloneable
    {
        public int IsSubQuest;
        public List<int> ListOfSubQuest;
        public int ShowProgress;
        public bool CantCancel;
        public string Holder;

        public object Clone()
        {
            CQuestAdditional copy = new CQuestAdditional();
            copy.IsSubQuest = this.IsSubQuest;
            copy.ListOfSubQuest = new List<int>(this.ListOfSubQuest);
            copy.ShowProgress = this.ShowProgress;
            copy.CantCancel = this.CantCancel;
            copy.Holder = this.Holder;
            return copy;
        }

        public CQuestAdditional()
        {
            this.IsSubQuest = new int();
            this.ListOfSubQuest = new List<int>();
            this.ShowProgress = new int();
            this.CantCancel = false;
            this.Holder = "";
        }

        public CQuestAdditional(string Holder)
        {
            this.IsSubQuest = new int();
            this.ListOfSubQuest = new List<int>();
            this.ShowProgress = new int();
            this.CantCancel = false;
            this.Holder = Holder;
        }
    }

    //public class TriggerInfo : ICloneable
    //{
    //    //public int itemID;
    //    public string name;


    //    public object Clone()
    //    {
    //        TriggerInfo copy = new TriggerInfo();

    //        copy.name = this.name;

    //        return copy;
    //    }

    //    public TriggerInfo()
    //    {
    //        this.name = "";

    //    }
    //    public TriggerInfo(string name)
    //    {
    //        this.name = name;
    //    }
    //}

    public class QuestItemInfo : ICloneable
    {
        public string title;
        public string description;
        public string activation;

        public object Clone()
        {
            QuestItemInfo copy = new QuestItemInfo();

            copy.title = this.title;
            copy.description = this.description;
            copy.activation = this.activation;
            return copy;
        }

        public QuestItemInfo()
        {
            this.title = "";
            this.description = "";
            this.activation = "";
        }

        public QuestItemInfo(string title, string description, string activation)
        {
            this.title = title;
            this.description = description;
            this.activation = activation;
        }
    }

    //! Класс различий - содержит текущую и устаревшую версию
    public class CDifference
    {
        public int cur_version;
        public int old_version;

        public CDifference(int cur_version, int old_version)
        {
            this.cur_version = cur_version;
            this.old_version = old_version;
        }
    }

    //! Класс объекта, хранящий данные о русском и английском имени NPC 
    public class NPCNameDataSourceObject :IComparable
    {
        //! Имя NPC по-английски
        private string value;
        //! Имя NPC по-русски
        private string displayString;

        public NPCNameDataSourceObject(string _value, string _display)
        {
            value = _value;
            displayString = _display;
        }

        public string DisplayString
        {
            get { return displayString; }
        }

        public string Value
        {
            get { return value; }
        }

        public int CompareTo(object obj)
        {
            if (obj == null) 
                return 1;

            NPCNameDataSourceObject otherNPCName = obj as NPCNameDataSourceObject;
            if (otherNPCName != null)
                return this.Value.CompareTo(otherNPCName.Value);
            else
                throw new ArgumentException("Object is not an NPC name");
        }
    };

}
