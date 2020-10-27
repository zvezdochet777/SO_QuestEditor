using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor
{
    public partial class MainForm
    {

        private void fillAutogeneratorTab()
        {
            int old_index = listBoxQT.SelectedIndex >= 0 ? listBoxQT.SelectedIndex : 0;
            
            listBoxQT.Items.Clear();
            listBoxTarget.Items.Clear();

            if (!QAutogenDatacs.data_quests.ContainsKey(this.currentNPC))
                return;

            foreach (var i in QAutogenDatacs.data_quests[currentNPC].data)
                listBoxQT.Items.Add(i.name.ToString());
            if (listBoxQT.Items.Count > 0)
                listBoxQT.SelectedIndex = old_index;

            AutoGenDialog dialogs = this.autogenDialogs[settings.getCurrentLocale()][this.currentNPC];

            foreach (var i in new List<ListBox>() { lbAGOnTest, lbAGOnTest2, lbAGClosed, lbAGClosed2, lbAGOpened, lbAGOpened2 })
                i.Items.Clear();

            foreach(var i in dialogs.ontest.Text)
                lbAGOnTest2.Items.Add(i.Key.ToString() + " " + i.Value);
            foreach (var i in dialogs.ontest.Titles)
                lbAGOnTest.Items.Add(i.Key.ToString() + " " + i.Value);

            foreach (var i in dialogs.opened.Text)
                lbAGOpened2.Items.Add(i.Key.ToString() + " " + i.Value);
            foreach (var i in dialogs.opened.Titles)
                lbAGOpened.Items.Add(i.Key.ToString() + " " + i.Value);

            foreach (var i in dialogs.failed.Text)
                lbAGClosed2.Items.Add(i.Key.ToString() + " " + i.Value);
            foreach (var i in dialogs.failed.Titles)
                lbAGClosed.Items.Add(i.Key.ToString() + " " + i.Value);

        }

        private void listBoxQT_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxTarget.Items.Clear();
            AutogenQuestType quest_type = QAutogenDatacs.data[currentNPC].getQuestTypeByName(listBoxQT.SelectedItem.ToString());
            foreach (var i in quest_type.targets)
            {
                listBoxTarget.Items.Add(i.name);
            }

            if (listBoxTarget.Items.Count > 0)
                listBoxTarget.SelectedIndex = 0;
        }



        private void btnChange_Click(object sender, EventArgs e)
        {
            AutogenQuestType quest_type = QAutogenDatacs.data[currentNPC].getQuestTypeByName(listBoxQT.SelectedItem.ToString());
            int target_type = QAutogenDatacs.data[currentNPC].getQuestTargetByName(quest_type.id, listBoxTarget.SelectedItem.ToString());

            AddListElementForm form = new AddListElementForm((ElementType)quest_type.id, quest_type.getTargetByType(target_type), this);
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                quest_type.setTargetByType(target_type, form.getData());
                isDirty = true;
            }

            fillAutogeneratorTab();


        }

        private void btnAddTarget_Click(object sender, EventArgs e)
        {
            AutogenQuestType quest_type = QAutogenDatacs.data[currentNPC].getQuestTypeByName(listBoxQT.SelectedItem.ToString());
            AddListElementForm form = new AddListElementForm((ElementType)quest_type.id, null, this);
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                var new_target = form.getData();
                new_target.id = quest_type.getNewTargetID();
                quest_type.setTargetByType(new_target.id, new_target);
                isDirty = true;
            }
            fillAutogeneratorTab();
        }


        private void btnDelTarget_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что сейчас хотите удалить тип цели?", "Внимание", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            AutogenQuestType quest_type = QAutogenDatacs.data[currentNPC].getQuestTypeByName(listBoxQT.SelectedItem.ToString());
            int target_type = QAutogenDatacs.data[currentNPC].getQuestTargetByName(quest_type.id, listBoxTarget.SelectedItem.ToString());
            if (target_type == 0)
            {
                MessageBox.Show("Ошибка");
                return;
            }

            quest_type.deleteTarget(target_type);
            isDirty = true;
            fillAutogeneratorTab();
        }


        private void btnAddQType_Click(object sender, EventArgs e)
        {
            if (QAutogenDatacs.data.ContainsKey(currentNPC) && QAutogenDatacs.data[currentNPC].data.Count == QAutogenDatacs.QuestTypes.Count)
            {
                MessageBox.Show("Все возможные типы уже добавлены", "Внимание");
                return;
            }

            AutogenTarget data = new AutogenTarget();
            foreach(var i in QAutogenDatacs.QuestTypes.Keys)
                data.counts.Add(i);
            if (QAutogenDatacs.data.ContainsKey(currentNPC))
                foreach (var i in QAutogenDatacs.data[currentNPC].data)
                    data.counts.Remove(i.id);

            AddListElementForm form = new AddListElementForm(ElementType.None, data, this);
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                var new_target = form.getData();
                foreach(var i in new_target.counts)
                {
                    AutogenQuestType new_type = new AutogenQuestType();
                    new_type.id = i;
                    new_type.name = QAutogenDatacs.QuestTypes[i];

                    if (!QAutogenDatacs.data.ContainsKey(currentNPC))
                        QAutogenDatacs.data.Add(currentNPC, new AutogenQuestData());
                    QAutogenDatacs.data[currentNPC].data.Add(new_type);

                }
                isDirty = true;
            }
            fillAutogeneratorTab();
        }

        private void btnDelQType_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что сейчас хотите удалить тип квеста?", "Внимание", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            AutogenQuestType quest_type = QAutogenDatacs.data[currentNPC].getQuestTypeByName(listBoxQT.SelectedItem.ToString());
            QAutogenDatacs.data[currentNPC].data.Remove(quest_type);
            fillAutogeneratorTab();
            isDirty = true;
        }

        private void listBoxTarget_DoubleClick(object sender, EventArgs e)
        {
            this.btnChange_Click(sender, e);
        }


        private void listBoxTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutogenQuestType quest_type = QAutogenDatacs.data[currentNPC].getQuestTypeByName(listBoxQT.SelectedItem.ToString());
            int target_type = QAutogenDatacs.data[currentNPC].getQuestTargetByName(quest_type.id, listBoxTarget.SelectedItem.ToString());

            AutogenTarget t = quest_type.getTargetByType(target_type);

            if (t.counts.Count != 2)
            {
                nupFromTargetCount.Value = 0;
                nupToTargetCount.Value = 0;
            }
            else
            {
                nupFromTargetCount.Value = t.counts[0];
                nupToTargetCount.Value = t.counts[1];
            }
            listBoxReward.Items.Clear();
            foreach (var i in t.rewards)
            {
                listBoxReward.Items.Add("деньги:" + i.money.ToString() + " опыт:" + i.exp.ToString());
            }
        }

        private void nupFromTargetCount_ValueChanged(object sender, EventArgs e)
        {
            AutogenQuestType quest_type = QAutogenDatacs.data[currentNPC].getQuestTypeByName(listBoxQT.SelectedItem.ToString());
            int target_type = QAutogenDatacs.data[currentNPC].getQuestTargetByName(quest_type.id, listBoxTarget.SelectedItem.ToString());
            AutogenTarget t = quest_type.getTargetByType(target_type);
            isDirty = true;
            if (t.counts.Count < 1)
            {
                t.counts.Add(Convert.ToInt32(nupFromTargetCount.Value));
                t.counts.Add(0);
                return;
            }
            t.counts[0] = Convert.ToInt32(nupFromTargetCount.Value);
        }

        private void nupToTargetCount_ValueChanged(object sender, EventArgs e)
        {
            AutogenQuestType quest_type = QAutogenDatacs.data[currentNPC].getQuestTypeByName(listBoxQT.SelectedItem.ToString());
            int target_type = QAutogenDatacs.data[currentNPC].getQuestTargetByName(quest_type.id, listBoxTarget.SelectedItem.ToString());
            AutogenTarget t = quest_type.getTargetByType(target_type);
            isDirty = true;
            if (t.counts.Count < 1)
            {
                t.counts.Add(0);
                t.counts.Add(Convert.ToInt32(nupToTargetCount.Value));
                return;
            }
            t.counts[1] = Convert.ToInt32(nupToTargetCount.Value);
        }

        private void btnAddReward_Click(object sender, EventArgs e)
        {
            AddListElementForm form = new AddListElementForm(ElementType.Reward, null, this);
            DialogResult result = form.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }
            AutogenQuestType quest_type = QAutogenDatacs.data[currentNPC].getQuestTypeByName(listBoxQT.SelectedItem.ToString());
            int target_type = QAutogenDatacs.data[currentNPC].getQuestTargetByName(quest_type.id, listBoxTarget.SelectedItem.ToString());
            AutogenTarget t = quest_type.getTargetByType(target_type);
            isDirty = true;
            AutogenTarget a = form.getData();
            Reward reward = new Reward();
            reward.exp = a.id;
            reward.money = a.int_param;
            t.rewards.Add(reward);
            listBoxTarget_SelectedIndexChanged(sender, e);
        }

        private void btnDelReward_Click(object sender, EventArgs e)
        {
            AutogenQuestType quest_type = QAutogenDatacs.data[currentNPC].getQuestTypeByName(listBoxQT.SelectedItem.ToString());
            int target_type = QAutogenDatacs.data[currentNPC].getQuestTargetByName(quest_type.id, listBoxTarget.SelectedItem.ToString());
            AutogenTarget t = quest_type.getTargetByType(target_type);
            t.rewards.RemoveAt(listBoxReward.SelectedIndex);
            isDirty = true;
            listBoxTarget_SelectedIndexChanged(sender, e);
        }

        private void btnChangeReward_Click(object sender, EventArgs e)
        {
            AutogenQuestType quest_type = QAutogenDatacs.data[currentNPC].getQuestTypeByName(listBoxQT.SelectedItem.ToString());
            int target_type = QAutogenDatacs.data[currentNPC].getQuestTargetByName(quest_type.id, listBoxTarget.SelectedItem.ToString());
            AutogenTarget t = quest_type.getTargetByType(target_type);
            Reward reward = t.rewards[listBoxReward.SelectedIndex];

            AutogenTarget a = new AutogenTarget();
            a.id = reward.exp;
            a.int_param = reward.money;
            isDirty = true;
            AddListElementForm form = new AddListElementForm(ElementType.Reward, a, this);
            DialogResult result = form.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }


            a = form.getData();
            reward.exp = a.id;
            reward.money = a.int_param;
            listBoxTarget_SelectedIndexChanged(sender, e);
        }

    }
}
