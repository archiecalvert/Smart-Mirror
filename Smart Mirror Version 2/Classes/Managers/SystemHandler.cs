using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Smart_Mirror_Version_2.Classes.Screens;
using Smart_Mirror_Version_2.Classes.Widgets;
using System.Collections.Generic;

namespace Smart_Mirror_Version_2.Classes.Managers
{
    public static class SystemHandler
    {
        public static List<ContentWindow> Windows = new List<ContentWindow>();
        public static List<Timer> Timers = new List<Timer>();
        public static List<AppIcon> AppIcons = new List<AppIcon>();
        public static List<Component> Components = new List<Component>();
        public static List<Widget> Widgets = new List<Widget>();
        static Effect IconMask;
        public static Effect BackgroundMask;
        public static void Initialise()
        {
            IconMask = Main._content.Load<Effect>("IconMask");
            IconMask.Parameters["Mask"].SetValue(Main._content.Load<Texture2D>("Apps/mask"));
            BackgroundMask = Main._content.Load<Effect>("Homescreen/BackgroundMask");
            BackgroundMask.Parameters["Mask"].SetValue(Main._content.Load<Texture2D>("Homescreen/homescreen-mask"));
        }
        
        public static void Update()
        {
            for(int i = Windows.Count - 1; i >= 0; i--)
            {
                Windows[i].Update();
            }
            for(int i =  Timers.Count - 1;i >= 0; i--)
            {
                Timers[i].Update();
            }
            for (int i = AppIcons.Count - 1; i >= 0; i--)
            {
                AppIcons[i].Update();
            }
            for (int i = Components.Count - 1; i >= 0; i--)
            {
                Components[i].Update();
            }
            for (int i = Widgets.Count - 1; i >= 0; i--)
            {
                Widgets[i].Update();
            }
        }
        public static void Draw()
        {
            for (int i = Windows.Count - 1; i >= 0; i--)
            {
                Windows[i].Draw();
            }
            for (int i = AppIcons.Count - 1; i >= 0; i--)
            {
                AppIcons[i].Draw();
            }
            for (int i = Components.Count - 1; i >= 0; i--)
            {
                Components[i].Draw();
            }
            for (int i = Widgets.Count - 1; i >= 0; i--)
            {
                Widgets[i].Draw();
            }
            Main._spriteBatch.End();
            Main._spriteBatch.Begin(SpriteSortMode.BackToFront, null, effect: IconMask);
            for (int i =  AppIcons.Count - 1; i>=0; i--)
            {
                AppIcons[i].DrawAppIcon();
            }
            
            Main._spriteBatch.End();
            Main.StartNewSpriteBatch();
            
        }
    }
}
