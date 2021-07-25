using System;
using System.Linq;
using System.Reflection;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using VRC.Core;

namespace Evolve.AvatarList
{
    internal static class Initialize
    {
        public static AvatarListApi CustomList;
        public static AviPButton FavoriteButton;
#pragma warning disable CS0649 // Le champ 'Initialize.QmMenuSwitchM' n'est jamais assigné et aura toujours sa valeur par défaut null
        public static MethodInfo QmMenuSwitchM;
#pragma warning restore CS0649 // Le champ 'Initialize.QmMenuSwitchM' n'est jamais assigné et aura toujours sa valeur par défaut null
        public static MethodInfo PopUM;
        public static MethodInfo KeyBoardM;
        public static MethodInfo ListUpdater;

        internal static void RenderElement(this UiVRCList uivrclist, Il2CppSystem.Collections.Generic.List<ApiAvatar> AvatarList)
        {
            uivrclist.Method_Protected_IEnumerator_List_1_T_Int32_Boolean_VRCUiContentButton_0<ApiAvatar>(AvatarList, 0, true);
        }

        public static void OnStart()
        {
            Config.LoadConfig();
           
            KeyBoardM = typeof(VRCUiPopupManager).GetMethods().First(delegate (MethodInfo x)
            {
                bool flag = !x.Name.Contains("Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_");
                bool result;
                if (flag) result = false;
                else
                {
                    bool flag2 = XrefScanner.XrefScan(x).Any((XrefInstance z) => z.Type == XrefType.Global && z.ReadAsObject() != null && z.ReadAsObject().ToString() == "UserInterface/MenuContent/Popups/InputKeypadPopup");
                    result = flag2;
                }
                return result;
            });
            PopUM = typeof(VRCUiPopupManager).GetMethods().First(delegate (MethodInfo x)
            {
                bool flag;
                if (x.Name.Contains("Method_Public_Void_String_String_Single_")) flag = x.Name.Replace("Method_Public_Void_String_String_Single_", "").All((char v) => char.IsDigit(v));
                else flag = false;
                bool flag2 = flag;
                if (flag2)
                {
                    bool flag3 = XrefScanner.XrefScan(x).Any((XrefInstance z) => z.Type == XrefType.Global && z.ReadAsObject() != null && z.ReadAsObject().ToString() == "UserInterface/MenuContent/Popups/AlertPopup");
                    if (flag3) return true;
                }
                return false;
            });
            ListUpdater = typeof(UiAvatarList).GetMethods().First(delegate (MethodInfo x)
            {
                bool flag;
                if (!x.Name.Contains("Method_Protected_Virtual_Void_Int32_")) flag = !x.Name.Replace("Method_Protected_Virtual_Void_Int32_", "").All((char z) => char.IsDigit(z));
                else flag = false;

                bool result;
                if (flag) result = false;
                else result = XrefScanner.XrefScan(x).Any((XrefInstance z) => z.Type == XrefType.Global && z.ReadAsObject() != null && z.ReadAsObject().ToString() == "standalonewindows");

                return result;
            });

            if (AvatarListHelper.CustomSearchList == null) AvatarListHelper.CustomSearchList = AvatarListApi.Create("Search", 1);
        }

        public static void Module()
        {
            CustomList = AvatarListApi.Create($"Evolve Engine [{Config.DAvatars.Count}]", 1);
            CustomList.AList.FirstLoad(Config.DAvatars);

            CustomList.AList.field_Public_SimpleAvatarPedestal_0.field_Internal_Action_3_String_GameObject_AvatarPerformanceStats_0 = new Action<string, GameObject, VRC.SDKBase.Validation.Performance.Stats.AvatarPerformanceStats>((x, y, z) =>
            {
                if (Config.DAvatars.Any(v => v.AvatarID == CustomList.AList.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0.id))
                {
                    FavoriteButton.Title.text = "Remove";
                    CustomList.ListTitle.text = $"Evolve Engine [{Config.DAvatars.Count}]";
                }
                else
                {
                    FavoriteButton.Title.text = "Save";
                    CustomList.ListTitle.text = $"Evolve Engine [{Config.DAvatars.Count}]";
                }
            });

            //Add-Remove Favorite Button
            FavoriteButton = AviPButton.Create("Save", -350f, 7.46f);
            FavoriteButton.SetSize(200f, 80f);
            FavoriteButton.SetAction(() =>
            {
                var avatar = CustomList.AList.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0;
                if (avatar.releaseStatus != "private")
                {
                    if (!Config.DAvatars.Any(v => v.AvatarID == avatar.id))
                    {
                        AvatarListHelper.AvatarListPassthru(avatar);
                        CustomList.AList.Refresh(Config.DAvatars.Select(x => x.AvatarID).Reverse());
                        FavoriteButton.Title.text = "Remove";
                        CustomList.ListTitle.text = $"Evolve Engine [{Config.DAvatars.Count}]";
                    }
                    else
                    {
                        AvatarListHelper.AvatarListPassthru(avatar);
                        CustomList.AList.Refresh(Config.DAvatars.Select(x => x.AvatarID).Reverse());
                        FavoriteButton.Title.text = "Save";
                        CustomList.ListTitle.text = $"Evolve Engine [{Config.DAvatars.Count}]";
                    }
                }
            });

            //Search
            AviPButton SearchButton = AviPButton.Create("Search", -350f, 4.80f);
            SearchButton.SetSize(200f, 80f);
            SearchButton.SetAction(() =>
            {
                Wrappers.PopupManager.InputeText("Evolve Engine", "Search", new Action<string>((a) =>
                {
                    AvatarListHelper.AvatarSearch(a);
                }));
            });

            //Add by ID

           
        }
    }
}