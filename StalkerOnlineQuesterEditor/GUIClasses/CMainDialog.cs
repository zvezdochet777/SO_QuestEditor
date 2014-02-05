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
    //! Вспомогательный класс для сопоставления ID диалога и узла графа
    public class GraphProperties
    {
        //! ID диалога для сопоставления узлу графа
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

        //! Возвращает нужный узел по словарю <PNode, DialogID> и нужному ID диалога
        public static PNode findNodeOnID(Dictionary<PNode,GraphProperties> source, int properties)
        {
            foreach (KeyValuePair<PNode, GraphProperties> pair in source)
                if (pair.Value.getDialogID() == properties)
                    return pair.Key;
            return null;
        }

    }
}
