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
        public string pathToCopyFiles;

        //! Список все локализаций ENG, GER etc.
        List<string> locales = new List<string>();
        //! Текущая локализация. Пока хранит 0 - видимо для русского языка
        int currentLocale = 0;

        public int MODE_EDITOR = 0;
        public int MODE_LOCALIZATION = 1;       

        string SETTINGS_PATH = "settings/";
        string SETTING_FILE = "Settings.xml";
        string ORIGINAL_PATH = "RUS\\";

        public System.String dialogXML = "Dialogs.xml";
        public System.String questXML = "Quests.xml";
        public System.String balanceXML = "Balance.xml";

        public CSettings(MainForm parent)
        {
            this.parent = parent;
            XDocument doc = XDocument.Load(SETTINGS_PATH + SETTING_FILE);
            this.iNumOperator = int.Parse(doc.Root.Element("operator").Value.ToString());
            try
            {
                foreach (string locale in doc.Root.Element("locales").Value.ToString().Split(','))
                    locales.Add(locale);
            }
            catch
            {
                System.Console.WriteLine("Can't parse locales");
            }

            try
            {
                this.mode = int.Parse(doc.Root.Element("mode").Value.ToString());
                System.Console.WriteLine("The mode is: " + mode.ToString());
            }
            catch
            {
                System.Console.WriteLine("Can't parse mode");
            }

            try
            {
                this.currentLocale = int.Parse(doc.Root.Element("current_locale").Value.ToString());
            }
            catch
            {

            }
            try
            {
                pathToCopyFiles = doc.Root.Element("pathToCopyFiles").Value;
            }
            catch
            { 
            
            }
        }

        //! Проверяет режим, и задает соответствующую надпись на главной форме
        public void checkMode()
        {
            if (getMode() == MODE_LOCALIZATION)
                parent.Text = "QuestEditor@SO_Team Режим перевода:" + getCurrentLocale();
            else
                parent.Text = "QuestEditor@SO_Team";
            parent.onChangeMode();
        }

        //! Возврщает номер оператора
        public int getOperatorNumber()
        {
            return this.iNumOperator;
        }

        public void setLocales(string locales)
        {
            //System.Console.WriteLine("CSettings::setLocales");
            //System.Console.WriteLine("locales: " + locales);
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

        //! Сохраняет все настройки в файл settings.xml
        public void saveSettings()
        {
            XDocument resultDoc = new XDocument(new XElement("root"));
            XElement oper = new XElement("operator", iNumOperator);
            XElement loc = new XElement("locales", getLocales());
            XElement mode = new XElement("mode", this.mode.ToString());
            XElement current_locale = new XElement("current_locale", this.currentLocale.ToString());
            XElement path = new XElement("pathToCopyFiles", this.pathToCopyFiles);

            resultDoc.Root.Add(oper);
            resultDoc.Root.Add(loc);
            resultDoc.Root.Add(mode);
            resultDoc.Root.Add(current_locale);
            resultDoc.Root.Add(path);
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
            //System.Console.WriteLine("CSettings::getMode " + mode.ToString());
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

        //! Возвращает путь к файлу с балансом
        public string getBalancePath()
        {
            return balanceXML;
        }

        //! Возвращает адрес локализованного файла квестов
        public string getQuestLocalePath()
        {
            string path = pathToCopyFiles;
            path += getCurrentLocale() + "\\";
            path += questXML;
            return path;        
        }

        public string getDialogLocalePath()
        {
            string path = pathToCopyFiles;
            path += getCurrentLocale() + "\\";
            path += dialogXML;
            return path;        
        }

        //! Возвращает путь к xml файлу с квестами
        public string getQuestsPath()
        {
            string path = pathToCopyFiles;
            path += ORIGINAL_PATH;
            path += questXML;
            return path;
            //return this.questXML;
        }

        //! Возвращает путь к xml файлу с диалогами
        public string getDialogsPath()
        {
            string path = pathToCopyFiles;
            path += ORIGINAL_PATH;
            path += dialogXML;
            return path;
            //return this.dialogXML;
        }

    }
}
