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
            ProcessStartInfo cmdsi = new ProcessStartInfo(@"C:\Python27\python.exe");
            cmdsi.Arguments = fileName;
            /*
            String command = @"/k C:\Python27\python.exe  " + fileName;
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
            cmdsi.Arguments = command;*/
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

        private void btnUpdateMobs_Click(object sender, EventArgs e)
        {
            string fileName = @"..\misc\developer_scripts\QuestEditorScripts\MobsParser.py";
            run_cmd(fileName);
            this.parent.mobConst = new CMobConstants();
            MessageBox.Show("Complete");
        }

        private void btnUpdateBoardQuests_Click(object sender, EventArgs e)
        {
            string fileName = @"..\misc\developer_scripts\QuestEditorScripts\QuestBoardParser.py";
            run_cmd(fileName);
            this.parent.billboardQuests = new BillboardQuests();
            MessageBox.Show("Complete");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = @"..\misc\developer_scripts\QuestEditorScripts\ObjectsParser.py";
            run_cmd(fileName);
            this.parent.triggerConst = new CTriggerConstants();
            this.parent.zoneConst = new CZoneConstants();
            this.parent.zoneMobConst = new CZoneMobConstants();
            this.parent.tpConst = new CTPConstants();
            this.parent.ManagerNPC = new CManagerNPC();
            MessageBox.Show("Complete");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fileName = @"..\misc\developer_scripts\QuestEditorScripts\DungeonParser.py";
            run_cmd(fileName);
        }
    }
}
