using Smart_Mirror_Version_2.Classes.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Mirror_Version_2.Classes
{
    public abstract class Component
    {
        public Component()
        {
            SystemHandler.Components.Add(this);
        }
        public abstract void Update();
        public abstract void Draw();
    }
}
