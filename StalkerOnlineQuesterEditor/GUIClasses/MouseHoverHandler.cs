using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using UMD.HCIL.Piccolo;
using UMD.HCIL.Piccolo.Nodes;
using UMD.HCIL.Piccolo.Util;
using UMD.HCIL.Piccolo.Event;

namespace StalkerOnlineQuesterEditor
{
    //! Класс, обрабатывающий наведение мыши на узлы диалога. В этом случае он показывает подсказку с действиями узла диалога
    class MouseHoverHandler : PBasicInputEventHandler
    {
        private MainForm mainForm;
        private RectangleManager RectManager;

        public MouseHoverHandler(MainForm mainform, RectangleManager rectManager)
        {
            mainForm = mainform;
            RectManager = rectManager;
        }

        //! Условие, при котором событие срабатывает - клавиши мыши не нажаты, мышь наведена на узел диалога
        public override bool DoesAcceptEvent(PInputEventArgs e)
        {
            return e.IsMouseEvent && e.Button == MouseButtons.None && e.IsMouseEnterOrMouseLeave;
            // && e.Modifiers == Keys.None
        }

        //! Навели мышкой на узел диалога - показываем подсказку
        public override void OnMouseEnter(object sender, PInputEventArgs e)
        {
            base.OnMouseEnter(sender, e);
            int temp;
            if (e.PickedNode.Tag != null && !RectManager.CheckIfRect(e.PickedNode.Tag, out temp))
            {
                e.PickedNode.Brush = Brushes.Aquamarine;
                mainForm.ShowDialogTooltip(e.PickedNode);
            }
        }

        //! Убрали мышку с диалога - убрали подсказку
        public override void OnMouseLeave(object sender, PInputEventArgs e)
        {
            base.OnMouseLeave(sender, e);
            int temp;
            if (e.PickedNode.Tag != null && !RectManager.CheckIfRect(e.PickedNode.Tag, out temp))
            {
                if (mainForm.isRoot(mainForm.getDialogIDOnNode(e.PickedNode)))
                    e.PickedNode.Brush = Brushes.Green;
                else
                    e.PickedNode.Brush = Brushes.White;
                mainForm.ResetDialogsTooltip();
            }
        }
    }
}
