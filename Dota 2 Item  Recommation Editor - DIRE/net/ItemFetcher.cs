using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Newtonsoft.Json;
using System.IO;
namespace dire.net
{
    class ItemFetcher
    {
        static string url = @"http://www.dota2.com/jsfeed/itemdata?&l=english"; //Url for json info
        public static Item[] AllItems;


         public static void getAllIcons (bool ForceUpdate = false) {

            try
            {
                string jsondata = string.Empty;
                if (File.Exists("cache/items.json") && !ForceUpdate && !Program.UpdateJson)
                {
                    jsondata = File.ReadAllText("cache/items.json");
                }
                else
                {
                    string tmp = ASCIIEncoding.UTF8.GetString(new WebClient().DownloadData(new Uri(url)));
                    tmp = tmp.Remove(0,12); //remove {"itemdata":
                    tmp = tmp.Remove(tmp.Length - 1); //remove last } for itemdata
                    File.WriteAllText("cache/items.json", tmp);
                    jsondata = tmp;
                }


                List<Item> items = new List<Item>();
                var j = JsonConvert.DeserializeObject<Dictionary<string,Item>>(jsondata);
                foreach (var i in j) {
                    if (!System.IO.File.Exists("cache/items/" + i.Key + ".png") || ForceUpdate)
                    {
                        IconFetcher.downloadItemIcon(i.Key,"cache/items/" + i.Key + ".png");
                    }
                    i.Value.DotaName = i.Key;
                    items.Add(i.Value);
                }
                AllItems = items.ToArray();
            }
            catch
            {
            }
    }

        public static string ResloveDotaNameToName (string DotaName) {
            foreach (Item i in AllItems)
            {
                if (i.DotaName == DotaName) {
                    return i.Name;
                }
            }
            throw new Exception("Item " + DotaName + " does not exist.");
        }

    }

    public class Item
    {
        public string DotaName { get; set; }

        public int ImageListIndex { get; set; }
        
        [JsonProperty(PropertyName = "dname")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "img")]
        public string img { get; set; }

        [JsonProperty(PropertyName = "cost")]
        public int Cost { get; set; }

        [JsonProperty(PropertyName = "components")]
        public string[] Components { get; set; }

        [JsonProperty(PropertyName = "attrib")]
        public string Attributes { get; set; }

        [JsonProperty(PropertyName = "desc")]
        public string Discription { get; set; }
        
    }

}
