using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{
    //! Класс настроек проекта - задается в форме OperatorSettings и хранит все данные в settings.xml
    public class CSettings
    {
        MainForm parent;
        //! Номер оператора (???) 
        int iNumOperator;
        //! Режим - обычный (MODE_SIMPLE) или перевод (MODE_LOCALIZATION)
        int mode = 0;
        //! Путь, по которому копировать файлы результата - Dialogs.xml, Quests.xml etc.
        public string pathToCopyFiles = @"..\..\..\res\scripts\common\data\Quests\";
        public string pathToLocalFiles = @"..\..\..\res\local\";

        //! Список все локализаций English, GER etc.
        List<string> locales = new List<string>();
        //! Текущая локализация. Пока хранит 0 - видимо для русского языка
        int currentLocale = 0;
        int lastNpcIndex = 0;

        public int MODE_EDITOR = 0;
        public int MODE_LOCALIZATION = 1;       

        string SETTINGS_PATH = "settings/";
        string SETTING_FILE = "Settings.xml";
        string ORIGINAL_PATH = "Russian\\";

        private string dialogTextsXML = "DialogTexts.xml";
        private string dialogDataXML = "DialogData.xml";
        private string questTextsXML = "QuestTexts.xml";
        private string questDataXML = "QuestData.xml";
        private string deletedQuests = "DeletedQuests.txt";
        private string lastQuestID = "lastQuestID.txt";

        public CSettings(MainForm parent)
        {
            this.parent = parent;
            try
            {
                XDocument doc = XDocument.Load(SETTINGS_PATH + SETTING_FILE);

                this.iNumOperator = int.Parse(doc.Root.Element("operator").Value.ToString());
                foreach (string locale in doc.Root.Element("locales").Value.ToString().Split(','))
                {
                    if (locale.Count() <= 3) continue;
                    locales.Add(locale);
                }
                this.mode = int.Parse(doc.Root.Element("mode").Value.ToString());
                this.currentLocale = int.Parse(doc.Root.Element("current_locale").Value.ToString());
                if (Directory.Exists(doc.Root.Element("pathToCopyFiles").Value))
                    pathToCopyFiles = doc.Root.Element("pathToCopyFiles").Value;
                lastNpcIndex = int.Parse(doc.Root.Element("LastNPcIndex").Value.ToString());
                if (Directory.Exists(doc.Root.Element("pathToLocalFiles").Value))
                    pathToLocalFiles = doc.Root.Element("pathToLocalFiles").Value;

            }
            catch
            {
                iNumOperator = 0;
                mode = MODE_EDITOR;
                currentLocale = 0;
                lastNpcIndex = 1;
                pathToCopyFiles = @"..\..\..\res\scripts\common\data\Quests\";
                pathToLocalFiles = @"..\..\..\res\local\";
                System.Console.WriteLine("Can't parse settings file! Defaults used");
            }
            if (!locales.Contains("English"))
                locales.Add("English");
            if (!Directory.Exists(pathToCopyFiles))
            {
                pathToCopyFiles = "source/Quests/";
            }
            if (!Directory.Exists(pathToLocalFiles))
            {
                pathToLocalFiles = "source/local/";
            }
        }

        //! Проверяет режим, и задает соответствующую надпись на главной форме
        public void checkMode()
        {
            if (getMode() == MODE_LOCALIZATION)
                parent.Text = "QuestEditor@SO_Team НОВЫЙ ПЕРСОНАЖ. Режим перевода:" + getCurrentLocale();
            else
                parent.Text = "QuestEditor@SO_Team НОВЫЙ ПЕРСОНАЖ";
            parent.onChangeMode();
        }

        //! Возврщает номер оператора
        public int getOperatorNumber()
        {
            return this.iNumOperator;
        }

        public void setLocales(string locales)
        {
            this.locales.Clear();
            foreach (string locale in locales.Split(','))
                this.locales.Add(locale.Trim());
        }

        //! Возвращает все локали в строке через запятую
        public string getLocales()
        {
            string ret = "";            
            foreach(string loc in locales)
            {
                if (ret == "")
                    ret += loc;
                else ret += "," + loc;
            }
            return ret;
        }

        //! Возвращает список всех локалей в виде списка строк
        public List<string> getListLocales()
        {
            return locales;
        }

        //! Задает номер оператора
        public void setOperatorNumber(int iNumOperator)
        {
            this.iNumOperator = iNumOperator;
        }

        public void setLastNpcIndex(int NpcIndex)
        {
            lastNpcIndex = NpcIndex;
        }
        public int getLastNpcIndex()
        {
            return lastNpcIndex;
        }

        //! Сохраняет все настройки в файл settings.xml
        public void saveSettings()
        {
            if (!Directory.Exists(SETTINGS_PATH))
                Directory.CreateDirectory(SETTINGS_PATH);
            XDocument resultDoc = new XDocument(new XElement("root"));
            XElement oper = new XElement("operator", iNumOperator);
            XElement loc = new XElement("locales", getLocales());
            XElement mode = new XElement("mode", this.mode.ToString());
            XElement current_locale = new XElement("current_locale", this.currentLocale.ToString());
            XElement lastNpcIndex = new XElement("LastNPcIndex", this.lastNpcIndex);
            XElement path = new XElement("pathToCopyFiles", this.pathToCopyFiles);
            XElement local_path = new XElement("pathToLocalFiles", this.pathToLocalFiles);
            resultDoc.Root.Add(oper);
            resultDoc.Root.Add(loc);
            resultDoc.Root.Add(mode);
            resultDoc.Root.Add(current_locale);
            resultDoc.Root.Add(lastNpcIndex);
            resultDoc.Root.Add(path);
            resultDoc.Root.Add(local_path); 
            resultDoc.Save(SETTINGS_PATH + SETTING_FILE);
        }

        //! Устанавливает текущую локализацию по номеру в спике и возвращает true в случае успеха
        public bool setLocale(int index_locale)
        {
            try
            {
                string locale = this.locales[index_locale];
                this.mode = this.MODE_LOCALIZATION;
                this.currentLocale = index_locale;
                checkMode();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //! Устанавливает Обычный режим (корректировка текстов)
        public void setEditorMode()
        {
            this.mode = this.MODE_EDITOR;
            checkMode();
        }

        //! Возвращает текущий режим (Simple или Localization)
        public int getMode()
        {
            return mode;
        }

        //! Возвращает порядковый номер текущей локали
        public int getCurrentIndexLocale()
        {
            return currentLocale;
        }

        //! Возвращает название текущей локали
        public string getCurrentLocale()
        {
            return locales[currentLocale];
        }

        public string GetDialogTextPath()
        {
            return Path.Combine(pathToLocalFiles, ORIGINAL_PATH, dialogTextsXML);
        }

        public string GetQuestTextPath()
        {
            return Path.Combine(pathToLocalFiles, ORIGINAL_PATH, questTextsXML);
        }

        public string GetDialogDataPath()
        {
            return Path.Combine(pathToCopyFiles, dialogDataXML);
        }

        public string GetDeletedQuestsPath()
        {
            return "source\\" + deletedQuests;
        }

        public string GetQuestDataPath()
        {
            return Path.Combine(pathToCopyFiles, questDataXML);
        }

        public string GetDialogLocaleTextPath()
        {
            return Path.Combine(pathToLocalFiles, getCurrentLocale(), dialogTextsXML);
        }

        public string GetQuestLocaleTextPath()
        {
            return Path.Combine(pathToLocalFiles, getCurrentLocale(), questTextsXML);
        }

        public string GetLastQuestIDPath()
        {
            return SETTINGS_PATH + lastQuestID;
        }
    }
}
