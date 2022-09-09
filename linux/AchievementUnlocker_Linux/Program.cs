using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Achievement_API
{
    class Program
    {

        static void Main(string[] args)
        {
            File.WriteAllText("profile.log", String.Empty);
            Tools.AppendText($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} >> Request >> ", ConsoleColor.Green);
            AchievementUnlocker_API.PackageReader.MainForm_PackageReader();
        }
    }
}
