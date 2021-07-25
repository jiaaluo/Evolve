using ButtonApi;
using Evolve.Utils;
using Evolve.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static VRC.SDKBase.VRC_EventHandler;

namespace Evolve.Buttons.Worlds
{
    class Murder23
    {
        public static void Initialize()
        {
            new QMToggleButton(WorldMenu.Murder23, 1, 0, "GodMode", () =>
            {
                Settings.MurderGodMode = true;
            }, "Disabled", () =>
            {
                Settings.MurderGodMode = false;
            }, "Will make you invinsible");

            new QMSingleButton(WorldMenu.Murder23, 2, 0, "Become\nBystander", () =>
            {
                Exploits.Exploits.SendRPC(VrcEventType.ActivateCustomTrigger, "ActivateCustomTrigger", GameObject.Find("Role Checking"), 0, 0, "Become B", VrcBooleanOp.False, VrcBroadcastType.Local);

            }, "You will become Bystander");

            new QMSingleButton(WorldMenu.Murder23, 3, 0, "Become\nDetective", () =>
            {
                Exploits.Exploits.SendRPC(VrcEventType.ActivateCustomTrigger, "ActivateCustomTrigger", GameObject.Find("Role Checking"), 0, 0, "Become D", VrcBooleanOp.False, VrcBroadcastType.Local);
            }, "You will become Detective");

            new QMSingleButton(WorldMenu.Murder23, 4, 0, "Become\nMurderer", () =>
            {
                Exploits.Exploits.SendRPC(VrcEventType.ActivateCustomTrigger, "ActivateCustomTrigger", GameObject.Find("Role Checking"), 0, 0, "Become M", VrcBooleanOp.False, VrcBroadcastType.Local);
            }, "You will become Murderer");

            new QMSingleButton(WorldMenu.Murder23, 1, 1, "Everyone\nBystander", () =>
            {
                Exploits.Exploits.SendRPC(VrcEventType.ActivateCustomTrigger, "ActivateCustomTrigger", GameObject.Find("Role Checking"), 0, 0, "Become B", VrcBooleanOp.False, VrcBroadcastType.Always);
            }, "Everyone Bystander");

            new QMSingleButton(WorldMenu.Murder23, 2, 1, "Everyone\nDetective", () =>
            {
                Exploits.Exploits.SendRPC(VrcEventType.ActivateCustomTrigger, "ActivateCustomTrigger", GameObject.Find("Role Checking"), 0, 0, "Become D", VrcBooleanOp.False, VrcBroadcastType.Always);
            }, "Everyone Detective");

            new QMSingleButton(WorldMenu.Murder23, 3, 1, "Everyone\nMurderer", () =>
            {
                Exploits.Exploits.SendRPC(VrcEventType.ActivateCustomTrigger, "ActivateCustomTrigger", GameObject.Find("Role Checking"), 0, 0, "Become B", VrcBooleanOp.False, VrcBroadcastType.Always);
            }, "Everyone Murderer");
        }
    }
}
