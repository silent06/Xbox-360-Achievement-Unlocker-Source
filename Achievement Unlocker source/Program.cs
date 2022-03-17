using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Achievement_API
{
    class Program
    {

        static void Main(string[] args)
        {

            //Console.BufferWidth = 70;
            //Console.SetWindowSize(60, 25);
            Server.Initialize();
            File.WriteAllText("Profile.log", String.Empty);
            Tools.AppendText($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} >> Request >> ", ConsoleColor.Green);
            AchievementUnlocker_API.PackageReader.MainForm_PackageReader();
        }
    }
}
