using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.XR;
using VRC;
using VRC.UserCamera;

namespace Evolve.Wrappers
{
    internal static class Utils
    {
        public enum NHDDDDJNDMB
        {
            Undefined,
            Loading,
            Error,
            Blocked,
            Safety,
            Substitute,
            Performance,
            Custom
        }

        public static VRCUiPopupManager VRCUiPopupManager
        {
            get
            {
                return VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0;
            }
        }

        public static VRCUiManager VRCUiManager
        {
            get
            {
                return VRCUiManager.prop_VRCUiManager_0;
            }
        }

        /* public static ModerationManager ModerationManager
         {
             get
             {
                 return ModerationManager.field_Private_Static_ModerationManager_0;
             }
         }*/

        public static NotificationManager NotificationManager
        {
            get
            {
                return NotificationManager.field_Private_Static_NotificationManager_0;
            }
        }

        public static VRCWebSocketsManager VRCWebSocketsManager
        {
            get
            {
                return VRCWebSocketsManager.field_Private_Static_VRCWebSocketsManager_0;
            }
        }

        public static NetworkManager NetworkManager
        {
            get
            {
                return NetworkManager.field_Internal_Static_NetworkManager_0;
            }
        }

        public static PlayerManager PlayerManager
        {
            get
            {
                return PlayerManager.field_Private_Static_PlayerManager_0;
            }
        }

        public static VRCPlayer LocalPlayer
        {
            get
            {
                return VRCPlayer.field_Internal_Static_VRCPlayer_0;
            }
            set
            {
                Utils.LocalPlayer = Utils.LocalPlayer;
            }
        }

        public static QuickMenu QuickMenu
        {
            get
            {
                return QuickMenu.prop_QuickMenu_0;
            }
        }

        public static UserInteractMenu UserInteractMenu
        {
            get
            {
                return UnityEngine.Resources.FindObjectsOfTypeAll<UserInteractMenu>()[0];
            }
        }

        public static QuickMenuContextualDisplay QuickMenuContextualDisplay
        {
            get
            {
                return Utils.QuickMenu.field_Private_QuickMenuContextualDisplay_0;
            }
        }

        public static Camera Camera
        {
            get
            {
                return VRCVrCamera.field_Private_Static_VRCVrCamera_0.field_Public_Camera_0;
            }
        }

        public static VRCVrCamera VRCVrCamera
        {
            get
            {
                return VRCVrCamera.field_Private_Static_VRCVrCamera_0;
            }
        }

        public static UserCameraController UserCameraController
        {
            get
            {
                return UserCameraController.field_Internal_Static_UserCameraController_0;
            }
        }

        public static VRCTrackingManager VRCTrackingManager
        {
            get
            {
                return VRCTrackingManager.field_Private_Static_VRCTrackingManager_0;
            }
        }

        public static Vector3 GetWorldCameraPosition()
        {
            VRCVrCamera vrcvrCamera = Utils.VRCVrCamera;
            Il2CppSystem.Type il2CppType = vrcvrCamera.GetIl2CppType();
            bool flag = il2CppType == Il2CppType.Of<VRCVrCameraSteam>();
            if (flag)
            {
                VRCVrCameraSteam vrcvrCameraSteam = vrcvrCamera.Cast<VRCVrCameraSteam>();
                Transform field_Private_Transform_ = vrcvrCameraSteam.field_Private_Transform_0;
                Transform field_Private_Transform_2 = vrcvrCameraSteam.field_Private_Transform_1;
                bool flag2 = field_Private_Transform_.name == "Camera (eye)";
                if (flag2)
                {
                    return field_Private_Transform_.position;
                }
                bool flag3 = field_Private_Transform_2.name == "Camera (eye)";
                if (flag3)
                {
                    return field_Private_Transform_2.position;
                }
            }
            else
            {
                bool flag4 = il2CppType == Il2CppType.Of<VRCVrCameraUnity>();
                if (flag4)
                {
                    VRCVrCameraUnity vrcvrCameraUnity = vrcvrCamera.Cast<VRCVrCameraUnity>();
                    return vrcvrCameraUnity.field_Public_Camera_0.transform.position;
                }
                bool flag5 = il2CppType == Il2CppType.Of<VRCVrCameraWave>();
                if (flag5)
                {
                    VRCVrCameraWave vrcvrCameraWave = vrcvrCamera.Cast<VRCVrCameraWave>();
                    return vrcvrCameraWave.transform.position;
                }
            }
            return vrcvrCamera.transform.parent.TransformPoint(Utils.GetLocalCameraPosition());
        }

        public static GameObject GetPlayerCamera()
        {
            return GameObject.Find("Camera (eye)");
        }

        public static Vector3 GetLocalCameraPosition()
        {
            VRCVrCamera vrcvrCamera = Utils.VRCVrCamera;
            Il2CppSystem.Type il2CppType = vrcvrCamera.GetIl2CppType();
            bool flag = il2CppType == Il2CppType.Of<VRCVrCamera>();
            Vector3 result;
            if (flag)
            {
                result = vrcvrCamera.transform.localPosition;
            }
            else
            {
                bool flag2 = il2CppType == Il2CppType.Of<VRCVrCameraSteam>();
                if (flag2)
                {
                    VRCVrCameraSteam vrcvrCameraSteam = vrcvrCamera.Cast<VRCVrCameraSteam>();
                    Transform field_Private_Transform_ = vrcvrCameraSteam.field_Private_Transform_0;
                    Transform field_Private_Transform_2 = vrcvrCameraSteam.field_Private_Transform_1;
                    bool flag3 = field_Private_Transform_.name == "Camera (eye)";
                    if (flag3)
                    {
                        result = vrcvrCamera.transform.parent.InverseTransformPoint(field_Private_Transform_.position);
                    }
                    else
                    {
                        bool flag4 = field_Private_Transform_2.name == "Camera (eye)";
                        if (flag4)
                        {
                            result = vrcvrCamera.transform.parent.InverseTransformPoint(field_Private_Transform_2.position);
                        }
                        else
                        {
                            result = Vector3.zero;
                        }
                    }
                }
                else
                {
                    bool flag5 = il2CppType == Il2CppType.Of<VRCVrCameraUnity>();
                    if (flag5)
                    {
                        bool isInVR = Utils.LocalPlayer.GetIsInVR();
                        if (isInVR)
                        {
                            result = vrcvrCamera.transform.localPosition + UnityEngine.XR.InputTracking.GetLocalPosition(XRNode.CenterEye);
                        }
                        else
                        {
                            VRCVrCameraUnity vrcvrCameraUnity = vrcvrCamera.Cast<VRCVrCameraUnity>();
                            result = vrcvrCamera.transform.parent.InverseTransformPoint(vrcvrCameraUnity.field_Public_Camera_0.transform.position);
                        }
                    }
                    else
                    {
                        bool flag6 = il2CppType == Il2CppType.Of<VRCVrCameraWave>();
                        if (flag6)
                        {
                            VRCVrCameraWave vrcvrCameraWave = vrcvrCamera.Cast<VRCVrCameraWave>();
                            result = vrcvrCameraWave.field_Public_Transform_0.InverseTransformPoint(vrcvrCamera.transform.position);
                        }
                        else
                        {
                            result = Vector3.zero;
                        }
                    }
                }
            }
            return result;
        }
    }
}