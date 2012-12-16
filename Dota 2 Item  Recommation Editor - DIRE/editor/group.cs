using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dire.net;

namespace dire.editor
{
    public struct group
    {
        public List<Item> items;

        public string GroupTitle;

        public group(List<Item> Items, string GroupTitle)
        {
            this.items = Items;
            this.GroupTitle = GroupTitle;
        }

    }
}
