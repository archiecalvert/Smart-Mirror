using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using Smart_Mirror_Version_2.Classes.Managers;
using System;
using static Smart_Mirror_Version_2.Classes.Managers.ActionManager;

namespace Smart_Mirror_Version_2.Classes
{
    public class AppIcon
    {
        string IconName;
        Texture2D Icon;
        BitmapFont Font = Main.UI_SMALL;
        Vector2 Location = new Vector2(70,138);
        int AppDimension = Main.Homescreen.AppDimensions;
        Timer ClickDelay;
        string launchData = "";
        public bool isClicked = false;
        
        bool HideLabel = Main.Homescreen.HideLabels;
        public AppIcon(string name, string directory, Vector2 gridLocation)
        {
            SystemHandler.AppIcons.Add(this);
            IconName = name;
            try
            {
                Icon = Texture2D.FromFile(Main._graphics.GraphicsDevice, "C:\\Users\\archi\\source\\repos\\Smart Mirror Version 2\\Smart Mirror Version 2\\Content\\Apps\\Icons\\" + directory);
            }
            catch
            {
                Icon = Texture2D.FromFile(Main._graphics.GraphicsDevice, directory);
            }
            Location += new Vector2((gridLocation.X - 1) * (AppDimension + 50), (gridLocation.Y - 1) * (AppDimension + Main.APPSPACING));
            ClickDelay = new Timer(0f);
            if (gridLocation.Y == 7) { Location.Y += 160; HideLabel = true; };
        }
        public void Update()
        {
            Rectangle MouseRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            if(!ClickDelay.isActive && MouseRectangle.Intersects(new Rectangle((int)(Location.X + Main.Offset.X), (int)(Location.Y + Main.Offset.Y), AppDimension, AppDimension)) && Mouse.GetState().LeftButton==ButtonState.Pressed)
            {
                ClickDelay = new Timer(2f);
                ClickDelay.Start();
                try
                {
                    OnClickEvent();
                }
                catch { Console.WriteLine("Failed to open app."); }
            }
        }
        public void Draw()
        {
            if(!HideLabel) Main._spriteBatch.DrawString(Font, IconName, new Vector2(Location.X + Main.Homescreen.Offset.X + AppDimension/2, Location.Y + AppDimension + 5 + Main.Homescreen.Offset.Y), Color.White, 0f, new Vector2((float)(Font.MeasureString(IconName).Width/2), 0), new Vector2(0.57f), SpriteEffects.None, 0.99f);
        }
        public void DrawAppIcon()
        {
            Main._spriteBatch.Draw(Icon, new Rectangle((int)(Location.X + Main.Homescreen.Offset.X), (int)(Location.Y + Main.Homescreen.Offset.Y), AppDimension, AppDimension), Color.White);
        }
        public AppClick OnClickEvent;
    }
}
