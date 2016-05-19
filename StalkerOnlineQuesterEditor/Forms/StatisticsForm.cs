using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{
    //! Форма отображения статистики по квестам и персонажам
    public partial class StatisticsForm : Form
    {
        //! Ссылка на родительскую главную форму
        MainForm parent;
        //! Общее число NPC 
        int NPCCount = 0;
        //! Экземпляр класса CQuests, хранящий всю инфу по квестам
        CQuests quests;
        //! Экземпляр класса CDialogs, хранящий всю инфу по диалогам
        CDialogs dialogs;
        CManagerNPC ManagerNPC;

        double Credits;
        int countOfQuests;
        int countOfAmountGold;
        int[] countOfExQuests = { 0, 0, 0 };
        List<int> lExperience;
        float[] averageExp = { 0, 0, 0 };

        int countOfDialogs;
        int countOfTitleLetters;
        int countOfTextLetters;
        int countOfTitleNoSpaces;
        int countOfTextNoSpaces;
        int countOfQuestTexts;
        int countOfQuestSpaceless;
        int topLevelQuests;
        Dictionary<string, Dictionary<int, CDialog>> loc = new Dictionary<string, Dictionary<int, CDialog>>();
        int locDialogs;
        int locTitle;
        int locText;
        int locTitleSpaceless;
        int locTextSpaceless;
        Dictionary<int, CQuest> locQuests = new Dictionary<int,CQuest>();
        int locQuestCount;
        int locQuestText;
        int locQuestSpaceless;

        //! Класс статистических данных по репутации одной фракции
        public class RepData
        {
            public int plus;
            public int minus;
            public int numPlus;
            public int numMinus;
        };

        //! Конструктор, получает элементы от главной формы
        public StatisticsForm(MainForm parent, int _NPCCount, CQuests _quests, CDialogs _dialogs, CManagerNPC manager)
        {
            InitializeComponent();
            this.parent = parent;
            NPCCount = _NPCCount;
            quests = _quests;
            dialogs = _dialogs;
            ManagerNPC = manager;
            calcStatistic();
            calcReputation();
            showOnScreen();
        }

        //! Заполняет статистику для формы
        void calcStatistic()
        {
            calcRewards();
            calcSymbolsInDialogs();
            calcSymbolsInQuests();
        }
        //! Считает денежные вознаграждения и опыт
        void calcRewards()
        {
            const int NumExp = 3;
            Credits = new double();
            countOfQuests = 0;
            countOfAmountGold = 0;
            lExperience = new List<int>(3);
            lExperience.Add(0); lExperience.Add(0); lExperience.Add(0);
            foreach (CQuest quest in quests.quest.Values)
            {
                countOfQuests++;
                if (quest.Reward.Experience.Any())
                {
                    for (int i = 0; i < NumExp; i++)
                    {
                        lExperience[i] += quest.Reward.Experience[i];
                        if (quest.Reward.Experience[i] != 0)
                            countOfExQuests[i]++;
                    }
                }
                //if (quest.Reward.Credits != 0)
                    countOfAmountGold++;
                Credits += quest.Reward.Credits;
            }
            for (int i = 0; i < NumExp; i++)
                averageExp[i] = lExperience[i] / countOfExQuests[i];        
        }
        //! Считает число символов в диалогах и квестах (для расчета стоимости услуг локализации)
        private void calcSymbolsInDialogs()
        {
            countOfTitleLetters = 0;
            countOfTextLetters = 0;
            countOfTitleNoSpaces = 0;
            countOfTextNoSpaces = 0;
            countOfDialogs = 0;

            loc = dialogs.locales[parent.settings.getCurrentLocale()];
            locDialogs = 0;
            locTitle = 0;
            locText = 0;
            locTitleSpaceless = 0;
            locTextSpaceless = 0;

            foreach (string npc in dialogs.dialogs.Keys)
            {
                if (!ManagerNPC.NpcData.ContainsKey(npc) || ManagerNPC.NpcData[npc].location == "notfound")
                    continue;

                foreach (int id in dialogs.dialogs[npc].Keys)
                {
                    if (!dialogs.dialogs[npc][id].coordinates.Active)
                        continue;

                    countOfDialogs++;
                    countOfTextLetters += dialogs.dialogs[npc][id].Text.Length;
                    countOfTitleLetters += dialogs.dialogs[npc][id].Title.Length;
                    countOfTextNoSpaces += dialogs.dialogs[npc][id].Text.Replace(" ", "").Length;
                    countOfTitleNoSpaces += dialogs.dialogs[npc][id].Title.Replace(" ", "").Length;
                
                    if ( !loc.ContainsKey(npc) || !loc[npc].ContainsKey(id) || loc[npc][id].version < dialogs.dialogs[npc][id].version)
                    {
                        locDialogs++;
                        locTitle += dialogs.dialogs[npc][id].Title.Length;
                        locText += dialogs.dialogs[npc][id].Text.Length;
                        locTitleSpaceless += dialogs.dialogs[npc][id].Title.Replace(" ", "").Length;
                        locTextSpaceless += dialogs.dialogs[npc][id].Text.Replace(" ", "").Length;                        
                    }
                }
            }
        }
        //! Считает число символов для перевода в квестах
        void calcSymbolsInQuests()
        {
            countOfQuestTexts = 0;
            countOfQuestSpaceless = 0;
            locQuestCount = 0;
            locQuestText = 0;
            locQuestSpaceless = 0;
            locQuests = quests.locales[parent.settings.getCurrentLocale()];
            topLevelQuests = quests.getCountTopLevelQuests();
            foreach (CQuest quest in quests.quest.Values)
            {
                countOfQuestTexts += quest.QuestInformation.Title.Length;
                countOfQuestTexts += quest.QuestInformation.Description.Length;
                countOfQuestSpaceless += quest.QuestInformation.Title.Replace(" ","").Length;
                countOfQuestSpaceless += quest.QuestInformation.Description.Replace(" ", "").Length;

                if (!locQuests.ContainsKey(quest.QuestID) || locQuests[quest.QuestID].Version < quest.Version)
                {
                    locQuestCount++;
                    locQuestText += quest.QuestInformation.Title.Length;
                    locQuestText += quest.QuestInformation.Description.Length;
                    locQuestSpaceless += quest.QuestInformation.Title.Replace(" ", "").Length;
                    locQuestSpaceless += quest.QuestInformation.Description.Replace(" ", "").Length;
                }
            }        
        }
        //! Считает статистику по бонусам и штрафам к репутации у каждой фракции и заполняет табличку
        void calcReputation()
        {
            Dictionary<int, RepData> RepStats = new Dictionary<int,RepData>();
            foreach (KeyValuePair<int, string> fraction in parent.fractions.getListOfFractions())
            {
                string id = fraction.Key.ToString();
                string name = fraction.Value;
                object[] row = { id, name, "0", "0", "0", "0", "0" };
                RepStats.Add(fraction.Key, new RepData());
                dataFractionStats.Rows.Add(row);
            }
            foreach (CQuest quest in quests.quest.Values)
            {
                if (quest.Reward.ReputationNotEmpty())
                {
                    foreach (KeyValuePair<int, int> oneRep in quest.Reward.Reputation)
                    {
                        if (oneRep.Value > 0)
                        {
                            RepStats[oneRep.Key].plus += oneRep.Value;
                            RepStats[oneRep.Key].numPlus++;
                        }
                        else if (oneRep.Value < 0)
                        {
                            RepStats[oneRep.Key].minus += oneRep.Value;
                            RepStats[oneRep.Key].numMinus++;                        
                        }
                    }
                }
            }
            foreach (DataGridViewRow row in dataFractionStats.Rows)
            { 
                int fractionID = int.Parse( row.Cells[0].Value.ToString() );
                row.Cells["colPlus"].Value = RepStats[fractionID].plus;
                row.Cells["colMinus"].Value = RepStats[fractionID].minus;
                row.Cells["colQuestsNum"].Value = RepStats[fractionID].numPlus + RepStats[fractionID].numMinus;
                if (RepStats[fractionID].numPlus != 0)
                    row.Cells["colAveragePlus"].Value = RepStats[fractionID].plus / RepStats[fractionID].numPlus;
                if (RepStats[fractionID].numMinus != 0)
                    row.Cells["colAverageMinus"].Value = RepStats[fractionID].minus / RepStats[fractionID].numMinus;
            }
        }

        //! Показывает результаты на экране
        void showOnScreen()
        {
            string symbols = "";
            symbols += "Общее количество NPC:        " + NPCCount.ToString() + "\n";
            symbols += "Общее количество квестов:    " + topLevelQuests.ToString() + " (c субквестами: " + countOfQuests.ToString() + ")\n";
            symbols += "Общее количество диалогов:   " + countOfDialogs.ToString() + "\n";
            symbols += "Общее количество знаков в словах NPC:   " + countOfTextLetters.ToString() + "\n";
                                            //", без пробелов: " + countOfTextNoSpaces.ToString() + "\n";
            symbols += "Общее количество знаков в словах ГГ:   " + countOfTitleLetters.ToString() + "\n";
                                            //", без пробелов: " + countOfTitleNoSpaces.ToString() + "\n";
            symbols += "Общее количество знаков в описаниях квестов:   " + countOfQuestTexts.ToString() + "\n\n";
                                            //", без пробелов: " + countOfQuestSpaceless.ToString() + "\n\n";

            symbols += "ОСТАЛОСЬ ЛОКАЛИЗОВАТЬ: \n";
            symbols += "Диалогов:   " + locDialogs.ToString() + "\n";
            symbols += "Знаков в словах NPC:   " + locText.ToString() + "\n";
                                            //", без пробелов: " + locTextSpaceless.ToString() + "\n";
            symbols += "Знаков в словах ГГ:   " + locTitle.ToString() + "\n";
                                            //", без пробелов: " + locTitleSpaceless.ToString() + "\n";
            symbols += "Квестов:   " + locQuestCount.ToString() + "\n";
            symbols += "Знаков в названиях и описаниях квестов:   " + locQuestText.ToString() + "\n\n";
                                            //", без пробелов: " + locQuestSpaceless.ToString() + "\n\n";
            lLocalizationInfo.Text = symbols;

            string rewards = "";
            rewards += "По наградам:\n";
            rewards += "Общее количество денег:         ";
            rewards += (Credits.ToString() + " руб." + "   Среднее: " + (Credits / countOfAmountGold).ToString("F0") + " руб.\n");
            rewards += "   Общее количество опыта:\n";
            rewards += "\aБоевого:         ";
            if (countOfExQuests[0] != 0 || lExperience[0] != 0)
                rewards += (lExperience[0].ToString() + "   Среднее: " + averageExp[0].ToString() + "\n");
            rewards += "\aВыживания:         ";
            if (countOfExQuests[1] != 0 || lExperience[1] != 0)
                rewards += (lExperience[1].ToString() + "   Среднее: " + averageExp[1].ToString() + "\n");
            rewards += "\aПоддержка:         ";
            if (countOfExQuests[2] != 0 || lExperience[2] != 0)
                rewards += (lExperience[2].ToString() + "   Среднее: " + averageExp[2].ToString() + "\n");

            lRewardInfo.Text = rewards;
                   
        }

        //! Нажатие ОК - закрытие формы
        private void bOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
