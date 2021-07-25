using UnityEngine;

namespace Evolve.Wrappers
{
    internal static class ConversionManager
    {
        public static ColorManager ColorManager = new ColorManager();

        public static string ConvertRGBtoHEX(float r, float g, float b)
        {
            byte b2 = (byte) (r * 255f);
            byte b3 = (byte) (g * 255f);
            byte b4 = (byte) (b * 255f);
            return b2.ToString("X2") + b3.ToString("X2") + b4.ToString("X2");
        }

        public static string ConvertRGBtoHEX(Color color)
        {
            byte b = (byte) (color.r * 255f);
            byte b2 = (byte) (color.g * 255f);
            byte b3 = (byte) (color.b * 255f);
            return b.ToString("X2") + b2.ToString("X2") + b3.ToString("X2");
        }

        public static string isFriend = ConversionManager.ConvertRGBtoHEX(ColorManager.FriendR, ColorManager.FriendG, ColorManager.FriendB);

        public static string Legend = ConversionManager.ConvertRGBtoHEX(ColorManager.LegendR, ColorManager.LegendG, ColorManager.LegendB);

        public static string Veteran = ConversionManager.ConvertRGBtoHEX(ColorManager.VeteranR, ColorManager.VeteranG, ColorManager.VeteranB);

        public static string Trusted = ConversionManager.ConvertRGBtoHEX(ColorManager.TrustedR, ColorManager.TrustedG, ColorManager.TrustedB);

        public static string Known = ConversionManager.ConvertRGBtoHEX(ColorManager.KnownR, ColorManager.KnownG, ColorManager.KnownB);

        public static string User = ConversionManager.ConvertRGBtoHEX(ColorManager.UserR, ColorManager.UserG, ColorManager.UserB);

        public static string NewUser = ConversionManager.ConvertRGBtoHEX(ColorManager.NewUserR, ColorManager.NewUserG, ColorManager.NewUserB);

        public static string Visitors = ConversionManager.ConvertRGBtoHEX(ColorManager.VisitorsR, ColorManager.VisitorsG, ColorManager.VisitorsB);

        public static string Nuisance = ConversionManager.ConvertRGBtoHEX(ColorManager.NuisanceR, ColorManager.NuisanceG, ColorManager.NuisanceB);
    }
}