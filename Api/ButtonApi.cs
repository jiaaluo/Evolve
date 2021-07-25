using Evolve.Utils;
using Il2CppSystem.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ButtonApi
{
    internal static class QMButtonAPI
    {
        public static string identifier = "Evolve";
        public static Color mBackground = Color.red;
        public static Color mForeground = Color.white;
        public static Color bBackground = Color.red;
        public static Color bForeground = Color.yellow;
        public static List<QMSingleButton> allSingleButtons = new List<QMSingleButton>();
        public static List<QMToggleButton> allToggleButtons = new List<QMToggleButton>();
        public static List<QMNestedButton> allNestedButtons = new List<QMNestedButton>();
    }

    internal class QMButtonBase
    {
        protected GameObject button;
        protected string btnQMLoc;
        protected string btnType;
        protected string btnTag;
        protected int[] initShift = { 0, 0 };
        protected Color OrigBackground;
        protected Color OrigText;

        public GameObject getGameObject()
        {
            return button;
        }

        public void setActive(bool isActive)
        {
            button.gameObject.SetActive(isActive);
        }

        public void setIntractable(bool isIntractable)
        {
            if (isIntractable)
            {
                setBackgroundColor(Settings.MenuColor(), false);
                setTextColor(Settings.TextColor(), false);
            }
            else setBackgroundColor(Color.grey / 2, false);

            button.gameObject.GetComponent<Button>().interactable = isIntractable;
        }

        public void setLocation(float buttonXLoc, float buttonYLoc)
        {
            button.GetComponent<RectTransform>().anchoredPosition += Vector2.right * (420 * (buttonXLoc + initShift[0]));
            button.GetComponent<RectTransform>().anchoredPosition += Vector2.down * (420 * (buttonYLoc + initShift[1]));

            btnTag = "(" + buttonXLoc + "," + buttonYLoc + ")";
            button.name = btnQMLoc + "/" + btnType + btnTag;
            button.GetComponent<Button>().name = btnType + btnTag;
        }

        public void setToolTip(string buttonToolTip)
        {
            button.GetComponent<UiTooltip>().field_Public_String_0 = buttonToolTip;
            button.GetComponent<UiTooltip>().field_Public_String_1 = buttonToolTip;
        }

        public void DestroyMe()
        {
            try
            {
                UnityEngine.Object.Destroy(button);
            }
            catch { }
        }

        public virtual void setBackgroundColor(Color buttonBackgroundColor, bool save = true)
        {
        }

        public virtual void setTextColor(Color buttonTextColor, bool save = true)
        {
        }
    }

    internal class QMSingleButton : QMButtonBase
    {
        public QMSingleButton(QMNestedButton btnMenu, float btnXLocation, float btnYLocation, string btnText, System.Action btnAction, string btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null)
        {
            btnQMLoc = btnMenu.getMenuName();
            initButton(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip, btnBackgroundColor, btnTextColor);
        }

        public QMSingleButton(string btnMenu, float btnXLocation, float btnYLocation, string btnText, System.Action btnAction, string btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null)
        {
            btnQMLoc = btnMenu;
            initButton(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip, btnBackgroundColor, btnTextColor);
        }

        private void initButton(float btnXLocation, float btnYLocation, string btnText, System.Action btnAction, string btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null)
        {
            btnType = "SingleButton";
            button = UnityEngine.Object.Instantiate(QMStuff.SingleButtonTemplate(), QMStuff.GetQuickMenuInstance().transform.Find(btnQMLoc), true);

            initShift[0] = -1;
            initShift[1] = 0;
            setLocation(btnXLocation, btnYLocation);
            setButtonText(btnText);
            setToolTip(btnToolTip);
            setAction(btnAction);

            if (btnBackgroundColor != null)
            {
                setBackgroundColor((Color) btnBackgroundColor);
            }
            else
            {
                OrigBackground = Settings.MenuColor();
            }

            if (btnTextColor != null)
            {
                setTextColor((Color) btnTextColor);
            }
            else
            {
                OrigText = Settings.TextColor();
            }

            setActive(true);
            QMButtonAPI.allSingleButtons.Add(this);
        }

        public void setButtonText(string buttonText)
        {
            button.GetComponentInChildren<Text>().text = buttonText;
        }

        public void setAction(System.Action buttonAction)
        {
            button.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            if (buttonAction != null)
            {
                button.GetComponent<Button>().onClick.AddListener(UnhollowerRuntimeLib.DelegateSupport.ConvertDelegate<UnityAction>(buttonAction));
            }
        }

        public override void setBackgroundColor(Color buttonBackgroundColor, bool save = true)
        {
            if (save)
            {
                OrigBackground = Settings.MenuColor();
            }
            button.GetComponentInChildren<UnityEngine.UI.Button>().colors = new ColorBlock()
            {
                colorMultiplier = 1f,
                disabledColor = Color.grey,
                highlightedColor = buttonBackgroundColor * 1.5f,
                normalColor = buttonBackgroundColor / 1.5f,
                pressedColor = Color.grey * 1.5f
            };
        }

        public override void setTextColor(Color buttonTextColor, bool save = true)
        {
            button.GetComponentInChildren<Text>().color = buttonTextColor;
            if (save)
            {
                OrigText = Settings.TextColor();
            }
        }
    }

    internal class QMToggleButton : QMButtonBase
    {
        public GameObject btnOn;
        public GameObject btnOff;
        public List<QMButtonBase> showWhenOn = new List<QMButtonBase>();
        public List<QMButtonBase> hideWhenOn = new List<QMButtonBase>();
        public bool shouldSaveInConfig = false;

        private System.Action btnOnAction = null;
        private System.Action btnOffAction = null;

        public QMToggleButton(QMNestedButton btnMenu, float btnXLocation, float btnYLocation, string btnTextOn, System.Action btnActionOn, string btnTextOff, System.Action btnActionOff, string btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null, bool shouldSaveInConfig = false, bool defaultPosition = false)
        {
            btnQMLoc = btnMenu.getMenuName();
            initButton(btnXLocation, btnYLocation, btnTextOn, btnActionOn, btnTextOff, btnActionOff, btnToolTip, btnBackgroundColor, btnTextColor, shouldSaveInConfig, defaultPosition);
        }

        public QMToggleButton(string btnMenu, int btnXLocation, int btnYLocation, string btnTextOn, System.Action btnActionOn, string btnTextOff, System.Action btnActionOff, string btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null, bool shouldSaveInConfig = false, bool defaultPosition = false)
        {
            btnQMLoc = btnMenu;
            initButton(btnXLocation, btnYLocation, btnTextOn, btnActionOn, btnTextOff, btnActionOff, btnToolTip, btnBackgroundColor, btnTextColor, shouldSaveInConfig, defaultPosition);
        }

        private void initButton(float btnXLocation, float btnYLocation, string btnTextOn, System.Action btnActionOn, string btnTextOff, System.Action btnActionOff, string btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null, bool shouldSaveInConf = false, bool defaultPosition = false)
        {
            btnType = "ToggleButton";
            button = UnityEngine.Object.Instantiate<GameObject>(QMStuff.ToggleButtonTemplate(), QMStuff.GetQuickMenuInstance().transform.Find(btnQMLoc), true);

            btnOn = button.transform.Find("Toggle_States_Visible/ON").gameObject;
            btnOff = button.transform.Find("Toggle_States_Visible/OFF").gameObject;
            initShift[0] = -3;
            initShift[1] = -1;
            setLocation(btnXLocation, btnYLocation);

            setOnText(btnTextOn);
            setOffText(btnTextOff);
            Text[] btnTextsOn = btnOn.GetComponentsInChildren<Text>();
            btnTextsOn[0].supportRichText = true;
            btnTextsOn[0].name = "Text_ON";
            btnTextsOn[0].resizeTextForBestFit = true;
            btnTextsOn[1].supportRichText = true;
            btnTextsOn[1].name = "Text_OFF";
            btnTextsOn[1].resizeTextForBestFit = true;
            Text[] btnTextsOff = btnOff.GetComponentsInChildren<Text>();
            btnTextsOff[0].supportRichText = true;
            btnTextsOff[0].name = "Text_ON";
            btnTextsOff[0].resizeTextForBestFit = true;
            btnTextsOff[1].supportRichText = true;
            btnTextsOff[1].name = "Text_OFF";
            btnTextsOff[1].resizeTextForBestFit = true;

            setToolTip(btnToolTip);
            setAction(btnActionOn, btnActionOff);
            btnOn.SetActive(false);
            btnOff.SetActive(true);

            if (btnBackgroundColor != null)
            {
                setBackgroundColor((Color) btnBackgroundColor);
            }
            else
            {
                OrigBackground = Settings.MenuColor();
            }

            if (btnTextColor != null)
            {
                setTextColor((Color) btnTextColor);
            }
            else
            {
                OrigText = Settings.TextColor();
            }

            setActive(true);
            shouldSaveInConfig = shouldSaveInConf;
            if (defaultPosition == true)  
            {
                setToggleState(true, true);
            }

            QMButtonAPI.allToggleButtons.Add(this);
        }

        public override void setBackgroundColor(Color buttonBackgroundColor, bool save = true)
        {
            UnityEngine.UI.Image[] btnBgColorList = ((btnOn.GetComponentsInChildren<UnityEngine.UI.Image>()).Concat(btnOff.GetComponentsInChildren<UnityEngine.UI.Image>()).ToArray()).Concat(button.GetComponentsInChildren<UnityEngine.UI.Image>()).ToArray();
            foreach (UnityEngine.UI.Image btnBackground in btnBgColorList)
            {
                btnBackground.color = buttonBackgroundColor;
            }

            if (save)
            {
                OrigBackground = Settings.MenuColor();
            }
        }

        public override void setTextColor(Color buttonTextColor, bool save = true)
        {
            Text[] btnTxtColorList = (btnOn.GetComponentsInChildren<Text>()).Concat(btnOff.GetComponentsInChildren<Text>()).ToArray();
            foreach (Text btnText in btnTxtColorList)
            {
                btnText.color = buttonTextColor;
            }

            if (save)
            {
                OrigText = Settings.TextColor();
            }
        }

        public void setAction(System.Action buttonOnAction, System.Action buttonOffAction)
        {
            btnOnAction = buttonOnAction;
            btnOffAction = buttonOffAction;

            button.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            button.GetComponent<Button>().onClick.AddListener(UnhollowerRuntimeLib.DelegateSupport.ConvertDelegate<UnityAction>((System.Action) (() =>
           {
               if (btnOn.activeSelf)
               {
                   setToggleState(false, true);
               }
               else
               {
                   setToggleState(true, true);
               }
           })));
        }

        public void setToggleState(bool toggleOn, bool shouldInvoke = false)
        {
            btnOn.SetActive(toggleOn);
            btnOff.SetActive(!toggleOn);
            try
            {
                if (toggleOn && shouldInvoke)
                {
                    btnOnAction.Invoke();
                    showWhenOn.ForEach(x => x.setActive(true));
                    hideWhenOn.ForEach(x => x.setActive(false));
                }
                else if (!toggleOn && shouldInvoke)
                {
                    btnOffAction.Invoke();
                    showWhenOn.ForEach(x => x.setActive(false));
                    hideWhenOn.ForEach(x => x.setActive(true));
                }
            }
            catch { }

            if (shouldSaveInConfig)
            {
            }
        }

        public string getOnText()
        {
            return btnOn.GetComponentsInChildren<Text>()[0].text;
        }

        public void setOnText(string buttonOnText)
        {
            Text[] btnTextsOn = btnOn.GetComponentsInChildren<Text>();
            btnTextsOn[0].text = buttonOnText;
            Text[] btnTextsOff = btnOff.GetComponentsInChildren<Text>();
            btnTextsOff[0].text = buttonOnText;
        }

        public void setOffText(string buttonOffText)
        {
            Text[] btnTextsOn = btnOn.GetComponentsInChildren<Text>();
            btnTextsOn[1].text = buttonOffText;
            Text[] btnTextsOff = btnOff.GetComponentsInChildren<Text>();
            btnTextsOff[1].text = buttonOffText;
        }
    }

    internal class QMNestedButton
    {
        protected QMSingleButton mainButton;
        protected QMSingleButton backButton;
        protected string menuName;
        protected string btnQMLoc;
        protected string btnType;

        public QMNestedButton(QMNestedButton btnMenu, float btnXLocation, float btnYLocation, string btnText, string btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null, Color? backbtnBackgroundColor = null, Color? backbtnTextColor = null)
        {
            btnQMLoc = btnMenu.getMenuName();
            initButton(btnXLocation, btnYLocation, btnText, btnToolTip, btnBackgroundColor, btnTextColor, backbtnBackgroundColor, backbtnTextColor);
        }

        public QMNestedButton(string btnMenu, float btnXLocation, float btnYLocation, string btnText, string btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null, Color? backbtnBackgroundColor = null, Color? backbtnTextColor = null)
        {
            btnQMLoc = btnMenu;
            initButton(btnXLocation, btnYLocation, btnText, btnToolTip, btnBackgroundColor, btnTextColor, backbtnBackgroundColor, backbtnTextColor);
        }

        public void initButton(float btnXLocation, float btnYLocation, string btnText, string btnToolTip, Color? btnBackgroundColor = null, Color? btnTextColor = null, Color? backbtnBackgroundColor = null, Color? backbtnTextColor = null)
        {
            btnType = "NestedButton";

            Transform menu = UnityEngine.Object.Instantiate<Transform>(QMStuff.NestedMenuTemplate(), QMStuff.GetQuickMenuInstance().transform);
            menuName = QMButtonAPI.identifier + btnQMLoc + btnText;
            menu.name = menuName;

            mainButton = new QMSingleButton(btnQMLoc, btnXLocation, btnYLocation, btnText, () => { QMStuff.ShowQuickmenuPage(menuName); }, btnToolTip, btnBackgroundColor, btnTextColor);

            Il2CppSystem.Collections.IEnumerator enumerator = menu.transform.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Il2CppSystem.Object obj = enumerator.Current;
                Transform btnEnum = obj.Cast<Transform>();
                if (btnEnum != null)
                {
                    UnityEngine.Object.Destroy(btnEnum.gameObject);
                }
            }

            if (backbtnTextColor == null)
            {
                backbtnTextColor = Color.yellow;
            }
            QMButtonAPI.allNestedButtons.Add(this);
            backButton = new QMSingleButton(this, 5, 2.25f, "<color=#581cff><---</color>", () => { QMStuff.ShowQuickmenuPage(btnQMLoc); }, "Go Back", backbtnBackgroundColor, backbtnTextColor);
            backButton.getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(1, 2);
        }

        public string getMenuName()
        {
            return menuName;
        }

        public QMSingleButton getMainButton()
        {
            return mainButton;
        }

        public QMSingleButton getBackButton()
        {
            return backButton;
        }

        public void DestroyMe()
        {
            mainButton.DestroyMe();
            backButton.DestroyMe();
        }
    }

    internal class QMStuff
    {
        // Internal cache of the BoxCollider Background for the Quick Menu
        private static BoxCollider QuickMenuBackgroundReference;

        // Internal cache of the Single Button Template for the Quick Menu
        private static GameObject SingleButtonReference;

        // Internal cache of the Toggle Button Template for the Quick Menu
        private static GameObject ToggleButtonReference;

        // Internal cache of the Nested Menu Template for the Quick Menu
        private static Transform NestedButtonReference;

        // Internal cache of the QuickMenu
        private static QuickMenu quickmenuInstance;

        // Internal cache of the VRCUiManager
        private static VRCUiManager vrcuimInstance;



        // Fetch the background from the Quick Menu
        public static BoxCollider QuickMenuBackground()
        {
            if (QuickMenuBackgroundReference == null)
                QuickMenuBackgroundReference = GetQuickMenuInstance().GetComponent<BoxCollider>();
            return QuickMenuBackgroundReference;
        }

        // Fetch the Single Button Template from the Quick Menu
        public static GameObject SingleButtonTemplate()
        {
            if (SingleButtonReference == null)
                SingleButtonReference = GetQuickMenuInstance().transform.Find("ShortcutMenu/WorldsButton").gameObject;
            return SingleButtonReference;
        }

        // Fetch the Toggle Button Template from the Quick Menu
        public static GameObject ToggleButtonTemplate()
        {
            if (ToggleButtonReference == null)
            {
                ToggleButtonReference = GetQuickMenuInstance().transform.Find("UserInteractMenu/BlockButton").gameObject;
            }
            return ToggleButtonReference;
        }

        // Fetch the Nested Menu Template from the Quick Menu
        public static Transform NestedMenuTemplate()
        {
            if (NestedButtonReference == null)
            {
                NestedButtonReference = GetQuickMenuInstance().transform.Find("CameraMenu");
            }
            return NestedButtonReference;
        }

        // Fetch the Quick Menu instance
        public static QuickMenu GetQuickMenuInstance()
        {
            if (quickmenuInstance == null)
            {
                quickmenuInstance = QuickMenu.prop_QuickMenu_0;
            }
            return quickmenuInstance;
        }
        // Fetch the VRCUiManager instance
        public static VRCUiManager GetVRCUiMInstance()
        {
            if (vrcuimInstance == null)
            {
                vrcuimInstance = VRCUiManager.prop_VRCUiManager_0;
            }
            return vrcuimInstance;
        }


        private static FieldInfo currentPageGetter;
        private static GameObject shortcutMenu;
        private static GameObject userInteractMenu;

        public static void ShowQuickmenuPage(string pagename)
        {
            QuickMenu quickmenu = GetQuickMenuInstance();
            Transform pageTransform = quickmenu?.transform.Find(pagename);
            if (pageTransform == null)
            {
            }

            if (currentPageGetter == null)
            {
                GameObject shortcutMenu = quickmenu.transform.Find("ShortcutMenu").gameObject;
                if (!shortcutMenu.activeInHierarchy)
                    shortcutMenu = quickmenu.transform.Find("UserInteractMenu").gameObject;


                FieldInfo[] fis = Il2CppType.Of<QuickMenu>().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where((fi) => fi.FieldType == Il2CppType.Of<GameObject>()).ToArray();
                //MelonLoader.MelonModLogger.Log("[QMStuff] GameObject Fields in QuickMenu:");
                int count = 0;
                foreach (FieldInfo fi in fis)
                {
                    GameObject value = fi.GetValue(quickmenu)?.TryCast<GameObject>();
                    if (value == shortcutMenu && ++count == 3)
                    {
                        //MelonLoader.MelonModLogger.Log("[QMStuff] currentPage field: " + fi.Name);
                        currentPageGetter = fi;
                        break;
                    }
                }
                if (currentPageGetter == null)
                {
                    return;
                }
            }

            currentPageGetter.GetValue(quickmenu)?.Cast<GameObject>().SetActive(false);

            GameObject infoBar = GetQuickMenuInstance().transform.Find("QuickMenu_NewElements/_InfoBar").gameObject;
            infoBar.SetActive(pagename == "ShortcutMenu");

            QuickMenuContextualDisplay quickmenuContextualDisplay = GetQuickMenuInstance().field_Private_QuickMenuContextualDisplay_0;
            quickmenuContextualDisplay.Method_Public_Void_EnumNPublicSealedvaUnNoToUs7vUsNoUnique_0(QuickMenuContextualDisplay.EnumNPublicSealedvaUnNoToUs7vUsNoUnique.NoSelection);
            //quickmenuContextualDisplay.Method_Public_Nested0_0(QuickMenuContextualDisplay.Nested0.NoSelection);

            pageTransform.gameObject.SetActive(true);

            currentPageGetter.SetValue(quickmenu, pageTransform.gameObject);

            if (shortcutMenu == null)
                shortcutMenu = QuickMenu.prop_QuickMenu_0.transform.Find("ShortcutMenu")?.gameObject;

            if (userInteractMenu == null)
                userInteractMenu = QuickMenu.prop_QuickMenu_0.transform.Find("UserInteractMenu")?.gameObject;

            if (pagename == "ShortcutMenu")
            {
                SetIndex(0);
            }
            else if (pagename == "UserInteractMenu")
            {
                SetIndex(3);
            }
            else
            {
                SetIndex(-1);
                shortcutMenu?.SetActive(false);
                userInteractMenu?.SetActive(false);
            }
        }

        public static void SetIndex(int index)
        {
            GetQuickMenuInstance().field_Private_EnumNPublicSealedvaUnShEmUsEmNoCaMo_nUnique_0 = (QuickMenu.EnumNPublicSealedvaUnShEmUsEmNoCaMo_nUnique) index;
        }
    }

    internal enum ButtonType
    {
        Default,
        Single,
        Nested,
        Toggle
    }

    internal enum MenuType
    {
        ShortCut,
        Music,
        UserInteract,
        Nested,
        UserInfo,
        Evolve,
    }

    internal static class ReworkedButtonAPI
    {
        public static List<Button> SpriteButton = new List<Button>();
        public static List<Image> SpritebtnImage = new List<Image>();
        public static List<object> ButtonOutput = new List<object>();
#pragma warning disable CS0649 // Le champ 'ReworkedButtonAPI.Img' n'est jamais assigné et aura toujours sa valeur par défaut null
        public static Image Img;
#pragma warning restore CS0649 // Le champ 'ReworkedButtonAPI.Img' n'est jamais assigné et aura toujours sa valeur par défaut null

        public static IEnumerator CreateButton(MenuType Menu, ButtonType type, string ButtonText, float X, float Y, System.Action action, string buttonTooltip, Color color, Color BtnText, QMNestedButton MainButton = null, string SpriteImage = null, string OnText = null, string OffText = null, UnityAction OffAction = null, Color? BackButtonColor = null, Color? BackButtonTextColor = null)
        {
            string MenuString = "ShortcutMenu";
            switch (Menu)
            {
                default: MenuString = "ShortcutMenu"; break;
                case MenuType.Music: MenuString = "EvolveShortcutMenuMusic"; break;
                case MenuType.Evolve: MenuString = "EvolveShortcutMenuEvolve"; break;
                case MenuType.ShortCut: MenuString = "ShortcutMenu"; break;
                case MenuType.UserInteract: MenuString = "UserInteractMenu"; break;
                case MenuType.UserInfo: MenuString = "UserInfo"; break;
            }

            switch (type)
            {
                default: break;
                case ButtonType.Single:
                    QMSingleButton x = null;
                    if (MainButton != null)
                    {
                        x = new QMSingleButton(MainButton, X, Y, ButtonText, action, buttonTooltip, color, BtnText);
                        x.getGameObject().name = "EvolveImageButton";
                    }
                    else
                    {
                        x = new QMSingleButton(MenuString, X, Y, ButtonText, action, buttonTooltip, color, BtnText);
                        x.getGameObject().name = "EvolveImageButton";
                    }

                    if (SpriteImage != null)
                    {
                        var image = new Sprite();
                        WWW www = new WWW(SpriteImage, null, new Il2CppSystem.Collections.Generic.Dictionary<string, string>()) ;
                        yield return www;
                        {
                            image = Sprite.CreateSprite(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0), 100 * 1000, 1000, SpriteMeshType.FullRect, Vector4.zero, false);
                            x.getGameObject().GetComponent<Image>().sprite = image;
                            SpriteButton.Add(x.getGameObject().GetComponent<Button>());
                            SpritebtnImage.Add(x.getGameObject().GetComponent<Image>());
                            foreach (Image img in SpritebtnImage)
                            {
                                try
                                {
                                    img.color = Color.white;
                                }
                                catch { }
                            }

                            foreach (Button btn in SpriteButton)
                            {
                                btn.colors = new ColorBlock()
                                {
                                    colorMultiplier = 1,
                                    disabledColor = Color.white / 2,
                                    highlightedColor = Color.white * 2,
                                    normalColor = Color.white,
                                    pressedColor = Color.white / 2
                                };
                            }
                        }
                    }
                    ButtonOutput.Add(x);
                    break;
            }
        }
    }
}