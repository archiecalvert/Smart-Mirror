using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AForge.Video;
using AForge.Video.DirectShow;


namespace Smart_Mirror_Version_2.Classes.VideoCapture
{

    public partial class CaptureCard : Form
    {
        VideoCaptureDevice device;
        FilterInfoCollection filterInfoCollection;
        public CaptureCard()
        {
            InitializeComponent();
            CaptureWindow.Top = 0;
            CaptureWindow.Left = 0;
            this.Width = 1920;
            this.Height = 1080;
            CaptureWindow.Width = this.Width;
            CaptureWindow.Height = this.Height;
        }

        private void CaptureCard_Load(object sender, EventArgs e)
        {
            FilterInfo webcam = null;
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filter in filterInfoCollection)
            {
                if (filter.Name == "Camera (NVIDIA Broadcast)") webcam = filter;
            }
            if (webcam != null)
            {
                this.device = new VideoCaptureDevice(webcam.MonikerString);
                device.NewFrame += new NewFrameEventHandler(NewWebCamFrame);
                device.Start();
            }
        }
        void NewWebCamFrame(object sender, NewFrameEventArgs args)
        {
            if (CaptureWindow.Image != null && CaptureWindow.IsAccessible) CaptureWindow.Image.Dispose();
            CaptureWindow.Image = (Bitmap)args.Frame.Clone();
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

        private void CaptureCard_Activated(object sender, EventArgs e)
        {
            
        }
    }
}

