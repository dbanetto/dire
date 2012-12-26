using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using dire.net;
using Newtonsoft.Json;

namespace dire.cache
{
    public static class cache
    {
        public static string status = "";
        public static bool updateinProgress = false;


        public static void UpdateCache()
        {
            updateinProgress = true;

            status = "Creating Folders";
            if (!Directory.Exists("cache"))
            {
                Directory.CreateDirectory("cache");
            }
            if (!Directory.Exists("cache/heros"))
            {
                Directory.CreateDirectory("cache/heros");
            }
            if (!Directory.Exists("cache/items"))
            {
                Directory.CreateDirectory("cache/items");
            }
            if (!Directory.Exists("cache/abilities"))
            {
                Directory.CreateDirectory("cache/abilities");
            }

            status = "Fetching Items icons and data";
            ItemFetcher.getAllIcons();
            status = "Fetching Hero icons and data";
            HeroFetcher.getAllIcons();
            status = "Fetching Abilities icons and data";
            AbilitiesFetcher.getAllIcons();

            updateinProgress = false;
        }

        public static void UpdateVerifyCache()
        {
            updateinProgress = true;
            
            status = "Creating Folders";
            if (!Directory.Exists("cache"))
            {
                Directory.CreateDirectory("cache");
            }
            if (!Directory.Exists("cache/heros"))
            {
                Directory.CreateDirectory("cache/heros");
                status = "Fetching Hero icons and data";
                HeroFetcher.getAllIcons();
            }
            if (!Directory.Exists("cache/items"))
            {
                Directory.CreateDirectory("cache/items");
                status = "Fetching Items icons and data";
                ItemFetcher.getAllIcons();
            }
            if (!Directory.Exists("cache/abilities"))
            {
                Directory.CreateDirectory("cache/abilities");
                status = "Fetching Abilities icons and data";
                AbilitiesFetcher.getAllIcons();
            }
            
            status = "Verifing Icons";
            VerifyCache();
            HeroFetcher.getAllIcons();
            ItemFetcher.getAllIcons();
            AbilitiesFetcher.getAllIcons();

            updateinProgress = false;
        }
        public static void UpdateCacheForced()
        {
            updateinProgress = true;

            status = "Creating Folders";
            if (!Directory.Exists("cache"))
            {
                Directory.CreateDirectory("cache");
            }
            if (!Directory.Exists("cache/heros"))
            {
                Directory.CreateDirectory("cache/heros");
            }
            if (!Directory.Exists("cache/items"))
            {
                Directory.CreateDirectory("cache/items");
            }
            if (!Directory.Exists("cache/abilities"))
            {
                Directory.CreateDirectory("cache/abilities");
            }
            
            status = "Fetching Items icons and data";
            ItemFetcher.getAllIcons(true);
            status = "Fetching Hero icons and data";
            HeroFetcher.getAllIcons(true);
            status = "Fetching Abilities icons and data";
            AbilitiesFetcher.getAllIcons(true);

            updateinProgress = false;
        }

        public static void VerifyCache()
        {
            string jsondata = File.ReadAllText("cache/abilities.json");

            var j = JsonConvert.DeserializeObject<Dictionary<string, Ability>>(jsondata);
            foreach (var i in j)
            {
                try
                {
                    Image.FromFile( "cache/abilities/" + i.Value.DotaName + ".png");
                }
                catch
                {
                    if (File.Exists("cache/abilities/" + i.Value.DotaName + ".png"))
                    {
                        File.Delete("cache/abilities/" + i.Value.DotaName + ".png");
                    }
                }
            }
            jsondata = File.ReadAllText("cache/heros.json");

            var jj = JsonConvert.DeserializeObject<Dictionary<string, Hero>>(jsondata);
            foreach (var i in jj)
            {
                try
                {
                    Image.FromFile(i.Value.ImagePathLarge);
                }
                catch
                {
                    if (File.Exists(i.Value.ImagePathLarge))
                    {
                        File.Delete(i.Value.ImagePathLarge);
                    }
                }
                try
                {
                    Image.FromFile(i.Value.ImagePathVert);
                }
                catch
                {
                    if (File.Exists(i.Value.ImagePathVert))
                    {
                        File.Delete(i.Value.ImagePathVert);
                    }
                }
            }
            
            jsondata = File.ReadAllText("cache/items.json");

            var v = JsonConvert.DeserializeObject<Dictionary<string, Item>>(jsondata);
            foreach (var i in v)
            {
                try
                {
                    Image.FromFile("cache/items/" + i.Value.DotaName + ".png");
                }
                catch
                {
                    if (File.Exists("cache/items/" + i.Value.DotaName + ".png"))
                    {
                        File.Delete("cache/items/" + i.Value.DotaName + ".png");
                    }
                }
            }
        }
    }
}
