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
        
        protected void loadFile(string path)
        {
            try
            {
                doc = XDocument.Load(path);
            }
            catch(Exception)
            {
                System.Windows.Forms.MessageBox.Show("Не удалось загрузить файл:" + System.IO.Path.GetFullPath(path), "Ошибка");
            }


        }
    }

    public class Items
    {
        protected Dictionary<int, string> _constants = new Dictionary<int, string>();
        public Items() { }

        public void Add(int id, string name)
        {
            _constants.Add(id, name);
        }

        public string getNameByID(int id)
        {
            if (!_constants.ContainsKey(id)) return "";
            return _constants[id];
        }

        public int getIDByName(string name)
        {
            int ret = 0;
            foreach (KeyValuePair<int, string> value in _constants)
                if (value.Value.Equals(name))
                    ret = value.Key;
            return ret;
        }

        public List<int> getIDs()
        {
            List<int> ret = new List<int>();
            foreach (int key in _constants.Keys)
                ret.Add(key);
            return ret;
        }

        public List<string> getNames()
        {
            List<string> ret = new List<string>();
            foreach (string value in _constants.Values)
                ret.Add(value);
            return ret;
        }
    }


    public class RepairConstants : Constants
    {
        //Dictionary<string, string> _constants = new Dictionary<string, string>();
        //! Конструктор, заполняет словарь на основе файлов xml
        public RepairConstants()
        {
            loadFile("source/NPCrepairs.xml");
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
            loadFile("source/NPCcommands.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                item.Name.ToString();
                string cmID = item.Element("id").Value;
                string name = item.Element("name").Value;
                _constants.Add(name, cmID);
            }
        }
    }

    //! Класс, содержащий информацию предметах для NPC
    public class NPCItems
    {
        public Items primaryWeapons = new Items();
        public Items secondaryyWeapons = new Items();
        public Items body = new Items();
        //! Конструктор, заполняет словарь на основе файлов xml

        public NPCItems()
        {
            XDocument doc;
            try
            {
                doc = XDocument.Load("source/NPCItems.xml");
            }
            catch(Exception)
            {
                System.Windows.Forms.MessageBox.Show("Не удалось загрузить файл:" + System.IO.Path.GetFullPath("source/NPCItems.xml"), "Ошибка");
                return;
            }
            foreach (XElement items in doc.Root.Elements())
            {
                string items_name = items.Name.ToString();

                foreach (XElement item in items.Elements())
                {
                    int ID = Convert.ToInt32(item.Element("id").Value.ToString());

                    string name = item.Element("name").Value;
                    switch (items_name)
                    {
                        case "NPC_BODY": body.Add(ID, name); break;
                    }
                }
                
            }
            if (!System.IO.File.Exists("source/ItemWeapons.xml"))
            {
                System.Windows.Forms.MessageBox.Show("Отсуствует файл ItemStrings.xml, нужно распарсить предметы", "Ошибка");
                return;
            }
            try
            {
                doc = XDocument.Load("source/ItemWeapons.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Не удалось загрузить файл:" + System.IO.Path.GetFullPath("source/ItemWeapons.xml"), "Ошибка");
                return;
            }
            foreach (XElement items in doc.Root.Elements())
            {
                  string weapon_type = items.Name.ToString();

                    foreach (XElement item in items.Elements())
                    {
                        int ID = Convert.ToInt32(item.Element("id").Value.ToString());

                        string name = item.Element("Name").Value;
                        switch (weapon_type)
                        {
                            case "PRIMARY_WEAPONS": primaryWeapons.Add(ID, name); break;
                            case "SECONDARY_WEAPONS": secondaryyWeapons.Add(ID, name); break;
                    }
                    }
                
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
            try
            {
                doc = XDocument.Load("source/AvatarActions.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Не удалось загрузить файл:" + System.IO.Path.GetFullPath("source/AvatarActions.xml"), "Ошибка");
                return;
            }
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

    public class NPCActions : Constants
    {
        public NPCActions()
        {
            loadFile("source/NPCDialogActions.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                item.Name.ToString();
                string cmID = item.Element("id").Value;
                string name = item.Element("name").Value;
                _constants.Add(name, cmID);
            }
        }
    }

    public class ListSounds
    {
        protected List<string> _constants;

        public ListSounds()
        {
            _constants = new List<string>();
            XDocument doc;
            try
            {
                doc = XDocument.Load("source/Sounds.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Не удалось загрузить файл:" + System.IO.Path.GetFullPath("source/Sounds.xml"), "Ошибка");
                return;
            }
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
