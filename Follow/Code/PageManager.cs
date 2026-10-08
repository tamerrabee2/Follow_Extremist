using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Follow.Code
{
    class PageManager
    {
        private readonly Main main;
        public  PageManager (Main main)
        {
            this.main = main;
        }
        public void LoadPage (UserControl pageUserControl)
        {
            // Load old page 
            var oldpag = main.panelContianer.Controls.OfType<UserControl>().FirstOrDefault();
            if (oldpag != null)
            {
                main.panelContianer.Controls.Remove(oldpag);// remove old page 
                oldpag.Dispose(); // تحرير الذاكرة من الصفحة 
            }

            // load new page 
            pageUserControl.Dock = DockStyle.Fill;
            main.panelContianer.Controls.Add(pageUserControl);
        }
        
    }
}
