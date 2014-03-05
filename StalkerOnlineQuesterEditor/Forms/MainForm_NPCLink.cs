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
    using NPCQuestDict = Dictionary<int, CDialog>;

    public partial class MainForm : Form
    {
        //! Верхнее или нижнее положение говнографа
        bool bNumOfIter;
        //! Координата X для текущего узла
        float rootx;
        //! Координата Y для текущего узла
        float rooty;
        //! Слой для ребер графа связей NPC
        PLayer edgeNPClinkLayer;
        //! Слой для узлов графа связей NPC
        PNodeList nodeNPClinkLayer;
        //! Словарь графов <Имя NPC, Узел на графе>
        Dictionary<string, PNode> mapGraphs = new Dictionary<string, PNode>();

        //! Нажатие на кнопку, выводит связи всех NPC с выбранным персонажем NPC
        private void bNpcLinkExecute_Click(object sender, EventArgs e)
        {
            if (NPCBox.SelectedIndex == -1)
                return;
            string npcName = NPCBox.SelectedItem.ToString();

            // очищаем поле графа
            bNumOfIter = true;
            rootx = (float)(this.ClientSize.Width / 5);
            rooty = (float)(this.ClientSize.Height / 5);
            this.npcLinkShower.Layer.RemoveAllChildren();
            mapGraphs.Clear();
            nodeNPClinkLayer = new PNodeList();
            edgeNPClinkLayer = new PLayer();
            // создаем уезл головного (выбранного) персонажа
            this.npcLinkShower.Root.AddChild(edgeNPClinkLayer);
            this.npcLinkShower.Camera.AddLayer(0, edgeNPClinkLayer);            
            PNode node = new PNode();
            addNodeToNpcLink(ref node, npcName);
            nodeNPClinkLayer.Add(node);

            // ищем все связи персонажа, т.е. квесты, которые он завершает от других NPC 
            foreach (CDialog dialog in dialogs.dialogs[npcName].Values)
            {
                List<int> list = dialog.Actions.CompleteQuests.ToList();
                list.AddRange(dialog.Actions.GetQuests.ToList());
                foreach (int questID in list)
                {
                    //! @todo это пиздец, делать проверку целостности
                    if (!quests.quest.ContainsKey(questID))
                        continue;
                    if (quests.quest[questID].Additional.Holder != npcName)
                    {
                        PNode new_node = new PNode();
                        string new_npc = quests.quest[questID].Additional.Holder;
                        if (mapGraphs.Keys.Contains(new_npc))
                            new_node = mapGraphs[new_npc];
                        else
                            addNodeToNpcLink(ref new_node, new_npc);

                        PrepareNodesForEdge(node, new_node, ref edgeNPClinkLayer);
                        if (!nodeNPClinkLayer.Contains(new_node))
                            nodeNPClinkLayer.Add(new_node);
                    }
                }            
            }
            npcLinkShower.Layer.AddChildren(nodeNPClinkLayer);
        }

        //! Заполняет граф связей по старой уёбищной схеме
        void fillNPCLinkView()
        {
            // Initialize, and create a layer for the edges (always underneath the nodes)
            this.npcLinkShower.Layer.RemoveAllChildren();
            nodeNPClinkLayer = new PNodeList();
            edgeNPClinkLayer = new PLayer();

            this.npcLinkShower.Root.AddChild(edgeNPClinkLayer);
            this.npcLinkShower.Camera.AddLayer(0, edgeNPClinkLayer);
            //////Image iBackground = Image.FromFile("source/map.jpg");
            //////this.npcLinkShower.BackgroundImage = iBackground;
            bNumOfIter = false;
            rootx = (float)(this.ClientSize.Width / 5);
            rooty = (float)(this.ClientSize.Height / 5);

            foreach (KeyValuePair<int, CQuest> quest in quests.quest)
                foreach (Dictionary<int, CDialog> dialog in dialogs.dialogs.Values)
                    foreach (KeyValuePair<int, CDialog> dial in dialog)
                        if (dial.Value.Actions.CompleteQuests.Contains(quest.Key) && !dial.Value.Holder.Equals(quest.Value.Additional.Holder)
                            && !dial.Value.Holder.Equals("") && !quest.Value.Additional.Holder.Equals(""))
                        {
                            string sQuestHolder = quest.Value.Additional.Holder;
                            string sDialogHolder = dial.Value.Holder;
                            PNode dialogHolder = new PNode();
                            PNode questHolder = new PNode();
                            
                            if (mapGraphs.Keys.Contains(sQuestHolder))
                                questHolder = mapGraphs[sQuestHolder];
                            else
                                addNodeToNpcLink(ref questHolder, sQuestHolder);

                            if (mapGraphs.Keys.Contains(sDialogHolder))
                                dialogHolder = mapGraphs[sDialogHolder];
                            else
                                addNodeToNpcLink(ref dialogHolder, sDialogHolder);

                            PrepareNodesForEdge(questHolder, dialogHolder, ref edgeNPClinkLayer);

                            if (!nodeNPClinkLayer.Contains(dialogHolder))
                                nodeNPClinkLayer.Add(dialogHolder);
                            if (!nodeNPClinkLayer.Contains(questHolder))
                                nodeNPClinkLayer.Add(questHolder);

                        }
            npcLinkShower.Layer.AddChildren(nodeNPClinkLayer);
        }

        //! Добавляет узел на граф связей NPC между собой
        void addNodeToNpcLink(ref PNode Holder, string name)
        {
            Holder = PPath.CreateRectangle(rootx, rooty, 180, 33);
            if (bNumOfIter)
            {
                rootx += 120.0f;
                rooty += 120.0f;
                bNumOfIter = false;
            }
            else
            {
                rootx += 120.0f;
                rooty -= 120.0f;
                bNumOfIter = true;
            }
            PText rootText = new PText(name);
            rootText.Pickable = false;
            rootText.X = Holder.X + 30;
            rootText.Y = Holder.Y + 5;
            Holder.AddChild(rootText);
            Holder.Tag = new ArrayList();
            mapGraphs.Add(name, Holder);
        }
    }
}
