using NAudio;
using NAudio.Wave;
using NAudio.Vorbis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        string[] files;

        List<string> localmusiclist=new List<string>{ };
        public Form1()
        {
            InitializeComponent();
        }

        private void musicplay(string filename)
        {
            axWindowsMediaPlayer1.URL = filename;
            string extension=Path.GetExtension(filename);


            if (extension == ".ogg") { Console.WriteLine("这是ogg文件。"); }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();

            }
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (localmusiclist.Count > 0)
            {
                axWindowsMediaPlayer1.URL = localmusiclist[listBox1.SelectedIndex];
                musicplay(axWindowsMediaPlayer1.URL);
                label1.Text=Path.GetFileNameWithoutExtension(localmusiclist[listBox1.SelectedIndex]);
            }
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "选择音频|*.mp3;*.flac;*.wav";
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                localmusiclist.Clear();
                listBox1.Items.Clear();

                if (files != null)
                {
                    Array.Clear(files, 0, files.Length);
                }

                files = openFileDialog1.FileNames;
                string[] array = files;
                foreach (string x in array)
                {
                    listBox1.Items.Add(x);
                    localmusiclist.Add(x);
                }

            }
           
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume= trackBar1.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (localmusiclist.Count > 0)
            {
                int index = listBox1.SelectedIndex + 1;

                if (index >= localmusiclist.Count()) { index = 0; }

                axWindowsMediaPlayer1.URL = localmusiclist[index];

                musicplay(axWindowsMediaPlayer1.URL);
                label1.Text = Path.GetFileNameWithoutExtension(localmusiclist[index]);

                listBox1.SelectedIndex = index;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "打开音频|*.ogg";

            string oggFilePath = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                oggFilePath = openFileDialog.FileName;
            }

            using (var vorbisReader = new VorbisWaveReader(oggFilePath))
            {
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(vorbisReader);
                    outputDevice.Play();

                    // 等待播放完成，或者您可以根据需要添加其他逻辑  
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }

            using (var vorbisStream = new VorbisWaveReader(oggFilePath))
            {
                // 创建WaveOutEvent实例来播放音频  
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(vorbisStream);
                    outputDevice.Play();

                    // 等待播放完成，或者你可以添加其他逻辑来处理播放过程  
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
