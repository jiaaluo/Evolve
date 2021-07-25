using System;
using System.Runtime.InteropServices;

internal class DiscordRpc
{
    public struct EventHandlers
    {
        public DiscordRpc.ReadyCallback readyCallback;
        public DiscordRpc.DisconnectedCallback disconnectedCallback;
        public DiscordRpc.ErrorCallback errorCallback;
        public DiscordRpc.JoinCallback joinCallback;
        public DiscordRpc.SpectateCallback spectateCallback;
        public DiscordRpc.RequestCallback requestCallback;
    }

    [Serializable]
    public struct RichPresence
    {
        public string state;
        public string details;
        public long startTimestamp;
        public long endTimestamp;
        public string largeImageKey;
        public string largeImageText;
        public string smallImageKey;
        public string smallImageText;
        public string partyId;
        public int partySize;
        public int partyMax;
        public string matchSecret;
        public string joinSecret;
        public string spectateSecret;
        public bool instance;
    }

    [Serializable]
    public struct JoinRequest
    {
#pragma warning disable CS0649 // Le champ 'DiscordRpc.JoinRequest.userId' n'est jamais assigné et aura toujours sa valeur par défaut null
        public string userId;
#pragma warning restore CS0649 // Le champ 'DiscordRpc.JoinRequest.userId' n'est jamais assigné et aura toujours sa valeur par défaut null
#pragma warning disable CS0649 // Le champ 'DiscordRpc.JoinRequest.username' n'est jamais assigné et aura toujours sa valeur par défaut null
        public string username;
#pragma warning restore CS0649 // Le champ 'DiscordRpc.JoinRequest.username' n'est jamais assigné et aura toujours sa valeur par défaut null
#pragma warning disable CS0649 // Le champ 'DiscordRpc.JoinRequest.discriminator' n'est jamais assigné et aura toujours sa valeur par défaut null
        public string discriminator;
#pragma warning restore CS0649 // Le champ 'DiscordRpc.JoinRequest.discriminator' n'est jamais assigné et aura toujours sa valeur par défaut null
#pragma warning disable CS0649 // Le champ 'DiscordRpc.JoinRequest.avatar' n'est jamais assigné et aura toujours sa valeur par défaut null
        public string avatar;
#pragma warning restore CS0649 // Le champ 'DiscordRpc.JoinRequest.avatar' n'est jamais assigné et aura toujours sa valeur par défaut null
    }

    public enum Reply
    {
        No,
        Yes,
        Ignore
    }

    [DllImport("Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Initialize")]
    public static extern void Initialize(string applicationId, ref DiscordRpc.EventHandlers handlers, bool autoRegister, string optionalSteamId);

    [DllImport("Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Shutdown")]
    public static extern void Shutdown();

    [DllImport("Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RunCallbacks")]
    public static extern void RunCallbacks();

    [DllImport("Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UpdatePresence")]
    public static extern void UpdatePresence(ref DiscordRpc.RichPresence presence);

    [DllImport("Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_ClearPresence")]
    public static extern void ClearPresence();

    [DllImport("Dependencies/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Respond")]
    public static extern void Respond(string userId, DiscordRpc.Reply reply);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ReadyCallback();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DisconnectedCallback(int errorCode, string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ErrorCallback(int errorCode, string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void JoinCallback(string secret);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SpectateCallback(string secret);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void RequestCallback(ref DiscordRpc.JoinRequest request);
}