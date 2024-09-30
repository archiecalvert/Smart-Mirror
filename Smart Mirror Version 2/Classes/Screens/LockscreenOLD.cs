/*
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;

namespace Smart_Mirror_Version_2.Classes.Screens
{
    public class Lockscreen : ContentWindow
    {
        public Lockscreen()
        {
            Main.Lockscreen = this;
        }
        
        
        public override void Update()
        {
            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                UnloadWindow();
                Homescreen Homescreen = new Homescreen();
            }
        }
        public override void Draw()
        {
            Main._spriteBatch.DrawString(Main.UI_LARGE, DateTime.Now.ToString("dddd, MMMM ") + DateTime.Now.Day.ToString(), new Vector2(Main.WindowDimensions.X / 2 , 200), Color.White, 0f, new Vector2(Main.UI_LARGE.MeasureString(DateTime.Now.ToString("dddd, MMMM M")).Width / 2, 0), new Vector2(0.5f), SpriteEffects.None, 1f);
            Main._spriteBatch.DrawString(Main.UI_VERYLARGE, DateTime.Now.ToString("HH:mm"), new Vector2(Main.WindowDimensions.X/2, 220), Color.White, 0f, new Vector2(Main.UI_VERYLARGE.MeasureString(DateTime.Now.ToString("HH:mm")).Width / 2,0), new Vector2(1f), SpriteEffects.None, 1f);
            Main._spriteBatch.DrawString(Main.UI_MEDIUM, "Tap to unlock", new Vector2(Main.WindowDimensions.X/2, 1400), Color.White, 0f, new Vector2(Main.UI_MEDIUM.MeasureString("Tap to unlock").Width / 2, 0), new Vector2(0.75f), SpriteEffects.None, 1f);
        }
    }
}
*/