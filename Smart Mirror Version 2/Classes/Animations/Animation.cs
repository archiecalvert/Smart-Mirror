using Smart_Mirror_Version_2.Classes.SystemApps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Mirror_Version_2.Classes.Animations
{
    public abstract class Animation
    {
        public App ParentApp;
        public Animation(App ParentApp)
        {
            this.ParentApp = ParentApp;
        }
        public bool IsRunning { get; set; }
    }
}
