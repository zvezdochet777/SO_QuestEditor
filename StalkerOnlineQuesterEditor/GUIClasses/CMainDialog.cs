using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMD.HCIL.Piccolo;
using UMD.HCIL.Piccolo.Nodes;
using UMD.HCIL.Piccolo.Util;
using UMD.HCIL.Piccolo.Event;

namespace StalkerOnlineQuesterEditor
{
    public class GraphProperties
    {
        int DialogID;


        public GraphProperties()
        {
            this.DialogID = new int();

        }
        public GraphProperties(int dialogID)
        {
            this.DialogID = dialogID;

        }

        public int getDialogID()
        {
            return DialogID;
        }

        public static PNode findNodeOnID(Dictionary<PNode,GraphProperties> source, int properties)
        {
            foreach (KeyValuePair<PNode, GraphProperties> pair in source)
                if (pair.Value.getDialogID() == properties)
                    return pair.Key;
            return null;
        }

    }
}
