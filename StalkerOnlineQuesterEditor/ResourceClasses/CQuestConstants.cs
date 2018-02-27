using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace StalkerOnlineQuesterEditor
{
    //! @todo: переделывать все нахер. 
    //! Класс констант квеста - типов. Лучше бы переделывать.
    public class СQuestConstants
    {
        List<СQuestType> ierarchyQuestsType = new List<СQuestType>();
        List<СQuestType> simpleQuestsType = new List<СQuestType>();

        public СQuestConstants()
        {
            simpleQuestsType.Add(new СQuestType(0, "0  Собрать необходимое количество предметов."));
            simpleQuestsType.Add(new СQuestType(16,"16 Собрать необходимое количество предметов авто."));
            simpleQuestsType.Add(new СQuestType(1, "1  Поговорить."));
            simpleQuestsType.Add(new СQuestType(2, "2  Убийство мобов с обязательной сдачей евента."));
            simpleQuestsType.Add(new СQuestType(3, "3  Убийство мобов."));
            simpleQuestsType.Add(new СQuestType(4, "4  Посещение зоны."));
            simpleQuestsType.Add(new СQuestType(5, "5  Сдача денег."));
            simpleQuestsType.Add(new СQuestType(6, "6  Действие над триггером."));
            simpleQuestsType.Add(new СQuestType(7, "7  Использовать предмет."));
            simpleQuestsType.Add(new СQuestType(8, "8  Покинуть зону."));
            simpleQuestsType.Add(new СQuestType(9, "9  Таймер."));
            simpleQuestsType.Add(new СQuestType(17,"17 Умереть."));
            simpleQuestsType.Add(new СQuestType(18,"18 Выйти из игры."));
            simpleQuestsType.Add(new СQuestType(19,"19 Экипировка предмета."));
            simpleQuestsType.Add(new СQuestType(20,"20 Добавление предмета."));
            simpleQuestsType.Add(new СQuestType(21,"21 Получение эффекта."));
            simpleQuestsType.Add(new СQuestType(22,"22 Необходимое количество репутации."));
            simpleQuestsType.Add(new СQuestType(23,"23 Необходимое количество репутации АВТО."));
            simpleQuestsType.Add(new СQuestType(24,"24 Убийство."));
            simpleQuestsType.Add(new СQuestType(25,"25 Собрать количество предметов категории."));
            simpleQuestsType.Add(new СQuestType(26,"26 Собрать количество предметов категории АВТО."));

            ierarchyQuestsType.Add(new СQuestType(50,"50 Игра против режиссера."));
            simpleQuestsType.Add(new СQuestType(51,"51 Создать NPC."));
            simpleQuestsType.Add(new СQuestType(52,"52 Создать Моба."));
            simpleQuestsType.Add(new СQuestType(53,"53 Убийство NPC."));
            simpleQuestsType.Add(new СQuestType(54,"54 Убийство NPC с обязательной сдачей евента."));

            ierarchyQuestsType.Add(new СQuestType(10,"10 Выполнение всех в любом порядке."));
            ierarchyQuestsType.Add(new СQuestType(11,"11 Выполнение всех по порядку."));
            ierarchyQuestsType.Add(new СQuestType(12,"12 Выполнение на выбор."));
            ierarchyQuestsType.Add(new СQuestType(13,"13 Выполнение всех в любом порядке со сдачей."));
            ierarchyQuestsType.Add(new СQuestType(14,"14 Выполнение всех по порядку со сдачей."));
            ierarchyQuestsType.Add(new СQuestType(15,"15 Выполнение на выбор со сдачей."));

            //Туториал
            //simpleQuestsType.Add(new СQuestType(200,"200 _экипировка предмета."));
            simpleQuestsType.Add(new СQuestType(201,"201 _выстрел."));
            simpleQuestsType.Add(new СQuestType(202,"202 _лечение."));
            simpleQuestsType.Add(new СQuestType(203,"203 _восстановление предмета."));
            simpleQuestsType.Add(new СQuestType(204,"204 _фонарик."));
            //simpleQuestsType.Add(new СQuestType(205,"205 _добавление предмета."));
            simpleQuestsType.Add(new СQuestType(206,"206 _событие ГУИ."));



        }
        public bool isSimple(int questType)
        {
            foreach(СQuestType quest in simpleQuestsType)
                if (quest.getType().Equals(questType))
                    return true;
            return false;
        }

        public string getDescription(int questType)
        {
            foreach (СQuestType quest in simpleQuestsType)
                if (quest.getType().Equals(questType))
                    return quest.getDescription();

            foreach (СQuestType quest in ierarchyQuestsType)
                if (quest.getType().Equals(questType))
                    return quest.getDescription();
            return "";
        }

        public List<СQuestType> getListQuests()
        {
            List<СQuestType> ret = new List<СQuestType>();
            ret.AddRange(ierarchyQuestsType);
            ret.AddRange(simpleQuestsType);
            return ret;
        }

        public int getQuestTypeOnDescription(string description)
        {
            foreach (СQuestType type in simpleQuestsType)
                if (type.getDescription().Equals(description))
                    return type.getType();
            foreach (СQuestType type in ierarchyQuestsType)
                if (type.getDescription().Equals(description))
                    return type.getType();
            return 0;
        }
    }

    public class СQuestType
    {
        int QuestType;
        string Description;


        public СQuestType(int typeID, string Description)
        {
            this.QuestType = typeID;
            this.Description = Description;
        }

        public string getDescription()
        {
            return Description;
        }

        public int getType()
        {
            return QuestType;
        }

    }
}
