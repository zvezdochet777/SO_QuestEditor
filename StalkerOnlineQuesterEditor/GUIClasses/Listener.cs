using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMD.HCIL.Piccolo;
using UMD.HCIL.Piccolo.Nodes;
using UMD.HCIL.Piccolo.Util;
using UMD.HCIL.Piccolo.Event;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{

    public class NodeDragHandler : PDragEventHandler
    {
        public PNode curNode = null;
        public PNode prevNode = null;

        private MainForm mainForm;       

        public NodeDragHandler(MainForm form)
        {
            this.mainForm = form;
        }

        public override bool DoesAcceptEvent(PInputEventArgs e)
        {
            //bool temp = e.IsMouseEvent;
            //bool t2 = e.IsMouseEnterOrMouseLeave;
            return e.IsMouseEvent && e.Modifiers == Keys.None && e.Button == MouseButtons.Left;
        }

        public override void OnMouseEnter(object sender, PInputEventArgs e)
        {
            base.OnMouseEnter(sender, e);
            //if ((e.Button == MouseButtons.None) && (e.PickedNode.Tag!= null))
            //    e.PickedNode.Brush = Brushes.Red;

        }

        public override void OnMouseLeave(object sender, PInputEventArgs e)
        {
            base.OnMouseLeave(sender, e);
            //if (e.Button == MouseButtons.None)
            //    e.PickedNode.Brush = Brushes.White;

        }
        //! Начало перетаскивания узлов диалога
        protected override void OnStartDrag(object sender, PInputEventArgs e)
        {
            base.OnStartDrag(sender, e);
            e.Handled = true;
            
            if (e.PickedNode.Tag != null)
                e.PickedNode.MoveToFront();
        }
        //! Закончили перетаскивать узел диалога
        protected override void OnEndDrag(object sender, PInputEventArgs e)
        {
            base.OnEndDrag(sender, e);
            float x = e.PickedNode.GlobalFullBounds.X;
            float y = e.PickedNode.GlobalFullBounds.Y;
            float w = e.PickedNode.GlobalFullBounds.Width;
            float h = e.PickedNode.GlobalFullBounds.Height;
            string str = sender.ToString();

            int dialogID = mainForm.getDialogIDOnNode(e.PickedNode);
            CDialog dialog = mainForm.getDialogOnIDConditional(dialogID);
            if (dialog != null)
            {
                mainForm.SaveCoordinates(dialog, e.PickedNode, dialog.coordinates.RootDialog);
                mainForm.setXYCoordinates(x, y, w, h);                
            }
        }

        //! Клик мыши по узлу диалога - выделяем его и потомков цветом
        public override void OnClick(object sender, PInputEventArgs e)
        {
            mainForm.clearToolstripLabel();
            e.Handled = true;
            if (e.PickedNode.Tag != null)
            {
                setCurrentNode(mainForm.getDialogIDOnNode(e.PickedNode));
            }
        }

        public override void OnDoubleClick(object sender, PInputEventArgs e)
        {
            e.Handled = true;
            int node = mainForm.getDialogIDOnNode(e.PickedNode);
            mainForm.bEditDialog_Click(sender, new EventArgs() );
        }

        /*
        public override void OnMouseMove(object sender, PInputEventArgs e)
        {
            
            mainForm.setXYCoordinates(StartingPoint.X, StartingPoint.Y, e.Position.X, e.Position.Y);
            base.OnMouseMove(sender, e);
            if (e.Modifiers == Keys.Shift)     // e.Button == MouseButtons.Left &&
            {
                float width = e.Position.X - (float)StartingPoint.X;
                float height = e.Position.Y - (float)StartingPoint.Y;
                PNode newNode = PPath.CreateRectangle((float)StartingPoint.X, (float)StartingPoint.Y, width, height);
                //mainForm.nodeLayer.Add(newNode);
                mainForm.drawingLayer.AddChild(newNode);
                mainForm.setXYCoordinates(StartingPoint.X, StartingPoint.Y, width, height);
            }
        }
         * */
        /*
        public override void OnMouseUp(object sender, PInputEventArgs e)
        {
            base.OnMouseUp(sender, e);
            if (e.Modifiers == Keys.Shift)     // e.Button == MouseButtons.Left &&
            {
                NodeCoordinates newPos = new NodeCoordinates((int)e.Position.X, (int)e.Position.Y, false, false);
                float width = Math.Abs(newPos.X - (float)StartingPoint.X);
                float height = Math.Abs(newPos.Y - (float)StartingPoint.Y);

                StartingPoint.X = (newPos.X < StartingPoint.X) ? (newPos.X) : (StartingPoint.X);
                StartingPoint.Y = (newPos.Y < StartingPoint.Y) ? (newPos.Y) : (StartingPoint.Y);

                PNode newNode = PPath.CreateRectangle((float)StartingPoint.X, (float)StartingPoint.Y, width, height);
                //mainForm.nodeLayer.Add(newNode);
                mainForm.drawingLayer.AddChild(newNode);
                newNode.MoveToBack();
                mainForm.setXYCoordinates(StartingPoint.X, StartingPoint.Y, width, height);
            }
        }*/

        protected override void OnDrag(object sender, PInputEventArgs e)
        {
            base.OnDrag(sender, e);
            if (e.PickedNode.Tag != null)
            {
                ArrayList edges = (ArrayList)e.PickedNode.Tag;
                foreach (Object edge in edges)
//                    if (edge.GetType().ToString().Equals("System.String"))
//                        System.Console.WriteLine("hello");
                   /* else*/ if (edge.GetType().ToString().Equals("UMD.HCIL.Piccolo.Nodes.PPath"))
                        MainForm.updateEdge((PPath)edge);
            }
        }

        public int getCurDialogID()
        {
            if (curNode != null)
                return mainForm.getDialogIDOnNode(curNode);
            else
                return 0;
           /* {
                foreach (PNode node in curNode.AllNodes)
                    if (node.GetType().ToString().Equals("UMD.HCIL.Piccolo.Nodes.PText"))
                        return int.Parse(((PText)node).Text);
            }
            return 0;*/
        }

        //! Пользователь выделил конкретный узел - красим его в красный, потомков - в желтый
        public void setCurrentNode(int dialogID)
        {
            if (curNode != null)
                if (!getCurDialogID().Equals(dialogID))
                {
                    if (mainForm.isRoot(mainForm.getDialogIDOnNode(curNode)))
                        curNode.Brush = Brushes.Green;
                    else
                        curNode.Brush = Brushes.White;
                    prevNode = curNode;
                }
            if (!getCurDialogID().Equals(dialogID))
            {
                mainForm.onDeselectNode();
                mainForm.deselectSubNodesDialogGraphView();
                
                if (dialogID == 0)
                    curNode = null;
                else
                    curNode = mainForm.getNodeOnDialogID(dialogID);
                if (curNode != null)
                {
                    curNode.Brush = Brushes.Red;
                    mainForm.selectNodeOnDialogTree(dialogID);
                    
                    mainForm.selectSubNodesDialogGraphView(dialogID);
                }
                mainForm.onSelectNode(dialogID);
            }

            if (mainForm.isDialogActive(dialogID))
                mainForm.startEmulator(dialogID);//, true);            
        }

   }

    public class PanHandler : PPanEventHandler
    {
        protected override void Pan(PInputEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                base.Pan(e);
            
        }
    }

    public class ZoomHandler : PZoomEventHandler
    {
        public override void OnMouseWheel(object sender, PInputEventArgs e)
        {
            base.OnMouseWheel(sender, e);
        }

        public override void OnMouseDown(object sender, PInputEventArgs e)
        {
            if (e.Button == MouseButtons.Right)                
                base.OnMouseDown(sender, e);
        }
    }

}
