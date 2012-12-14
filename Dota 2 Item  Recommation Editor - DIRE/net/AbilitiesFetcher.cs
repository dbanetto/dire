using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace dire.net
{
    class AbilitiesFetcher
    {
        static string url = @"http://www.dota2.com/jsfeed/heropediadata?feeds=abilitydata&l=english";

        //To do: get images (large and small) for each Hero saved in /cache/heros/{Hero}_abilities/{name}.png
        //Add ability to Hero

        public static void getAllIcons(bool ForceUpdate = false)
        {

            try
            {
                string jsondata = string.Empty;
                if (File.Exists("cache/abilities.json") && !ForceUpdate)
                {
                    jsondata = File.ReadAllText("cache/abilities.json");
                }
                else
                {
                    string tmp = ASCIIEncoding.UTF8.GetString(new WebClient().DownloadData(new Uri(url)));

                    tmp = tmp.Remove(0, 15); //remove {"abilitydata":
                    tmp = tmp.Remove(tmp.Length - 1); //remove last } for itemdata

                    File.WriteAllText("cache/abilities.json", tmp);
                    jsondata = tmp;
                }

                var j = JsonConvert.DeserializeObject<Dictionary<string, Ability>>(jsondata);
                List<Ability> h = new List<Ability>();
                foreach (var i in j)
                {
                    if (!(System.IO.File.Exists("cache/abilities/" + i.Key + ".png")) || ForceUpdate)
                    {
                        IconFetcher.downloadAbilityIcon(i.Key, "cache/abilities/" + i.Key + ".png");
                    }

                    i.Value.DotaName = i.Key;
                    h.Add(i.Value);

                }

                //Sort each ability to its Hero
                foreach (Ability a in h)
                {
                    foreach (Hero hero in HeroFetcher.AllHeros)
                    {
                        //Check Hero in ability against Hero name in list

                        if (a.HeroName.Replace("_", " ").ToLower() == hero.Name.ToLower() || a.HeroName.ToLower() == hero.DotaName.ToLower() || a.DotaName.Contains(hero.DotaName))
                        {
                            a.DotaHeroName = hero.DotaName;
                            for (int i = 0; i < hero.Abilities.Length; i++)
                            {
                                if (hero.Abilities[i] == null)
                                {
                                    //Put ability in free slot
                                    hero.Abilities[i] = a;
                                    break;
                                }
                            }
                        }
                    }
                }

            }
            catch
            {
            }
        }

    }




    public class Ability
    {
        [JsonProperty(PropertyName = "dname")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "desc")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "dmg")]
        public string Damage { get; set; }

        [JsonProperty(PropertyName = "attrib")]
        public string Attributes { get; set; }

        [JsonProperty(PropertyName = "hurl")]
        public string HeroName { get; set; }

        public string DotaHeroName { get; set; }

        //key
        public string DotaName { get; set; }

    }
}