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

namespace TagMusicApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MusicPlayer.instance = new MusicPlayer();

            MusicList musics = new MusicList();

            Music ranko = new Music(@"D:\Music\[2012-2017] Cinderella Girls [Console Games]\01. CINDERELLA MASTER Solo\[2012.08.08] CINDERELLA MASTER 006 Kanzaki Ranko\01. Tsubomi Yume Miru Rapsodia ~Alma no Michibiki~.mp3");
            Music rika = new Music(@"D:\Music\[2012-2017] Cinderella Girls [Console Games]\01. CINDERELLA MASTER Solo\[2012.04.18] CINDERELLA MASTER 005 Jougasaki Rika\01. DOKIDOKI Rhythm.mp3");
            Music mika = new Music(@"D:\Music\[2012-2017] Cinderella Girls [Console Games]\01. CINDERELLA MASTER Solo\[2012.08.08] CINDERELLA MASTER 009 Jougasaki Mika\01. TOKIMEKI Escalate.mp3");
            Music star = new Music(@"D:\Music\임시음원\【アイドルマスター ミリオンライブ！】 流星群.mp3");
            Music dear = new Music(@"D:\Music\임시음원\바바 코노미 (馬場このみ) - dear....mp3");
            Music tonarini = new Music(@"D:\Music\임시음원\아즈사 곁에.mp3");

            musics.Musics.Add(ranko);
            musics.Musics.Add(rika);
            musics.Musics.Add(mika);
            musics.Musics.Add(star);
            musics.Musics.Add(dear);

            MusicTag cinderella = new MusicTag("신데마스");
            MusicTag main = new MusicTag("본가마스");
            MusicTag million = new MusicTag("밀리마스");

            cinderella.AddMusic(ranko);
            cinderella.AddMusic(rika);
            cinderella.AddMusic(mika);
            million.AddMusic(star);
            million.AddMusic(dear);
            million.AddMusic(tonarini);
            main.AddMusic(tonarini);

            MusicListBox.ItemsSource = cinderella.CombineLists(million).Musics;
        }

        public Grid GetMusicItem(Music music)
        {
            Grid result = new Grid();

            // 템플릿 자체 설정
            RowDefinition temp1 = new RowDefinition();
            RowDefinition temp2 = new RowDefinition();
            temp1.Height = new GridLength(1, GridUnitType.Star);
            temp2.Height = new GridLength(1, GridUnitType.Star);
            result.Height = 50;
            result.RowDefinitions.Add(temp1);
            result.RowDefinitions.Add(temp2);
            result.HorizontalAlignment = HorizontalAlignment.Stretch;

            // 좌상단 StackPanel 설정
            StackPanel luStackPanel = new StackPanel();
            luStackPanel.Orientation = Orientation.Horizontal;
            luStackPanel.HorizontalAlignment = HorizontalAlignment.Left;

            TextBlock musicName = new TextBlock();
            musicName.Text = music.Name;
            musicName.VerticalAlignment = VerticalAlignment.Center;
            musicName.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            musicName.Margin = new Thickness(4, 0, 4, 0);
            luStackPanel.Children.Add(musicName);

            result.Children.Add(luStackPanel);

            // 우상단 StackPanel 설정
            StackPanel ruStackPanel = new StackPanel();
            ruStackPanel.Orientation = Orientation.Horizontal;
            ruStackPanel.HorizontalAlignment = HorizontalAlignment.Right;

            TextBlock musicTime = new TextBlock();
            musicTime.Text = $"{(int)music.TotalTime.TotalSeconds / 60}:{music.TotalTime.TotalSeconds % 60}";
            musicTime.VerticalAlignment = VerticalAlignment.Center;
            musicTime.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            musicTime.Margin = new Thickness(4, 0, 4, 0);
            ruStackPanel.Children.Add(musicTime);

            result.Children.Add(ruStackPanel);

            // 좌하단 StackPanel 설정
            StackPanel ldStackPanel = new StackPanel();
            ldStackPanel.Orientation = Orientation.Horizontal;
            ldStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(ldStackPanel, 1);

            TextBlock musicArtist = new TextBlock();
            musicArtist.Text = music.Artist;
            musicArtist.VerticalAlignment = VerticalAlignment.Center;
            musicArtist.Foreground = new SolidColorBrush(Color.FromRgb(89, 106, 109));
            musicArtist.Margin = new Thickness(4, 0, 4, 0);
            ldStackPanel.Children.Add(musicArtist);

            result.Children.Add(ldStackPanel);

            // 우하단 StackPanel 설정
            StackPanel rdStackPanel = new StackPanel();
            ldStackPanel.Orientation = Orientation.Horizontal;
            rdStackPanel.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetRow(rdStackPanel, 1);

            Button moreButton = new Button();
            moreButton.VerticalAlignment = VerticalAlignment.Center;
            moreButton.BorderBrush = null;
            moreButton.Background = null;
            moreButton.Foreground = new SolidColorBrush(Color.FromRgb(124, 148, 135));
            moreButton.Margin = new Thickness(5, 0, 5, 0);
            rdStackPanel.Children.Add(moreButton);

            TextBlock moreButtonText = new TextBlock();
            moreButtonText.Text = "#More";
            moreButtonText.VerticalAlignment = VerticalAlignment.Center;
            moreButton.Content = moreButtonText;
            moreButton.Click += (x, y) =>
            {
                System.Diagnostics.Debug.WriteLine(( (x as Button).Content as TextBlock ).Text);
            };

            result.Children.Add(rdStackPanel);

            return result;
        }

        public void TagMoreButton(object sender, RoutedEventArgs e)
        {
            Music music = (sender as Button).DataContext as Music;

            MusicPlayer.instance.SetMusic(music);
            MusicPlayer.instance.Play();
        }
    }
}