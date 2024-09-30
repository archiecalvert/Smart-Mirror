namespace Smart_Mirror_Version_2.Classes.VideoCapture
{
    partial class CaptureCard
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
            CaptureWindow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)CaptureWindow).BeginInit();
            SuspendLayout();
            // 
            // CaptureWindow
            // 
            CaptureWindow.Location = new System.Drawing.Point(-3, -2);
            CaptureWindow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            CaptureWindow.Name = "CaptureWindow";
            CaptureWindow.Size = new System.Drawing.Size(1486, 793);
            CaptureWindow.TabIndex = 0;
            CaptureWindow.TabStop = false;
            // 
            // CaptureCard
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1484, 791);
            Controls.Add(CaptureWindow);
            Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            Name = "CaptureCard";
            Text = "CaptureCard";
            Activated += CaptureCard_Activated;
            Load += CaptureCard_Load;
            ((System.ComponentModel.ISupportInitialize)CaptureWindow).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox CaptureWindow;
    }
}