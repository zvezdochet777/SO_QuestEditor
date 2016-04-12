using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

using UMD.HCIL.Piccolo;
using UMD.HCIL.Piccolo.Nodes;
using UMD.HCIL.Piccolo.Util;
using UMD.HCIL.Piccolo.Event;

namespace StalkerOnlineQuesterEditor
{
    //! Class, that catches the LeftMouseButton event from the Piccollo canvas and draws a rectangle on it
    public class RectangleDrawingHandler : PBasicInputEventHandler
    {
        // The rectangle that is currently getting created.
        protected PPath rectangle;

        // The mouse press location for the current pressed, drag, release sequence.
        protected PointF pressPoint;

        // The current drag location.
        protected PointF dragPoint;

        private MainForm mainForm;

        public RectangleDrawingHandler(MainForm form)
        {
            this.mainForm = form;
        }

        public override bool DoesAcceptEvent(PInputEventArgs e)
        {
            //var node = e.PickedNode;
            //e.Handled = true;
            return e.IsMouseEvent && e.Button == MouseButtons.Left && e.Modifiers == Keys.Shift;
            //return base.DoesAcceptEvent(e);
        }

        public override void OnMouseDown(object sender, PInputEventArgs e)
        {
            base.OnMouseDown(sender, e);
            PLayer layer = e.Canvas.Layer;

            // Initialize the locations.
            pressPoint = e.Position;
            dragPoint = pressPoint;

            // Create a new rectangle and add it to the canvas layer so that we can see it.
            rectangle = new PPath();
            rectangle.Pen = new Pen(Brushes.Black, (float)(1 / e.Camera.ViewScale));
            layer.AddChild(rectangle);

            // update the rectangle shape.
            UpdateRectangle();
        }

        public override void OnMouseDrag(object sender, PInputEventArgs e)
        {
            base.OnMouseDrag(sender, e);
            // Update the drag point location.
            dragPoint = e.Position;

            // Update the rectangle shape.
            UpdateRectangle();
        }

        public override void OnMouseUp(object sender, PInputEventArgs e)
        {
            base.OnMouseUp(sender, e);
            // Update the rectangle shape.
            UpdateRectangle();
            if (rectangle != null)
            {
                PPath layeredRect = new PPath();
                layeredRect = PPath.CreateRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                mainForm.drawingLayer.AddChild(layeredRect);
                rectangle.PathReference.Reset();
            }
            rectangle = null;
        }

        public void UpdateRectangle()
        {
            // Create a new bounds that contains both the press and current drag point.
            if (rectangle != null)
            {
                RectangleF r = RectangleF.Empty;
                r = PUtil.AddPointToRect(r, pressPoint);
                r = PUtil.AddPointToRect(r, dragPoint);

                // Set the rectangles bounds.
                rectangle.PathReference.Reset();
                rectangle.AddRectangle(r.X, r.Y, r.Width, r.Height);
            }
        }
    }

}
