using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using Smart_Mirror_Version_2.Classes.Managers;
using Smart_Mirror_Version_2.Classes.Screens;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Smart_Mirror_Version_2
{
    public class Main : Game
    {

        //                      DISPLAY VARIABLES
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static Vector2 WindowDimensions = new Vector2(2560, 1600);
        public static Homescreen Homescreen;
        public static Lockscreen Lockscreen;
        public static Vector2 Offset = new Vector2(0, 50);
        public static Rectangle AppBounds = new Rectangle(750, 50 ,1700, 1500);
        
        //                      SYSTEM VARIABLES
        public static ContentManager _content;
        public static int FPSCap = 60;
        public static int APPSPACING = 64; //70
        public static bool SkipAPICalls = false;
        public static float FPS = 0;
        //                            FONTS
        public static BitmapFont UI_SMALL;
        public static BitmapFont UI_MEDIUM;
        public static BitmapFont UI_LARGE;
        public static BitmapFont UI_VERYLARGE;
        public static SpriteFont UI_SpriteFont;
        

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _content = Content;
            //Window.IsBorderless = true;
            Window.AllowAltF4 = true;
        }

        protected override void Initialize()
        {
            //FONTS
            UI_SMALL = Content.Load<BitmapFont>("Fonts/UI_SMALLBOLD");
            UI_MEDIUM = Content.Load<BitmapFont>("Fonts/UI_MEDIUMBOLD");
            UI_LARGE = Content.Load<BitmapFont>("Fonts/UI_LARGEBOLD");
            UI_VERYLARGE = Content.Load<BitmapFont>("Fonts/UI_VERYLARGE");
            UI_SpriteFont = Content.Load<SpriteFont>("Fonts/UI_SpriteFont");
            //
            Lockscreen Lockscreen = new Lockscreen();
            SystemHandler.Initialise();
            //GRAPHICS SETTINGS
            IsFixedTimeStep = true;
            _graphics.SynchronizeWithVerticalRetrace = false;
            TargetElapsedTime = TimeSpan.FromSeconds(1f/ FPSCap);
            _graphics.PreferredBackBufferWidth = (int)WindowDimensions.X - 1;
            _graphics.PreferredBackBufferHeight = (int)WindowDimensions.Y + 49;
            Window.Position = new Point(0, 0);
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }
        protected override void Update(GameTime gameTime)
        {
            FPS = (float)(1 / gameTime.ElapsedGameTime.TotalSeconds);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!this.IsActive) return;
            // TODO: Add your update logic here
            SystemHandler.Update();
           
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            StartNewSpriteBatch();
            SystemHandler.Draw();
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public static void StartNewSpriteBatch()
        {
            _spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, blendState: null, samplerState: SamplerState.LinearClamp);

        }
    }
}