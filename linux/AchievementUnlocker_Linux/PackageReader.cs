using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using X360;
using X360.IO;
using X360.STFS;
using X360.FATX;
using X360.Other;
using X360.Media;
using X360.Profile;
using X360.SVOD;
using X360.GDFX;
using AchievementUnlocker_API;


namespace AchievementUnlocker_API
{
    public static class PackageReader
    {

        public static List<string> Files = new List<string>();
        public static RSAParams PublicKV;
        private static void ReadFile(string file)
        {

            try
            {
                switch (VariousFunctions.ReadFileType(file))
                {
                    case XboxFileType.STFS:
                        {

                            STFSPackage xPackage = new STFSPackage(file);
                            Achievement_API.Tools.AppendText($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} >> Reading Package >> ", ConsoleColor.Green);
                            if (!xPackage.ParseSuccess)
                                return;
                            PackageExplorer xExplorer = new PackageExplorer();
                            Achievement_API.Tools.AppendText($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} >> Setting Package >> ", ConsoleColor.Green);
                            xExplorer.set(ref xPackage);

                        }
                        break;

                    case XboxFileType.SVOD:
                        {
                            SVODPackage hd = new SVODPackage(file, null);
                            if (!hd.IsValid)
                                return;
                        }
                        break;

                    case XboxFileType.Music:
                        {
                            MusicFile xfile = new MusicFile(file);

                        }
                        break;

                    case XboxFileType.GPD:
                        {
                            GameGPD y = new GameGPD(file, 0xFFFFFFFF);
                            /*Need to add*/
                        }
                        break;

                    case XboxFileType.FATX:
                        {
                            FATXDrive xdrive = new FATXDrive(file);
                            Files.Add(file);
                        }
                        break;

                    case XboxFileType.GDF:
                        {

                        }
                        break;

                    default: return;
                }
                Files.Add(file);
                Files.Clear();
            }
            catch (Exception ex)
            {
                Achievement_API.Tools.AppendText($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} >> Failed to Read File >> ", ConsoleColor.Green);
                //Achievement_API.Tools.AppendText(ex.Message, ConsoleColor.Red);
                Process.GetCurrentProcess().Kill();
                File.Delete(file);
                return;
            }
        }

        public static void MainForm_PackageReader()
        {
            Achievement_API.Tools.AppendText($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} >> Incoming Request... >> ", ConsoleColor.Green);
            PublicKV = new RSAParams("bin/KV.bin");
            if (!PublicKV.Valid)
            {
                Achievement_API.Tools.AppendText($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} >> No KV.bin Process Killed >> ", ConsoleColor.Green);
                Process.GetCurrentProcess().Kill();
                return;
            }
            ///
            //ShellHelper.Shell("cd /var/www/html/achieve/bin/");
            //ShellHelper.Shell("rename -f 's/profile./profile.bin/' profile.");
            string rename = "start rename.bat";
            ShellHelper.Shell(rename);
            
            string file =  "profile.bin";

            ReadFile(file);

            
        }

    }
}
