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
        // 메인 그리드의 현재 그리드(현재 선택된 메뉴)
        Grid NowGrid;
        
        MusicList list_All;
        MusicList list_Playing;
        MusicTagList list_Tags;
        
        // cscore 관련 변수들
        MMDevice device;
        ISoundOut soundOut = null;
        IWaveSource soundSource = null;

        // Update 함수의 루프 여부 (false로 할 시 루프가 종료 > 재시작 필요)
        bool isUpdating = true;

        // 현재 재생중인 음악의 번호
        public int playingIndex = 0;

        // 진행 바의 길이
        float progressBar = 0;

        

        // 생성자
        public MainWindow()
        {
            InitializeComponent();
            
            // list들을 xaml과 연동
            list_All = new MusicList();
            list_All = (MusicList)FindResource("AllMusics");
            list_Playing = new MusicList();
            list_Playing = (MusicList)FindResource("PlayingMusics");
            list_Tags = new MusicTagList();
            list_Tags = (MusicTagList)FindResource("TagList");

            // cscore Device 설정
            device = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)[0];

            Debug.Print(list_All.Count.ToString());

            // 파일이 없을 시 한개 불러오기
            if (list_All.Count < 1)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                Nullable<bool> result = dlg.ShowDialog();

                list_All.Add(new Music(dlg.FileName));
            }

            // 플레이리스트의 첫번쨰 음악을 오픈
            Open(list_All[0].Path);

            // 현재 열고있는 탭을 PlayingTab으로
            NowGrid = PlayingTab;
            
            Update();

            // TEST AREA
            soundOut.Volume = 0.5f;
        }

        // 0.1초당 한번씩 실행되는 함수
        private async void Update()
        {
            while (isUpdating)
            {
                progressBar = (float)soundSource.Position / soundSource.Length * (float)Screen.Width;

                ProgressBar.Width = progressBar;

                if (soundSource.Position == soundSource.Length)
                {
                    if (++playingIndex >= list_Playing.Count)
                        playingIndex = 0;

                    Open(list_Playing[playingIndex].Path);
                    soundOut.Play();
                }

                await Task.Delay(100);
            }
        }

        // 리스트에서 음악 더블클릭 (list_Playing)
        private void PlayingMusicList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Open(((Music)PlayingMusicListBox.SelectedItem).Path);
            soundOut.Play();
        }

        // 리스트에서 음악 더블클릭 (list_Playing)
        private void AllMusicList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            list_Playing.Clear();
            foreach(Music m in list_All)
            {
                list_Playing.Add(m);
            }
            Open(((Music)AllMusicListBox.SelectedItem).Path);
            soundOut.Play();
        }

        // 음악 파일 실행 전 Clean up 작업
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
        

        // 음악 파일 실행
        private void Open(string filename)
        {
            CleanupPlayback();

            soundSource = CodecFactory.Instance.GetCodec(filename);

            soundOut = new WasapiOut() { Device = device, Latency = 100 };
            soundOut.Initialize(soundSource);
        }

        // 음악 재생, 일시정지
        private void PlayButton_Click(object sender = null, RoutedEventArgs e = null)
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

        // 다음곡 재생
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            playingIndex++;
            if (playingIndex >= list_Playing.Count)
                playingIndex = 0;

            bool isPlaing = soundOut.PlaybackState == PlaybackState.Playing;

            Open(list_Playing[playingIndex].Path);
            if (isPlaing)
                soundOut.Play();
        }

        // list_Playing에 해당 태그를 추가
        private void AddTag(MusicTag tags)
        {
            foreach (Music m in tags)
            {
                bool overlapped = false;
                foreach(Music temp in list_Playing)
                {
                    if (temp.Path == m.Path)
                        overlapped = true;
                }

                if(!overlapped)
                    list_Playing.Add(m);
            }
        }

        // (미사용) 파일 실행
        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            list_All.Add(new Music(dlg.FileName));
        }

        // 창 종료 전에 실행
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            soundOut.Stop();
            soundOut.Dispose();
        }

        // 상단 메뉴 (AllMusic)
        private void Menu_AllMusicTabButton_Click(object sender, RoutedEventArgs e)
        {
            NowGrid.Visibility = Visibility.Hidden;
            AllMusicTab.Visibility = Visibility.Visible;
            NowGrid = AllMusicTab;
        }

        // 상단 메뉴 (PlayingTab)
        private void Menu_PlayingTabButton_Click(object sender, RoutedEventArgs e)
        {
            NowGrid.Visibility = Visibility.Hidden;
            PlayingTab.Visibility = Visibility.Visible;
            NowGrid = PlayingTab;
        }

        // 상단 메뉴 (TagTab)
        private void Menu_TagTabButton_Click(object sender, RoutedEventArgs e)
        {
           NowGrid.Visibility = Visibility.Hidden;
            TagTab.Visibility = Visibility.Visible;
            NowGrid = TagTab;
        }

        private void FillAddButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            bool overlapped = false;
            foreach (Music temp in list_All)
            {
                if (temp.Path == dlg.FileName)
                    overlapped = true;
            }

            if (!overlapped)
                list_All.Add(new Music(dlg.FileName));
        }



        private void TagsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            list_Playing.Clear();
            foreach (Music m in ((KeyValuePair<string, MusicTag>)(TagsListBox.SelectedItem)).Value)
            {
                list_Playing.Add(m);
            }
            Open(((Music)AllMusicListBox.SelectedItem).Path);
            soundOut.Play();
        }

        private void TagAddButton_Click(object sender, RoutedEventArgs e)
        {
            TagAddWindow window = new TagAddWindow();
            string temp = string.Empty;

            if (window.ShowDialog() == true)
            {
                temp = window.TagName;
            }

            if(!list_Tags.ContainsKey(temp))
                list_Tags.Add(window.TagName, new MusicTag());

            Debug.Print(PlayingMusicListBox.SelectedItem.ToString());
            list_Tags[temp].Add((Music)PlayingMusicListBox.SelectedItem);
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            Open(list_Tags["asdf"][1].Path);
            soundOut.Play();
        }
    }
}
