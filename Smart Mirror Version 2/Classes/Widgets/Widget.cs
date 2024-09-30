using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using Smart_Mirror_Version_2.Classes.Managers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Smart_Mirror_Version_2.Classes.Widgets
{
    public abstract class Widget
    {
        Vector2 ScreenPosition = Main.Homescreen.AppInitialPosition;
        Color Colour;
        string Title;
        BitmapFont Font = Main.UI_SMALL;
        public Rectangle Bounds;
        Texture2D Background;
        public Widget(Rectangle Bounds, Color BackgroundColour, string WidgetName, Texture2D Widget)
        {
            SystemHandler.Widgets.Add(this);
            Vector2 Position = new Vector2(Bounds.X, Bounds.Y);
            ScreenPosition += new Vector2((Position.X - 1) * (Main.Homescreen.AppDimensions + 50), (Position.Y - 1) * (Main.Homescreen.AppDimensions + Main.APPSPACING));
            Colour = BackgroundColour;
            Title = WidgetName;
            this.Bounds = Bounds;
            this.Background = Widget;
            this.Bounds.X = (int)ScreenPosition.X;
            this.Bounds.Y = (int)(ScreenPosition.Y);
        }
        public abstract void Update();
        public virtual void Draw()
        {
            DrawBackground(Background);
            if (!Main.Homescreen.HideLabels) Main._spriteBatch.DrawString(Font, Title, new Vector2(ScreenPosition.X + Main.Homescreen.Offset.X + (Bounds.Width / 2), ScreenPosition.Y + (Bounds.Height + 5 + Main.Homescreen.Offset.Y)), Color.White, 0f, new Vector2((float)(Font.MeasureString(Title).Width / 2), 0), new Vector2(0.57f), SpriteEffects.None, 1f);

        }
        public void DrawBackground(Texture2D Background)
        {
            Main._spriteBatch.Draw(Background, new Rectangle((int)(ScreenPosition.X + Main.Homescreen.Offset.X), (int)(ScreenPosition.Y + Main.Homescreen.Offset.Y), (int)Bounds.Width, (int)Bounds.Height), null, Colour, 0f, Vector2.Zero, SpriteEffects.None, 0.99f);
        }

    }
}
