using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using dire.net;
namespace dire.editor
{
    public struct item
    {
        public string  ItemName , Name;

        public item(string Name , string DotaName)
        {
            //This is the Dota 2 name such as item_tango
            this.ItemName = DotaName;
            this.Name = Name;

        }

        public string ResloveName(string DotaName)
        {
            foreach (Item i in ItemFetcher.AllItems)
            {
                if (i.DotaName == DotaName)
                {
                    return i.Name;
                }
            }
            throw new Exception("Hero " + DotaName + " not found");
        }
    }

   
}
