using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace dire
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static bool UpdateJson = false;
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                frmSettings.LoadSettings("cache/settings.json",null);
            }
            catch
            {
                frmSettings.Settings = new Setting();
            }

            bool forceUpdate = false;
            bool UpdateVerifyCache = false;
            foreach (string str in args)
            {
                if (str.ToLower() == "--force" || str.ToLower() == "-f")
                {
                    forceUpdate = true;
                }
                if (str.ToLower() == "--update" || str.ToLower() == "-u")
                {
                    UpdateJson = true;
                }
                if (str.ToLower() == "--verify" || str.ToLower() == "-v")
                {
                    UpdateVerifyCache = true;
                }
            }

            splashscreen = new frmSplash();

            Thread t;
            if (forceUpdate)
            {
                t = new Thread(new ThreadStart(cache.cache.UpdateCacheForced));
            }
            else {
                if (UpdateVerifyCache)
                {
                    t = new Thread(new ThreadStart(cache.cache.UpdateVerifyCache));
                }
                else
                {
                     t = new Thread(new ThreadStart(cache.cache.UpdateCache));
                }
            }
            //Thread ui = new Thread(new ThreadStart( splashscreen.Show ));
            //ui.Start();
            splashscreen.Show();
            t.Start();
            do
            {
                splashscreen.Update();
                //splashscreen.ChangeStatusText(cache.cache.status);
                Thread.Sleep(50);
            } while (cache.cache.updateinProgress);
            t.Join();
            splashscreen.Close();
#if DEBUG
            Application.Run(new frmSkillBuild());
#else
            Application.Run(new frmHeroPicker());
#endif
        }
        static frmSplash splashscreen;
        
    }
}
