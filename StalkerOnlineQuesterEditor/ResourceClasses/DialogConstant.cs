using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    public abstract class DialogConstants
    {
        //Константы для настройки клановых опций
        public static DialogConstant CLANOPT_MEMBER = new DialogConstant(1, "соклановец");
        public static DialogConstant CLANOPT_NOTMEMBER = new DialogConstant(2, "не_соклановец");
        public static DialogConstant CLANOPT_ENEMY = new DialogConstant(3, "враг");
        public static DialogConstant CLANOPT_NOTENEMY = new DialogConstant(4, "не_враг");
        public static DialogConstant CLANOPT_PEACE = new DialogConstant(5, "мирное_время");
        public static DialogConstant CLANOPT_CAPTURE = new DialogConstant(6, "время_захвата");
        public static DialogConstant CLANOPT_ANYCLAN = new DialogConstant(7, "клан");
        public static DialogConstant CLANOPT_SINGLE = new DialogConstant(8, "бесклановый_одиночка");


        public static int getNumByText(string text)
        {
            return 0;
        }

        public static string getTextByNum(int num)
        {
            return "";
        }

    }

    public sealed class DialogConstant
    {
        int num;
        public string text;


        public DialogConstant(int num, string text)
        {
            this.num = num;
            this.text = text;
        }

        public string getText()
        {
            return text;
        }

        public int getNum()
        {
            return num;
        }
    }


    public class Constants
    {
        protected Dictionary<string, string> _constants = new Dictionary<string, string>();
        protected XDocument doc = new XDocument();

        //! Конструктор, заполняет словарь на основе файлов xml
        public Constants()
        {

        }
        //! Возвращает ID комманды
        public string getTtID(string name)
        {
            return _constants[name];
        }
        //! Возвращает название по ID комманды
        public string getName(string tpID)
        {
            string ret = "";
            foreach (KeyValuePair<string, string> value in _constants)
                if (value.Value.Equals(tpID))
                    ret = value.Key;
            return ret;
        }
        //! Возвращает список всех названий по-русски комманд
        public List<string> getKeys()
        {
            List<string> ret = new List<string>();
            foreach (string key in _constants.Keys)
                ret.Add(key);
            return ret;
        }
    }


    public class RepairConstants : Constants
    {
        //Dictionary<string, string> _constants = new Dictionary<string, string>();
        //! Конструктор, заполняет словарь на основе файлов xml
        public RepairConstants()
        {
            doc = XDocument.Load("source/NPCrepairs.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                string cmID = item.Element("id").Value;
                string name = item.Element("name").Value;
                _constants.Add(name, cmID);
            }
        }
    }

    //! Класс, содержащий информацию командах для NPC
    public class CommandConstants : Constants
    {
        //Dictionary<string, string> _constants = new Dictionary<string, string>();
        //! Конструктор, заполняет словарь на основе файлов xml
        public CommandConstants()
        {
            doc = XDocument.Load("source/NPCcommands.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                string cmID = item.Element("id").Value;
                string name = item.Element("name").Value;
                _constants.Add(name, cmID);
            }
        }
    }

    public class AvatarActions
    {
       protected List<string> _constants;

        public AvatarActions()
        {
            _constants = new List<string>();
            XDocument doc;
            doc = XDocument.Load("source/AvatarActions.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                string name = item.Value;
                _constants.Add(name);
            }
        }

        public List<string> getKeys()
        {
            return _constants;
        }
    }

    public class ListSounds
    {
        protected List<string> _constants;

        public ListSounds()
        {
            _constants = new List<string>();
            XDocument doc;
            doc = XDocument.Load("source/Sounds.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                string name = item.Value;
                _constants.Add(name);
            }
        }

        public List<string> getKeys()
        {
            return _constants;
        }
    }
}
