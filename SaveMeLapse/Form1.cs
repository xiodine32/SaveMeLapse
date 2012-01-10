using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
using System.IO;

namespace SaveMeLapse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveThread temp = new SaveThread(Convert.ToInt32(textBox1.Text));
            Thread lol = new Thread(new ThreadStart(temp.RunMainFunction));
            lol.Start();
            this.Close();
        }
    }

    public class SaveThread
    {
        private int pause, name = 0;
        private Bitmap bmpScreenshot;
        private Graphics gfxScreenshot;
        public SaveThread(int pause)
        {
            this.pause = pause;
            name = -1;
            bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format16bppArgb1555);
            gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            File.WriteAllText("saving.DELME","Delete me to stop the program");
        }
        public void RunMainFunction()
        {
            Thread.Sleep(5000);
            while (File.Exists("saving.DELME")) {
                SaveScreenshot();
                Thread.Sleep(pause);
            }
        }
        private void SaveScreenshot()
        {
            name++;
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            bmpScreenshot.Save("screen_"+name+".png", ImageFormat.Png);
        }
    }
}
