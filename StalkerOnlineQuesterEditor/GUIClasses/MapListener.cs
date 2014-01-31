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

    public class MapNodeDragHandler : PDragEventHandler
    {
        public MainForm form;
        

        public MapNodeDragHandler(MainForm form)
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



            }
        }


   }

}
