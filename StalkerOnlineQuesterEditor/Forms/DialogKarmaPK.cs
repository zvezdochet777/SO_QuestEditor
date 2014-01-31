using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{
    public partial class DialogKarmaPK : Form
    {
        EditDialogForm form;
        public DialogKarmaPK(EditDialogForm form)
        {
            this.form = form;
            InitializeComponent();
            label2.Text = "от 500 до 400 (включительно) – Законопослушный.\nот 399 до 250 (включительно) – Нейтральный.\nот 249 до 100 (включительно) – Вне закона.\nот 99 и ниже – Убийца";
            List<int> karma = form.editKarmaPK;
            if (karma.Any())
            {
                if (karma[0] == 0 || karma[0] == 1)
                {
                    aTextBox.Text = karma[1].ToString();
                }
                if (karma[0] == 0 || karma[0] == 2)
                {
                    bTextBox.Text = karma[2].ToString();
                }
            }

        }

        private void bOK_Click(object sender, EventArgs e)
        {
            form.editKarmaPK.Clear();
            int flag = 0;
            int a = 0;
            int b = 0;
            if ((aTextBox.Text != "") || (bTextBox.Text != ""))
            {
                if (aTextBox.Text != "")
                {
                    a = int.Parse(aTextBox.Text);
                    flag = 1;
                }
                if (bTextBox.Text != "")
                {
                    b = int.Parse(bTextBox.Text);
                    if (flag == 1)
                    {
                        flag = 0;
                    }
                    else
                    {
                        flag = 2;
                    }
                }

                form.editKarmaPK.Add(flag);
                form.editKarmaPK.Add(a);
                form.editKarmaPK.Add(b);
            }

            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DialogKarmaPK_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Enabled = true;
        }
    }
}
