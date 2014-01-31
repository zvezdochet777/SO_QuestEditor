using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{

    public class CSettings
    {

        MainForm parent;
        int iNumOperator;
        int mode = 0;

        List<string> locales = new List<string>();
        int currentLocale = 0;


        public int MODE_SIMPLE = 0;
        public int MODE_LOCALIZATION = 1;

        

        string SETTINGS_PATH = "settings/";
        string SETTING_FILE = "Settings.xml";

        string LOCALES_PATH = "locales/";

        public System.String dialogXML = "Dialogs.xml";
        public System.String questXML = "Quests.xml";
        public System.String startQuestXML = "StartQuests.xml";
        public System.String balanceXML = "Balance.xml";

        public CSettings(MainForm parent)
        {
            this.parent = parent;
            XDocument doc = XDocument.Load(SETTINGS_PATH + SETTING_FILE);
            this.iNumOperator = int.Parse(doc.Root.Element("operator").Value.ToString());
            try
            {
                foreach (string locale in doc.Root.Element("locales").Value.ToString().Split(','))
                {
                    locales.Add(locale);
                }
                createLocalesFolder();
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

        }

        public void checkMode()
        {
            if (getMode() == 1)
                parent.Text = "SourceEditor@madcat Режим перевода:" + getCurrentLocale();
            else
                parent.Text = "SourceEditor@madcat";
            parent.onChangeMode();
        }

        private void createLocalesFolder()
        {

            //System.Console.WriteLine("CSettings::createLocalesFolder");
            foreach(string locale in this.locales)
                if (!Directory.Exists(LOCALES_PATH + locale))
                {
                    Directory.CreateDirectory(LOCALES_PATH + locale);
                }
        }

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
            createLocalesFolder();

        }

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

        public List<string> getListLocales()
        {
            return locales;
        }


        public void setOperatorNumber(int iNumOperator)
        {
            this.iNumOperator = iNumOperator;
        }

        public void saveSettings()
        {
            XDocument resultDoc = new XDocument(new XElement("root"));
            XElement oper = new XElement("operator", iNumOperator);
            XElement loc = new XElement("locales", getLocales());
            XElement mode = new XElement("mode", this.mode.ToString());
            XElement current_locale = new XElement("current_locale", this.currentLocale.ToString());

            resultDoc.Root.Add(oper);
            resultDoc.Root.Add(loc);
            resultDoc.Root.Add(mode);
            resultDoc.Root.Add(current_locale);
            resultDoc.Save(SETTINGS_PATH + SETTING_FILE);
        }

        public string getQuestsPath()
        {
            return this.questXML;
        }

        public string getStartQuestsPath()
        {
            return this.startQuestXML;
        }

        public string getDialogsPath()
        {
            return this.dialogXML;
        }

        public string getCurrentLocalePapth()
        {
            return "locales\\" + this.locales[currentLocale];
        }

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

        public void setSimple()
        {
            this.mode = this.MODE_SIMPLE;
            checkMode();
        }

        public int getMode()
        {
            //System.Console.WriteLine("CSettings::getMode " + mode.ToString());
            return mode;
        }

        public int getCurrentIndexLocale()
        {
            return currentLocale;
        }

        public string getCurrentLocale()
        {
            return locales[currentLocale];
        }

        public string getBalanceName()
        {
            return balanceXML;
        }

    }
}
