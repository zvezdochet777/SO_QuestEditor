using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{

    public class COperNote
    {
        public string sHistory;
        public string iOperator;
        public string iLevel;
        public int iWorked;

        public COperNote()
        {
            this.iLevel = "";
            this.iWorked = new int();
            this.iOperator = "";
            this.sHistory = "";
        }

        public COperNote(string iLevel, int iWorked, string iOperator, string sHistory)
        {
            this.iLevel = iLevel;
            this.iWorked = iWorked;
            this.iOperator = iOperator;
            this.sHistory = sHistory;
        }

        public XElement getXML()
        {
            XElement ret = new XElement("note",
                new XElement("level", iLevel),
                new XElement("worked", iWorked.ToString()),
                new XElement("operator", iOperator),
                new XElement("history", sHistory));
            return ret;
        }
    }

    public class COperNotes
    {
        string fileName;

        Dictionary<int, COperNote> notes;

        public COperNotes(string docPath)
        {
            this.fileName = docPath;
            this.notes = new Dictionary<int, COperNote>();

            if (File.Exists(docPath))
            {
                XDocument notesDoc = XDocument.Load(docPath);
                foreach (XElement item in notesDoc.Root.Elements())
                {
                    int id = int.Parse(item.Element("id").Value);
                    string level = item.Element("level").Value;
                    int worked = int.Parse(item.Element("worked").Value);
                    string oper = item.Element("operator").Value;
                    string hist = item.Element("history").Value;
                    this.notes.Add(id, new COperNote(level, worked, oper, hist));

                }
            }
        }

        public void addNote(int questID, string iLevel, int iWorked, string iOperator, string sHistory)
        {
            this.notes[questID] = new COperNote(iLevel, iWorked, iOperator, sHistory);
        }

        public COperNote getNote(int questID)
        {
            if (notes.Keys.Contains(questID))
                return notes[questID];
            else return null;
        }


        public void save()
        {
            XDocument saveDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("root")
                );
            List<XElement> elements = new List<XElement>();
            foreach (KeyValuePair<int, COperNote> note in notes)
            {
                XElement el = note.Value.getXML();
                el.Add(new XElement("id", note.Key.ToString()));
                elements.Add(el);
            }
            saveDoc.Root.Add(elements);
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(this.fileName, settings))
            {
                saveDoc.Save(w);
            }
        }

    }


}
