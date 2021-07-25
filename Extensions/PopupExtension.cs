using System;
using Il2CppSystem.Diagnostics;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;

namespace Evolve.Wrappers
{
    internal static class PopupManager
    {
        public static void AlertV2(this VRCUiPopupManager instance, string title, string Content, string buttonname, Action onSucces)
        {
            instance.Method_Public_Void_String_String_String_Action_Action_1_VRCUiPopup_1(title, Content, buttonname, onSucces, null);
        }
        public static void AlertV2(this VRCUiPopupManager instance, string title, string Content, string buttonname, Action action, string button2, Action action2)
        {
            instance.Method_Public_Void_String_String_String_Action_String_Action_Action_1_VRCUiPopup_1(title, Content, buttonname, action, button2, action2, null);
        }

        public static void Alert(this VRCUiPopupManager instance, string title, string Content, string buttonname, Action onSucces)
        {
            instance.Method_Public_Void_String_String_String_Action_Action_1_VRCUiPopup_0(title, Content, buttonname, onSucces, null);
        }

        public static void Alert(this VRCUiPopupManager instance, string title, string Content, string buttonname, Action action, string button2, Action action2)
        {
            instance.Method_Public_Void_String_String_String_Action_String_Action_Action_1_VRCUiPopup_0(title, Content, buttonname, action, button2, action2, null);
        }

        public static void ShowAlert(this VRCUiPopupManager instance, string title, string Content)
        {
            instance.Method_Public_Void_String_String_Single_1(title, Content);
        }

        public static void HideCurrentPopUp(this VRCUiPopupManager instance)
        {
            Utils.VRCUiManager.HideScreen("POPUP");
        }

        public static void HideCurrentScreen(this VRCUiPopupManager instance)
        {
            Utils.VRCUiManager.HideScreen("SCREEN");
        }

        public static void QueHudMessage(this VRCUiManager instance, string Message)
        {
            instance.field_Private_List_1_String_0.Add(Message);
        }

        public static void ClearHudMessages(this VRCUiManager instance)
        {
            instance.field_Private_List_1_String_0.Clear();
            instance.field_Public_Text_0.text = "";
        }

        /*public static void InputPopUp(this VRCUiPopupManager instance, string title, string okButtonName, Action<string> onSuccess, Action Button2, string def = null)
        {
            instance.Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_0(title, "", InputField.InputType.Standard, false, okButtonName,(string g, List<KeyCode> l, Text t) =>
            {
                if (string.Empty == g)
                {
                    g = def;
                }
                onSuccess(g);
            }, Button2, def ?? "", true, null);
        }*/

        public static void InputeText(string title, string text, System.Action<string> okaction)
        {
            VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_Int32_0(title, "", InputField.InputType.Standard, false, text,
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>>
                (new Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>
                (delegate (string s, Il2CppSystem.Collections.Generic.List<KeyCode> k, Text t)
                {
                    okaction(s);
                })), null, "...", true, null);
        }

        /*internal static void AskInGameInput(this VRCUiPopupManager instance, string title, string okButtonName, Action<string> onSuccess, string def = null)
        {
            PopupManager.IsTyping = true;
            instance.InputPopUp(title, okButtonName, delegate (string g)
            {
                onSuccess(g);
                instance.HideCurrentPopUp();
                PopupManager.IsTyping = false;
            }, delegate
            {
                instance.HideCurrentPopUp();
                PopupManager.IsTyping = false;
            }, def);
        }*/

#pragma warning disable CS0649 // Le champ 'PopupManager.IsTyping' n'est jamais assigné et aura toujours sa valeur par défaut false
        public static bool IsTyping;
#pragma warning restore CS0649 // Le champ 'PopupManager.IsTyping' n'est jamais assigné et aura toujours sa valeur par défaut false
    }
}