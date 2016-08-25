using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StalkerOnlineQuesterEditor
{

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

    public class CQuestPrecondition
    {
       	public int Repeat;
        public double TakenPeriod;
        public CQuestPrecondition()
        {
            this.Repeat = new int();
            this.TakenPeriod = new double();
        }

        public object Clone()
        {
            CQuestPrecondition copy = new CQuestPrecondition();
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
        public List<int> MassQuests;
        public string TeleportTo;
        public int FailedIf;
        public int MaxGroup;
        public int MinGroup;
        public int MaxMember;
        public int MinMember;
        public float basePercent;

        public object Clone()
        {
            CQuestRules copy = new CQuestRules();

            copy.TypeOfItems = new List<int>(this.TypeOfItems);
            copy.NumOfItems = new List<int>(this.NumOfItems);
            copy.AttrOfItems = new List<int>(this.AttrOfItems);
            copy.Scenarios = new List<int>(this.Scenarios);
            copy.MassQuests = new List<int>(this.MassQuests);
            copy.TeleportTo = (string)this.TeleportTo.Clone();
            copy.FailedIf = this.FailedIf;
            copy.MaxGroup = this.MaxGroup;
            copy.MinGroup = this.MinGroup;
            copy.MaxMember = this.MaxMember;
            copy.MinMember = this.MinMember;
            copy.basePercent = this.basePercent;
            return copy;
        }

        public CQuestRules()
        {
            this.TypeOfItems = new List<int>();
            this.NumOfItems = new List<int>();
            this.AttrOfItems = new List<int>();
            this.Scenarios = new List<int>();
            this.MassQuests = new List<int>();
            this.FailedIf = new int();
            this.MaxGroup = new int();
            this.MinGroup = new int();
            this.TeleportTo = "";
            this.basePercent = new float();
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
        public string DebugData;

        public object Clone()
        {
            CQuestAdditional copy = new CQuestAdditional();
            copy.IsSubQuest = this.IsSubQuest;
            copy.ListOfSubQuest = new List<int>(this.ListOfSubQuest);
            copy.ShowProgress = this.ShowProgress;
            copy.CantCancel = this.CantCancel;
            copy.Holder = this.Holder;
            copy.DebugData = this.DebugData;
            return copy;
        }

        public CQuestAdditional()
        {
            this.IsSubQuest = new int();
            this.ListOfSubQuest = new List<int>();
            this.ShowProgress = new int();
            this.CantCancel = false;
            this.Holder = "";
            this.DebugData = "";
        }

        public CQuestAdditional(string Holder)
        {
            this.IsSubQuest = new int();
            this.ListOfSubQuest = new List<int>();
            this.ShowProgress = new int();
            this.CantCancel = false;
            this.Holder = Holder;
            this.DebugData = "";
        }
    }

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

}