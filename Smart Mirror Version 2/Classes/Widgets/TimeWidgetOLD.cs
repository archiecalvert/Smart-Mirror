using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;

namespace Smart_Mirror_Version_2.Classes.Widgets
{
    public class TimeWidgetOLD : Widget
    {
        static Texture2D Background = Main._content.Load<Texture2D>("Homescreen/Widgets/Calendar/banner");
        //static Texture2D Banner = Main._content.Load<Texture2D>("Homescreen/Widgets/Calendar/banner");

        static Vector2 Dimensions = new Vector2(282, 282);
        public TimeWidgetOLD(Vector2 Position) : base(
            new Rectangle((int)(Position.X), (int)(Position.Y), (int) Dimensions.X, (int) Dimensions.Y),
            Color.White,
            "Calendar",
            Background
            )
        {

        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            base.Draw();
            BitmapFont Font1 = Main.UI_MEDIUM;
            BitmapFont Font2 = Main.UI_LARGE;
            Main._spriteBatch.DrawString(Font2, DateTime.Now.ToString("dddd").ToUpper(), new Vector2(Bounds.X + Main.Homescreen.Offset.X + (Bounds.Width/2),Bounds.Y + 20 + Main.Homescreen.Offset.Y), Color.White, 0f,   new Vector2((int)Font2.MeasureString(DateTime.Now.ToString("dddd").ToUpper()).Width/2, 0), new Vector2(0.4f), SpriteEffects.None, 1f);
            Main._spriteBatch.DrawString(Font2, DateTime.Now.Day.ToString(), new Vector2(Bounds.X + Main.Homescreen.Offset.X + (Bounds.Width / 2), Bounds.Y + 67 + Main.Homescreen.Offset.Y), Color.Black, 0f, new Vector2((int)Font2.MeasureString(DateTime.Now.Day.ToString()).Width / 2, 0), new Vector2(1.3f), SpriteEffects.None, 1f);

        }

    }
}
