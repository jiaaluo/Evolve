using Evolve.Bot;
using Evolve.Buttons;
using Evolve.ConsoleUtils;
using Evolve.Utils;
using Evolve.Wrappers;
using MelonLoader;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using UnityEngine;
using VRC.Core;

namespace Evolve.Login
{
    internal class Authorization
    {
        #region Utils
        public static string Convert(WebResponse res)
        {
            string strResponse = "";
            using (var stream = res.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                strResponse = reader.ReadToEnd();
            }

            res.Dispose();
            return strResponse;
        }

        public static string[] Array(WebResponse res)
        {
            return Convert(res).Split(Environment.NewLine.ToCharArray());
        }
        #endregion 
        #region Lists
        public static string[] GetLifeTime()
        {
            var client = WebRequest.Create("https://dl.dropboxusercontent.com/s/ijhqur4gytv879i/LifeTime?dl=0");
            var response = Array(client.GetResponse());
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            client.CachePolicy = noCachePolicy;
            ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate c, X509Chain cc, SslPolicyErrors ssl) => true;
            response = response.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return response;
        }

        public static string[] GetVIP()
        {
            var client = WebRequest.Create("https://dl.dropboxusercontent.com/s/pmg8v45stx0b2ac/VIP?dl=0");
            var response = Array(client.GetResponse());
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            client.CachePolicy = noCachePolicy;
            ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate c, X509Chain cc, SslPolicyErrors ssl) => true;
            response = response.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return response;
        }

        public static string[] GetTester()
        {
            var client = WebRequest.Create("https://dl.dropboxusercontent.com/s/eoc1f708vswwmgf/Tester?dl=0");
            var response = Array(client.GetResponse());
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            client.CachePolicy = noCachePolicy;
            ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate c, X509Chain cc, SslPolicyErrors ssl) => true;
            response = response.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return response;
        }

        public static string[] GetAdmin()
        {
            var client = WebRequest.Create("https://dl.dropboxusercontent.com/s/c9ki0zehwnb3l9h/Admin?dl=0");
            var response = Array(client.GetResponse());
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            client.CachePolicy = noCachePolicy;
            ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate c, X509Chain cc, SslPolicyErrors ssl) => true;
            response = response.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return response;
        }

        public static string[] GetCrashers()
        {
            var client = WebRequest.Create("https://dl.dropboxusercontent.com/s/7gldfr2cdftvlox/U4J3B8C34S89X5?dl=0");
            var response = Array(client.GetResponse());
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            client.CachePolicy = noCachePolicy;
            ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate c, X509Chain cc, SslPolicyErrors ssl) => true;
            response = response.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return response;
        }
        #endregion

        public static string Subscribtion;
        public static int AccessLevel;
        public static bool IsAuthorized = false;
        public static IEnumerator Check()
        {
            IsAuthorized = false;
            yield return new WaitForSeconds(1);
            while (APIUser.CurrentUser == null) yield return new WaitForEndOfFrame();
            if (!DetectedMods)
            {
                try
                {
                    EvoConsole.LogWarn("Checking for authorization...");
                    if (GetAdmin().Contains(APIUser.CurrentUser.id) && !IsAuthorized)
                    {
                        EvoConsole.LogSuccess("Authorized");
                        Subscribtion = "Admin";
                        IsAuthorized = true;
                        AccessLevel = 4;
                        AdminStuff();
                        Authorized();
                    }

                    else if (GetTester().Contains(APIUser.CurrentUser.id) && !IsAuthorized)
                    {
                        EvoConsole.LogSuccess("Authorized");
                        Subscribtion = "Tester";
                        IsAuthorized = true;
                        AccessLevel = 3;
                        TesterStuff();
                        Authorized();
                    }

                    else if (GetVIP().Contains(APIUser.CurrentUser.id) && !IsAuthorized)
                    {
                        EvoConsole.LogSuccess("Authorized");
                        Subscribtion = "VIP";
                        IsAuthorized = true;
                        AccessLevel = 2;
                        Authorized();
                    }

                    else if (GetLifeTime().Contains(APIUser.CurrentUser.id) && !IsAuthorized)
                    {
                        EvoConsole.LogSuccess("Authorized");
                        Subscribtion = "Standard";
                        IsAuthorized = true;
                        AccessLevel = 1;
                        Authorized();
                    }

                    else if (!IsAuthorized)
                    {
                        EvoConsole.LogError("Unauthorized");
                        MelonCoroutines.Start(Unauthorized());
                    }
                }
                catch {Protections.ClientChecks.O8H7S5434HV894NC7S7(true); }
            }
            else
            {
                try
                {
                    IsAuthorized = false;
                    APIUser.Logout();
                    MelonCoroutines.Start(Check());
                    Wrappers.Utils.VRCUiPopupManager.ShowAlert("Evolve Engine", $"Some mods/clients have been detected and removed please restart your game and try again.");
                    EvoConsole.LogError("Some mods/clients have been detected and removed please restart your game and try again.");
                }
                catch { Protections.ClientChecks.O8H7S5434HV894NC7S7(true); }
            }
        }

        public static bool DetectedMods = false;
        public static void Authorized()
        {
            if (IsAuthorized)
            {
                try
                {
                    TotalUsers();
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low/GeneratedID"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low/GeneratedID");
                        StreamWriter File = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low/GeneratedID/IdentificationID.txt", false);
                        File.Write(new System.Random().Next(1, 500).ToString());
                        File.Close();
                    }

                    string[] Crashers = GetCrashers();
                    foreach (var Line in Crashers)
                    {
                        if (Line.Contains("Audio")) Settings.AudioCrash = Encoding.UTF8.GetString(System.Convert.FromBase64String(Line.Split(':')[1]));
                        else if (Line.Contains("CCDIK")) Settings.CCDIK = Encoding.UTF8.GetString(System.Convert.FromBase64String(Line.Split(':')[1]));
                        else if (Line.Contains("Material")) Settings.MaterialCrash = Encoding.UTF8.GetString(System.Convert.FromBase64String(Line.Split(':')[1]));
                        else if (Line.Contains("Mesh-Poly")) Settings.MeshPolyCrash = Encoding.UTF8.GetString(System.Convert.FromBase64String(Line.Split(':')[1]));
                    }
                    Protections.ClientChecks.BlacklistedAvatars.Add(Settings.AudioCrash);
                    Protections.ClientChecks.BlacklistedAvatars.Add(Settings.CCDIK);
                    Protections.ClientChecks.BlacklistedAvatars.Add(Settings.MaterialCrash);
                    Protections.ClientChecks.BlacklistedAvatars.Add(Settings.MeshPolyCrash);
                    ColorChanger.ApplyNewColor();
                    if (!Settings.IsAdmin) Discord.WebHooks.SendEmbedMessage("Login", $"**Name:** {APIUser.CurrentUser.displayName}\n**UserID:** {APIUser.CurrentUser.id}\n**Evolve ID:** {Functions.GetEvoID()}\n**Subscription:** {Subscribtion}");
                    else Discord.WebHooks.SendEmbedMessage("Login", $"**Name:** {APIUser.CurrentUser.displayName}\n**UserID:** ????????\n**Evolve ID:** {Functions.GetEvoID()}\n**Subscription:** Admin");

                }
                catch { }
            }
            else Protections.ClientChecks.O8H7S5434HV894NC7S7(true);
        }

        public static IEnumerator Unauthorized()
        {
            yield return new WaitForSeconds(0);
            try
            {
                IsAuthorized = false;
                var EvolveID = Functions.GetEvoID();
                if (EvolveID == "Null") Discord.WebHooks.SendWarningMessage($"**Unauthorized user tried to login**\n\n**-----------------[VT]-----------------**\n**Name:** {APIUser.CurrentUser.displayName}\n**UserID:** {APIUser.CurrentUser.id}\n**VT:** {Functions.GetVT()}\n\n**-----------------[DT]-----------------**\n{Functions.GetDT()}\n\n**-----------------[Other]-----------------**\n**IA:** {Functions.GetIA()}\n**MA:** {Functions.GetMA()}\n**Evolve ID:** {Functions.GetEvoID()}");
                else Discord.WebHooks.SendWarningMessage($"**Unauthorized user tried to login**\n\n**-----------------[VT]-----------------**\n**Name:** {APIUser.CurrentUser.displayName}\n**UserID:** {APIUser.CurrentUser.id}\n**Evolve ID:** {EvolveID}");
                APIUser.Logout();
                MelonCoroutines.Start(Check());
            }
            catch { Protections.ClientChecks.O8H7S5434HV894NC7S7(true); }
        }

        public static void TesterStuff()
        {
            Settings.IsTester = true;
            LogsMenu.Logger.setActive(true);
            MelonCoroutines.Start(EvolveMenu.StaffPanelAndMenu());
            MelonCoroutines.Start(Exploits.Exploits.PenCrash());
        } 

        public static void AdminStuff()
        {
            Settings.IsAdmin = true;
            LogsMenu.Logger.setActive(true);

            MelonCoroutines.Start(EvolveMenu.StaffPanelAndMenu());
            if (Functions.GetEvoID() == "-999")
            {
                BotMenu.Initialize();
                NullInstancesBots.Initialize();
                PhotonBots.Initialize();
                Server.Initialize();
                RegistryKey Key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", true);
                EvoConsole.Log($"Product ID: {Key.GetValue("ProductId")}");
                Key.SetValue("ProductId", $"{Utilities.RandomNumbersToString(5)}-{Utilities.RandomNumbersToString(4)}-{Utilities.RandomNumbersToString(4)}-{Utilities.RandomStringWithNumbers(4)}");
                EvoConsole.Log($"Spoofed: {Key.GetValue("ProductId")}");
                Key.Close();

                RegistryKey Key3 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", true);
                EvoConsole.Log($"HWID ID: {Key3.GetValue("HwProfileGuid")}");
                Key3.SetValue("HwProfileGuid", "{" + $"{Utilities.RandomStringWithNumbers(8)}-715d-11eb-97d8-{Utilities.RandomStringWithNumbers(12)}" + "}");
                EvoConsole.Log($"Spoofed: {Key3.GetValue("HwProfileGuid")}");
                Key3.Close();
            }
        }

        public static int UsersCount = 0;
        public static void TotalUsers()
        {
            List<string> AllUsers = new List<string>();
            foreach (var String in GetLifeTime())
            {
                if (String.Contains("usr") && !AllUsers.Contains(String)) AllUsers.Add(String);
            }
            foreach (var String in GetVIP())
            {
                if (String.Contains("usr") && !AllUsers.Contains(String)) AllUsers.Add(String);
            }
            foreach (var String in GetTester())
            {
                if (String.Contains("usr") && !AllUsers.Contains(String)) AllUsers.Add(String);
            }
            foreach (var String in GetAdmin())
            {
                if (String.Contains("usr") && !AllUsers.Contains(String)) AllUsers.Add(String);
            }
            UsersCount = AllUsers.Count();
        }
    }
}