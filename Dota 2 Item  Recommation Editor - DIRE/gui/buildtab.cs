using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

using dire.net;
using dire.editor;
using System.Windows;


namespace dire.gui
{
    class BuildTab : TabPage
    {
        group group;
        int cost;

        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public group Group
        {
            get { return group; }
            set { group = value; }
        }

        private ListView ItemsList;


        public BuildTab(string GroupName, group Group)
            : base(GroupName)
        {
            this.group = Group;

            init();
        }

        public BuildTab( group Group)
            : base(Group.GroupTitle)
        {
            init();
            this.group = Group;
            for (int i = 0; i < group.items.Count; i++  )
            {
                this.AddItem(group.items[i]);
            }

            
        }

        
        public BuildTab(string GroupName)
            : base(GroupName)
        {
            
            this.group = new group();
            this.group.items = new List<Item>();
            this.group.GroupTitle = GroupName;

            init();
        }

        private void init()
        {
            cost = 0;
            main.Build.UpdateCurrentCost();
            this.Controls.Add(ItemsList);
            this.Location = new System.Drawing.Point(4, 22);
            this.Size = new System.Drawing.Size(346, 366);
            this.TabIndex = 0;
            this.UseVisualStyleBackColor = true;

            ItemsList = new ListView();
            ItemsList.Dock = DockStyle.Fill;
            ItemsList.Location = new System.Drawing.Point(4, 22);
            
            

            ItemsList.Items.Clear();
            ItemsList.GridLines = true;
            ItemList.ItemSelectionChanged += ItemList_ItemSelectionChanged;
            ItemList.MouseClick += ItemList_MouseClick;
            ItemsList.MouseDoubleClick += new MouseEventHandler(ItemsList_MouseDoubleClick);

            this.Controls.Add(ItemsList);
        }

        void ItemList_MouseClick(object sender, MouseEventArgs e)
        {
            if (ItemsList.SelectedItems.Count == 1)
            {
                //Change Infobox
                main.Build.SetInfoBox(ItemFetcher.AllItems[ItemsList.SelectedItems[0].ImageIndex]);
            }
        }

        void ItemList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //Change Infobox
            main.Build.SetInfoBox(ItemFetcher.AllItems[e.Item.ImageIndex]);
        }

        void ItemsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ItemsList.SelectedItems.Count > 0)
            {
                foreach (ListViewItem i in ItemsList.SelectedItems)
                {
                    this.group.items.Remove(ItemFetcher.AllItems[i.ImageIndex]);
                    ItemsList.Items.Remove(i);
                    cost -= ItemFetcher.AllItems[i.ImageIndex].Cost;
                    main.Build.UpdateCurrentCost();
                }
            }
        }

        public void AddItem(ListViewItem i)
        {
            this.group.items.Add(ItemFetcher.AllItems[i.ImageIndex]);
            this.ItemsList.Items.Add(i);
            cost += ItemFetcher.AllItems[i.ImageIndex].Cost;

            main.Build.UpdateCurrentCost();

        }

        public void AddItem(Item i)
        {
            //this.group.items.Add(i);
            this.ItemsList.Items.Add( new ListViewItem(i.Name, i.ImageListIndex));
            cost += i.Cost;

            main.Build.UpdateCurrentCost();

        }

        public ListView ItemList { get { return this.ItemsList; } }

    }
}
