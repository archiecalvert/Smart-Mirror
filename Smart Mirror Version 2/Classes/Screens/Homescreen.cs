using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using Newtonsoft.Json;
using Smart_Mirror_Version_2.Classes.Managers;
using Smart_Mirror_Version_2.Classes.SystemApps;
using Smart_Mirror_Version_2.Classes.Widgets;
using System;
using System.Collections.Generic;
using System.IO;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
namespace Smart_Mirror_Version_2.Classes.Screens
{
    public class Homescreen : ContentWindow
    {
        public List<AppIcon> Icons;
        public Texture2D BlankTexture = new Texture2D(Main._graphics.GraphicsDevice,1,1);
        Texture2D HomescreenBackground = Texture2D.FromFile(Main._graphics.GraphicsDevice, "C:\\Users\\archi\\source\\repos\\Smart Mirror Version 2\\Smart Mirror Version 2\\Content\\Homescreen\\Backgrounds\\grey.png");
        Texture2D Dock = Main._content.Load<Texture2D>("Homescreen/dock");
        public Vector2 AppInitialPosition = new Vector2(70, 138);
        public int AppDimensions = 109;
        public bool HideLabels = false;
        WeatherLarge WeatherWidget;
        TimeWidget TimeWidget;
        public Vector2 Offset = new Vector2(0,50);
        public Homescreen()
        {
            Main.Homescreen = this;
            WeatherWidget = new WeatherLarge(new Vector2(1, 1), new Color(2, 99, 180));
            TimeWidget = new TimeWidget(new Vector2(1, 3));
            LoadApps();
        }
        public override void Update()
        {
            Offset = Main.Offset;
        }
        public override void Draw()
        {
            DrawBackground();
            Main._spriteBatch.DrawString(Main.UI_SMALL, DateTime.Now.ToString("HH:mm"), new Vector2(140 + Offset.X, 35 + Offset.Y), Color.White, 0f, new Vector2(Main.UI_SMALL.MeasureString(DateTime.Now.ToString("HH:mm")).Width / 2, 0), new Vector2(0.8f), SpriteEffects.None, 1f);
            Main._spriteBatch.Draw(Dock, new Rectangle(42 + (int)Main.Homescreen.Offset.X,1305 + (int)Main.Homescreen.Offset.Y, 647,170), new Color(0,0,0,75));

        }
        public void DrawBackground()
        {
            float BackScale = 1.5f;
            Main._spriteBatch.Draw(HomescreenBackground, new Rectangle(20 + (int)Offset.X, (int)(5 + Offset.Y), (int)(459 * BackScale), (int)(994 * BackScale)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layerDepth: 0f);
        }
        public void LoadApps()
        {
            List<IconSpace> apps = new List<IconSpace>();
            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "//Apps//layout.data");
            while (!sr.EndOfStream)
            {
                apps.Add(JsonConvert.DeserializeObject<IconSpace>(sr.ReadLine()));
            }
            sr.Close();
            sr.Dispose();
            foreach(IconSpace app in apps)
            {
                try
                {
                    StreamReader sr2 = new StreamReader(Directory.GetCurrentDirectory() + "//Apps//" + app.Name + "//appdata.json");
                    string data = sr2.ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(data);
                    AppIcon icon = new AppIcon(app.Name, Directory.GetCurrentDirectory() + "//Apps//" + app.Name + "//icon.png", new Vector2(app.X, app.Y));
                    switch ((string)json["AppType"])
                    {
                        case "Web Page":
                            icon.OnClickEvent = () => ActionManager.OpenWebPage((string)json["URL"]);
                            break;
                        case "Executable":
                            icon.OnClickEvent = () => ActionManager.OpenApplication((string)json["ExecutableName"]);
                            break;
                        case "Windows Store Application":
                            icon.OnClickEvent = () => ActionManager.OpenWindowsApp((string)json["AUMID"]);
                            break;
                        case "System App":
                            switch ((string)json["AppName"])
                            {
                                case "Settings":
                                    icon.OnClickEvent = () => new SettingsApp();
                                    break;
                                default:
                                    Console.WriteLine("Failed to load "+app.Name+" app. Skipping...");
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch { Console.WriteLine("Failed to load "+app.Name+" app. Skipping..."); }
            }
        }
        public void AddAppToHomescreen(string AppName, int Page, float X, float Y)
        {
            IconSpace icon = new IconSpace()
            {
                Name = AppName,
                Page = Page,
                X = X,
                Y = Y
            };
            if(!File.Exists(Directory.GetCurrentDirectory() + "//Apps//layout.data"))
            {
                File.Create(Directory.GetCurrentDirectory() + "//Apps//layout.data");
            }
            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "//Apps//layout.data");
            string data = sr.ReadToEnd();
            sr.Close();
            StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "//Apps//layout.data");
            sw.WriteLine(data + JsonConvert.SerializeObject(icon));
            sw.Close();
        }
        public class IconSpace
        {
            public string Name;
            public int Page;
            public float X;
            public float Y;
        }
    }
}
