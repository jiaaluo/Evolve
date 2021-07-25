using ButtonApi;
using Evolve.Api;
using Evolve.Utils;
using Evolve.Wrappers;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace Evolve.Buttons.Worlds.Interact
{
    class Murder4
    {
        public static QMSingleButton AssignMurderNetwork;
        public static QMSingleButton AssignDetectiveNetwork;
        public static QMSingleButton AssignBystanderNetwork;
        public static QMSingleButton GiveDGun;
        public static QMSingleButton GiveSGun;
        public static QMSingleButton GiveLuger;
        public static QMSingleButton GiveKnife;
        public static QMSingleButton GiveTrap;
        public static QMSingleButton GiveGrenade;
        public static bool GoldenGun = false;
        public static bool TeleportLoopBool = false;
        public static bool BlindLoopBool = false;
        public static bool KillLoopBool = false;
        public static bool AbortLoopBool = false;

        public static void Initialize()
        {
            Panels.PanelMenu(WorldMenuInteract.Murder4, 1, 0.89f, "\nGive weapons", 1.15f, 3.5f, "Give weapons");
            Panels.PanelMenu(WorldMenuInteract.Murder4, 0, 0.3f, "\nSet Role", 1.1f, 2.4F, "Set this player's role");

            AssignMurderNetwork = new QMSingleButton(WorldMenuInteract.Murder4, 0, -0.25f, "Bystander", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncAssignB", Player._player);
                }
            }, "Make him bystander");

            AssignDetectiveNetwork = new QMSingleButton(WorldMenuInteract.Murder4, 0, 0.25f, "Detective", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncAssignD", Player._player);
                }
            }, "Make him detective");

            AssignBystanderNetwork = new QMSingleButton(WorldMenuInteract.Murder4, 0, 0.75f, "Murder", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncAssignM", Player._player);
                }
            }, "Make him murder");

            AssignMurderNetwork.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            AssignDetectiveNetwork.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            AssignBystanderNetwork.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            GiveDGun = new QMSingleButton(WorldMenuInteract.Murder4, 1, -0.25f, "Dct. Gun", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var DetectiveGun = GameObject.Find("Game Logic").transform.Find("Weapons/Revolver");
                Functions.TakeOwnershipIfNecessary(DetectiveGun.gameObject);
                DetectiveGun.GetComponent<VRCPickup>().Drop();
                DetectiveGun.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);

            }, "Get the detective's gun");

            GiveSGun = new QMSingleButton(WorldMenuInteract.Murder4, 1, 0.25f, "Shotgun", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Gun = GameObject.Find("Weapons").transform.Find("Unlockables/Shotgun (0)");
                Functions.TakeOwnershipIfNecessary(Gun.gameObject);
                Gun.GetComponent<VRCPickup>().Drop();
                Gun.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);

            }, "Spawn a shotgun");

            GiveGrenade = new QMSingleButton(WorldMenuInteract.Murder4, 1, 0.75f, "Grenade", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Gun = GameObject.Find("Weapons").transform.Find("Unlockables/Frag (0)");
                Functions.TakeOwnershipIfNecessary(Gun.gameObject);
                Gun.GetComponent<VRCPickup>().Drop();
                Gun.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);

            }, "Spawn a grenade");

            GiveLuger = new QMSingleButton(WorldMenuInteract.Murder4, 1, 1.25f, "Luger", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Gun = GameObject.Find("Weapons").transform.Find("Unlockables/Luger (0)");
                Functions.TakeOwnershipIfNecessary(Gun.gameObject);
                Gun.GetComponent<VRCPickup>().Drop();
                Gun.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);

            }, "Spawn luger");

            GiveKnife = new QMSingleButton(WorldMenuInteract.Murder4, 1, 1.75f, "Knife", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Gun = GameObject.Find("Game Logic").transform.Find("Weapons/Knife (0)");
                Functions.TakeOwnershipIfNecessary(Gun.gameObject);
                Gun.GetComponent<VRCPickup>().Drop();
                Gun.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);

            }, "Spawn a knife");

            GiveTrap = new QMSingleButton(WorldMenuInteract.Murder4, 1, 2.25f, "Trap", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Gun = GameObject.Find("Game Logic").transform.Find("Weapons/Bear Trap (0)");
                Functions.TakeOwnershipIfNecessary(Gun.gameObject);
                Gun.GetComponent<VRCPickup>().Drop();
                Gun.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);

            }, "Spawn a trap");

            GiveDGun.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            GiveSGun.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            GiveKnife.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            GiveGrenade.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            GiveLuger.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            GiveTrap.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            var TPLoop = new QMToggleButton(WorldMenuInteract.Murder4, 2, 0, "Teleport\nLoop", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                List<GameObject> Behaviours = new List<GameObject>();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Behaviours.Add(Object.gameObject);
                }
                Notifications.Notify("Udon protection enabled");
                TeleportLoopBool = true;
                Settings.UdonExploitProtections = true;
                MelonCoroutines.Start(TeleportLoop());
                IEnumerator TeleportLoop()
                {
                    while (TeleportLoopBool)
                    {
                        foreach (var Behaviour in Behaviours)
                        {
                            if (Player == null) yield break;
                            else Exploits.Exploits.SendUdonRPC(Behaviour, "SyncAssignM", Player._player);
                        }
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                TeleportLoopBool = false;
            }, "Teleport all players to random places in loop");
            WorldMenuInteract.AllToggles.Add(TPLoop);

            var KillLoop = new QMToggleButton(WorldMenuInteract.Murder4, 3, 0, "Kill\nLoop", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                List<GameObject> Behaviours = new List<GameObject>();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Behaviours.Add(Object.gameObject);
                }
                Notifications.Notify("Udon protection enabled");
                Settings.UdonExploitProtections = true;
                KillLoopBool = true;
                MelonCoroutines.Start(KillLoop());
                IEnumerator KillLoop()
                {
                    while (KillLoopBool)
                    {
                        foreach (var Behaviour in Behaviours)
                        {
                            if (Player == null) yield break;
                            else Exploits.Exploits.SendUdonRPC(Behaviour, "SyncKill", Player._player);
                        }
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                KillLoopBool = false;
            }, "Kill all players in loop");
            WorldMenuInteract.AllToggles.Add(KillLoop);

            var BlindLoop = new QMToggleButton(WorldMenuInteract.Murder4, 4, 0, "Blind\nLoop", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                Notifications.Notify("Udon protection enabled");
                BlindLoopBool = true;
                Settings.UdonExploitProtections = true;
                MelonCoroutines.Start(BlindLoop());
                IEnumerator BlindLoop()
                {
                    while (BlindLoopBool)
                    {
                        if (Player == null) yield break;
                        else
                        {
                            Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnLocalPlayerFlashbanged", Player._player);
                            yield return new WaitForSeconds(0.5f);
                            Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnLocalPlayerBlinded", Player._player);
                            yield return new WaitForSeconds(0.5f);
                        }
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                BlindLoopBool = false;
            }, "Blind all players in loop");
            WorldMenuInteract.AllToggles.Add(BlindLoop);


            new QMSingleButton(WorldMenuInteract.Murder4, 2, 1, "Kill", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
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
                            Functions.TakeOwnershipIfNecessary(Knife.gameObject);
                            Knife.GetComponent<VRCPickup>().Drop();
                            Networking.RPC(RPC.Destination.All, Knife.gameObject, Knife.GetComponent<VRCPickup>().PickupEventName, new Il2CppSystem.Object[0]);
                            Networking.RPC(RPC.Destination.All, Knife.gameObject, Knife.GetComponent<VRCPickup>().UseDownEventName, new Il2CppSystem.Object[0]);
                            Knife.GetComponent<Rigidbody>().useGravity = false;
                            Knife.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);
                            yield return new WaitForSeconds(0.1f);
                        }
                        Knife.GetComponent<Rigidbody>().useGravity = true;
                        Knife.transform.position = OriginalPosition;
                        Knife.transform.rotation = OriginalRotation;
                        Networking.RPC(RPC.Destination.All, Knife.gameObject, Knife.GetComponent<VRCPickup>().UseUpEventName, new Il2CppSystem.Object[0]);
                        Networking.RPC(RPC.Destination.All, Knife.gameObject, Knife.GetComponent<VRCPickup>().DropEventName, new Il2CppSystem.Object[0]);
                    }
                }
            }, "Kill target");

            new QMSingleButton(WorldMenuInteract.Murder4, 3, 1, "Kill 2", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncKill", Player._player);
                }
            }, "Kill target");

            new QMSingleButton(WorldMenuInteract.Murder4, 2, 2, "Blind", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnLocalPlayerFlashbanged", Player._player);
            }, "Blind target");

            new QMSingleButton(WorldMenuInteract.Murder4, 3, 2, "Blind 2", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "OnLocalPlayerBlinded", Player._player);
            }, "Blind 2 target");
        }
    }
}
