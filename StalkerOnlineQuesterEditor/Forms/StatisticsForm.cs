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

        float Credits;
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

        //! Конструктор, получает элементы от главной формы
        public StatisticsForm(MainForm parent, int _NPCCount, CQuests _quests, CDialogs _dialogs)
        {
            InitializeComponent();
            this.parent = parent;
            NPCCount = _NPCCount;
            quests = _quests;
            dialogs = _dialogs;
            calcStatistic();
            showOnScreen();
        }

        //! Заполняет статистику для формы
        void calcStatistic()
        {
            calcRewards();
            calcSymbolsInDialogs();
            calcSymbolsInQuests();
        }
        //! Считает денежные вознагражлдения и опыт
        void calcRewards()
        {
            const int NumExp = 3;
            Credits = new float();
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
                if (!dialogs.NpcData.ContainsKey(npc) || dialogs.NpcData[npc].location == "notfound")
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

        //! Показывает результаты на экране
        void showOnScreen()
        {
            string str = "";
            str += "Общее количество NPC:        " + NPCCount.ToString() + "\n";
            str += "Общее количество квестов:    " + countOfQuests.ToString() + "\n";
            str += "Общее количество диалогов:   " + countOfDialogs.ToString() + "\n";
            str += "Общее количество знаков в словах NPC:   " + countOfTextLetters.ToString() + "\n";
                                            //", без пробелов: " + countOfTextNoSpaces.ToString() + "\n";
            str += "Общее количество знаков в словах ГГ:   " + countOfTitleLetters.ToString() + "\n";
                                            //", без пробелов: " + countOfTitleNoSpaces.ToString() + "\n";
            str += "Общее количество знаков в описаниях квестов:   " + countOfQuestTexts.ToString() + "\n\n";
                                            //", без пробелов: " + countOfQuestSpaceless.ToString() + "\n\n";

            str += "ОСТАЛОСЬ ЛОКАЛИЗОВАТЬ: \n";
            str += "Диалогов:   " + locDialogs.ToString() + "\n";
            str += "Знаков в словах NPC:   " + locText.ToString() + "\n";
                                            //", без пробелов: " + locTextSpaceless.ToString() + "\n";
            str += "Знаков в словах ГГ:   " + locTitle.ToString() + "\n";
                                            //", без пробелов: " + locTitleSpaceless.ToString() + "\n";
            str += "Квестов:   " + locQuestCount.ToString() + "\n";
            str += "Знаков в названиях и описаниях квестов:   " + locQuestText.ToString() + "\n\n";
                                            //", без пробелов: " + locQuestSpaceless.ToString() + "\n\n";

            str += "По наградам:\n";
            str += "Общее количество денег:         ";
            str += (Credits.ToString() + " руб." + "   Среднее: " + (Credits / countOfAmountGold).ToString() + " руб.\n");
            str += "   Общее количество опыта:\n";
            str += "\aБоевого:         ";
            if (countOfExQuests[0] != 0 || lExperience[0] != 0)
                str += (lExperience[0].ToString() + "   Среднее: " + averageExp[0].ToString() + "\n");
            str += "\aВыживания:         ";
            if (countOfExQuests[1] != 0 || lExperience[1] != 0)
                str += (lExperience[1].ToString() + "   Среднее: " + averageExp[1].ToString() + "\n");
            str += "\aПоддержка:         ";
            if (countOfExQuests[2] != 0 || lExperience[2] != 0)
                str += (lExperience[2].ToString() + "   Среднее: " + averageExp[2].ToString() + "\n");
            labelInfo.Text = str;        
        }

        //! Нажатие ОК - закрытие формы
        private void bOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
