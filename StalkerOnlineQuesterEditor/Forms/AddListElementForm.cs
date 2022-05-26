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
        Creature = 1,
        Items = 2,
        Trigger = 3,
        Reward = 4,
        None = 5
    }


    public partial class AddListElementForm : Form
    {
        MainForm parent;

        ElementType current_type;

        public AddListElementForm(ElementType elementType, AutogenTarget data, string name, MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.Text = data == null ? "Добавляем новую цель" : "Меняем цель";
            setupUI(elementType, data, name);
        }

        public string getName()
        {
            return tbText1.Text;
        }
        public AutogenTarget getData()
        {
            AutogenTarget result = new AutogenTarget();
            //result.id = id > 0 ? this.id : QAutogenDatacs.getNewTargetID();
            int int_param = 0;
            label3.Visible = false;
            tbText3.Visible = false;
            tbText4.Visible = false;
            label5.Visible = false;

            if (current_type == ElementType.Creature)
            {
                int_param = parent.mobConst.getTypeOnDescription(comboBox1.SelectedItem.ToString());
            }

            if (current_type == ElementType.None)
            {
                foreach (var i in QAutogenDatacs.QuestTypes)
                {
                    if (i.Value == comboBox1.SelectedItem.ToString())
                        result.counts.Add(i.Key);
                }
            }
            if (current_type == ElementType.Items)
            {
                int_param = parent.itemConst.getIDOnName(comboBox1.SelectedItem.ToString());
            }
            if (current_type == ElementType.Reward)
            {
                int_param = Convert.ToInt32(tbText1.Text);
                result.id = Convert.ToInt32(tbText2.Text);

                int fraction = 0; 
                int repValue = 0;
                int repOT = 0;
                if (comboBox2.SelectedItem != null)
                    fraction = parent.fractions2.getFractionIDByDescr(comboBox2.SelectedItem.ToString());
                try
                {
                    repValue = Convert.ToInt32(tbText3.Text);
                }
                catch
                {
                    if (fraction > 0)
                    {
                        MessageBox.Show("Количество репутации указано неверно");
                        return null;
                    }
                }
                try
                {
                    repOT = Convert.ToInt32(tbText4.Text);
                }
                catch
                {
                    if (fraction > 0)
                    {
                        MessageBox.Show("Количество очков торговли указано неверно");
                        return null;
                    }
                }


                if (fraction > 0 && (repValue != 0 || repOT != 0))
                    result.str_param = fraction.ToString() + ":" + repValue.ToString()+":"+ repOT.ToString();
            }
            else
                result.str_param = comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : "";

            result.int_param = int_param;
           
            return result;
        }

        private void setupUI(ElementType elementType, AutogenTarget data, string name)
        {
            current_type = elementType;
            this.tbText2.Visible = elementType == ElementType.Reward;
            this.comboBox2.Visible = elementType == ElementType.Creature;
            label4.Visible = false;
            tbText3.Visible = false;
            label5.Visible = false;
            tbText4.Visible = false;
            this.comboBox1.Visible = (new ElementType[] { ElementType.Creature, ElementType.Items, ElementType.None}).Contains(elementType);

            if (elementType == ElementType.Creature)
            {
                label1.Text = "Тип моба";
                label2.Text = "Подип моба";
                comboBox1.Items.Clear();
                Dictionary <int, CMobDescription> mobs = parent.mobConst.getAllDescriptions();
                foreach(var i in mobs)
                {
                    comboBox1.Items.Add(i.Value.getName());
                }
                if (data != null)
                {
                    tbText1.Text = name;
                    comboBox1.SelectedItem = mobs[data.int_param].getName();
                    comboBox2.SelectedItem = data.str_param;
                }
                else { comboBox1.SelectedIndex = 0; }
            }
            else if (elementType == ElementType.Items)
            {
                label1.Text = "Тип предмета";
                comboBox1.Visible = true;
                tbText1.Visible = true;
                label2.Visible = false;
                label3.Visible = false;
                comboBox1.Items.Clear();
                comboBox2.Visible = false;
                foreach (var i in parent.itemConst.getAllItems())
                {
                    comboBox1.Items.Add(i.Value.getName());
                }
                if(data != null)
                    comboBox1.SelectedItem = parent.itemConst.getItemName(data.int_param);
                tbText1.Text = name;
            }
            else if (elementType == ElementType.None)
            {
                label1.Text = "Тип квеста";
                tbText1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                comboBox1.Items.Clear();
                comboBox2.Visible = false;
                foreach (var i in data.counts)
                {
                    comboBox1.Items.Add(QAutogenDatacs.QuestTypes[i]);
                }
            }
            else if (elementType == ElementType.Reward)
            {
                label1.Text = "Опыт:";
                label2.Text = "Группа:";
                label3.Text = "Деньги";
                label4.Text = "Репутация";
                label5.Text = "Очки торг";
                label4.Visible = true;
                tbText3.Visible = true;
                label5.Visible = true;
                tbText4.Visible = true;
                //label2.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = true;

                tbText1.Text = data == null ? "0" : data.int_param.ToString();
                tbText2.Text = data == null ? "0" : data.id.ToString();
                int fraction = data == null ? 0 : Convert.ToInt32(data.str_param.Split(':')[0]);
                
                foreach (var i in parent.fractions2.getListOfFractions())
                    comboBox2.Items.Add(i.Value);
                comboBox2.SelectedItem = parent.fractions2.getFractionDesctByID(fraction);
                tbText3.Text = data == null ? "0" : data.str_param.Split(':')[1];
                tbText4.Text = data == null ? "0" : data.str_param.Split(':')[2];
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (current_type != ElementType.Creature) return;
            comboBox2.Items.Clear();
            
            foreach (var i in parent.mobConst.getLevelsOnDescription(comboBox1.SelectedItem.ToString()))
                comboBox2.Items.Add(i);
            comboBox2.SelectedIndex = 0;
        }
    }
}
