using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Evolve.AvatarList
{
    internal class Config
    {
        public static string MainFolder = "Evolve/Avatars List";
        public static string AvatarLists = MainFolder + "/avatars.json";


        public string CustomName = "Evolve Engine";
        public bool Public = true;
        public bool Custom = true;

        public static List<CompatibleAvatarObject> DAvatars = new List<CompatibleAvatarObject> {
            new CompatibleAvatarObject()
            {
                Name = "Robot",
                AvatarID = "avtr_c38a1615-5bf5-42b4-84eb-a8b6c37cbd11",
                ThumbnailImageUrl = "https://d348imysud55la.cloudfront.net/Avatar-Robot-Image-2017415f1_3_a.file_0e8c4e32-7444-44ea-ade4-313c010d4bae.1.png",
            }
        };

        public static List<CompatibleAvatarObject> DataBase = new List<CompatibleAvatarObject>();

        public static void UpdateAvatars()
        {
            File.WriteAllText(AvatarLists, JsonConvert.SerializeObject(DAvatars, Formatting.Indented));
        }

        public static void LoadConfig()
        {
            if (!Directory.Exists(MainFolder))
            {
                Directory.CreateDirectory(MainFolder);
            }

            if (!File.Exists(AvatarLists))
            {
                File.WriteAllText(AvatarLists, JsonConvert.SerializeObject(DAvatars, Formatting.Indented));
            }
            else
            {
                DAvatars = JsonConvert.DeserializeObject<List<CompatibleAvatarObject>>(File.ReadAllText(AvatarLists));
            }
        }
    }
}