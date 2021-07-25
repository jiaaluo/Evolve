using ButtonApi;
using Evolve.Buttons;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Evolve.ConsoleUtils
{
    internal class EvoVrConsole
    {
#pragma warning disable CS0649 // Le champ 'EvoVrConsole.LogText' n'est jamais assigné et aura toujours sa valeur par défaut null
        public static Text LogText;
#pragma warning restore CS0649 // Le champ 'EvoVrConsole.LogText' n'est jamais assigné et aura toujours sa valeur par défaut null
        public static List<Text> AllLogsText = new List<Text>();
        public static int YPos = 0;

        public static IEnumerator Initialize()
        {
            while (EvolveMenu.ConsoleButton == null) yield return null;
            for (int I = 0; I < 20; I++)
            {
                var Parent = EvolveMenu.ConsoleButton.getGameObject();
                var NewText = UnityEngine.Object.Instantiate<GameObject>(QMStuff.GetQuickMenuInstance().transform.Find("QuickMenu_NewElements/_InfoBar/EarlyAccessText").gameObject, Parent.transform);
                NewText.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(835, -880 + YPos, 0);
                NewText.SetActive(true);
                var TextComponent = NewText.GetComponent<Text>();
                TextComponent.supportRichText = true;
                TextComponent.fontSize = 32;
                TextComponent.fontStyle = FontStyle.Normal;
                TextComponent.text = "";
                TextComponent.GetComponent<RectTransform>().sizeDelta = new Vector2(16 * 100, 100);
                YPos += 40;
                AllLogsText.Add(TextComponent);
            }
        }

        public enum LogsType
        {
            Info,
            Voice,
            Avatar,
            Block,
            Join,
            Left,
            Warn,
            Kick,
            Logout,
            Ban,
            Friend,
            Event,
            RPC,
            Empty,
            Server,
            Crash,
            Moderation,
            Api,
            Bot,
        }

        public static void Log(LogsType Type, string Text)
        {
            try
            {
                AllLogsText[19].text = AllLogsText[18].text;
                AllLogsText[18].text = AllLogsText[17].text;
                AllLogsText[17].text = AllLogsText[16].text;
                AllLogsText[16].text = AllLogsText[15].text;
                AllLogsText[15].text = AllLogsText[14].text;
                AllLogsText[14].text = AllLogsText[13].text;
                AllLogsText[13].text = AllLogsText[12].text;
                AllLogsText[12].text = AllLogsText[11].text;
                AllLogsText[11].text = AllLogsText[10].text;
                AllLogsText[10].text = AllLogsText[9].text;
                AllLogsText[9].text = AllLogsText[8].text;
                AllLogsText[8].text = AllLogsText[7].text;
                AllLogsText[7].text = AllLogsText[6].text;
                AllLogsText[6].text = AllLogsText[5].text;
                AllLogsText[5].text = AllLogsText[4].text;
                AllLogsText[4].text = AllLogsText[3].text;
                AllLogsText[3].text = AllLogsText[2].text;
                AllLogsText[2].text = AllLogsText[1].text;
                AllLogsText[1].text = AllLogsText[0].text;

                switch (Type)
                {
                    case LogsType.Server:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#00f7ff>[Server]:</color>  " + Text;
                        break;
                    case LogsType.Api:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#00ffe5>[API]:</color>  " + Text;
                        break;
                    case LogsType.Bot:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#5900ff>[Bot]:</color>  " + Text;
                        break;
                    case LogsType.Empty:
                        AllLogsText[0].text = "" + Text;
                        break;
                    case LogsType.RPC:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#d900ff>[RPC]:</color>  " + Text;
                        break;
                    case LogsType.Event:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#6f00ff>[Event]:</color>  " + Text;
                        break;
                    case LogsType.Info:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#949494>[Info]:</color>  " + Text;
                        break;
                    case LogsType.Avatar:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#00FF62>[Avatar]:</color>  " + Text;
                        break;
                    case LogsType.Join:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#ff00cc>[+]:</color>  " + Text;
                        break;
                    case LogsType.Left:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#6b6b6b>[-]:</color>  " + Text;
                        break;
                    case LogsType.Warn:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#ffea00>[Warn]:</color>  " + Text;
                        break;
                    case LogsType.Crash:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#ff00e6>[Crash]:</color>  " + Text;
                        break;
                    case LogsType.Moderation:
                        AllLogsText[0].text = "<color=#5100ff>[Evolve</color><color=#ff007b>Engine]</color>  <color=#ffea00>[Moderation]:</color>  " + Text;
                        break;
                }
            }
            catch { }
        }
    }
}