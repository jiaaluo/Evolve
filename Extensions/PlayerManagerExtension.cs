using ExitGames.Client.Photon;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace Evolve.Wrappers
{
    internal static class PlayerManagerExtension
    {
        public static List<Player> AllPlayers(this PlayerManager Instance)
        {
            return Instance.field_Private_List_1_Player_0;
        }

        public static Player[] GetAllPlayers(this PlayerManager instance)
        {
            return instance.prop_ArrayOf_Player_0;
        }

        public static Player GetPlayer(this PlayerManager Instance, int Index)
        {
            List<Player> list = Instance.AllPlayers();
            return list[Index];
        }

        public static Player GetPlayerWithPlayerID(this PlayerManager Instance, int playerID)
        {
            List<Player> list = Instance.AllPlayers();
            foreach (Player player in list.ToArray())
            {
                bool flag = player.GetVRCPlayerApi().playerId == playerID;
                if (flag)
                {
                    return player;
                }
            }
            return null;
        }

        public static Player GetPlayer(this PlayerManager Instance, string UserID)
        {
            List<Player> list = Instance.AllPlayers();
            for (int i = 0; i < list.Count; i++)
            {
                Player instance = list[i];
                bool flag = instance.GetAPIUser().UserID() == UserID;
                if (flag)
                {
                    return list[i];
                }
            }
            return null;
        }

        public static Player GetPlayerByRayCast(this RaycastHit RayCast)
        {
            GameObject gameObject = RayCast.transform.gameObject;
            return Utils.PlayerManager.GetPlayer(VRCPlayerApi.GetPlayerByGameObject(gameObject).playerId);
        }

        public static ulong GetSteamID(this VRCPlayer player)
        {
            return player.prop_Player_0._vrcplayer.field_Private_UInt64_0;
        }

        /*public static Hashtable GetHashtable(this ObjectPublicObInPlBoStBoHaStObInUnique photonPlayer)
        {
            return photonPlayer.prop_Hashtable_0;
        }*/
    }
}