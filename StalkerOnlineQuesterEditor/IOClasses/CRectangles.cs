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
        private Color color;
        public float coordX;
        public float coordY;
        public int Width;
        public int Height;

        public CRectangle(int id, float _x, float _y, int width, int height, string text, Color _color)
        {
            ID = id;
            coordX = _x;
            coordY = _y;
            Width = width;
            Height = height;
            Text = text;
            color = _color;
        }

        public CRectangle(int id, PointF point, SizeF size)
        {
            ID = id;
            coordX =    point.X;
            coordY =    point.Y;
            Width = (int) size.Width;
            Height = (int) size.Height;
            Text = "";
            color = Color.Black;
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

        public Color RectColor
        {
            get { return color; }
            set { color = value; }
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

        //! Назначает текущего NPC, все операции добавления, удаления, редактирования прямугольников будут работать исходя из этого текущего NPC
        public void SetCurrentNPC(string currentNPC)
        {
            CurrentNPC = currentNPC;
            if (!Rectangles.ContainsKey(CurrentNPC))
                Rectangles.Add(CurrentNPC, new NPCRectangles());
        }

        //! Находит незанятый ID прямоугольника и возвращает его.
        private int GetNewID()
        {
            for (int newID = 0; ; newID++)
                if (!Rectangles[CurrentNPC].Keys.Contains(newID))
                    return newID;
        }

        //! Добавляет новый прямоугольник в словарь
        public int AddRectangle(string npc, float x, float y, SizeF size)
        {
            SetCurrentNPC(npc);
            int newID = GetNewID();
            CRectangle newRect = new CRectangle(newID, x, y, Convert.ToInt32(size.Width), Convert.ToInt32(size.Height), "", Color.Black);
            Rectangles[CurrentNPC].Add(newID, newRect);
            return newID;
        }

        //Задает ID текущего (выделенного) прямоугольника. Используется для события Mouse_LeftButtonClick
        public void SetSelectedRectangleID(int selectedID)
        {
            SelectedRectID = selectedID;
        }

        //! Удаляет текущий прямоугольник из словаря.
        public void RemoveRectangle()
        {
            Rectangles[CurrentNPC].Remove(SelectedRectID);
        }

        //! Меняет координаты прямоугольника, принадлежащего npc c id на новые. Используется при событии Drag.
        public void ChangeCoordinates(string npc, int id, float newX, float newY)
        {
            Rectangles[npc][id].coordX = newX;
            Rectangles[npc][id].coordY = newY;
            Console.WriteLine("ChangeCoordinates " + npc + " " + id + " " + newX + "," + newY);
        }

        public CRectangle GetSelectedRectangle()
        {
            return Rectangles[CurrentNPC][SelectedRectID];
        }

        //! Создает уникальный тег для прямоугольника на форме.
        public object SetUniqueTag(int rectangleID)
        {
            object tag = new object();
            tag = RectFlag + rectangleID.ToString();
            return tag;
        }

        //! Определяет по тегу выделенного элемента, является ли он прямоугольником. В этом случае возвращает true и его rectID 
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

        //! Возвращает словарь, состоящий из ID и CRectangles для одного NPC. Используется для рисования на Piccolo Canvas.
        public NPCRectangles GetRectanglesForNpc(string NpcName)
        {
            NPCRectangles list = new NPCRectangles();
            if (Rectangles.ContainsKey(NpcName))
                list = Rectangles[NpcName];
            return list;
        }

        //! Сохранение данных о прямоугольниках в файл Rectangles.xml
        public void SaveData()
        {
            XDocument resultDoc = new XDocument(new XElement("root"));
            XElement npcElement;

            foreach (string npcName in Rectangles.Keys)
	        {
                if (Rectangles[npcName].Count == 0)
                    continue;
                npcElement = new XElement("NPC", new XAttribute("NPC_Name", npcName));
                foreach (CRectangle rectangle in Rectangles[npcName].Values)
                {
                    Console.WriteLine("coord " + rectangle.coordX + " " + rectangle.coordY);
                    npcElement.Add(new XElement("Rect",
                            new XAttribute("ID", rectangle.GetID()),
                            new XElement("X", Convert.ToString(rectangle.coordX)),
                            new XElement("Y", Convert.ToString(rectangle.coordY)),
                            new XElement("width", rectangle.Width.ToString()),
                            new XElement("height", rectangle.Height.ToString()),
                            new XElement("Text", rectangle.GetText()),
                            new XElement("Color", rectangle.RectColor.ToKnownColor())));
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

        //! Загрузка данных о прямогоульниках из файла Rectangles.xml
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
                    float x = float.Parse(rectangle.Element("X").Value);
                    float y = float.Parse(rectangle.Element("Y").Value);
                    int width = int.Parse(rectangle.Element("width").Value);
                    int height = int.Parse(rectangle.Element("height").Value);
                    string text = rectangle.Element("Text").Value.ToString();
                    Color color = Color.Black;
                    if (rectangle.Descendants().Any(itm2 => itm2.Name == "Color"))
                        color = Color.FromName(rectangle.Element("Color").Value.ToString());

                    Rectangles[npcName].Add(id, new CRectangle(id, x ,y, width, height, text, color));
                }
            }
        }
    }
}
