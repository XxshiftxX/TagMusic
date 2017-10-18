using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

using System.Collections.ObjectModel;
using System.ComponentModel;

using TagLib;

using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.Codecs;
using CSCore.SoundOut;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        MusicList list;
        MMDevice device;
        ISoundOut soundOut = null;
        IWaveSource soundSource = null;
        bool isLooping = true;

        // 현재 재생중인 음악의 번호
        public int playingIndex = 0;

        float progressBar = 0;

        MusicTag testTag;

        // 생성자
        public MainWindow()
        {
            InitializeComponent();

            list = new MusicList();

            list = (MusicList)FindResource("musics");

            testTag = (MusicTag)FindResource("Tag1");

            device = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)[0];

            Open(list[0].Path);

            loop();
        }

        private async void loop()
        {
            while (isLooping)
            {
                progressBar = (float)soundSource.Position / soundSource.Length * (float)Screen.Width;

                ProgressBar.Width = progressBar;

                if (soundSource.Position == soundSource.Length)
                {
                    if (++playingIndex >= list.Count)
                        playingIndex = 0;

                    Open(list[playingIndex].Path);
                    soundOut.Play();
                }

                await Task.Delay(100);
            }
        }

        // 리스트에서 음악 더블클릭
        private void MusicList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Open(((Music)MusicListBox.SelectedItem).Path);
            soundOut.Play();
        }

        // 음악 실행 전 Clean up 작업
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

        private void Open(string filename)
        {
            CleanupPlayback();

            soundSource = CodecFactory.Instance.GetCodec(filename);

            soundOut = new WasapiOut() { Device = device, Latency = 100 };
            soundOut.Initialize(soundSource);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (soundOut.PlaybackState == PlaybackState.Playing)
            {
                soundOut.Stop();
            }
            else
            {
                soundOut.Play();
                soundSource.Position = 21229056;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            playingIndex++;
            if (playingIndex >= list.Count)
                playingIndex = 0;

            bool isPlaing = soundOut.PlaybackState == PlaybackState.Playing;

            Open(list[playingIndex].Path);
            if (isPlaing)
                soundOut.Play();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Music m in testTag)
            {
                bool overlapped = false;
                foreach(Music temp in list)
                {   
                    if (temp.Path == m.Path)
                        overlapped = true;
                }

                if(!overlapped)
                    list.Add(m);
            }
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                testTag.Add(new Music(dlg.FileName));
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            soundOut.Stop();
            soundOut.Dispose();
        }
    }

    public class MusicList : ObservableCollection<Music> { }

    public class MusicTag : ObservableCollection<Music> { }

    public class Music : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string strName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(strName));
            }
        }

        string path;
        File file;

        public Music()
        {

        }

        public Music(string path)
        {
            this.path = path;
            file = File.Create(path);
        }

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                Notify("name");
            }
        }
        public string Name
        {
            get
            {
                if (file == null)
                    file = File.Create(path);
                if (file.Tag.Title == null)
                    return path.Split('\\')[path.Split('\\').Length - 1];

                return file.Tag.Title;
            }
        }
        public string Artist
        {
            get
            {
                if (file == null)
                    file = File.Create(path);

                if (file.Tag.Artists.Length < 1)
                    return "Unknown Artist";

                return file.Tag.Artists[0];
            }
        }
    }
}
