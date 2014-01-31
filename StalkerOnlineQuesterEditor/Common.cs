using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StalkerOnlineQuesterEditor
{
    class Common
    {
        Dictionary<string, string> rep;
        public Common()
        {
            rep = new Dictionary<string, string>();
            rep.Add("\n", "<n>");
            rep.Add("\r", "<p>");
        }
        public string decode(string input)
        {
            //System.Console.WriteLine("Common::decode");
            //System.Console.WriteLine("input:" + input);
            //foreach (KeyValuePair<string, string> element in rep)
            //    input = input.Replace(element.Value, element.Key);
            //System.Console.WriteLine("output:" + input);
            return input;
        }
        public string encode(string input)
        {
            //System.Console.WriteLine("Common::encode");
            //System.Console.WriteLine("input:" + input);
            //foreach (KeyValuePair<string, string> element in rep)
            //    input = input.Replace(element.Key, element.Value);
            //System.Console.WriteLine("output:" + input);
            return input;
            
        }
    }
}
