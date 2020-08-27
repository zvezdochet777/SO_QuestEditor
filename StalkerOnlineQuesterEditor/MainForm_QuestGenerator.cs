using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{
    public partial class MainForm : Form
    {
        


        private void btnAddTarget_Click(object sender, EventArgs e)
        {
            AddListElementForm form = new AddListElementForm(ElementType.Creature);
        }
    }
}
