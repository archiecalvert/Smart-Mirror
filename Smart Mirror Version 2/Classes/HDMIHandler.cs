using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Xna.Framework.Graphics;

namespace Smart_Mirror_Version_2.Classes
{
    public class HDMIHandler : Component
    {
        VideoCaptureDevice device;
        FilterInfoCollection filterInfoCollection;
        Texture2D Frame;
        public HDMIHandler()
        {
            //Frame.SetData(new[] { Color.White });
            FilterInfo webcam = null;
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo filter in filterInfoCollection)
            {
                if (filter.Name == "USB3.0 Capture") webcam = filter;
            }
            this.device = new VideoCaptureDevice(webcam.MonikerString);
            device.NewFrame += new NewFrameEventHandler(NewWebCamFrame);
            device.Start();
        }
        public void NewWebCamFrame(object sender, NewFrameEventArgs args)
        {
            Bitmap img = args.Frame;
            Texture2D tx = null;
            using (MemoryStream s = new MemoryStream())
            {
                img.Save(s, System.Drawing.Imaging.ImageFormat.Png);
                s.Seek(0, SeekOrigin.Begin); //must do this, or error is thrown in next line
                tx = Texture2D.FromFile(Main._graphics.GraphicsDevice, s.ToString());
            }
            Frame = tx;
        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            if(Frame!=null)Main._spriteBatch.Draw(Frame, destinationRectangle: new Microsoft.Xna.Framework.Rectangle(50,50,1920,1080), Microsoft.Xna.Framework.Color.White);
        }
    }
}
