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
    //! Форма настроек редактора
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
            if (CSettings.getOperatorNumber() >= 9)
                operatorSelectComboBox.SelectedIndex = CSettings.getOperatorNumber() - 3;
            else
                operatorSelectComboBox.SelectedIndex = CSettings.getOperatorNumber();

            localesTextBox.Text = CSettings.getLocales();
            foreach (string locale in localesTextBox.Text.Split(','))
                localeComboBox.Items.Add(locale);

            if (CSettings.getMode() == CSettings.MODE_LOCALIZATION)
            {
                localizeCheckBox.Checked = true;
                localeComboBox.SelectedIndex = CSettings.getCurrentIndexLocale();
            }
            tbAddressToCopyFiles.Text = CSettings.pathQuestDataFiles;
        }

        //! Нажатие ОК - магические действия с номером оператора и выход на главную
        private void bOK_Click(object sender, EventArgs e)
        {
            //operator settings
            if (CSettings.getOperatorNumber() != operatorSelectComboBox.SelectedIndex)
            {
                int operatorIndex = 0;
                if (operatorSelectComboBox.SelectedIndex >= 6)
                    operatorIndex = operatorSelectComboBox.SelectedIndex + 3;
                else
                    operatorIndex = operatorSelectComboBox.SelectedIndex;
                CSettings.setOperatorNumber(operatorIndex);
            }

            //locales settings
            CSettings.setLocales(localesTextBox.Text);
            if (localizeCheckBox.Checked)
            {
                if (!CSettings.setLocale(localeComboBox.SelectedIndex))
                    localizeCheckBox.Enabled = false;
            }
            else
                CSettings.setEditorMode();

            CSettings.pathQuestDataFiles = tbAddressToCopyFiles.Text;
            CSettings.saveSettings();
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //! Обновляет список локалей из xml файла и записывает их в комбо-бокс
        private void bLocaleRefresh_Click(object sender, EventArgs e)
        {
            localeComboBox.Items.Clear();
            CSettings.setLocales(localesTextBox.Text);
            CSettings.saveSettings();
            localeComboBox.Items.Clear();
            foreach (string locale in CSettings.getLocales().Split(','))
                localeComboBox.Items.Add(locale);
        }

        //! Клик по чекбоксу "локализация"
        private void localizeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CSettings.getLocales() == "")
                localizeCheckBox.Checked = false;
            localeComboBox.Enabled = localizeCheckBox.Checked;
            localeComboBox.SelectedIndex = 0;
        }

        //! Закрытие формы настроек
        private void OperatorSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Enabled = true;
            parent.Visible = true;
        }

    }
}
