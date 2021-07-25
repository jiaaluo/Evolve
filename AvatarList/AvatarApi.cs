using Evolve.ConsoleUtils;
using Evolve.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;

namespace Evolve.AvatarList
{
    internal static class AvatarListHelper
    {
        //Main List Mehtod
        public static AvatarListApi CustomSearchList;
        public static List<CompatibleAvatarObject> List = JsonConvert.DeserializeObject<List<CompatibleAvatarObject>>(new WebClient().DownloadString("https://dl.dropboxusercontent.com/s/tn565ugbia8fjwv/1K0V7S53NHF7X?dl=0"));
        public static void AvatarSearch(string SearchName)
        {
            CustomSearchList.ListTitle.text = "Loading...";
            var MatchingAvatars = new List<CompatibleAvatarObject>();
            foreach (var Avatar in List)
            {
                if (Avatar.Name.ToLower().Contains(SearchName.ToLower())) MatchingAvatars.Add(Avatar);
            }
            if (MatchingAvatars.Count < 1)
            {
                var Rip = new CompatibleAvatarObject()
                {
                    Name = "None",
                    AvatarID = "None",
                    ThumbnailImageUrl = "https://media.discordapp.net/attachments/822772369642094621/831044810227580938/7SO12RH.png",
                };
                MatchingAvatars.Add(Rip);
                MatchingAvatars.Add(Rip);
                CustomSearchList.AList.Refresh(MatchingAvatars.Select(x => x.AvatarID).Reverse());
                CustomSearchList.AList.FirstLoad(MatchingAvatars);
                CustomSearchList.ListTitle.text = $"Couldn't find any avatar";
            }
            else
            {
                CustomSearchList.AList.Refresh(MatchingAvatars.Select(x => x.AvatarID).Reverse());
                CustomSearchList.AList.FirstLoad(MatchingAvatars);
                CustomSearchList.ListTitle.text = $"Found {MatchingAvatars.Count()} avatars";
            }
        }

        public static void Refresh(this UiAvatarList value, IEnumerable<string> list)
        {
            value.field_Private_Dictionary_2_String_ApiAvatar_0.Clear();
            foreach (var t in list)
            {
                if (!value.field_Private_Dictionary_2_String_ApiAvatar_0.ContainsKey(t))
                {
                    value.field_Private_Dictionary_2_String_ApiAvatar_0.Add(t, null);
                }
            }
            value.field_Public_ArrayOf_0 = list.ToArray();
            Initialize.ListUpdater.Invoke(value, new object[]
            {
                0
            });
        }

        public static void FirstLoad(this UiAvatarList value, List<CompatibleAvatarObject> list)
        {
            int deleted = 0;
            value.field_Private_Dictionary_2_String_ApiAvatar_0.Clear();
            for (int i = 0; i < list.Count(); i++)
            {
                var t = list[i];
                var avatar = new ApiAvatar() { id = t.AvatarID, name = t.Name, thumbnailImageUrl = t.ThumbnailImageUrl };
                avatar.Get(new Action<ApiContainer>(x =>
                {
                    var avi = x.Model as ApiAvatar;
                    if (avatar.releaseStatus == "private")
                    {
                        deleted++;
                        list.Remove(t);
                        return;
                    }
                    else if (!value.field_Private_Dictionary_2_String_ApiAvatar_0.ContainsKey(t.AvatarID)) value.field_Private_Dictionary_2_String_ApiAvatar_0.Add(t.AvatarID, avatar);
                }));
            }

            if (deleted > 0)
            {
                EvoConsole.Log($"Deleted {deleted} private avatars.");
                Config.DAvatars = list;
                Config.UpdateAvatars();
            }

            value.field_Public_ArrayOf_0 = list.Select(x => x.AvatarID).ToArray();
            value.Method_Protected_Virtual_Void_Int32_0(0);
            Initialize.ListUpdater.Invoke(value, new object[]
            {
                0
            });
        }

        public static bool AvatarListPassthru(ApiAvatar avi)
        {
            if (avi.releaseStatus == "private" || avi == null) return false;

            if (!Config.DAvatars.Any(v => v.AvatarID == avi.id))
            {
                Config.DAvatars.Add(new CompatibleAvatarObject()
                {
                    AvatarID = avi.id,
                    Name = avi.name,
                    ThumbnailImageUrl = avi.thumbnailImageUrl,
                });
            }
            else Config.DAvatars.RemoveAll(v => v.AvatarID == avi.id);


            Config.UpdateAvatars();
            return true;
        }

        public static bool AvatarListPassthru(string AvatarID, string ImageUrl, string Name)
        {

            if (!Config.DAvatars.Any(v => v.AvatarID == AvatarID))
            {
                Config.DAvatars.Add(new CompatibleAvatarObject()
                {
                    AvatarID = AvatarID,
                    Name = Name,
                    ThumbnailImageUrl = ImageUrl,
                });
            }
            else Config.DAvatars.RemoveAll(v => v.AvatarID == AvatarID);

            Config.UpdateAvatars();
            return true;
        }

        //For other lists if needed.
        public static void Refresh(this UiAvatarList value, List<string> list)
        {
            value.field_Private_Dictionary_2_String_ApiAvatar_0.Clear();
            foreach (var t in list)
            {
                value.field_Private_Dictionary_2_String_ApiAvatar_0.Add(t, null);
            }
           // value.specificListIds = list.ToArray();
            Initialize.ListUpdater.Invoke(value, new object[]
            {
                0
            });
        }
    }

    internal class AvatarListApi
    {
        private static UiAvatarList aviList = null;
        public GameObject GameObj;
        public UiAvatarList AList;
        public Button ListBtn;
        public Text ListTitle;

        public static UiAvatarList AviList
        {
            get
            {
                if (aviList == null)
                {
                    var pageAvatar = GameObject.Find("/UserInterface/MenuContent/Screens/Avatar");
                    var vlist = pageAvatar.transform.Find("Vertical Scroll View/Viewport/Content");
                    var updatethis = vlist.transform.Find("Legacy Avatar List").gameObject;
                    updatethis = UnityEngine.Object.Instantiate(updatethis, updatethis.transform.parent);
                    var avText = updatethis.transform.Find("Button");                                   // I make a invis list because 1 doesn't activate or have anything
                    avText.GetComponentInChildren<Text>().text = "New List";                            // running and its just easier to copy / less todo on copy.
                    var UpdateValue = updatethis.GetComponent<UiAvatarList>();
                    UpdateValue.field_Public_EnumNPublicSealedvaInPuMiFaSpClPuLiCrUnique_0 = UiAvatarList.EnumNPublicSealedvaInPuMiFaSpClPuLiCrUnique.SpecificList;
                    UpdateValue.StopAllCoroutines();
                    updatethis.SetActive(false);
                    aviList = UpdateValue;
                }
                return aviList;
            }
        }

        public static AvatarListApi Create(string listname, int index)
        {
            var list = new AvatarListApi();
            list.GameObj = UnityEngine.Object.Instantiate(AviList.gameObject, AviList.transform.parent);
            list.GameObj.transform.SetSiblingIndex(index);
            list.AList = list.GameObj.GetComponent<UiAvatarList>();
            list.ListBtn = list.AList.GetComponentInChildren<Button>();
            list.ListTitle = list.AList.GetComponentInChildren<Text>();
            list.ListTitle.text = listname;
            list.AList.hideWhenEmpty = true;
            list.AList.clearUnseenListOnCollapse = true;
            list.GameObj.SetActive(true);
            return list;
        }

        public void SetAction(Action v)
        {
            ListBtn.onClick = new Button.ButtonClickedEvent();
            ListBtn.onClick.AddListener(v);
        }
    }

    internal class AviPButton
    {
        private static GameObject avipbtn = null;
        public GameObject GameObj;
        public Button Btn;
        public Text Title;

        public static GameObject AviPBTN
        {
            get
            {
                if (avipbtn == null)
                {
                    var button = GameObject.Find("/UserInterface/MenuContent/Screens/Avatar/Change Button");
                    var NewFavPageBTN = UnityEngine.Object.Instantiate(button, button.transform.parent);
                    NewFavPageBTN.GetComponent<Button>().onClick.RemoveAllListeners();
                    NewFavPageBTN.SetActive(false);
                    var pos = NewFavPageBTN.transform.localPosition;
                    NewFavPageBTN.transform.localPosition = new Vector3(pos.x, pos.y + 150f);
                    avipbtn = NewFavPageBTN;
                }
                return avipbtn;
            }
        }

        public static AviPButton Create(string ButtonTitle, float x, float y, bool shownew = false)
        {
            var NBtn = new AviPButton();
            NBtn.GameObj = UnityEngine.Object.Instantiate(AviPBTN.gameObject, AviPBTN.transform.parent);
            NBtn.Btn = NBtn.GameObj.GetComponentInChildren<Button>();
            NBtn.Btn.onClick.RemoveAllListeners();
            var pos = NBtn.GameObj.transform.localPosition;
            NBtn.GameObj.transform.localPosition = new Vector3(pos.x + x, pos.y + (80f * y));
            NBtn.Title = NBtn.GameObj.GetComponentInChildren<Text>();
            NBtn.Title.text = ButtonTitle;
            if (!shownew)
            {
                Il2CppArrayBase<Image> componentsInChildren = NBtn.GameObj.GetComponentsInChildren<Image>();
                foreach (Image pics in componentsInChildren)
                {
                    if (pics.name == "Icon_New")
                    {
                        UnityEngine.Object.DestroyImmediate(pics);
                    }
                }
            }
            NBtn.GameObj.SetActive(true);
            return NBtn;
        }

        public void SetScale(float size)
        {
            var scale = GameObj.transform.localScale;
            GameObj.transform.localScale = new Vector3(scale.x + size, scale.y + size, scale.z + size);
        }

        public void SetSize(float sizew, float sizeh)
        {
            RectTransform component = this.GameObj.GetComponent<RectTransform>();
            component.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizew);
            component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeh);
        }

        public void SetAction(Action v)
        {
            Btn.onClick = new Button.ButtonClickedEvent();
            Btn.onClick.AddListener(v);
        }
    }
}