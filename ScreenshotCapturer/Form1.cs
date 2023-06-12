using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;

namespace ScreenshotCapturer
{
    public partial class Form1 : Form
    {
        private static Bitmap bmpScreenshot;
        private static Graphics gfxScreenshot;

        string pathNoName;
        int count = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (saveScreenshot.ShowDialog() == DialogResult.OK)
            {
                pathNoName = saveScreenshot.FileName;
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(count > 5)
            {
                timer1.Stop();
                return;
            }

            // Hide the form so that it does not appear in the screenshot
            this.Hide();
            Thread.Sleep(400);
            //Set the bitmap object to the size of the screen
            bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            // Create a graphics object from the bitmap
            gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            // Take the screenshot from the upper left corner to the right bottom corner
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            // Path to date
            saveScreenshot.FileName = pathNoName.Insert(pathNoName.Length - 4, DateTime.Now.ToString("HH_mm_ss"));
            // Save the screenshot to the specified path that the user has chosen
            bmpScreenshot.Save(saveScreenshot.FileName, ImageFormat.Png);
            count++;
            // Show the form again
            this.Show();
    }
    }
}