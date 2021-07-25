using ButtonApi;
using Cinemachine;
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
using VRC.Core;

namespace Evolve.Buttons.Worlds
{
    class AmongUs
    {
        public static bool GoldenGun = false;
        public static bool TeleportLoopBool = false;
        public static bool BlindLoopBool = false;
        public static bool KillLoopBool = false;
        public static bool AbortLoopBool = false;
        public static QMToggleButton AmongGod;
        public static void Initialize()
        {
            Panels.PanelMenu(WorldMenu.AmongUs, 0, 0.3f, "\nAssign Role", 1.1f, 2.4F, "Become whatever you want");
            Panels.PanelMenu(WorldMenu.AmongUs, 5, 0.3f, "\nEveryone Roles", 1.1f, 2.4F, "Make everyone in the lobby as the same role");

            AmongGod = new QMToggleButton(WorldMenu.AmongUs, 1, 0, "GodMode", () =>
            {
                Settings.JarGameGodMode = true;
            }, "Disabled", () =>
            {
                Settings.JarGameGodMode = false;
            }, "Will make you invinsible");
            WorldMenu.AllToggles.Add(AmongGod);

            var AntiVK = new QMToggleButton(WorldMenu.AmongUs, 2, 0, "Anti vote kick", () =>
            {
                Settings.AmongAntiVoteOut = true;
            }, "Disabled", () =>
            {
                Settings.AmongAntiVoteOut = false;
            }, "People can't vote you out");
            WorldMenu.AllToggles.Add(AntiVK);

            new QMSingleButton(WorldMenu.AmongUs, 3, 0, "Force meeting", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "SyncEmergencyMeeting");
            }, "Force meeting");

            new QMSingleButton(WorldMenu.AmongUs, 4, 0, "Kill Lobby", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncKill");
                }
            }, "Kill Lobby");

            new QMSingleButton(WorldMenu.AmongUs, 1, 1, "Skip Vote", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "SyncVoteResultSkip");
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "SyncEndVotingPhase");
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "SyncCloseVoting");
            }, "Skip Vote");

            new QMSingleButton(WorldMenu.AmongUs, 2, 1, "Vote Kick\nLobby", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncVotedOut");
                }
            }, "Vote kick lobby");

            new QMSingleButton(WorldMenu.AmongUs, 3, 1, "Vote Kick\nID", () =>
            {
                Wrappers.PopupManager.InputeText("Enter ID, Max: 31", "Comfirm", (ID) =>
                {
                    var Nodes = GameObject.Find("Player Nodes");
                    foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                    {
                        if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, $"SyncVotedFor{ID}");
                    }
                });
             }, "Vote kick ID");

            new QMSingleButton(WorldMenu.AmongUs, 4, 1, "Abort Game", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "SyncAbort");
            }, "Abort game");

            new QMSingleButton(WorldMenu.AmongUs, 1, 2, "Report Lobby", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, $"SyncReport");
                }
            }, "Report lobby");

            new QMSingleButton(WorldMenu.AmongUs, 2, 2, "Find Body", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, $"SyncBodyFound");
                }
            }, "Find Body");

            new QMSingleButton(WorldMenu.AmongUs, 3, 2, "Crewmate wins", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "SyncVictoryB");
            }, "Crewmate wins");

            new QMSingleButton(WorldMenu.AmongUs, 4, 2, "Impostor wins", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Game Logic"), "SyncVictoryM");
            }, "Crewmate wins");

            var Crewmate = new QMSingleButton(WorldMenu.AmongUs, 0, -0.25f, "Crewmate", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, $"SyncAssignB", null, true);
                }
            }, "Become a crewmate");
            Crewmate.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            var Impostor = new QMSingleButton(WorldMenu.AmongUs, 0, 0.25f, "Impostor", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, $"SyncAssignM", null, true);
                }
            }, "Become Impostor");
            Impostor.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            var CrewmateNetwork = new QMSingleButton(WorldMenu.AmongUs, 5, -0.25f, "Crewmate", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, $"SyncAssignB");
                }
            }, "Everyone a crewmate");
            CrewmateNetwork.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            var ImpostorNetwork = new QMSingleButton(WorldMenu.AmongUs, 5, 0.25f, "Impostor", () =>
            {
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Exploits.Exploits.SendUdonRPC(Object.gameObject, $"SyncAssignM");
                }
            }, "Everyone Impostor");
            ImpostorNetwork.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            //Page 2

            var TPLoop = new QMToggleButton(WorldMenu.AmongUsPage2, 1, 0, "Teleport\nLoop", () =>
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

            var KillLoop = new QMToggleButton(WorldMenu.AmongUsPage2, 2, 0, "Kill\nLoop", () =>
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

            var BlindLoop = new QMToggleButton(WorldMenu.AmongUsPage2, 3, 0, "Vote Kick\nLoop", () =>
            {
                Notifications.Notify("Udon protection enabled");
                BlindLoopBool = true;
                Settings.UdonExploitProtections = true;

                List<GameObject> Behaviours = new List<GameObject>();
                var Nodes = GameObject.Find("Player Nodes");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name != Nodes.name) Behaviours.Add(Object.gameObject);
                }

                MelonCoroutines.Start(BlindLoop());
                IEnumerator BlindLoop()
                {
                    while (BlindLoopBool)
                    {
                        foreach (var Behaviour in Behaviours)
                        {
                            Exploits.Exploits.SendUdonRPC(Behaviour, "SyncVotedOut");
                        }
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield break;
                }
            }, "Disabled", () =>
            {
                BlindLoopBool = false;
            }, "Vote kick all players in loop");
            WorldMenu.AllToggles.Add(BlindLoop);

            var AbortLoop = new QMToggleButton(WorldMenu.AmongUsPage2, 4, 0, "Abort\nLoop", () =>
            {
                AbortLoopBool = true;
                var Object = GameObject.Find("Game Logic");
                MelonCoroutines.Start(AbortLoop());
                IEnumerator AbortLoop()
                {
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
        }
    }
}
