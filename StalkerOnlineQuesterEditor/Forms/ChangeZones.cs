using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StalkerOnlineQuesterEditor.Forms
{
    public partial class ChangeZones : Form
    {
        private string zones = "";
        private MainForm mainform;
        public ChangeZones(MainForm mainform, string zones)
        {
            InitializeComponent();
            this.mainform = mainform;
            this.zones = zones;
            fillTable();
        }

        private void  fillTable()
        {
            foreach (KeyValuePair<string, CZoneDescription> item in mainform.zoneConst.getAllZones())
                ((DataGridViewComboBoxColumn)dataGridView1.Columns[0]).Items.Add(item.Key);
            foreach(var zone in zones.Split(','))
            {
                if (!zone.Trim().Any()) continue;
                object[] row = { zone.Trim() };
                dataGridView1.Rows.Add(row);
            }
        }

        public string getZones()
        {
            zones = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string zoneName = row.Cells["Column1"].FormattedValue.ToString();
                zones += zoneName + " ";
            }
            return zones.Trim().Replace(' ', ',');
        }
    }
}
