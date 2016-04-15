using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    using NPCRectangles = Dictionary<int, CRectangle>;

    //! Прямоугольник, нарисованный пользователем на доске Piccolo. Хранит свои координаты и надпись на нем.
    public class CRectangle
    {
        private int ID;
        private string Text;
        public int coordX;
        public int coordY;
        public int Width;
        public int Height;

        public CRectangle(int id, int _x, int _y, int width, int height, string text)
        {
            ID = id;
            coordX = _x;
            coordY = _y;
            Width = width;
            Height = height;
            Text = text;
        }

        public CRectangle(int id, PointF point, SizeF size)
        {
            ID = id;
            coordX = (int) point.X;
            coordY = (int) point.Y;
            Width = (int) size.Width;
            Height = (int) size.Height;
        }

        public int GetID()
        {
            return ID;
        }

        public string GetText()
        {
            return Text;
        }

        public void SetText(string text)
        {
            Text = text;
        }
    }
            

    //! Класс, управляющий всеми прямоугольниками в текущей сессии.
    public class RectangleManager
    {
        private const string RectFilename = "Rectangles.xml";
        private const string RectFlag = "rect";
        private Dictionary<string, NPCRectangles> Rectangles;
        private string CurrentNPC;
        private int SelectedRectID;

        public RectangleManager()
        {
            Rectangles = new Dictionary<string, NPCRectangles>();
        }

        public void SetCurrentNPC(string currentNPC)
        {
            CurrentNPC = currentNPC;
            if (!Rectangles.ContainsKey(CurrentNPC))
                Rectangles.Add(CurrentNPC, new NPCRectangles());
        }

        private int GetNewID()
        {
            for (int newID = 0; ; newID++)
                if (!Rectangles[CurrentNPC].Keys.Contains(newID))
                    return newID;
        }

        public int AddRectangle(string npc, PointF point, SizeF size)
        {
            SetCurrentNPC(npc);
            int newID = GetNewID();
            CRectangle newRect = new CRectangle(newID, point, size);
            Rectangles[CurrentNPC].Add(newID, newRect);
            return newID;
        }

        public void RemoveRectangle()
        {
            Rectangles[CurrentNPC].Remove(SelectedRectID);
        }

        public void ChangeText(int id, string text)
        {
            Rectangles[CurrentNPC][id].SetText(text);
        }

        public void ChangeCoordinates(string npc, int id, int newX, int newY)
        {
            Rectangles[npc][id].coordX = newX;
            Rectangles[npc][id].coordY = newY;
        }

        public bool CheckIfRect(object NodeTag, out int rectID)
        {
            rectID = -1;
            if (NodeTag != null)
            {
                string tagString = NodeTag.ToString();                
                if (tagString.StartsWith(RectFlag))
                {
                    int flagLength = RectFlag.Length;
                    string idString = tagString.Substring(flagLength, tagString.Length - flagLength);
                    rectID = int.Parse(idString);
                    return true;
                }
            }
            return false;
        }

        public void SetSelectedRectangleID(int selectedID)
        {
            SelectedRectID = selectedID;
        }

        public NPCRectangles GetRectanglesForNpc(string NpcName)
        {
            NPCRectangles list = new NPCRectangles();
            if (Rectangles.ContainsKey(NpcName))
                list = Rectangles[NpcName];
            return list;
        }

        public void SaveData()
        {
            XDocument resultDoc = new XDocument(new XElement("root"));
            XElement npcElement;
            foreach (string npcName in Rectangles.Keys)
	        {
                npcElement = new XElement("NPC", new XAttribute("NPC_Name", npcName));
                foreach (CRectangle rectangle in Rectangles[npcName].Values)
                {                    
                    npcElement.Add(new XElement("Rect",
                            new XAttribute("ID", rectangle.GetID()),
                            new XElement("X", rectangle.coordX.ToString()),
                            new XElement("Y", rectangle.coordY.ToString()),
                            new XElement("width", rectangle.Width.ToString()),
                            new XElement("height", rectangle.Height.ToString()),
                            new XElement("Text", rectangle.GetText())));
                }
                resultDoc.Root.Add(npcElement);
            }
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = false;
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(RectFilename, settings))
            {
                resultDoc.Save(w);
            }        
        }

        public void LoadData()
        {
            if (!File.Exists(RectFilename))
                return;

            XDocument doc = XDocument.Load(RectFilename);
            foreach (XElement item in doc.Root.Elements())
            {
                string npcName = item.Attribute("NPC_Name").Value.ToString();
                if (!Rectangles.ContainsKey(npcName))
                    Rectangles.Add(npcName, new NPCRectangles());

                foreach (XElement rectangle in item.Elements())
                {
                    int id = int.Parse(rectangle.Attribute("ID").Value);
                    int x = int.Parse(rectangle.Element("X").Value);
                    int y = int.Parse(rectangle.Element("Y").Value);
                    int width = int.Parse(rectangle.Element("width").Value);
                    int height = int.Parse(rectangle.Element("height").Value);
                    string text = rectangle.Element("Text").Value.ToString();
                    Rectangles[npcName].Add(id, new CRectangle(id, x ,y, width, height, text));
                }
            }
        }
    }
}
