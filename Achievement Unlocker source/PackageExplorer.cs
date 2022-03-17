using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.AdvTree;
using X360;
using X360.IO;
using X360.Other;
using X360.STFS;
using X360.Profile;
using System.IO;
using Achievement_API;



namespace AchievementUnlocker_API
{

    public class PackageExplorer
    {

        public List<byte> PassIndex(PassCode[] xIn)
        {
            List<byte> xReturn = new List<byte>();
            for (int i = 0; i < xIn.Length; i++)
            {
                switch (xIn[i])
                {
                    case PassCode.UpDPad:
                        xReturn.Add(1);
                        break;

                    case PassCode.DownDPad:
                        xReturn.Add(2);
                        break;

                    case PassCode.LeftDPad:
                        xReturn.Add(3);
                        break;

                    case PassCode.RightDPad:
                        xReturn.Add(4);
                        break;

                    case PassCode.X:
                        xReturn.Add(5);
                        break;

                    case PassCode.Y:
                        xReturn.Add(6);
                        break;

                    case PassCode.LTrigger:
                        xReturn.Add(7);
                        break;

                    case PassCode.RTrigger:
                        xReturn.Add(8);
                        break;

                    case PassCode.LBumper:
                        xReturn.Add(9);
                        break;

                    case PassCode.RBumper:
                        xReturn.Add(10);
                        break;

                    default:
                        xReturn.Add(0);
                        break;
                }
            }
            return xReturn;
        }

        ProfilePackage xprof;
        STFSPackage xpack;
        STFSPackage xPackage
        {
            get
            {
                if (xprof != null)
                    return xprof;
                return xpack;
            }
        }
        GameGPD xLoadedGPD;
        FileEntry xLoadedEntry;
        string textBoxX4;
        int listBox3;
        int listBox6;
        int i;
        int i2;
        int i3;
        public DateTime dateTimePicker2;
        public DateTime dateTimePicker1;
        List<string> listBox2 = new List<string>();
        List<string> xItems = new List<string>();
        RSAParams KV => PackageReader.PublicKV;
        private void GetGPD()
        {
            for (i = 0; i < xItems.Count; i++)
            {
                listBox3 = i;

                if (xItems.Count > 0)
                {
                    //Tools.AppendText(string.Concat(new object[] { "<!> ", listBox3, " -> ", "Success!", "\n" }), ConsoleColor.Green);
                }

                TitlePlayedEntry x = xprof.UserGPD.TitlesPlayed[listBox3];
                string path = x.TitleID.ToString("X").ToUpper() + ".gpd";
                Tools.AppendText(string.Concat(new object[] { "<!> ", path, " -> ", "Success!", "\n" }), ConsoleColor.Green);

                FileEntry xent = xPackage.GetFile(path);
                if (xent == null)
                {
                    Tools.AppendText(string.Concat(new object[] { "<!> ", "Issue:", " -> ", "Error Could not load GPD", "\n" }), ConsoleColor.Green);
                    return;
                }

                string xOut = VariousFunctions.GetTempFileLocale();
                if (!xent.Extract(xOut))
                {
                    Tools.AppendText(string.Concat(new object[] { "<!> ", "Issue:", " -> ", "Error Extraction error GPD", "\n" }), ConsoleColor.Green);
                    return;
                }
                //Tools.AppendText(string.Concat(new object[] { "<!> ", "TempFile:", xOut, "GPD", "\n" }), ConsoleColor.Green);
                GameGPD xload = new GameGPD(xOut, x.TitleID);
                if (!xload.IsValid)
                {
                    Tools.AppendText(string.Concat(new object[] { "<!> ", "Issue:", " -> ", "Error parsing GPD", "\n" }), ConsoleColor.Green);
                    xload.Close();
                    try { VariousFunctions.DeleteFile(xOut); }
                    catch { }
                    xload = null;
                    return;
                }

                for (i2 = 0; i2 < xload.Achievements.Length; i2++)
                {
                    listBox6 = i2;
                    Tools.AppendText(string.Concat(new object[] { "<!> ", "Achievement Number", " -> ", listBox6, "\n" }), ConsoleColor.Green);

                    listBox2.Add(xload.Achievements[i2].Title);/*Load Title ID*/
                    xload.Achievements[i2].LockType = FlagType.UnlockedOffline;/*Set Lock Status*/
                    xload.Achievements[i2].Update();/*Update Achievement Score*/
                    Tools.AppendText(string.Concat(new object[] { "<!> ", "Locked?", " -> ", xload.Achievements[i2].LockType, "\n" }), ConsoleColor.Green);
                    Tools.AppendText(string.Concat(new object[] { "<!> ", xload.Achievements[i2].Title, " -> ", "Unlocked", "\n" }), ConsoleColor.Green);

                }
                xLoadedGPD = xload;
                xLoadedEntry = xent;
                saveGPD(null, null);/*Save GPD*/
                if (xprof.UserGPD.TitlesPlayed[i].PossibleCount != xprof.UserGPD.TitlesPlayed[i].EarnedCount && listBox2.Count > 0)
                {

                }
                string xTitleStrings = xload.GetStringByID(0x8000);
                if (xTitleStrings != null)
                    textBoxX4 = xTitleStrings;
                else textBoxX4 = "Unknown";
                Tools.AppendText(string.Concat(new object[] { "<!> ", "Total Achievements:", listBox6, " -> ", "\n" }), ConsoleColor.Green);
                Tools.AppendText(string.Concat(new object[] { "<!> ", "", " -> ", "GPD Loaded!", "\n" }), ConsoleColor.Green);

            }
            xLoadedGPD.Close();
            //xLoadedGPD = null;
        }


        public void set(ref STFSPackage x)
        {
            xpack = x;

            if (xPackage.FileNameShort.Length < 30) { }
            if (xPackage.Header.IDTransfer == TransferLock.NoTransfer) {
                Tools.AppendText(string.Concat(new object[] { "<!> ", xPackage.Header.IDTransfer, " -> ", "Profile TransferLock", "\n" }), ConsoleColor.Green);
            }               
            else
            {
                Tools.AppendText(string.Concat(new object[] { "<!> ", xPackage.Header.IDTransfer, " -> ", "Profile TransferLock", "\n" }), ConsoleColor.Green);
            }
            if (xPackage.Header.ThisType != PackageType.Profile)
            {
                Tools.AppendText(string.Concat(new object[] { "<!> ", "Not Valid", " -> ", "Profile TransferLock", "\n" }), ConsoleColor.Green);
                return;
            }
            xprof = new ProfilePackage(ref x);
            xpack = null;
            xprof.RemovePhDAT();
            if (xprof.HasValidAccount)
            {
                if (xprof.UserFile.IsLiveEnabled)
                {
                    PassCode[] xPass = xprof.UserFile.GetPassCode();
                    if (xPass.Length > 0)
                    {
                        Tools.AppendText(string.Concat(new object[] { "<!> ", xPass, " -> ", "PassCode", "\n" }), ConsoleColor.Green);
                    }
                }

            }
            if (xprof.HasDashGPD)
            {
                UserInfo xInfo = xprof.GetUserStrings();
                if (xInfo.Bio != null) {
                    Tools.AppendText(string.Concat(new object[] { "<!> ", xInfo.Bio, " -> ", "Bio", "\n" }), ConsoleColor.Green);
                }

                if (xInfo.Motto != null) {
                    Tools.AppendText(string.Concat(new object[] { "<!> ", xInfo.Motto, " -> ", "Motto", "\n" }), ConsoleColor.Green);
                }

                if (xInfo.Name != null) {
                    Tools.AppendText(string.Concat(new object[] { "<!> ", xInfo.Name, " -> ", "Name", "\n" }), ConsoleColor.Green);
                }

                if (xInfo.Location != null) {
                    Tools.AppendText(string.Concat(new object[] { "<!> ", xInfo.Location, " -> ", "Location", "\n" }), ConsoleColor.Green);
                }
                string GamerTag = xprof.UserFile.GetGamertag(); 
                string XUID = VariousFunctions.EndianConvert(BitConverter.GetBytes(xprof.UserFile.XUID)).HexString();
                Tools.AppendText(string.Concat(new object[] { "<!> ", GamerTag, " -> ", "GamerTag", "\n" }), ConsoleColor.Green);
                Tools.AppendText(string.Concat(new object[] { "<!> ", XUID, " -> ", "XUID", "\n" }), ConsoleColor.Green);
                Tools.AppendText(string.Concat(new object[] { "<!> ", xPackage.Header.SaveConsoleID, " -> ", "SaveConsoleID", "\n" }), ConsoleColor.Green);
                Tools.AppendText(string.Concat(new object[] { "<!> ", xPackage.Header.ProfileID, " -> ", "ProfileID", "\n" }), ConsoleColor.Green);
                Setting temp = xprof.UserGPD.GetSetting(GPDIDs.GCardZone);

                if (temp != null)
                {

                    GamerZone[] zones = (GamerZone[])Enum.GetValues(typeof(GamerZone));

                    if (Enum.IsDefined(typeof(GamerZone), temp.Data))
                    {
                        
                        string zoneA = VariousFunctions.EndianConvert(BitConverter.GetBytes((uint)temp.Data)).HexString();
                        if(zoneA == "0")
                            Tools.AppendText(string.Concat(new object[] { "<!> ", "Xbox", " -> ", "Zone", "\n" }), ConsoleColor.Green); 
                        if (zoneA == "1")
                            Tools.AppendText(string.Concat(new object[] { "<!> ", "Recreation", " -> ", "Zone", "\n" }), ConsoleColor.Green);
                        if (zoneA == "2")
                            Tools.AppendText(string.Concat(new object[] { "<!> ", "Pro", " -> ", "Zone", "\n" }), ConsoleColor.Green);
                        if (zoneA == "3")
                            Tools.AppendText(string.Concat(new object[] { "<!> ", "Family", " -> ", "Zone", "\n" }), ConsoleColor.Green);
                        if (zoneA == "4")
                            Tools.AppendText(string.Concat(new object[] { "<!> ", "UnderGround", " -> ", "Zone", "\n" }), ConsoleColor.Green);
                        if (zoneA == "5")
                            Tools.AppendText(string.Concat(new object[] { "<!> ", "Cheater", " -> ", "Zone", "\n" }), ConsoleColor.Green);
                        if (zoneA == "255")
                            Tools.AppendText(string.Concat(new object[] { "<!> ", "Unknown", " -> ", "Zone", "\n" }), ConsoleColor.Green);
                        
                    }
                    else
                    {

                        //GamerZone.Unknown;

                    }

                }
                temp = xprof.UserGPD.GetSetting(GPDIDs.GCardRep, SettingType.Float);

                if (temp != null)
                {

                }
                SetNmric(GPDIDs.GCardCredit);

                Tools.AppendText(string.Concat(new object[] { "<!> ", "GamerScore", " -> ", xNum, "\n" }), ConsoleColor.Green);
                SetList();
                GetGPD();
                //unlockachievments();
                Rehash();
                Tools.AppendText(string.Concat(new object[] { "<!> ", "New GamerScore", " -> ", xNum, "\n" }), ConsoleColor.Green);
                xLoadedGPD.Close();
            }
            if (!xprof.IsValidProfile)
            {
                if (xprof.HasValidAccount && !xprof.HasDashGPD)
                {
                    Tools.AppendText(string.Concat(new object[] { "<!> ", "", " -> ", "Valid Profile but no GPD", "\n" }), ConsoleColor.Green);
                }
                else if (!xprof.HasValidAccount && xprof.HasDashGPD) {
                    Tools.AppendText(string.Concat(new object[] { "<!> ", "New GamerScore", " -> ", "May be a GPD but not a Profile", "\n" }), ConsoleColor.Green);
                }
                else
                {
                    Tools.AppendText(string.Concat(new object[] { "<!> ", "New GamerScore", " -> ", "Error..is this a valid profile?", "\n" }), ConsoleColor.Green);
                }
            }
        }

        void SetList()
        {         
            for (i3 = 0; i3 < xprof.UserGPD.TitlesPlayed.Length; i3++)
            {
                xItems.Add(xprof.UserGPD.TitlesPlayed[i3].Title + " (" + xprof.UserGPD.TitlesPlayed[i3].TitleID.ToString("X") + ")");
                Tools.AppendText(string.Concat(new object[] { "<!> ", xprof.UserGPD.TitlesPlayed[i3].Title, " -> ", "Titles", "\n" }), ConsoleColor.Green);
            }
            //xLoadedGPD.Close();
        }
        string xNum;
        void SetNmric(GPDIDs xID)
        {

                Setting x = xprof.UserGPD.GetSetting(xID, SettingType.UInt32);
            if (x != null)
                xNum = ((uint)x.Data).ToString();          
        }
    

        void Fixer()/*Profile Fixer*/
        {
            RSAParams x = null;
            bool strongsigned = true;
            bool strongsignedPIR = false;
            bool rehash = true;
            Tools.AppendText(string.Concat(new object[] { "<!> ", "", " -> ", "Fixing Profile", "\n" }), ConsoleColor.Green);
            try
            {
                if (strongsigned)/*we want this one*/
                    x = KV;
                else if (!strongsigned)
                    x = new RSAParams(StrongSigned.LIVE);
                else if (strongsignedPIR)
                    x = new RSAParams(StrongSigned.PIRS);
                if (rehash)
                    xPackage.FlushPackage(x);
                else xPackage.UpdateHeader(x);
                Tools.AppendText(string.Concat(new object[] { "<!> ", "", " -> ", "Rehashing Profile", "\n" }), ConsoleColor.Green);
                return;
            }
            catch (Exception ex)
            {
                Tools.AppendText(ex.Message, ConsoleColor.Red);
                return;
            }

        }
    
        private void saveGPD(object sender, EventArgs e)
        {

            try
            {
                xLoadedEntry.Replace(xLoadedGPD.GetStream());
                xprof.UserGPD.UpdateTitle(xLoadedGPD);
                xprof.SaveDash();
                SetNmric(GPDIDs.GCardCredit);
                return;
            }
            catch (Exception ex)
            {
                Tools.AppendText(ex.Message, ConsoleColor.Red);
                return;
            }

        }
        private void Rehash() {       
            Fixer();
        }


        private void unlockachievments()
        {
            if (listBox2.Count < 0 || xLoadedGPD == null)
                return;
            Tools.AppendText(string.Concat(new object[] { "<!> ", "Updating GPD Achievements", " -> ", "", "\n" }), ConsoleColor.Green);
            /*does nothing*/
        }

    }
}
