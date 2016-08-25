using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StalkerOnlineQuesterEditor.ResourceClasses
{
    public abstract class DialogConstants
    {
        //Константы для настройки клановых опций
        public static DialogConstant CLANOPT_MEMBER     = new DialogConstant(1, "соклановец");
        public static DialogConstant CLANOPT_NOTMEMBER  = new DialogConstant(2, "не_соклановец");
        public static DialogConstant CLANOPT_ENEMY      = new DialogConstant(3, "враг");
        public static DialogConstant CLANOPT_NOTENEMY   = new DialogConstant(4, "не_враг");
        public static DialogConstant CLANOPT_PEACE      = new DialogConstant(5, "мирное_время");
        public static DialogConstant CLANOPT_CAPTURE    = new DialogConstant(6, "время_захвата");
        public static DialogConstant CLANOPT_ANYCLAN    = new DialogConstant(7, "клан");
        public static DialogConstant CLANOPT_SINGLE     = new DialogConstant(8, "бесклановый_одиночка");


        public static int getNumByText(string text)
        {
            return 0;
        }

        public static string getTextByNum(int num)
        {
            return "";
        }

    }

    public sealed class DialogConstant
    {
        int num;
        public string text;


        public DialogConstant(int num, string text)
        {
            this.num = num;
            this.text = text;
        }

        public string getText()
        {
            return text;
        }

        public int getNum()
        {
            return num;
        }
    }
}
