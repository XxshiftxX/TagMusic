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
using System.Collections.ObjectModel;

namespace Tag_Music
{
    public partial class MusicTextbox : Form
    {
        MMDevice device;
        ISoundOut soundOut = null;
        IWaveSource soundSource = null;

        BindingList<string> nowPlayingMusics;
        int nowPlayingIndex = 0;

        BindingList<string> newTag = new BindingList<string>
        {
            @"D:\Music\V3\【初音ミク】 Hand in Hand (Magical Mirai ver.) 【マジカルミライ 2015】.mp3",
            @"D:\Music\V3\MKDR feat. [하츠네 미쿠] Umi Kun Cover (DECO   27).mp3"
        };

        BindingList<string> newTag2 = new BindingList<string>
        {
            @"D:\Music\V3\【初音ミク】 Hand in Hand (Magical Mirai ver.) 【マジカルミライ 2015】.mp3"
        };

        public MusicTextbox()
        {
            InitializeComponent();

            device = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)[0];

            nowPlayingMusics = new BindingList<string>
            {
                @"D:\Music\V3\【初音ミク】 Hand in Hand (Magical Mirai ver.) 【マジカルミライ 2015】.mp3",
                @"D:\Music\V3\Manic   Luna feat.오토마치 우나 & 라나.mp3",
                @"D:\Music\V3\PinocchioP - I'm glad you're evil too   ピノキオピー - きみも悪い人でよかった.mp3"
            };

            Open(nowPlayingMusics[nowPlayingIndex]);

            MusicList.DataSource = nowPlayingMusics;
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

        private void AddTagToList(params BindingList<string>[] tags)
        {
            foreach(BindingList<string> item in tags)
            {
                BindingList<string> temp = new BindingList<string>();
                foreach (string jtem in nowPlayingMusics.Union(item))
                {
                    temp.Add(jtem);
                }
                nowPlayingMusics = temp;
            }
        }

        private void DeleteTagToList(params BindingList<string>[] tags)
        {
            foreach (BindingList<string> item in tags)
            {
                BindingList<string> temp = new BindingList<string>();
                foreach (string jtem in nowPlayingMusics.Except(item))
                {
                    string add = temp.AddNew();
                    add = jtem;
                }
                foreach(string jtem in temp)
                {
                    string add = nowPlayingMusics.AddNew();
                    add = jtem;
                }
            }
        }
        
        void listOfParts_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = "hi";

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
                Play.Text = "Pause";
                soundOut.Play();
            }
            else
            {
                Play.Text = "Play";
                soundOut.Pause();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nowPlayingIndex++;
            if (nowPlayingIndex >= nowPlayingMusics.Count)
                nowPlayingIndex = 0;

            if (soundOut.PlaybackState == PlaybackState.Playing)
            {
                Open(nowPlayingMusics[nowPlayingIndex]);
                soundOut.Play();
            }
            else
            {
                Open(nowPlayingMusics[nowPlayingIndex]);
            }
        }

        private void Test_Click(object sender, EventArgs e)
        {
            AddTagToList(newTag);
        }

        private void Test2_Click(object sender, EventArgs e)
        {
            DeleteTagToList(newTag2);
        }
    }
}
