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

namespace Evolve.Buttons.Worlds.Interact
{
    class AmongUs
    {
        public static bool GoldenGun = false;
        public static bool TeleportLoopBool = false;
        public static bool BlindLoopBool = false;
        public static bool KillLoopBool = false;
        public static bool AbortLoopBool = false;
        public static void Initialize()
        {
            Panels.PanelMenu(WorldMenuInteract.AmongUs, 0, 0.3f, "\nSet Role", 1.1f, 2.4F, "Make him whatever you want");

            var Crewmate = new QMSingleButton(WorldMenuInteract.AmongUs, 0, -0.25f, "Crewmate", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncAssignB", Player._player);
                }
            }, "Make him Crewmate");
            Crewmate.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            var Impostor= new QMSingleButton(WorldMenuInteract.AmongUs, 0, 0.25f, "Impostor", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncAssignM", Player._player);
                }
            }, "Make him Impostor");
            Impostor.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);


            var TPLoop = new QMToggleButton(WorldMenuInteract.AmongUs, 1, 0, "Teleport\nLoop", () =>
            {
                Notifications.Notify("Udon protection enabled");
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                List<GameObject> Behaviours = new List<GameObject>();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Behaviours.Add(Object.gameObject);
                }
                TeleportLoopBool = true;
                MelonCoroutines.Start(TeleportLoop());
                Settings.UdonExploitProtections = true;
                IEnumerator TeleportLoop()
                {
                    while (TeleportLoopBool)
                    {
                        foreach (var Behaviour in Behaviours)
                        {
                            Exploits.Exploits.SendUdonRPC(Behaviour, "SyncAssignM", Player._player);
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

            var KillLoop = new QMToggleButton(WorldMenuInteract.AmongUs, 2, 0, "Kill\nLoop", () =>
            {
                Notifications.Notify("Udon protection enabled");
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                List<GameObject> Behaviours = new List<GameObject>();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Behaviours.Add(Object.gameObject);
                }
                Settings.UdonExploitProtections = true;
                KillLoopBool = true;
                MelonCoroutines.Start(KillLoop());
                IEnumerator KillLoop()
                {
                    while (KillLoopBool)
                    {
                        foreach (var Behaviour in Behaviours)
                        {
                            Exploits.Exploits.SendUdonRPC(Behaviour, "SyncKill", Player._player);
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

            var KickLoop = new QMToggleButton(WorldMenuInteract.AmongUs, 3, 0, "Vote Kick\nLoop", () =>
            {
                Notifications.Notify("Udon protection enabled");
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                List<GameObject> Behaviours = new List<GameObject>();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Behaviours.Add(Object.gameObject);
                }
                BlindLoopBool = true;
                Settings.UdonExploitProtections = true;
                MelonCoroutines.Start(BlindLoop());
                IEnumerator BlindLoop()
                {
                    while (BlindLoopBool)
                    {
                        foreach (var Behaviour in Behaviours)
                        {
                            Exploits.Exploits.SendUdonRPC(Behaviour, "SyncVotedOut", Player._player);
                        }
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                BlindLoopBool = false;
            }, "Blind all players in loop");
            WorldMenuInteract.AllToggles.Add(KickLoop);

            new QMSingleButton(WorldMenuInteract.AmongUs, 1, 1, "Kill", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncKill", Player._player);
                }
            }, "Kill target");

            new QMSingleButton(WorldMenuInteract.AmongUs, 2, 1, "Vote kick", () =>
            {
                var Player = Wrappers.Utils.QuickMenu.SelectedVRCPlayer();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncVotedOut", Player._player);
                }
            }, "Vote kick target");
        }
    }
}
