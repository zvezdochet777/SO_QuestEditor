using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{

    public class PerksConstants
    {

        //! Конструктор, заполняет словарь на основе файлов xml
        public PerksConstants()
        { 
            loadFile("source/perks.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                int cmID = Convert.ToInt32(item.Element("id").Value);
                string name = item.Element("name").Value;
                _constants.Add(cmID, name);
            }

        }

        protected Dictionary<int, string> _constants = new Dictionary<int, string>();
        protected XDocument doc = new XDocument();

        //! Возвращает ID комманды
        public int getID(string name)
        {
            int ret = 0;
            foreach (KeyValuePair<int, string> value in _constants)
                if (value.Value.Equals(name))
                    ret = value.Key;
            return ret;
        }
        //! Возвращает название по ID комманды
        public string getName(int tpID)
        {
            return _constants[tpID];

            
        }
        //! Возвращает список всех названий по-русски комманд
        public List<string> getNames()
        {
            List<string> ret = new List<string>();
            foreach (string key in _constants.Values)
                ret.Add(key);
            return ret;
        }

        protected void loadFile(string path)
        {
            try
            {
                doc = XDocument.Load(path);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Не удалось загрузить файл:" + System.IO.Path.GetFullPath(path), "Ошибка");
            }


        }
    }




    //! Класс, содержащий информацию о всех навыках
    public class SkillConstants: Constants
    {

        //! Конструктор, заполняет словарь на основе файлов xml
        public SkillConstants()
        {
            doc = XDocument.Load("source/Skills.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                string cmID = item.Element("id").Value;
                string name = item.Element("name").Value;
                _constants.Add(name, cmID);
            }
                        
        }


    }

    public class DialogSkill
    {
        public string max;
        public string min;

        public DialogSkill(string min, string max)
        {
            this.min = min;
            this.max = max;
        }

        public string getValue()
        {
            return min + ":" + max;
        }
    }
    public class ListDialogSkills
    {
        protected Dictionary<string, DialogSkill> _skills = new Dictionary<string, DialogSkill>();

        public ListDialogSkills()
        {

        }

        public void Add(string name, string minVal = "", string maxVal = "")
        {
            _skills.Add(name, new DialogSkill(minVal, maxVal));
        }

        public bool Any()
        {
            return _skills.Any();
        }

        public void Clear()
        {
            _skills.Clear();
        }

        public DialogSkill getSkillValByName(string name)
        {
            if (_skills.ContainsKey(name))
                return _skills[name];
            return null;
        }


        public XElement getSkills()
        {
            XElement result_skills = null;
            if (!this.Any())
                return null;
            foreach(string skill_name in this._skills.Keys)
            {
                if (_skills[skill_name].getValue() == ":")
                    continue;
                if (result_skills == null)
                    result_skills = new XElement("Skills", new XElement("skill",
                                                              new XElement("id", skill_name),
                                                              new XElement("value", _skills[skill_name].getValue())));
                else
                    result_skills.Add(new XElement("skill", new XElement("id", skill_name), new XElement("value", _skills[skill_name].getValue())));
            }
            return result_skills;
        }
    }

    

}
