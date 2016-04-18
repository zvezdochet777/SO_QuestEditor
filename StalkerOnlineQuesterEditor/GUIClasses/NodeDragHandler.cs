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
    //! Класс, отвечающий за Drag нодов диалогов по канвасу Piccollo. Также обрабатывает левый клик и выделение нодов
    public class NodeDragHandler : PDragEventHandler
    {
        public PNode curNode = null;

        private MainForm mainForm;       

        public NodeDragHandler(MainForm form)
        {
            this.mainForm = form;
        }

        public override bool DoesAcceptEvent(PInputEventArgs e)
        {
            return e.IsMouseEvent && e.Modifiers == Keys.None && e.Button == MouseButtons.Left;
        }

        public override void OnMouseEnter(object sender, PInputEventArgs e)
        {
            base.OnMouseEnter(sender, e);
            if ((e.Button == MouseButtons.None) && (e.PickedNode.Tag!= null))
                e.PickedNode.Brush = Brushes.Red;
        }

        public override void OnMouseLeave(object sender, PInputEventArgs e)
        {
            base.OnMouseLeave(sender, e);
            if (e.Button == MouseButtons.None)
                e.PickedNode.Brush = Brushes.White;

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

            int rectId;
            if (mainForm.RectManager.CheckIfRect(e.PickedNode.Tag, out rectId))
                mainForm.RectManager.ChangeCoordinates(mainForm.GetCurrentNPC(), rectId, (int) x, (int) y);
        }

        //! Клик мыши по узлу диалога - выделяем его и потомков цветом
        public override void OnClick(object sender, PInputEventArgs e)
        {
            mainForm.clearToolstripLabel();
            e.Handled = true;
            int rectId;
            if (mainForm.RectManager.CheckIfRect(e.PickedNode.Tag, out rectId))
                SelectRectangle(e.PickedNode, rectId);
            else if (e.PickedNode.Tag != null)
                SelectCurrentNode(mainForm.getDialogIDOnNode(e.PickedNode));
        }

        public override void OnDoubleClick(object sender, PInputEventArgs e)
        {
            e.Handled = true;
            int node = mainForm.getDialogIDOnNode(e.PickedNode);
            mainForm.bEditDialog_Click(sender, new EventArgs() );
        }

        protected override void OnDrag(object sender, PInputEventArgs e)
        {
            base.OnDrag(sender, e);
            int temp;
            if (e.PickedNode.Tag != null && !mainForm.RectManager.CheckIfRect(e.PickedNode.Tag, out temp))
            {
                ArrayList edges = (ArrayList)e.PickedNode.Tag;
                foreach (Object edge in edges)
                    if (edge.GetType().ToString().Equals("UMD.HCIL.Piccolo.Nodes.PPath"))
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
        public void SelectCurrentNode(int dialogID)
        {
            //! Меняем цвет у предыдущего выделенного узла на стандартный - белый или зеленый (у root)
            if (curNode != null)
                if (!getCurDialogID().Equals(dialogID))
                {
                    if (mainForm.isRoot(mainForm.getDialogIDOnNode(curNode)))
                        curNode.Brush = Brushes.Green;
                    else
                        curNode.Brush = Brushes.White;
                }
            //! Выделяем цветом новый узел и его потомков, включаем кнопки редактирования
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
                mainForm.selectedItemType = SelectedItemType.dialog;
                mainForm.onSelectNode(dialogID);
            }

            if (mainForm.isDialogActive(dialogID))
                mainForm.startEmulator(dialogID);//, true);            
        }

        public void SelectRectangle(PNode rectangle, int rectID)
        {
            mainForm.DeselectRectangles();
            rectangle.Brush = Brushes.Azure;
            mainForm.RectManager.SetSelectedRectangleID(rectID);
            mainForm.selectedItemType = SelectedItemType.rectangle;
            mainForm.onSelectNode(-1);
        }

   }

}
