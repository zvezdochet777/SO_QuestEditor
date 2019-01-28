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
    //! Класс, отвечающий за Drag нодов диалогов и прямоугольников по канвасу Piccollo. Также обрабатывает левый клик и выделение нодов
    public class NodeDragHandler : PDragEventHandler
    {
        public PNode SelectedNode = null;

        private MainForm mainForm;       

        public NodeDragHandler(MainForm form)
        {
            this.mainForm = form;
        }

        //! Условие, при котором событие будет срабатывать - только левый клик, никаких зажатых кнопок на клавиатуре
        public override bool DoesAcceptEvent(PInputEventArgs e)
        {
            return e.IsMouseEvent && e.Modifiers == Keys.None && e.Button == MouseButtons.Left;
        }

        //! Начало перетаскивания узлов диалога
        protected override void OnStartDrag(object sender, PInputEventArgs e)
        {
            base.OnStartDrag(sender, e);
            e.Handled = true;
            
            if (e.PickedNode.Tag != null)
                e.PickedNode.MoveToFront();
        }
        //! Закончили перетаскивать узел диалога или прямоугольник. Сохраняем их координаты
        protected override void OnEndDrag(object sender, PInputEventArgs e)
        {
            base.OnEndDrag(sender, e);
            float x = e.PickedNode.FullBounds.X;
            float y = e.PickedNode.FullBounds.Y;
            float w = e.PickedNode.Bounds.Width;
            float h = e.PickedNode.Bounds.Height;
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
                mainForm.RectManager.ChangeCoordinates(mainForm.GetCurrentNPC(), rectId, x, y);
        }

        //! Клик мыши по узлу диалога или прямоугольнику - выделяем диалог и его потомков цветом, либо выделяем прямугольник
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

        //! Вызываем форму редактирования для диалога или прямоугольника
        public override void OnDoubleClick(object sender, PInputEventArgs e)
        {
            e.Handled = true;
            int node = mainForm.getDialogIDOnNode(e.PickedNode);
            mainForm.bEditDialog_Click(sender, new EventArgs() );
        }

        //! При перетаскивании перерисовываем связи диалога. К прямоугольникам не относится
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

        //! Возвращает текущий ID диалога по выделенной ноде curNode
        public int getCurDialogID()
        {
            if (SelectedNode != null)
                return mainForm.getDialogIDOnNode(SelectedNode);
            else
                return 0;
        }

        //! Пользователь выделил конкретный узел - красим его в красный, потомков - в желтый
        public void SelectCurrentNode(int dialogID)
        {
            //! Выделяем цветом новый узел и его потомков, включаем кнопки редактирования
            if (!getCurDialogID().Equals(dialogID))
            {
                PNode previousNode = (SelectedNode != null) ? (SelectedNode) : (new PNode());
                mainForm.onDeselectNode();
                mainForm.deselectSubNodesDialogGraphView();
                
                if (dialogID == 0)
                    SelectedNode = null;
                else
                    SelectedNode = mainForm.getNodeOnDialogID(dialogID);
                if (SelectedNode != null)
                {
                    SelectedNode.Brush = mainForm.GetBrushForNode(SelectedNode);
                    mainForm.selectNodeOnDialogTree(dialogID);
                    mainForm.selectSubNodesDialogGraphView(dialogID);
                }
                mainForm.selectedItemType = SelectedItemType.dialog;
                mainForm.onSelectNode(dialogID);

                //! Меняем цвет у предыдущего выделенного узла на стандартный - белый или зеленый (у root)
                if (previousNode != null)
                    previousNode.Brush = mainForm.GetBrushForNode(previousNode);
            }

            if (mainForm.isDialogActive(dialogID))
                mainForm.startEmulator(dialogID);//, true);            
        }

        //! Пользователь выделил прямугольников кликом мыши - красим его в спец цвет, задаем его ID как текущий
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
