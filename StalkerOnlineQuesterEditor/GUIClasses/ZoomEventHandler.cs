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
                            return true;
            return false;
        }

        //! Обработка перемещения колесика мыши из WheelDelta в значение зума
        public override void OnMouseWheel(object sender, PInputEventArgs e)
        {
            float delta = e.WheelDelta / 120;
            float normal = 1.0F;
            float step = 0.1F;
            float scale = normal + delta * step;

            e.TopCamera.ScaleBy(scale, e.Position.X, e.Position.Y);
            // WheelDelta == 120 and -120
            // Normal scale is 1.0F
            // Scaling should be between 0.3 and 3.0
            // scaling by negative number rotates the whole canvas by 180 degreees
            //MessageBox.Show("WheelDelta: " + e.WheelDelta.ToString());
        }

        public override void OnClick(object sender, PInputEventArgs e)
        {
            e.TopCamera.Scale = 1.0F;
        }
        
    }
}
