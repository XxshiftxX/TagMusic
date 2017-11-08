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

using System.ComponentModel;

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
        public MusicList allMusicList;
        public MusicList playingMusicList;

        public MusicTagList allTagList;

        public int playingIndex = 0;


        private Grid _nowGrid;

        private MMDevice _device;
        private ISoundOut _soundOut = null;
        private IWaveSource _soundSource = null;

        private bool _isUpdating = true;

        private float _progressBar = 0;
        

        // 생성자
        public MainWindow()
        {
            InitializeComponent();
            
            // list들을 xaml과 연동
            allMusicList = new MusicList();
            allMusicList = (MusicList)FindResource("rscAllMusicList");
            playingMusicList = new MusicList();
            playingMusicList = (MusicList)FindResource("rscPlayingMusicList");
            allTagList = new MusicTagList();
            allTagList = (MusicTagList)FindResource("rscAllTagList");

            // cscore Device 설정
            _device = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)[0];

            Debug.Print(allMusicList.Count.ToString());

            // 파일이 없을 시 한개 불러오기
            if (allMusicList.Count < 1)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                Nullable<bool> result = dlg.ShowDialog();

                allMusicList.Add(new Music(dlg.FileName));
            }

            // 플레이리스트의 첫번쨰 음악을 오픈
            Open(allMusicList[0].Path);

            // 현재 열고있는 탭을 PlayingTab으로
            _nowGrid = PlayingTab;
            
            Update();

            // TEST AREA
            _soundOut.Volume = 0.5f;
        }

        // 0.1초당 한번씩 실행되는 함수
        private async void Update()
        {
            while (_isUpdating)
            {
                _progressBar = (float)_soundSource.Position / _soundSource.Length * (float)Screen.Width;

                ProgressBar.Width = _progressBar;

                if (_soundSource.Position == _soundSource.Length)
                {
                    if (++playingIndex >= playingMusicList.Count)
                        playingIndex = 0;

                    Open(playingMusicList[playingIndex].Path);
                    _soundOut.Play();
                }

                await Task.Delay(100);
            }
        }

        // 리스트에서 음악 더블클릭 (list_Playing)
        private void PlayingMusicList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Open(((Music)PlayingMusicListBox.SelectedItem).Path);
            _soundOut.Play();
        }

        // 리스트에서 음악 더블클릭 (list_Playing)
        private void AllMusicList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            playingMusicList.Clear();
            foreach(Music m in allMusicList)
            {
                playingMusicList.Add(m);
            }
            Open(((Music)AllMusicListBox.SelectedItem).Path);
            _soundOut.Play();
        }

        // 음악 파일 실행 전 Clean up 작업
        private void CleanupPlayback()
        {
            if (_soundOut != null)
            {
                _soundOut.Dispose();
                _soundOut = null;
            }
            if (_soundSource != null)
            {
                _soundSource.Dispose();
                _soundSource = null;
            }
        }
        

        // 음악 파일 실행
        private void Open(string filename)
        {
            CleanupPlayback();

            _soundSource = CodecFactory.Instance.GetCodec(filename);

            _soundOut = new WasapiOut() { Device = _device, Latency = 100 };
            _soundOut.Initialize(_soundSource);
        }

        // 음악 재생, 일시정지
        private void PlayButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            if (_soundOut.PlaybackState == PlaybackState.Playing)
            {
                _soundOut.Stop();
            }
            else
            {
                _soundOut.Play();
                _soundSource.Position = 21229056;
            }
        }

        // 다음곡 재생
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            playingIndex++;
            if (playingIndex >= playingMusicList.Count)
                playingIndex = 0;

            bool isPlaing = _soundOut.PlaybackState == PlaybackState.Playing;

            Open(playingMusicList[playingIndex].Path);
            if (isPlaing)
                _soundOut.Play();
        }

        // list_Playing에 해당 태그를 추가
        private void AddTag(MusicTag tags)
        {
            foreach (Music m in tags)
            {
                bool overlapped = false;
                foreach(Music temp in playingMusicList)
                {
                    if (temp.Path == m.Path)
                        overlapped = true;
                }

                if(!overlapped)
                    playingMusicList.Add(m);
            }
        }

        // (미사용) 파일 실행
        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            allMusicList.Add(new Music(dlg.FileName));
        }

        // 창 종료 전에 실행
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _soundOut.Stop();
            _soundOut.Dispose();
        }

        // 상단 메뉴 (AllMusic)
        private void Menu_AllMusicTabButton_Click(object sender, RoutedEventArgs e)
        {
            _nowGrid.Visibility = Visibility.Hidden;
            AllMusicTab.Visibility = Visibility.Visible;
            _nowGrid = AllMusicTab;
        }

        // 상단 메뉴 (PlayingTab)
        private void Menu_PlayingTabButton_Click(object sender, RoutedEventArgs e)
        {
            _nowGrid.Visibility = Visibility.Hidden;
            PlayingTab.Visibility = Visibility.Visible;
            _nowGrid = PlayingTab;
        }

        // 상단 메뉴 (TagTab)
        private void Menu_TagTabButton_Click(object sender, RoutedEventArgs e)
        {
           _nowGrid.Visibility = Visibility.Hidden;
            TagTab.Visibility = Visibility.Visible;
            _nowGrid = TagTab;
        }

        private void FillAddButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            bool overlapped = false;
            foreach (Music temp in allMusicList)
            {
                if (temp.Path == dlg.FileName)
                    overlapped = true;
            }

            if (!overlapped)
                allMusicList.Add(new Music(dlg.FileName));
        }



        private void TagsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            playingMusicList.Clear();
            foreach (Music m in ((KeyValuePair<string, MusicTag>)(TagsListBox.SelectedItem)).Value)
            {
                playingMusicList.Add(m);
            }
            Open(((Music)AllMusicListBox.SelectedItem).Path);
            _soundOut.Play();
        }

        private void TagAddButton_Click(object sender, RoutedEventArgs e)
        {
            TagAddWindow window = new TagAddWindow();
            string temp = string.Empty;

            if (window.ShowDialog() == true)
            {
                temp = window.TagName;
            }

            if(!allTagList.ContainsKey(temp))
                allTagList.Add(window.TagName, new MusicTag());

            Debug.Print(PlayingMusicListBox.SelectedItem.ToString());
            allTagList[temp].Add((Music)PlayingMusicListBox.SelectedItem);
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            Open(allTagList["asdf"][1].Path);
            _soundOut.Play();
        }
    }
}
