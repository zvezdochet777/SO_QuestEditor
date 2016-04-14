using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    //! Прямоугольник, нарисованный пользователем на доске Piccolo. Хранит свои координаты и надпись на нем.
    public class CRectangle
    {
        private int ID;
        private string Text;
        public int coordX;
        public int coordY;
        public int Width;
        public int Height;

        public CRectangle(int id, int _x, int _y, int width, int height)
        {
            ID = id;
            coordX = _x;
            coordY = _y;
            Width = width;
            Height = height;
        }

        public CRectangle(int id, Point point, Size size)
        {
            ID = id;
            coordX = point.X;
            coordY = point.Y;
            Width = size.Width;
            Height = size.Height;
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
        public Dictionary<int, CRectangle> Rectangles;

        public RectangleManager()
        {
            Rectangles = new Dictionary<int, CRectangle>();
        }

        private int GetNewID()
        {
            for (int newID = 0; ; newID++)
                if (!Rectangles.Keys.Contains(newID))
                    return newID;
        }

        public void AddRectangle(Point point, Size size)
        {
            int newID = GetNewID();
            CRectangle newRect = new CRectangle(newID, point, size);
            Rectangles.Add(newID, newRect);
        }

        public void RemoveRectangle(int id)
        {
            Rectangles.Remove(id);
        }

        public void ChangeText(int id, string text)
        {
            Rectangles[id].SetText(text);
        }

        public void SaveData()
        {
            XDocument resultDoc = new XDocument(new XElement("root"));
            XElement rectElement;
            foreach (CRectangle rectangle in Rectangles.Values)
            {
                //npc_element = new XElement("NPC", new XAttribute("NPC_Name", NPC_Name));
                rectElement = new XElement("Rect");
                rectElement.Add(new XAttribute("ID", rectangle.GetID()),
                        new XElement("X", rectangle.coordX.ToString()),
                        new XElement("Y", rectangle.coordY.ToString()),
                        new XElement("width", rectangle.Width.ToString()),
                        new XElement("height", rectangle.Height.ToString()),
                        new XElement("Text", rectangle.GetText()));
                resultDoc.Root.Add(rectElement);
            }
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create("Rectangles.xml", settings))
            {
                resultDoc.Save(w);
            }        
        }
    }
}
