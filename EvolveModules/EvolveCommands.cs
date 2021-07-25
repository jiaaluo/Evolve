using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ButtonApi;
using Evolve.Api;
using Evolve.Buttons;
using Evolve.Utils;
using Evolve.Wrappers;
using UnityEngine;
using VRC.Core;

namespace Evolve.Modules
{
    class EvolveCommands
    {
        public static IEnumerator CreateHandler()
        {
            while (VRCPlayer.field_Internal_Static_VRCPlayer_0 != true) yield return null;
            GameObject Handler = GameObject.Find($"Q29tbWFuZCB0byBydW4= {APIUser.CurrentUser.id}");
            if (Handler == null) Handler = new GameObject($"Q29tbWFuZCB0byBydW4= {APIUser.CurrentUser.id}");
        }
        public static void GetCommand(string Sender, string Message)
        {
            var Decode = Convert.FromBase64String(Message);
            var Command = Encoding.UTF8.GetString(Decode);
            if (Command == "TeleportToMoon") TeleportToMoon();
            if (Command == "Kick") Kick();
            if (Command == "CloseGame") CloseGame();
            if (Command == "ProtectionsOn") ProtectionsToggle(true);
            if (Command == "ProtectionsOff") ProtectionsToggle(false);
        }

        #region Commands
        public static void TeleportToMoon()
        {
            VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position = new Vector3(VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.x, 100, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position.z);
        }

        public static void Kick()
        {
            Functions.ForceJoin("wrld_4432ea9b-729c-46e3-8eaf-846aa0a37fdd:Evolve");
            IEnumerator Wait()
            {
                yield return new WaitForSeconds(2);
                while (VRCPlayer.field_Internal_Static_VRCPlayer_0 != true) yield return null;
                yield return new WaitForSeconds(2);
                Wrappers.Utils.VRCUiPopupManager.ShowAlert("Evolve Engine", "You have been kicked by an Evolve staff member.");
            }
        }

        public static void CloseGame()
        {
            Process.GetCurrentProcess().Kill();
        }

        public static void ProtectionsToggle(bool State)
        {
            ProtectionsMenu.RPCBlock.setToggleState(State, true);
            ProtectionsMenu.EventsBlock.setToggleState(State, true);
            ProtectionsMenu.Portals.setToggleState(State, true);
            ProtectionsMenu.Pickups.setToggleState(State, true);
            ProtectionsMenu.Chairs.setToggleState(State, true);
            ProtectionsMenu.UdonProt.setToggleState(State, true);
            ProtectionsMenu.AvatarCleaning.setToggleState(State, true);
            if (State) Notifications.StaffNotify("Your protections were enabled\nby an admin");
            else Notifications.StaffNotify("Your protections were disabled\nby an admin");
        }
        #endregion
    }
}
