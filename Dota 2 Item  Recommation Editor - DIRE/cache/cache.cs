using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using dire.net;

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

    }
}
