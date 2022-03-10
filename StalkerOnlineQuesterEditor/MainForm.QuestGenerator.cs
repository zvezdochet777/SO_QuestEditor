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
            

            //Вкладка диалоги
            cbNPCNature.SelectedIndex = 0;
            cbNPCNature_SelectedIndexChanged(null, null);

            //Вкладка квесты
            List<ListBoxItem> list = new List<ListBoxItem>();

            if (QAutogenDatacs.data_quests.ContainsKey(this.currentNPC))
            {
                if (QAutogenDatacs.data_quests[currentNPC].data.Count > 0)
                    foreach (var i in QAutogenDatacs.data_quests[currentNPC].data)
                    {
                        string name = QAutogenDatacs.QuestTypes[i.id];
                        list.Add(new ListBoxItem(i.id, name));
                    }
                else
                {
                    listBoxTarget.DataSource = null;
                    //listBoxTarget.Items.Clear();
                    //listBoxTarget.DataSource = new List<ListBoxItem>();
                    listBoxTarget.Update();
                    listBoxTarget.Refresh();
                    listBoxReward.Items.Clear();
                }
            }
            else
            {
                listBoxTarget.DataSource = null;
                //listBoxTarget.Items.Clear();
                //listBoxTarget.DataSource = new List<ListBoxItem>();
                listBoxTarget.Update();
                listBoxTarget.Refresh();
                listBoxReward.Items.Clear();
            }
            listBoxQT.DataSource = null;
            listBoxQT.DataSource = list;
            if (listBoxQT.Items.Count > 0)
            {
                listBoxQT.SelectedIndex = 0;
                listBoxQT.SelectedIndex = old_index;
            }
            listBoxQT.Update();
            listBoxQT.Refresh();

            //Console.WriteLine("fillAutogeneratorTab:" + );
        }

        private void AGInit()
        {
            btnAGOpenedChange.Click += (sender, e) => { onBtnAGChangeClick(sender, e, lbAGOpened, true, "opened");};
            btnAGOnTestChange.Click += (sender, e) => { onBtnAGChangeClick(sender, e, lbAGOnTest, true, "on_test"); };
            btnAGClosedChange.Click += (sender, e) => { onBtnAGChangeClick(sender, e, lbAGClosed, true, "closed"); };

            btnAGOpened2Change.Click += (sender, e) => { onBtnAGChangeClick(sender, e, lbAGOpened2, false, "opened"); };
            btnAGOnTest2Change.Click += (sender, e) => { onBtnAGChangeClick(sender, e, lbAGOnTest2, false, "on_test"); };
            btnAGClosed2Change.Click += (sender, e) => { onBtnAGChangeClick(sender, e, lbAGClosed2, false, "closed"); };

            btnAGOpenedAdd.Click += (sender, e) => { onBtnAGAddClick(sender, e, lbAGOpened, true, "opened"); };
            btnAGOnTestAdd.Click += (sender, e) => { onBtnAGAddClick(sender, e, lbAGOnTest, true, "on_test"); };
            btnAGClosedAdd.Click += (sender, e) => { onBtnAGAddClick(sender, e, lbAGClosed, true, "closed"); };

            btnAGOpened2Add.Click += (sender, e) => { onBtnAGAddClick(sender, e, lbAGOpened2, false, "opened"); };
            btnAGOnTest2Add.Click += (sender, e) => { onBtnAGAddClick(sender, e, lbAGOnTest2, false, "on_test"); };
            btnAGClosed2Add.Click += (sender, e) => { onBtnAGAddClick(sender, e, lbAGClosed2, false, "closed"); };

            btnAGOpenedDel.Click += (sender, e) => { onBtnAGDelClick(sender, e, lbAGOpened, true, "opened"); };
            btnAGOnTestDel.Click += (sender, e) => { onBtnAGDelClick(sender, e, lbAGOnTest, true, "on_test"); };
            btnAGClosedDel.Click += (sender, e) => { onBtnAGDelClick(sender, e, lbAGClosed, true, "closed"); };

            btnAGOpened2Del.Click += (sender, e) => { onBtnAGDelClick(sender, e, lbAGOpened2, false, "opened"); };
            btnAGOnTest2Del.Click += (sender, e) => { onBtnAGDelClick(sender, e, lbAGOnTest2, false, "on_test"); };
            btnAGClosed2Del.Click += (sender, e) => { onBtnAGDelClick(sender, e, lbAGClosed2, false, "closed"); };

            btnAGAcceptQuestChange.Click += (sender, e)  => { onBtnAGNChangeClick(sender, e, lbAGAcceptQuest, "yes"); };
            btnAGAcceptQuestAdd.Click += (sender, e)     => { onBtnAGNAddClick(sender, e, lbAGAcceptQuest, "yes"); };
            btnAGAcceptQuestDel.Click += (sender, e)     => { onBtnAGNDelClick(sender, e, lbAGAcceptQuest, "yes"); };

            btnAGDeclineQuestChange.Click += (sender, e) => { onBtnAGNChangeClick(sender, e, lbAGDeclineQuest, "no"); };
            btnAGDeclineQuestAdd.Click += (sender, e) => { onBtnAGNAddClick(sender, e, lbAGDeclineQuest, "no"); };
            btnAGDeclineQuestDel.Click += (sender, e) => { onBtnAGNDelClick(sender, e, lbAGDeclineQuest, "no"); };

            btnAGGetQuestChange.Click += (sender, e) => { onBtnAGNChangeClick(sender, e, lbAGGetQuest, "get"); };
            btnAGGetQuestAdd.Click += (sender, e) => { onBtnAGNAddClick(sender, e, lbAGGetQuest, "get"); };
            btnAGGetQuestDel.Click += (sender, e) => { onBtnAGNDelClick(sender, e, lbAGGetQuest, "get"); };

            listBoxQT.DisplayMember = "Name";
            listBoxQT.ValueMember = "id";
            

            listBoxTarget.DisplayMember = "Name";
            listBoxTarget.ValueMember = "id";
            fillAutogeneratorTab();
        }

        private void onBtnAGChangeClick(object sender, EventArgs e, ListBox list, bool is_title, string type_dialog)
        {

            if (list.SelectedItem == null)
            {
                MessageBox.Show("Не выделен диалог для изменения");
                return;
            }
            string text = list.SelectedItem.ToString();
            int nature_id = NPCAdditionalData.getNatureByName(cbNPCNature.SelectedItem.ToString());

            DialogLocal tmp = null;
            List<DialogLocal> dialogs;
            if (is_title)
                dialogs = QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].titles;
            else
                dialogs = QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].texts;

            foreach (var i in dialogs)
            {
                if (i.text == text) { tmp = i; break; };
            }
            if (tmp == null) return;
            Forms.InputBox input = new Forms.InputBox("Change:", text);
            DialogResult result = input.ShowDialog();
            if (result != DialogResult.OK) return;
            tmp.text = input.getResult();
            tmp.version += 1;
            isDirty = true;
            cbNPCNature_SelectedIndexChanged(sender, e);
        }

        private void onBtnAGNChangeClick(object sender, EventArgs e, ListBox list, string type_dialog)
        {
            if (list.SelectedItem == null)
            {
                MessageBox.Show("Не выделен диалог для изменения");
                return;
            }
            string text = list.SelectedItem.ToString();

            DialogLocal tmp = null;
            List<DialogLocal> dialogs;
            dialogs = QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].titles;

            foreach (var i in dialogs)
            {
                if (i.text == text) { tmp = i; break; };
            }
            if (tmp == null) return;
            Forms.InputBox input = new Forms.InputBox("Change:", text);
            DialogResult result = input.ShowDialog();
            if (result != DialogResult.OK) return;
            tmp.text = input.getResult();
            tmp.version += 1;
            isDirty = true;
            cbNPCNature_SelectedIndexChanged(sender, e);
        }

        private void onBtnAGNAddClick(object sender, EventArgs e, ListBox list, string type)
        {
            Forms.InputBox input = new Forms.InputBox("Add:", "");
            DialogResult result = input.ShowDialog();
            if (result != DialogResult.OK) return;
            isDirty = QAutogenDatacs.addDialog(0, input.getResult(), type, true);
            cbNPCNature_SelectedIndexChanged(sender, e);
        }

        private void onBtnAGNDelClick(object sender, EventArgs e, ListBox list, string type)
        {
            if (list.SelectedItem == null)
            {
                MessageBox.Show("Не выделен диалог для удаления");
                return;
            }
            string text = list.SelectedItem.ToString();
            DialogResult result = MessageBox.Show("Вы уверены, что сейчас хотите удалить фразу? [" + text + "]", "Внимание", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            int frase_id = QAutogenDatacs.getFraseIDByText(text, true);
            if (frase_id == -1)
            {
                MessageBox.Show("Ошибка удаления фразы");
                return;
            }
            isDirty = QAutogenDatacs.removeDialog(0, frase_id, type, true);
            if (!isDirty)
            {
                MessageBox.Show("Ошибка удаления фразы. Что-то пошло не так");
            }
            cbNPCNature_SelectedIndexChanged(sender, e);
        }


        private void onBtnAGAddClick(object sender, EventArgs e, ListBox list, bool is_title, string type)
        {
            if (cbNPCNature.SelectedItem == null)
            {
                MessageBox.Show("Не выбран характер");
                return;
            }
            int nature_id = NPCAdditionalData.getNatureByName(cbNPCNature.SelectedItem.ToString());
            Forms.InputBox input = new Forms.InputBox("Add:", "");
            DialogResult result = input.ShowDialog();
            if (result != DialogResult.OK) return;
            isDirty = QAutogenDatacs.addDialog(nature_id, input.getResult(), type, is_title);
            if (!isDirty)
            {
                MessageBox.Show("Ошибка добавления фразы. Что-то пошло не так");
            }
            cbNPCNature_SelectedIndexChanged(sender, e);
        }

        private void onBtnAGDelClick(object sender, EventArgs e, ListBox list, bool is_title, string type)
        {
            if (list.SelectedItem == null)
            {
                MessageBox.Show("Не выделен диалог для удаления");
                return;
            }
            string text = list.SelectedItem.ToString();
            DialogResult result = MessageBox.Show("Вы уверены, что сейчас хотите удалить фразу? [" + text + "]", "Внимание", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            int nature_id = NPCAdditionalData.getNatureByName(cbNPCNature.SelectedItem.ToString());
            int frase_id = QAutogenDatacs.getFraseIDByText(text, is_title);
            if (frase_id == -1)
            {
                MessageBox.Show("Ошибка удаления фразы");
                return;
            }
            isDirty = QAutogenDatacs.removeDialog(nature_id, frase_id, type, is_title);
            if (!isDirty)
            {
                MessageBox.Show("Ошибка удаления фразы. Что-то пошло не так");
            }
            cbNPCNature_SelectedIndexChanged(sender, e);
        }



        private void cbNPCNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var i in new List<ListBox>() { lbAGOnTest, lbAGOnTest2, lbAGClosed, lbAGClosed2, lbAGOpened, lbAGOpened2,
                                                    lbAGAcceptQuest, lbAGDeclineQuest, lbAGGetQuest})
                i.Items.Clear();

            foreach (var i in QAutogenDatacs.data_dialogs.netral.yes)
                lbAGAcceptQuest.Items.Add(QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].getTitleByID(Convert.ToInt32(i)));
            foreach (var i in QAutogenDatacs.data_dialogs.netral.no)
                lbAGDeclineQuest.Items.Add(QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].getTitleByID(Convert.ToInt32(i)));
            foreach (var i in QAutogenDatacs.data_dialogs.netral.work)
                lbAGGetQuest.Items.Add(QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].getTitleByID(Convert.ToInt32(i)));


            int nature_id = NPCAdditionalData.getNatureByName(cbNPCNature.SelectedItem.ToString());
            if (nature_id < 0)
                return;
            var _dialogs = QAutogenDatacs.data_dialogs.get_nature(nature_id);

            

            foreach (var i in _dialogs.dialogs_ontest.texts)
                lbAGOnTest2.Items.Add(QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].getTextByID(Convert.ToInt32(i)));
            foreach (var i in _dialogs.dialogs_ontest.titles)
                lbAGOnTest.Items.Add(QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].getTitleByID(Convert.ToInt32(i)));

            foreach (var i in _dialogs.dialogs_opened.texts)
                lbAGOpened2.Items.Add(QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].getTextByID(Convert.ToInt32(i)));
            foreach (var i in _dialogs.dialogs_opened.titles)
                lbAGOpened.Items.Add(QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].getTitleByID(Convert.ToInt32(i)));

            foreach (var i in _dialogs.dialogs_closed.texts)
                lbAGClosed2.Items.Add(QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].getTextByID(Convert.ToInt32(i)));
            foreach (var i in _dialogs.dialogs_closed.titles)
                lbAGClosed.Items.Add(QAutogenDatacs.locals_dialogs[CSettings.ORIGINAL_PATH].getTitleByID(Convert.ToInt32(i)));

            
        }

        private void listBoxQT_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxTarget.Items.Clear();
            if (listBoxQT.SelectedItem == null)
                return;
            int id = (listBoxQT.SelectedItem as ListBoxItem).id;
            
            // List<ListBoxItem> list = new List<ListBoxItem>();
            if (QAutogenDatacs.data_quests.ContainsKey(currentNPC))
            {
                AutogenQuestType quest_type = QAutogenDatacs.data_quests[currentNPC].getQuestTypeByID(id);
                foreach (var i in quest_type.targets)
                {
                    string name = QAutogenDatacs.locals_quests[CSettings.ORIGINAL_PATH].getTextByID(currentNPC, id, i.id);
                    listBoxTarget.Items.Add(new ListBoxItem(i.id, name));
                }
            }
            //listBoxTarget.DataSource = null;
            //listBoxTarget.DataSource = list;
            
            if (listBoxTarget.Items.Count > 0)
                listBoxTarget.SelectedIndex = 0;
            listBoxTarget.Refresh();
            listBoxTarget.Update();
            

        }



        private void btnChange_Click(object sender, EventArgs e)
        {
            if (listBoxQT.SelectedItem == null)
                return;
            int qt_id = (listBoxQT.SelectedItem as ListBoxItem).id;
            AutogenQuestType quest_type = QAutogenDatacs.data_quests[currentNPC].getQuestTypeByID(qt_id);
            int target_type = (int)listBoxTarget.SelectedValue;
            string name = QAutogenDatacs.locals_quests[CSettings.ORIGINAL_PATH].getTextByID(currentNPC, quest_type.id, target_type);
            AddListElementForm form = new AddListElementForm((ElementType)quest_type.id, quest_type.getTargetByType(target_type), name, this);
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                isDirty = QAutogenDatacs.changeQuestTarget(currentNPC, quest_type.id, target_type, form.getName());               
            }

            fillAutogeneratorTab();
        }

        private void btnAddTarget_Click(object sender, EventArgs e)
        {
            if (listBoxQT.SelectedItem == null)
                return;
            int qt_id = (listBoxQT.SelectedItem as ListBoxItem).id;
            AutogenQuestType quest_type = QAutogenDatacs.data_quests[currentNPC].getQuestTypeByID(qt_id);
            AddListElementForm form = new AddListElementForm((ElementType)quest_type.id, null, "", this);
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                var new_target = form.getData();
                var text = form.getName();
                isDirty = QAutogenDatacs.addQuestTarget(currentNPC, qt_id, text, new_target);
            }
            fillAutogeneratorTab();
        }


        private void btnDelTarget_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что сейчас хотите удалить тип цели?", "Внимание", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;
            int target_type = (listBoxTarget.SelectedItem as ListBoxItem).id;
            if (listBoxQT.SelectedItem == null)
                return;
            int qt_id = (listBoxQT.SelectedItem as ListBoxItem).id;
            isDirty = QAutogenDatacs.deleteQuestTarget(currentNPC, qt_id, target_type);
            fillAutogeneratorTab();
        }

        private void btnAddQType_Click(object sender, EventArgs e)
        {
            if (QAutogenDatacs.data_quests.ContainsKey(currentNPC) && QAutogenDatacs.data_quests[currentNPC].data.Count == QAutogenDatacs.QuestTypes.Count)
            {
                MessageBox.Show("Все возможные типы уже добавлены", "Внимание");
                return;
            }

            AutogenTarget data = new AutogenTarget();
            foreach(var i in QAutogenDatacs.QuestTypes.Keys)
                data.counts.Add(i);
            if (QAutogenDatacs.data_quests.ContainsKey(currentNPC))
                foreach (var i in QAutogenDatacs.data_quests[currentNPC].data)
                    data.counts.Remove(i.id);

            AddListElementForm form = new AddListElementForm(ElementType.None, data, "", this);
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                var new_target = form.getData();
                foreach(var i in new_target.counts)
                {
                    AutogenQuestType new_type = new AutogenQuestType();
                    new_type.id = i;
                    
                    if (!QAutogenDatacs.data_quests.ContainsKey(currentNPC))
                        QAutogenDatacs.data_quests.Add(currentNPC, new AutogenQuestData());
                    QAutogenDatacs.data_quests[currentNPC].data.Add(new_type);
                }
                isDirty = true;
            }
            fillAutogeneratorTab();
        }

        private void btnDelQType_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что сейчас хотите удалить тип квеста?", "Внимание", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;
            if (listBoxQT.SelectedItem == null)
                return;
            int qt_id = (listBoxQT.SelectedItem as ListBoxItem).id;
            AutogenQuestType quest_type = QAutogenDatacs.data_quests[currentNPC].getQuestTypeByID(qt_id);
            QAutogenDatacs.data_quests[currentNPC].data.Remove(quest_type);
            fillAutogeneratorTab();
            isDirty = true;
        }

        private void listBoxTarget_DoubleClick(object sender, EventArgs e)
        {
            this.btnChange_Click(sender, e);
        }


        private void listBoxTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxQT.SelectedItem == null)
                return;
            int qt_id = (listBoxQT.SelectedItem as ListBoxItem).id;
            AutogenQuestType quest_type = QAutogenDatacs.data_quests[currentNPC].getQuestTypeByID(qt_id);
            int target_type = (listBoxTarget.SelectedItem as ListBoxItem).id;

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
                string title = "деньги:" + i.money.ToString() + " опыт:" + i.exp.ToString();
                if (i.repGroup > 0 && i.repValue != 0)
                    title += " реп:" + fractions2.getFractionDesctByID(i.repGroup);
                listBoxReward.Items.Add(title);
            }
        }

        private void nupFromTargetCount_ValueChanged(object sender, EventArgs e)
        {
            if (listBoxQT.SelectedItem == null)
                return;
            int qt_id = (listBoxQT.SelectedItem as ListBoxItem).id;
            AutogenQuestType quest_type = QAutogenDatacs.data_quests[currentNPC].getQuestTypeByID(qt_id);
            int target_type = (listBoxTarget.SelectedItem as ListBoxItem).id;
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
            if (listBoxQT.SelectedItem == null)
                return;
            int qt_id = (listBoxQT.SelectedItem as ListBoxItem).id;
            AutogenQuestType quest_type = QAutogenDatacs.data_quests[currentNPC].getQuestTypeByID(qt_id);
            int target_type = (listBoxTarget.SelectedItem as ListBoxItem).id;

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
            AddListElementForm form = new AddListElementForm(ElementType.Reward, null, "", this);
            DialogResult result = form.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }
            if (listBoxQT.SelectedItem == null)
                return;
            int qt_id = (listBoxQT.SelectedItem as ListBoxItem).id;
            AutogenQuestType quest_type = QAutogenDatacs.data_quests[currentNPC].getQuestTypeByID(qt_id);
            int target_type = (int)listBoxTarget.SelectedValue;
            AutogenTarget t = quest_type.getTargetByType(target_type);
            isDirty = true;
            AutogenTarget a = form.getData();
            Reward reward = new Reward();
            reward.exp = a.id;
            reward.money = a.int_param;
            if (a.str_param != "" && a.str_param != null)
            {
                reward.repGroup = Convert.ToInt32(a.str_param.Split(':')[0]);
                reward.repValue = Convert.ToInt32(a.str_param.Split(':')[1]);
            }
            
            t.rewards.Add(reward);
            listBoxTarget_SelectedIndexChanged(sender, e);
        }

        private void btnDelReward_Click(object sender, EventArgs e)
        {
            if (listBoxQT.SelectedItem == null)
                return;
            int qt_id = (listBoxQT.SelectedItem as ListBoxItem).id;
            AutogenQuestType quest_type = QAutogenDatacs.data_quests[currentNPC].getQuestTypeByID(qt_id);
            int target_type = (int)listBoxTarget.SelectedValue;
            AutogenTarget t = quest_type.getTargetByType(target_type);
            t.rewards.RemoveAt(listBoxReward.SelectedIndex);
            isDirty = true;
            listBoxTarget_SelectedIndexChanged(sender, e);
        }

        private void btnChangeReward_Click(object sender, EventArgs e)
        {
            if (listBoxQT.SelectedItem == null)
                return;
            int qt_id = (listBoxQT.SelectedItem as ListBoxItem).id;
            AutogenQuestType quest_type = QAutogenDatacs.data_quests[currentNPC].getQuestTypeByID(qt_id);
            int target_type = (int)listBoxTarget.SelectedValue;
            AutogenTarget t = quest_type.getTargetByType(target_type);
            Reward reward = t.rewards[listBoxReward.SelectedIndex];

            AutogenTarget a = new AutogenTarget();
            a.id = reward.exp;
            a.int_param = reward.money;
            a.str_param = reward.repGroup.ToString() + ":" + reward.repValue.ToString();
            isDirty = true;
            AddListElementForm form = new AddListElementForm(ElementType.Reward, a, "", this);
            DialogResult result = form.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }


            a = form.getData();
            reward.exp = a.id;
            reward.money = a.int_param;
            if (a.str_param != null && a.str_param != "")
            {
                reward.repGroup = Convert.ToInt32(a.str_param.Split(':')[0]);
                reward.repValue = Convert.ToInt32(a.str_param.Split(':')[1]);
            }

            listBoxTarget_SelectedIndexChanged(sender, e);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbNPCNature_SelectedIndexChanged(sender, e);
        }

        protected class ListBoxItem
        {
            public int id;
            public string name;

            public ListBoxItem(int id, string name)
            {
                this.id = id; this.name = name;
            }

            public string Name
            {
                get
                {
                    return name;
                }
            }

            public int Id
            {

                get
                {
                    return id;
                }
            }

            public static explicit operator int(ListBoxItem x)
            { 
                return x.id;
            }

            public override string ToString()
            {
                return name;
            }
        }

    }

    
}
