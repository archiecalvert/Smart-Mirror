using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace Smart_Mirror_Version_2.Classes.Widgets
{
    public class WeatherLarge : Widget
    {
        //----------------WEATHER SETTINGS------------------
        string PlaceName = "Manchester";
        public HttpClient client;
        string API_KEY = "3d374192618c4ba3aaa80527221907";
        List<WeatherDataItem> WeatherData;
        public bool CallInProgress = false;
        public bool HasWeatherData = false;
        public float UpdateFrequency = 30f; //Minutes
        
        //--------------------------------------------------
        static Texture2D Background = Main._content.Load<Texture2D>("Homescreen/Widgets/large");
        public DateTime NextUpdate = DateTime.Now;
        bool UpdateFlag = false;
        static Vector2 Dimensions = new Vector2(586, 282);
        public WeatherLarge(Vector2 Position, Color BackgroundColour) : base(
            new Rectangle((int)(Position.X), (int)(Position.Y), (int)Dimensions.X, (int)Dimensions.Y),
            BackgroundColour,
            "Weather",
            Background
            )
        {
            //CONSTRUCTOR CODE GOES BELOW
            client = new HttpClient();
            Thread thread = new Thread(() => WeatherData = CallAPI(API_KEY, PlaceName));
            thread.Start();
            //--------------------------
        }
        public override void Update()
        {
            
            
        }
        public List<WeatherDataItem> CallAPI(string key, string location)
        {  
            try
            {
                UpdateFlag = false;
                NextUpdate = DateTime.Now.AddMinutes(UpdateFrequency);
                CallInProgress = true;
                if (Main.SkipAPICalls) goto SKIPCALL;
                Console.WriteLine("Calling API");
                string url = "http://api.weatherapi.com/v1/forecast.json?key=" + key + "&q=" + location + "&days=2&aqi=no&alerts=no";
                var send = client.GetAsync(url);
                send.Wait();
                if (send.IsCompleted)
                {
                    var response = send.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync();
                        data.Wait();
                        JObject jsonData = (JObject)JsonConvert.DeserializeObject(data.Result);
                        List<WeatherDataItem> dataArray = new List<WeatherDataItem>();
                        for (int i = 0; i < 6; i++)
                        {
                            int Hour = DateTime.Now.Hour + i + 1;
                            if (Hour >= 24) Hour -= 24;
                            WeatherDataItem currentItem = null;
                            if (DateTime.Now.Hour > Hour)
                            {
                                currentItem = new WeatherDataItem
                                {
                                    Time = DateTime.Now.AddHours(i + 1).Hour,
                                    Icon = WeatherIcon(Hour, (string)jsonData.SelectToken("forecast").SelectToken("forecastday").SelectToken("[1]").SelectToken("hour").SelectToken("[" + (int)Hour + "]").SelectToken("condition").SelectToken("text")),
                                    Temperature = (float)jsonData.SelectToken("forecast").SelectToken("forecastday").SelectToken("[1]").SelectToken("hour").SelectToken("[" + (int)Hour + "]").SelectToken("temp_c"),
                                    ChanceOfRain = (float)jsonData.SelectToken("forecast").SelectToken("forecastday").SelectToken("[1]").SelectToken("hour").SelectToken("[" + (int)Hour + "]").SelectToken("chance_of_rain")
                                };
                            }
                            else
                            {
                                currentItem = new WeatherDataItem
                                {
                                    Time = DateTime.Now.AddHours(i + 1).Hour,
                                    Icon = WeatherIcon(Hour, (string)jsonData.SelectToken("forecast").SelectToken("forecastday").SelectToken("[0]").SelectToken("hour").SelectToken("[" + (int)Hour + "]").SelectToken("condition").SelectToken("text")),
                                    Temperature = (float)jsonData.SelectToken("forecast").SelectToken("forecastday").SelectToken("[0]").SelectToken("hour").SelectToken("[" + (int)Hour + "]").SelectToken("temp_c"),
                                    ChanceOfRain = (float)jsonData.SelectToken("forecast").SelectToken("forecastday").SelectToken("[0]").SelectToken("hour").SelectToken("[" + (int)Hour + "]").SelectToken("chance_of_rain")
                                };
                            }
                            dataArray.Add(currentItem);
                        }
                        HasWeatherData = true;
                        CallInProgress = false;
                        UpdateFlag = false;
                        Console.WriteLine("API Call Successful. Time Completed: " + DateTime.Now.ToString());
                        Console.WriteLine("Next API Call: " + NextUpdate);
                        Console.WriteLine();
                        return dataArray;
                    }
                    else
                    {
                        Console.WriteLine(response.ReasonPhrase);
                    }
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                
            }
            SKIPCALL:
            CallInProgress = false;
            if (WeatherData == null)
            {
                HasWeatherData = false;
                return null;
            }
            return WeatherData;
        }
        public record WeatherDataItem()
        {
            public int Time { get; set; }
            public Texture2D Icon { get; set; }
            public float Temperature { get; set; }
            public float ChanceOfRain { get; set; }


        }
        public Texture2D WeatherIcon(int Hour, string Conditions)
        {
            switch(Conditions.ToUpper())
            {
                case string a when Conditions.ToUpper().Contains("CLOUD"):
                    return Main._content.Load<Texture2D>("Homescreen/Widgets/BBC Weather/Clouds");
                case string a when Conditions.ToUpper().Contains("RAIN"):
                    return Main._content.Load<Texture2D>("Homescreen/Widgets/BBC Weather/Rain");
                case string a when Conditions.ToUpper().Contains("CLEAR") || Conditions.ToUpper().Contains("SUNNY"):
                    if(Hour < 19 && Hour > 7)
                    {
                        return Main._content.Load<Texture2D>("Homescreen/Widgets/BBC Weather/Clear");
                    }
                    else
                    {
                        return Main._content.Load<Texture2D>("Homescreen/Widgets/BBC Weather/Night");

                    }
                case string a when Conditions.ToUpper().Contains("OVERCAST"):
                    return Main._content.Load<Texture2D>("Homescreen/Widgets/BBC Weather/Overcast");
                case string a when Conditions.ToUpper().Contains("DRIZZLE"):
                    return Main._content.Load<Texture2D>("Homescreen/Widgets/BBC Weather/Rain");
                default:
                    return null;
            }
        }

        public override void Draw()
        {
            if (DateTime.Compare(DateTime.Now, NextUpdate) >= 0) UpdateFlag = true;
            if (UpdateFlag)
            {
                UpdateFlag = false;
                Thread thread = new Thread(() => WeatherData = CallAPI(API_KEY, PlaceName));
                thread.Start();
            }
            base.Draw();
            Main._spriteBatch.DrawString(Main.UI_SMALL, PlaceName, new Vector2(Bounds.X + Main.Homescreen.Offset.X + 32, Bounds.Y + 25 + Main.Homescreen.Offset.Y), Color.White, 0f, Vector2.Zero, new Vector2(0.7f), SpriteEffects.None, 1f);
            if(WeatherData != null)
            {
                for(int i = 0; i<WeatherData.Count; i++)
                {
                    int IconSize = 50;
                    string TimeText = "";
                    if (WeatherData[i].Time < 10) { TimeText = "0" + WeatherData[i].Time + ":00"; }
                    else { TimeText = WeatherData[i].Time + ":00"; }
                    Main._spriteBatch.DrawString(Main.UI_SMALL, TimeText, new Vector2((float)(Bounds.X + Main.Homescreen.Offset.X + (i * 90.6)) + 60, (float)(Bounds.Y + Main.Homescreen.Offset.Y + 63)), Color.White, 0f, new Vector2(Main.UI_SMALL.MeasureString(TimeText).Width / 2, 0), new Vector2(0.55f), SpriteEffects.None, 1f);
                    Main._spriteBatch.DrawString(Main.UI_SMALL, ((int)(WeatherData[i].Temperature)).ToString() + "°", new Vector2((float)(Bounds.X + Main.Homescreen.Offset.X + (i * 90.6)) + 60, (float)(Bounds.Y + Main.Homescreen.Offset.Y + 63 + 100)), Color.White, 0f, new Vector2(Main.UI_SMALL.MeasureString(((int)(WeatherData[i].Temperature)).ToString() + "°").Width / 2, 0), new Vector2(0.55f), SpriteEffects.None, 1f);
                    Main._spriteBatch.DrawString(Main.UI_SMALL, ((int)(WeatherData[i].ChanceOfRain)).ToString() + "%", new Vector2((float)(Bounds.X + Main.Homescreen.Offset.X + (i * 90.6)) + 60, (float)(Bounds.Y + Main.Homescreen.Offset.Y + 63 + 150)), Color.White, 0f, new Vector2(Main.UI_SMALL.MeasureString(((int)(WeatherData[i].ChanceOfRain) + "%").ToString()).Width / 2, 0), new Vector2(0.55f), SpriteEffects.None, 1f);
                    if (WeatherData[i].Icon != null) Main._spriteBatch.Draw(WeatherData[i].Icon, new Rectangle((int)(Bounds.X + Main.Homescreen.Offset.X + (i * 90.6)) + 60, (int)(Bounds.Y + Main.Homescreen.Offset.Y + 113), IconSize, IconSize), null, Color.White, 0f, new Vector2(IconSize, IconSize/2), SpriteEffects.None, 1f);
                    Main._spriteBatch.Draw(Main._content.Load<Texture2D>("Homescreen/Widgets/BBC Weather/chanceOfRainIcon"), new Rectangle((int)(Bounds.X + Main.Homescreen.Offset.X + (i * 90.6)) + 60, (int)(Bounds.Y + Main.Homescreen.Offset.Y + 203), 15, 15), null, Color.White, 0f, new Vector2(15/2, 15 / 2), SpriteEffects.None, 1f);

                }
            }
        }
    }
}