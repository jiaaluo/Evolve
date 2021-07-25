using Evolve.ConsoleUtils;
using Evolve.Utils;
using MelonLoader;
using System;
using VRC.Core;

namespace DiscordRichPresence
{
    internal static class DiscordManager
    {
        private static DiscordRpc.RichPresence presence;
        private static DiscordRpc.EventHandlers eventHandlers;
        private static bool Enabled = false;
        public static void Init()
        {
            eventHandlers = default(DiscordRpc.EventHandlers);
            eventHandlers.errorCallback = delegate (int code, string message) { };
            presence.state = "World: Loading...";
            presence.details = "Name: ?";
            presence.largeImageKey = "logo";
            presence.partySize = 0;
            presence.partyMax = 0;
            presence.startTimestamp = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            presence.partyId = "";
            presence.largeImageText = "Evolve Engine";
            try
            {
                ApiServerEnvironment serverEnvironment = VRCApplicationSetup.prop_VRCApplicationSetup_0.field_Public_ApiServerEnvironment_0;
                DiscordRpc.Initialize("864235690572906507", ref eventHandlers, true, "438100");
                DiscordRpc.UpdatePresence(ref presence);
                Enabled = true;
            }
            catch { }
        }

        public static void Update()
        {
            try
            {
                if (!Enabled) return;
                if (APIUser.CurrentUser != null)
                {
                    if (VRCPlayer.field_Internal_Static_VRCPlayer_0 != true)
                    {
                        if (Settings.HideDiscordName == true) presence.details = $"Name: Hidden";
                        else presence.details = $"Name: {APIUser.CurrentUser.displayName}";
                        if (Settings.HideDiscordWorld == true) presence.state = $"World: Hidden";
                        else presence.state = $"World: Loading...";
                        DiscordRpc.UpdatePresence(ref presence);
                    }

                    else
                    {
                        if (Settings.HideDiscordName == true) presence.details = $"Name: Hidden";
                        else presence.details = $"Name: {APIUser.CurrentUser.displayName}";
                        if (Settings.HideDiscordWorld == true) presence.state = $"World: Hidden";
                        else presence.state = $"World: {RoomManager.field_Internal_Static_ApiWorld_0.name}";
                        DiscordRpc.UpdatePresence(ref presence);
                    }
                }
                else
                {
                    if (Settings.HideDiscordName == true) presence.details = $"Name: Hidden";
                    else presence.details = $"Name: ?";
                    if (Settings.HideDiscordWorld == true) presence.state = $"World: Hidden";
                    else presence.state = $"World: {RoomManager.field_Internal_Static_ApiWorld_0.name}";
                    DiscordRpc.UpdatePresence(ref presence);
                }
            }
            catch { }
        }

        public static void OnApplicationQuit()
        {
            DiscordRpc.Shutdown();
        }
    }
}