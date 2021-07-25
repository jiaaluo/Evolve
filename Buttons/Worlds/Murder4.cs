using ButtonApi;
using Evolve.Api;
using Evolve.AvatarList;
using Evolve.ConsoleUtils;
using Evolve.Utils;
using Evolve.Wrappers;
using MelonLoader;
using Mono.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using VRC.Networking;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Wrapper.Modules;

namespace Evolve.Buttons.Worlds
{
    class Murder4
    {
        public static QMSingleButton Single5;
        public static QMSingleButton Single6;
        public static QMSingleButton Single7;
        public static QMSingleButton Single8;
        public static QMSingleButton Single9;
        public static QMSingleButton Single10;
        public static QMSingleButton Single11;
        public static QMSingleButton Single12;
        public static QMSingleButton Single13;
        public static QMSingleButton Single14;
        public static QMSingleButton AssignMurderLocal;
        public static QMSingleButton AssignDetectiveLocal;
        public static QMSingleButton AssignBystanderLocal;
        public static QMSingleButton AssignMurderNetwork;
        public static QMSingleButton AssignDetectiveNetwork;
        public static QMSingleButton AssignBystanderNetwork;
        public static QMSingleButton KillMurder4Lobby;
        public static QMSingleButton KillMurder4Lobby2;
        public static QMToggleButton MurderGod;
        public static bool GoldenGun = false;
        public static bool TeleportLoopBool = false;
        public static bool BlindLoopBool = false;
        public static bool KillLoopBool = false;
        public static bool AbortLoopBool = false;
        public static bool ClueSoundSpawn = false;
        public static bool NoBlindKill = false;
        public static bool Miniguntoggle= false;
        public static bool IsHoldingDGun = false;
        public static bool FreeForAll = false;
        public static bool GoldenFriends = false;
        public static bool MurdererEsp = true;
        public static bool Hell = false;
        public static bool DravenModeToggle = false;
        public static bool SeeRoles = false;
        public static bool EarrapeMode = false;
        public static int Kills = 0;
        public static int Death = 0;
        public static List<GameObject> MurdersList = new List<GameObject>();


        public static IEnumerator CheckRoles()
        {
            yield return new WaitForSeconds(1);
            try
            {
                var MurdererName = "";
                var DetectiveName = "";
                var Nodes = GameObject.Find("Player List Group");
                var MurdererColor = new Color(0.5377358f, 0.1648718f, 0.1728278f);
                var DetectiveColor = new Color(0.2976544f, 0.251424f, 0.4716981f);
                Nodes.SetActive(true);
                var GameCollider = GameObject.Find("Game Area Bounds").GetComponent<BoxCollider>();
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name.Contains("Player Entry"))
                    {
                        if (Object.GetComponent<Image>().color == MurdererColor)
                        {
                            MurdererName = Object.GetComponentInChildren<Text>().text;
                        }

                        else if (Object.GetComponent<Image>().color == DetectiveColor)
                        {
                            DetectiveName = Object.GetComponentInChildren<Text>().text;
                        }
                    }
                }

                if (MurdererName.Length > 0 && DetectiveName.Length > 0)
                {
                    Notifications.Notify($"<color=red>{MurdererName}</color> is the murderer\n<color=cyan>{DetectiveName}</color> is the detective");
                    EvoVrConsole.Log(EvoVrConsole.LogsType.Info, $"<color=red>{MurdererName}</color> is the murderer, <color=cyan>{DetectiveName}</color> is the detective");
                }
                else MelonCoroutines.Start(CheckRoles());
            }
            catch { }
        }

        public static void Initialize()
        {
            Panels.PanelMenu(WorldMenu.Murder4, 1, 0.89f, "\nSpawn weapons", 1.15f, 3.5f, "Spawn weapons");
            Panels.PanelMenu(WorldMenu.Murder4, 4, 0.89f, "\nDoors", 1.15f, 3.5f, "Doors");
            Panels.PanelMenu(WorldMenu.Murder4, 0, 0.3f, "\nAssign Role", 1.1f, 2.4F, "Become whatever you want");
            Panels.PanelMenu(WorldMenu.Murder4, 5, 0.3f, "\nEveryone Roles", 1.1f, 2.4F, "Make everyone in the lobby as the same role");


            var TPLoop = new QMToggleButton(WorldMenu.Murder4Page2, 1, 0, "Teleport\nLoop", () =>
            {
                Notifications.Notify("Udon protection enabled");
                TeleportLoopBool = true;
                Settings.UdonExploitProtections = true;

                List<GameObject> Behaviours = new List<GameObject>();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Behaviours.Add(Object.gameObject);
                }

                MelonCoroutines.Start(TeleportLoop());
                IEnumerator TeleportLoop()
                {
                    while (TeleportLoopBool)
                    {
                        foreach (var Behaviour in Behaviours)
                        {
                            Exploits.Exploits.SendUdonRPC(Behaviour, "SyncAssignM");
                        }
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                TeleportLoopBool = false;
            }, "Teleport all players to random places in loop");
            WorldMenu.AllToggles.Add(TPLoop);

            var KillLoop = new QMToggleButton(WorldMenu.Murder4Page2, 2, 0, "Kill\nLoop", () =>
            {
                Notifications.Notify("Udon protection enabled");
                Settings.UdonExploitProtections = true;
                KillLoopBool = true;
                List<GameObject> Behaviours = new List<GameObject>();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Behaviours.Add(Object.gameObject);
                }

                MelonCoroutines.Start(KillLoop());
                IEnumerator KillLoop()
                {
                    while (KillLoopBool)
                    {
                        foreach (var Behaviour in Behaviours)
                        {
                            Exploits.Exploits.SendUdonRPC(Behaviour, "SyncKill");
                        }
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                KillLoopBool = false;
            }, "Kill all players in loop");
            WorldMenu.AllToggles.Add(KillLoop);

            var BlindLoop = new QMToggleButton(WorldMenu.Murder4Page2, 3, 0, "Blind\nLoop", () =>
            {
                Notifications.Notify("Udon protection enabled");
                BlindLoopBool = true;
                Settings.UdonExploitProtections = true;
                MelonCoroutines.Start(BlindLoop());
                IEnumerator BlindLoop()
                {
                    var MyID = APIUser.CurrentUser.id;
                    while (BlindLoopBool)
                    {
                        Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnLocalPlayerFlashbanged");
                        yield return new WaitForSeconds(0.5f);
                        Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnLocalPlayerBlinded");
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                BlindLoopBool = false;
            }, "Blind all players in loop");
            WorldMenu.AllToggles.Add(BlindLoop);

            var AbortLoop = new QMToggleButton(WorldMenu.Murder4Page2, 4, 0, "Abort\nLoop", () =>
            {
                AbortLoopBool = true;
                var Object = GameObject.Find("Game Logic");
                MelonCoroutines.Start(AbortLoop());
                IEnumerator AbortLoop()
                {
                    var MyID = APIUser.CurrentUser.id;
                    while (AbortLoopBool)
                    {
                        Exploits.Exploits.SendUdonRPC(Object, "SyncAbort");
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                AbortLoopBool = false;
            }, "Abort the game in loop");
            WorldMenu.AllToggles.Add(AbortLoop);

            new QMSingleButton(WorldMenu.Murder4Page2, 2, 1, "Blind\nLobby", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnLocalPlayerFlashbanged");
            }, "Blind target");

            new QMSingleButton(WorldMenu.Murder4Page2, 3, 1, "Blind\nLobby 2", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnLocalPlayerBlinded");
            }, "Blind target");

            new QMSingleButton(WorldMenu.Murder4Page2, 3, 2, "Disarm\nLobby", () =>
            {
                foreach (var Pickup in Exploits.Exploits.AllUdonPickups)
                {
                    if (Pickup.IsHeld) Pickup.Drop();
                }
            }, "Disarm target");

            new QMSingleButton(WorldMenu.Murder4Page2, 4, 1, "Abort Game", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "SyncAbort");
            }, "Abort Game");

            MurderGod = new QMToggleButton(WorldMenu.Murder4, 2, 0, "GodMode", () =>
            {
                Settings.JarGameGodMode = true;
            }, "Disabled", () =>
            {
                Settings.JarGameGodMode = false;
            }, "Will make you invinsible");
            WorldMenu.AllToggles.Add(MurderGod);

            var KnifeShield = new QMToggleButton(WorldMenu.Murder4, 3, 0, "Knife Shield", () =>
            {
                Settings.Murder4Shield = true;
            }, "Disabled", () =>
            {
                Settings.Murder4Shield = false;
            }, "Oh damn boi");
            WorldMenu.AllToggles.Add(KnifeShield);

            var GoldGun = new QMToggleButton(WorldMenu.Murder4, 2, 1, "God Shots", () =>
            {
                GoldenGun = true;
                MelonCoroutines.Start(GetSupporterGun());
                IEnumerator GetSupporterGun()
                {
                    List<VRCPickup> SyncPatron = new List<VRCPickup>()
                    {
                        GameObject.Find("Game Logic").transform.Find("Weapons/Unlockables/Luger (1)").GetComponent<VRCPickup>(),
                        GameObject.Find("Game Logic").transform.Find("Weapons/Unlockables/Luger (0)").GetComponent<VRCPickup>(),
                        GameObject.Find("Game Logic").transform.Find("Weapons/Unlockables/Shotgun (0)").GetComponent<VRCPickup>(),
                        GameObject.Find("Game Logic").transform.Find("Weapons/Revolver").GetComponent<VRCPickup>(),
                    };
                    var Object = GameObject.Find("Patreon");

                    while (GoldenGun)
                    {
                        yield return new WaitForSeconds(1);
                        foreach (var Pickup in SyncPatron)
                        {
                            if (!GoldenFriends)
                            {
                                if (Pickup.IsHeld && Networking.IsOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Pickup.gameObject))
                                {
                                    IsHoldingDGun = true;
                                    Exploits.Exploits.SendUdonRPC(Object, "CleanString");
                                    Exploits.Exploits.SendUdonRPC(Object, "GetPatronTier");
                                    Exploits.Exploits.SendUdonRPC(Pickup.gameObject, "Check");
                                    yield return null;
                                }
                                else IsHoldingDGun = false;
                            }
                            else
                            {
                                if (Pickup.IsHeld && Networking.IsOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Pickup.gameObject))
                                {
                                    IsHoldingDGun = true;
                                    Exploits.Exploits.SendUdonRPC(Object, "CleanString");
                                    Exploits.Exploits.SendUdonRPC(Object, "GetPatronTier");
                                    Exploits.Exploits.SendUdonRPC(Pickup.gameObject, "SyncPatronSkin");
                                    yield return null;
                                }
                                else IsHoldingDGun = false;

                                foreach (var Player in Wrappers.Utils.PlayerManager.AllPlayers())
                                {
                                    if (Player.prop_APIUser_0.isFriend)
                                    {
                                        if (Pickup.IsHeld && Networking.IsOwner(Player.field_Private_VRCPlayerApi_0, Pickup.gameObject))
                                        {
                                            Exploits.Exploits.SendUdonRPC(Object, "CleanString");
                                            Exploits.Exploits.SendUdonRPC(Object, "GetPatronTier");
                                            Exploits.Exploits.SendUdonRPC(Pickup.gameObject, "SyncPatronSkin");
                                            yield return null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                GoldenGun = false;
            }, "Oooooouuhh !");
            WorldMenu.AllToggles.Add(GoldGun);

            Single5 = new QMSingleButton(WorldMenu.Murder4, 1, -0.25f, "Dtc. Gun", () =>
            {
                foreach(var Pickups in Exploits.Exploits.AllUdonPickups)
                {
                    if (Pickups.name.ToLower().Contains("revolver"))
                    {
                        if (ClueSoundSpawn) Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnPlayerUnlockedClues");
                        Functions.TakeOwnershipIfNecessary(Pickups.gameObject);
                        Pickups.Drop();
                        Pickups.transform.position = new Vector3(VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.x, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.y + 1.3f, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.z);
                    }
                }
            }, "Get the detective's gun");
            Single5.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            Single6 = new QMSingleButton(WorldMenu.Murder4, 1, 0.25f, "Shotgun", () =>
            {
                foreach (var Pickups in Exploits.Exploits.AllUdonPickups)
                {
                    if (Pickups.name.ToLower().Contains("shotgun"))
                    {
                        if (ClueSoundSpawn) Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnPlayerUnlockedClues");
                        Functions.TakeOwnershipIfNecessary(Pickups.gameObject);
                        Pickups.Drop();
                        Pickups.transform.position = new Vector3(VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.x, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.y + 1.3f, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.z);
                    }
                }

            }, "Spawn a shotgun");
            Single6.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            Single7 = new QMSingleButton(WorldMenu.Murder4, 1, 0.75f, "Grenade", () =>
            {
                foreach (var Pickups in Exploits.Exploits.AllUdonPickups)
                {
                    if (Pickups.name.ToLower().Contains("frag"))
                    {
                        if (ClueSoundSpawn) Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnPlayerUnlockedClues");
                        Functions.TakeOwnershipIfNecessary(Pickups.gameObject);
                        Pickups.Drop();
                        Pickups.transform.position = new Vector3(VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.x, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.y + 1.3f, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.z);
                    }
                }

            }, "Spawn a grenade");
            Single7.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            Single8 = new QMSingleButton(WorldMenu.Murder4, 1, 1.25f, "Luger", () =>
            {
                foreach (var Pickups in Exploits.Exploits.AllUdonPickups)
                {
                    if (Pickups.name.ToLower().Contains("luger (0)"))
                    {
                        if (ClueSoundSpawn) Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnPlayerUnlockedClues");
                        Functions.TakeOwnershipIfNecessary(Pickups.gameObject);
                        Pickups.Drop();
                        Pickups.transform.position = new Vector3(VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.x, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.y + 1.3f, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.z);
                    }
                }

            }, "Spawn luger");
            Single8.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            Single9 = new QMSingleButton(WorldMenu.Murder4, 1, 1.75f, "2 Knifes", () =>
            {
                foreach (var Pickups in Exploits.Exploits.AllUdonPickups)
                {
                    if (Pickups.name.ToLower().Contains("knife (0)" )|| Pickups.name.ToLower().Contains("knife (1)"))
                    {
                        if (ClueSoundSpawn) Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnPlayerUnlockedClues");
                        Functions.TakeOwnershipIfNecessary(Pickups.gameObject);
                        Pickups.Drop();
                        Pickups.transform.position = new Vector3(VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.x, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.y + 1.3f, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.z);
                    }
                }

            }, "Spawn a knife");
            Single9.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            Single10 = new QMSingleButton(WorldMenu.Murder4, 1, 2.25f, "Trap", () =>
            {
                foreach (var Pickups in Exploits.Exploits.AllUdonPickups)
                {
                    if (Pickups.name.ToLower().Contains("bear trap (0)"))
                    {
                        if (ClueSoundSpawn) Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnPlayerUnlockedClues");
                        Functions.TakeOwnershipIfNecessary(Pickups.gameObject);
                        Pickups.GetComponent<VRCPickup>().Drop();
                        Pickups.transform.position = new Vector3(VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.x, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.y + 1.3f, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.z) + Vector3.forward;
                    }
                }

            }, "Spawn a trap");
            Single10.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            new QMSingleButton(WorldMenu.Murder4, 3, 2, "Teleport in game", () =>
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position = new Vector3(0.455f, 0.504f, 115);

            }, "Teleport you to the game's map");

            var WhosMurderer = new QMToggleButton(WorldMenu.Murder4, 2, 2, "Roles\nOversee", () =>
             {
                 Notifications.StaffNotify("Feature is currently broken");
                 SeeRoles = true;
                  MelonCoroutines.Start(CheckRoles());

            }, "Disabled", ()=>
            {
                 SeeRoles = false;
            }, "Will tell you everytimes a game start who the murderer is.");
            WorldMenu.AllToggles.Add(WhosMurderer);

            AssignBystanderLocal = new QMSingleButton(WorldMenu.Murder4, 0, -0.25f, "Bystander", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncAssignB", null, true);
                }
            }, "Become bystander");

            AssignDetectiveLocal = new QMSingleButton(WorldMenu.Murder4, 0, 0.25f, "Detective", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncAssignD", null, true);
                }
            }, "Become detective");

            AssignMurderLocal = new QMSingleButton(WorldMenu.Murder4, 0, 0.75f, "Murder", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncAssignM", null, true);
                }
            }, "Become murder");

            AssignBystanderLocal.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            AssignDetectiveLocal.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            AssignMurderLocal.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            AssignBystanderNetwork = new QMSingleButton(WorldMenu.Murder4, 5, -0.25f, "Bystander", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncAssignB");
                }
            }, "Everyone bystander");

            AssignDetectiveNetwork = new QMSingleButton(WorldMenu.Murder4, 5, 0.25f, "Detective", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncAssignD");
                }
            }, "Everyone detective");

            AssignMurderNetwork = new QMSingleButton(WorldMenu.Murder4, 5, 0.75f, "Murder", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncAssignM");
                }
            }, "Everyone murder");

            AssignBystanderNetwork.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            AssignDetectiveNetwork.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            AssignMurderNetwork.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            KillMurder4Lobby2 = new QMSingleButton(WorldMenu.Murder4, 3, 1.25f, "Kill Lobby 2", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncKill");
                }
            }, "Kill lobby 2");

            KillMurder4Lobby = new QMSingleButton(WorldMenu.Murder4, 3, 0.75f, "Kill Lobby", () =>
            {
                List<Transform> Knifes = new List<Transform>()
                {
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (0)"),
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (1)"),
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (2)"),
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (3)"),
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (4)"),
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (5)"),
                };
                foreach (var Knife in Knifes)
                {
                    MelonCoroutines.Start(Timer());

                    IEnumerator Timer()
                    {
                        Vector3 OriginalPosition = Knife.transform.position;
                        Quaternion OriginalRotation = Knife.transform.rotation;
                        for (int i = 0; i < Wrappers.Utils.PlayerManager.AllPlayers().Count; i++)
                        {
                            var Player = Wrappers.Utils.PlayerManager.AllPlayers()[i];
                            if (Player.prop_APIUser_0.id != VRCPlayer.field_Internal_Static_VRCPlayer_0.GetAPIUser().id)
                            {
                                Functions.TakeOwnershipIfNecessary(Knife.gameObject);
                                Knife.GetComponent<VRCPickup>().Drop();
                                Networking.RPC(RPC.Destination.All, Knife.gameObject, Knife.GetComponent<VRCPickup>().PickupEventName, new Il2CppSystem.Object[0]);
                                Networking.RPC(RPC.Destination.All, Knife.gameObject, Knife.GetComponent<VRCPickup>().UseDownEventName, new Il2CppSystem.Object[0]);
                                Knife.GetComponent<Rigidbody>().useGravity = false;
                                Knife.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);
                                yield return new WaitForSeconds(0.1f);
                            }
                        }
                        Knife.GetComponent<Rigidbody>().useGravity = true;
                        Knife.transform.position = OriginalPosition;
                        Knife.transform.rotation = OriginalRotation;
                        Networking.RPC(RPC.Destination.All, Knife.gameObject, Knife.GetComponent<VRCPickup>().UseUpEventName, new Il2CppSystem.Object[0]);
                        Networking.RPC(RPC.Destination.All, Knife.gameObject, Knife.GetComponent<VRCPickup>().DropEventName, new Il2CppSystem.Object[0]);
                    }
                }
            }, "Kill everyone");

            KillMurder4Lobby.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            KillMurder4Lobby2.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            Single11 = new QMSingleButton(WorldMenu.Murder4, 4, -0.25f, "Lock", () =>
            {
                List<Transform> Doors = new List<Transform>()
                {
                    GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact lock"),
                    GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact lock"),
                };
                foreach (var Door in Doors)
                {
                    Functions.TakeOwnershipIfNecessary(Door.gameObject);
                    Door.GetComponent<UdonBehaviour>().Interact();
                }
            }, "Lock doors");
            Single11.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            Single12 = new QMSingleButton(WorldMenu.Murder4, 4, 0.25f, "Unlock", () =>
            {
                List<Transform> Doors = new List<Transform>()
                {
                    GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact shove"),
                    GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact shove"),
                };
                foreach (var Door in Doors)
                {
                    Functions.TakeOwnershipIfNecessary(Door.gameObject);
                    Door.GetComponent<UdonBehaviour>().Interact();
                    Door.GetComponent<UdonBehaviour>().Interact();
                    Door.GetComponent<UdonBehaviour>().Interact();
                    Door.GetComponent<UdonBehaviour>().Interact();
                }
            }, "Lock doors");
            Single12.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            Single13 = new QMSingleButton(WorldMenu.Murder4, 4, 0.75f, "Open", () =>
            {
                List<Transform> Doors = new List<Transform>()
                {
                    GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact open"),
                    GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact open"),
                };
                foreach (var Door in Doors)
                {
                    Functions.TakeOwnershipIfNecessary(Door.gameObject);
                    Door.GetComponent<UdonBehaviour>().Interact();
                }
            }, "Open doors");
            Single13.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            Single14 = new QMSingleButton(WorldMenu.Murder4, 4, 1.25f, "Close", () =>
            {
                List<Transform> Doors = new List<Transform>()
                {
                    GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact close"),
                    GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact close"),
                };
                foreach (var Door in Doors)
                {
                    Functions.TakeOwnershipIfNecessary(Door.gameObject);
                    Door.GetComponent<UdonBehaviour>().Interact();
                }
            }, "Close doors");
            Single14.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            new QMSingleButton(WorldMenu.Murder4Page2, 1, 2, "Murderer\nwins", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "SyncVictoryM");
            }, "Murderer wins");

            new QMSingleButton(WorldMenu.Murder4Page2, 2, 2, "Bystander\nwins", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "SyncVictoryB");
            }, "Bystander wins");

            var CluesSound = new QMToggleButton(WorldMenu.Murder4Page2, 0, 0, "Clues sound\nfor spawns", () =>
            {
                ClueSoundSpawn = true;
            }, "Disabled", () =>
            {
                ClueSoundSpawn = false;
            }, "Will make a sound when spawning weapons to yourself.");
            WorldMenu.AllToggles.Add(CluesSound);

            new QMSingleButton(WorldMenu.Murder4Page2, 1, 1, "Get flash camera", () =>
            {
                foreach (var Pickup in Exploits.Exploits.AllUdonPickups)
                {
                    if (Pickup.name.ToLower().Contains("flashcamera"))
                    {
                        if (ClueSoundSpawn) Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnPlayerUnlockedClues");
                        Functions.TakeOwnershipIfNecessary(Pickup.gameObject);
                        Pickup.Drop();
                        Pickup.transform.position = new Vector3(VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.x, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.y + 1.3f, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.z);
                    }
                }
            }, "Get the flash camera");

            var NoKillBlind = new QMToggleButton(WorldMenu.Murder4Page2, 0, 1, "No blind", () =>
            {
                NoBlindKill = true;
                MelonCoroutines.Start(Loop());
                IEnumerator Loop()
                {
                    var FlashBang = GameObject.Find("Flashbang HUD Anim");
                    var BlindHub = GameObject.Find("Blind HUD Anim");
                    while (NoBlindKill)
                    {
                        if (FlashBang.active) FlashBang.SetActive(false);
                        if (BlindHub.active) BlindHub.SetActive(false);
                        yield return new WaitForEndOfFrame();
                    }
                    if (FlashBang.active) FlashBang.SetActive(true);
                    if (BlindHub.active) BlindHub.SetActive(true);
                    yield break;
                }
            }, "Disabled", () =>
            {
                NoBlindKill = false;
            }, "Will remove that annoying screen effect and the penalty when killing someone");
            WorldMenu.AllToggles.Add(NoKillBlind);

            var Minigun = new QMToggleButton(WorldMenu.Murder4Page2, 0, 2, "Minigun", () =>
            {
                Miniguntoggle = true;
                MelonCoroutines.Start(Loop());
                IEnumerator Loop()
                {
                    var DetectiveGun = GameObject.Find("Game Logic").transform.Find("Weapons/Revolver").GetComponent<VRCPickup>();
                    var Behaviour = DetectiveGun.gameObject.GetComponent<UdonBehaviour>();
                    while (Miniguntoggle)
                    {
                        yield return new WaitForSeconds(0.02f);
                        if (DetectiveGun.IsHeld && Networking.IsOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, DetectiveGun.gameObject)) Behaviour.SendCustomEvent("Fire");
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                Miniguntoggle = false;
            }, "Brrrrrrrr");
            WorldMenu.AllToggles.Add(Minigun);

            var FFA = new QMToggleButton(WorldMenu.Murder4Page2, 5, 0, "FFA", () =>
            {
                Kills = 0;
                Death = 0;
                FreeForAll = true;
                MelonCoroutines.Start(Loop());
                IEnumerator Loop()
                {
                    var LobbyZone = GameObject.Find("Lobby Area Bounds");
                    var GameZone = GameObject.Find("Game Area Bounds");
                    List<GameObject> Behaviours = new List<GameObject>();
                    var Nodes = GameObject.Find("Player Nodes");
                    foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                    {
                        if (Object.name != Nodes.name) Behaviours.Add(Object.gameObject);
                    }
                    /* var Unlockables = GameObject.Find("Unlockables");
                     foreach (var Pickup in Exploits.Exploits.AllUdonPickups)
                     {
                         if (Pickup.name == "Luger (0)") Pickup.transform.position = new Vector3(Unlockables.transform.position.x - 43.447f, Unlockables.transform.position.y + 21.335f, -11.911f);
                         if (Pickup.name == "Luger (1)") Pickup.transform.position = new Vector3(-22.047f, 21.29f, 8.845f);
                         if (Pickup.name == "Shotgun (0)") Pickup.transform.position = new Vector3(-46.129f, 24.018f, 2.624f);
                         if (Pickup.name == "Frag (0)") Pickup.transform.position = new Vector3(-36.998f, 21.041f, 24.588f);
                     }
                     EvoVrConsole.Log(EvoVrConsole.LogsType.Info, "Spawned all weapons to different positions.");*/
                    while (FreeForAll)
                    {
                        for (int I = 0; I < Wrappers.Utils.PlayerManager.AllPlayers().Count; I++)
                        {
                            var Player = Wrappers.Utils.PlayerManager.AllPlayers()[I];
                            var Distance = Vector3.Distance(Player.transform.position, GameZone.transform.position);
                            if (Distance > 100 && Distance <129)
                            {
                                if (Player.DisplayName() == APIUser.CurrentUser.displayName)
                                {
                                    Death++;
                                    EvoVrConsole.Log(EvoVrConsole.LogsType.Info, $"You died {Death} times");
                                    EvoVrConsole.Log(EvoVrConsole.LogsType.Info, $"Respawning...");
                                }
                                else EvoVrConsole.Log(EvoVrConsole.LogsType.Info, $"{Player.DisplayName()} died, respawning in a few seconds...");
                                foreach (var Behaviour in Behaviours)
                                {
                                     Exploits.Exploits.SendUdonRPC(Behaviour, $"SyncAssignM", Player);
                                }
                            }
                            yield return new WaitForSeconds(0.5f);
                        }
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "SyncAbort");
                FreeForAll = false;
            }, "A game mode where everyone kill everyone in loop");
            WorldMenu.AllToggles.Add(FFA);

         
            var DravenMode = new QMToggleButton(WorldMenu.Murder4Page2, 5, 1, "Draven\nMode", () =>
            {
                DravenModeToggle = true;
                var AllKnifes = new List<VRCPickup>()
                {
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (0)").GetComponent<VRCPickup>(),
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (1)").GetComponent<VRCPickup>(),
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (2)").GetComponent<VRCPickup>(),
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (3)").GetComponent<VRCPickup>(),
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (4)").GetComponent<VRCPickup>(),
                    GameObject.Find("Game Logic").transform.Find("Weapons/Knife (5)").GetComponent<VRCPickup>(),
                };
                MelonCoroutines.Start(Loop());
                IEnumerator Loop()
                {
                    while (DravenModeToggle)
                    {
                        yield return new WaitForSeconds(0.3f);
                        foreach (var Knife in AllKnifes)
                        {
                            if (Knife.IsHeld && Networking.IsOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Knife.gameObject))
                            {
                                Knife.gameObject.GetComponent<UdonBehaviour>().SendCustomEvent("_onPickupUseDown");
                            }
                        }
                    }
                }
            }, "Disabled", () =>
            {
                DravenModeToggle = false;
            }, "Make your knifes spin really fast when holding them.");


            var Earrape = new QMToggleButton(WorldMenu.Murder4Page2, 4, 2, "Ear rape", () =>
            {
                EarrapeMode = true;
                MelonCoroutines.Start(Loop());
                IEnumerator Loop()
                {
                    while (EarrapeMode)
                    {
                        yield return new WaitForSeconds(0.5f);
                        Exploits.Exploits.SendUdonRPC(null, "Play");
                    }
                }
            }, "Disabled", () =>
            {
                EarrapeMode = false;
            }, "Will play loud sounds.");
            WorldMenu.AllToggles.Add(Earrape);
        }
    }
}
