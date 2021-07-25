using Evolve.Api;
using Evolve.Exploits;
using Evolve.Modules;
using Evolve.Modules.AvatarHider;
using Evolve.Modules.NearClippingPlaneAdjuster;
using Evolve.Modules.PortableMirror;
using Evolve.Modules.PostProcessing;
using Evolve.Utils;
using Evolve.Wrappers;
using MelonLoader;
using ButtonApi;
using System;
using System.Linq;
using Evolve.ConsoleUtils;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.Networking;
using VRCSDK2;
using static VRC.SDKBase.VRC_EventHandler;
using Resources = UnityEngine.Resources;
using VRC_EventHandler = VRC.SDKBase.VRC_EventHandler;
using System.Collections.Generic;
using System.Diagnostics;
using UnhollowerBaseLib;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRTK;
using Object = Il2CppSystem.Object;
using VRC_Pickup = VRC.SDKBase.VRC_Pickup;
using System.Collections;
using MelonCoroutines = MelonLoader.MelonCoroutines;

namespace Evolve.Buttons
{
    internal class WorldMenuInteract
    {
        public static QMNestedButton ThisMenu;
        public static QMNestedButton Murder4;
        public static QMNestedButton AmongUs;
        private static List<QMNestedButton> AllMenues = new List<QMNestedButton>();
        public static List<QMToggleButton> AllToggles = new List<QMToggleButton>();
        public static IEnumerator CheckWorld()
        {
            while (RoomManager.field_Internal_Static_ApiWorld_0 == null) yield return null;
            {
                var ApiWorld = RoomManager.field_Internal_Static_ApiWorld_0;
                try
                {
                    foreach (var Menu in AllMenues)
                    {
                        Menu.getMainButton().setIntractable(false);
                        var MenuObject = GameObject.Find(Menu.getMenuName());
                    }

                    if (ApiWorld.id == "wrld_858dfdfc-1b48-4e1e-8a43-f0edc611e5fe") Murder4.getMainButton().setIntractable(true);
                    if (ApiWorld.id == "wrld_dd036610-a246-4f52-bf01-9d7cea3405d7") AmongUs.getMainButton().setIntractable(true);

                    foreach (var ToggleButton in AllToggles) ToggleButton.setToggleState(false, true);
                    Settings.UdonExploitProtections = ProtectionsMenu.UdonProt.btnOn.active;
                }
                catch { }
            }
        }

        public static void Initialize()
        {
            ThisMenu = new QMNestedButton(EvolveInteract.ThisMenu, 0, -0.25f, "Worlds", "Worlds Menu", null, null, null, Color.yellow);
            ThisMenu.getMainButton().getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);

            Murder4 = new QMNestedButton(ThisMenu, 1, 0, "Murder 4", "", null, null, null, Color.yellow);
            Worlds.Interact.Murder4.Initialize();
            AllMenues.Add(Murder4);

            AmongUs = new QMNestedButton(ThisMenu, 2, 0, "Among Us", "", null, null, null, Color.yellow);
            Worlds.Interact.AmongUs.Initialize();
            AllMenues.Add(AmongUs);
        }
    }
}