using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{
    public enum ElementType
    {
        Creature,
        Items,
        Trigger,
        Text
    }


    public partial class AddListElementForm : Form
    {
        public AddListElementForm(ElementType elementType)
        {
            InitializeComponent();
            setupUI(elementType);
        }

        private void setupUI(ElementType elementType)
        {
            this.text.Visible = elementType == ElementType.Text;
            this.comboBox2.Visible = elementType == ElementType.Creature;
            this.comboBox1.Visible = new ElementType[] { ElementType.Creature, ElementType.Items }.Contains(elementType);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
