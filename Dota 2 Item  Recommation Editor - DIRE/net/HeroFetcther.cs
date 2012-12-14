using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace dire.net
{
    class HeroFetcher
    {
        static string url = @"http://www.dota2.com/jsfeed/heropickerdata?l=english";
        public static Hero[] AllHeros;

        public static void getAllIcons(bool ForceUpdate = false)
        {

            try
            {
                string jsondata = string.Empty;
                if (File.Exists("cache/heros.json") && !ForceUpdate && !Program.UpdateJson)
                {
                    jsondata = File.ReadAllText("cache/heros.json");
                }
                else
                {
                    string tmp = ASCIIEncoding.UTF8.GetString(new WebClient().DownloadData(new Uri(url)));
                    File.WriteAllText("cache/heros.json" , tmp);
                    jsondata = tmp;
                }

                var j = JsonConvert.DeserializeObject<Dictionary<string, Hero>>( jsondata );
                List<Hero> h = new List<Hero>();
                foreach (var i in j)
                {
                    if (!(System.IO.File.Exists("cache/heros/" + i.Key + ".png")) || ForceUpdate)
                    {
                        IconFetcher.downloadHeroIcon(i.Key, "cache/heros/" + i.Key + ".png");
                    }

                    i.Value.DotaName = i.Key;
                    i.Value.Abilities = new Ability[15]; //maximum number of abilities to one Hero (Invoker)
                    h.Add(i.Value);

                }
                AllHeros = h.ToArray();
            }
            catch
            {
            }
        }

        public static void GenerateObjects()
        {
                string jsondata = string.Empty;
                jsondata = File.ReadAllText("cache/heros.json");


                var j = JsonConvert.DeserializeObject<Dictionary<string, Hero>>(jsondata);
                List<Hero> h = new List<Hero>();
                foreach (var i in j)
                {

                    i.Value.DotaName = i.Key;
                    i.Value.Abilities = new Ability[15]; //maximum number of abilities to one Hero (Invoker)
                    h.Add(i.Value);

                }
                AllHeros = h.ToArray();
        }
        
        public static string ResloveDotaNameToName(string DotaName)
        {
            foreach (Hero i in AllHeros)
            {
                if (i.DotaName == DotaName)
                {
                    return i.Name;
                }
            }
            throw new Exception("Hero " + DotaName + " does not exist.");
        }
        public static Hero ResloveDotaNameToHero(string DotaName)
        {
            foreach (Hero i in AllHeros)
            {
                if (i.DotaName == DotaName)
                {
                    return i;
                }
            }
            throw new Exception("Hero " + DotaName + " does not exist.");
        }
        
    }

    public class Hero
    {
        public string DotaName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public Ability[] Abilities { get; set; }

        public int ImageListIndex { get; set; }

        public string ImagePath { get { return "cache/heros/" + DotaName + ".png"; } }
        public string ImagePathLarge { get { return "cache/heros/" + DotaName + ".png"; } }
        public string ImagePathSmall { get { return "cache/heros/" + DotaName + "_sm.png"; } } 
    }
}
