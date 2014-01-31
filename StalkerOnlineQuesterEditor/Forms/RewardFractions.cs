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
    public partial class RewardFractions : Form
    {
        EditQuestForm form;
        public RewardFractions(EditQuestForm form)
        {
            this.form = form;
            InitializeComponent();

            dataFractions.LostFocus += new EventHandler(dataFractions_LostFocus);
            dataFractions.GotFocus += new EventHandler(dataFractions_GotFocus);

            foreach (KeyValuePair<int, string> pair in form.parent.fractions.getListOfFractions())
            {
                string id = pair.Key.ToString();
                string name = pair.Value;
                bool rew = false;
                if (form.editQuestReward.Fractions.Contains(pair.Key))
                    rew = true;
                //if (form.editQuestReward.Reputation.Keys.Contains(pair.Key))
                //    rew = form.editQuestReward.Reputation[pair.Key].ToString();
                object[] row = { id, name, rew };
                dataFractions.Rows.Add(row);
            }

            if (form.editQuestReward.Unlimited == 1)
                unlimitedCheckBox.Checked = true;


            //label1.Text = 
            //"торговцы(+100%)/военные(+20%)/наемники(+20%)/бандиты(-10%)/старые-сталкеры(-20%)/барыги(-30%)/охотники(-10%)\n"+
            //"барыги(+100%)/бандиты(+20%)/старые-сталкеры(+20%)/военные(-10%)/наемники(-20%)/торговцы(-30%)/охотники(+10%)\n" +
            //"бандиты(+100%)/старые-сталкеры(+10%)/барыги(+10%)/военные(-60%)/наемники(-40%)/охотники(-10%)\n" +
            //"военные(+100%)/наемники(+10%)/торговцы(+10%)/бандиты(-60%)/старые-сталкеры(-40%)/охотники(+10%)\n" +

            //"наемники(+100%)/военные(+20%)/торговцы(+10%)/бандиты(-40%)/старые-сталкеры(-60%)/барыги(-10%)\n" +
            //"старые-сталкеры(+100%)/бандиты(+20%)/барыги(+10%)/военные(-40%)/наемники(-60%)/торговцы(-10%)\n" +

            //"охотники(+100%)/военные(+10%)/бандиты(+10%)/торговцы(-20%)/барыги(-20%)/ученые(+10%)\n" +
            //"ученые(+100%)/военные(-10%)/бандиты(-10%)/наемники(-10%)/старые-сталкеры(-10%)/мутанты зоны(+20%)/фракция группировок зоны(+10%)/охотники(+20%)\n" +

            //"мутанты зоны(+100%)/военные(-10%)/бандиты(-10%)/торговцы(-20%)/барыги(-20%)/охотники(-30%)/старые-сталкеры(+10%)\n" +
            //"фракция группировок зоны(+100%)/военные(+5%)/бандиты(+5%)/наемники(-10%)/старые-сталкеры(-10%)/ученые(+5%)/охотники(-5%)";

        }


        private void RewardFractions_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Enabled = true;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            //form.editQuestReward.Reputation.Clear();
            form.editQuestReward.Fractions.Clear();
            foreach (DataGridViewRow row in dataFractions.Rows)
            {
                int id = int.Parse(row.Cells[0].FormattedValue.ToString());

                bool rew = false;
                if (row.Cells[2].FormattedValue.ToString() != "")
                    rew = bool.Parse(row.Cells[2].FormattedValue.ToString());
                if (rew == true)
                    form.editQuestReward.Fractions.Add(id); //.Reputation.Add(id, rew);
            }

            if (unlimitedCheckBox.Checked)
                form.editQuestReward.Unlimited = 1;
            else
                form.editQuestReward.Unlimited = 0;
            form.checkRewardIndicates();
            this.Close();
        }

        //private void autoSet_Click(object sender, EventArgs e)
        //{
        //    if (dataFractions.SelectedRows.Count == 1)
        //    {
        //        int fractdID = int.Parse(dataFractions.SelectedRows[0].Cells[0].Value.ToString());
        //        int value = int.Parse(dataFractions.SelectedRows[0].Cells[2].Value.ToString());
        //        //System.Console.WriteLine("Selected: " + fractdID.ToString());
        //        calcAutoSet(value, fractdID);
        //        return;

        //    }
        //}

        void calcAutoSet(int value, int fractionID)
        {
            //System.Console.WriteLine("calcAutoSet: " + value.ToString() + " " + fractionID.ToString());
            switch (fractionID)
            {
                case 5:
                    setValue(5, value);
                    setValue(8, value * 0.2);
                    setValue(9, value * 0.2);
                    setValue(7, - value * 0.1);
                    setValue(13,- value * 0.2);
                    setValue(11, - value * 0.1);
                    break;
                case 6:
                    setValue(6, value);
                    setValue(7, value * 0.2);
                    setValue(13,value * 0.2 );
                    setValue(8, - value * 0.1);
                    setValue(9, - value * 0.2);
                    setValue(5, - value * 0.3);
                    setValue(11, value * 0.1);   
                    break;
                case 7:
                    setValue(7, value);
                    setValue(13, value * 0.1);
                    setValue(6, value * 0.1);
                    setValue(8, - value * 0.6);
                    setValue(9, - value * 0.4);
                    setValue(11, - value * 0.1);
                    break;
                case 8:
                    setValue(8, value);
                    setValue(9, value * 0.1);
                    setValue(5, value * 0.1);
                    setValue(7, - value * 0.6);
                    setValue(13, - value * 0.4 );
                    setValue(11, value * 0.1);
                    break;
                case 9:
                    setValue(9, value);
                    setValue(8, value * 0.2);
                    setValue(5, value * 0.1);
                    setValue(7, - value * 0.4);
                    setValue(13,- value * 0.6);
                    setValue(6, - value * 0.1);
                    break;
                case 13:
                    setValue(13, value);
                    setValue(7, value * 0.2);
                    setValue(6, value * 0.1);
                    setValue(8, - value * 0.4);
                    setValue(9, - value * 0.6);
                    setValue(5, - value * 0.1);
                    break;
                case 11:
                    setValue(13, value);
                    setValue(8, value * 0.1);
                    setValue(7, value * 0.1);
                    setValue(5, -value * 0.2);
                    setValue(6, -value * 0.2);
                    setValue(12, -value * 0.1);
                    break;
                case 12:
                    setValue(12, value);
                    setValue(8, - value * 0.1);
                    setValue(7, - value * 0.1);
                    setValue(9, -value * 0.1);
                    setValue(13, -value * 0.1);
                    setValue(14, value * 0.2);
                    //setValue(14, value * 0.1); группировки зоны
                    setValue(11, value * 0.2);
                    break;
                case 14:
                    setValue(14, value);
                    setValue(8, -value * 0.1);
                    setValue(7, -value * 0.1);
                    setValue(5, -value * 0.2);
                    setValue(6, -value * 0.2);
                    setValue(11,-value * 0.3);
                    setValue(13, value * 0.1);
                    break;
                case 10: //группировки зоны
                    setValue(10, value);
                    setValue(8, value * 0.05);
                    setValue(7, value * 0.05);
                    setValue(9, -value * 0.1);
                    setValue(13, -value * 0.1);
                    setValue(12, value * 0.05);
                    setValue(11, -value * 0.05);
                    break;
            }

        }

        void setValue(int fractionID, double value)
        {
            int val = Convert.ToInt32(value);
            foreach (DataGridViewRow row in dataFractions.Rows)
            {
                if (int.Parse(row.Cells[0].Value.ToString()) == fractionID)
                {
                    row.Cells[2].Value = val.ToString();
                    return;
                }
            }
        }



        private void dataFractions_LostFocus(object sender, EventArgs e)
        {
        }

        private void dataFractions_GotFocus(object sender, EventArgs e)
        {
        }
        
    }
}
