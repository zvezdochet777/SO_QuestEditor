using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UMD.HCIL.Piccolo;
using UMD.HCIL.Piccolo.Nodes;
using UMD.HCIL.Piccolo.Util;
using UMD.HCIL.Piccolo.Event;
using System.Collections;
using System.Xml.Linq;

namespace StalkerOnlineQuesterEditor
{
    //! Словарь <DialogID, CDialog> - используется для диалогов 1 персонажа NPC
    using DialogDict = Dictionary<int, CDialog>;
    //! Словарь <уровень в иерархии, список узлов>
    using levelDict = Dictionary<int, List<int>>;
    //! Тип выделенного элемента на экране
    public enum SelectedItemType
    { 
        none = 0,
        dialog = 1,
        rectangle = 2
    };

    public partial class MainForm : Form
    {
        public levelDict nodesOnLevel;
        //DIALOGS
        //******************SETTERS/GETTERS****************************
        //! Возвращает вершину графа диалогов - корневую фразу
        CDialog getRootDialog()
        {
            DialogDict Dialogs = getDialogDictionary(currentNPC); //this.dialogs.dialogs[currentNPC];
            CDialog result = null;
            foreach (CDialog dialog in Dialogs.Values)
            {
                if (dialog.coordinates.RootDialog)
                {
                    rootElements.Add(dialog.DialogID);
                    result = dialog;
                }
            }
            
            // костыль для локализации
            if (result == null)
            {
                Dialogs = this.dialogs.dialogs[currentNPC];
                foreach (CDialog dialog in Dialogs.Values)
                {
                    if (dialog.coordinates.RootDialog)
                    {
                        rootElements.Add(dialog.DialogID);
                            string loc = settings.getCurrentLocale();
                            if (!dialogs.locales[loc].ContainsKey(currentNPC))
                            {
                                CDialog toadd = new CDialog();
                                toadd = (CDialog)dialog.Clone();
                                toadd.Text = "";
                                toadd.Title = "";
                                toadd.version = 0;
                                //dialogs.locales[loc].Add(;
                                Dictionary<int, CDialog> newdict = new Dictionary<int, CDialog>();
                                newdict.Add(toadd.DialogID, toadd);
                                dialogs.locales[loc].Add(currentNPC, newdict);
                            }

                            else if (!dialogs.locales[loc][currentNPC].ContainsKey(dialog.DialogID))
                            {
                                CDialog toadd = (CDialog)dialog.Clone();
                                toadd.Text = "";
                                toadd.Title = "";
                                toadd.version = 0;
                                dialogs.locales[settings.getCurrentLocale()][currentNPC].Add(toadd.DialogID, toadd);
                            }
                            else
                                dialogs.locales[settings.getCurrentLocale()][currentNPC][dialog.DialogID].coordinates.RootDialog = true;
                        result = dialog;
                    }
                }
            }
            

            return result;
        }

        //! Возвращает Узел по известному ID диалога
        public PNode getNodeOnDialogID(int dialogID)
        {
            return GraphProperties.findNodeOnID(graphs, dialogID);
        }
        //! Возвращает DialogID по известному узлу графа
        public int getDialogIDOnNode(PNode node)
        {
            if (graphs.Keys.Contains(node))
                return graphs[node].getDialogID();
            else return 0;
        }

        public List<CDialog> getDialogsWithDialogIDInNodes(int dialogID)
        {
            List<CDialog> ret = new List<CDialog>();
            foreach (CDialog dialog in dialogs.dialogs[currentNPC].Values)
                if (dialog.Nodes.Contains(dialogID))
                    ret.Add(dialog);
            return ret;
        }

        //! Возвращает экземпляр диалога по ID (всегда по-русски)
        public CDialog getDialogOnDialogID(int dialogID)
        {
            if (dialogID != 0)
                return dialogs.dialogs[currentNPC][dialogID];
            else
                return null;
        }

        //! Возвращает диалог по ID, если он принадлежит этому NPC (всегда по-русски)
        public CDialog getDialogOnDialogID(string npc_name, int dialogID)
        {
            if (dialogs.dialogs.Keys.Contains(npc_name) && (dialogID != 0) && dialogs.dialogs[npc_name].Keys.Contains(dialogID))
                return dialogs.dialogs[npc_name][dialogID];
            else
                return null;
        }

        //! Возвращает диалог по ID в зависимости от режима и локализации
        public CDialog getDialogOnIDConditional(int dialogID)
        {
            if (dialogID != 0)
            {
                if (settings.getMode() == settings.MODE_EDITOR)
                    return dialogs.dialogs[currentNPC][dialogID];
                else
                {
                    CDialog dd = new CDialog();
                    dd = dialogs.getLocaleDialog(dialogID, settings.getCurrentLocale(), currentNPC);
                    //CDialog d = dialogs.getLocaleDialog(dialogID, settings.getCurrentLocale(), currentNPC);
                    if (dd != null)
                        return dd;
                    else
                    {
                        dd = (CDialog) dialogs.dialogs[currentNPC][dialogID].Clone();
                        dd.version = 0;
                        return dd;
                    }
                }
            }
            else
                return null;
        }

        //! Возвращает словарь диалогов одного NPC в зависимости от локализации
        public DialogDict getDialogDictionary(string NPCName)
        {
            if (settings.getMode() == settings.MODE_EDITOR)
                return dialogs.dialogs[NPCName];
            else
            { 
                if (dialogs.locales[ settings.getCurrentLocale() ].ContainsKey( NPCName ) )
                    return dialogs.locales[ settings.getCurrentLocale() ][NPCName];
                else
                    return dialogs.dialogs[NPCName];
            }
        }

        //! Возвращает ID для нового диалога
        public int getDialogsNewID()
        {
            List<int> availableID = new List<int>();
            foreach (Dictionary<int, CDialog> pairDialog in dialogs.dialogs.Values)
                foreach (CDialog dial in pairDialog.Values)
                    availableID.Add(dial.DialogID);
            for (int i = 1; ; i++)
                if (!availableID.Contains(i))
                    return i;
        }

        //**********************WORK WITH FORM ****************************************************

        void fillDialogTree(CDialog root, DialogDict dialogs)
        {
            this.treeDialogs.Nodes.Clear();//tree clear
            this.treeDialogs.Nodes.Add("Active", "Active");
            this.treeDialogs.Nodes.Add("Recycle", "Recycle");
            foreach (TreeNode treeNode in this.treeDialogs.Nodes.Find("Active", true))
                treeNode.Nodes.Add(root.DialogID.ToString(), root.DialogID.ToString());
            this.fillNPCBoxSubquests(root);

            TreeNode treeActiveNode = this.treeDialogs.Nodes.Find("Active", true)[0];
            TreeNode treeRecycleNode = this.treeDialogs.Nodes.Find("Recycle", true)[0];
            foreach (CDialog dialog in dialogs.Values)
                if (!treeActiveNode.Nodes.ContainsKey(dialog.DialogID.ToString()))
                {
                    treeRecycleNode.Nodes.Add(dialog.DialogID.ToString(), dialog.DialogID.ToString());
                    dialog.coordinates.Active = false;
                    setNonActiveDialog(dialog.Holder, dialog.DialogID);
                }

            this.treeDialogs.ExpandAll();
        }
        //! Удаляет диалог из локализаций при его удалении из русской части диалогов
        void setNonActiveDialog(string holder, int id)
        {
            dialogs.locales[settings.getListLocales()[0]][holder][id].coordinates.Active = false;
        }

        //! Заполняет граф диалога нужными узлами
        void fillDialogGraphView(CDialog root)
        {
            // Initialize, and create a layer for the edges (always underneath the nodes)
            this.DialogShower.Layer.RemoveAllChildren();
            this.DialogShower.Camera.RemoveAllChildren();
            nodeLayer = new PNodeList();
            edgeLayer = new PLayer();
            drawingLayer = new PLayer();

            DialogShower.Camera.AddChild(drawingLayer);                      
            DialogShower.Camera.AddChild(edgeLayer);
            DrawRectangles();

            // Show root node
            float rootx = root.coordinates.X;
            float rooty = root.coordinates.Y;
            if (rootx == 0 && rooty == 0)
            {
                rootx = (float)(this.ClientSize.Width / 5);
                rooty = (float)(this.ClientSize.Height / 5);
            }
            SizeF size = CalcEllipsisSizeForNode(root.DialogID);
            PNode rootNode = PPath.CreateEllipse(rootx, rooty, size.Width, size.Height);
            rootNode.Brush = Brushes.Green;

            PText rootText = new PText(root.DialogID.ToString());
            rootText.Pickable = false;
            rootText.X = rootNode.X + 15;
            rootText.Y = rootNode.Y + 10;
            rootNode.Tag = new ArrayList();

            //          ((ArrayList)rootNode.Tag).Add(root.DialogID);
            rootNode.AddChild(rootText);
            nodeLayer.Add(rootNode);
            if (!graphs.Keys.Contains(rootNode))
                graphs.Add(rootNode, new GraphProperties(root.DialogID));
            SaveCoordinates(root, rootNode, true);
            this.fillDialogSubgraphView(root, rootNode, 1, ref edgeLayer, ref nodeLayer, false);

            this.DialogShower.Camera.AddChildren(nodeLayer);            
            //edgeLayer.MoveToFront();
            //drawingLayer.MoveToBack();
            //CalcNodesOnLevel(root);
        }

        //! @brief Отображает все дочерние узлы на графе диалогов 
        //! @param root Старший диалог, экземпляр CDialog
        //! @param rootNode Старший узел, экземпляр PNode
        //! @param level Уровень наследования узлов
        //! @param edgeLayer
        //! @param nodeLayer
        //! @param stopAfterThat
        void fillDialogSubgraphView(CDialog root, PNode rootNode, float level, ref PLayer edgeLayer, ref PNodeList nodeLayer, bool stopAfterThat)
        {
            //System.Console.WriteLine("subgraph: dialogID: " + root.DialogID.ToString() + ", level: " + level.ToString());
            float ix = rootNode.X;
            float iy = rootNode.Y;
            float i = 1;//Number of elements in string
            float localLevel = level;
            //System.Console.WriteLine("dialogID:" + root.DialogID + " toDialog:" + root.Actions.ToDialog);
            if (root.Actions.ToDialog != 0)
            {
                //System.Console.WriteLine("To dialog ID:"+root.Actions.ToDialog+" of "+root.DialogID);
                PNode toDialogNode = getNodeOnDialogID(root.Actions.ToDialog);

                if (toDialogNode == null)
                    System.Console.WriteLine("Node is miss, dialogID = " + root.Actions.ToDialog.ToString());
                else
                {
                    PrepareNodesForEdge(toDialogNode, rootNode, ref edgeLayer);
                    nodeLayer.Add(toDialogNode);
                    if (!stopAfterThat)
                    {
                        if (!isRoot(root.Actions.ToDialog))
                        {
                            if ( dialogs.dialogs[currentNPC][root.Actions.ToDialog].Nodes.Any() )
                                this.fillDialogSubgraphView(this.dialogs.dialogs[currentNPC][root.Actions.ToDialog], toDialogNode, localLevel + 1, ref edgeLayer, ref nodeLayer, false);
                            else if ( dialogs.dialogs[currentNPC][root.Actions.ToDialog].Actions.ToDialog != 0 )
                                this.fillDialogSubgraphView(this.dialogs.dialogs[currentNPC][root.Actions.ToDialog], toDialogNode, localLevel, ref edgeLayer, ref nodeLayer, true);
                        }
                    }
                }
            }
            else
                foreach (int subdialogs in root.Nodes)
                {
                    PNode node = getNodeOnDialogID(subdialogs);
                    float x = getDialogOnIDConditional(subdialogs).coordinates.X;
                    float y = getDialogOnIDConditional(subdialogs).coordinates.Y;

                    if (x == 0 && y == 0)
                    {
                        i++;
                        x = (float)(ix) + (120 * i) - 80 * root.Nodes.Count - 40 * level;
                        y = (float)(iy + 60) + 50 * level;
                    }

                    if (node == null)
                    {
                        SizeF size = CalcEllipsisSizeForNode(subdialogs);
                        node = PPath.CreateEllipse(x, y, size.Width, size.Height);
                        PText text = new PText(subdialogs.ToString());
                        text.Pickable = false;
                        text.X = node.X + 15;
                        text.Y = node.Y + 10;
                        node.Tag = new ArrayList();
                        //((CMainDialog)node).DialogID = subdialogs;
                        //((ArrayList)node.Tag).Add(subdialogs);
                        node.AddChild(text);
                    }
                    SaveCoordinates( dialogs.dialogs[currentNPC][subdialogs], node);
                    
                    PrepareNodesForEdge( node, rootNode, ref edgeLayer);
                    nodeLayer.Add(node);
                    if (!graphs.Keys.Contains(node))
                        graphs.Add(node, new GraphProperties(subdialogs));
                    if (!stopAfterThat)
                    {

                        if ( dialogs.dialogs[currentNPC][subdialogs].Nodes.Any() )
                            this.fillDialogSubgraphView(dialogs.dialogs[currentNPC][subdialogs], node, localLevel + 1, ref edgeLayer, ref nodeLayer, false);
                        else if ( dialogs.dialogs[currentNPC][subdialogs].Actions.ToDialog != 0 )
                            this.fillDialogSubgraphView(dialogs.dialogs[currentNPC][subdialogs], node, localLevel, ref edgeLayer, ref nodeLayer, true);
                    }
                }
        }

        //! Добавляет узел на граф
        void addNodeOnDialogGraphView(int dialogID, int parentDialogID)
        {
            PNode parentDialog = getNodeOnDialogID(parentDialogID);

            float x = new float();
            x = parentDialog.X - 60;
            float y = new float();
            y = parentDialog.Y + 60;

            SizeF size = CalcEllipsisSizeForNode(dialogID);
            PNode newDialog = PPath.CreateEllipse(x, y, size.Width, size.Height);
            PText text = new PText(dialogID.ToString());
            text.Pickable = false;
            text.X = newDialog.X + 15;
            text.Y = newDialog.Y + 10;
            newDialog.Tag = new ArrayList();
            newDialog.AddChild(text);
            PrepareNodesForEdge(newDialog, parentDialog, ref edgeLayer);
            nodeLayer.Add(newDialog);

            if ((!getDialogOnDialogID(dialogID).Actions.Exit) && (getDialogOnDialogID(dialogID).Actions.ToDialog != 0))
            {
                PNode target = getNodeOnDialogID(getDialogOnDialogID(dialogID).Actions.ToDialog);
                PrepareNodesForEdge(newDialog, target, ref edgeLayer);
            }

            //updateEdge(edge);
            DialogShower.Layer.AddChildren(nodeLayer);

            if (!graphs.Keys.Contains(newDialog))
                graphs.Add(newDialog, new GraphProperties(dialogID));

            if (getDialogOnDialogID(dialogID).Nodes.Any())
                foreach (int subdialog in getDialogOnDialogID(dialogID).Nodes)
                    addNodeOnDialogGraphView(subdialog, dialogID);

            DialogSelected(false);
        }

        //! Возвращает размер эллипса для Узла диалога по заданному ID диалога (дли широких надписей размер больше)
        SizeF CalcEllipsisSizeForNode(int dialogId)
        {
            SizeF size = new SizeF(0,0);
            if (dialogId / 1000 == 0)
                size = new SizeF(50, 30);
            else if (dialogId / 1000 > 0)
                size = new SizeF(60, 40);
            return size;
        }

        //! Добавляем в теги узлов данные о гранях, в теги граней - данные об узлах
        void PrepareNodesForEdge(PNode node1, PNode node2, ref PLayer edgeLayer)
        {
            PPath edge = new PPath();
            edge.Pickable = false;
            ((ArrayList)node1.Tag).Add(edge);
            ((ArrayList)node2.Tag).Add(edge);
            edge.Tag = new ArrayList();
            ((ArrayList)edge.Tag).Add(node1);
            ((ArrayList)edge.Tag).Add(node2);
            edgeLayer.AddChild(edge);
            updateEdge(edge);
        }

        //! Создает линии - связи между узлами на графе диалогов
        public static void updateEdge(PPath edge)
        {
            // Note that the node's "FullBounds" must be used (instead of just the "Bound") 
            // because the nodes have non-identity transforms which must be included when
            // determining their position.
            ArrayList nodes = (ArrayList)edge.Tag;
            PNode node1 = (PNode)nodes[0];
            PNode node2 = (PNode)nodes[1];
            PointF start = PUtil.CenterOfRectangle(node1.FullBounds);
            PointF end = PUtil.CenterOfRectangle(node2.FullBounds);
            edge.Reset();
            edge.AddLine(start.X, start.Y, end.X, end.Y);
        }
        
        //! Сохраняет координаты узла 
        public void SaveCoordinates(CDialog dialog, PNode node, bool isRoot)
        {
            dialog.coordinates.X = (int) node.FullBounds.X;
            dialog.coordinates.Y = (int) node.FullBounds.Y;
            dialog.coordinates.RootDialog = isRoot;
            //! костылек
            if (settings.getMode() == settings.MODE_LOCALIZATION)
            {
                string locale = settings.getCurrentLocale();
                if (dialogs.locales[settings.getCurrentLocale()].ContainsKey(dialog.Holder))
                {
                    string npc = dialog.Holder;
                    if ( dialogs.locales[locale][npc].ContainsKey(dialog.DialogID) )
                     dialogs.locales[locale][npc][dialog.DialogID].coordinates.RootDialog = isRoot;
                }
            }
        }
        //! Сохраняет координаты узла со значением false для параметра isRoot
        public void SaveCoordinates(CDialog dialog, PNode node)
        {
            SaveCoordinates(dialog, node, false);
        }

        //! Считает число нодов в каждом уровне. Можно убрать, оставив рекурсивный вызов AddNodesToLevel
        void CalcNodesOnLevel(CDialog root)
        {
            // словарь level - nodes list
            levelDict nodesOnLevel = new levelDict();
            List<int> nodes = new List<int>();
            System.Console.WriteLine("*****NodesOnLevel*******");

            AddNodesToLevel(root, 1, ref nodesOnLevel);

            // вывод в консоль
            for (int i = 1; i <= nodesOnLevel.Count; i++)
            {
                System.Console.WriteLine("\nLevel " + i.ToString() + ":  (totally:" + nodesOnLevel[i].Count.ToString() +")"); 
                for (int k = 0; k < nodesOnLevel[i].Count; k++)
                    System.Console.Write(" " + nodesOnLevel[i][k]);
            }
            //************
        }
        //! рекурсивно считает число нодов на каждом уровне и сохраняет в словарь
        void AddNodesToLevel(CDialog root, int level, ref levelDict dict)
        {
            List<int> temp = new List<int>();
            if (!dict.Keys.Contains(level))
                dict.Add(level, temp);
            foreach (int node in root.Nodes)
            {
                dict[level].Add(node);
                CDialog dialogT = getDialogOnDialogID(node);
                AddNodesToLevel(dialogT, level + 1, ref dict);
            }
        }

        void removeNodeFromDialogGraphView(int node)
        {
            bool haveBeenDeleted = false;
            CDialog dialog = this.dialogs.dialogs[currentNPC][node];

            foreach (KeyValuePair<int, CDialog> dial in dialogs.dialogs[currentNPC])
            {
                dial.Value.Nodes.Remove(node);
                dialogs.locales[settings.getListLocales()[0]][currentNPC][dial.Value.DialogID].Nodes.Remove(node);
            }
            //foreach (CDialog dial in this.NPCDialogs[NPCBox.SelectedItem.ToString()])
            //  dial.Nodes.Remove(node);

            PNode removedNode = getNodeOnDialogID(node);

            if (removedNode != null)
            {
                foreach (PNode path in edgeLayer.AllNodes)
                    if (((ArrayList)path.Tag) != null)
                        if (((ArrayList)path.Tag).Contains(removedNode))
                        {
                            edgeLayer.RemoveChild(path);
                        }
                graphs.Remove(removedNode);
            }
            if (DialogShower.Layer.AllNodes.Contains(removedNode))
            {
                DialogShower.Layer.RemoveChild(removedNode);
                haveBeenDeleted = true;
            }
            if (haveBeenDeleted)
                removePassiveNodeFromDialogGraphView();
            //System.Console.WriteLine("after delete node in graphs:" + graphs.Count);
        }

        void removePassiveNodeFromDialogGraphView()
        {
            DialogSelected(false);

            TreeNode treeNodes = treeDialogs.Nodes["Recycle"];
            foreach (TreeNode treeNode in treeNodes.Nodes)
                removeNodeFromDialogGraphView(int.Parse(treeNode.Text));
        }

        public void selectSubNodesDialogGraphView(int dialogID)
        {
            subNodes.Clear();

            if (dialogs.dialogs[currentNPC][dialogID].Nodes.Any())
                foreach (int sub in dialogs.dialogs[currentNPC][dialogID].Nodes)
                {
                    PNode node = getNodeOnDialogID(sub);
                    if (node != null)
                        subNodes.Add(node);
                }

            if (dialogs.dialogs[currentNPC][dialogID].Actions.ToDialog != 0)
            {
                PNode node = getNodeOnDialogID(dialogs.dialogs[currentNPC][dialogID].Actions.ToDialog);
                if (node != null)
                    subNodes.Add(node);
            }

            if (subNodes.Any())
                foreach (PNode subNode in subNodes)
                    subNode.Brush = Brushes.Yellow;
        }

        public void deselectSubNodesDialogGraphView()
        {
            foreach (PNode subNode in subNodes)
                if (!isRoot(getDialogIDOnNode(subNode)))
                    subNode.Brush = Brushes.White;
                else
                    subNode.Brush = Brushes.Green;
            subNodes = new List<PNode>();
        }
        //! Заменяет диалог с dialogID на dialog (используется в форме редактирования диалогов)
        public void replaceDialog(CDialog dialog, int dialogID)
        {
            dialogs.dialogs[currentNPC][dialogID] = dialog;
            dialogs.locales[settings.getListLocales()[0]][currentNPC][dialogID].InsertNonTextData(dialog);
        }
        //! Добавляет диалог в ветку (используется при добавлении диалога в форме EditDialogForm)
        public void addActiveDialog(int newID, CDialog dialog, int parentID)
        {
            // добавляем в русский словарь персонажей
            dialogs.dialogs[currentNPC].Add(newID, dialog);
            dialogs.dialogs[currentNPC][parentID].Nodes.Add(newID);
            // добавляем в английскую локаль
            CDialog newDialog = (CDialog) dialog.Clone();
            dialogs.locales[settings.getListLocales()[0]][currentNPC].Add(newID, newDialog);
            dialogs.locales[settings.getListLocales()[0]][currentNPC][parentID].Nodes.Add(newID);

            addNodeOnDialogGraphView(newID, parentID);
        }
        public void addPassiveDialog(int parentID, int dialogID)
        {
            this.dialogs.dialogs[currentNPC][parentID].Nodes.Add(dialogID);
            addNodeOnDialogGraphView(dialogID, parentID);
        }
        //-------------------------------------------------
        public void DialogSelected(bool withGraph)
        {
            CDialog root = new CDialog();
            DialogDict dialogs = getDialogDictionary(currentNPC);
            root = getRootDialog();
            root = getDialogOnIDConditional( root.DialogID );
            fillDialogTree(root, dialogs);
            if (withGraph)
            {
                graphs = new Dictionary<PNode, GraphProperties>();
                this.fillDialogGraphView(root);
            }
        }

        public void DrawRectangles()
        {
            Dictionary<int, CRectangle> rects = new Dictionary<int, CRectangle>();
            rects = RectManager.GetRectanglesForNpc(GetCurrentNPC());
            drawingLayer.RemoveAllChildren();
            foreach (CRectangle rect in rects.Values)
            {
                PPath newRect = PPath.CreateRectangle(rect.coordX, rect.coordY, rect.Width, rect.Height);
                newRect.Tag = RectManager.SetUniqueTag(rect.GetID());
                PText rectText = new PText(rect.GetText());
                rectText.Bounds = newRect.Bounds;
                rectText.Pickable = false;
                newRect.AddChild(rectText);
                drawingLayer.AddChild(newRect);
            }            
        }

        public void DeselectRectangles()
        {
            PNodeList rectList = drawingLayer.ChildrenReference;
            for (int i = 0; i < rectList.Count; i++)
                rectList[i].Brush = Brushes.White;
        }
    }
}
