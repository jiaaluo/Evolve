using Evolve.Wrappers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evolve.Modules
{
    class GetIsEvolved
    {
        public static Dictionary<string, string> EvolvedUsers = new Dictionary<string, string>();
        public static IEnumerator CreateHandler()
        {
            while (VRCPlayer.field_Internal_Static_VRCPlayer_0 != true) yield return null;
            GameObject Handler = GameObject.Find($"R2V0SXNFdm9sdmVk");
            if (Handler == null) Handler =  new GameObject("R2V0SXNFdm9sdmVk");
        }

        public static void IsEvolved(string UserID, string Subscription)
        {
            if(!EvolvedUsers.ContainsKey(UserID)) EvolvedUsers.Add(UserID, Subscription);
            else
            {
                EvolvedUsers.Remove(UserID);
                EvolvedUsers.Add(UserID, Subscription);
            }
            CustomNameplates.Refresh(Wrappers.Utils.PlayerManager.GetPlayer(UserID)._vrcplayer.field_Public_PlayerNameplate_0);
        }
    }
}
