using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.Codecs;
using CSCore.SoundOut;

//using TagLib;

using System.IO;
using System.Diagnostics;

namespace Tag_Music
{
    public partial class Form1 : Form
    {
        MMDevice device;
        ISoundOut soundOut = null;
        IWaveSource soundSource = null;

        string[] Musics;
        int nowPlayingIndex = 0;

        public Form1()
        {
            InitializeComponent();

            device = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)[0];

            Musics = new string[]{
                @"D:\Music\V3\【初音ミク】 Hand in Hand (Magical Mirai ver.) 【マジカルミライ 2015】.mp3",
                @"D:\Music\V3\Manic   Luna feat.오토마치 우나 & 라나.mp3",
                @"D:\Music\V3\PinocchioP - I'm glad you're evil too   ピノキオピー - きみも悪い人でよかった.mp3" };

            Open(Musics[nowPlayingIndex]);
        }

        private void CleanupPlayback()
        {
            if (soundOut != null)
            {
                soundOut.Dispose();
                soundOut = null;
            }
            if (soundSource != null)
            {
                soundSource.Dispose();
                soundSource = null;
            }
        }

        private void Open(string fileName)
        {
            CleanupPlayback();

            soundSource = CodecFactory.Instance.GetCodec(fileName);

            soundOut = new WasapiOut() { Device = device, Latency = 100 };
            soundOut.Initialize(soundSource);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            soundOut.Stop();
            soundOut.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (soundOut.PlaybackState == PlaybackState.Paused || soundOut.PlaybackState == PlaybackState.Stopped)
            {
                button1.Text = "Pause";
                soundOut.Play();
            }
            else
            {
                button1.Text = "Play";
                soundOut.Pause();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nowPlayingIndex++;
            if (nowPlayingIndex >= Musics.Length)
                nowPlayingIndex = 0;

            if (soundOut.PlaybackState == PlaybackState.Playing)
            {
                Open(Musics[nowPlayingIndex]);
                soundOut.Play();
            }
            else
            {
                Open(Musics[nowPlayingIndex]);
            }
        }
    }
}
