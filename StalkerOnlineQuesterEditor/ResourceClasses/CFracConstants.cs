using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    public class CFracConstants
    {
        Dictionary<int, string> fractions;

        public CFracConstants()
        {
            this.fractions = new Dictionary<int, string>();
            parseFractions("source/fractions.xml");
        }

        void parseFractions(string path)
        {
            XDocument doc = XDocument.Load(path);
            foreach (XElement item in doc.Root.Elements())
            {
                int id = int.Parse(item.Element("id").Value);
                string name = item.Element("name").Value.ToString();
                fractions.Add(id, name);
            }
        }

        public int getFractionIDByDescr(string frac_name)
        {
            foreach (KeyValuePair<int, string> pair in this.fractions)
            {
                if (pair.Value == frac_name) return pair.Key;
            }
            return 0;
        }

        public string getFractionDesctByID(int frac_id)
        {
            foreach (KeyValuePair<int, string> pair in this.fractions)
            {
                if (pair.Key == frac_id) return pair.Value;
            }
            return "";
        }

        public Dictionary<int, string> getListOfFractions()
        {
            return this.fractions;
        }

        public int genLenListOfFractions()
        {
            return this.fractions.Count;
        }

    }
}
