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
    //! Класс, обрабатывающий зуминг с помощью колесика мыши
    class ZoomEventHandler : PBasicInputEventHandler
    {
        public ZoomEventHandler()
        {
        }

        //! Условие, при котором срабатывает событие - дельта колесика мыши должна быть не равной нулю
        public override bool DoesAcceptEvent(PInputEventArgs e)
        {
            if (e.IsMouseEvent)
                if (e.Modifiers == Keys.None)
                    if (e.Button == MouseButtons.Middle || e.WheelDelta != 0)
                        //if (e.WheelDelta != 0)
                            return true;
            //return e.Button == MouseButtons.Middle;
            //return base.DoesAcceptEvent(e);
            return false;
        }

        protected override bool PBasicInputEventHandlerAcceptsEvent(PInputEventArgs e)
        {
            return false;
            //return base.PBasicInputEventHandlerAcceptsEvent(e);
        }

        //! Обработка перемещения колесика мыши из WheelDelta в значение зума
        public override void OnMouseWheel(object sender, PInputEventArgs e)
        {
            //base.OnMouseWheel(sender, e);
            float delta = e.WheelDelta / 120;
            float normal = 1.0F;
            float step = 0.1F;
            float scale = normal + delta * step;

            e.TopCamera.ScaleBy(scale, e.Position.X, e.Position.Y);
            // WheelDelta == 120 and -120
            // Normal scale is 1.0F
            // Scaling should be between 0.3 and 3.0
            //MessageBox.Show("WheelDelta: " + e.WheelDelta.ToString());
        }
        
        //! Отладочная функция по клику на колесико
        public override void OnMouseDown(object sender, PInputEventArgs e)
        {
            //base.OnMouseDown(sender, e);
            // scaling by negative number rotates the whole canvas by 180 degreees
            e.TopCamera.ScaleBy(0.8F, e.Position.X, e.Position.Y);
            float scale = e.TopCamera.Scale;
            float gscale = e.TopCamera.GlobalScale;
            float vscale = e.TopCamera.ViewScale;
            MessageBox.Show("Scale data: " + scale.ToString() + " " + gscale.ToString()+ " " +vscale.ToString());
            
            //mainForm.setXYCoordinates(0, scale, gscale, vscale);
        }
    }
}
