using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AForge.Video;
using AForge.Video.DirectShow;


namespace CaptureCard
{
    
    public partial class Video : Form
    {
        VideoCaptureDevice device;
        FilterInfoCollection filterInfoCollection;
        public Video()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FilterInfo webcam = null;
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filter in filterInfoCollection)
            {
                if (filter.Name == "USB3.0 Capture") webcam = filter;
            }
            this.device = new VideoCaptureDevice(webcam.MonikerString);
            device.NewFrame += new NewFrameEventHandler(NewWebCamFrame);
            device.Start();
        }
        void NewWebCamFrame(object sender, NewFrameEventArgs args)
        {
            CaptureWindow.Image = args.Frame;
        }
        public void SetPosition(int x, int y)
        {
            this.Top = y;
            this.Left = x;
        }
        public void SetSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            CaptureWindow.Size = new Size(width, height);
            CaptureWindow.Top = 0;
            CaptureWindow.Left = 0;
        }
    }
}
