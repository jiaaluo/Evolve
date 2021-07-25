using Evolve.ConsoleUtils;
using Evolve.Modules;
using Evolve.Utils;
using System;
using System.Text;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.Management;
using VRC.SDKBase;

namespace Evolve.Wrappers
{
    internal static class PlayerExtensions
    {
        public static bool IsBlockedEitherWay(string userId)
        {
            var moderationManager = ModerationManager.prop_ModerationManager_0;
            if (moderationManager == null) return false;
            if (APIUser.CurrentUser.id == userId)  return false;

            var moderationsDict = ModerationManager.prop_ModerationManager_0.field_Private_Dictionary_2_String_List_1_ApiPlayerModeration_0;
            if (!moderationsDict.ContainsKey(userId)) return false;

            foreach (var playerModeration in moderationsDict[userId])
            {
                if (playerModeration != null && playerModeration.moderationType == ApiPlayerModeration.ModerationType.Block) return true;
            }

            return false;

        }

        public static APIUser GetAPIUser(this Player Instance)
        {
            return Instance.prop_APIUser_0;
        }

        public static APIUser GetAPIUser(this VRCPlayer Instance)
        {
            return Instance._player.prop_APIUser_0;
        }

        public static APIUser GetAPIUser(this PlayerNet Instance)
        {
            return Instance._vrcPlayer._player.prop_APIUser_0;
        }

        public static Player GetPlayer(this VRCPlayer Instance)
        {
            return Instance._player;
        }

        public static Player GetPlayer(this PlayerNet Instance)
        {
            return Instance.prop_Player_0;
        }

        public static VRCPlayer GetVRCPlayer(this Player Instance)
        {
            return Instance._vrcplayer;
        }

        public static VRCPlayer GetVRCPlayer(this PlayerNet Instance)
        {
            return Instance._vrcPlayer;
        }

        public static VRCPlayerApi GetVRCPlayerApi(this Player Instance)
        {
            return Instance.prop_VRCPlayerApi_0;
        }

        public static VRCPlayerApi GetVRCPlayerApi(this VRCPlayer Instance)
        {
            return Instance.prop_VRCPlayerApi_0;
        }

        public static VRCPlayerApi GetVRCPlayerApi(this PlayerNet Instance)
        {
            return Instance.GetVRCPlayer().GetVRCPlayerApi();
        }

        public static string UserID(this Player Instance)
        {
            return Instance.GetAPIUser().id;
        }

        public static string UserID(this VRCPlayer Instance)
        {
            return Instance.GetAPIUser().id;
        }

        public static string UserID(this PlayerNet Instance)
        {
            return Instance.GetAPIUser().id;
        }

        public static string UserID(this APIUser Instance)
        {
            return Instance.id;
        }

        public static string DisplayName(this Player Instance)
        {
            return Instance.GetAPIUser().displayName;
        }

        public static string DisplayName(this VRCPlayer Instance)
        {
            return Instance.GetAPIUser().displayName;
        }

        public static string DisplayName(this PlayerNet Instance)
        {
            return Instance.GetAPIUser().displayName;
        }

        public static string DisplayName(this APIUser Instance)
        {
            return Instance.displayName;
        }

        public static bool GetIsMaster(this Player Instance)
        {
            return Instance.GetVRCPlayerApi().isMaster;
        }

        public static bool GetIsMaster(this VRCPlayer Instance)
        {
            return Instance.GetVRCPlayerApi().isMaster;
        }

        public static bool GetIsMaster(this PlayerNet Instance)
        {
            return Instance.GetVRCPlayerApi().isMaster;
        }

        public static bool GetIsInVR(this Player Instance)
        {
            return Instance.GetVRCPlayerApi().IsUserInVR();
        }

        public static bool GetIsInVR(this VRCPlayer Instance)
        {
            return Instance.GetVRCPlayerApi().IsUserInVR();
        }

        public static bool GetIsInVR(this PlayerNet Instance)
        {
            return Instance.GetVRCPlayerApi().IsUserInVR();
        }

        public static short GetPing(this VRCPlayer Instance)
        {
            return Instance.prop_PlayerNet_0.field_Private_Int16_0;
        }

        public static bool IsEvolved(this VRCPlayer Instance)
        {
            if (GetIsEvolved.EvolvedUsers.ContainsKey(Instance.UserID())) return true;
            else return false;
        }

        public static void SetBotTarget(this VRCPlayer Instance)
        {
            Bot.Target.VRCPlayer = Instance;
            Bot.Server.SendMessage($"Target/{Instance.UserID()}");
        }

        public static string GetEvolveSubscription(this VRCPlayer Instance)
        {
            if (GetIsEvolved.EvolvedUsers.ContainsKey(Instance.UserID())) return GetIsEvolved.EvolvedUsers[Instance.UserID()];
            else return "None";
        }

        public static int GetFrames(this VRCPlayer Instance)
        {
            return (Instance.prop_PlayerNet_0.prop_Byte_0 != 0) ? ((int) (1000f / Instance.prop_PlayerNet_0.prop_Byte_0)) : 0;
        }

        public static string GetPingColored(this VRCPlayer Instance)
        {
            bool flag = Instance.GetPing() <= 75;
            string arg;
            if (flag)
            {
                arg = "<color=#59D365>";
            }
            else
            {
                bool flag2 = Instance.GetPing() >= 75 && Instance.GetPing() <= 150;
                if (flag2)
                {
                    arg = "<color=#FF7000>";
                }
                else
                {
                    arg = "<color=red>";
                }
            }
            return string.Format("{0}{1}</color>", arg, Instance.GetPing());
        }

        public static string GetFramesColored(this VRCPlayer Instance)
        {
            bool flag = Instance.GetFrames() >= 80;
            string arg;
            if (flag)
            {
                arg = "<color=#59D365>";
            }
            else
            {
                bool flag2 = Instance.GetFrames() <= 80 && Instance.GetFrames() >= 30;
                if (flag2)
                {
                    arg = "<color=#FF7000>";
                }
                else
                {
                    arg = "<color=red>";
                }
            }
            return string.Format("{0}{1}</color>", arg, Instance.GetFrames());
        }

        public static void ReloadAvatar(this VRCPlayer Instance)
        {
            VRCPlayer.Method_Public_Static_Void_APIUser_0(Instance.GetAPIUser());
        }

        public static void ReloadAvatar(this Player Instance)
        {
            Instance.GetVRCPlayer().ReloadAvatar();
        }

        public static string GetRank(this APIUser Instance)
        {
            string result;
            if (Instance.tags.Contains("Evolved")) result = "Evolved";
            else if (Instance.hasModerationPowers || Instance.tags.Contains("admin_moderator")) result = "Moderator";
            else if (Instance.hasSuperPowers || Instance.tags.Contains("admin_")) result = "Admin";
            else if (Instance.hasVIPAccess || (Instance.tags.Contains("system_legend") && Instance.tags.Contains("system_trust_legend") && Instance.tags.Contains("system_trust_trusted"))) result = "Legend";
            else if (Instance.hasLegendTrustLevel || (Instance.tags.Contains("system_trust_legend") && Instance.tags.Contains("system_trust_trusted"))) result = "Veteran";
            else if (Instance.hasVeteranTrustLevel) result = "Trusted";
            else if (Instance.hasTrustedTrustLevel) result = "Known";
            else if (Instance.hasKnownTrustLevel) result = "User";
            else if (Instance.hasBasicTrustLevel || Instance.isNewUser) result = "New User";
            else if (Instance.hasNegativeTrustLevel) result = "NegativeTrust";
            else if (Instance.hasVeryNegativeTrustLevel) result = "VeryNegativeTrust";
            else result = "Visitor"; 
            return result;
        }

        public static bool GetIsFriend(this APIUser Instance)
        {
            return Instance.isFriend || APIUser.IsFriendsWith(Instance.id);
        }

        public static bool IsAvatarLoaded(this Player Instance)
        {
            return Evolve.Utils.Utilities.PolyCount(Instance.gameObject) >= 2421;
        }

        public static bool GetIsDANGER(this VRCPlayer Instance)
        {
            return Instance.GetVRCPlayerApi().isModerator || Instance.GetAPIUser().hasModerationPowers || Instance.GetAPIUser().hasSuperPowers || Instance.GetAPIUser().hasSuperPowers;
        }

        public static bool GetIsBot(this VRCPlayer Instance)
        {
            return Instance.transform.position.x == 0 && Instance.UserID() != APIUser.CurrentUser.id;
        }

        public static bool GetIsQuest(this VRCPlayer Instance)
        {
            return Instance._player.prop_APIUser_0.IsOnMobile;
        }

        public static ApiAvatar GetApiAvatar(this Player Instance)
        {
            return Instance.prop_ApiAvatar_0;
        }

        public static ApiAvatar GetApiAvatar(this VRCPlayer Instance)
        {
            return Instance.prop_ApiAvatar_0;
        }
        public static GameObject GetAvatar(this Player Instance)
        {
            return Instance.GetVRCPlayer().GetAvatarManager().GetAvatar();
        }
        public static VRCAvatarManager GetAvatarManager(this VRCPlayer Instance)
        {
            return Instance.prop_VRCAvatarManager_0;
        }
        public static GameObject GetAvatar(this VRCPlayer Instance)
        {
            return Instance.GetAvatarManager().GetAvatar();
        }

        public static GameObject GetAvatar(this VRCAvatarManager Instance)
        {
            if (Instance.prop_GameObject_0 != null)
            {
                return Instance.prop_GameObject_0;
            }
            if (Instance.field_Public_GameObject_1 != null)
            {
                return Instance.field_Public_GameObject_1;
            }
            return null;
        }
        public static APIUser SelectedAPIUser(this QuickMenu instance)
        {
            return instance.field_Private_APIUser_0;
        }

        public static VRCPlayer SelectedVRCPlayer(this QuickMenu instance)
        {
            return instance.field_Private_Player_0.prop_VRCPlayer_0;
        }

        public static Player SelectedPlayer(this QuickMenu instance, VRCPlayer currentUser)
        {
            return instance.field_Private_Player_0.prop_VRCPlayer_0.GetPlayer();
        }
    }
}