using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{
    //! Класс настроек проекта - задается в форме OperatorSettings и хранит все данные в settings.xml
    public static class CSettings
    {
        static MainForm parent;
        //! Номер оператора (???) 
        static int iNumOperator;
        //! Режим - обычный (MODE_SIMPLE) или перевод (MODE_LOCALIZATION)
        static int mode = 0;
        //! Путь, по которому копировать файлы результата - Dialogs.xml, Quests.xml etc.
        public static string old_pathDialogsDataFiles = @"..\..\..\res\scripts\server_data\Quests\";
        public static string new_pathDialogsDataFiles = @"..\..\..\res\scripts\server_data\DialogData\";
        public static string pathQuestDataFiles = @"..\..\..\res\scripts\common\data\Quests\";
        public static string pathToLocalFiles = @"..\..\..\res\local\";

        public static bool errorFinder = false;

        //! Список все локализаций English, GER etc.
        static List<string> locales = new List<string>();
        //! Текущая локализация. Пока хранит 0 - видимо для русского языка
        static int currentLocale = 0;
        static int lastNpcIndex = 0;

        static string appLang = "ru-RU";

        public static int MODE_EDITOR = 0;
        public static int MODE_LOCALIZATION = 1;

        static string SETTINGS_PATH = "settings/";
        public static string SETTING_FILE = "Settings.xml";
        public static string MAIN_SETTING_FILE = "MainSettings.xml";
        public static string ORIGINAL_PATH = "Russian";

        private static string dialogTextsXML = "DialogTexts.xml";
        private static string dialogTextsDIR = "DialogTexts/";
        private static string dialogDataXML = "DialogData.xml";
        private static string questTextsXML = "QuestTexts.xml";
        private static string questDataXML = "QuestData.xml";
        private static string deletedQuests = "DeletedQuests.txt";
        private static string lastQuestID = "lastQuestID.txt";

        public static void init(MainForm parent)
        {
            CSettings.parent = parent;
            try
            {
                XDocument doc = XDocument.Load(SETTINGS_PATH + MAIN_SETTING_FILE);
                foreach (string locale in doc.Root.Element("locales").Value.ToString().Split(','))
                {
                    if (locale.Count() <= 3) continue;
                    locales.Add(locale);
                }
                
                doc = XDocument.Load(SETTINGS_PATH + SETTING_FILE);

                iNumOperator = int.Parse(doc.Root.Element("operator").Value.ToString());
                
                mode = int.Parse(doc.Root.Element("mode").Value.ToString());
                currentLocale = int.Parse(doc.Root.Element("current_locale").Value.ToString());
                if (Directory.Exists(doc.Root.Element("new_pathDialogsDataFiles").Value))
                    new_pathDialogsDataFiles = doc.Root.Element("new_pathDialogsDataFiles").Value;
                if (Directory.Exists(doc.Root.Element("pathQuestDataFiles").Value))
                    pathQuestDataFiles = doc.Root.Element("pathQuestDataFiles").Value;
                lastNpcIndex = int.Parse(doc.Root.Element("LastNPcIndex").Value.ToString());
                if (Directory.Exists(doc.Root.Element("pathToLocalFiles").Value))
                    pathToLocalFiles = doc.Root.Element("pathToLocalFiles").Value;
                if (doc.Root.Element("app_lang")!= null)
                {
                    appLang = doc.Root.Element("app_lang").Value.ToString();
                }
                if (doc.Root.Element("errorFinder") != null)
                    errorFinder = Convert.ToBoolean(doc.Root.Element("errorFinder").Value);

            }
            catch
            {
                iNumOperator = 0;
                mode = MODE_EDITOR;
                currentLocale = 0;
                lastNpcIndex = 1;
                old_pathDialogsDataFiles = @"..\..\..\res\scripts\server_data\Quests\";
                new_pathDialogsDataFiles = @"..\..\..\res\scripts\server_data\DialogData\";
                pathQuestDataFiles = @"..\..\..\res\scripts\common\data\Quests\";
                pathToLocalFiles = @"..\..\..\res\local\";
                System.Console.WriteLine("Can't parse settings file! Defaults used");
            }
            if (!locales.Contains("English"))
                locales.Add("English");
            if (!Directory.Exists(pathQuestDataFiles))
            {
                pathQuestDataFiles = "source/Quests/";
            }
            if (!Directory.Exists(new_pathDialogsDataFiles))
            {
                new_pathDialogsDataFiles = "source/DialogData/";
            }
            if (!Directory.Exists(pathToLocalFiles))
            {
                pathToLocalFiles = "source/local/";
            }
        }

        //! Проверяет режим, и задает соответствующую надпись на главной форме
        public static void checkMode()
        {
            if (getMode() == MODE_LOCALIZATION)
                parent.Text = "QuestEditor@SO_Team НОВЫЙ ПЕРСОНАЖ. Режим перевода:" + getCurrentLocale();
            else
                parent.Text = "QuestEditor@SO_Team НОВЫЙ ПЕРСОНАЖ";
            parent.onChangeMode();
        }

        //! Возврщает номер оператора
        public static int getOperatorNumber()
        {
            return iNumOperator;
        }

        public static string getLanguage()
        {
            return appLang;
        }

        public static void setLanguage(string lang)
        {
            appLang = lang;
        }

        public static void setLocales(string new_locales)
        {
            locales.Clear();
            foreach (string locale in new_locales.Split(','))
                locales.Add(locale.Trim());
        }

        //! Возвращает все локали в строке через запятую
        public static string getLocales()
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

        public static bool hasErrorFinder()
        {
            return errorFinder;
        }

        public static void setErrorFinder(bool value)
        {
            errorFinder = value;
        }

        // почему-то в обычном листе нет русского
        public static List<string> getFullListLocales()
        {
            List<string> result = new List<string>();
            result.Add(CSettings.ORIGINAL_PATH);
            result.AddRange(locales);
            return result;
        }

        //! Возвращает список всех локалей в виде списка строк
        public static List<string> getListLocales()
        {
            return locales;
        }

        //! Задает номер оператора
        public static void setOperatorNumber(int _iNumOperator)
        {
            iNumOperator = _iNumOperator;
        }

        public static void setLastNpcIndex(int NpcIndex)
        {
            lastNpcIndex = NpcIndex;
        }
        public static int getLastNpcIndex()
        {
            return lastNpcIndex;
        }

        //! Сохраняет все настройки в файл settings.xml
        public static void saveSettings()
        {
            if (!Directory.Exists(SETTINGS_PATH))
                Directory.CreateDirectory(SETTINGS_PATH);
            XDocument resultDoc = new XDocument(new XElement("root"));
            resultDoc.Root.Add(new XElement("operator", iNumOperator));
            resultDoc.Root.Add(new XElement("locales", getLocales()));
            resultDoc.Root.Add(new XElement("mode", CSettings.mode.ToString()));
            resultDoc.Root.Add(new XElement("current_locale", currentLocale.ToString()));
            resultDoc.Root.Add(new XElement("LastNPcIndex", CSettings.lastNpcIndex));
            resultDoc.Root.Add(new XElement("pathQuestDataFiles", pathQuestDataFiles));
            resultDoc.Root.Add(new XElement("new_pathDialogsDataFiles", new_pathDialogsDataFiles));
            resultDoc.Root.Add(new XElement("pathToLocalFiles", pathToLocalFiles));
            resultDoc.Root.Add(new XElement("app_lang", appLang));
            resultDoc.Root.Add(new XElement("errorFinder", errorFinder));
            resultDoc.Save(SETTINGS_PATH + SETTING_FILE);
        }

        //! Устанавливает текущую локализацию по номеру в спике и возвращает true в случае успеха
        public static bool setLocale(int index_locale)
        {
            try
            {
                string locale = locales[index_locale];
                mode = MODE_LOCALIZATION;
                currentLocale = index_locale;
                checkMode();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //! Устанавливает Обычный режим (корректировка текстов)
        public static void setEditorMode()
        {
            mode = MODE_EDITOR;
            checkMode();
        }

        //! Возвращает текущий режим (Simple или Localization)
        public static int getMode()
        {
            return mode;
        }

        //! Возвращает порядковый номер текущей локали
        public static int getCurrentIndexLocale()
        {
            return currentLocale;
        }

        //! Возвращает название текущей локали
        public static string getCurrentLocale()
        {
            return locales[currentLocale];
        }

        public static string old_GetDialogTextPath()
        {
            return Path.Combine(pathToLocalFiles, ORIGINAL_PATH, dialogTextsXML);
        }
        public static string GetDialogTextPath(string local)
        {
            return Path.Combine(pathToLocalFiles, local, dialogTextsDIR);
        }

        public static string GetQuestTextPath()
        {
            return Path.Combine(pathToLocalFiles, ORIGINAL_PATH, questTextsXML);
        }

        public static string GetDialogDataPath()
        {
            return new_pathDialogsDataFiles;
        }

        public static string old_GetDialogDataPath()
        {
            return Path.Combine(old_pathDialogsDataFiles, dialogDataXML);
        }

        public static string GetDeletedQuestsPath()
        {
            return "source\\" + deletedQuests;
        }

        public static string GetQuestDataPath()
        {
            return Path.Combine(pathQuestDataFiles, questDataXML);
        }

        public static string GetDialogLocaleTextPath(string locale)
        {
            return Path.Combine(pathToLocalFiles, locale, dialogTextsDIR);
        }

        public static string GetQuestLocaleTextPath(string locale)
        {
            return Path.Combine(pathToLocalFiles, locale, questTextsXML);
        }

        public static string GetLastQuestIDPath()
        {
            return SETTINGS_PATH + lastQuestID;
        }
    }
}
