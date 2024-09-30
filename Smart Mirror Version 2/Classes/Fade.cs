using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Smart_Mirror_Version_2.Classes.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Mirror_Version_2.Classes
{
    public class Fade
    {
        /*
        Texture2D BlankTexture = new Texture2D(Main._graphics.GraphicsDevice, 1, 1);
        Timer FadeTimer;
        public Fade(float FadeDuration)
        {
            BlankTexture.SetData(new[] { Color.Black });
            FadeTimer = new Timer(FadeDuration);
        }
        public override void Update()
        {
            if (!FadeTimer.isActive)
            {
                SystemHandler.Components.Remove(this);
            }
        }
        public override void Draw()
        {
            Main._spriteBatch.Draw(BlankTexture, new Rectangle(0,0,2560,1600), null, new Color(0,0,0,(FadeTimer.duration/FadeTimer.MaxTime) * 255), 0f,Vector2.Zero,SpriteEffects.None, layerDepth:1f);
        }
        */
    }
}
