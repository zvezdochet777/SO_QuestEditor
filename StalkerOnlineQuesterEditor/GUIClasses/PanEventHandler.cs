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
    public class PanEventHandler : PBasicInputEventHandler
    {
        private MainForm mainForm;

        public PanEventHandler(MainForm main)
        {
            mainForm = main;
        }

        public override bool DoesAcceptEvent(PInputEventArgs e)
        {
            return e.IsMouseEvent && e.Button == MouseButtons.Right;
            //return base.DoesAcceptEvent(e);
        }

        public override void OnMouseDown(object sender, PInputEventArgs e)
        {
            //base.OnMouseDown(sender, e);
            //e.Position;
            //e.CanvasPosition.
            mainForm.setXYCoordinates(e.Position.X, e.CanvasPosition.X, e.Position.Y, e.CanvasPosition.Y);
        }

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
        }

        public override void OnDoubleClick(object sender, PInputEventArgs e)
        {
            e.TopCamera.SetOffset(0, 0);
        }
    }
}
