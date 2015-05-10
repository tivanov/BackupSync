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
        private MainForm parent;

        public AddEntryForm(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }
        

        private void odberiSource_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {//postavi go tekstot na odbranata pateka
                tbSourcePath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {//postavi go tekstot na odbranata pateka
                tbDestPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {//dopolnitelni proverki za validacija
                if (!Directory.Exists(tbSourcePath.Text))
                    throw new Exception("Оригиналниот директориум не постои.");
                if (!Directory.Exists(tbDestPath.Text))
                    throw new Exception("Дестинацискиот директориум не постои.");                
                if (parent.EntryExists(tbSourcePath.Text, tbDestPath.Text)) 
                    throw new Exception("Директориумот кој сакате да го додадете е веќе синхронизиран на бараната локација.");
                
                NewEntry = new SyncEntry(tbSourcePath.Text, tbDestPath.Text, cbCopyAll.Checked);
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
            //proverki za validacija
            if (tbDestPath.Text != null && !tbDestPath.Text.Equals("") &&   //destinacijata ne e prazna
                tbSourcePath.Text != null && !tbSourcePath.Text.Equals("")  //izvorot ne e prazen
                && !tbSourcePath.Text.Equals(tbDestPath.Text, StringComparison.OrdinalIgnoreCase) &&    //izvor i destinacija ne se isti
                tbDestPath.Text.Substring(0, tbDestPath.Text.LastIndexOf("\\")).IndexOf(tbSourcePath.Text, StringComparison.OrdinalIgnoreCase)<0) //destinacija ne e poddirektorium na izvor
                btnOk.Enabled = true;
            else btnOk.Enabled = false;
        }
    }
}
