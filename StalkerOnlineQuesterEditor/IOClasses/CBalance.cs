using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{
    public class CBalance
    {

        MainForm parent;
        XDocument doc = new XDocument();

        public Dictionary<int, CBalanceFractions> fraction;

        public CBalance(MainForm parent)
        {
            this.parent = parent;
            this.fraction = new Dictionary<int, CBalanceFractions>();
            parseXML(parent.settings.getBalanceName());
        }

        private void parseXML(string xml_name)
        {
            doc = XDocument.Load(xml_name);
            foreach(XElement element in doc.Root.Element("fractions").Elements())
            {
                CBalanceFractions balance = new CBalanceFractions();

                int id = int.Parse(element.Element("id").Value);
                double limit = double.Parse(element.Element("limit").Value);
                double cat_1 = double.Parse(element.Element("cat_1").Value);
                double cat_2 = double.Parse(element.Element("cat_2").Value);
                double cat_3 = double.Parse(element.Element("cat_3").Value);

                balance.cat_1 = cat_1;
                balance.cat_2 = cat_2;
                balance.cat_3 = cat_3;
                balance.limit = limit;

                foreach (XElement penalty_fraction in element.Element("penalty").Elements())
                {
                    int penalty_id = int.Parse(penalty_fraction.Element("id").Value);
                    double penalty_cat_1 = double.Parse(penalty_fraction.Element("cat_1").Value);
                    double penalty_cat_2 = double.Parse(penalty_fraction.Element("cat_2").Value);
                    double penalty_cat_3 = double.Parse(penalty_fraction.Element("cat_3").Value);

                    balance.penalty.Add(penalty_id, new CFractionPenalty(penalty_cat_1, penalty_cat_2, penalty_cat_3));
                }

                fraction.Add(id, balance);
            }
        }

        void replaceOldFile(int start, string name, string oldName)
        {
            if (File.Exists(name + start.ToString()))
                replaceOldFile((start + 1), (name + "_" + start), name);
            else if (File.Exists(oldName))
                File.Move(oldName, name);
        }

        public void save()
        {
            saveMXL(parent.settings.getBalanceName());
        }

        public void saveMXL(string fileName)
        {
            string newOldName = (fileName.Replace(".xml", "") + "_" + DateTime.UtcNow.ToString() + ".xml").Replace(':', '_');
            replaceOldFile(0, newOldName, fileName);

            XDocument resultDoc = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement("root")
            );
            XElement fractions = new XElement("fractions");

            foreach (var key in fraction.Keys)
            {
                XElement fract = new XElement("fraction");
                var fract_info = fraction[key];



                XElement penalty =  new XElement("penalty");

                foreach (var penalty_key in fract_info.penalty.Keys)
                {
                    penalty.Add(new XElement("fraction",
                                    new XElement("id", penalty_key.ToString()),
                                    new XElement("cat_1", fract_info.penalty[penalty_key].cat_1.ToString()),
                                    new XElement("cat_2", fract_info.penalty[penalty_key].cat_2.ToString()),
                                    new XElement("cat_3", fract_info.penalty[penalty_key].cat_3.ToString())));
                }

                fract.Add(new XElement("id", key.ToString()),
                            new XElement("limit", fract_info.limit.ToString()),
                            new XElement("cat_1", fract_info.cat_1.ToString()),
                            new XElement("cat_2", fract_info.cat_2.ToString()),
                            new XElement("cat_3", fract_info.cat_3.ToString()),
                            penalty);

                fractions.Add(fract);
            }

            resultDoc.Root.Add(fractions);

            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(fileName, settings))
            {
                resultDoc.Save(w);
            }
        }
    }
}
