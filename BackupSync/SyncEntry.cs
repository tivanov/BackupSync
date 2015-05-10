using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BackupSync
{
    [Serializable]
    public class SyncEntry
    {
        #region PROMENLIVI
        //promenlivi za detekcija na greski
        public  bool    HasError;
        public  string  ErrorMsg;
        public  bool    SourceDamaged;
        public  bool    DestDamaged;

        //promenlivi za status na kopiranje
        private bool    firstCopyDone;
        public  bool    IsCopying;
        
        //promenliva za status na nadgleduvanje
        public  bool    IsWatched;
        
        //podatoci za direktoriumite
        private string  sourceDir;
        private string  destDir;
        public  string  SourceDir         {get { return sourceDir.Remove(0, sourceDir.LastIndexOf("\\") + 1); }}
        public  string  DestDir           {get { return destDir.Remove(0, destDir.LastIndexOf("\\") + 1); }}
        public  string  SourceDirFullPath {get { return sourceDir; }}
        public  string  DestDirFullPath   {get { return destDir; }}

        //promenlivi za listata na neodamna sinhronizirani
        public Queue<String> recentSync;
        private const int MaxRecentNo = 10;

        [NonSerialized]
        private FileSystemWatcher watcher;
        [NonSerialized]
        private BackgroundWorker worker;
        #endregion


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="sourceDir"> pateka na izvornata datoteka.</param>   
        /// <param name="destDir"> pateka na rezervnata kopija.</param>   
        /// <param name="sourceDir"> oznacuva dali treba izvornata datoteka vo start da bide kopirana vo destinaciskata datoteka.</param>  
        public SyncEntry(string sourceDir, string destDir, bool shouldCopy)
        {
            this.recentSync = new Queue<string>(10);
            this.sourceDir = sourceDir;
            this.destDir = destDir;
            SetupWatcher();
            firstCopyDone = true;
            if (shouldCopy)
            {//ako treba da se kopira postavi BackgroundWorker
                firstCopyDone = false;
                worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.RunWorkerAsync();
            }
            //ako ne se osteteni pocni so nadgleduvanje
            else if (!DirsDamaged())
                StartWatching();                
        }

        #region WORKER
        /// <summary>
        /// Nastan koj se slucuva koga BackgroundWorker - ot treba da pocne so rabota
        /// </summary>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            IsCopying = true;
            FileOperations.DirectoryCopy(sourceDir, destDir, true);
        }

        /// <summary>
        /// Nastan koj se slucuva koga BackgroundWorker - ot zavrsil so rabota
        /// </summary>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsCopying = false;
            firstCopyDone = true;
            if (!DirsDamaged())
                StartWatching();
        }
        #endregion

        #region WATCHER
        /// <summary>
        /// Go podesuva FileSystemWatcher-ot da go nadgleduva izvorniot direktorium, se podesuvaat filtri i 
        /// EventHandler-i
        /// </summary>
        internal void SetupWatcher()
        {

            try
            {
                watcher = new FileSystemWatcher();
                //patekata koja treba da se nadgleduva
                watcher.Path = sourceDir;

                //postavuvanje na filtri za eventi
                watcher.IncludeSubdirectories = true;
                watcher.NotifyFilter = System.IO.NotifyFilters.DirectoryName;
                watcher.NotifyFilter = watcher.NotifyFilter | System.IO.NotifyFilters.FileName;
                watcher.NotifyFilter = watcher.NotifyFilter | System.IO.NotifyFilters.Attributes;

                //postavuvanje na event handlers
                watcher.Changed += new FileSystemEventHandler(eventChangeRaised);
                watcher.Created += new FileSystemEventHandler(eventCreateRaised);
                watcher.Deleted += new FileSystemEventHandler(eventDeleteRaised);
                watcher.Renamed += new RenamedEventHandler(eventRenameRaised);                
            }
            catch (ArgumentException ex)
            {                
                if (watcher != null)
                    watcher.Dispose();
                watcher = null;
            }
        }

        /// <summary>
        /// Go deaktivira SystemFileWatcher-ot so sto toj prestanuva da gi sledi promenite na originalniot direktorium.
        /// </summary>  
        public void StopWatching()
        {
            if (watcher != null)
            {//onevozmozuvanje na watcherot
                watcher.EnableRaisingEvents = false;                
            }
            IsWatched = false;
        }


        /// <summary>
        /// Go aktivira SystemFileWatcher-ot i pocnuva da sledi promeni na originalniot direktorium.
        /// </summary>
        public void StartWatching()
        {
                if (!DirsDamaged() && firstCopyDone)
                {//ovozmozuvanje na watcherot
                    if (watcher == null) 
                        SetupWatcher(); //kreiranje na watcherot
                    watcher.EnableRaisingEvents = true;
                    IsWatched = true;
                }
                else
                    IsWatched = false;
        }
        /// <summary>
        /// Nastan koj se slucuva koga nekoj direktorium ili datoteka vo sledeniot direktorium e preimenuvan
        /// </summary>
        private void eventRenameRaised(object sender, RenamedEventArgs e)
        {
            string source = destDir + e.OldFullPath.Replace(sourceDir, "\\");
            string dest = destDir  + e.FullPath.Replace(sourceDir, "\\");
            IsCopying = true;
            try
            {
                FileOperations.Move(source, dest);
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMsg = ex.Message;
            }
            IsCopying = false;
            addToRecent(e.FullPath.Remove(0, e.FullPath.LastIndexOf("\\") + 1));
        }

        /// <summary>
        /// Nastan koj se slucuva koga nekoj direktorium ili datoteka vo sledeniot direktorium e izbrisan
        /// </summary>
        private void eventDeleteRaised(object sender, FileSystemEventArgs e)
        {
            string target = destDir + e.FullPath.Replace(sourceDir, "\\");
            IsCopying = true;
            try
            {
                FileOperations.Delete(target);
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMsg = ex.Message;
            }
            IsCopying = false;
        }

        /// <summary>
        /// Nastan koj se slucuva koga e kreiran nekoj direktorium ili datoteka vo sledeniot direktorium
        /// </summary>
        private void eventCreateRaised(object sender, FileSystemEventArgs e)
        {
                string source = e.FullPath;
                string dest = destDir + source.Replace(sourceDir, @"\");
                IsCopying = true;
            try
            {
                FileOperations.Copy(source, dest);
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMsg = ex.Message;
            }
                IsCopying = false;
                addToRecent(e.FullPath.Remove(0, e.FullPath.LastIndexOf("\\") + 1));
        }

        /// <summary>
        /// Nastan koj se slucuva koga nekoj direktorium ili datoteka vo sledeniot direktorium e izmenet
        /// </summary>
        private void eventChangeRaised(object sender, FileSystemEventArgs e)
        {
                string source = e.FullPath;
                string dest = destDir + source.Replace(sourceDir, @"\");
                IsCopying = true;
            try
            {
                FileOperations.Copy(source, dest);
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMsg = ex.Message;
            }
                IsCopying = false;
                addToRecent(e.FullPath.Remove(0, e.FullPath.LastIndexOf("\\") + 1));
        }

        /// <summary>
        /// Dodava datoteka vo listata na posledno sinhronizirani datoteki
        /// </summary>
        /// <param name="fileName">patekata na datotekata koja treba da se dodade.</param>   
        private void addToRecent(string fileName)
        {
            if (!recentSync.Contains(fileName))
                if (recentSync.Count < MaxRecentNo)
                    recentSync.Enqueue(fileName);
                else
                {
                    recentSync.Dequeue();
                    recentSync.Enqueue(fileName);
                }
        }
        #endregion

        /// <summary>
        /// Vrakja ListViewItem reprezentacija na objektot
        /// </summary>
        public System.Windows.Forms.ListViewItem AsListViewItem()
        {
            String [] s = new String[2];
            s[0] = this.SourceDir;
            s[1] = this.DestDir;
            System.Windows.Forms.ListViewItem lvi = new System.Windows.Forms.ListViewItem(s);
            if (SourceDamaged || DestDamaged)
                lvi.BackColor = Color.Red;
            return lvi;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            SyncEntry e = obj as SyncEntry;
            if (e == null) return false;
            return this.sourceDir.Equals(e.sourceDir) && this.destDir.Equals(e.sourceDir);
        }


        /// <summary>
        /// Proveruva dali direktoriumite se osteteni/izbrisani. Gi podesuva vnatresnite promenlivi i  
        /// vrakja true dokolku eden ili dvata se osteteni/izbrisani i false inaku.
        /// </summary>
        public bool DirsDamaged()
        {
            SourceDamaged = !Directory.Exists(SourceDirFullPath);
            DestDamaged = !Directory.Exists(DestDirFullPath);
            bool dirsDamaged = SourceDamaged || DestDamaged;
            if (dirsDamaged && IsWatched) StopWatching();
            return dirsDamaged;
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {            
            if (IsWatched)
                StartWatching();
        }

        ~SyncEntry()
        {
            if (watcher != null) 
                watcher.Dispose();
            watcher = null;
        }
    }
}
