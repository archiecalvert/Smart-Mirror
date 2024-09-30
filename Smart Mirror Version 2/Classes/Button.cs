using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Smart_Mirror_Version_2.Classes.Managers;
using System;

namespace Smart_Mirror_Version_2.Classes
{
    public class Button : Component
    {
        Timer ClickDelay;
        Rectangle Bounds;
        Rectangle MouseRect = new Rectangle(0,0,1,1);
        Texture2D Texture;
        Texture2D BlankRectangle = new Texture2D(Main._graphics.GraphicsDevice, 1, 1);
        Color ButtonColour;
        float LayerDepth = 0f;
        SpriteBatch SpriteBatch = Main._spriteBatch;
        Vector2 MouseOffset = Vector2.Zero;
        public Button(Rectangle Bounds, float LayerDepth, Color Colour)
        {
            this.Bounds = Bounds;
            ClickDelay = new Timer(0.25f);
            BlankRectangle.SetData(new[] { Color.White });
            ButtonColour = Colour;
            this.LayerDepth = LayerDepth;
        }
        public Button(Rectangle Bounds, float LayerDepth, Texture2D Texture)
        {
            this.Bounds = Bounds;
            ClickDelay = new Timer(0.25f);
            BlankRectangle.SetData(new[] { Color.White });
            this.Texture = Texture;
            this.LayerDepth = LayerDepth;
        }
        public override void Update()
        {
            MouseRect.Location =  Mouse.GetState().Position + MouseOffset.ToPoint();
            if(MouseRect.Intersects(Bounds) && !ClickDelay.isActive && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                ClickDelay.Start();
                Click();
            }
        }
        public void AddLocationOffset(Vector2 Offset)
        {
            MouseOffset = -Offset;
        }
        public void RemoveFromSystemHandler()
        {
            SystemHandler.Components.Remove(this);
        }
        public void SetSpriteBatch(ref SpriteBatch SpriteBatch)
        {
            this.SpriteBatch = SpriteBatch;
        }
        public override void Draw()
        {
            if(Texture!= null)
            {
                SpriteBatch.Draw(Texture, Bounds, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, LayerDepth);
            }
            else
            {
                SpriteBatch.Draw(BlankRectangle, Bounds, null, ButtonColour, 0f, Vector2.Zero, SpriteEffects.None, LayerDepth);
            }
        }
        public ActionManager.Event Click;
    }
}
