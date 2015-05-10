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

namespace BackupSync
{
    public partial class AddEntryForm : Form
    {
        public SyncEntry NewEntry { get; set; }
        private Form1 parent;

        public AddEntryForm(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
        }
        

        private void odberiSource_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {
                tbSourcePath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {
                tbDestPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(tbSourcePath.Text))
                    throw new Exception("Оригиналниот директориум не постои.");
                if (!Directory.Exists(tbDestPath.Text))
                    throw new Exception("Дестинацискиот директориум не постои.");
                NewEntry = new SyncEntry(tbSourcePath.Text, tbDestPath.Text, cbCopyAll.Checked, false);
                if (parent.EntryExists(NewEntry)) 
                    throw new Exception("Директориумот кој сакате да го додадете е веќе синхронизиран на бараната локација.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIzlez_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbSourcePath_TextChanged(object sender, EventArgs e)
        {
            if (tbDestPath.Text != null && !tbDestPath.Text.Equals("") &&
                tbSourcePath.Text != null && !tbSourcePath.Text.Equals("") 
                && !tbSourcePath.Text.Equals(tbDestPath.Text, StringComparison.OrdinalIgnoreCase) &&
                tbDestPath.Text.IndexOf(tbSourcePath.Text, StringComparison.OrdinalIgnoreCase)<0)
                btnOk.Enabled = true;
            else btnOk.Enabled = false;
        }
    }
}
