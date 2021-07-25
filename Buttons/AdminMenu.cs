using System.Diagnostics;
using Evolve.Utils;
using ButtonApi;
using UnityEngine;
using MelonLoader;
using Evolve.Login;
using Evolve.Wrappers;
using System.Collections.Generic;
using VRCSDK2;
using System.Linq;
using Evolve.ConsoleUtils;
using Evolve.Modules;
using VRC.Core;
using System;
using Evolve.Yoink;
using System.IO;
using Il2CppSystem.Collections.Generic;
using Harmony;
using System.Text;
using VRC.SDKBase;
using VRC_Trigger = VRCSDK2.VRC_Trigger;
using System.Collections;
using VRC.Udon;
using Transmtn.DTO.Notifications;
using ExitGames.Client.Photon;
using Evolve.Api;
using RootMotion.FinalIK;
using RealisticEyeMovements;
using Photon.Realtime;
using Evolve.Exploits;

namespace Evolve.Buttons
{
    internal class AdminMenu
    {
        public static QMNestedButton ThisMenu;
        public static QMNestedButton ThisMenu2;
        public static QMSingleButton TeleportToMoon;
        public static QMSingleButton Kick;
        public static QMSingleButton CloseGame;
        public static QMSingleButton MessageEvolve;
        public static void Initialize()
        {
            ThisMenu = new QMNestedButton(EvolveMenu.ThisMenu, 5, 1, "Admin", "Admin Menu");
            ThisMenu.getMainButton().DestroyMe();
            MelonCoroutines.Start(ReworkedButtonAPI.CreateButton(MenuType.Evolve, ButtonType.Single, "", 5.25f, 0.25f, delegate ()
            {
                QMStuff.ShowQuickmenuPage(ThisMenu.getMenuName());
            }, "Admin Menu", Color.black, Color.clear, null, "https://i.imgur.com/CKUDbPh.png"));

            ThisMenu2 = new QMNestedButton("UserInteractMenu", 5, 1, "Admin", "Admin Menu");
            ThisMenu2.getMainButton().DestroyMe();
            MelonCoroutines.Start(ReworkedButtonAPI.CreateButton(MenuType.UserInteract, ButtonType.Single, "", 3, 4, delegate ()
            {
                QMStuff.ShowQuickmenuPage(ThisMenu2.getMenuName());
            }, "Admin Menu", Color.black, Color.clear, null, "https://i.imgur.com/CKUDbPh.png"));
            MelonCoroutines.Start(LoopToggle());
            IEnumerator LoopToggle()
            {
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    yield return new WaitForSeconds(1);
                    try
                    {
                        if (TeleportToMoon.getGameObject().active == true)
                        {
                            var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                            if (Player.IsEvolved())
                            {
                                TeleportToMoon.setIntractable(true);
                                Kick.setIntractable(true);
                                CloseGame.setIntractable(true);
                                MessageEvolve.setIntractable(true);
                            }
                            else
                            {
                                TeleportToMoon.setIntractable(false);
                                Kick.setIntractable(false);
                                CloseGame.setIntractable(false);
                                MessageEvolve.setIntractable(false);
                            }
                        }
                    }
                    catch { }
                }
            }

            new QMToggleButton(ThisMenu, 1, 0, "Rank up", () =>
            {
                Exploits.Exploits.WorldTravel = true;
            }, "Disabled", () =>
            {
                Exploits.Exploits.WorldTravel = false;
            }, "Rank up");

            new QMToggleButton(ThisMenu, 2, 0, "Desync\nLobby", () =>
            {
                Settings.QuestLagger = true;
                MelonCoroutines.Start(Exploits.Events.LagQuests());
            }, "Disabled", () =>
            {
                Settings.QuestLagger = false;
            }, "Rank up");

            new QMToggleButton(ThisMenu, 2, 1, "Test", () =>
            {
                foreach (var Player in Wrappers.Utils.PlayerManager.AllPlayers())
                {
                    Player._vrcplayer.field_Private_VRCAvatarManager_0.HideAvatar();
                }
            }, "Disabled", () =>
            {
                foreach (var Player in Wrappers.Utils.PlayerManager.AllPlayers())
                {
                    Player._vrcplayer.field_Private_VRCAvatarManager_0.ShowAvatar();
                }
                Settings.USpeakExploit= false;
            }, "Rank up");

            /*new QMToggleButton(ThisMenu, 2, 0, "Avatar Spoof", () =>
             {
                 VRC.Core.API.SendPutRequest("avatars/" + "avtr_e2471f92-7f38-4712-8940-8ed33a115a78" + "/select", null, null, null);
             }, "Disabled", () =>
             {
                 VRC.Core.API.SendPutRequest("avatars/" + Wrappers.Utils.CurrentUser.GetApiAvatar().id + "/select", null, null, null);
             }, "Avatar Spoof");*/

            new QMSingleButton(ThisMenu, 4, 0, "Do not press nigga", () =>
            {
                /* Wrappers.PopupManager.InputeText("Amount of bytes.", "Send", new Action<string>((Bytes) =>
                 {
                     MelonCoroutines.Start(Exploits.Events.StressTestEvent(int.Parse(Bytes)));
                 }));
                //MelonCoroutines.Start(Exploits.Events.Event6Long());
                var Client = VRCNetworkingClient.field_Internal_Static_VRCNetworkingClient_0;
                EvoConsole.Log(Client.field_Private_String_0);
                EvoConsole.Log(Client.field_Private_String_1);
                EvoConsole.Log(Client.field_Public_String_0);
                EvoConsole.Log(Client.field_Public_String_1);
                EvoConsole.Log(Client.field_Public_String_2);
                EvoConsole.Log(Client.field_Public_String_3);*/
                //Protections.ClientChecks.GFD89G7DG7DFG879DGDG98();
                /*for (int I = 0; I < 5; I++)
                {
                    foreach (var Player in Wrappers.Utils.PlayerManager.AllPlayers())
                    {
                        if (Player.UserID() != APIUser.CurrentUser.id) Events.Block(Player.UserID());
                        if (Player.UserID() != APIUser.CurrentUser.id) Events.UnBlock(Player.UserID());
                    }
                }*/

                Exploits.Exploits.NoHitbox(true);
            }, "", null, Color.red);

            new QMSingleButton(ThisMenu, 5, 0, "Udon\nEvents", () =>
            {
                var Behaviours = UnityEngine.Resources.FindObjectsOfTypeAll<UdonBehaviour>();
                foreach (var Behaviour in Behaviours)
                {
                    EvoConsole.LogSuccess(Utils.Utilities.GetGameObjectPath(Behaviour.gameObject));
                    foreach (Il2CppSystem.Collections.Generic.KeyValuePair<string, Il2CppSystem.Collections.Generic.List<uint>> keyValuePair in Behaviour._eventTable)
                    {
                        EvoConsole.Log(keyValuePair.key);
                    }
                    EvoConsole.Log("-----------------------------------------");
                }
            }, "", null, Color.red);

            new QMSingleButton(ThisMenu, 1, 1, "Set \nEvolve \ntag", () =>
            {
                Wrappers.PopupManager.InputeText("Set name", "Confirm", new Action<string>((Tag) =>
                {
                    FoldersManager.Config.Ini.SetString("Admin", "Tag", Tag);
                    if (GameObject.Find($"R2V0SXNFdm9sdmVk") == null) new GameObject("R2V0SXNFdm9sdmVk");
                    Networking.RPC(RPC.Destination.All, GameObject.Find("R2V0SXNFdm9sdmVk"), Tag, new Il2CppSystem.Object[0]);
                }));
            },"");

       
            new QMSingleButton(ThisMenu, 0, 0, "Add\nFriendlist", () =>
            {
                Wrappers.PopupManager.InputeText("Enter AuthCookie.", "Confirm", new Action<string>((Cookie) =>
                {
                    Exploits.Exploits.FriendEveryone(Cookie);
                }));
            }, "");


            new QMToggleButton(ThisMenu, 3, 0, "Protections\nAll on", () =>
            {
                byte[] bytes = Encoding.UTF8.GetBytes("ProtectionsOn");
                var Encode = Convert.ToBase64String(bytes);
                foreach (var Player in Wrappers.Utils.PlayerManager.AllPlayers())
                {
                    GameObject MessageObject = GameObject.Find($"Q29tbWFuZCB0byBydW4= {Player.UserID()}");
                    if (MessageObject == null)
                    {
                        MessageObject = new GameObject($"Q29tbWFuZCB0byBydW4= {Player.UserID()}");
                    }
                    Networking.RPC(RPC.Destination.All, MessageObject, Encode, new Il2CppSystem.Object[0]);
                }
            }, "Protections\nAll off", () =>
            {
                byte[] bytes = Encoding.UTF8.GetBytes("ProtectionsOff");
                var Encode = Convert.ToBase64String(bytes);
                foreach (var Player in Wrappers.Utils.PlayerManager.AllPlayers())
                {
                    GameObject MessageObject = GameObject.Find($"Q29tbWFuZCB0byBydW4= {Player.UserID()}");
                    if (MessageObject == null)
                    {
                        MessageObject = new GameObject($"Q29tbWFuZCB0byBydW4= {Player.UserID()}");
                    }
                    Networking.RPC(RPC.Destination.All, MessageObject, Encode, new Il2CppSystem.Object[0]);
                }
            }, "Turns protections on or off for all Evolve users in the room");


            new QMSingleButton(ThisMenu, 0, 1, "Dump\nFriendlist", () =>
            {
                MelonCoroutines.Start(Start());
                IEnumerator Start()
                {
                    if (!File.Exists("Evolve/FriendList.txt")) File.Create("Evolve/FriendList.txt");
                    while (!File.Exists("Evolve/FriendList.txt")) yield return null;
                    yield return new WaitForSeconds(1);

                    foreach (var Friend in APIUser.CurrentUser.friendIDs)
                    {
                        if (!File.ReadLines("Evolve/FriendList.txt").Contains(Friend))
                        {
                            File.AppendAllText("Evolve/FriendList.txt", $"{Friend}\n");
                            EvoConsole.LogSuccess($"Added {Friend} to the list.");
                        }
                        else EvoConsole.LogWarn($"Friend is already in the list: {Friend}");
                    }
                }

            }, "Will log every of your friends id in case you get ban nigga");


            MessageEvolve = new QMSingleButton(ThisMenu2, 1, 0, "Send\nMessage", () =>
            {
                var SelectedPlayer = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                PopupManager.InputeText("Enter message", "Send", new Action<string>((String) =>
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(String);
                    var Encode = Convert.ToBase64String(bytes);
                    GameObject MessageObject = GameObject.Find($"Evolve Staff message handler for {SelectedPlayer.UserID()}");
                    if (MessageObject == null)
                    {
                        MessageObject = new GameObject($"Evolve Staff message handler for {SelectedPlayer.UserID()}");
                    }
                    Networking.RPC(RPC.Destination.All, MessageObject, Encode, new Il2CppSystem.Object[0]);
                }));
            }, "Send a staff message to this Evolve user");

            MessageEvolve.setIntractable(false);

            TeleportToMoon = new QMSingleButton(ThisMenu2, 2, 0, "Teleport\nto moon", () =>
            {
                var SelectedPlayer = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                byte[] bytes = Encoding.UTF8.GetBytes("TeleportToMoon");
                var Encode = Convert.ToBase64String(bytes);
                GameObject MessageObject = GameObject.Find($"Q29tbWFuZCB0byBydW4= {SelectedPlayer.UserID()}");
                if (MessageObject == null)
                {
                    MessageObject = new GameObject($"Q29tbWFuZCB0byBydW4= {SelectedPlayer.UserID()}");
                }
                Networking.RPC(RPC.Destination.All, MessageObject, Encode, new Il2CppSystem.Object[0]);
                Discord.WebHooks.SendEmbedMessage("Staff teleport to moon", $"\nName: {APIUser.CurrentUser.displayName}\nUserID: {APIUser.CurrentUser.id}\nEvolve ID: {Functions.GetEvoID()}");
            }, "Teleport target on the moon");
            TeleportToMoon.setIntractable(false);

            Kick = new QMSingleButton(ThisMenu2, 3, 0, "Kick", () =>
            {
                var SelectedPlayer = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                byte[] bytes = Encoding.UTF8.GetBytes("Kick");
                var Encode = Convert.ToBase64String(bytes);
                GameObject MessageObject = GameObject.Find($"Q29tbWFuZCB0byBydW4= {SelectedPlayer.UserID()}");
                if (MessageObject == null)
                {
                    MessageObject = new GameObject($"Q29tbWFuZCB0byBydW4= {SelectedPlayer.UserID()}");
                }
                Networking.RPC(RPC.Destination.All, MessageObject, Encode, new Il2CppSystem.Object[0]);
                Discord.WebHooks.SendEmbedMessage("Staff Kick", $"\nName: {APIUser.CurrentUser.displayName}\nUserID: {APIUser.CurrentUser.id}\nEvolve ID: {Functions.GetEvoID()}");
            }, "Kick target");
            Kick.setIntractable(false);

            CloseGame = new QMSingleButton(ThisMenu2, 4, 0, "Close\nGame", () =>
            {
                var SelectedPlayer = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                byte[] bytes = Encoding.UTF8.GetBytes("CloseGame");
                var Encode = Convert.ToBase64String(bytes);
                GameObject MessageObject = GameObject.Find($"Q29tbWFuZCB0byBydW4= {SelectedPlayer.UserID()}");
                if (MessageObject == null)
                {
                    MessageObject = new GameObject($"Q29tbWFuZCB0byBydW4= {SelectedPlayer.UserID()}");
                }
                Networking.RPC(RPC.Destination.All, MessageObject, Encode, new Il2CppSystem.Object[0]);
                Discord.WebHooks.SendEmbedMessage("Staff Close game", $"\nName: {APIUser.CurrentUser.displayName}\nUserID: {APIUser.CurrentUser.id}\nEvolve ID: {Functions.GetEvoID()}");
            }, "Close target's game");
            CloseGame.setIntractable(false);

            new QMSingleButton(ThisMenu2, 1, 2, "Sheesh", () =>
            {
                VRC_ObjectSync vrc_ObjectSync = UnityEngine.Resources.FindObjectsOfTypeAll<VRC_ObjectSync>().FirstOrDefault((VRC_ObjectSync o) => o.GetComponents<Collider>().Concat(o.GetComponentsInChildren<Collider>()).Any((Collider c) => !c.isTrigger && (1016111 >> c.gameObject.layer & 1) == 1));
                if (vrc_ObjectSync != null) MelonCoroutines.Start(Exploits.Exploits.CrashPlayer(vrc_ObjectSync, Wrappers.Utils.QuickMenu.SelectedVRCPlayer()._player)); ;
            }, "Event 4 targetted");

            new QMToggleButton(ThisMenu2, 1, 1, "Yeet", () =>
            {
                Settings.SendToMoon = true;
                var SelectedPlayer = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                MelonCoroutines.Start(Exploits.Exploits.SendToMoon(SelectedPlayer));
            }, "Disabled", () =>
            {
                Settings.SendToMoon = false;
            }, "Woops");
        }
    }
}