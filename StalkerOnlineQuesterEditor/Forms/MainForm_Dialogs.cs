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
    using NPCQuestDict = Dictionary<int, CDialog>;
    //! Словарь <уровень в иерархии, список узлов>
    using levelDict = Dictionary<int, List<int>>;
    public partial class MainForm : Form
    {
        public levelDict nodesOnLevel;
        //DIALOGS
        //******************SETTERS/GETTERS****************************
        //! Возвращает вершину графа диалогов - корневую фразу у заданного NPC
        public CDialog getRootDialog(string npc_name)
        {
            NPCQuestDict dialogs = this.dialogs.dialogs[npc_name];
            foreach (CDialog dialog in dialogs.Values)
            {
                //System.Console.WriteLine(dialog.QuestDialog.ToString() + " vs " + currentQuestDialog.ToString());
                if (dialog.QuestDialog == currentQuestDialog)
                {
                    rootElements.Add(dialog.DialogID);
                    return dialog;

                }
            }
            return null;
        }

        //! Возвращает вершину графа диалогов - корневую фразу
        CDialog getRootDialog()
        {            
            CDialog ret_dial = getRootDialog(currentNPC);
            if (ret_dial==null)
                System.Console.WriteLine("MainForm::getRootDialog null");
            else
                System.Console.WriteLine("MainForm::getRootDialog " + ret_dial.DialogID.ToString());

            return ret_dial;
            //NPCQuestDict dialogs = this.dialogs.dialogs[currentNPC];
            //foreach (CDialog dialog in dialogs.Values)
            //{
            //    //System.Console.WriteLine(dialog.QuestDialog.ToString() + " vs " + currentQuestDialog.ToString());
            //    if (dialog.QuestDialog == currentQuestDialog)
            //    {
            //        rootElements.Add(dialog.DialogID);
            //        return dialog;

            //    }
            //}
            //return null;
        }

        //! Возвращает Узел по известному ID диалога
        public PNode getNodeOnDialogID(int dialogID)
        {
            return GraphProperties.findNodeOnID(graphs, dialogID);
        }

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
                    CDialog d = dialogs.getLocaleDialog(dialogID, settings.getCurrentLocale(), currentNPC);
                    if (d != null)
                        return dialogs.getLocaleDialog(dialogID, settings.getCurrentLocale(), currentNPC);
                    else
                    {
                        d = dialogs.dialogs[currentNPC][dialogID];
                        d.version = 0;
                        return d;
                    }
                }
            }
            else
                return null;
        }

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

        public void replaceDialog(CDialog dialog, int dialogID)
        {
            dialogs.dialogs[currentNPC][dialogID] = dialog;
        }
        //**********************WORK WITH FORM

        void fillDialogTree(CDialog root, NPCQuestDict dialogs)
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
                    treeRecycleNode.Nodes.Add(dialog.DialogID.ToString(), dialog.DialogID.ToString());

            this.treeDialogs.ExpandAll();
        }

        //! Заполняет граф диалога нужными узлами
        void fillDialogGraphView(CDialog root)
        {
            // Initialize, and create a layer for the edges (always underneath the nodes)
            this.DialogShower.Layer.RemoveAllChildren();
            nodeLayer = new PNodeList();
            edgeLayer = new PLayer();

            this.DialogShower.Root.AddChild(edgeLayer);
            this.DialogShower.Camera.AddLayer(0, edgeLayer);

            // Show root node
            float rootx = (float)(this.ClientSize.Width / 5);
            float rooty = (float)(this.ClientSize.Height / 5);
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
            //SaveCoordinates(root, rootNode);
            this.fillDialogSubgraphView(root, rootNode, 1, ref edgeLayer, ref nodeLayer, false);
            this.DialogShower.Layer.AddChildren(nodeLayer);
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
                    float x = dialogs.dialogs[currentNPC][subdialogs].coordinates.X;
                    float y = dialogs.dialogs[currentNPC][subdialogs].coordinates.Y;

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
                    //SaveCoordinates( dialogs.dialogs[currentNPC][subdialogs], node);
                    
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

        void addNodeOnDialogGraphView(int dialogID, int parentDialogID)
        {
            PNode parentDialog = getNodeOnDialogID(parentDialogID);

            float x = new float();
            x = parentDialog.X + 10;
            float y = new float();
            y = parentDialog.Y + 10;

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
        public void SaveCoordinates(CDialog dialog, PNode node)
        {
            dialog.coordinates.X = (int) node.FullBounds.X;
            dialog.coordinates.Y = (int) node.FullBounds.Y;
            dialog.coordinates.RootDialog = false;
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

        public void addActiveDialog(int newID, CDialog dialog, int parentID)
        {
            this.dialogs.dialogs[currentNPC].Add(newID, dialog);
            //this.NPCDialogs[currentNPC].Add(dialog);
            dialogs.dialogs[currentNPC][parentID].Nodes.Add(newID);
            addNodeOnDialogGraphView(newID, parentID);
        }
        public void addPassiveDialog(int parentID, int dialogID)
        {
            this.dialogs.dialogs[currentNPC][parentID].Nodes.Add(dialogID);
            addNodeOnDialogGraphView(dialogID, parentID);
        }
        //-------------------------------------------------
        void DialogSelected(bool withGraph)
        {
            bAddDialog.Enabled = true;
            CDialog root = new CDialog();
            NPCQuestDict dialogs = this.dialogs.dialogs[currentNPC];
            root = getRootDialog();
            fillDialogTree(root, dialogs);
            if (withGraph)
            {
                graphs = new Dictionary<PNode, GraphProperties>();
                this.fillDialogGraphView(root);
            }
        }
        
    }
}
