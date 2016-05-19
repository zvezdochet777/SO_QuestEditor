using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMD.HCIL.Piccolo;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{
    public struct NpcData
    {
        public string rusName;
        public string engName;
        public string location;
        public string coordinates;

        public NpcData(string rn, string en, string loc, string cd)
        {
            rusName = rn;
            engName = en;
            location = loc;
            coordinates = cd;
        }
    }

}
