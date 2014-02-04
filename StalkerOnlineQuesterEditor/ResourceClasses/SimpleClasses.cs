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
            this.ListOfCompletedQuests = new List<int>();
            this.ListOfOpenedQuests = new List<int>();
            this.ListOfOnTestQuests = new List<int>();
            this.ListOfFailedQuests = new List<int>();

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
            string ret = "";
            foreach (int key in this.Reputation.Keys)
                if (this.Reputation[key].Any())
            {
                    if (ret != "")
                        ret += ";";
                    if (this.Reputation[key][0] == 0)
                        ret += key.ToString() + ":"
                            + this.Reputation[key][0].ToString() + ":"
                            + this.Reputation[key][1].ToString() + ":"
                            + this.Reputation[key][2].ToString();
                    else if (this.Reputation[key][0] == 1)
                        ret += key.ToString() + ":"
                            + this.Reputation[key][0].ToString() + ":"
                            + this.Reputation[key][1].ToString() + ":";
                    else if (this.Reputation[key][0] == 2)
                        ret += key.ToString() + ":"
                            + this.Reputation[key][0].ToString() + "::"
                            + this.Reputation[key][2].ToString();
            }
            return ret;
        }
    }

    public class Actions //: ICloneable
    {
        public bool Exit;
        //public bool Trade;
        public int Event;
        public int ToDialog;
        public List<int> CompleteQuests;
        public List<int> GetQuests;
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
            this.CompleteQuests = new List<int>();
            this.GetQuests = new List<int>();
            this.Data = "";
        }
    }

    //! Класс диалога (одна ветка в xml файле)
    public class CDialog
    {
        public int DialogID;
        //public List<string> Holder;
        public string Holder;
        public string Title;
        public string Text;
        public int QuestDialog;
        public CDialogPrecondition Precondition;
        //public List<int> Actions;
        public Actions Actions;
        public ListOfQuests Nodes;
        public int version;

        public CDialog(string Holder, string Title, string Text , int QuestDialog, CDialogPrecondition Precondition, Actions Actions, ListOfQuests Nodes,int DialogID, int version)
        {
            this.Holder = Holder;
            this.Title = Title;
            this.Text = Text;
            this.QuestDialog = QuestDialog;
            this.Precondition = Precondition;
            this.Actions = Actions;
            this.Nodes = Nodes;
            this.DialogID = DialogID;
            this.version = version;
        }
        public CDialog()
        {
            //this.Holder = new List<string>();
            this.Holder = "";
            this.Title = "";
            this.Text = "";
            this.QuestDialog = new int();
            this.Precondition = new CDialogPrecondition();
            this.Actions = new Actions();
            this.Nodes = new List<int>();
            this.DialogID = new int();
            this.version = new int();
        }
     
    }

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

    }

    public class CQuestInformation : ICloneable
    {
        	public string Title;
			public string Description;
            //public string TypeOfHolder;
            //public string NameOfHolder;
            public string onWin;
            public string onFailed;
            public Dictionary<int,QuestItemInfo> Items;
            //public string Trigger;

            public object Clone()
            {
                CQuestInformation copy = new CQuestInformation();
                copy.Title = (string)this.Title.Clone();
                copy.Description = (string)this.Description.Clone();
                //copy.TypeOfHolder = (string)this.TypeOfHolder.Clone();
                //copy.NameOfHolder = (string)this.NameOfHolder.Clone();
                copy.onWin = (string)this.onWin.Clone();
                copy.onFailed = (string)this.onFailed.Clone();
                //copy.Trigger = (string)this.Trigger.Clone();
                foreach (KeyValuePair<int, QuestItemInfo> item in this.Items)
                    copy.Items.Add(item.Key, (QuestItemInfo)item.Value.Clone());
                return copy;

            }
            
            public CQuestInformation()
            {
                this.Title = "";
                this.Description = "";
                //this.TypeOfHolder = "";
                //this.NameOfHolder = "";
                this.onFailed = "";
                this.onWin = "";
                //this.Trigger = "";
                this.Items = new Dictionary<int, QuestItemInfo>();
            }

            //public CQuestInformation()
            //{
            //    this.Title = "";
            //    this.Description = "";
            //    //this.TypeOfHolder = "Trader";
            //    //this.NameOfHolder = NameOfHolder;
            //    //this.Trigger = "";
            //    this.Items = new Dictionary<int, QuestItemInfo>();
            //}

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
                copy.AObjectAttrs = this.AObjectAttrs;
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
            copy.ListOfCompletedQuests = this.ListOfCompletedQuests;
            copy.ListOfOpenedQuests = this.ListOfOpenedQuests;
            copy.ListOfOnTestQuests = copy.ListOfOnTestQuests;
            copy.Repeat = this.Repeat;
            copy.TakenPeriod = this.TakenPeriod;

            return copy;
        }
    }

    public class CQuestRules : ICloneable
    {
        //List<int> Reputation;
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

            copy.TypeOfItems = this.TypeOfItems;
            copy.NumOfItems = this.NumOfItems;
            copy.AttrOfItems = this.AttrOfItems;
            copy.Scenarios = this.Scenarios;
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
          //  this.Reputation = new List<int>(3);
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

        public CEffect()
        {

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

    public class CQuestReward : ICloneable
    {
        public int Expirience;
        public List<int> TypeOfItems;
        public List<int> NumOfItems;
        public List<int> AttrOfItems;
        public List<int> EventCodes;
        public string TeleportTo;
        public float Credits;
        //public Dictionary<int, int> Reputation;
        public List<int> Fractions;
        public int Difficulty;
        public int Unlimited;
        public int KarmaPK;
        public List<CEffect> Effects;

        public object Clone()
        {
            CQuestReward copy = new CQuestReward();
            copy.Expirience = this.Expirience;
            copy.TypeOfItems = this.TypeOfItems;
            copy.NumOfItems = this.NumOfItems;
            copy.AttrOfItems = this.AttrOfItems;
            copy.EventCodes = this.EventCodes;

            copy.TeleportTo = (string)this.TeleportTo.Clone();
            copy.Credits = this.Credits;
            //copy.Reputation = this.Reputation;
            copy.KarmaPK = this.KarmaPK;
            copy.Effects = this.Effects;

            copy.Fractions = this.Fractions;
            copy.Difficulty = this.Difficulty;
            copy.Unlimited = this.Unlimited;




            return copy;
        }

        public CQuestReward()
        {
            this.Expirience = new int();
            this.TypeOfItems = new List<int>();
            this.NumOfItems = new List<int>();
            this.AttrOfItems = new List<int>();
            this.Credits = new float();
            this.EventCodes = new List<int>();
            //this.Reputation = new Dictionary<int, int>();
            this.Fractions = new List<int>();
            this.KarmaPK = new int();
            this.TeleportTo = "";
            this.Effects = new List<CEffect>();
            this.Difficulty = 1;
            this.Unlimited = 0;
        }

        //public string getReputation()
        //{
            //string ret = "";
            //if (this.Reputation.Any())
            //{
            //    foreach (int key in this.Reputation.Keys)
            //    {
            //        if (!ret.Equals(""))
            //            ret += ",";
            //        ret += (key.ToString() + ":" + this.Reputation[key].ToString());
            //    }
            //}
        //    return ret;
        //}

    }


    public class CQuestPenalty : ICloneable
    {
        public int Expirience;
        public List<int> TypeOfItems;
        public List<int> NumOfItems;
        //public List<int> EventCodes;
        public string TeleportTo;
        public float Credits;
        //public Dictionary<int, int> Reputation;

        public object Clone()
        {
            CQuestPenalty copy = new CQuestPenalty();
            copy.Expirience = this.Expirience;
            copy.TypeOfItems = this.TypeOfItems;
            copy.NumOfItems = this.NumOfItems;
            copy.TeleportTo = (string)this.TeleportTo.Clone();
            copy.Credits = this.Credits;
            //copy.Reputation = this.Reputation;


            return copy;
        }

        public CQuestPenalty()
        {
            this.Expirience = new int();
            this.TypeOfItems = new List<int>();
            this.NumOfItems = new List<int>();
            this.Credits = new float();
            //this.Reputation = new Dictionary<int, int>();
            this.TeleportTo = "";
            //this.EventCodes = new List<int>();
        }
        //public string getReputation()
        //{
        //    string ret = "";

        //    foreach (int key in this.Reputation.Keys)
        //    {
        //        if (!ret.Equals(""))
        //            ret += ",";
        //        ret += (key.ToString() + ":" + this.Reputation[key].ToString());
        //    }

        //    return ret;
        //}
    }

    public class CQuestAdditional : ICloneable
    {
            public int IsSubQuest;
            public List<int> ListOfSubQuest;
            public int ShowProgress;
            public string Holder;


            public object Clone()
            {
                CQuestAdditional copy = new CQuestAdditional();

                copy.IsSubQuest = this.IsSubQuest;
                copy.ListOfSubQuest = this.ListOfSubQuest;
                copy.ShowProgress = this.ShowProgress;
                copy.Holder = this.Holder;

                return copy;
            }

            public CQuestAdditional()
            {
                this.IsSubQuest = new int();
                this.ListOfSubQuest = new List<int>();
                this.ShowProgress = new int();
                this.Holder = "";
            }

            public CQuestAdditional(string Holder)
            {
                this.IsSubQuest = new int();
                this.ListOfSubQuest = new List<int>();
                this.ShowProgress = new int();
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
        //public int itemID;
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
            //this.itemID = new int();
            this.title = "";
            this.description = "";
            this.activation = "";
        }
        //public QuestItemInfo(int itemID, string title, string description)
        public QuestItemInfo(string title, string description, string activation)
        {
            //this.itemID = itemID;
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
        //public string npc_name;
        //public int id;

        public CDifference(int cur_version, int old_version)//, string npc_name, int id)
        {
            this.cur_version = cur_version;
            this.old_version = old_version;
            //this.npc_name = npc_name;
            //this.id = id;
        }
    }


    public class CBalanceFractions
    {
        public double limit;
        public double cat_1;
        public double cat_2;
        public double cat_3;
        public Dictionary<int, CFractionPenalty> penalty;

        public CBalanceFractions()
        {
            limit = 0;
            penalty = new Dictionary<int, CFractionPenalty>();
        }
    }

    public class CFractionPenalty
    {
        public double cat_1;
        public double cat_2;
        public double cat_3;

        public CFractionPenalty()
        {
        }

        public CFractionPenalty(double cat_1, double cat_2, double cat_3)
        {
            this.cat_1 = cat_1;
            this.cat_2 = cat_2;
            this.cat_3 = cat_3;
        }
    }
}
