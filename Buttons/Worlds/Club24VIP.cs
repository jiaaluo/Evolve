using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ButtonApi;
using UnityEngine;
using Evolve.Exploits;
using VRC.SDKBase;
using Evolve.Wrappers;

namespace Evolve.Buttons.Worlds
{
    class Club24VIP
    {
        public static QMToggleButton VipSwich;
        public static void Initialize()
        {
            VipSwich = new QMToggleButton(WorldMenu.Club24VIP, 1, 0, "<color=#ffb700>VIP Access</color>", () =>
            {
                if (Login.Authorization.AccessLevel > 1)
                {
                    GameObject.Find("Cube (18)").GetComponent<VRC_Trigger>().Interact();
                    var Object = GameObject.Find("ticket out");
                    Object.SetActive(true);
                    Object.GetComponent<VRC_Trigger>().Interact();
                }
                else
                {
                    VipSwich.setToggleState(false, false);
                    Wrappers.Utils.VRCUiPopupManager.ShowAlert("Evolve Engine", "This feature is reserved for VIP users or require an access level of 2");
                }
            }, "Normal Access", () =>
            {
                var Object = GameObject.Find("ticket out");
                Object.SetActive(true);
                Object.GetComponent<VRC_Trigger>().Interact();
                GameObject.Find("Cube (18)").GetComponent<VRC_Trigger>().Interact();
            },"");

            new QMSingleButton(WorldMenu.Club24VIP, 2, 0, "Staff Only", () =>
            {
                if (Login.Authorization.Subscribtion == "Admin") VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position = new Vector3(-8.781f, 3.665f, 13.339f);
                else Wrappers.Utils.VRCUiPopupManager.ShowAlert("Evolve Engine", "This feature is reserved for the staff.");
            }, "Staff Only");
        }
    }
}
