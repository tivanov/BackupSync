using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupSync
{
    class FileOperations
    {
        public static void Copy(string source, string dest)
        {
            try
            {                
                if (System.IO.Directory.Exists(source))
                {// ako e kreiran folder
                    DirectoryCopy(source, dest, true);
                }
                else
                {//ako e kreiran fajl
                    File.Copy(source, dest, true);
                }

            }
            catch (Exception ioe)
            {
                //EventLog.WriteEntry("FileSync Copy File failed: " + ioe.Message);
            }
        }
        public static void Move(string source, string dest)
        {
            try
            {
                if (System.IO.Directory.Exists(source))
                {
                    Directory.Move(source, dest);
                }
                else
                {
                    File.Move(source, dest);
                }
            }
            catch (Exception ioe)
            {
                //EventLog.WriteEntry("FileSync Rename File failed: " + ioe.Message);
            }
        }

        public static void Delete(string target)
        {
            try
            {
                if (System.IO.Directory.Exists(target))
                    Directory.Delete(target, true);
                else
                    File.Delete(target);
            }
            catch (Exception ioe)
            {
                //EventLog.WriteEntry("FileSync Delete File failed: " + ioe.Message);
            }
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

    }
}
