namespace CaptureCard
{
    partial class Video
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CaptureWindow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CaptureWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // CaptureWindow
            // 
            this.CaptureWindow.Location = new System.Drawing.Point(25, 27);
            this.CaptureWindow.Name = "CaptureWindow";
            this.CaptureWindow.Size = new System.Drawing.Size(703, 393);
            this.CaptureWindow.TabIndex = 0;
            this.CaptureWindow.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 467);
            this.Controls.Add(this.CaptureWindow);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CaptureWindow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox CaptureWindow;
    }
}

