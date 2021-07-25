using Evolve.Api;
using Evolve.Buttons;
using Evolve.ConsoleUtils;
using Evolve.Exploits;
using Evolve.Module;
using Evolve.Module;
using Evolve.Modules;
using Evolve.Utils;
using Evolve.Wrappers;
using MelonLoader;
using System;
using System.Collections;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;
using static Evolve.ConsoleUtils.EvoVrConsole;

namespace Evolve.Patch
{
    internal class JoinLeft
    {
        public static float EvoCD;

        //I hate this class ngl
        public static void OnPlayerJoin(Player NewPlayer)
        {

            try
            {
                Log(LogsType.Join, NewPlayer.DisplayName());
                CustomNameplates.Update(NewPlayer._vrcplayer);

                if (Time.time >= EvoCD)
                {
                    EvoCD = Time.time + 1;
                    if (GameObject.Find($"R2V0SXNFdm9sdmVk") == null) new GameObject("R2V0SXNFdm9sdmVk");
                    if (Settings.IsAdmin && FoldersManager.Config.Ini.GetString("Admin", "Tag").Length > 0) Networking.RPC(RPC.Destination.All, GameObject.Find("R2V0SXNFdm9sdmVk"), FoldersManager.Config.Ini.GetString("Admin", "Tag"), new Il2CppSystem.Object[0]);
                    else Networking.RPC(RPC.Destination.All, GameObject.Find("R2V0SXNFdm9sdmVk"), Login.Authorization.Subscribtion, new Il2CppSystem.Object[0]);
                }
                if (Settings.NotifyFriend) if (APIUser.CurrentUser.friendIDs.Contains(NewPlayer.UserID())) Notifications.Notify($"<color=#09ff00>Friend</color>\n{NewPlayer.DisplayName()} joined.");
                if (Settings.CapsuleEsp) Esp.PlayerCapsuleEsp(NewPlayer, true);
                if (Settings.MeshEsp) Esp.PlayerMeshEsp(NewPlayer, true);

                if (Settings.Oversee)
                {
                    foreach (var Player in OverseeMenu.AllPlayers)
                    {
                        if (Player.PlayerID == NewPlayer.prop_APIUser_0.id)
                        {
                            var Type = FoldersManager.Config.Ini.GetString("Oversee", "Action");
                            if (Type == "Crash")
                            {
                                Notifications.Notify($"<color=#5500ff>Oversee</color>\nCrashing: <color=white>{NewPlayer.DisplayName()}</color>");
                                string CrashType = FoldersManager.Config.Ini.GetString("Crashers", "Type");
                                if (CrashType.Length < 1)
                                {
                                    FoldersManager.Config.Ini.SetString("Crashers", "Type", "Material");
                                    CrashType = FoldersManager.Config.Ini.GetString("Crashers", "Type");
                                }

                                if (CrashType == "Material") MelonCoroutines.Start(AvatarCrash.TargetCrash(NewPlayer._vrcplayer, Settings.MaterialCrash, 6));
                                else if (CrashType == "CCD-IK") MelonCoroutines.Start(AvatarCrash.TargetCrash(NewPlayer._vrcplayer, Settings.CCDIK, 6));
                                else if (CrashType == "Mesh-Poly") MelonCoroutines.Start(AvatarCrash.TargetCrash(NewPlayer._vrcplayer, Settings.MeshPolyCrash, 10));
                                else if (CrashType == "Audio") MelonCoroutines.Start(AvatarCrash.TargetCrash(NewPlayer._vrcplayer, Settings.AudioCrash, 15));
                                else if (CrashType == "Custom") MelonCoroutines.Start(AvatarCrash.TargetCrash(NewPlayer._vrcplayer, FoldersManager.Config.Ini.GetString("Crashers", "CustomID"), 10));
                                EvoVrConsole.Log(EvoVrConsole.LogsType.Crash, $"Crashing target with <color=white>{CrashType}</color>");
                            }
                            else if (Type == "Notify")
                            {
                                Notifications.Notify($"<color=#5500ff>Oversee</color>\n<color=white>{NewPlayer.DisplayName()}</color> has joined.");
                            }
                            else if (Type == "Leave")
                            {
                                Notifications.Notify($"<color=#5500ff>Oversee</color>\nLeaving lobby in 2 seconds");
                                EvoVrConsole.Log(LogsType.Warn, $"<color=white>{NewPlayer.DisplayName()}</color> has joined, leaving lobby...");
                                MelonCoroutines.Start(WaitAndLeave());
                                IEnumerator WaitAndLeave()
                                {
                                    yield return new WaitForSeconds(2);
                                    Functions.ForceJoin("wrld_4432ea9b-729c-46e3-8eaf-846aa0a37fdd:Evolve");
                                }
                            }
                        }
                    }
                }
            }
            catch {}
        }

        public static void OnPlayerLeft(Player player)
        {
            Log(LogsType.Left, player.GetAPIUser().displayName);
            if (Settings.NotifyFriend) if (APIUser.CurrentUser.friendIDs.Contains(player.UserID())) Notifications.Notify($"<color=#09ff00>Friend</color>\n{player.DisplayName()} left.");
        }
    }
}