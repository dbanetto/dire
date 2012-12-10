using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

namespace dire.net
{
    public static class IconFetcher
    {
        static string items_base =     @"http://media.steampowered.com/apps/dota2/images/items/";
        static string hero_base =      @"http://media.steampowered.com/apps/dota2/images/heroes/";
        static string abilities_base = @"http://media.steampowered.com/apps/dota2/images/abilities/";


        static string abilities_end =       "_hp2.png";
        static string abilities_end_small = "_hp1.png";
        
        static string hero_end =       "_hphover.png";
        static string hero_end_small = "_sb.png";
        
        static string item_end = "_lg.png";

        public static WebClient webClient = new WebClient();

        public static void downloadItemIcon(string item, string path)
        {
            webClient.DownloadFile(new Uri(items_base + item + item_end) , path);
        }

        public static void downloadHeroIcon(string hero, string path)
        {
            webClient.DownloadFile(new Uri(hero_base + hero + hero_end), path);
            webClient.DownloadFile(new Uri(hero_base + hero + hero_end_small), path.Split('.')[0] + "_sm." + path.Split('.')[1]); //adds a _sm between file name and .png
        }

        public static void downloadAbilityIcon(string ability, string path)
        {
            webClient.DownloadFile(new Uri(abilities_base + ability + abilities_end), path);
            //webClient.DownloadFile(new Uri(abilities_base + ability + abilities_end_small), path.Split('.')[0] + "_sm." + path.Split('.')[1]); //adds a _sm between file name and .png
        }

    }
}
