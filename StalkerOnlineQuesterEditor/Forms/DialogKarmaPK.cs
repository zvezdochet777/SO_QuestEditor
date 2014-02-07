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
    //! Форма заполнения КармыПК для диалогов.
    public partial class DialogKarmaPK : Form
    {
        EditDialogForm form;
        //! Конструктор - заполняет поля формы значениями кармы ПК
        public DialogKarmaPK(EditDialogForm form)
        {
            this.form = form;
            InitializeComponent();
            labelDescription.Text = "Задаются пороги Кармы A,B такие, что A < Karma < B \n" +
                "Игрок начинает игру с Кармой ПК = 0. \n" +
                "За каждое ПК убийство Карма увеличивается на 100. \n" +
                "После превышения Кармой 500 жизнь осложняется: \n" +
                "Дроп выше, NPC не разговаривают, ник подсвечен красным";

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

        //! Нажатие ОК - сохранение типа проверки кармы, порогов a,b
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
        //! Нажатие Отмена - выход без сохранения
        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //! Закрытие формы
        private void DialogKarmaPK_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Enabled = true;
        }
    }
}
