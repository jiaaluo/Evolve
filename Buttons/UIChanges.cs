using ButtonApi;
using Evolve.ConsoleUtils;
using Evolve.Utils;
using MelonLoader;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI;

namespace Evolve.Buttons
{
    internal class UIChanges
    {
        public static string ConsoleUrl = "https://i.imgur.com/VcltXpG.png";
        public static string PlayerListUrl = "https://i.imgur.com/QIuzeFj.png";
        public static string SelfInfoUrl = "https://i.imgur.com/Y5YQSsf.png";
        public static string UserInfoUrl = "https://i.imgur.com/Y5YQSsf.png";
#pragma warning disable CS0649 // Le champ 'UIChanges.ConsoleObject' n'est jamais assigné et aura toujours sa valeur par défaut null
        public static GameObject ConsoleObject;
#pragma warning restore CS0649 // Le champ 'UIChanges.ConsoleObject' n'est jamais assigné et aura toujours sa valeur par défaut null
#pragma warning disable CS0649 // Le champ 'UIChanges.PlayerListObject' n'est jamais assigné et aura toujours sa valeur par défaut null
        public static GameObject PlayerListObject;
#pragma warning restore CS0649 // Le champ 'UIChanges.PlayerListObject' n'est jamais assigné et aura toujours sa valeur par défaut null
#pragma warning disable CS0649 // Le champ 'UIChanges.SelfInfoObject' n'est jamais assigné et aura toujours sa valeur par défaut null
        public static GameObject SelfInfoObject;
#pragma warning restore CS0649 // Le champ 'UIChanges.SelfInfoObject' n'est jamais assigné et aura toujours sa valeur par défaut null
#pragma warning disable CS0649 // Le champ 'UIChanges.UserInfoObject' n'est jamais assigné et aura toujours sa valeur par défaut null
        public static GameObject UserInfoObject;
#pragma warning restore CS0649 // Le champ 'UIChanges.UserInfoObject' n'est jamais assigné et aura toujours sa valeur par défaut null
        public static bool GameLoaded = false;
        public static bool UILoaded = false;

        public static List<string> ShortcutMenuButtons = new List<string>
        {
            //"/UserInterface/QuickMenu/ShortcutMenu/WorldsButton",
            "/UserInterface/QuickMenu/ShortcutMenu/AvatarButton",
            "/UserInterface/QuickMenu/ShortcutMenu/SocialButton",
            "/UserInterface/QuickMenu/ShortcutMenu/SafetyButton",
            "/UserInterface/QuickMenu/ShortcutMenu/GoHomeButton",
            "/UserInterface/QuickMenu/ShortcutMenu/RespawnButton",
            "/UserInterface/QuickMenu/ShortcutMenu/SettingsButton",
            "/UserInterface/QuickMenu/ShortcutMenu/ReportWorldButton",
            "/UserInterface/QuickMenu/ShortcutMenu/UIElementsButton",
            "/UserInterface/QuickMenu/ShortcutMenu/CameraButton",
            "/UserInterface/QuickMenu/ShortcutMenu/EmoteButton",
            "/UserInterface/QuickMenu/ShortcutMenu/EmojiButton",
           /* "/UserInterface/QuickMenu/UserInteractMenu/KickButton",
            "/UserInterface/QuickMenu/UserInteractMenu/WarnButton",
            "/UserInterface/QuickMenu/UserInteractMenu/ViewPlaylistsButton",
            "/UserInterface/QuickMenu/UserInteractMenu/ShowAvatarStatsButton"*/
        };

        public static List<string> FirstLine = new List<string>
        {
            //"/UserInterface/QuickMenu/ShortcutMenu/WorldsButton",
            "/UserInterface/QuickMenu/ShortcutMenu/AvatarButton",
            "/UserInterface/QuickMenu/ShortcutMenu/SocialButton",
            "/UserInterface/QuickMenu/ShortcutMenu/SafetyButton"
        };

        public static List<string> SecondLine = new List<string>
        {
            "/UserInterface/QuickMenu/ShortcutMenu/GoHomeButton",
            "/UserInterface/QuickMenu/ShortcutMenu/RespawnButton",
            "/UserInterface/QuickMenu/ShortcutMenu/SettingsButton",
            "/UserInterface/QuickMenu/ShortcutMenu/ReportWorldButton"
        };

        public static List<string> ThirdLine = new List<string>
        {
            "/UserInterface/QuickMenu/ShortcutMenu/UIElementsButton",
            "/UserInterface/QuickMenu/ShortcutMenu/CameraButton",
            "/UserInterface/QuickMenu/ShortcutMenu/EmoteButton",
            "/UserInterface/QuickMenu/ShortcutMenu/EmojiButton"
        };

        public static List<string> ToolTipAndUserOver = new List<string>
        {
            "/UserInterface/QuickMenu/QuickMenu_NewElements/_CONTEXT/QM_Context_ToolTip",
            "/UserInterface/QuickMenu/QuickMenu_NewElements/_CONTEXT/QM_Context_User_Hover",
            "/UserInterface/QuickMenu/QuickMenu_NewElements/_CONTEXT/QM_Context_User_Selected"
        };
        public static IEnumerator Initialize()
        {
            while (VRCPlayer.field_Internal_Static_VRCPlayer_0 != true)
            {
                yield return null;
            }

            var KickButton = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/KickButton");
            var WarnButton = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/WarnButton");
            var PlaylistButton = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/ViewPlaylistsButton");
            var ReportUserButton = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/ReportAbuseButton");
            var StatsButton = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/ShowAvatarStatsButton");
            var ReportUserText = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/ReportAbuseButton/Text");
            var KickButtonText = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/KickButton/Text");
            var WarnButtonText = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/WarnButton/Text");
            var PlaylistButtonText = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/ViewPlaylistsButton/Text");
            var StatsButtonText = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/ShowAvatarStatsButton/Text");
            var StatsButtonImage = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/ShowAvatarStatsButton/Image");
            var MicOffButton = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/MicOffButton");
            var FirstPanel = GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_Background/Panel");
            var SecondPanel = GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_CONTEXT/QM_Context_ToolTip/Panel");
            var ThirdPanel = GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_CONTEXT/QM_Context_User_Hover/Panel");
            var FourthPanel = GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_InfoBar/Panel");
            var CloneAvatarButton = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/CloneAvatarButton");
            var CloneAvatarText = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu/CloneAvatarButton/Text");
            var GoHome = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/GoHomeButton");
            var RepsawnButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/RespawnButton");
            var SettingsButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/SettingsButton");
            var ReportButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/ReportWorldButton");
            var SitButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/SitButton");
            var CalibrateButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/CalibrateButton");
            var RankToggleButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/Toggle_States_ShowTrustRank_Colors");
            var MicButton = GameObject.Find("/UserInterface/QuickMenu/MicControls/MicButton");
            var BuildText = GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_InfoBar/BuildNumText");
            var FpsText = GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_InfoBar/FPSText");
            var PingText = GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_InfoBar/PingText");
            var WorldText = GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_InfoBar/WorldText");
            var MicText = GameObject.Find("/UserInterface/QuickMenu/MicControls/MuteText");
            var VRCPlusMiniBanner = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/VRCPlusMiniBanner");
            var VRCPlusThankYou = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/VRCPlusThankYou");
            var VRCPlusUserIconButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/UserIconButton");
            var VRCPlusUserIconCameraButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/UserIconCameraButton");
            var VRCPlusHeaderContainerr = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/HeaderContainer/VRCPlusBanner");
            var GalleryButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/GalleryButton");
            var ArrowLeft = GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_CONTEXT/QM_Context_User_Selected/PreviousArrow_Button");
            var ArrowRight = GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_CONTEXT/QM_Context_User_Selected/NextArrow_Button");
            var HomeNotifTabs = GameObject.Find("/UserInterface/QuickMenu/QuickModeTabs");
            var InviteTab = GameObject.Find("/UserInterface/QuickMenu/QuickModeTabs/NotificationsTab");
            var HomeTab = GameObject.Find("/UserInterface/QuickMenu/QuickModeTabs/HomeTab");

            Object.Destroy(GalleryButton);
            UnityEngine.Object.Destroy(VRCPlusMiniBanner);
            UnityEngine.Object.Destroy(VRCPlusUserIconButton);
            UnityEngine.Object.Destroy(VRCPlusUserIconCameraButton);
            UnityEngine.Object.Destroy(VRCPlusHeaderContainerr);

            WorldText.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            BuildText.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            FpsText.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            PingText.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

            WorldText.GetComponent<Text>().color = Settings.MenuColor();
            BuildText.GetComponent<Text>().color = Settings.MenuColor();
            FpsText.GetComponent<Text>().color = Settings.MenuColor();
            PingText.GetComponent<Text>().color = Settings.MenuColor();

            WorldText.GetComponent<Text>().fontSize = 50; 
            BuildText.GetComponent<Text>().fontSize = 50;
            FpsText.GetComponent<Text>().fontSize = 50;
            PingText.GetComponent<Text>().fontSize = 50;

            WorldText.gameObject.transform.parent = EvolveMenu.LowerPanel.getGameObject().transform;
            BuildText.gameObject.transform.parent = EvolveMenu.LowerPanel.getGameObject().transform;
            FpsText.gameObject.transform.parent = EvolveMenu.LowerPanel.getGameObject().transform;
            PingText.gameObject.transform.parent = EvolveMenu.LowerPanel.getGameObject().transform;

            WorldText.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(-450, 310);
            BuildText.gameObject.GetComponent<RectTransform>().anchoredPosition = WorldText.gameObject.GetComponent<RectTransform>().anchoredPosition - new Vector2(210, 65);
            PingText.gameObject.GetComponent<RectTransform>().anchoredPosition = WorldText.gameObject.GetComponent<RectTransform>().anchoredPosition - new Vector2(290, 65 * 2);
            FpsText.gameObject.GetComponent<RectTransform>().anchoredPosition = WorldText.gameObject.GetComponent<RectTransform>().anchoredPosition - new Vector2(290, 65 * 3);

            MicText.gameObject.GetComponent<RectTransform>().anchoredPosition -= new Vector2(105, 0);

            HomeNotifTabs.GetComponent<RectTransform>().anchoredPosition += new Vector2(999999, 999999);
            HomeNotifTabs.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0.08f);
            Object.Destroy(HomeTab);
            Object.Destroy(InviteTab);

            MicButton.gameObject.GetComponent<RectTransform>().anchoredPosition -= new Vector2(105, 0);
            RankToggleButton.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 12 * 105);
            CalibrateButton.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(8 * 105, 4 * -105);
            SitButton.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(8 * 105, 4 * -105);
            RepsawnButton.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(1260, 630);
            GoHome.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(1680, 420);
            ReportButton.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 210);
            SettingsButton.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(420, 0);

            KickButton.GetComponent<RectTransform>().anchoredPosition = WarnButton.GetComponent<RectTransform>().anchoredPosition - new Vector2(0, 105);
            KickButton.GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            WarnButton.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 105);
            WarnButton.GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            PlaylistButton.GetComponent<RectTransform>().anchoredPosition = WarnButton.GetComponent<RectTransform>().anchoredPosition - new Vector2(420, 0);
            PlaylistButton.GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            StatsButton.GetComponent<RectTransform>().anchoredPosition = PlaylistButton.GetComponent<RectTransform>().anchoredPosition - new Vector2(0, 210);
            StatsButton.GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            ReportUserButton.GetComponent<RectTransform>().anchoredPosition = StatsButton.GetComponent<RectTransform>().anchoredPosition - new Vector2(0, 210);
            ReportUserButton.GetComponent<RectTransform>().sizeDelta /= new Vector2(1,2);
            MicOffButton.GetComponent<RectTransform>().anchoredPosition += new Vector2(420,0);
            ReportUserText.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 105);
            KickButtonText.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 105);
            WarnButtonText.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 105);
            PlaylistButtonText.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 105);
            StatsButtonText.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 105);
            ReportUserText.GetComponent<Text>().text = "Report";
            PlaylistButtonText.GetComponent<Text>().text = "Playlists";
            StatsButtonText.GetComponent<Text>().text = "Stats";
            StatsButtonImage.transform.position = new Vector3(999 * 999, 0, 0);

            foreach (string Buttons in ShortcutMenuButtons)
            {
                GameObject MenuButton = GameObject.Find(Buttons);
                MenuButton.GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            }

            foreach (string Buttons in FirstLine)
            {
                GameObject MenuButton = GameObject.Find(Buttons);
                MenuButton.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 315);
            }

            foreach (string Buttons in SecondLine)
            {
                GameObject MenuButton = GameObject.Find(Buttons);
                MenuButton.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 105);
            }

            foreach (string Buttons in ThirdLine)
            {
                GameObject MenuButton = GameObject.Find(Buttons);
            }

            foreach (string Buttons in ToolTipAndUserOver)
            {
                GameObject MenuButton = GameObject.Find(Buttons);
                MenuButton.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 2 * 105);
            }

            UnityEngine.Object.Destroy(FirstPanel);
            UnityEngine.Object.Destroy(SecondPanel);
            UnityEngine.Object.Destroy(ThirdPanel);
            FourthPanel.transform.localScale = new Vector3(0, 0, 0);
            FourthPanel.transform.position = new Vector3(999 * 999, 999 * 999, 999 * 999);
            var EarlyText = GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_InfoBar/EarlyAccessText");
            EarlyText.SetActive(false);
            UnityEngine.Object.Destroy(GameObject.Find("/UserInterface/QuickMenu/QuickMenu_NewElements/_InfoBar/NameText"));
            CloneAvatarButton.GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
            CloneAvatarButton.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 105);
            CloneAvatarText.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 105);

            Wrappers.Utils.QuickMenu.GetComponent<BoxCollider>().size += new Vector3(0, 500, 0);
            Wrappers.Utils.QuickMenu.GetComponent<BoxCollider>().transform.position -= new Vector3(0, 100, 0);

            MelonCoroutines.Start(WorldButtonChanges());
            IEnumerator WorldButtonChanges()
            {
                var shortcutmenu = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu");
                while (shortcutmenu.active == false)
                {
                    yield return null;
                }

                {
                    var WorldButton = GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/WorldsButton");
                    WorldButton.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 315);
                    WorldButton.GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
                }
            }
        }

        public static IEnumerator Initialize2()
        {
            for (; ; )
            {
                try
                {
                    var HomeNotifTabs = GameObject.Find("/UserInterface/QuickMenu/QuickModeTabs");
                    var MicButton = GameObject.Find("/UserInterface/QuickMenu/MicControls/MicButton");
                    var MicText = GameObject.Find("/UserInterface/QuickMenu/MicControls/MuteText");
                    var shortcutmenu = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu");
                    var LobbyMenu = GameObject.Find("/UserInterface/QuickMenu/EvolveEvolveShortcutMenuEvolveLobby");
                    var SelfMenu = GameObject.Find("/UserInterface/QuickMenu/EvolveEvolveShortcutMenuEvolveSelf");
                    var UserMenu = GameObject.Find("/UserInterface/QuickMenu/UserInteractMenu");
                    var RankToggleButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/Toggle_States_ShowTrustRank_Colors");
                    var Message = GameObject.Find("UserInterface/QuickMenu/NotificationInteractMenu/Message");
                    var NotifMenu = GameObject.Find("UserInterface/QuickMenu/NotificationInteractMenu");
                    if (NotifMenu.active == true)
                    {
                        if (Message.active != true) Message.SetActive(true); 
                    }
                    if (shortcutmenu.gameObject.active)
                    {
                        MicButton.SetActive(true);
                        MicText.SetActive(true);
                        HomeNotifTabs.SetActive(true);
                    }
                    else
                    {
                        MicButton.SetActive(false);
                        MicText.SetActive(false);
                        HomeNotifTabs.SetActive(false);
                    }

                    if (LobbyMenu.gameObject.active == true)
                    {
                        PlayerListObject.SetActive(true);
                    }
                    else
                    {
                        PlayerListObject.SetActive(false);
                    }

                    if (SelfMenu.gameObject.active == true)
                    {
                        SelfInfoObject.SetActive(true);
                    }
                    else
                    {
                        SelfInfoObject.SetActive(false);
                    }

                    if (UserMenu.gameObject.active == true)
                    {
                        UserInfoObject.SetActive(true);
                    }
                    else
                    {
                        UserInfoObject.SetActive(false);
                    }
                }
                catch { }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}