using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Smart_Mirror_Version_2.Classes.Managers;
using Smart_Mirror_Version_2.Classes.Screens;
using System;

namespace Smart_Mirror_Version_2.Classes.SystemApps
{
    public abstract class App : ContentWindow
    {
        //RENDER VARIABLES
        public SpriteBatch SpriteBatch;
        RenderTarget2D RenderTarget;
        public Rectangle Bounds;
        //--------------------
        Texture2D Corner;
        int CornerRadius = 32;
        protected Texture2D BlankTexture = new Texture2D(Main._graphics.GraphicsDevice, 1, 1);
        //ANIMATION VARIABLES
        public static bool isClosing = false;
        public App()
        {
            UnloadAnimation = UnloadWindow;
            isClosing = false;
            RenderTarget = new RenderTarget2D(Main._graphics.GraphicsDevice, Main.AppBounds.Width, Main.AppBounds.Height);
            Corner = Main._content.Load<Texture2D>("Apps/corner-mask");
            BlankTexture.SetData(new[] { Color.White });
            SpriteBatch = new SpriteBatch(Main._graphics.GraphicsDevice);
            Bounds = Main.AppBounds;
            Bounds.X += Bounds.Width/2;
            Bounds.Y += Bounds.Height/2;
        }
        public void BeginRender()
        {
            Main._spriteBatch.End();
            Main._graphics.GraphicsDevice.SetRenderTarget(RenderTarget);
            SpriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, blendState: null, samplerState: SamplerState.LinearClamp);
        }
        public ActionManager.Event UnloadAnimation;
        public override void Draw()
        {
            base.Draw();
            if (isClosing) UnloadAnimation();
        }
        public void SetUnloadAnimation(ActionManager.Event Animation)
        {
            UnloadAnimation = () => { Animation(); isClosing = true;};
        }
        public void EndRender()
        {
            SpriteBatch.End();
            Main._graphics.GraphicsDevice.SetRenderTarget(null);
            Main.StartNewSpriteBatch();
            Main._spriteBatch.Draw(RenderTarget, Bounds, null, Color.White, 0f, new Vector2(Bounds.Width/2, Bounds.Height/2), SpriteEffects.None, 1f);
        }
        public void DrawAppWindow(Color Colour)
        {
            float layer = 0.9f;
            //TOP LEFT
            SpriteBatch.Draw(Corner, new Rectangle(0, 0, CornerRadius, CornerRadius), null, Colour, 0f, Vector2.Zero, SpriteEffects.None, layer);
            SpriteBatch.Draw(BlankTexture, new Rectangle(CornerRadius, 0, Main.AppBounds.Width - (CornerRadius * 2), CornerRadius), null, Colour, 0f, Vector2.Zero, SpriteEffects.None, layer);
            //TOP RIGHT
            SpriteBatch.Draw(Corner, new Rectangle(Main.AppBounds.Width, 0, CornerRadius, CornerRadius), null, Colour, (float)(Math.PI / 2), Vector2.Zero, SpriteEffects.None, layer);
            SpriteBatch.Draw(BlankTexture, new Rectangle(0, CornerRadius, Main.AppBounds.Width, Main.AppBounds.Height - (CornerRadius * 2)), null, Colour, 0f, Vector2.Zero, SpriteEffects.None, layer);
            SpriteBatch.Draw(BlankTexture, new Rectangle(CornerRadius, Main.AppBounds.Height - CornerRadius, Main.AppBounds.Width - (CornerRadius * 2), CornerRadius), null, Colour, 0f, Vector2.Zero, SpriteEffects.None, layer);
            //BOTTOM RIGHT
            SpriteBatch.Draw(Corner, new Rectangle(Main.AppBounds.Width, Main.AppBounds.Height, CornerRadius, CornerRadius), null, Colour, (float)(Math.PI), Vector2.Zero, SpriteEffects.None, layer);
            //BOTTOM LEFT
            SpriteBatch.Draw(Corner, new Rectangle(0, Main.AppBounds.Height, CornerRadius, CornerRadius), null, Colour, (float)(Math.PI * 3 / 2), Vector2.Zero, SpriteEffects.None, layer);
        }
        public void DrawSecondaryPanel(int SecondaryPanelWidth, Color Colour)
        {
            SpriteBatch.Draw(Corner, new Rectangle(Main.AppBounds.Width, 0, CornerRadius, CornerRadius), null, Colour, (float)(Math.PI / 2), Vector2.Zero, SpriteEffects.None, 0.91f);
            SpriteBatch.Draw(Corner, new Rectangle(Main.AppBounds.Width, Main.AppBounds.Height, CornerRadius, CornerRadius), null, Colour, (float)(Math.PI), Vector2.Zero, SpriteEffects.None, 0.91f);
            SpriteBatch.Draw(BlankTexture, new Rectangle(Main.AppBounds.Width - SecondaryPanelWidth, 0, SecondaryPanelWidth - CornerRadius, Main.AppBounds.Height), null, Colour, 0f, Vector2.Zero, SpriteEffects.None, 0.91f);
            SpriteBatch.Draw(BlankTexture, new Rectangle(Main.AppBounds.Width - CornerRadius, CornerRadius, CornerRadius, Main.AppBounds.Height - (2 * CornerRadius)), null, Colour, 0f, Vector2.Zero, SpriteEffects.None, 0.91f);
            SpriteBatch.Draw(BlankTexture, new Rectangle(Main.AppBounds.Width - SecondaryPanelWidth, 0, 2, Main.AppBounds.Height), null, Color.Black, 0f, Vector2.Zero, SpriteEffects.None, 0.92f);
        }
    }
}
