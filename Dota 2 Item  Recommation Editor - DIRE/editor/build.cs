using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using dire.net;
namespace dire.editor
{
    public class build
    {
        Hero hero;
        string author = string.Empty, title = string.Empty;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public Hero Hero
        {
            get { return hero; }
            set { hero = value; }
        }
        int tabIndex = 0;
        List<group> items = new List<group>();

        public List<group> Items
        {
            get { return items; }
            set { items = value; }
        }

        public build(Hero Hero, string Author, string Title, List<group> Items)
        {
            //What Hero is it for
            hero = Hero;
            //Who made it
            author = Author;
            //Build Name
            title = Title;
            //Build Items and groups
            this.items = Items;
        }

        #region Write Build
        public void WriteBuild(string file, bool backup = true)
        {
            if (File.Exists(file) && backup)
            {
                //Backup file
                File.Copy(file, file + "_"  + DateTime.UtcNow.ToString("H-mm-dd-MM-yy") + ".backup");
            }

            StreamWriter fs = new StreamWriter (new FileStream(file , FileMode.Create));

            fs.AutoFlush = true;

            fs.WriteLine("\"itembuilds/default_generic.txt\""); //Keeps the file to standard
            write_start_regoin(fs);
            fs.WriteLine(TabIndex() + "\"author\"		\"" + author + "\""); //Author
            fs.WriteLine(TabIndex() + "\"hero\"  		\"" + "npc_dota_hero_" + hero.DotaName + "\""); //Hero
            fs.WriteLine(TabIndex() + "\"Title\"			\"" + title + "\"\n"); //Title
            fs.WriteLine(TabIndex() + "\"Items\"");
                write_start_regoin(fs); //Items regoin
                foreach (group g in items) //Items
                {
                    write_group(fs, g);
                }
                write_end_regoin(fs); //finished Items
            write_end_regoin(fs); //finished build
            fs.Flush();
            fs.Close();
        }

        private void write_start_regoin(StreamWriter fs ){
            fs.WriteLine(TabIndex() + "{");
            tabIndex++;
        }

        private void write_end_regoin(StreamWriter fs)
        {
            tabIndex--;
            fs.WriteLine(TabIndex() + "}");
        }

        private string TabIndex()
        {
            string o = "";
            for (int i = 0; i < tabIndex; i++)
            {
                o = o + "\t";
            }
            return o;
        }

        private void write_group(StreamWriter fs, group g)
        {
            fs.WriteLine(TabIndex() + "\"" + g.GroupTitle + "\"");
            write_start_regoin(fs);

            foreach (item i in g.items) {
                fs.WriteLine(TabIndex() + "\"item\" \t\"item_" + i.ItemName + "\"");
            }

            write_end_regoin(fs);

        }
        #endregion
        #region Read Build
        public static build LoadBuild(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Hero Lhero = null;
            string Lauthor = "", Ltitle = "";
            List<group> Litems = new List<group>();
            group tempGroup = new group();
            bool isItems = false; //is reading Items
            

            foreach (string s in lines)
            {
                if (isItems)
                {
                    if (s.TrimStart().StartsWith("\"item"))
                    {
                        //Item found
                        tempGroup.items.Add(new item("", s.Split('\"')[s.Split('\"').Length - 2]));

                    }
                    else if (s.TrimStart().StartsWith("\"") && s.EndsWith("\""))
                    {

                        if (tempGroup.GroupTitle != null)
                        {
                            Litems.Add(new group( tempGroup.items, tempGroup.GroupTitle));
                        }
                        //Name of item Group
                        tempGroup = new group(); //init Group
                        tempGroup.items = new List<item>(); //init Items
                        tempGroup.GroupTitle = s.TrimStart().Split('\"')[1];

                    }
                    else
                    {
                        continue;
                    }
                }
                
                
                if (s.TrimStart().StartsWith("\"author"))
                {
                    Lauthor = s.Split('\"')[s.Split('\"').Length - 2];
                    continue;
                }

                if (s.TrimStart().StartsWith("\"Title"))
                {
                    string t = s.Split('\"')[s.Split('\"').Length - 2];
                    Ltitle = s.Split('\"')[s.Split('\"').Length - 2];
                    continue;
                }

                if (s.TrimStart().StartsWith("\"hero"))
                {
                    Lhero = HeroFetcher.ResloveDotaNameToHero( s.Split('\"')[s.Split('\"').Length - 2].Remove(0, 14) );
                    continue;
                }

                if (s.TrimStart().StartsWith("\"Items"))
                {
                    isItems = true;
                    continue;
                }

                if (s == "}") //EOF
                {
                    break;
                }
            }

            return new build(Lhero, Lauthor, Ltitle, Litems);

        }
        #endregion
    }
}
