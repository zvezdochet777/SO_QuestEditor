using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{
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

    public class PvPRanks: Constants
    {

        public PvPRanks()
        {
            string path = "../../../res/scripts/common/pvp_rating_config.py";

            string name = "";
            string rating = "0";

            string local_name_path = "../../../res/local/Russian/pvp_rating_config.xml";
            Dictionary<string, string> names = new Dictionary<string, string>();
            doc = XDocument.Load(local_name_path);
            foreach (XElement item in doc.Root.Elements("rank").Elements())
            {
                names.Add(item.Name.ToString(), item.Value);
            }

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("\"name\":"))
                    {
                        name = line.Split(':')[1].Split(',')[0].Replace("\"", "");
                        name = names[name.Split('.').Last()];
                    }
                    else if (line.Contains("\"rating\":"))
                    {
                        rating = line.Split(':')[1].Split(',')[0].Replace("\"", "");
                        _constants.Add(name, rating);
                    }
                }
            }
        }
    }
}
