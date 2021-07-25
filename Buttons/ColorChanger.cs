using Evolve.Utils;
using ButtonApi;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Evolve.Buttons
{
    internal static class ColorChanger
    {
        private static List<Image> MainImageColor;
        private static List<Image> DimmerImageColor;
        private static List<Image> DarkerImageColor;
        private static List<Text> SecondaryColor;
        private static bool setupSkybox;
        private static GameObject loadingBackground;
        private static GameObject initialLoadingBackground;

        public static string B64TexturesGradient = "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";

        public static void ApplyNewColor()
        {
            Color SecondColor = Settings.TextColor();
            Color color = Settings.MenuColor();
            Color color2 = new Color(color.r, color.g, color.b, 0.4f);
            new Color(color.r / 0.75f, color.g / 0.75f, color.b / 0.75f);
            Color color3 = new Color(color.r / 0.75f, color.g / 0.75f, color.b / 0.75f, 0.9f);
            Color color4 = new Color(color.r / 2.5f, color.g / 2.5f, color.b / 2.5f);
            Color color5 = new Color(color.r / 2.8f, color.g / 2.8f, color.b / 2.8f, 0.25f);
            if (ColorChanger.MainImageColor == null || ColorChanger.MainImageColor.Count == 0)
            {
                ColorChanger.MainImageColor = new List<Image>();
                GameObject menuContent = QMStuff.GetVRCUiMInstance().field_Public_GameObject_0;
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Screens/Settings_Safety/_Description_SafetyLevel").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Custom/ON").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_None/ON").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Normal/ON").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Maxiumum/ON").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/InputKeypadPopup/Rectangle/Panel").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/InputKeypadPopup/InputField").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/StandardPopupV2/Popup/Panel").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/StandardPopup/InnerDashRing").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/StandardPopup/RingGlow").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/UpdateStatusPopup/Popup/Panel").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/InputPopup/InputField").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/UpdateStatusPopup/Popup/InputFieldStatus").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/AdvancedSettingsPopup/Popup/Panel").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/AddToPlaylistPopup/Popup/Panel").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/BookmarkFriendPopup/Popup/Panel (2)").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/EditPlaylistPopup/Popup/Panel").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/PerformanceSettingsPopup/Popup/Panel").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/AlertPopup/Lighter").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/RoomInstancePopup/Popup/Panel").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/ReportWorldPopup/Popup/Panel").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/ReportUserPopup/Popup/Panel").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/SearchOptionsPopup/Popup/Panel (1)").GetComponent<Image>());
                //ColorChanger.normalColorImage.Add(menuContent.transform.Find("Screens/UserInfo/User Panel/Panel (1)").GetComponent<Image>());
                ColorChanger.MainImageColor.Add(GameObject.Find("UserInterface/QuickMenu/QuickModeMenus/QuickModeNotificationsMenu").GetComponentInChildren<Image>());

                foreach (Transform transform in from x in menuContent.GetComponentsInChildren<Transform>(true)
                                                where x.name.Contains("Panel_Header")
                                                select x)
                {
                    foreach (Image item in transform.GetComponentsInChildren<Image>())
                    {
                        ColorChanger.MainImageColor.Add(item);
                    }
                }
                foreach (Transform transform2 in from x in menuContent.GetComponentsInChildren<Transform>(true)
                                                 where x.name.Contains("Handle")
                                                 select x)
                {
                    foreach (Image item2 in transform2.GetComponentsInChildren<Image>())
                    {
                        ColorChanger.MainImageColor.Add(item2);
                    }
                }
                try
                {
                    ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>());
                    ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>());
                    ColorChanger.MainImageColor.Add(menuContent.transform.Find("Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").GetComponent<Image>());
                }
                catch (Exception)
                {
                    new Exception();
                }
            }
            if (ColorChanger.DimmerImageColor == null || ColorChanger.DimmerImageColor.Count == 0)
            {
                ColorChanger.DimmerImageColor = new List<Image>();
                GameObject menuContent2 = QMStuff.GetVRCUiMInstance().field_Public_GameObject_0;
                ColorChanger.DimmerImageColor.Add(menuContent2.transform.Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Custom/ON/TopPanel_SafetyLevel").GetComponent<Image>());
                ColorChanger.DimmerImageColor.Add(menuContent2.transform.Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_None/ON/TopPanel_SafetyLevel").GetComponent<Image>());
                ColorChanger.DimmerImageColor.Add(menuContent2.transform.Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Normal/ON/TopPanel_SafetyLevel").GetComponent<Image>());
                ColorChanger.DimmerImageColor.Add(menuContent2.transform.Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Maxiumum/ON/TopPanel_SafetyLevel").GetComponent<Image>());
                foreach (Transform transform3 in from x in menuContent2.GetComponentsInChildren<Transform>(true)
                                                 where x.name.Contains("Fill")
                                                 select x)
                {
                    foreach (Image item3 in transform3.GetComponentsInChildren<Image>())
                    {
                        ColorChanger.DimmerImageColor.Add(item3);
                    }
                }
            }
            if (ColorChanger.DarkerImageColor == null || ColorChanger.DarkerImageColor.Count == 0)
            {
                ColorChanger.DarkerImageColor = new List<Image>();
                GameObject menuContent3 = QMStuff.GetVRCUiMInstance().field_Public_GameObject_0;
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/InputKeypadPopup/Rectangle").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/StandardPopupV2/Popup/BorderImage").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/StandardPopup/Rectangle").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/StandardPopup/MidRing").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/UpdateStatusPopup/Popup/BorderImage").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/AdvancedSettingsPopup/Popup/BorderImage").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/AddToPlaylistPopup/Popup/BorderImage").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/BookmarkFriendPopup/Popup/BorderImage").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/EditPlaylistPopup/Popup/BorderImage").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/PerformanceSettingsPopup/Popup/BorderImage").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/RoomInstancePopup/Popup/BorderImage").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/RoomInstancePopup/Popup/BorderImage (1)").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/ReportWorldPopup/Popup/BorderImage").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/ReportUserPopup/Popup/BorderImage").GetComponent<Image>());
                ColorChanger.DarkerImageColor.Add(menuContent3.transform.Find("Popups/SearchOptionsPopup/Popup/BorderImage").GetComponent<Image>());
                foreach (Transform transform4 in from x in menuContent3.GetComponentsInChildren<Transform>(true)
                                                 where x.name.Contains("Background")
                                                 select x)
                {
                    foreach (Image item4 in transform4.GetComponentsInChildren<Image>())
                    {
                        ColorChanger.DarkerImageColor.Add(item4);
                    }
                }
            }
            if (ColorChanger.SecondaryColor == null || ColorChanger.SecondaryColor.Count == 0)
            {
                ColorChanger.SecondaryColor = new List<Text>();
                GameObject menuContent4 = QMStuff.GetVRCUiMInstance().field_Public_GameObject_0;
                foreach (Text item5 in menuContent4.transform.Find("Popups/InputPopup/Keyboard/Keys").GetComponentsInChildren<Text>(true))
                {
                    ColorChanger.SecondaryColor.Add(item5);
                }
                foreach (Text item6 in menuContent4.transform.Find("Popups/InputKeypadPopup/Keyboard/Keys").GetComponentsInChildren<Text>(true))
                {
                    ColorChanger.SecondaryColor.Add(item6);
                }
            }
            foreach (Image image in ColorChanger.MainImageColor)
            {
                image.color = color;
            }
            foreach (Image image2 in ColorChanger.DimmerImageColor)
            {
                image2.color = color;
            }
            foreach (Image image3 in ColorChanger.DarkerImageColor)
            {
                image3.color = color5;
            }
            foreach (Text text in ColorChanger.SecondaryColor)
            {
                text.color = SecondColor;
            }
            if (!ColorChanger.setupSkybox)
            {
                try
                {
                    var Gradiant = new Texture2D(16, 16);
                    ImageConversion.LoadImage(Gradiant, Convert.FromBase64String(B64TexturesGradient), false);
                    ColorChanger.loadingBackground = QMStuff.GetVRCUiMInstance().field_Public_GameObject_0.transform.Find("Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").gameObject;
                    ColorChanger.loadingBackground.GetComponent<MeshRenderer>().material.SetTexture("_Tex", ReplaceCubemap.BuildCubemap(Gradiant));
                    ColorChanger.loadingBackground.GetComponent<MeshRenderer>().material.SetColor("_Tint", Color.black);
                    ColorChanger.loadingBackground.GetComponent<MeshRenderer>().material.SetTexture("_Tex", ReplaceCubemap.BuildCubemap(Gradiant));
                    ColorChanger.setupSkybox = true;
                    ColorChanger.initialLoadingBackground = GameObject.Find("LoadingBackground_TealGradient_Music/SkyCube_Baked");
                    ColorChanger.initialLoadingBackground.GetComponent<MeshRenderer>().material.SetTexture("_Tex", ReplaceCubemap.BuildCubemap(Gradiant));
                    ColorChanger.initialLoadingBackground.GetComponent<MeshRenderer>().material.SetColor("_Tint", Color.black);
                    ColorChanger.initialLoadingBackground.GetComponent<MeshRenderer>().material.SetTexture("_Tex", ReplaceCubemap.BuildCubemap(Gradiant));
                }
                catch (Exception)
                {
                }
            }
            if (ColorChanger.setupSkybox && ColorChanger.loadingBackground != null)
            {
                ColorChanger.loadingBackground.GetComponent<MeshRenderer>().material.SetColor("_Tint", Color.black);
            }
            ColorBlock colors = new ColorBlock
            {
                colorMultiplier = 1f,
                disabledColor = Color.black,
                highlightedColor = SecondColor * 1.5f,
                normalColor = color,
                pressedColor = SecondColor / 1.5f,
                fadeDuration = 0.5f,
            };
            color.a = 1;
            if (UnityEngine.Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>().Count != 0)
            {
                UnityEngine.Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>().FirstOrDefault<HighlightsFXStandalone>().highlightColor = color;
            }
            try
            {
                if (Settings.UIMicIconColorChangingEnabled)
                {
                    using (IEnumerator<Image> enumerator2 = UnityEngine.Object.FindObjectOfType<HudVoiceIndicator>().transform.GetComponentsInChildren<Image>().GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            Image image4 = enumerator2.Current;
                            if (image4.gameObject.name != "PushToTalkKeybd" && image4.gameObject.name != "PushToTalkXbox")
                            {
                                image4.color = color;
                            }
                        }
                        foreach (HudVoiceIndicator hudVoiceIndicator in UnityEngine.Object.FindObjectsOfType<HudVoiceIndicator>())
                        {
                            hudVoiceIndicator.transform.Find("VoiceDotDisabled").GetComponent<FadeCycleEffect>().enabled = true;
                        }
                    }
                }
                foreach (Image image5 in UnityEngine.Object.FindObjectOfType<HudVoiceIndicator>().transform.GetComponentsInChildren<Image>())
                {
                    if (image5.gameObject.name != "PushToTalkKeybd" && image5.gameObject.name != "PushToTalkXbox")
                    {
                        image5.color = color;
                    }
                }
            }
            catch (Exception)
            {
                new Exception();
            }
            if (QMStuff.GetVRCUiMInstance().field_Public_GameObject_0 != null)
            {
                GameObject gameObject = QMStuff.GetVRCUiMInstance().field_Public_GameObject_0;
                try
                {
                    Transform transform5 = gameObject.transform.Find("Popups/InputPopup");
                    color4.a = 0.8f;
                    transform5.Find("Rectangle").GetComponent<Image>().color = color4;
                    color4.a = 0.5f;
                    color.a = 0.8f;
                    transform5.Find("Rectangle/Panel").GetComponent<Image>().color = color;
                    color.a = 0.5f;
                    Transform transform6 = gameObject.transform.Find("Backdrop/Header/Tabs/ViewPort/Content/Search");
                    transform6.Find("SearchTitle").GetComponent<Text>().color = color;
                    transform6.Find("InputField").GetComponent<Image>().color = color;
                }
                catch (Exception)
                {
                }
                try
                {
                    ColorBlock colors2 = new ColorBlock
                    {
                        colorMultiplier = 1f,
                        disabledColor = Color.black,
                        highlightedColor = SecondColor * 1.5f,
                        normalColor = color,
                        pressedColor = SecondColor / 1.5f,
                        fadeDuration = 0.5f,
                    };
                    gameObject.GetComponentsInChildren<Transform>(true).FirstOrDefault((Transform x) => x.name == "Row:4 Column:0").GetComponent<Button>().colors = colors;
                    color.a = 0.5f;
                    color4.a = 1f;
                    colors2.normalColor = color4;
                    foreach (Slider slider in gameObject.GetComponentsInChildren<Slider>(true))
                    {
                        slider.colors = colors2;
                    }
                    color4.a = 0.5f;
                    colors2.normalColor = color;
                    foreach (Button button in gameObject.GetComponentsInChildren<Button>(true))
                    {
                        button.colors = colors;
                    }
                    gameObject = GameObject.Find("QuickMenu");
                    foreach (Button button2 in gameObject.GetComponentsInChildren<Button>(true))
                    {
                        if (button2.gameObject.name != "rColorButton" && button2.gameObject.name != "gColorButton" && button2.gameObject.name != "bColorButton" && button2.gameObject.name != "ColorPickPreviewButton" && button2.transform.parent.parent.name != "EmojiMenu" && button2.gameObject.name != "EvolveImageButton" && button2.gameObject.name != "EvolveConsole")
                        {
                            button2.colors = colors;
                        }
                    }
                    foreach (UiToggleButton uiToggleButton in gameObject.GetComponentsInChildren<UiToggleButton>(true))
                    {
                        foreach (Image image6 in uiToggleButton.GetComponentsInChildren<Image>(true))
                        {
                            image6.color = color * 1.1f;
                        }
                    }
                    foreach (Slider slider2 in gameObject.GetComponentsInChildren<Slider>(true))
                    {
                        slider2.colors = colors2;
                        foreach (Image image7 in slider2.GetComponentsInChildren<Image>(true))
                        {
                            image7.color = color;
                        }
                    }
                    foreach (Toggle toggle in gameObject.GetComponentsInChildren<Toggle>(true))
                    {
                        toggle.colors = colors2;
                        foreach (Image image8 in toggle.GetComponentsInChildren<Image>(true))
                        {
                            image8.color = SecondColor;
                        }
                    }
                }
                catch (Exception)
                {
                    new Exception();
                }
                VRCUiCursorManager.field_Private_Static_VRCUiCursorManager_0.field_Public_VRCUiCursor_0.field_Public_Color_0 = SecondColor;
                VRCUiCursorManager.field_Private_Static_VRCUiCursorManager_0.field_Public_VRCUiCursor_1.field_Public_Color_0 = SecondColor;
                VRCUiCursorManager.field_Private_Static_VRCUiCursorManager_0.field_Public_VRCUiCursor_2.field_Public_Color_0 = SecondColor;
                VRCUiCursorManager.field_Private_Static_VRCUiCursorManager_0.field_Public_VRCUiCursor_3.field_Public_Color_0 = SecondColor;
                VRCUiCursorManager.field_Private_Static_VRCUiCursorManager_0.field_Public_VRCUiCursor_4.field_Public_Color_0 = SecondColor;

                //All the quickmenu text
                foreach (Text text in QuickMenu.prop_QuickMenu_0.GetComponentsInChildren<Text>(true))
                {
                    text.color = new Color(SecondColor.r * 1.25f, SecondColor.g * 1.25f, SecondColor.b * 1.25f);
                    text.fontStyle = FontStyle.Italic;
                }

                //all Menu text
                foreach (Text text in GameObject.Find("/UserInterface").GetComponentsInChildren<Text>(true))
                {
                    text.color = new Color(SecondColor.r * 1.25f, SecondColor.g * 1.25f, SecondColor.b * 1.25f);
                }

                if (Settings.UIActionMenuColorChangingEnabled)
                {
                    try
                    {
                        foreach (PedalGraphic pedalGraphic in UnityEngine.Resources.FindObjectsOfTypeAll<PedalGraphic>())
                        {
                            pedalGraphic.color = color;
                        }
                        foreach (ActionMenu actionMenu in UnityEngine.Resources.FindObjectsOfTypeAll<ActionMenu>())
                        {
                            actionMenu.field_Public_FollowTrackingTarget_0.GetComponentInChildren<Image>().color = SecondColor;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}