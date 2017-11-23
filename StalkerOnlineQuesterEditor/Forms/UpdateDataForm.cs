using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace StalkerOnlineQuesterEditor.Forms
{
    public partial class UpdateDataForm : Form
    {
        protected MainForm parent;
        public UpdateDataForm(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void run_cmd(string fileName)
        {
            //tbOutput.Clear();
          
            String command = @"/k C:\Python27\python.exe  " + fileName;
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
            cmdsi.Arguments = command;
            Process cmd = Process.Start(cmdsi);
            cmd.WaitForExit();    

           
            /*

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\Python27\python.exe", fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p.Start();

            using (StreamReader reader = p.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    tbOutput.AppendText(result);
                }
           
          // string output = p.StandardOutput.ReadToEnd();
             p.WaitForExit();
           //tbOutput.AppendText(output);
            */
        }

        private void btnUpdateItems_Click(object sender, EventArgs e)
        {
            string fileName = @"..\misc\developer_scripts\QuestEditorScripts\ItemsParser.py";
            run_cmd(fileName);
            this.parent.itemConst = new CItemConstants();
            this.parent.itemCategories = new CItemCategories();
            MessageBox.Show("Complete");
        }

        private void btnUpdateNPCStats_Click(object sender, EventArgs e)
        {
            string fileName = @"..\misc\developer_scripts\QuestEditorScripts\NPCparser.py";
            run_cmd(fileName);
            this.parent.ManagerNPC = new CManagerNPC();
            MessageBox.Show("Complete");
        }

        private void btnUpdateTP_Click(object sender, EventArgs e)
        {
            string fileName = @"..\misc\developer_scripts\QuestEditorScripts\TeleportPointParser.py";
            run_cmd(fileName);
            this.parent.tpConst = new CTPConstants();
            MessageBox.Show("Complete");
        }

        private void btnUpdateAreas_Click(object sender, EventArgs e)
        {
            string fileName = @"..\misc\developer_scripts\QuestEditorScripts\AllAreaParser.py";
            run_cmd(fileName);
            this.parent.zoneConst = new CZoneConstants();
            this.parent.zoneMobConst = new CZoneMobConstants();
            MessageBox.Show("Complete");
        }

        private void btnUpdateTriggers_Click(object sender, EventArgs e)
        {
            string fileName = @"..\misc\developer_scripts\QuestEditorScripts\TriggersParser.py";
            run_cmd(fileName);
            this.parent.triggerConst = new CTriggerConstants();
            MessageBox.Show("Complete");
        }

        private void btnUpdateMobs_Click(object sender, EventArgs e)
        {
            string fileName = @"..\misc\developer_scripts\QuestEditorScripts\MobsParser.py";
            run_cmd(fileName);
            this.parent.mobConst = new CMobConstants();
            MessageBox.Show("Complete");
        }
    }
}
