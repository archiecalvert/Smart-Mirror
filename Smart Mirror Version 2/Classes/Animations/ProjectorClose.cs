using Microsoft.Xna.Framework;
using Smart_Mirror_Version_2.Classes.Managers;
using Smart_Mirror_Version_2.Classes.SystemApps;
using System;

namespace Smart_Mirror_Version_2.Classes.Animations
{
    public class ProjectorClose : Animation
    {
        float PercentageOpenWidth = 100;
        float PercentageOpenHeight = 100;
        int maxPercentageHeight = 2;
        float Duration = 0.35f;
        Timer AnimationTimer;
        bool initialised = false;
        public int count = 0;
        public float maxHeight;
        public ProjectorClose(App ParentApp, Rectangle Bounds) : base(ParentApp)
        {
            IsRunning = false;
            AnimationTimer = new Timer(Duration);
            PercentageOpenWidth = 100;
            PercentageOpenHeight = 100;
            float percentage = (float)maxPercentageHeight / 100;
            maxHeight = (int)(Bounds.Height * percentage);
        }
        
        public void Animation(ref Rectangle Bounds, ActionManager.Event UnloadMethod)
        {
            if(!initialised) { AnimationTimer.Start(); initialised = true;}
            if (Bounds.Height > maxHeight)
            {
                PercentageOpenHeight -= easeOut((float)(AnimationTimer.PercentageRemaining()*2)/100) * (100) / ((Duration / 2) * Main.FPS);
                Bounds.Height = (int)(Main.AppBounds.Height * (PercentageOpenHeight / 100));
                if(Bounds.Height < maxHeight)
                {
                    Bounds.Height = (int)maxHeight;
                }
            }
            else
            {
                PercentageOpenWidth -= easeOut((float)(1-(AnimationTimer.PercentageRemaining()*2)/100)) * (100 / (Duration / 2 * Main.FPS));
                Bounds.Width = (int)(Main.AppBounds.Width * (PercentageOpenWidth / 100));
                if(Bounds.Width < 0) UnloadMethod();
            }
            
            
        }
        float easeOut(float input)
        {
            return (float)Math.Sin((input * Math.PI) / 2);
        }
    }
}
