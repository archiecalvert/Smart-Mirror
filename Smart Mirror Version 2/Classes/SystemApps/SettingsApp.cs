using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using Smart_Mirror_Version_2.Classes.Animations;
using System.Collections.Generic;
using System.Diagnostics;
namespace Smart_Mirror_Version_2.Classes.SystemApps
{
    public class SettingsApp : App
    {
        Color PrimaryColour = new Color(78,77,77);
        Color SecondaryColour = new Color(60, 59, 58);
        int SecondaryPanelWidth = Main.AppBounds.Width - 400;
        List<Button> Buttons = new List<Button>();
        Button GeneralButton;
        
        public SettingsApp()
        {
            ProjectorClose projectorClose = new ProjectorClose(this, Bounds);
            base.SetUnloadAnimation(() => projectorClose.Animation(ref Bounds, () => UnloadWindow()));
            GeneralButton = new Button(new Rectangle(0, 120, 400, 50), 1f, Main._content.Load<Texture2D>("Apps/Settings/SubMenu Buttons/General"));
            Buttons.Add(GeneralButton);
            foreach(var button in Buttons)
            {
                button.RemoveFromSystemHandler();
                button.SetSpriteBatch(ref SpriteBatch);
                button.AddLocationOffset(Main.AppBounds.Location.ToVector2());
            }
            GeneralButton.Click = () => UnloadAnimation();
        }
        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                UnloadAnimation();
            }
            foreach(var button in Buttons)
            {
                button.Update();
            }
        }
        public override void Draw()
        {
            base.Draw();
            BeginRender();
            DrawAppWindow(PrimaryColour);
            DrawSecondaryPanel(SecondaryPanelWidth, SecondaryColour);
            SpriteBatch.DrawString(Main.UI_MEDIUM, "Settings", new Vector2(30, 25), Color.White, 0f, Vector2.Zero, new Vector2(0.75f), SpriteEffects.None, 1f);
            foreach(var button in Buttons)
            {
                button.Draw();
            }
            EndRender();
        }
    }
}
