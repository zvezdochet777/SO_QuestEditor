using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor.Forms
{
    public partial class LocaleDialogForm : Form
    {
        MainForm parent;
        CDialog originalDialog;

        public LocaleDialogForm(MainForm parent, int selectedDialogID)
        {
            InitializeComponent();
            this.parent = parent;
            
            var dialog = parent.getDialogOnDialogID(selectedDialogID);
            //System.Console.WriteLine("LocaleDialogForm::__init__ " + dialog);
            NPCReactiontextBox.Text = dialog.Text;
            textBoxAnswer.Text = dialog.Title;
            lViewNpcName.Text = dialog.Holder;
            lViewDialogId.Text = dialog.DialogID.ToString();
            this.originalDialog = dialog;
            var localeDialog = parent.getLocaleDialog(dialog.DialogID, dialog.Holder);

            if (localeDialog != null)
            {
                textBoxNPCReactionLocale.Text = localeDialog.Text;
                textBoxAnswerLocale.Text = localeDialog.Title;
            }


        }

        private void LocaleDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.parent.Enabled = true;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            CDialog newDialog = new CDialog();
            newDialog.Text = textBoxNPCReactionLocale.Text;
            newDialog.Title = textBoxAnswerLocale.Text;
            newDialog.DialogID = originalDialog.DialogID;
            newDialog.Holder = originalDialog.Holder;
            newDialog.version = originalDialog.version;
            newDialog.Nodes = originalDialog.Nodes;
            parent.addLocaleDialog(newDialog);
            this.Close();
        }
    }
}
