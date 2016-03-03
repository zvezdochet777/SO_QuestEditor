using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StalkerOnlineQuesterEditor
{

    public class DialogEvent
    {
        private string RusName;
        //private string EngName;
        private int EventID;
        public DialogEvent(string Name, int ID)
        {
            RusName = Name;
            EventID = ID;
        }

        public string Display
        {
            get { return RusName; }
            set { RusName = value; }
        }

        public int Value
        {
            get { return EventID; }
            set { EventID = value; }
        }
    }

    public class ListDialogEvents
    {
        private List<DialogEvent> allEvents = new List<DialogEvent>();
        public ListDialogEvents()
        {
            allEvents.Add(new DialogEvent("Пусто", 0));
            allEvents.Add(new DialogEvent("Торговля", 1));
            allEvents.Add(new DialogEvent("Обмен", 2));
            allEvents.Add(new DialogEvent("Создание клана", 3));
            allEvents.Add(new DialogEvent("Починка", 4));
            allEvents.Add(new DialogEvent("Телепорт", 5));
            allEvents.Add(new DialogEvent("Комплексная починка", 6));
            allEvents.Add(new DialogEvent("Бартер", 7));
            allEvents.Add(new DialogEvent("Телепорт на базу", 8));
            allEvents.Add(new DialogEvent("Идти в зону ПВП", 9));
            allEvents.Add(new DialogEvent("Начало ПВП", 10));
            allEvents.Add(new DialogEvent("Покраска предмета", 11));
            allEvents.Add(new DialogEvent("Купить флаг", 13));
            allEvents.Add(new DialogEvent("Нанять охранника", 14));
            allEvents.Add(new DialogEvent("Нанять торговца", 15));
            allEvents.Add(new DialogEvent("Переход к диалогу", 100));
        }

        public List<DialogEvent> GetFullList()
        {
            return allEvents;
        }

        public string GetEventName(int eventID)
        {
            if (eventID == 0)
                return "";
            var stuff = allEvents.Where<DialogEvent>(item => item.Value == eventID);
            return stuff.FirstOrDefault().Display + ".";
        }
    }

}
