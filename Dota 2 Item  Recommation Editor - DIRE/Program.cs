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
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            splashscreen = new Splash();
            Thread t = new Thread(new ThreadStart(cache.cache.UpdateCache ));
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
            Application.Run(new SkillBuild());
#else
            Application.Run(new HeroPicker());
#endif
        }
        static Splash splashscreen;
        
    }
}
