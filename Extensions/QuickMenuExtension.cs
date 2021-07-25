using Transmtn.DTO.Notifications;

namespace Evolve.Wrappers
{
    internal static class QuickMenuExtension
    {
        public static void SelectPlayer(this QuickMenu Instance, VRC.Player Instance2)
        {
            Instance.Method_Public_Void_Player_PDM_0(Instance2);
        }

        public static Notification Notification(this QuickMenu Instance)
        {
            return Instance.field_Private_Notification_0;
        }
    }
}