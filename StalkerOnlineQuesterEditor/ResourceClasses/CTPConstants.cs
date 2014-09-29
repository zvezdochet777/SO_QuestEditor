using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace StalkerOnlineQuesterEditor
{
    //! Класс, содержащий информацию о всех возможных точках, куда может телепортироваться игрок
    public class CTPConstants
    {
        //! Словарь <Описание телепорта по-русски, ID телепорта>
        Dictionary<string, string> tp = new Dictionary<string, string>();
        XDocument doc = new XDocument();

        //! Конструктор, заполняет словарь на основе файлов xml
        public CTPConstants()
        {
            // берем неназванные точки телепорта из файла, созданного парсером по всем chunk игры
            XDocument allPoints = new XDocument();
            List<string> realTeleports = new List<string>();
            allPoints = XDocument.Load("source/teleport_points.xml");
            foreach (XElement item in allPoints.Root.Elements())
            {
                string id = item.Element("id").Value;
                //if (!tp.ContainsKey(id))
                    //tp.Add(id, id);
                realTeleports.Add(id);
            }
            
            doc = XDocument.Load("source/TPoints.xml");
            foreach (XElement item in doc.Root.Elements())
            {
                string tpID = item.Element("id").Value;
                string name = item.Element("name").Value;
                if (realTeleports.Contains(tpID))
                    tp.Add(name, tpID);
                else
                    tp.Add(name + " ОШИБКА", tpID);
            }

            foreach (string real in realTeleports)
            {
                if (!tp.ContainsValue(real))
                    tp.Add(real + " тест", real);
            }
            
        }
        //! Возвращает ID точки телепорта по названию
        public string getTtID(string name)
        {
            return tp[name];
        }
        //! Возвращает название по ID точки телепорта
        public string getName(string tpID)
        {
            string ret = "";
            foreach (KeyValuePair<string, string> value in tp)
                if (value.Value.Equals(tpID))
                    ret = value.Key;
            return ret;
        }
        //! Возвращает список всех названий по-русски точек телепорта
        public List<string> getKeys()
        {
            List<string> ret = new List<string>();
            foreach (string key in tp.Keys)
                ret.Add(key);
            return ret;
        }
    }
}
