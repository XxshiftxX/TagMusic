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

            List<Music> musics = new List<Music>();

            musics.Add(new Music() { name = "TOKIMEKI 에스컬레이트", artist = "죠가사키 미카", totalTime = new TimeSpan(0, 4, 10)});

            MusicListBox.ItemsSource = musics;

            musics.Add(new Music() { name = "메르헨 데뷔", artist = "아베 나나", totalTime = new TimeSpan(0, 4, 32)});
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

        public void tagMoreButton(object sender, RoutedEventArgs e)
        {

        }
    }
}