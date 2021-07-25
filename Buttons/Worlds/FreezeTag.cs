using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ButtonApi;
using UnityEngine;
using Evolve.Exploits;

namespace Evolve.Buttons.Worlds
{
    class FreezeTag
    {
        public static void Initialize()
        {
            /* var Nodes = GameObject.Find("controldeligator");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name.Contains("tagplayerctrl")) Exploits.Exploits.SendUdonRPC(Object.gameObject, "SyncKill");
                }*/

            new QMSingleButton(WorldMenu.FreezeTag, 1, 0, "Unfreeze lobby", () =>
            {
                var Nodes = GameObject.Find("controldeligator");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name.Contains("tagplayerctrl")) Exploits.Exploits.SendUdonRPC(Object.gameObject, "unfreezeme");
                }
            }, "unfreeze everyone in the lobby");


            new QMSingleButton(WorldMenu.FreezeTag, 2, 0, "Everyone runner", () =>
            {
                var Nodes = GameObject.Find("controldeligator");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name.Contains("tagplayerctrl")) Exploits.Exploits.SendUdonRPC(Object.gameObject, "attackerprep");
                }
            }, "unfreeze everyone in the lobby");

            new QMSingleButton(WorldMenu.FreezeTag, 3, 0, "Everyone tagger", () =>
            {
                var Nodes = GameObject.Find("controldeligator");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name.Contains("tagplayerctrl")) Exploits.Exploits.SendUdonRPC(Object.gameObject, "targetprep");
                }
            }, "unfreeze everyone in the lobby");

            new QMSingleButton(WorldMenu.FreezeTag, 4, 0, "Break game", () =>
            {
                var Nodes = GameObject.Find("controldeligator");
                foreach (var Object in Nodes.GetComponentsInChildren<Transform>())
                {
                    if (Object.name.Contains("tagplayerctrl"))
                    {
                        Exploits.Exploits.SendUdonRPC(Object.gameObject, "usersfail");
                        Exploits.Exploits.SendUdonRPC(Object.gameObject, "masterfail");
                    }
                }
            }, "unfreeze everyone in the lobby");


            new QMSingleButton(WorldMenu.FreezeTag, 1, 1, "Taggers win", () =>
            {
                var Controller = GameObject.Find("gamecontroler");
                Exploits.Exploits.SendUdonRPC(Controller.gameObject, "tagwon");
            }, "Taggers win");

            new QMSingleButton(WorldMenu.FreezeTag, 2, 1, "Runners win", () =>
            {
                var Controller = GameObject.Find("gamecontroler");
                Exploits.Exploits.SendUdonRPC(Controller.gameObject, "runwon");
            }, "Runners win");

            new QMSingleButton(WorldMenu.FreezeTag, 3, 1, "Force start", () =>
            {
                var Controller = GameObject.Find("gamecontroler");
                Exploits.Exploits.SendUdonRPC(Controller.gameObject, "startgame");
            }, "Force start the game");

            new QMSingleButton(WorldMenu.FreezeTag, 4, 1, "End game", () =>
            {
                var Controller = GameObject.Find("gamecontroler");
                Exploits.Exploits.SendUdonRPC(Controller.gameObject, "endgame");
            }, "End game");
        }
    }
}
