using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
namespace dire.editor
{
    public class build
    {
        string hero = string.Empty, author = string.Empty, title = string.Empty;
        int tabIndex = 0;
        List<group> items = new List<group>();

        public build(string Hero, string Author, string Title, List<group> Items)
        {
            //What hero is it for
            hero = Hero;
            //Who made it
            author = Author;
            //Build Name
            title = Title;
            //Build items and groups
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
            fs.WriteLine(TabIndex() + "\"author\"		\"" + author + "\""); //author
            fs.WriteLine(TabIndex() + "\"hero\"  		\"" + hero + "\""); //hero
            fs.WriteLine(TabIndex() + "\"Title\"			\"" + title + "\"\n"); //title
            fs.WriteLine(TabIndex() + "\"Items\"");
                write_start_regoin(fs); //items regoin
                foreach (group g in items) //items
                {
                    write_group(fs, g);
                }
                write_end_regoin(fs); //finished items
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
            string Lhero = "", Lauthor = "", Ltitle = "";
            List<group> Litems = new List<group>();
            group tempGroup = new group();
            bool isItems = false; //is reading items
            

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
                        tempGroup.items = new List<item>(); //init items
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
                    Lhero = s.Split('\"')[s.Split('\"').Length - 2];
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
