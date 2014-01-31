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
    public partial class OperatorSettings : Form
    {
        MainForm parent;
        //bool bOperatorChanged;
        public OperatorSettings(MainForm parent)
        {
            InitializeComponent();

            this.parent = parent;
            //this.bOperatorChanged = false;
            operatorSelectComboBox.Items.Add("Разраб(без огр.)");
            operatorSelectComboBox.Items.Add("Оператор 1");
            operatorSelectComboBox.Items.Add("Оператор 2");
            operatorSelectComboBox.Items.Add("Оператор 3");
            operatorSelectComboBox.Items.Add("Дизайнер");
            operatorSelectComboBox.Items.Add("Оператор 5");
            operatorSelectComboBox.Items.Add("Оператор 6");
            operatorSelectComboBox.Items.Add("Оператор 7");
            operatorSelectComboBox.Items.Add("Оператор 8");
            operatorSelectComboBox.Items.Add("Оператор 9");
            operatorSelectComboBox.Items.Add("Оператор 10");
            operatorSelectComboBox.Items.Add("Оператор 12");
            operatorSelectComboBox.Items.Add("Оператор 13");
            operatorSelectComboBox.Items.Add("Оператор 14");
            operatorSelectComboBox.Items.Add("Оператор 15");
            operatorSelectComboBox.Items.Add("Оператор 16");
            if (parent.settings.getOperatorNumber() >= 9)
                operatorSelectComboBox.SelectedIndex = parent.settings.getOperatorNumber() - 3;
            else
                operatorSelectComboBox.SelectedIndex = parent.settings.getOperatorNumber();

            localesTextBox.Text = parent.settings.getLocales();
            foreach (string locale in localesTextBox.Text.Split(','))
                localeComboBox.Items.Add(locale);


            if (parent.settings.getMode() == 1)
            {
                localizeCheckBox.Checked = true;
                localeComboBox.SelectedIndex = parent.settings.getCurrentIndexLocale();
            }



        }

        private void operatorSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Console.WriteLine("bOperatorChanged");
            //if (parent.settings.getOperatorNumber() != operatorSelectComboBox.SelectedIndex)
            //    System.Console.WriteLine("bOperatorChanged=true");
            //    this.bOperatorChanged = true;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            //operator settings
            if (parent.settings.getOperatorNumber() != operatorSelectComboBox.SelectedIndex)
            {
                int operatorIndex = 0;
                if (operatorSelectComboBox.SelectedIndex >= 6)
                    operatorIndex = operatorSelectComboBox.SelectedIndex + 3;
                else
                    operatorIndex = operatorSelectComboBox.SelectedIndex;
                parent.settings.setOperatorNumber(operatorIndex);
                

            }
            //locales settings

            parent.settings.setLocales(localesTextBox.Text);
            if (localizeCheckBox.Checked)
            {
                if (!parent.settings.setLocale(localeComboBox.SelectedIndex))
                    localizeCheckBox.Enabled = false;
            }
            else
                parent.settings.setSimple();
            
            
            
            parent.settings.saveSettings();
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bLocaleRefresh_Click(object sender, EventArgs e)
        {
            localeComboBox.Items.Clear();
            parent.settings.setLocales(localesTextBox.Text);
            parent.settings.saveSettings();
            localeComboBox.Items.Clear();
            foreach (string locale in parent.settings.getLocales().Split(','))
                localeComboBox.Items.Add(locale);
        }

        private void localizeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (parent.settings.getLocales() == "")
                localizeCheckBox.Checked = false;
            localeComboBox.Enabled = localizeCheckBox.Checked;

        }

        private void OperatorSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Enabled = true;
            parent.Visible = true;
        }

        private void bCreateExamples_Click(object sender, EventArgs e)
        {
            parent.createExamples();
        }

        private void bCreateResult_Click(object sender, EventArgs e)
        {
            parent.createResults();
        }
    }
}
