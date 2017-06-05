using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


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
        public CQuestReward QuestPenalty;
        public CQuestAdditional Additional;
        public bool hidden;

        public object Clone()
        {
            CQuest copy = new CQuest();
            copy.QuestID = this.QuestID;
            copy.Version = 0;
            copy.QuestInformation = (CQuestInformation)this.QuestInformation.Clone();
            copy.Precondition = (CQuestPrecondition)this.Precondition.Clone();
            copy.QuestRules = (CQuestRules)this.QuestRules.Clone();
            copy.Reward = (CQuestReward)this.Reward.Clone();
            copy.QuestPenalty = (CQuestReward)this.QuestPenalty.Clone();
            copy.Additional = (CQuestAdditional)this.Additional.Clone();
            copy.Target = (CQuestTarget)this.Target.Clone();
            copy.hidden = this.hidden;
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
            this.QuestPenalty = new CQuestReward();
            this.Additional = new CQuestAdditional();
            this.Target = new CQuestTarget();
            this.hidden = false;
        }

        public CQuest(int questID, int Version, CQuestInformation questInformation, CQuestPrecondition precondition, CQuestRules questRules, CQuestReward reward, CQuestAdditional additional, CQuestTarget target, CQuestReward penalty, bool hidden = false)
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
            this.hidden = hidden;
        }

        public void InsertNonTextData(CQuest source)
        {
            this.Precondition = (CQuestPrecondition) source.Precondition.Clone();
            this.QuestRules = (CQuestRules) source.QuestRules.Clone();
            this.Reward = (CQuestReward) source.Reward.Clone();
            this.Additional = (CQuestAdditional) source.Additional.Clone();
            this.Target = (CQuestTarget) source.Target.Clone();
            this.QuestPenalty = (CQuestReward)source.QuestPenalty.Clone();        
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
        public float percent;
        public bool usePercent;

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
            copy.percent = this.percent;
            copy.usePercent = this.usePercent;
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
            this.percent = new float();
            this.usePercent = false;
        }

        public bool Any()
        {
            return QuestType != 0 || ObjectType != 0 || NumOfObjects != 0 || ObjectAttr != 0 || AreaName != "" ||
                    Time != 0.0f || IsGroup != 0 || IsClan || ObjectName != "" || AObjectAttrs.Any() ||
                    onFin != 0 || percent != 0 || usePercent;

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
        public bool omniCounter;
        public CQuestPrecondition()
        {
            this.Repeat = new int();
            this.TakenPeriod = new double();
            this.omniCounter = false;
        }

        public object Clone()
        {
            CQuestPrecondition copy = new CQuestPrecondition();
            copy.Repeat = this.Repeat;
            copy.TakenPeriod = this.TakenPeriod;
            copy.omniCounter = this.omniCounter;
            return copy;
        }
        public bool Any()
        {
            return Repeat != 0 || TakenPeriod != 0 || omniCounter;
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
        public int MaxGroup;
        public int MinGroup;
        public int MaxMember;
        public int MinMember;
        public float basePercent;
        public NPC npc;
        public Mob mobs;

        public object Clone()
        {
            CQuestRules copy = new CQuestRules();

            copy.TypeOfItems = new List<int>(this.TypeOfItems);
            copy.NumOfItems = new List<int>(this.NumOfItems);
            copy.AttrOfItems = new List<int>(this.AttrOfItems);
            copy.Scenarios = new List<int>(this.Scenarios);
            copy.MassQuests = new List<int>(this.MassQuests);
            copy.TeleportTo = (string)this.TeleportTo.Clone();
            copy.MaxGroup = this.MaxGroup;
            copy.MinGroup = this.MinGroup;
            copy.MaxMember = this.MaxMember;
            copy.MinMember = this.MinMember;
            copy.basePercent = this.basePercent;
            copy.npc = this.npc;
            copy.mobs = this.mobs;
            return copy;
        }

        public bool Any()
        {
            return TypeOfItems.Any() || NumOfItems.Any() || AttrOfItems.Any() || Scenarios.Any() || MassQuests.Any() ||
                MaxGroup != 0 || MinGroup != 0 || TeleportTo != "" || basePercent != 0 || MinMember != 0 || MaxMember != 0 || npc.Any() || mobs.Any();

        }
        public CQuestRules()
        {
            this.TypeOfItems = new List<int>();
            this.NumOfItems = new List<int>();
            this.AttrOfItems = new List<int>();
            this.Scenarios = new List<int>();
            this.MassQuests = new List<int>();
            this.MaxGroup = new int();
            this.MinGroup = new int();
            this.MaxMember = new int();
            this.MinMember = new int();
            this.TeleportTo = "";
            this.basePercent = new float();
            this.npc = new NPC();
            this.mobs = new Mob();
           
        }

        public class Mob
        {
            public int mob_type = 0;
            public string level = "0";
            public string way = "";
            public int count = 0;
            public int scen_type = 0;
            public bool uniq = false;
            public string questID = "";
            public bool invulnerable = false;

            public bool Any()
            {
                return mob_type > 0 && count > 0 && scen_type > 0;
            }

            public XElement getXML()
            {
                XElement result = null;
                if (!this.Any())
                    return null;

                result = new XElement("mobs");
                result.Add(new XElement("mobType", mob_type));
                if (level.Any())
                    result.Add(new XElement("level", level));
                result.Add(new XElement("count", count));
                result.Add(new XElement("scenType", scen_type));
                if (way.Any())
                    result.Add(new XElement("way", way));
                if (invulnerable)
                    result.Add(new XElement("invulnerable", "1"));

                if (uniq)
                    result.Add(new XElement("uniq", "1"));
                if (questID.Any())
                    result.Add(new XElement("questID", questID));
                return result;
            }

            public void setXML(XElement element)
            {
                if (element.Element("mobType") != null)
                    int.TryParse(element.Element("mobType").Value.ToString(), out mob_type);
                if (element.Element("level") != null)
                    level = element.Element("level").Value.ToString();
                if (element.Element("count") != null)
                    int.TryParse(element.Element("count").Value.ToString(), out count);
                if (element.Element("scenType") != null)
                    int.TryParse(element.Element("scenType").Value.ToString(), out scen_type);
                if (element.Element("way") != null)
                    way = element.Element("way").Value;
                if (element.Element("uniq") != null)
                    uniq = true;
                if (element.Element("invulnerable") != null)
                    invulnerable = true;
                if (element.Element("questID") != null)
                    questID = element.Element("questID").Value.ToString();
            }
        }

        public class NPC
        {
            public string name = "";
            public string displayName = "";
            public string way = "";
            public int fraction = 0;
            public int reputation = 0;
            public string animation = "";
            public int weapon = 0, hand = 0, boots = 0, body = 0, armor = 0, legs = 0, cap = 0, mask = 0, back = 0, head = 0;
            public float walkSpeed = 0;
            public bool uniq = false;
            public bool invulnerable = false;
            public bool mobNoAgr = false;

            public bool Any()
            {
                return name.Any() || displayName.Any() || way.Any() || fraction != 0 || reputation != 0 || animation.Any() ||
                        weapon != 0 || hand != 0 || boots != 0 || body != 0 ||
                        armor != 0 || legs != 0 || cap != 0 || mask != 0 ||
                        back != 0 || head != 0;
            }

            public void setXML(XElement element)
            {
                if (element.Element("name") != null)
                    name = element.Element("name").Value;
                if (element.Element("displayName") != null)
                    displayName = element.Element("displayName").Value;
                if (element.Element("way") != null)
                    way = element.Element("way").Value;
                if (element.Element("fraction") != null)
                    int.TryParse(element.Element("fraction").Value, out fraction);
                if (element.Element("reputation") != null)
                    int.TryParse(element.Element("reputation").Value, out reputation);
                if (element.Element("animation") != null)
                    animation = element.Element("animation").Value;
                if (element.Element("weapon") != null)
                    int.TryParse(element.Element("weapon").Value, out weapon);
                if (element.Element("hand") != null)
                    int.TryParse(element.Element("hand").Value, out hand);
                if (element.Element("boots") != null)
                    int.TryParse(element.Element("boots").Value, out boots);
                if (element.Element("body") != null)
                    int.TryParse(element.Element("body").Value, out body);
                if (element.Element("armor") != null)
                    int.TryParse(element.Element("armor").Value, out armor);
                if (element.Element("legs") != null)
                    int.TryParse(element.Element("legs").Value, out legs);
                if (element.Element("cap") != null)
                    int.TryParse(element.Element("cap").Value, out cap);
                if (element.Element("mask") != null)
                    int.TryParse(element.Element("mask").Value, out mask);
                if (element.Element("back") != null)
                    int.TryParse(element.Element("back").Value, out back);
                if (element.Element("head") != null)
                    int.TryParse(element.Element("head").Value, out head);
                if (element.Element("uniq") != null)
                    uniq = true;
                if (element.Element("invulnerable") != null)
                    invulnerable = true;
                if (element.Element("mobNoAggr") != null)
                    mobNoAgr = true;
                if (element.Element("walkSpeed") != null)
                    float.TryParse(element.Element("walkSpeed").Value, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out walkSpeed);
            }

            public XElement getXML()
            {
                XElement result = null;
                if (!this.Any())
                    return null;

                result = new XElement("npc");
                if (name.Any())
                    result.Add(new XElement("name", name));
                if (displayName.Any())
                    result.Add(new XElement("displayName", displayName));
                if (way.Any())
                    result.Add(new XElement("way", way));
                if (fraction != 0)
                    result.Add(new XElement("fraction", Global.GetIntAsString(fraction)));
                if (reputation != 0)
                    result.Add(new XElement("reputation", Global.GetIntAsString(reputation)));
                if (animation.Any())
                    result.Add(new XElement("animation", animation));
                if (invulnerable)
                    result.Add(new XElement("invulnerable", "1"));
                if (walkSpeed != 0.0)
                    result.Add(new XElement("walkSpeed", walkSpeed));
                if (weapon != 0)
                    result.Add(new XElement("weapon", Global.GetIntAsString(weapon)));
                if (hand != 0)
                    result.Add(new XElement("hand", Global.GetIntAsString(hand)));
                if (boots != 0)
                    result.Add(new XElement("boots", Global.GetIntAsString(boots)));
                if (body != 0)
                    result.Add(new XElement("body", Global.GetIntAsString(body)));
                if (armor != 0)
                    result.Add(new XElement("armor", Global.GetIntAsString(armor)));
                if (legs != 0)
                    result.Add(new XElement("legs", Global.GetIntAsString(legs)));
                if (cap != 0)
                    result.Add(new XElement("cap", Global.GetIntAsString(cap)));
                if (mask != 0)
                    result.Add(new XElement("mask", Global.GetIntAsString(mask)));
                if (back != 0)
                    result.Add(new XElement("back", Global.GetIntAsString(back)));
                if (head != 0)
                    result.Add(new XElement("head", Global.GetIntAsString(head)));
                if (uniq) result.Add(new XElement("uniq", "1"));
                if (mobNoAgr) result.Add(new XElement("mobNoAggr", "1"));
                return result;
            }
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
        public int KarmaPK;
        public List<CEffect> Effects;
        public bool RewardWindow; 

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
            copy.RewardWindow = this.RewardWindow;
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
            this.RewardWindow = false;
        }
        public bool Any()
        {
            return Experience.Any() || TypeOfItems.Any() || NumOfItems.Any() ||
                AttrOfItems.Any() || Probability.Any() || Credits != 0 || ReputationNotEmpty() ||
                KarmaPK != 0 || Effects.Any() || RewardWindow;
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

        public bool Any()
        {
            return Experience.Any() || TypeOfItems.Any() || NumOfItems.Any() || Credits != 0;
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

        public bool Any()
        {
            return ListOfSubQuest.Any() || IsSubQuest != 0 || ShowProgress != 0 || CantCancel || Holder != "" || DebugData != "";
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