using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace StalkerOnlineQuesterEditor
{
    //! Перечисление типов посика при локализации
    public enum FindType { none = 0, actualOnly = 1, outdatedOnly = 2, all = 3 };

    public class Global
    {
        //! Возвращает список как строку со значениями через запятую
        public static string GetListAsString<T>(List<T> list, char separator = ',')
        {
            string result = "";
            foreach (T element in list)
            {
                if (result.Equals(""))
                    result += element.ToString();
                else
                    result += separator + element.ToString();
            }
            return result;
        }



        public static string GetListAsString(List<float> list, char separator = ',')
        {
            string result = "";
            foreach (float element in list)
            {
                if (result.Equals(""))
                    result += element.ToString("G6", CultureInfo.InvariantCulture);
                else
                    result += separator + element.ToString("G6", CultureInfo.InvariantCulture);
            }
            return result;
        }

        //! Возвращает булевское значение строкой: "1" или ""
        public static string GetBoolAsString(bool booleanValue)
        {
            if (booleanValue)
                return "1";
            else
                return "";
        }

        //! Возвращает целое как строку: "123" или "" в случае нуля
        public static string GetIntAsString(int intValue)
        {
            if (intValue == 0)
                return "";
            else
                return intValue.ToString();
        }

        public static string GetNamedList(string data, List<int> list)
        {
            if (list.Count > 0)
                return data + GetListAsString(list);
            else
                return "";
        }

        public static System.Xml.XmlWriterSettings GetXmlSettings()
        {
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = false;
            return settings;
        }
    }

    //! Класс различий - содержит текущую и устаревшую версию
    public class CDifference
    {
        public int cur_version;
        public int old_version;

        public CDifference(int cur_version, int old_version)
        {
            this.cur_version = cur_version;
            this.old_version = old_version;
        }
    }
}
