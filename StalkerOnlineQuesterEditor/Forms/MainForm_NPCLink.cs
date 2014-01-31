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
            bool bNumOfIter = false;
            float rootx = (float)(this.ClientSize.Width / 5);
            float rooty = (float)(this.ClientSize.Height / 5);

            foreach (KeyValuePair<int, CQuest> quest in quests.quest)
                foreach (Dictionary<int, CDialog> dialog in dialogs.dialogs.Values)
                    foreach (KeyValuePair<int, CDialog> dial in dialog)
                        if (dial.Value.Actions.CompleteQuests.Contains(quest.Key) && !dial.Value.Holder.Equals(quest.Value.Additional.Holder)
                            && !dial.Value.Holder.Equals("") && !quest.Value.Additional.Holder.Equals(""))
                        {
                            //System.Console.WriteLine("------");
                            //System.Console.WriteLine(dial.Value.Holder);
                            //System.Console.WriteLine(quest.Value.QuestInformation.NameOfHolder);
                            //System.Console.WriteLine("------");

                            string sQuestHolder = quest.Value.Additional.Holder;
                            string sDialogHolder = dial.Value.Holder;

                            PNode dialogHolder;
                            PNode questHolder;

                            if (mapGraphs.Keys.Contains(sQuestHolder))
                            {
                                questHolder = mapGraphs[sQuestHolder];
                            }
                            else
                            {
                                questHolder = PPath.CreateRectangle(rootx, rooty, 180, 33);
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
                                PText rootText = new PText(sQuestHolder);
                                rootText.Pickable = false;
                                rootText.X = questHolder.X;
                                rootText.Y = questHolder.Y;
                                questHolder.AddChild(rootText);
                                questHolder.Tag = new ArrayList();
                                mapGraphs.Add(sQuestHolder, questHolder);
                            }

                            if (mapGraphs.Keys.Contains(sDialogHolder))
                            {
                                dialogHolder = mapGraphs[sDialogHolder];
                            }
                            else
                            {
                                dialogHolder = PPath.CreateRectangle(rootx, rooty, 180, 33);
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
                                PText rootText = new PText(sDialogHolder);
                                rootText.Pickable = false;
                                rootText.X = dialogHolder.X;
                                rootText.Y = dialogHolder.Y;
                                dialogHolder.AddChild(rootText);
                                dialogHolder.Tag = new ArrayList();
                                mapGraphs.Add(sDialogHolder, dialogHolder);
                            }

                            PPath edge = new PPath();
                            edge.Pickable = false;
                            ((ArrayList)dialogHolder.Tag).Add(edge);
                            ((ArrayList)questHolder.Tag).Add(edge);
                            edge.Tag = new ArrayList();
                            ((ArrayList)edge.Tag).Add(dialogHolder);
                            ((ArrayList)edge.Tag).Add(questHolder);
                            edgeNPClinkLayer.AddChild(edge);
                            updateEdge(edge);
                            if (!nodeNPClinkLayer.Contains(dialogHolder))
                                nodeNPClinkLayer.Add(dialogHolder);
                            if (!nodeNPClinkLayer.Contains(questHolder))
                                nodeNPClinkLayer.Add(questHolder);

                        }
            npcLinkShower.Layer.AddChildren(nodeNPClinkLayer);
        }
    }
}
