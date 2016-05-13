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
    //! Класс, обрабатывающий перемещение камеры на поле Piccolo с помощью правой кнопки мыши. Pan event.
    public class PanEventHandler : PBasicInputEventHandler
    {
        public PanEventHandler()
        {
        }

        //! Условие, при котором событие будет срабатывать - нажата правая кнопка мыши.
        public override bool DoesAcceptEvent(PInputEventArgs e)
        {
            return e.IsMouseEvent && e.Button == MouseButtons.Right;
        }

        //! Основная функция. При перемещении мыши с нажатой правой кнопкой смещаем всю сцену на дельту мыши.
        public override void OnMouseDrag(object sender, PInputEventArgs e)
        {
            e.TopCamera.OffsetBy(e.Delta.Width, e.Delta.Height);
            //e.Camera.OffsetBy(e.Delta.Width, e.Delta.Height);

            // works perfect, centrelize breaks
            //e.Camera.TranslateBy(e.Delta.Width, e.Delta.Height);
            //e.TopCamera.TranslateBy(e.Delta.Width, e.Delta.Height);

            // nothing happens
            //e.Camera.TranslateViewBy(e.Delta.Width, e.Delta.Height);
            //e.TopCamera.TranslateViewBy(e.Delta.Width, e.Delta.Height);
            //e.Camera.TranslateViewBy(e.CanvasDelta.Width, e.CanvasDelta.Height);
            
            // nothing works here
            //e.TopCamera.InvalidateFullBounds();
            //e.TopCamera.InvalidateLayout();
            //e.TopCamera.InvalidatePaint();
            //e.Camera.InvalidateFullBounds();
            //e.Camera.InvalidateLayout();
            //e.TopCamera.ApplyViewConstraints();
        }

        //! По двойному клику правой кнопки возвращаемся в центр канваса.
        public override void OnDoubleClick(object sender, PInputEventArgs e)
        {
            e.TopCamera.SetOffset(0, 0);
        }
    }
}
