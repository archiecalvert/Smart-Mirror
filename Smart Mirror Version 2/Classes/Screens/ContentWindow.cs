using Smart_Mirror_Version_2.Classes.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Mirror_Version_2.Classes.Screens
{
    public abstract class ContentWindow
    {
        public ContentWindow()
        {
            SystemHandler.Windows.Add(this);
        }
        public abstract void Update();
        public virtual void Draw() { }
        public virtual void UnloadWindow()
        {
            SystemHandler.Windows.Remove(this);
        }
    }
}
