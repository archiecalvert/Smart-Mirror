using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using Smart_Mirror_Version_2.Classes.VideoCapture;
using Smart_Mirror_Version_2.Classes.Managers;
using System.Threading;
namespace Smart_Mirror_Version_2.Classes.Screens
{
    public class Lockscreen : ContentWindow
    {
        public Texture2D BlankTexture = new Texture2D(Main._graphics.GraphicsDevice, 1, 1);
        Texture2D LockscreenBackground = Texture2D.FromFile(Main._graphics.GraphicsDevice, "C:\\Users\\archi\\source\\repos\\Smart Mirror Version 2\\Smart Mirror Version 2\\Content\\Homescreen\\Backgrounds\\grey.png");
        public Vector2 Offset = new Vector2(0, 50);
        
        public Lockscreen()
        {
            Main.Lockscreen = this;
        }
        public override void Update()
        {
            Offset = Main.Offset;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                UnloadWindow();
                Homescreen Homescreen = new Homescreen();
            }
        }
        public override void Draw()
        {
            
            Main._spriteBatch.End();
            Main._spriteBatch.Begin(SpriteSortMode.BackToFront, null, effect: SystemHandler.BackgroundMask);
            DrawBackground();
            Main._spriteBatch.End();
            Main.StartNewSpriteBatch();
            //Main._spriteBatch.DrawString(Main.UI_SMALL, DateTime.Now.ToString("HH:mm"), new Vector2(105 + Offset.X, 25 + Offset.Y), Color.White, 0f, new Vector2(Main.UI_SMALL.MeasureString(DateTime.Now.ToString("HH:mm")).Width / 2, 0), new Vector2(0.65f), SpriteEffects.None, 1f);
            Main._spriteBatch.DrawString(Main.UI_SMALL, DateTime.Now.ToString("HH:mm"), new Vector2(140 + Offset.X, 35 + Offset.Y), Color.White, 0f, new Vector2(Main.UI_SMALL.MeasureString(DateTime.Now.ToString("HH:mm")).Width / 2, 0), new Vector2(0.8f), SpriteEffects.None, 1f);

            Main._spriteBatch.DrawString(Main.UI_LARGE, DateTime.Now.ToString("dddd, MMMM ") + DateTime.Now.Day.ToString(), new Vector2(Offset.X + (459*1.5f)/2,Offset.Y + 130), Color.White, 0f, new Vector2(Main.UI_LARGE.MeasureString(DateTime.Now.ToString("dddd, MMMM M")).Width / 2, 0), new Vector2(0.35f), SpriteEffects.None, 1f);
            Main._spriteBatch.DrawString(Main.UI_VERYLARGE, DateTime.Now.ToString("HH:mm"), new Vector2(Offset.X + (459 * 1.5f) / 2, 200), Color.White, 0f, new Vector2(Main.UI_VERYLARGE.MeasureString(DateTime.Now.ToString("HH:mm")).Width / 2, 0), new Vector2(0.8f), SpriteEffects.None, 1f);
            Main._spriteBatch.DrawString(Main.UI_MEDIUM, "Tap to unlock", new Vector2(Offset.X + (459 * 1.5f) / 2, 1400), Color.White, 0f, new Vector2(Main.UI_MEDIUM.MeasureString("Tap to unlock").Width / 2, 0), new Vector2(0.65f), SpriteEffects.None, 1f);
        }
        public void DrawBackground()
        {
            float BackScale = 1.5f;
            Main._spriteBatch.Draw(LockscreenBackground, new Rectangle(20 + (int)Offset.X, (int)(5 + Offset.Y), (int)(459 * BackScale), (int)(994 * BackScale)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layerDepth: 0f);
        }
    }
}
