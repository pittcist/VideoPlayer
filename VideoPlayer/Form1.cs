using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyVideo;
using System.IO;

// Modified April 16, 2016

namespace VideoPlayer
{
    public partial class Form1 : Form
    {
        Media newMedia = new Media();
        OpenFileDialog file;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file = new OpenFileDialog();

            file.Filter = "Video Files | *.avi; *.mpg; *.wmv; *.mkv; *.mpeg; *.mp4; *.rm; *.wav; *.mp3";
            file.InitialDirectory = Directory.GetCurrentDirectory();
            file.ShowDialog();

            if (file.FileName != "")
            {
                newMedia.Open(file.FileName, pictureBox1);
                playButton.Enabled = true;
                stopButton.Enabled = true;
                pauseButton.Enabled = true;
                fsButton.Enabled = true;
                volumnTrackBar.Value = 5;
                progressBar1.Maximum = newMedia.Duration();
                progressTrackBar.Maximum = newMedia.Duration();
                newMedia.Stop();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            playButton.Enabled = false;
            stopButton.Enabled = false;
            pauseButton.Enabled = false;
            fsButton.Enabled = false;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            newMedia.Stop();
            timer1.Enabled = false;
        }

        private void fsButton_Click(object sender, EventArgs e)
        {
            newMedia.FullScreen();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            newMedia.Close();
            newMedia.Open(file.FileName, pictureBox1);
            newMedia.Play();
            timer1.Enabled = true;
            newMedia.Volume = volumnTrackBar.Value*100;
            newMedia.Position = progressTrackBar.Value;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            newMedia.Pause = !newMedia.Pause;
            timer1.Enabled = !timer1.Enabled;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = newMedia.Position;
            progressTrackBar.Value = newMedia.Position;

          //  progressLabel.Text = newMedia.Position.ToString();
            progressLabel.Text = (newMedia.Position / Convert.ToDecimal(newMedia.Duration())).ToString("P0");
        }

        private void volumnTrackBar_Scroll(object sender, EventArgs e)
        {
            newMedia.Volume = volumnTrackBar.Value * 100;
        }

        private void progressTrackBar_Scroll(object sender, EventArgs e)
        {
            newMedia.Position = progressTrackBar.Value;
        }

    }
}
