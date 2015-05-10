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
        private bool    firstCopyDone;
        public  bool    IsCopying;
        public  bool    SourceDamaged;
        public  bool    DestDamaged;
        public  bool    IsWatched;
        private string  sourceDir;
        private string  destDir;
        public  string  SourceDir         {get { return sourceDir.Remove(0, sourceDir.LastIndexOf("\\") + 1); }}
        public  string  DestDir           {get { return destDir.Remove(0, destDir.LastIndexOf("\\") + 1); }}
        public  string  SourceDirFullPath {get { return sourceDir; }}
        public  string  DestDirFullPath   {get { return destDir; }}

        public Queue<String> recentSync;
        private const int MaxRecentNo = 10;

        [NonSerialized]
        private FileSystemWatcher watcher;
        [NonSerialized]
        private BackgroundWorker worker;

        public SyncEntry(string sourceDir, string destDir, bool shouldCopy, bool startWatching)
        {
            this.recentSync = new Queue<string>(10);
            this.sourceDir = sourceDir;
            this.destDir = destDir;
            SetupWatcher();
            firstCopyDone = true;
            if (shouldCopy)
            {
                firstCopyDone = false;
                worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.RunWorkerAsync();
            }
            else if (startWatching && !DirsDamaged())
                StartNotifying();
                
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            IsCopying = true;
            FileOperations.DirectoryCopy(sourceDir, destDir, true);
            
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsCopying = false;
            firstCopyDone = true;
            if (!DirsDamaged())
                StartNotifying();
        }
          
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

        public void StopNotifying()
        {
            if (watcher != null)
            {//ovozmozuvanje na watcherot
                watcher.EnableRaisingEvents = false;                
            }
            IsWatched = false;
        }

        public void StartNotifying()
        {
                if (!DirsDamaged() && firstCopyDone)
                {//ovozmozuvanje na watcherot
                    if (watcher == null)
                        SetupWatcher();
                    watcher.EnableRaisingEvents = true;
                    IsWatched = true;
                }
                else
                    IsWatched = false;
        }

        private void eventRenameRaised(object sender, RenamedEventArgs e)
        {
            string source = destDir + e.OldFullPath.Replace(sourceDir, "\\");
            string dest = destDir  + e.FullPath.Replace(sourceDir, "\\");
            IsCopying = true;
            FileOperations.Move(source, dest);
            IsCopying = false;
            addToRecent(e.FullPath.Remove(0, e.FullPath.LastIndexOf("\\") + 1));
        }

        private void eventDeleteRaised(object sender, FileSystemEventArgs e)
        {
            string target = destDir + e.FullPath.Replace(sourceDir, "\\");
            IsCopying = true;
            FileOperations.Delete(target);
            IsCopying = false;
        }

        private void eventCreateRaised(object sender, FileSystemEventArgs e)
        {
                string source = e.FullPath;
                string dest = destDir + source.Replace(sourceDir, @"\");
                IsCopying = true;
                FileOperations.Copy(source, dest);
                IsCopying = false;
                addToRecent(e.FullPath.Remove(0, e.FullPath.LastIndexOf("\\") + 1));
        }       

        private void eventChangeRaised(object sender, FileSystemEventArgs e)
        {
                string source = e.FullPath;
                string dest = destDir + source.Replace(sourceDir, @"\");
                IsCopying = true;
                FileOperations.Copy(source, dest);
                IsCopying = false;
                addToRecent(e.FullPath.Remove(0, e.FullPath.LastIndexOf("\\") + 1));
        }

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

        public bool DirsDamaged()
        {
            SourceDamaged = !Directory.Exists(SourceDirFullPath);
            DestDamaged = !Directory.Exists(DestDirFullPath);
            bool dirsDamaged = SourceDamaged || DestDamaged;
            if (dirsDamaged && IsWatched) StopNotifying();
            return dirsDamaged;
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {            
            if (IsWatched)
                StartNotifying();
        }

        ~SyncEntry()
        {
            if (watcher != null) 
                watcher.Dispose();
            watcher = null;
        }
    }
}
