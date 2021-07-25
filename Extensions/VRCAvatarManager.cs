using Evolve.ConsoleUtils;
using Evolve.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks.Triggers;

namespace Evolve.Wrappers
{
    internal static class VRCAvatarManagerExtension
    {
        public static void DestroyAvatar(this VRCAvatarManager Instance)
        {
            UnityEngine.Object.Destroy(Instance.transform.Find("Avatar").gameObject);
        }
        public static void HideAvatar(this VRCAvatarManager Instance)
        {
            Instance.gameObject.SetActive(false);
        }

        public static void ShowAvatar(this VRCAvatarManager Instance)
        {
            Instance.gameObject.SetActive(true);
        }
    }
}
