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
    public partial class MainForm : Form
    {
        private bool damagedFiles = false;
        private const short MAX_ENTRIES = 20;
        private int entriesNo;
        List<SyncEntry> syncEntries = new List<SyncEntry>();

        /// <summary>
        /// Vrsi deserijalizacija na site SyncEntry od fajlot cija pateka e zacuvana vo Properties.Resources.SyncEntries
        /// </summary>
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
                if (File.Exists(Properties.Resources.SyncEntries))
                    MessageBox.Show("Се појави грешка при вчитување на листата на синхронизирани фајлови.\n" + e.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                syncEntries = new List<SyncEntry>();
            }
        }

        /// <summary>
        /// Vrsi erijalizacija na site SyncEntry vo fajlot cija pateka e zacuvana vo Properties.Resources.SyncEntries
        /// </summary>
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

        /// <summary>
        /// Vrsi dodavanje na nov SyncEtry i dopolnitelno ja povikuva funkcijata SaveItems za da se zacuvaat site.
        /// Dopolnitelno prikazuva BaloonTip za da go izvesti korisnikot ako se kopiraat podatoci.
        /// </summary>
        /// <param name="newEntry"> Noviot SyncEntry koj treba da se dodade.</param>   
        private void AddEntry(SyncEntry newEntry)
        {
            newEntry.StartWatching();
            syncEntries.Add(newEntry);
            SaveEntries();
            lvEntries.Items.Add(newEntry.AsListViewItem());
            if (newEntry.IsCopying)
            {
                trayIcon.BalloonTipIcon = ToolTipIcon.Info;
                trayIcon.BalloonTipTitle = "BackupSync";
                trayIcon.BalloonTipText = "Податоците од последно додадениот директориум се синхронизираат.";
                trayIcon.ShowBalloonTip(4000);
            }
        }

        public MainForm()
        {
            InitializeComponent();
            LoadEntries();            
        }

        /// <summary>
        /// Se povikuva pri isklucuvanje na formata. Prikazuva dijalog za potvrda, i ako se odgovori potvrdno aplikacijata se isklucuva
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.ExitMsg, "Излез", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;
            else
                SaveEntries();            
        }
        /// <summary>
        /// Se povikuva pri dvoen klik na Tray ikonata. Ja prikazuva formata
        /// </summary>
        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }
        /// <summary>
        /// Se povikuva pri klik na 'Postauvanja' od ContextMenu-to. Ja prikazuva formata
        /// </summary>
        private void postavuvanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //prikazuvanje na site SyncEtries vo ListView kontrolata
            foreach (SyncEntry se in syncEntries)
                lvEntries.Items.Add(se.AsListViewItem());
            btnDodaj.Enabled = entriesNo < MAX_ENTRIES;
        }
        
        /// <summary>
        /// Se povikuva koga kje se klikne na kopceto Dodaj. Otvora nova forma vo koja se vnesuvaat potrebnite podatoci 
        /// za nov SyncEntry. Dopolnitelno go zgolemuva brojot na Entries i go onevozmozuva kopceto Dodaj dokolku se
        /// dostigne MAX_ENTRIES
        /// </summary>
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

        /// <summary>
        /// Proveruva dali postoi SyncEntry so daden original i rezervna kopija i vrakja true ili false soodvetno.
        /// </summary>
        /// <param name="sourceDir"> pateka na original.</param>   
        /// <param name="destDir"> pateka na kopija.</param> 
        public bool EntryExists(string sourceDir, string destDir)
        {
            if (syncEntries == null)
            {
                syncEntries = new List<SyncEntry>();
                return false;
            }
            foreach (SyncEntry se in syncEntries)
            {
                if (se.SourceDirFullPath.Equals(sourceDir) && se.DestDirFullPath.Equals(destDir))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Se povikuva koga kje se klikne na kopceto Izbrisi. Go zapira nadgleduvanjeto i go brise entry-to.
        /// Dopolnitelno go namaluva brojot na Entries i go ovozmozuva kopceto Dodaj dokolku e pomal od MAX_ENRIES
        /// </summary>
        private void btnIzbrisi_Click(object sender, EventArgs e)
        {
            if (lvEntries.SelectedItems.Count > 0)
            {
                int index = lvEntries.SelectedIndices[0];
                SyncEntry toDelete = syncEntries.ElementAt(index);
                toDelete.StopWatching();
                syncEntries.RemoveAt(index);
                lvEntries.Items.Remove(lvEntries.SelectedItems[0]);
                SaveEntries();
                btnDodaj.Enabled = entriesNo < MAX_ENTRIES;
            }
        }

        /// <summary>
        /// Se povikuva koga kje se promeni selekcijata na lvEntries
        /// </summary>
        private void lvEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvEntries.SelectedItems.Count > 0)
            {//ako ima selektirano entry stavi go vo promenliva i namesti gi text boxovite 
                SyncEntry selectedEntry = syncEntries.ElementAt(lvEntries.SelectedIndices[0]);
                tbDestFull.Text = selectedEntry.DestDirFullPath;
                tbOriginalFull.Text = selectedEntry.SourceDirFullPath;
                btnToggle.Visible = true;

                if (selectedEntry.HasError)
                {//ako ima greski prijavi
                    btnToggle.Visible = false;
                    lblStatus.Text = "Грешка";
                    lblStatus.BackColor = Color.Red;
                    lvEntries.SelectedItems[0].BackColor = Color.Red;
                    pbStatus.Image = null;
                }
                if (selectedEntry.IsWatched)
                {//ako e se vo red i direktoriumot e nadgleduvan
                    btnToggle.Text = "Не Надгледувај";
                    btnToggle.Image = BackupSync.Properties.Resources.Delete_24x24;
                    lblStatus.Text = "Се надгледува";
                    lblStatus.BackColor = Color.Green;
                    
                    if (selectedEntry.IsCopying)
                    {//ako se kopira nesto stavi ikona za kopiranje
                        pbStatus.Image = Properties.Resources.Refresh_48x48;
                        ttImage.SetToolTip(pbStatus, "Директориумите се синхронизираат.");
                    }
                    else
                    {//ako ne se kopira stavi ikona deka e sinhronizirano
                        pbStatus.Image = Properties.Resources.Check_48x48;
                        ttImage.SetToolTip(pbStatus, "Директориумите се синхронизирани.");
                    }
                }
                else if (selectedEntry.DirsDamaged())
                {//Ako direktoriumot e osteten prijavi
                    btnToggle.Visible = false;
                    lblStatus.Text = "Оштетен";
                    lblStatus.BackColor = Color.Red;
                    lvEntries.SelectedItems[0].BackColor = Color.Red;
                    pbStatus.Image = null;
                }
                else
                {//ako folderot ne se nadgleduva
                    btnToggle.Text = "Надгледуваj";
                    btnToggle.Image = BackupSync.Properties.Resources.Check_24x24;
                    lblStatus.Text = "Не се надгледува";
                    lblStatus.BackColor = Color.Orange;
                    if (selectedEntry.IsCopying)
                    {//ako se kopira prijavi, stoi samo za prvoto kopiranje koga se dodava nov SyncEntry
                        pbStatus.Image = Properties.Resources.Refresh_48x48;
                        ttImage.SetToolTip(pbStatus, "Директориумите се синхронизираат.");
                    }
                    else
                    {//ako ne se kopira trgni ja ikonata i tooltip-ot
                        pbStatus.Image = null;
                        ttImage.RemoveAll();
                    }
                    
                }
                //reload na listata na neodamna sinhronizirani
                lbRecent.Items.Clear();
                lbRecent.Items.AddRange(selectedEntry.recentSync.ToArray()); 
            }
            else
            {//inaku ne e nisto selektirano i resetiraj se
                tbDestFull.Text = "";
                tbOriginalFull.Text = "";
                btnToggle.Visible = false;
                lblStatus.BackColor = Color.WhiteSmoke;
                lbRecent.Items.Clear();
                pbStatus.Image = null;
                ttImage.RemoveAll();
            }
        }
        /// <summary>
        /// Se povikuva koga se promenuva goleminata na formata
        /// </summary>
        private void Form1_Resize(object sender, EventArgs e)
        {
            //detektiraj dali e minimizirana i ako e podesi ja ikonata vo Tray i sokrij ja formata
            if (FormWindowState.Minimized == this.WindowState)
            {
                trayIcon.Visible = true;
                trayIcon.BalloonTipIcon = ToolTipIcon.Info;
                trayIcon.BalloonTipTitle = "BackupSync";
                trayIcon.BalloonTipText = "BackupSync ги синхронизира вашите податоци во позадина.";
                trayIcon.ShowBalloonTip(4000);
                this.Hide();
            }
            //ako ne e trgni ja ikonata od Tray
            else if (FormWindowState.Normal == this.WindowState)
                trayIcon.Visible = false;
        }
        /// <summary>
        /// se povikuva pri klik na 'Sinhroniziraj gi site' od MenuStrip i vklucuva sinhronizacija na site
        /// SyncEntries od listata
        /// </summary>
        private void синхронизирајГиСитеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(SyncEntry si in syncEntries)
            {
                if (!si.DirsDamaged())
                    si.StartWatching();
            }
            lvEntries_SelectedIndexChanged(null, null);
        }
        /// <summary>
        /// se povikuva pri klik na 'Zapri sinhronizacija' od MenuStrip i isklucuva sinhronizacija na site
        /// SyncEntries od listata
        /// </summary>
        private void заприСинхронизацијаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (SyncEntry si in syncEntries)
            {
                if (!si.SourceDamaged)
                    si.StopWatching();
            }
            lvEntries_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// se povikuva pri klik na kopceto za promena na sostojbata na sledenje na originalniot direktorium
        /// </summary>
        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (lvEntries.SelectedItems.Count > 0)
            {//ako selektiraniot entry e nadgleduvan iskluci nadgleduvanje i obratno
                SyncEntry selectedEntry = syncEntries.ElementAt(lvEntries.SelectedIndices[0]);
                if (!selectedEntry.IsWatched)
                    selectedEntry.StartWatching();
                else
                    selectedEntry.StopWatching();
                lvEntries_SelectedIndexChanged(null, null);
            }
        }

        /// <summary>
        /// se povikuva pri klik na 'Izlez' od menito i ja isklucuva aplikacijata
        /// </summary>
        private void излезToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// se povikuva pri klik na 'Za programata' od 'Pomos' menito i prikazuva about tekst
        /// </summary>
        private void заПрограматаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }
        /// <summary>
        /// se povikuva na sekoja sekunda i vrsi razni proverki
        /// </summary>
        private void checkerTimer_Tick(object sender, EventArgs e)
        {
            damagedFiles = false;
            bool IsCopyingFiles = false;
            for (int i = 0; i<syncEntries.Count; i++)
            {
                if (syncEntries[i].DirsDamaged())
                {//ako e osteten napravi go so crvena pozadina
                    lvEntries.Items[i].BackColor = Color.Red;
                    damagedFiles = true;
                }
                else
                {//vo sprotivno so bela
                    lvEntries.Items[i].BackColor = Color.White;
                }
                //ako ima datoteki koi se kopiraat postavi flag
                if (syncEntries[i].IsCopying)
                    IsCopyingFiles = true;
            }
            if (ErrorBaloonTimer.Enabled != damagedFiles)
            {//Vkluci go ili iskluci go tajmerot za osteteni fajlovi
                ErrorBaloonTimer.Enabled = damagedFiles;
                if (damagedFiles)
                    ErrorBaloonTimer_Tick(null, null);
            }
            //eden reload na informaciite za da se zabelezat promeni dokolku ima
            if (lvEntries.SelectedItems.Count > 0)
                lvEntries_SelectedIndexChanged(null, null);

            if (IsCopyingFiles && trayIcon.Visible)
            {//Ako e vidliva Tray ikonata i se kopiraat fajlovi smeni ja ikonata i tekstot
                trayIcon.Icon = Properties.Resources.Sync;
                trayIcon.Text = "BackupSunc: Во тек е копирање на датотеки.";
            }
            else
            {//Inaku vrati ja na default
                trayIcon.Icon = Properties.Resources.Download;
                trayIcon.Text = "BackupSunc";
            }
            
        }
        /// <summary>
        /// se povikuva na sekoi 10 minuti ako ima osteteni fajlovi/direktoriumi i prikazuva BaloonTip
        /// za da se informira korisnikot.
        /// </summary>
        private void ErrorBaloonTimer_Tick(object sender, EventArgs e)
        {
            if (damagedFiles)
            {
                trayIcon.BalloonTipIcon = ToolTipIcon.Error;
                trayIcon.BalloonTipTitle = "Грешка";
                trayIcon.BalloonTipText = "Еден или повеќе директориуми се оштетени. Ве молиме проверете ја листата на синхронизирани директориуми.";
                trayIcon.ShowBalloonTip(10000);
                lvEntries_SelectedIndexChanged(null, null);
            }
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            if (lvEntries.SelectedItems.Count > 0)
            {
                SyncEntry selectedEntry = syncEntries.ElementAt(lvEntries.SelectedIndices[0]);
                if (selectedEntry.HasError)
                    MessageBox.Show("Се појави грешка при синхронизација на една или повеќе датотеки. Ве молиме проверете ги директориумите и додадете ја врската одново.\n" + selectedEntry.ErrorMsg, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
