using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace Evolve.Wrappers
{
    class NetworkingExtension
    {
        public static GameObject Instantiate(VRC_EventHandler.VrcBroadcastType broadcast, string prefabPathOrDynamicPrefabName, Vector3 position, Quaternion rotation)
        {
            return Networking.Instantiate(broadcast, prefabPathOrDynamicPrefabName, position, rotation);
        }

        public static Il2CppSystem.Collections.Generic.List<GameObject> GetDynamicPrefabs()
        {
            return VRC_SceneDescriptor.Instance.DynamicPrefabs;
        }

        public static void SpawnObject(string prefabName)
        {
            NetworkingExtension.Instantiate(0, prefabName, VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position, new Quaternion(0, 0f, 0f, 0.7f));
        }
    }
}
