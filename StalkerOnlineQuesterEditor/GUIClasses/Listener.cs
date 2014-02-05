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
        public MainForm form;
        

        public NodeDragHandler(MainForm form)
        {
            this.form = form;
        }

        public override bool DoesAcceptEvent(PInputEventArgs e)
        {
            return e.IsMouseEvent && (e.Button != MouseButtons.None || e.IsMouseEnterOrMouseLeave);
        }

        public override void OnMouseEnter(object sender, PInputEventArgs e)
        {
            base.OnMouseEnter(sender, e);
            //if ((e.Button == MouseButtons.None) && (e.PickedNode.Tag!= null))
            //e.PickedNode.Brush = Brushes.Red;

        }

        public override void OnMouseLeave(object sender, PInputEventArgs e)
        {
            base.OnMouseLeave(sender, e);
            // if (e.Button == MouseButtons.None)
            //e.PickedNode.Brush = Brushes.White;

        }

        protected override void OnStartDrag(object sender, PInputEventArgs e)
        {
            base.OnStartDrag(sender, e);
            e.Handled = true;
            // e.CanvasPosition.X, e.CanvasPosition.Y
            float x = e.PickedNode.GlobalFullBounds.X;
            float y = e.PickedNode.GlobalFullBounds.Y;
            float w = e.PickedNode.GlobalFullBounds.Width;
            float h = e.PickedNode.GlobalFullBounds.Height;

            form.setXYCoordinates(x, y, w ,h);
            
            if (e.PickedNode.Tag != null)
                e.PickedNode.MoveToFront();
        }
        public override void OnClick(object sender, PInputEventArgs e)
        {
            //System.Console.WriteLine("i have "+form.graphs.Count+" graphs");
            form.clearToolstripLabel();
            e.Handled = true;
            if (e.PickedNode.Tag != null)
            {
                setCurrentNode(form.getDialogIDOnNode(e.PickedNode));
            }


        }


        string getStringFromnode(PText node){
            return "";
        }

        public override void OnDoubleClick(object sender, PInputEventArgs e)
        {
            e.Handled = true;
            //System.Console.WriteLine("DoubleClick");
        }

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
                return form.getDialogIDOnNode(curNode);
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
                    if (form.isRoot(form.getDialogIDOnNode(curNode)))
                        curNode.Brush = Brushes.Green;
                    else
                        curNode.Brush = Brushes.White;
                    prevNode = curNode;
                }
            if (!getCurDialogID().Equals(dialogID))
            {
                form.onDeselectNode();
                form.deselectSubNodesDialogGraphView();
                
                if (dialogID == 0)
                    curNode = null;
                else
                    curNode = form.getNodeOnDialogID(dialogID);
                if (curNode != null)
                {
                    curNode.Brush = Brushes.Red;
                    form.selectNodeOnDialogTree(dialogID);
                    
                    form.selectSubNodesDialogGraphView(dialogID);
                }
                form.onSelectNode(dialogID);
            }

            if (form.isDialogActive(dialogID))
                form.startEmulator(dialogID);//, true);
        }

   }

}
