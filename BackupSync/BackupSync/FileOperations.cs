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
        /// <summary>
        /// Kopira datoteka ili direktorium. Pritoa dokolku postoi takva datoteka/direktorium vo destinacijata se prezapisuvaat.
        /// </summary>
        /// <param name="source"> pateka na original.</param>   
        /// <param name="dest"> pateka na kopija.</param> 
        public static void Copy(string source, string dest)
        {//Treba da se izvrsuva vo poseben thread za da ne blokira pri pogolemi fajlovi ama nema vreme
                if (System.IO.Directory.Exists(source))
                {// ako e kreiran folder
                    DirectoryCopy(source, dest, true);
                }
                else
                {//ako e kreiran fajl
                    File.Copy(source, dest, true);
                }
        }

        /// <summary>
        /// Premestuva datoteka ili direktorium. Pritoa dokolku postoi takva datoteka/direktorium vo destinacijata se prezapisuvaat.
        /// </summary>
        /// <param name="source"> pateka na original.</param>   
        /// <param name="dest"> pateka na kopija.</param> 
        public static void Move(string source, string dest)
        {//Treba da se izvrsuva vo poseben thread za da ne blokira pri pogolemi fajlovi ama nema vreme
                if (System.IO.Directory.Exists(source))
                    Directory.Move(source, dest);
                else
                    File.Move(source, dest);
        }

        public static void Delete(string target)
        {//Treba da se izvrsuva vo poseben thread za da ne blokira pri pogolemi fajlovi ama nema vreme
                if (System.IO.Directory.Exists(target))
                    Directory.Delete(target, true);
                else
                    File.Delete(target);
        }

        /// <summary>
        /// Kopira direktorium. Pritoa dokolku postoi takov direktorium vo destinacijata se prezapisuva.
        /// Prevzemena od MSDN.
        /// </summary>
        /// <param name="sourceDirName"> pateka na original.</param>   
        /// <param name="destDirName"> pateka na kopija.</param> 
        /// <param name="copySubDirs"> oznacuva dali da se povikuva funkcijata za site poddirektoriumi.</param> 
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
