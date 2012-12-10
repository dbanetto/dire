using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dire.editor
{
    public struct group
    {
        public List<item> items;

        public string GroupTitle;

        public group(List<item> Items, string GroupTitle)
        {
            this.items = Items;
            this.GroupTitle = GroupTitle;
        }

    }
}
