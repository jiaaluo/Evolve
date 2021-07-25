using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ButtonApi;
using UnityEngine;
using Evolve.Exploits;

namespace Evolve.Buttons.Worlds
{
    class JustBClub
    {
        public static void Initialize()
        {
            new QMSingleButton(WorldMenu.JustBClub, 1, 0, "Become VIP", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Patreon"), "IsPatron");
            }, "");

            new QMSingleButton(WorldMenu.JustBClub, 2, 0, "Become Elite", () =>
            {
                Exploits.Exploits.SendUdonRPC(GameObject.Find("Patreon"), "IsElite");
            }, "");
        }
    }
}
