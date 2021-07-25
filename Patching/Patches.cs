using Evolve.Buttons;
using Evolve.ConsoleUtils;
using Evolve.Modules;
using Evolve.Utils;
using Evolve.Wrappers;
using ExitGames.Client.Photon;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Il2CppSystem.Xml;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.Networking;
using VRC.SDKBase;
using Evolve.FoldersManager;
using Evolve.Module;
using Harmony;
using Newtonsoft.Json;
using Photon.Realtime;
using RootMotion.FinalIK;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using static Evolve.ConsoleUtils.EvoVrConsole;
using Player = VRC.Player;
using UnityEngine.UI;
using VRCSDK2.Validation.Performance;
using Newtonsoft.Json.Linq;
using BestHTTP;
using Evolve.Loaders;
using Photon.Pun;
using System.Web.Mvc;
using static Evolve.Buttons.RPCMenu;
using UnhollowerBaseLib.Attributes;
using Steamworks;
using static Evolve.Buttons.UdonRPCMenu;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Security.Principal;
using VRC.Management;
using Evolve.Buttons.Worlds;
using static VRC.Core.API;
using BestHTTP.Forms;
using Transmtn.DTO.Notifications;
using Evolve.AvatarList;
using Config = Evolve.FoldersManager.Config;
using System.Net;
using Evolve.Api;
using System.Net.Http;
using System.Security.Policy;
using VRC.SDKBase.Validation.Performance;
using System.Text;
using UnhollowerRuntimeLib.XrefScans;
using Evolve.Protections;

namespace Evolve.Patch
{
    public class Patches
    {
        public static bool PatchSucess = false;
        public static int MainPatchesCount;

        private static void ADlg(VRCEventDelegate<Player> field, Action<Player> eventHandlerA)
        {
            field.field_Private_HashSet_1_UnityAction_1_T_0.Add(eventHandlerA);
        }

        private static bool SF;
        private static bool AFF;
        public static event Action<Player> Join;
        public static event Action<Player> Leave;

        [Obsolete]
        public static HarmonyInstance Instance = HarmonyInstance.Create("[Evolve Patches]");
        public static HarmonyMethod GetPatch(string name)
        {
            return new HarmonyMethod(typeof(Patches).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
        }

        private static Il2CppSystem.Object SpoofedHWID;
        private static Il2CppSystem.Object SpoofedDeviceName;
        private static Il2CppSystem.Object SpoofedGraphicDeviceName;
        private static Il2CppSystem.Object SpoofedGraphicDeviceId;
        private static Il2CppSystem.Object SpoofedVendorDeviceId;
        public static void Initialize()
        {
            OnPlayerJoinOrLeft();
            Join += JoinLeft.OnPlayerJoin;
            Leave += JoinLeft.OnPlayerLeft;

            Patch();
        }

        [Obsolete]
        public static unsafe void Patch()
        {
            var No = long.Parse("2173162573851") ^ -1822076715 ^ 0xCCA000000000000L ^ 0x22A7DA944CL ^ -7968319096167071744L ^ -3816365552469803008L ^ 0x48E877F1F3000000L ^ -3805541685128069120L ^ -6026324156323004416L ^ 0x1C2942AACD390000L ^ -2340264320232849408L ^ 0x4230000000000000L ^ 0x3D9CF2DDDA149800L ^ 0x316991CC0L ^ 0 ^ -2145479602233933824L ^ 0x6BBF276000000000L ^ 0 ^ 0x2B98CF5627BC0000L ^ 0x1D2E9628A000000L ^ 0x2FEED00000000000L ^ 0x537F000000000000L ^ -6972949015494792192L ^ 0x6A00000000000000L ^ -279495115381760L ^ 0 ^ 0 ^ 0x269D9 ^ 0 ^ 0 ^ 0x29C1BC8F49000000L ^ 0x26420C4E67000000L ^ 0 ^ 0xBF9CC200000000L ^ 0x50575A0000000000L ^ 0x5EF87EAFC7079000L ^ 0x67909B0000000000L ^ -2319916758049226752L ^ 0x490685E592FC6400L ^ 0xFDBF8 ^ 0 ^ 0x2000000000000000L ^ 0xC0000000u ^ 0x820000 ^ 0 ^ -115975173177344L ^ 0x4922390000000000L ^ 0x578E7ABDF9000000L ^ 0x259AD71700000000L ^ 0x63BB4A0000000L ^ 0x6100000000000000L ^ -5188146770730811392L ^ 0x57FD3E0000000000L ^ -4276730796141707264L ^ -6786001544839371008L ^ 0 ^ 0x5277C5E0D600000L ^ 0 ^ -5941371953334321152L ^ -7540573888590118912L ^ 0x116302D900000000L ^ 0 ^ 0x5400000000000000L ^ -3575858104132173824L ^ 0x436191A723B70000L ^ 0x6891E861A200000L ^ -4611686018427387904L ^ 0x1F2B028600000000L ^ -8745643619089645568L ^ 0 ^ 0x7E0F430000000000L ^ 0x4924000000000L ^ 0x46B7F4BF28D26D00L ^ 0 ^ 0 ^ 0 ^ 0xD894000000L ^ 0x4A4A44376C2A0000L ^ 0 ^ 0x2A52138F90000000L ^ 0 ^ 0x52BC0 ^ 0x39682E22000000L ^ 0xAF7DC00000000L ^ 0x120BCA6F0000L ^ 0x4F034386D1BC0000L ^ 0x160840000000000L ^ 0x789278100D000000L ^ 0x5C054D0E930L ^ -17592186044416L ^ -2830512365802356736L ^ 0x26657C8418F6EFL ^ 0 ^ 0x4C00000000000000L ^ 0x46B0D08388000000L ^ -1209379031510679552L ^ 0 ^ 0x53421C1372000000L ^ 0x355E000000000000L ^ 0 ^ 0x37D128F300000000L ^ 0x30000 ^ 0x14E20C1D62C42800L ^ 0 ^ -8515242394525958144L ^ 0x63286B0000000000L ^ 0x45F9710000000000L ^ 0x40B5B5E9A1380000L ^ 0 ^ 0 ^ 0x46E1980000000000L ^ 0x552E178000000000L ^ 0x7100000000000000L ^ 0 ^ 0 ^ 0x756A490948B0EA00L ^ -4858255253784494080L ^ 0x6800000000000000L ^ 0x1040000000000000L ^ 0x38A2279F00000000L ^ 0 ^ 0 ^ 0x6C1D620000000000L ^ 0x475FF9633600L ^ 0 ^ -977125538244067328L ^ -82522213862559L ^ 0 ^ 0x65C2000000000000L ^ 0 ^ -1326310090260611072L ^ long.MinValue ^ 0x4901000000000000L ^ -8134912768680656896L ^ 0x32119EEDDL ^ 0 ^ 0x1F200000000000L ^ 0 ^ 0 ^ long.MinValue ^ 0x7056860000000000L ^ -2132450025513418752L ^ 0x295ACC538000000L ^ 0 ^ 0x72BFEAFD6C800000L ^ 0x3673E879F51A0000L ^ 0x6C4D000000000000L ^ -1785995011112828928L ^ 0x557E698D8AA28000L ^ 0 ^ 0 ^ 0 ^ 0x2481BAB400000000L ^ 0 ^ 0x229F347CCL ^ -1 ^ 0x2100000000000000L ^ 0x5E23C7EE93780000L ^ 0x43C5F8DE88F89400L ^ -3668643526L ^ 0x4F7D959CAFA20000L ^ 0x1000000000000000L ^ 0x2DC9F0CB8FC60000L ^ 0x5360000000000L ^ 0x39CEA36000000000L ^ 0 ^ -6708111644968353792L ^ 0 ^ 0x3CB438B898220000L ^ 0 ^ 0x530CDCA00EF12100L ^ 0 ^ 0x1AFC460000000000L ^ -8556839292003942400L ^ -8523625244752084992L ^ 0 ^ 0 ^ 0 ^ 0 ^ -102346541645774848L ^ 0x6C00000000000L ^ 0 ^ 0 ^ 0x3A00000000000000L ^ 0x7A40000000000000L ^ 0 ^ 0x150A9A0000L ^ 0x90EBB9BBu ^ -8796093022208L ^ 0x21B39D0000000000L ^ 0 ^ 0x5ADD2ECF5D000000L ^ 0 ^ 0 ^ 0x1D11CC1A4000000L ^ -2864289363007635456L ^ 0x1439FCC7C0000000L ^ 0x887C00000000L ^ 0 ^ -8546673436703850496L ^ 0 ^ 0x55653E3DAF000000L ^ 0x1C1DCB20FEFA5200L ^ 0x3626000000000000L ^ 0x6900000000000000L ^ 0x2A00000000000000L ^ -1637459926619565056L ^ 0x64297DB3 ^ -34468790272L ^ -35184372088832L ^ 0x6D00000000000000L ^ -4611686018427387904L ^ 0x3A8074AC00000000L ^ -215993974035316736L ^ 0x61839C0000000000L ^ 0x35D21362DE1D0000L ^ -6812843387394195456L ^ 0x41A5000000000000L ^ 0x18669D6BFC296200L ^ -1191817827951050752L ^ 0x44BD2F0000000000L ^ 0x1C5D000000000000L ^ 0 ^ 0 ^ 0x169C369637000000L ^ 0 ^ 0x5410975660600000L ^ 0 ^ -1879048192 ^ 0x6100000000000000L ^ 0x188211800000L ^ 0x1537B10000000000L ^ 0 ^ long.MinValue ^ 0xCF193E135000000L ^ 0x3A64A65800000000L ^ 0x775C1B5379000000L ^ 0 ^ 0x24E8000000000000L ^ long.MinValue ^ 0x1A4000000000L ^ 0 ^ -9119789245425254400L ^ 0x3900000000000000L ^ 0 ^ -1029424358575046656L ^ -8285429028383358976L ^ -218296301002752L ^ 0x3F69013CC790000L ^ 0x57FD57248D000000L ^ 0x1A66CAA7640CEA00L ^ 0 ^ 0x2A62DD0000000000L ^ -63350767616L ^ -3381370963363233792L ^ 0x3BB4FD8A00000L ^ -506889355919360000L ^ -4098128227564781568L ^ 0x7720D516CD000000L ^ 0 ^ -204509162766336L ^ 0x38D2CAC662060000L ^ 0x312F2F2800000000L ^ 0 ^ 0xC27EFE ^ -2933944628392493056L ^ 0 ^ -1015596900344135680L ^ 0x7100000000000000L ^ -2483813344364L ^ 0x165CEB0000000000L ^ -8918899510332751872L ^ 0 ^ -1662046838755164160L ^ -6854478632857894912L ^ -1886209865282486272L ^ 0x227074C2C7E40000L ^ 0 ^ -2480561153L ^ 0x236665289F545100L ^ 0x22FD000000000000L ^ -7208292678583189504L ^ 0 ^ 0 ^ 0x8E0000000L ^ -2515566150608224256L ^ long.MinValue ^ 0x6D4B15EC00000000L ^ 0 ^ 0x5C ^ 0x340A8949C3C0000L ^ 0 ^ 0 ^ 0 ^ -6500890165223021312L ^ 0 ^ 0x98F000000000000L ^ 0x596420EC00000000L ^ -103691894492L ^ 0x296C025682D60000L ^ 0x6C1919D2CC000000L ^ -6869221376L ^ -6946316783670263808L ^ 0x5FA2C5C57L ^ -7250736839250100992L ^ 0x2EA767F00000000L ^ -9695232 ^ 0x27E4000000000000L ^ -5856632329836953600L ^ 0x4000000000000000L ^ 0x7C35D88248EE0000L ^ 0 ^ 0x448046F25E300000L ^ 0x87CCB44C000000L ^ -54980231168L ^ 0x3DA9000000000000L ^ 0x3C30000000000000L ^ -5619446959279439872L ^ -806879232 ^ -4375209654595092480L ^ -6649990085934579712L ^ 0x375D22D3B4BF0000L ^ 0x3528F41ECDE80000L ^ 0 ^ 0xB98F27648000000L ^ 0x5D088A0858000000L ^ -4942815240896118784L ^ 0x5F33650000000000L ^ long.MinValue ^ -525805904864L ^ 0x27DCB80000000000L ^ 0 ^ 0x11E8570498000000L ^ 0x747A000000000000L ^ -2377900603251621888L ^ 0x39F0000000L ^ -2738188573441261568L ^ 0x2701194F2CD00000L ^ 0 ^ 0x2CD384C7CB230000L ^ 0xA3FECE5527D5800L ^ -2738188573441261568L ^ 0x1D5C832A0C97CB00L ^ 0x4BA5E72EA5A02L ^ 0x4EC4DE60189B0000L ^ 0x164A6E88000L ^ 0x6A00000000L ^ 0x47EF3FE000000000L ^ 0x1F49000000000000L ^ 0x25FB040000000000L ^ 0x35E1F86C ^ 0x6B63AF0C6A9C0000L ^ 0x68CDA10000000000L ^ -4250999496747515904L ^ -7133701809754865664L ^ 0x4865130000000000L ^ 0x2FFEEB2A0000000L ^ -5764607523034234880L ^ 0x8F1AD523B000000L ^ 0x87560000000L ^ 0x1E94000000000000L ^ -239286650941734912L ^ -5024187596796854272L ^ 0x794EF20000000000L ^ 0x566C154F862E0000L ^ 0x4B9A019BDF98E100L ^ 0 ^ 0x3378586140L ^ -9066553134481932288L ^ 0x1E46BCE300000000L ^ 0x98F536900000000L;
            var bytes = new byte[SystemInfo.deviceUniqueIdentifier.Length / 2];
            new System.Random(Environment.TickCount).NextBytes(bytes);
            SpoofedHWID = new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(string.Join("", bytes.Select(it => it.ToString("x2")))));
            var icallName = "UnityEngine.SystemInfo::GetDeviceUniqueIdentifier";
            var icallAddress = IL2CPP.il2cpp_resolve_icall(icallName);
            CompatHook((IntPtr) (&icallAddress), typeof(Patches).GetMethod(nameof(GetHWIDPatch), BindingFlags.Static | BindingFlags.NonPublic)!.MethodHandle.GetFunctionPointer());

            var DeviceNameByte = new byte[SystemInfo.deviceName.Length / 2];
            new System.Random(Environment.TickCount).NextBytes(DeviceNameByte);
            SpoofedDeviceName = new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(string.Join("", DeviceNameByte.Select(it => it.ToString("x2")))));
            var DeviceName = "UnityEngine.SystemInfo::GetDeviceName";
            var DeviceNameAddress = IL2CPP.il2cpp_resolve_icall(DeviceName);
            CompatHook((IntPtr) (&DeviceNameAddress), typeof(Patches).GetMethod(nameof(GetDeviceName), BindingFlags.Static | BindingFlags.NonPublic)!.MethodHandle.GetFunctionPointer());

            var GraphicDeviceNameByte = new byte[SystemInfo.graphicsDeviceName.Length / 2];
            new System.Random(Environment.TickCount).NextBytes(GraphicDeviceNameByte);
            SpoofedGraphicDeviceName = new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(string.Join("", GraphicDeviceNameByte.Select(it => it.ToString("x2")))));
            var GraphicDeviceName = "UnityEngine.SystemInfo::GetGraphicsDeviceName";
            var GraphicDeviceNameAddress = IL2CPP.il2cpp_resolve_icall(GraphicDeviceName);
            CompatHook((IntPtr) (&GraphicDeviceNameAddress), typeof(Patches).GetMethod(nameof(GetGraphicDeviceName), BindingFlags.Static | BindingFlags.NonPublic)!.MethodHandle.GetFunctionPointer());

            var GraphicDeviceIdByte = new byte[SystemInfo.graphicsDeviceID.ToString().Length / 2];
            new System.Random(Environment.TickCount).NextBytes(GraphicDeviceIdByte);
            SpoofedGraphicDeviceId = new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(string.Join("", GraphicDeviceIdByte.Select(it => it.ToString("x2")))));
            var GraphicDeviceId = "UnityEngine.SystemInfo::GetGraphicsDeviceID";
            var GraphicDeviceIdAddress = IL2CPP.il2cpp_resolve_icall(GraphicDeviceId);
            CompatHook((IntPtr) (&GraphicDeviceIdAddress), typeof(Patches).GetMethod(nameof(GetGraphicDeviceId), BindingFlags.Static | BindingFlags.NonPublic)!.MethodHandle.GetFunctionPointer());

            var VendorDeviceIdByte = new byte[SystemInfo.graphicsDeviceVendorID.ToString().Length / 2];
            new System.Random(Environment.TickCount).NextBytes(VendorDeviceIdByte);
            SpoofedVendorDeviceId = new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(string.Join("", VendorDeviceIdByte.Select(it => it.ToString("x2")))));
            var VendorDeviceId = "UnityEngine.SystemInfo::GetGraphicsDeviceVendorID";
            var VendorDeviceIdAddress = IL2CPP.il2cpp_resolve_icall(VendorDeviceId);
            CompatHook((IntPtr) (&VendorDeviceIdAddress), typeof(Patches).GetMethod(nameof(GetVendorDeviceId), BindingFlags.Static | BindingFlags.NonPublic)!.MethodHandle.GetFunctionPointer());

            var matchingMethods = typeof(Analytics).GetMethods().Where(it => it.Name.StartsWith("Method_Private_Static_Void_"));
            foreach (var method in matchingMethods)
            {
                Instance.Patch(method, GetPatch(nameof(ReturnFalse)));
            }

            var matchingMethods2 = typeof(Analytics).GetMethods().Where(it => it.Name.StartsWith("Method_Public_Static_Void_"));
            foreach (var method in matchingMethods2)
            {
                Instance.Patch(method, GetPatch(nameof(ReturnFalse)));
            }
            Instance.Patch(AccessTools.Property(typeof(PhotonPeer), "RoundTripTime").GetMethod, null, GetPatch(nameof(Ping)));
            Instance.Patch(AccessTools.Property(typeof(Time), "smoothDeltaTime").GetMethod, null, GetPatch(nameof(Frames)));
            Instance.Patch(AccessTools.Property(typeof(HTTPRequest), "Timeout").SetMethod, GetPatch(nameof(Timout)));
            Instance.Patch(AccessTools.Method(typeof(UserInteractMenu), "Update"), GetPatch(nameof(Yoink)));
            Instance.Patch(AccessTools.Method(typeof(ImageDownloader), "DownloadImage"), GetPatch(nameof(NoIPLog)));
            Instance.Patch(AccessTools.Method(typeof(LoadBalancingClient), "Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), GetPatch(nameof(OpRaiseEvent)));
            Instance.Patch(AccessTools.Method(typeof(LoadBalancingPeer), "Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), GetPatch(nameof(OpRaiseEvent2)));
            Instance.Patch(AccessTools.Method(typeof(LoadBalancingPeer), "Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_1"), GetPatch(nameof(OpRaiseEvent2)));
            Instance.Patch(AccessTools.Method(typeof(LoadBalancingPeer), "Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_2"), GetPatch(nameof(OpRaiseEvent2)));
            Instance.Patch(AccessTools.Method(typeof(LoadBalancingClient), "OnEvent"), GetPatch(nameof(OnEvent)));
            //Instance.Patch(AccessTools.Method(typeof(VRCNetworkingClient), "OnEvent"), GetPatch(nameof(OnEvent2)));
            //Instance.Patch(AccessTools.Method(typeof(LoadBalancingClient), "Method_Public_Boolean_EnterRoomParams_0"), GetPatch(nameof(OpJoinRoom)));
            Instance.Patch(AccessTools.Method(typeof(VRC_EventDispatcherRFC), "Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0"), GetPatch(nameof(RPC)));
            Instance.Patch(AccessTools.Method(typeof(UdonSync), "UdonSyncRunProgramAsRPC"), GetPatch(nameof(UdonEvents)));
            Instance.Patch(AccessTools.Method(typeof(PhotonPeer), "SendOperation"), GetPatch(nameof(Operation)));
            Instance.Patch(AccessTools.Method(typeof(API), "SendPutRequest"), GetPatch(nameof(SendPutRequest)));
            Instance.Patch(AccessTools.Method(typeof(PortalInternal), "Method_Private_Void_0"), GetPatch(nameof(PortalCooldown)));
            Instance.Patch(AccessTools.Method(typeof(PortalInternal), "Method_Private_Void_1"), GetPatch(nameof(PortalCooldown)));
            Instance.Patch(AccessTools.Method(typeof(PortalInternal), "ConfigurePortal"), GetPatch(nameof(ConfigurePortal)));
            Instance.Patch(AccessTools.Method(typeof(PeerBase).Assembly.GetType("ExitGames.Client.Photon.EnetPeer"), "EnqueueOperation"), GetPatch(nameof(EnqueueOperation)));
            Instance.Patch(AccessTools.Method(typeof(PerformanceScannerSet), "Method_Public_IEnumerator_GameObject_AvatarPerformanceStats_MulticastDelegateNPublicSealedBoCoUnique_0"), GetPatch(nameof(NoStats)));
            Instance.Patch(AccessTools.Method(typeof(AssetManagement), "Method_Public_Static_Object_Object_Vector3_Quaternion_Boolean_Boolean_Boolean_0"), GetPatch(nameof(AssetLoaded)));
            Instance.Patch(typeof(IKSolverHeuristic).GetMethods().Where(m => m.Name.Equals("IsValid") && m.GetParameters().Length == 1).First(), prefix: new HarmonyMethod(typeof(Patches).GetMethod("IKSolver", BindingFlags.NonPublic | BindingFlags.Static)));
            //Instance.Patch(typeof(DynamicBone).GetMethod(nameof(DynamicBone.OnEnable)), new HarmonyMethod(typeof(DynamicPerformances), nameof(DynamicPerformances.OnEnablePrefix)));
            //Instance.Patch(typeof(DynamicBone).GetMethod(nameof(DynamicBone.OnDisable)), new HarmonyMethod(typeof(DynamicPerformances), nameof(DynamicPerformances.OnDisablePrefix)));
            //Instance.Patch(typeof(AvatarClone).GetMethod(nameof(AvatarClone.LateUpdate)), new HarmonyMethod(typeof(DynamicPerformances), nameof(DynamicPerformances.LateUpdatePrefix)));
            //Instance.Patch(XrefScanner.XrefScan(typeof(DynamicBone).GetMethod(nameof(DynamicBone.OnEnable))).Single(it => it.Type == XrefType.Method && it.TryResolve() != null).TryResolve(), new HarmonyMethod(typeof(DynamicPerformances), nameof(DynamicPerformances.ResetParticlesPatch)));
            Instance.Patch(AccessTools.Method(typeof(Analytics), "Awake"), GetPatch(nameof(ReturnFalse)));
            Instance.Patch(AccessTools.Method(typeof(Analytics), "Update"), GetPatch(nameof(ReturnFalse)));
            Instance.Patch(AccessTools.Method(typeof(Analytics), "OnEnable"), GetPatch(nameof(ReturnFalse)));
            PatchSucess = true;
        }

        #region Patches

        private static bool AssetLoaded(ref UnityEngine.Object __0, ref Vector3 __1, ref Quaternion __2, ref bool __3, ref bool __4, ref bool __5)
        {
            var Object = __0.TryCast<GameObject>();
            if (Object != null)
            {
                if (Object.name.Contains("_CustomAvatar") || Object.name == "Avatar" || Object.name.ToLower().Contains("prefab")) return !Settings.IsCrashing;
            }
            return true;
        }

        private static bool NoStats()
        {
            return !Settings.NoStats;
        }
        private static bool OnPlayerJoin(ref Player __0)
        {
            return true;
        }

        private static bool OpJoinRoom(ref EnterRoomParams __0)
        {
            var No = long.Parse("2173162573851") ^ -1822076715 ^ 0xCCA000000000000L ^ 0x22A7DA944CL ^ -7968319096167071744L ^ -3816365552469803008L ^ 0x48E877F1F3000000L ^ -3805541685128069120L ^ -6026324156323004416L ^ 0x1C2942AACD390000L ^ -2340264320232849408L ^ 0x4230000000000000L ^ 0x3D9CF2DDDA149800L ^ 0x316991CC0L ^ 0 ^ -2145479602233933824L ^ 0x6BBF276000000000L ^ 0 ^ 0x2B98CF5627BC0000L ^ 0x1D2E9628A000000L ^ 0x2FEED00000000000L ^ 0x537F000000000000L ^ -6972949015494792192L ^ 0x6A00000000000000L ^ -279495115381760L ^ 0 ^ 0 ^ 0x269D9 ^ 0 ^ 0 ^ 0x29C1BC8F49000000L ^ 0x26420C4E67000000L ^ 0 ^ 0xBF9CC200000000L ^ 0x50575A0000000000L ^ 0x5EF87EAFC7079000L ^ 0x67909B0000000000L ^ -2319916758049226752L ^ 0x490685E592FC6400L ^ 0xFDBF8 ^ 0 ^ 0x2000000000000000L ^ 0xC0000000u ^ 0x820000 ^ 0 ^ -115975173177344L ^ 0x4922390000000000L ^ 0x578E7ABDF9000000L ^ 0x259AD71700000000L ^ 0x63BB4A0000000L ^ 0x6100000000000000L ^ -5188146770730811392L ^ 0x57FD3E0000000000L ^ -4276730796141707264L ^ -6786001544839371008L ^ 0 ^ 0x5277C5E0D600000L ^ 0 ^ -5941371953334321152L ^ -7540573888590118912L ^ 0x116302D900000000L ^ 0 ^ 0x5400000000000000L ^ -3575858104132173824L ^ 0x436191A723B70000L ^ 0x6891E861A200000L ^ -4611686018427387904L ^ 0x1F2B028600000000L ^ -8745643619089645568L ^ 0 ^ 0x7E0F430000000000L ^ 0x4924000000000L ^ 0x46B7F4BF28D26D00L ^ 0 ^ 0 ^ 0 ^ 0xD894000000L ^ 0x4A4A44376C2A0000L ^ 0 ^ 0x2A52138F90000000L ^ 0 ^ 0x52BC0 ^ 0x39682E22000000L ^ 0xAF7DC00000000L ^ 0x120BCA6F0000L ^ 0x4F034386D1BC0000L ^ 0x160840000000000L ^ 0x789278100D000000L ^ 0x5C054D0E930L ^ -17592186044416L ^ -2830512365802356736L ^ 0x26657C8418F6EFL ^ 0 ^ 0x4C00000000000000L ^ 0x46B0D08388000000L ^ -1209379031510679552L ^ 0 ^ 0x53421C1372000000L ^ 0x355E000000000000L ^ 0 ^ 0x37D128F300000000L ^ 0x30000 ^ 0x14E20C1D62C42800L ^ 0 ^ -8515242394525958144L ^ 0x63286B0000000000L ^ 0x45F9710000000000L ^ 0x40B5B5E9A1380000L ^ 0 ^ 0 ^ 0x46E1980000000000L ^ 0x552E178000000000L ^ 0x7100000000000000L ^ 0 ^ 0 ^ 0x756A490948B0EA00L ^ -4858255253784494080L ^ 0x6800000000000000L ^ 0x1040000000000000L ^ 0x38A2279F00000000L ^ 0 ^ 0 ^ 0x6C1D620000000000L ^ 0x475FF9633600L ^ 0 ^ -977125538244067328L ^ -82522213862559L ^ 0 ^ 0x65C2000000000000L ^ 0 ^ -1326310090260611072L ^ long.MinValue ^ 0x4901000000000000L ^ -8134912768680656896L ^ 0x32119EEDDL ^ 0 ^ 0x1F200000000000L ^ 0 ^ 0 ^ long.MinValue ^ 0x7056860000000000L ^ -2132450025513418752L ^ 0x295ACC538000000L ^ 0 ^ 0x72BFEAFD6C800000L ^ 0x3673E879F51A0000L ^ 0x6C4D000000000000L ^ -1785995011112828928L ^ 0x557E698D8AA28000L ^ 0 ^ 0 ^ 0 ^ 0x2481BAB400000000L ^ 0 ^ 0x229F347CCL ^ -1 ^ 0x2100000000000000L ^ 0x5E23C7EE93780000L ^ 0x43C5F8DE88F89400L ^ -3668643526L ^ 0x4F7D959CAFA20000L ^ 0x1000000000000000L ^ 0x2DC9F0CB8FC60000L ^ 0x5360000000000L ^ 0x39CEA36000000000L ^ 0 ^ -6708111644968353792L ^ 0 ^ 0x3CB438B898220000L ^ 0 ^ 0x530CDCA00EF12100L ^ 0 ^ 0x1AFC460000000000L ^ -8556839292003942400L ^ -8523625244752084992L ^ 0 ^ 0 ^ 0 ^ 0 ^ -102346541645774848L ^ 0x6C00000000000L ^ 0 ^ 0 ^ 0x3A00000000000000L ^ 0x7A40000000000000L ^ 0 ^ 0x150A9A0000L ^ 0x90EBB9BBu ^ -8796093022208L ^ 0x21B39D0000000000L ^ 0 ^ 0x5ADD2ECF5D000000L ^ 0 ^ 0 ^ 0x1D11CC1A4000000L ^ -2864289363007635456L ^ 0x1439FCC7C0000000L ^ 0x887C00000000L ^ 0 ^ -8546673436703850496L ^ 0 ^ 0x55653E3DAF000000L ^ 0x1C1DCB20FEFA5200L ^ 0x3626000000000000L ^ 0x6900000000000000L ^ 0x2A00000000000000L ^ -1637459926619565056L ^ 0x64297DB3 ^ -34468790272L ^ -35184372088832L ^ 0x6D00000000000000L ^ -4611686018427387904L ^ 0x3A8074AC00000000L ^ -215993974035316736L ^ 0x61839C0000000000L ^ 0x35D21362DE1D0000L ^ -6812843387394195456L ^ 0x41A5000000000000L ^ 0x18669D6BFC296200L ^ -1191817827951050752L ^ 0x44BD2F0000000000L ^ 0x1C5D000000000000L ^ 0 ^ 0 ^ 0x169C369637000000L ^ 0 ^ 0x5410975660600000L ^ 0 ^ -1879048192 ^ 0x6100000000000000L ^ 0x188211800000L ^ 0x1537B10000000000L ^ 0 ^ long.MinValue ^ 0xCF193E135000000L ^ 0x3A64A65800000000L ^ 0x775C1B5379000000L ^ 0 ^ 0x24E8000000000000L ^ long.MinValue ^ 0x1A4000000000L ^ 0 ^ -9119789245425254400L ^ 0x3900000000000000L ^ 0 ^ -1029424358575046656L ^ -8285429028383358976L ^ -218296301002752L ^ 0x3F69013CC790000L ^ 0x57FD57248D000000L ^ 0x1A66CAA7640CEA00L ^ 0 ^ 0x2A62DD0000000000L ^ -63350767616L ^ -3381370963363233792L ^ 0x3BB4FD8A00000L ^ -506889355919360000L ^ -4098128227564781568L ^ 0x7720D516CD000000L ^ 0 ^ -204509162766336L ^ 0x38D2CAC662060000L ^ 0x312F2F2800000000L ^ 0 ^ 0xC27EFE ^ -2933944628392493056L ^ 0 ^ -1015596900344135680L ^ 0x7100000000000000L ^ -2483813344364L ^ 0x165CEB0000000000L ^ -8918899510332751872L ^ 0 ^ -1662046838755164160L ^ -6854478632857894912L ^ -1886209865282486272L ^ 0x227074C2C7E40000L ^ 0 ^ -2480561153L ^ 0x236665289F545100L ^ 0x22FD000000000000L ^ -7208292678583189504L ^ 0 ^ 0 ^ 0x8E0000000L ^ -2515566150608224256L ^ long.MinValue ^ 0x6D4B15EC00000000L ^ 0 ^ 0x5C ^ 0x340A8949C3C0000L ^ 0 ^ 0 ^ 0 ^ -6500890165223021312L ^ 0 ^ 0x98F000000000000L ^ 0x596420EC00000000L ^ -103691894492L ^ 0x296C025682D60000L ^ 0x6C1919D2CC000000L ^ -6869221376L ^ -6946316783670263808L ^ 0x5FA2C5C57L ^ -7250736839250100992L ^ 0x2EA767F00000000L ^ -9695232 ^ 0x27E4000000000000L ^ -5856632329836953600L ^ 0x4000000000000000L ^ 0x7C35D88248EE0000L ^ 0 ^ 0x448046F25E300000L ^ 0x87CCB44C000000L ^ -54980231168L ^ 0x3DA9000000000000L ^ 0x3C30000000000000L ^ -5619446959279439872L ^ -806879232 ^ -4375209654595092480L ^ -6649990085934579712L ^ 0x375D22D3B4BF0000L ^ 0x3528F41ECDE80000L ^ 0 ^ 0xB98F27648000000L ^ 0x5D088A0858000000L ^ -4942815240896118784L ^ 0x5F33650000000000L ^ long.MinValue ^ -525805904864L ^ 0x27DCB80000000000L ^ 0 ^ 0x11E8570498000000L ^ 0x747A000000000000L ^ -2377900603251621888L ^ 0x39F0000000L ^ -2738188573441261568L ^ 0x2701194F2CD00000L ^ 0 ^ 0x2CD384C7CB230000L ^ 0xA3FECE5527D5800L ^ -2738188573441261568L ^ 0x1D5C832A0C97CB00L ^ 0x4BA5E72EA5A02L ^ 0x4EC4DE60189B0000L ^ 0x164A6E88000L ^ 0x6A00000000L ^ 0x47EF3FE000000000L ^ 0x1F49000000000000L ^ 0x25FB040000000000L ^ 0x35E1F86C ^ 0x6B63AF0C6A9C0000L ^ 0x68CDA10000000000L ^ -4250999496747515904L ^ -7133701809754865664L ^ 0x4865130000000000L ^ 0x2FFEEB2A0000000L ^ -5764607523034234880L ^ 0x8F1AD523B000000L ^ 0x87560000000L ^ 0x1E94000000000000L ^ -239286650941734912L ^ -5024187596796854272L ^ 0x794EF20000000000L ^ 0x566C154F862E0000L ^ 0x4B9A019BDF98E100L ^ 0 ^ 0x3378586140L ^ -9066553134481932288L ^ 0x1E46BCE300000000L ^ 0x98F536900000000L;
            EvoConsole.Log("Hashtable");
            var Object = JsonConvert.SerializeObject(Serialize.FromIL2CPPToManaged<object>(__0.field_Public_Hashtable_0));
            EvoConsole.Log(Object);

            EvoConsole.Log("ExpectedUser");
            var Object1 = JsonConvert.SerializeObject(__0.field_Public_ArrayOf_0);
            EvoConsole.Log(Object1);

            EvoConsole.Log("Some Object");
            EvoConsole.Log(JsonConvert.SerializeObject(__0.field_Public_ObjectPublicStObBoObUnique_0));

            EvoConsole.Log("Some famOrAssem Bool_0");
            EvoConsole.Log(__0.field_FamOrAssem_Boolean_0.ToString());

            EvoConsole.Log("Some string_0");
            EvoConsole.Log(__0.field_Public_String_0);

            EvoConsole.Log("Some bool_0");
            EvoConsole.Log(__0.field_Public_Boolean_0.ToString());

            EvoConsole.Log("Some bool_1");
            EvoConsole.Log(__0.field_Public_Boolean_1.ToString());

            EvoConsole.Log("Room Hashtable");
            var Object2 = JsonConvert.SerializeObject(Serialize.FromIL2CPPToManaged<object>(__0.field_Public_RoomOptions_0.field_Public_Hashtable_0));
            EvoConsole.Log(Object2);

            EvoConsole.Log("Room array_0");
            EvoConsole.Log(JsonConvert.SerializeObject(__0.field_Public_RoomOptions_0.field_Public_ArrayOf_0));

            EvoConsole.Log("Room array_1");
            EvoConsole.Log(JsonConvert.SerializeObject(__0.field_Public_RoomOptions_0.field_Public_ArrayOf_1));

            EvoConsole.Log("Room bool_0");
            EvoConsole.Log(__0.field_Public_RoomOptions_0.field_Private_Boolean_0.ToString());

            EvoConsole.Log("Room bool_1");
            EvoConsole.Log(__0.field_Public_RoomOptions_0.field_Private_Boolean_1.ToString());

            EvoConsole.Log("Room bool_2");
            EvoConsole.Log(__0.field_Public_RoomOptions_0.field_Private_Boolean_2.ToString());

            EvoConsole.Log("Room bool_3");
            EvoConsole.Log(__0.field_Public_RoomOptions_0.field_Private_Boolean_3.ToString());

            EvoConsole.Log("Room bool_4");
            EvoConsole.Log(__0.field_Public_RoomOptions_0.field_Private_Boolean_4.ToString());

            EvoConsole.Log("Room bool_5");
            EvoConsole.Log(__0.field_Public_RoomOptions_0.field_Private_Boolean_5.ToString());

            EvoConsole.Log("Room bool_6");
            EvoConsole.Log(__0.field_Public_RoomOptions_0.field_Private_Boolean_6.ToString());

            EvoConsole.Log("Room byte_0");
            EvoConsole.Log(__0.field_Public_RoomOptions_0.field_Public_Byte_0.ToString());

            EvoConsole.Log("Room int_0");
            EvoConsole.Log(__0.field_Public_RoomOptions_0.field_Public_Int32_0.ToString());

            EvoConsole.Log("Room int_1");
            EvoConsole.Log(__0.field_Public_RoomOptions_0.field_Public_Int32_1.ToString());

            return true;
        }

        private static void OnPlayerJoinOrLeft()
        {
            if (ReferenceEquals(NetworkManager.field_Internal_Static_NetworkManager_0, null)) return;
            var field0 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0;
            var field1 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1;

            ADlg(field0, Event1);
            ADlg(field1, Event2);
        }
        private static void CompatHook(IntPtr first, IntPtr second)
        {
            typeof(Imports).GetMethod("Hook", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)!.Invoke(null, new object[] { first, second });
        }
        private static IntPtr GetHWIDPatch() => SpoofedHWID.Pointer;
        private static IntPtr GetDeviceName() => SpoofedDeviceName.Pointer;
        private static IntPtr GetGraphicDeviceName() => SpoofedGraphicDeviceName.Pointer;
        private static IntPtr GetGraphicDeviceId() => SpoofedGraphicDeviceId.Pointer;
        private static IntPtr GetVendorDeviceId() => SpoofedVendorDeviceId.Pointer;
        private static bool ReturnTrue() => true;
        private static bool ReturnFalse() => false;
        private static bool HasSpoofedQuest = false;

        private static void Platform(ref string __result)
        {
            if (Settings.QuestSpoof && RoomManager.field_Internal_Static_ApiWorld_0 != null && HasSpoofedQuest == false)
            {
                __result = "android";
                while (VRCPlayer.field_Internal_Static_VRCPlayer_0 != true) return;
                HasSpoofedQuest = true;
            }
        }

        private static bool Timout(ref Il2CppSystem.TimeSpan __0)
        {

            return false;
        }

        private static bool SendPutRequest(ref string __0, ref ApiContainer __1, ref Il2CppSystem.Collections.Generic.Dictionary<string, Il2CppSystem.Object> __2, ref CredentialsBundle __3)
        {
            try
            {
                if (__2 != null && (__0 == "visits" || __0 == "joins"))
                {

                    if (Settings.OfflineSpoof) return false;
                    if (Settings.WorldSpoof)
                    {
                        __2.Clear();
                        __2.Add("userId", APIUser.CurrentUser.id);
                        __2.Add("worldId", "");
                    }
                }
                /* if (Settings.ApiLogs)
                 {
                     EvoConsole.Log($"Target: {__0}");
                     var Dictionary = Serialization.FromIL2CPPToManaged<object>(__2);
                     EvoConsole.Log(JsonConvert.SerializeObject(Dictionary));
                     EvoConsole.Log($"Api container: {__1.Code} / {__1.Text} ");
                 }*/
            }
            catch { }
            return true;
        }

        private bool disableCache => true;

        private static bool PortalCooldown() => !Settings.PortalBypass;

        private static bool IKSolver(ref IKSolverHeuristic __instance, ref bool __result, ref string message)
        {
            if (__instance.maxIterations > 9999 && Settings.CrashLogs) EvoVrConsole.Log(LogsType.Crash, "Prevented IK crash");
            if (__instance.maxIterations > 60)
            {
                __result = false;
                return false;
            }

            return true;
        }

        private static bool EnqueueOperation(Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> __0, byte __1)
        {
            if (__1 == 252 && Settings.SteamSpoof) PropertiesChanger(__0, 251);

            /*if (__1 == 226)
            {
                if (__0.ContainsKey(249))
                {
                    if (__0[249].Cast<Il2CppSystem.Collections.Hashtable>().ContainsKey("user"))
                    {
                        var User = JsonConvert.DeserializeObject<Hashtable>(JsonConvert.SerializeObject(Serialization.FromIL2CPPToManaged<object>(__0[249].Cast<Il2CppSystem.Collections.Hashtable>()["user"])));
                        User["last_platform"] = "android";
                        EvoConsole.LogSuccess(JsonConvert.SerializeObject(User));
                        __0[249].Cast<Il2CppSystem.Collections.Hashtable>()["user"] = Serialization.FromManagedToIL2CPP<Il2CppSystem.Collections>(JsonConvert.SerializeObject(User));
                    }
                }
                var Object = Serialization.FromIL2CPPToManaged<object>(__0);
                EvoConsole.Log(JsonConvert.SerializeObject(Object, Formatting.Indented));
            }
            /*if (__1 == 226)
            {
                if (__0.ContainsKey(248))
                {
                    var Token = __0[(byte)248].Cast<Il2CppSystem.Collections.Hashtable>()[Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>((byte)2)];
                    var JWT = MiscFunc.ConvertFromJWT(Token.ToString());
                    var Dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(JWT);
                    Dictionary["ip"] = "999.999.999.999";
                    EvoConsole.LogSuccess($"Success changing values:\n{JsonConvert.SerializeObject(Dictionary)}");
                    var NewToken = MiscFunc.ConvertToJWT(JsonConvert.SerializeObject(Dictionary));
                    var FullToken = $"{Token.ToString().Split('.')[0]}.{NewToken}.{Token.ToString().Split('.')[2]}";
                    EvoConsole.LogSuccess($"New token: {FullToken}");
                    __0[(byte)248].Cast<Il2CppSystem.Collections.Hashtable>()[Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>((byte)2)] = Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>($"{FullToken}");
                }
            }*/
            /* try
             {
                 EvoConsole.LogError($"Code: {__1}");
                 var Value = Serialization.FromIL2CPPToManaged<object>(__0);
                 EvoConsole.Log(JsonConvert.SerializeObject(Value, Formatting.Indented));
             }
             catch { }*/
            return true;
        }

        private static void PropertiesChanger(Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> param, byte propertyIndex)
        {
            try
            {
                var No = long.Parse("2173162573851") ^ -1822076715 ^ 0xCCA000000000000L ^ 0x22A7DA944CL ^ -7968319096167071744L ^ -3816365552469803008L ^ 0x48E877F1F3000000L ^ -3805541685128069120L ^ -6026324156323004416L ^ 0x1C2942AACD390000L ^ -2340264320232849408L ^ 0x4230000000000000L ^ 0x3D9CF2DDDA149800L ^ 0x316991CC0L ^ 0 ^ -2145479602233933824L ^ 0x6BBF276000000000L ^ 0 ^ 0x2B98CF5627BC0000L ^ 0x1D2E9628A000000L ^ 0x2FEED00000000000L ^ 0x537F000000000000L ^ -6972949015494792192L ^ 0x6A00000000000000L ^ -279495115381760L ^ 0 ^ 0 ^ 0x269D9 ^ 0 ^ 0 ^ 0x29C1BC8F49000000L ^ 0x26420C4E67000000L ^ 0 ^ 0xBF9CC200000000L ^ 0x50575A0000000000L ^ 0x5EF87EAFC7079000L ^ 0x67909B0000000000L ^ -2319916758049226752L ^ 0x490685E592FC6400L ^ 0xFDBF8 ^ 0 ^ 0x2000000000000000L ^ 0xC0000000u ^ 0x820000 ^ 0 ^ -115975173177344L ^ 0x4922390000000000L ^ 0x578E7ABDF9000000L ^ 0x259AD71700000000L ^ 0x63BB4A0000000L ^ 0x6100000000000000L ^ -5188146770730811392L ^ 0x57FD3E0000000000L ^ -4276730796141707264L ^ -6786001544839371008L ^ 0 ^ 0x5277C5E0D600000L ^ 0 ^ -5941371953334321152L ^ -7540573888590118912L ^ 0x116302D900000000L ^ 0 ^ 0x5400000000000000L ^ -3575858104132173824L ^ 0x436191A723B70000L ^ 0x6891E861A200000L ^ -4611686018427387904L ^ 0x1F2B028600000000L ^ -8745643619089645568L ^ 0 ^ 0x7E0F430000000000L ^ 0x4924000000000L ^ 0x46B7F4BF28D26D00L ^ 0 ^ 0 ^ 0 ^ 0xD894000000L ^ 0x4A4A44376C2A0000L ^ 0 ^ 0x2A52138F90000000L ^ 0 ^ 0x52BC0 ^ 0x39682E22000000L ^ 0xAF7DC00000000L ^ 0x120BCA6F0000L ^ 0x4F034386D1BC0000L ^ 0x160840000000000L ^ 0x789278100D000000L ^ 0x5C054D0E930L ^ -17592186044416L ^ -2830512365802356736L ^ 0x26657C8418F6EFL ^ 0 ^ 0x4C00000000000000L ^ 0x46B0D08388000000L ^ -1209379031510679552L ^ 0 ^ 0x53421C1372000000L ^ 0x355E000000000000L ^ 0 ^ 0x37D128F300000000L ^ 0x30000 ^ 0x14E20C1D62C42800L ^ 0 ^ -8515242394525958144L ^ 0x63286B0000000000L ^ 0x45F9710000000000L ^ 0x40B5B5E9A1380000L ^ 0 ^ 0 ^ 0x46E1980000000000L ^ 0x552E178000000000L ^ 0x7100000000000000L ^ 0 ^ 0 ^ 0x756A490948B0EA00L ^ -4858255253784494080L ^ 0x6800000000000000L ^ 0x1040000000000000L ^ 0x38A2279F00000000L ^ 0 ^ 0 ^ 0x6C1D620000000000L ^ 0x475FF9633600L ^ 0 ^ -977125538244067328L ^ -82522213862559L ^ 0 ^ 0x65C2000000000000L ^ 0 ^ -1326310090260611072L ^ long.MinValue ^ 0x4901000000000000L ^ -8134912768680656896L ^ 0x32119EEDDL ^ 0 ^ 0x1F200000000000L ^ 0 ^ 0 ^ long.MinValue ^ 0x7056860000000000L ^ -2132450025513418752L ^ 0x295ACC538000000L ^ 0 ^ 0x72BFEAFD6C800000L ^ 0x3673E879F51A0000L ^ 0x6C4D000000000000L ^ -1785995011112828928L ^ 0x557E698D8AA28000L ^ 0 ^ 0 ^ 0 ^ 0x2481BAB400000000L ^ 0 ^ 0x229F347CCL ^ -1 ^ 0x2100000000000000L ^ 0x5E23C7EE93780000L ^ 0x43C5F8DE88F89400L ^ -3668643526L ^ 0x4F7D959CAFA20000L ^ 0x1000000000000000L ^ 0x2DC9F0CB8FC60000L ^ 0x5360000000000L ^ 0x39CEA36000000000L ^ 0 ^ -6708111644968353792L ^ 0 ^ 0x3CB438B898220000L ^ 0 ^ 0x530CDCA00EF12100L ^ 0 ^ 0x1AFC460000000000L ^ -8556839292003942400L ^ -8523625244752084992L ^ 0 ^ 0 ^ 0 ^ 0 ^ -102346541645774848L ^ 0x6C00000000000L ^ 0 ^ 0 ^ 0x3A00000000000000L ^ 0x7A40000000000000L ^ 0 ^ 0x150A9A0000L ^ 0x90EBB9BBu ^ -8796093022208L ^ 0x21B39D0000000000L ^ 0 ^ 0x5ADD2ECF5D000000L ^ 0 ^ 0 ^ 0x1D11CC1A4000000L ^ -2864289363007635456L ^ 0x1439FCC7C0000000L ^ 0x887C00000000L ^ 0 ^ -8546673436703850496L ^ 0 ^ 0x55653E3DAF000000L ^ 0x1C1DCB20FEFA5200L ^ 0x3626000000000000L ^ 0x6900000000000000L ^ 0x2A00000000000000L ^ -1637459926619565056L ^ 0x64297DB3 ^ -34468790272L ^ -35184372088832L ^ 0x6D00000000000000L ^ -4611686018427387904L ^ 0x3A8074AC00000000L ^ -215993974035316736L ^ 0x61839C0000000000L ^ 0x35D21362DE1D0000L ^ -6812843387394195456L ^ 0x41A5000000000000L ^ 0x18669D6BFC296200L ^ -1191817827951050752L ^ 0x44BD2F0000000000L ^ 0x1C5D000000000000L ^ 0 ^ 0 ^ 0x169C369637000000L ^ 0 ^ 0x5410975660600000L ^ 0 ^ -1879048192 ^ 0x6100000000000000L ^ 0x188211800000L ^ 0x1537B10000000000L ^ 0 ^ long.MinValue ^ 0xCF193E135000000L ^ 0x3A64A65800000000L ^ 0x775C1B5379000000L ^ 0 ^ 0x24E8000000000000L ^ long.MinValue ^ 0x1A4000000000L ^ 0 ^ -9119789245425254400L ^ 0x3900000000000000L ^ 0 ^ -1029424358575046656L ^ -8285429028383358976L ^ -218296301002752L ^ 0x3F69013CC790000L ^ 0x57FD57248D000000L ^ 0x1A66CAA7640CEA00L ^ 0 ^ 0x2A62DD0000000000L ^ -63350767616L ^ -3381370963363233792L ^ 0x3BB4FD8A00000L ^ -506889355919360000L ^ -4098128227564781568L ^ 0x7720D516CD000000L ^ 0 ^ -204509162766336L ^ 0x38D2CAC662060000L ^ 0x312F2F2800000000L ^ 0 ^ 0xC27EFE ^ -2933944628392493056L ^ 0 ^ -1015596900344135680L ^ 0x7100000000000000L ^ -2483813344364L ^ 0x165CEB0000000000L ^ -8918899510332751872L ^ 0 ^ -1662046838755164160L ^ -6854478632857894912L ^ -1886209865282486272L ^ 0x227074C2C7E40000L ^ 0 ^ -2480561153L ^ 0x236665289F545100L ^ 0x22FD000000000000L ^ -7208292678583189504L ^ 0 ^ 0 ^ 0x8E0000000L ^ -2515566150608224256L ^ long.MinValue ^ 0x6D4B15EC00000000L ^ 0 ^ 0x5C ^ 0x340A8949C3C0000L ^ 0 ^ 0 ^ 0 ^ -6500890165223021312L ^ 0 ^ 0x98F000000000000L ^ 0x596420EC00000000L ^ -103691894492L ^ 0x296C025682D60000L ^ 0x6C1919D2CC000000L ^ -6869221376L ^ -6946316783670263808L ^ 0x5FA2C5C57L ^ -7250736839250100992L ^ 0x2EA767F00000000L ^ -9695232 ^ 0x27E4000000000000L ^ -5856632329836953600L ^ 0x4000000000000000L ^ 0x7C35D88248EE0000L ^ 0 ^ 0x448046F25E300000L ^ 0x87CCB44C000000L ^ -54980231168L ^ 0x3DA9000000000000L ^ 0x3C30000000000000L ^ -5619446959279439872L ^ -806879232 ^ -4375209654595092480L ^ -6649990085934579712L ^ 0x375D22D3B4BF0000L ^ 0x3528F41ECDE80000L ^ 0 ^ 0xB98F27648000000L ^ 0x5D088A0858000000L ^ -4942815240896118784L ^ 0x5F33650000000000L ^ long.MinValue ^ -525805904864L ^ 0x27DCB80000000000L ^ 0 ^ 0x11E8570498000000L ^ 0x747A000000000000L ^ -2377900603251621888L ^ 0x39F0000000L ^ -2738188573441261568L ^ 0x2701194F2CD00000L ^ 0 ^ 0x2CD384C7CB230000L ^ 0xA3FECE5527D5800L ^ -2738188573441261568L ^ 0x1D5C832A0C97CB00L ^ 0x4BA5E72EA5A02L ^ 0x4EC4DE60189B0000L ^ 0x164A6E88000L ^ 0x6A00000000L ^ 0x47EF3FE000000000L ^ 0x1F49000000000000L ^ 0x25FB040000000000L ^ 0x35E1F86C ^ 0x6B63AF0C6A9C0000L ^ 0x68CDA10000000000L ^ -4250999496747515904L ^ -7133701809754865664L ^ 0x4865130000000000L ^ 0x2FFEEB2A0000000L ^ -5764607523034234880L ^ 0x8F1AD523B000000L ^ 0x87560000000L ^ 0x1E94000000000000L ^ -239286650941734912L ^ -5024187596796854272L ^ 0x794EF20000000000L ^ 0x566C154F862E0000L ^ 0x4B9A019BDF98E100L ^ 0 ^ 0x3378586140L ^ -9066553134481932288L ^ 0x1E46BCE300000000L ^ 0x98F536900000000L;
                var Hashtable = param[propertyIndex].Cast<Il2CppSystem.Collections.Hashtable>();
                if (propertyIndex == 251)
                {
                    if (Hashtable.ContainsKey("steamUserID")) Hashtable["steamUserID"] = "999";
                }

            }
            catch { }
        }

        private static void Ping(ref int __result)
        {
            try
            {
                if (Settings.PingSpoof) __result = Settings.Ping;
            }
            catch { }
        }

        private static void Frames(ref float __result)
        {
            if (Settings.FramesSpoof) __result = (1f / Settings.Frames);
        }

        private static bool OpRaiseEvent(ref byte __0, ref Il2CppSystem.Object __1, ref RaiseEventOptions __2, ref SendOptions __3)
        {
            if (Settings.IsAdmin)
            {
                if (Settings.Send7 && __0 == 7 || Settings.Send7 && __0 == 8)
                {
                    object value = Serialize.FromIL2CPPToManaged<object>(__1);
                    var Serialized = JsonConvert.SerializeObject(value, Formatting.Indented);
                    Bot.PhotonBots.SendMessage($"{__0}>{Serialized}");
                }

                if (Settings.LoggerEnable && Settings.MyEventsLog)
                {
                    var Code = __0;
                    object value = Serialize.FromIL2CPPToManaged<object>(__1);
                    var Serialized = JsonConvert.SerializeObject(value, Formatting.Indented);
                    var TargetActors = "None";
                    if (__2.field_Public_ArrayOf_Int32_0 != null)
                    {
                        var List = new List<string>();
                        foreach (var Actor in __2.field_Public_ArrayOf_Int32_0)
                        {
                            List.Add(Actor.ToString());
                        }
                        TargetActors = string.Join(", ", List.ToArray());
                    }

                    try
                    {
                        var Dict = JsonConvert.DeserializeObject<Dictionary<byte, object>>(Serialized);
                        var AllKeys = new List<string>();
                        foreach (var Key in Dict.Keys)
                        {
                            AllKeys.Add($"({Key.GetType()}) {Key} : ({Dict[Key].GetType()}) {Dict[Key]}");
                        }
                        EvoConsole.Log($"\n-----------[OpRaiseEvent]-----------\n\nCode: {Code}\nSender: Me\nTarget Actors: {TargetActors}\nReceiver: {__2.field_Public_ReceiverGroup_0}\nCaching: {__2.field_Public_EventCaching_0}\nByte 0: {__2.field_Public_Byte_0}\nByte 1: {__2.field_Public_Byte_1}\nWebflags: {__2.field_Public_WebFlags_0}\nObject type: {__1}\nSerialized data:\n{Serialized}\nKeys and objects type:\n{string.Join("\n", AllKeys.ToArray())}");
                        if (Code == 33)
                        {
                            foreach (var Value in Dict[(byte)3] as JArray)
                            {
                                EvoConsole.Log($"Value: {Value} Type: {Value.GetType()}");
                            }
                        }
                    }
                    catch { EvoConsole.Log($"\n-----------[OpRaiseEvent]-----------\n\nCode: {Code}\nSender: Me\nTarget Actors: {TargetActors}\nReceiver: {__2.field_Public_ReceiverGroup_0}\nCaching: {__2.field_Public_EventCaching_0}\nByte 0: {__2.field_Public_Byte_0}\nByte 1: {__2.field_Public_Byte_1}\nWebflags: {__2.field_Public_WebFlags_0}\nObject type: {__1}\nSerialized data:\n{Serialized}"); }
                }
            }
            if (Settings.Serialize) if(__0 == 7 || __0 == 206 || __0 == 201 || __0 == 9 || __0 == 6) return false;
            if (Settings.MyEventsLog) Log(LogsType.Event, $"From: <color=white>You</color>, Event: <color=#00ffe1>{__0}</color>");
            if (Settings.InvisibleJoin && __0 == 202) return false;
            if (Settings.GhostMode && __0 == 33) return false;
            else __2.field_Public_ReceiverGroup_0 = ReceiverGroup.Others;
            if (__0 == 4 || __0 == 5) return !Settings.LockRoom;

            return true;
        }

        private static bool OpRaiseEvent2(ref byte __0, ref Il2CppSystem.Object __1, ref RaiseEventOptions __2, ref SendOptions __3)
        {
            if (Settings.Serialize) if (__0 == 7 || __0 == 206 || __0 == 201 || __0 == 9 || __0 == 6) return false;
            if (Settings.InvisibleJoin && __0 == 202) return false;
            if (Settings.GhostMode && __0 == 33) return false;
            else __2.field_Public_ReceiverGroup_0 = ReceiverGroup.Others;
            if (__0 == 4 || __0 == 5) return !Settings.LockRoom;
            return true;
        }

        private static bool NoIPLog(ref string __0)
        {
            try
            {
                if (__0.Contains("vrchat.cloud"))
                {
                    return !Settings.IpProtection;
                }
            }
            catch { }
            return true;
        }
        private static bool ConfigurePortal(ref string __0, ref string __1, ref int __2, ref Player __3)
        {
            EvoVrConsole.Log(EvoVrConsole.LogsType.RPC, __3.DisplayName() + " <color=white>Dropped A Portal</color>");
            if (Settings.PortalProtection) return false;
            if (Settings.LoggerEnable && Settings.IsAdmin) Logger.Logger.Save($"Portal Drop from: {__3.DisplayName()}");

            return true;
        }
        private static bool Yoink(ref UserInteractMenu __instance)
        {
            try
            {
                if (Settings.ForceClone)
                {
                    if (__instance.field_Public_MenuController_0.activeAvatar.releaseStatus == "public")
                    {
                        __instance.field_Public_Button_1.GetComponentInChildren<UnityEngine.UI.Text>().text = "Yoink!";
                        __instance.field_Public_Button_1.interactable = true;
                        __instance.field_Public_MenuController_0.activeUser.allowAvatarCopying = true;
                    }

                    if (__instance.field_Public_MenuController_0.activeAvatar.releaseStatus == "private")
                    {
                        __instance.field_Public_Button_1.GetComponentInChildren<UnityEngine.UI.Text>().text = "Private!";
                        __instance.field_Public_Button_1.interactable = false;
                        __instance.field_Public_MenuController_0.activeUser.allowAvatarCopying = false;
                    }
                }
                else
                {
                    __instance.field_Public_Button_1.GetComponentInChildren<UnityEngine.UI.Text>().text = "Clone";
                    __instance.field_Public_Button_1.GetComponentInChildren<Text>().color = Color.white;
                }

                if (__instance.field_Public_MenuController_0.activeAvatar.releaseStatus == "public")
                {
                    EvolveInteract.ButtonHidding.setActive(false);
                }

                if (__instance.field_Public_MenuController_0.activeAvatar.releaseStatus == "private")
                {
                    EvolveInteract.ButtonHidding.setActive(true);
                }
            }
            catch { }
            return true;
        }

        private static void Event1(Player player)
        {
            if (!SF)
            {
                AFF = true;
                SF = true;
            }

           (AFF ? Join : Leave)?.Invoke(player);
        }

        private static void Event2(Player player)
        {
            if (!SF)
            {
                AFF = false;
                SF = true;
            }

            (AFF ? Leave : Join)?.Invoke(player);
        }

        private static bool UdonEvents(string __0, ref Player __1)
        {
            if (Settings.UdonRPCList)
            {
                var SerializedUdonRPC = new SerializeUdonRPC
                {
                    EventName = __0,
                    Player = __1,
                };

                bool ShouldAdd = true;
                foreach (var RPC in UdonRPCList)
                {
                    if (ShouldAdd)
                    {
                        if (RPC.EventName == __0 && RPC.Player == __1) ShouldAdd = false;
                        else ShouldAdd = true;
                    }
                }
                if (ShouldAdd)
                {
                    UdonRPCMenu.UdonRPCList.Add(SerializedUdonRPC);
                    UdonRPCMenu.Refresh();
                    if (Settings.UdonRPCLogs) EvoVrConsole.Log(LogsType.RPC, "<color=#00ff6a>Added to the udon rpc list</color>.");
                }
            }

            if (Settings.UdonExploitProtections)
            {
                if (Settings.UdonRPCLogs) EvoVrConsole.Log(LogsType.RPC, $"Blocked <color=#00ffe1>{__0}</color> from <color=magenta>{__1.DisplayName()}</color>");
                return false;
            }
            else if (Settings.UdonRPCLogs) EvoVrConsole.Log(LogsType.RPC, $"<color=#00ffe1>{__0}</color> from <color=magenta>{__1.DisplayName()}</color>");

            switch (__0)
            {
                case "SyncFire":
                    if (Murder4.GoldenGun && Murder4.IsHoldingDGun && __1.DisplayName() == APIUser.CurrentUser.displayName)
                    {
                        var PatronSound = GameObject.Find("Patron skin sound");
                        Exploits.Exploits.SendUdonRPC(PatronSound, "Play");
                    }
                    break;

                case "SyncKill":
                    if (Murder4.FreeForAll && __1.DisplayName() == APIUser.CurrentUser.displayName)
                    {
                        Murder4.Kills++;
                        Log(LogsType.Info, $"You have killed {Murder4.Kills} players");
                    }
                    return !Settings.JarGameGodMode;

                case "SyncAssignM":
                    if (Murder4.SeeRoles) MelonCoroutines.Start(Murder4.CheckRoles());
                    break;

                case "SyncVotedOut":
                    return !Settings.AmongAntiVoteOut;

                case "SyncPenalty":
                    return !Murder4.NoBlindKill;
            }

            return true;
        }
        private static bool RPC(ref Player __0, ref VRC_EventHandler.VrcEvent __1, ref VRC_EventHandler.VrcBroadcastType __2, ref int __3, ref float __4)
        {
            try
            {
                var Player = __0;
                string Sender = Player.DisplayName();

                if (Settings.IsAdmin)
                {
                    if (Settings.LoggerEnable && Settings.RPCEventsLogs)
                    {
                        if (__1.ParameterObject != null) EvoConsole.Log($"\n-----------[RPC]-----------\n\nSender: {Sender}\nType: {__1.EventType}\nBroadcast: {__2}\nString: {__1.ParameterString}\nGameObject Name: {__1.ParameterObject.name}\nGameObject position: {__1.ParameterObject.transform.position.x}, {__1.ParameterObject.transform.position.y}, {__1.ParameterObject.transform.position.y}\nFloat: {__1.ParameterFloat}\nInt: {__1.ParameterInt}\nBool: {__1.ParameterBoolOp}");
                        else
                        {
                            var AllNames = new List<string>();
                            foreach (var Object in __1.ParameterObjects)
                            {
                                AllNames.Add(Object.name);
                            }
                            EvoConsole.Log($"\n-----------[RPC]-----------\n\nSender: {Sender}\nType: {__1.EventType}\nBroadcast: {__2}\nString: {__1.ParameterString}\nGameObjects: {string.Join(", ", AllNames)}\nFloat: {__1.ParameterFloat}\nInt: {__1.ParameterInt}\nBool: {__1.ParameterBoolOp}");
                        }
                    }
                }

                if (__1.ParameterObject.name.Contains($"R2V0SXNFdm9sdmVk")) GetIsEvolved.IsEvolved(__0.field_Private_APIUser_0.id , __1.ParameterString);
                else if (__1.ParameterObject.name.Contains($"Evolve message handler for {APIUser.CurrentUser.id}")) EvolveMessageReader.GetMessage(Sender, __1.ParameterString);
                else if (__1.ParameterObject.name.Contains($"Evolve Staff message handler for {APIUser.CurrentUser.id}")) EvolveMessageReader.GetStaffMessage(Sender, __1.ParameterString);
                else if (Functions.GetEvoID() != "-999" && __1.ParameterObject.name.Contains($"Q29tbWFuZCB0byBydW4= {APIUser.CurrentUser.id}")) EvolveCommands.GetCommand(Sender, __1.ParameterString);
                if (Settings.WorldTriggers) __2 = VRC_EventHandler.VrcBroadcastType.Always;

                if (Settings.RPCList)
                {
                    var SerializedRPC = new SerializeRPC
                    {
                        EventType = __1.EventType,
                        Name = __1.Name,
                        ParameterObject = __1.ParameterObject,
                        ParameterInt = __1.ParameterInt,
                        ParameterFloat = __1.ParameterFloat,
                        ParameterString = __1.ParameterString,
                        ParameterBoolOp = __1.ParameterBoolOp,
                        Broadcast = __2
                    };
                    bool ShouldAdd = true;
                    foreach (var RPC in RPCMenu.RPCList)
                    {
                        if (ShouldAdd)
                        {
                            if (RPC.ParameterString == SerializedRPC.ParameterString && RPC.ParameterObject.name == SerializedRPC.ParameterObject.name && RPC.ParameterBoolOp == SerializedRPC.ParameterBoolOp && RPC.ParameterInt == SerializedRPC.ParameterInt && RPC.ParameterFloat == SerializedRPC.ParameterFloat) ShouldAdd = false;
                            else ShouldAdd = true;
                        }
                    }
                    if (ShouldAdd)
                    {
                        RPCMenu.RPCList.Add(SerializedRPC);
                        RPCMenu.Refresh();
                        if (Settings.RPCEventsLogs) EvoVrConsole.Log(LogsType.RPC, "<color=#00ff6a>Added to the rpc list</color>.");
                    }
                }

                if (Settings.RPCBlock)
                {
                    if (__1.ParameterString.Length > 40)
                    {
                        if (Sender != APIUser.CurrentUser.displayName)
                        {
                            EvoVrConsole.Log(LogsType.Crash, $"Too long RPC blocked from: <color=magenta>{Sender}</color>");
                            return false;
                        }
                        else if (VRCPlayer.field_Internal_Static_VRCPlayer_0.GetIsMaster())
                        {
                            EvoVrConsole.Log(LogsType.Crash, $"Prevented from being disconnected by: <color=magenta>{Sender}</color>");
                            return false;
                        }
                    }

                    if (Sender != APIUser.CurrentUser.displayName && !APIUser.IsFriendsWith(Player.UserID()))
                    {
                        if (Settings.RPCEventsLogs) EvoVrConsole.Log(EvoVrConsole.LogsType.RPC, $"Blocked: <color=#00ffe1>{__1.ParameterObject.name}</color>, {Sender}");
                        return false;
                    }
                    else if (Settings.RPCEventsLogs) EvoVrConsole.Log(EvoVrConsole.LogsType.RPC, $"[{Sender}]: <color=#00ffe1>{__1.ParameterObject.name}</color>");
                }
                else if (Settings.RPCEventsLogs) Log(LogsType.RPC, $"[{Sender}]: <color=#00ffe1>{__1.ParameterObject.name}</color>");

                switch (__1.ParameterString)
                {
                    case "SpawnEmojiRPC": 
                    case "PlayEmoteRPC":
                        var Array = Networking.DecodeParameters(__1.ParameterBytes);
                        if (Array == null) Array = new Il2CppSystem.Object[0];
                        string DecodedParam = "";
                        foreach (Il2CppSystem.Object value in Array) DecodedParam = Il2CppSystem.Convert.ToString(value);
                        if (int.Parse(DecodedParam) < 0 || int.Parse(DecodedParam) > 57)
                        {
                            if (Sender != APIUser.CurrentUser.displayName)
                            {
                                Log(LogsType.Crash, $"<color=magenta>{Sender}</color> used an invalid Emote/Emoji ID: <color=white>{DecodedParam}</color>");
                                return false;
                            }
                        }
                        break;

                    case "Die Local":
                    case "Play sound":
                    case "dead":
                        return !Settings.MurderGodMode;

                    case "TeleportRPC":
                    case "Player Node":
                        return !Settings.JarGameGodMode;
                }
            }
            catch { }
            return true;
        }

        private static bool Operation(ref byte __0, ref Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> __1, ref SendOptions __2)
        {
            /*object value = Serialize.FromIL2CPPToManaged<object>(__1);
            EvoConsole.Log($"\nCode: {__0}\n{JsonConvert.SerializeObject(value, Formatting.Indented)}");*/
            /*if (__0 == 230 && __1.ContainsKey(216))
            {
                EvoConsole.LogSuccess(__1[216].ToString());
                __1[216] = "token=authcookie_21b1c661-d3f3-4edf-8145-da9aba20605b&user=usr_b1764f98-9439-43a4-9076-ccb2236c0923&hwid=56a9479870168eafa67f86eb0dfb27088e4aad17&platform=android";
                EvoConsole.LogSuccess(__1[216].ToString());
            }*/
            return true;
        }

        internal static List<byte> BlacklistedEvents = new List<byte>();

        private static List<string> ModeratedUsers = new List<string>();
        public static byte LastReceivedEvent;
        public static byte LastReceivedEvent2;
        public static int AmountOfSpam = 0;
        public static int AmountOfSpam2 = 0;

        private static bool OnEvent(ref EventData __0)
        {
            try
            {    
                var Parameters = __0.Parameters;
                if (Settings.IsAdmin)
                {
                    if (Settings.LoggerEnable && Settings.EventLog && __0.Code != 7 && __0.Code != 1 && __0.Code != 8)
                    {
                        var Code = __0.Code;
                        var Sender = "Null";
                        if (__0.Sender != null) Sender = Wrappers.Utils.PlayerManager.GetPlayerWithPlayerID(__0.Sender).GetVRCPlayer().DisplayName();
                        object value = Serialize.FromIL2CPPToManaged<object>(Parameters);
                        var Serialized = JsonConvert.SerializeObject(value, Formatting.Indented);

                        EvoConsole.Log($"\n-----------[OnEvent]-----------\n\nCode: {Code}\nSender: {Sender}\nSerialized data:\n{Serialized}");
                    }
                }

                if (__0.Code != 7 && __0.Code != 1 && __0.Code != 8 && __0.Code != 33 && __0.Code != 253 && __0.Code != 254)
                {
                    if (Settings.EventBlock)
                    {
                        if (LastReceivedEvent == __0.Code)
                        {
                            if (AmountOfSpam > 15)
                            {
                                if (!BlacklistedEvents.Contains(__0.Code)) BlacklistedEvents.Add(__0.Code);
                            }
                            else AmountOfSpam++;
                        }
                        else
                        {
                            if (BlacklistedEvents.Contains(LastReceivedEvent)) BlacklistedEvents.Remove(LastReceivedEvent);
                            LastReceivedEvent = __0.Code;
                            AmountOfSpam = 0;
                        }

                        if (BlacklistedEvents.Contains(__0.Code))
                        {
                            if (Settings.EventLog) EvoVrConsole.Log(EvoVrConsole.LogsType.Event, $"Blocked event code <color=#00ffe1>{__0.Code}</color> from <color=magenta>{Wrappers.Utils.PlayerManager.GetPlayerWithPlayerID(__0.Sender).GetVRCPlayer().DisplayName()}</color>");
                            return false;
                        }
                    }
                    if (Settings.EventLog) EvoVrConsole.Log(EvoVrConsole.LogsType.Event, $"<color=magenta>{Wrappers.Utils.PlayerManager.GetPlayerWithPlayerID(__0.Sender).GetVRCPlayer().DisplayName()}</color> sent event code <color=#00ffe1>{__0.Code}</color>");
                }

                switch (__0.Code)
                {
                    case 1:
                        if (Settings.MuteNonFriends)
                        {
                            if (!APIUser.CurrentUser.friendIDs.Contains(Wrappers.Utils.PlayerManager.GetPlayerWithPlayerID(__0.Sender).GetVRCPlayer().UserID())) return false;
                        }
                        break;

                    case 2:

                        if (Settings.Moderation)
                        {
                            object value = Serialize.FromIL2CPPToManaged<object>(__0.Parameters);
                            var Dict = JsonConvert.SerializeObject(value, Formatting.Indented);
                            if (Dict.Contains($"kicked"))
                            {
                                if (Settings.ModerationLogs)
                                {
                                    Log(LogsType.Moderation, $"<color=red>Kick</color> attempt from the owner of the room");
                                    Notifications.Notify($"<color=#ffea00>[Moderation]</color>\n<color=red>Kick</color> attempt from the owner");
                                }
                                return false;
                            }
                        }

                        break;

                    case 33:
                        var Object = Serialize.FromIL2CPPToManaged<object>(Parameters);
                        var String = JsonConvert.SerializeObject(Object);
                        var Event = JsonConvert.DeserializeObject<Dictionary<byte, object>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Dictionary<byte, object>>(String)[245]));

                        //Block
                        if (Event.Keys.Contains((byte)0) && Event.Keys.Contains((byte)1) && Event.Keys.Contains((byte)10) && Event.Keys.Contains((byte)11))
                        {
                            var User = Wrappers.Utils.PlayerManager.GetPlayerWithPlayerID(int.Parse(Event[1].ToString()));
                            var BlockState = bool.Parse(Event[10].ToString());
                            var MuteState = bool.Parse(Event[11].ToString());

                            if (BlockState && !MuteState)
                            {
                                ModeratedUsers.Add(User.UserID());
                                if (Settings.ModerationLogs)
                                {
                                    EvoVrConsole.Log(LogsType.Moderation, $"<color=magenta>{User.DisplayName()}</color> has you <color=red>Blocked</color> & <color=#00bbff>Unmuted</color>");
                                    Notifications.Notify($"<color=#ffea00>[Moderation]</color>\n<color=magenta>{User.DisplayName()}</color> <color=red>Blocked</color> & <color=#00bbff>Unmuted</color>");
                                }
                                return !Settings.Moderation;
                            }
                            else if (!BlockState && MuteState)
                            {
                                ModeratedUsers.Add(User.UserID());
                                if (Settings.ModerationLogs)
                                {
                                    EvoVrConsole.Log(LogsType.Moderation, $"<color=magenta>{User.DisplayName()}</color> has you <color=red>Unblocked</color> & <color=#00bbff>Muted</color>");
                                    Notifications.Notify($"<color=#ffea00>[Moderation]</color>\n<color=magenta>{User.DisplayName()}</color> <color=red>Unblocked</color> & <color=#00bbff>Muted</color>");
                                }
                                return !Settings.Moderation;
                            }
                            else if (BlockState && MuteState)
                            {
                                ModeratedUsers.Add(User.UserID());
                                if (Settings.ModerationLogs)
                                {
                                    EvoVrConsole.Log(LogsType.Moderation, $"<color=magenta>{User.DisplayName()}</color> has you <color=red>Blocked</color> & <color=#00bbff>Muted</color>");
                                    Notifications.Notify($"<color=#ffea00>[Moderation]</color>\n<color=magenta>{User.DisplayName()}</color> <color=red>Blocked</color> & <color=#00bbff>Muted</color>");
                                }
                                return !Settings.Moderation;
                            }
                            else if (!BlockState && !MuteState && ModeratedUsers.Contains(User.UserID()))
                            {
                                ModeratedUsers.Remove(User.UserID());
                                if (Settings.ModerationLogs)
                                {
                                    EvoVrConsole.Log(LogsType.Moderation, $"<color=magenta>{User.DisplayName()}</color> has you <color=red>Unblocked</color> & <color=#00bbff>Unmuted</color>");
                                    Notifications.Notify($"<color=#ffea00>[Moderation]</color>\n<color=magenta>{User.DisplayName()}</color> <color=red>Unblocked</color> & <color=#00bbff>Unmuted</color>");
                                }
                            }
                        }
                        break;
                }

            }
            catch { }
            return true;
        }

        private static bool OnEvent2(ref EventData __0)
        {
            try
            {
                if (__0.Code != 7 && __0.Code != 1 && __0.Code != 8 && __0.Code != 33 && __0.Code != 253 && __0.Code != 254)
                {
                    if (Settings.EventBlock)
                    {
                        if (LastReceivedEvent2 == __0.Code)
                        {
                            if (AmountOfSpam2 > 15)
                            {
                                if (!BlacklistedEvents.Contains(__0.Code)) BlacklistedEvents.Add(__0.Code);
                            }
                            else AmountOfSpam2++;
                        }
                        else
                        {
                            if (BlacklistedEvents.Contains(LastReceivedEvent2)) BlacklistedEvents.Remove(LastReceivedEvent2);
                            LastReceivedEvent2 = __0.Code;
                            AmountOfSpam2 = 0;
                        }

                        if (BlacklistedEvents.Contains(__0.Code)) return false;

                    }
                }

                switch (__0.Code)
                {
                    case 1:
                        if (Settings.MuteNonFriends)
                        {
                            if (!APIUser.CurrentUser.friendIDs.Contains(Wrappers.Utils.PlayerManager.GetPlayerWithPlayerID(__0.Sender).GetVRCPlayer().UserID())) return false;
                        }
                        break;

                    case 2:

                        if (Settings.Moderation)
                        {
                            object value = Serialize.FromIL2CPPToManaged<object>(__0.Parameters);
                            var Dict = JsonConvert.SerializeObject(value, Formatting.Indented);
                            if (Dict.Contains($"kicked")) return false;

                        }

                        break;

                    case 33:
                        var Object = Serialize.FromIL2CPPToManaged<object>(__0.Parameters);
                        var String = JsonConvert.SerializeObject(Object);
                        var Event = JsonConvert.DeserializeObject<Dictionary<byte, object>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Dictionary<byte, object>>(String)[245]));

                        //Block
                        if (Event.Keys.Contains((byte)0) && Event.Keys.Contains((byte)1) && Event.Keys.Contains((byte)10) && Event.Keys.Contains((byte)11))
                        {
                            var User = Wrappers.Utils.PlayerManager.GetPlayerWithPlayerID(int.Parse(Event[1].ToString()));
                            var BlockState = bool.Parse(Event[10].ToString());
                            var MuteState = bool.Parse(Event[11].ToString());

                            if (BlockState && !MuteState)
                            {
                                ModeratedUsers.Add(User.UserID());
                                return !Settings.Moderation;
                            }
                            else if (!BlockState && MuteState)
                            {
                                ModeratedUsers.Add(User.UserID());
                                return !Settings.Moderation;
                            }
                            else if (BlockState && MuteState)
                            {
                                ModeratedUsers.Add(User.UserID());
                                return !Settings.Moderation;
                            }
                            else if (!BlockState && !MuteState && ModeratedUsers.Contains(User.UserID()))
                            {
                                ModeratedUsers.Remove(User.UserID());
                            }
                        }
                        break;
                }

            }
            catch { }
            return true;
        }

        #endregion Patches
    }
}