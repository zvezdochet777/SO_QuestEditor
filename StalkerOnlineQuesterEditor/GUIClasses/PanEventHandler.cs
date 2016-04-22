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
        private MainForm mainForm;

        public PanEventHandler(MainForm main)
        {
            mainForm = main;
        }

        //! Условие, при котором событие будет срабатывать - нажата правая кнопка мыши.
        public override bool DoesAcceptEvent(PInputEventArgs e)
        {
            return e.IsMouseEvent && e.Button == MouseButtons.Right;
            //return base.DoesAcceptEvent(e);
        }

        //! Отладочная функция. По одиночному клику возвращаем текущие координаты
        public override void OnMouseDown(object sender, PInputEventArgs e)
        {
            //base.OnMouseDown(sender, e);
            //mainForm.setXYCoordinates(e.Position.X, e.CanvasPosition.X, e.Position.Y, e.CanvasPosition.Y);

            PointF local = e.Position;
            
            PointF global = e.Camera.LocalToGlobal(local);
            PointF parent = e.Camera.LocalToParent(local);
            PointF view = e.Camera.LocalToView(local);            

            // local and global differs. Global looks to have zero and the current pan position, local - some centralized one.
            //mainForm.setXYCoordinates(local.X, local.Y, global.X, global.Y);

            // parent looks like the global stuff
            //mainForm.setXYCoordinates(local.X, local.Y, parent.X, parent.Y);

            // view is the same as local
            //mainForm.setXYCoordinates(local.X, local.Y, view.X, view.Y);

            // all the same shit for TopCamera and Camera. They works in the same way 100%.

            mainForm.setXYCoordinates(local.X, local.Y, global.X, global.Y);

            /*
            RectangleF bounds = e.Camera.GlobalBounds;
            RectangleF fullb = e.Camera.GlobalFullBounds;
            float rotat = e.Camera.GlobalRotation;
            float scale = e.Camera.GlobalScale;
            PointF trans = e.Camera.GlobalTranslation;
            */
        }

        //! Основная функция. При перемещении мыши с нажатой правой кнопкой смещаем всю сцену на дельту мыши.
        public override void OnMouseDrag(object sender, PInputEventArgs e)
        {
            // works perfect, centrelize breaks
            //e.Camera.TranslateBy(e.Delta.Width, e.Delta.Height);
            //e.TopCamera.TranslateBy(e.Delta.Width, e.Delta.Height);

            // nothing happens
            //e.Camera.TranslateViewBy(e.Delta.Width, e.Delta.Height);
            //e.TopCamera.TranslateViewBy(e.Delta.Width, e.Delta.Height);
            //e.Camera.TranslateViewBy(e.CanvasDelta.Width, e.CanvasDelta.Height);

            // works perfect, centrelize breaks
            //e.Camera.OffsetBy(e.Delta.Width, e.Delta.Height);
            e.TopCamera.OffsetBy(e.Delta.Width, e.Delta.Height);
            
            // nothing works here
            /*
            e.TopCamera.InvalidateFullBounds();
            e.TopCamera.InvalidateLayout();
            e.TopCamera.InvalidatePaint();
            e.Camera.InvalidateFullBounds();
            e.Camera.InvalidateLayout();
            e.TopCamera.ApplyViewConstraints();
             */ 
        }

        //! По двойному клику правой кнопки возвращаемся в центр канваса.
        public override void OnDoubleClick(object sender, PInputEventArgs e)
        {
            e.TopCamera.SetOffset(0, 0);
        }
    }
}
