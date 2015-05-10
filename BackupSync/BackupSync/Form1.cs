using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace BackupSync
{
    public partial class Form1 : Form
    {
        private bool damagedFiles = false;
        private const short MAX_ENTRIES = 20;
        private int entriesNo;
        List<SyncEntry> syncEntries = new List<SyncEntry>();

        private void LoadEntries()
        {
            try
            {
                using (Stream file = File.Open(Properties.Resources.SyncEntries, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter fmt = new BinaryFormatter();
                    syncEntries = (List<SyncEntry>)fmt.Deserialize(file);
                    entriesNo = syncEntries.Count;    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Се појави грешка при вчитување на листата на синхронизирани фајлови.\n" + e.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                syncEntries = new List<SyncEntry>();
            }
        }

        private void SaveEntries()
        {
            try
            {
                using (Stream file = File.Open(Properties.Resources.SyncEntries, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter fmt = new BinaryFormatter();
                    fmt.Serialize(file, syncEntries);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(Properties.Resources.SaveErrorMsg + e.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddEntry(SyncEntry newEntry)
        {
            newEntry.StartNotifying();
            syncEntries.Add(newEntry);
            SaveEntries();
            lvEntries.Items.Add(newEntry.AsListViewItem());
        }

        public Form1()
        {
            InitializeComponent();
            LoadEntries();
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.ExitMsg, "Излез", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;
            else
            {
                SaveEntries();
            }
            
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void postavuvanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void izlezToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //prikazuvanje na site SyncEtries vo ListView kontrolata
            foreach (SyncEntry se in syncEntries)
                lvEntries.Items.Add(se.AsListViewItem());
                btnDodaj.Enabled = entriesNo < MAX_ENTRIES;
        }

        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            
            AddEntryForm frm = new AddEntryForm(this);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                AddEntry(frm.NewEntry);
                entriesNo++;
                btnDodaj.Enabled = entriesNo < MAX_ENTRIES;
            }
        }

        public bool EntryExists(SyncEntry entryToCheck)
        {
            if (syncEntries == null)
            {
                syncEntries = new List<SyncEntry>();
                return false;
            }
            return syncEntries.Contains(entryToCheck);
        }

        private void btnIzbrisi_Click(object sender, EventArgs e)
        {
            if (lvEntries.SelectedItems.Count > 0)
            {
                int index = lvEntries.SelectedIndices[0];
                SyncEntry toDelete = syncEntries.ElementAt(index);
                toDelete.StopNotifying();
                syncEntries.RemoveAt(index);
                lvEntries.Items.Remove(lvEntries.SelectedItems[0]);
                SaveEntries();
                btnDodaj.Enabled = entriesNo < MAX_ENTRIES;
            }
        }

        private void lvEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvEntries.SelectedItems.Count > 0)
            {
                SyncEntry selectedEntry = syncEntries.ElementAt(lvEntries.SelectedIndices[0]);
                tbDestFull.Text = selectedEntry.DestDirFullPath;
                tbOriginalFull.Text = selectedEntry.SourceDirFullPath;

                if (selectedEntry.IsWatched)
                {
                    btnToggle.Text = "Не Надгледувај";
                    btnToggle.Image = BackupSync.Properties.Resources.Delete_24x24;
                    lblStatus.Text = "Се надгледува";
                    lblStatus.BackColor = Color.Green;
                    btnToggle.Visible = true;
                    if (selectedEntry.IsCopying)
                    {
                        pbStatus.Image = Properties.Resources.Refresh_48x48;
                        ttImage.SetToolTip(pbStatus, "Директориумите се синхронизираат.");
                    }
                    else
                    {
                        pbStatus.Image = Properties.Resources.Check_48x48;
                        ttImage.SetToolTip(pbStatus, "Директориумите се синхронизирани.");
                    }
                }
                else if (selectedEntry.DirsDamaged())
                {
                    btnToggle.Visible = false;
                    lblStatus.Text = "Оштетен";
                    lblStatus.BackColor = Color.Red;
                    lvEntries.SelectedItems[0].BackColor = Color.Red;
                    pbStatus.Image = null;
                }
                else
                {
                    btnToggle.Text = "Надгледуваj";
                    btnToggle.Image = BackupSync.Properties.Resources.Check_24x24;
                    lblStatus.Text = "Не се надгледува";
                    lblStatus.BackColor = Color.Orange;
                    btnToggle.Visible = true;
                    pbStatus.Image = null;
                }

                lbRecent.Items.Clear();
                lbRecent.Items.AddRange(selectedEntry.recentSync.ToArray());
                
                
            }
            else
            {
                tbDestFull.Text = "";
                tbOriginalFull.Text = "";
                btnToggle.Visible = false;
                lblStatus.BackColor = Color.WhiteSmoke;
                lbRecent.Items.Clear();
                pbStatus.Image = null;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                trayIcon.Visible = true;
                trayIcon.BalloonTipIcon = ToolTipIcon.Info;
                trayIcon.BalloonTipTitle = "BackupSync";
                trayIcon.BalloonTipText = "BackupSync ги синхронизира вашите податоци во позадина.";
                trayIcon.ShowBalloonTip(4000);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                trayIcon.Visible = false;
            }
        }

        private void синхронизирајГиСитеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(SyncEntry si in syncEntries)
            {
                if (!si.DirsDamaged())
                    si.StartNotifying();
            }
            lvEntries_SelectedIndexChanged(null, null);
        }

        private void заприСинхронизацијаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (SyncEntry si in syncEntries)
            {
                if (!si.SourceDamaged)
                    si.StopNotifying();
            }
            lvEntries_SelectedIndexChanged(null, null);
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (lvEntries.SelectedItems.Count > 0)
            {
                SyncEntry selectedEntry = syncEntries.ElementAt(lvEntries.SelectedIndices[0]);
                if (!selectedEntry.IsWatched)
                    selectedEntry.StartNotifying();
                else
                    selectedEntry.StopNotifying();
                lvEntries_SelectedIndexChanged(null, null);
            }
        }

        private void излезToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void заПрограматаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        private void checkerTimer_Tick(object sender, EventArgs e)
        {
            damagedFiles = false;
            for (int i = 0; i<syncEntries.Count; i++)
            {
                if (lvEntries.Items[i].BackColor.Equals(Color.Red))
                {
                    lvEntries.Items[i].BackColor = Color.White;
                }
                if (syncEntries[i].DirsDamaged())
                {
                    lvEntries.Items[i].BackColor = Color.Red;
                    damagedFiles = true;                    
                }

            }
            if (ErrorBaloonTimer.Enabled != damagedFiles)
            {
                ErrorBaloonTimer.Enabled = damagedFiles;
                if (damagedFiles)
                    ErrorBaloonTimer_Tick(null, null);
            }
            if (lvEntries.SelectedItems.Count > 0)
                lvEntries_SelectedIndexChanged(null, null);
            
        }

        private void ErrorBaloonTimer_Tick(object sender, EventArgs e)
        {
            if (damagedFiles)
            {
                trayIcon.BalloonTipIcon = ToolTipIcon.Error;
                trayIcon.BalloonTipTitle = "Грешка";
                trayIcon.BalloonTipText = "Еден или повеќе фајлови се оштетени. Ве молиме проверете ја листата на синхронизирани фајлови.";
                trayIcon.ShowBalloonTip(10000);
                lvEntries_SelectedIndexChanged(null, null);
            }
        }
    }
}
