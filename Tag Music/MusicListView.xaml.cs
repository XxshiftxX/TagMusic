using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Tag_Music
{
    /// <summary>
    /// MusicListView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MusicListView : Page
    {
        // WIP Test
        public MusicListView()
        {
            Music[] list = new Music[10]
               {
                new Music("Heal me", "*luna", "j_edm", "vocaloid", "JPN"),
                new Music("Tell me", "*luna", "j_edm", "vocaloid", "JPN"),
                new Music("인도어계라면 트랙 메이커", "Yunomi", "Nicamoq", "j_edm", "JPN"),
                new Music("소소고금", "Reol", "Utaite", "JPN"),
                new Music("우주를 줄게", "볼빨간사춘기", "KOR"),
                new Music("아름다운 빛깔을 입고서", "신데마스", "아이마스", "슈오미_슈코", "코바야카와_사에", "JPN", "정말로_쓸데없고_매우매우_매우_긴_태그_하나", "그리고", "짧고", "많은", "태그"),
                new Music("푸른 일번성", "신데마스", "아이마스", "슈오미_슈코", "JPN"),
                new Music("Lemon", "요네즈_켄시", "Utaite", "JPN"),
                new Music("아모르 파티", "김연자", "KOR"),
                new Music("마음 플로트", "Yunomi", "Nicamoq", "j_edm", "JPN")
               };

            var processed = MusicListBuilder.GetMusiclist(list, new string[] { "JPN", "볼빨간사춘기" }, new string[] {});

            InitializeComponent();

            MusicListBox.ItemsSource = processed;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Hello!");
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Music m = MusicListBox.SelectedItem as Music;
            MessageBox.Show($"{m.Name}\n" +
                $"{string.Join(", ", m.Tags)}");
        }

        private void ExecuteTag(string tag)
        {

        }
    }

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView list = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            int index = list.ItemContainerGenerator.IndexFromContainer(item);
            return index.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double height;
            double param;

            if(double.TryParse(value.ToString(), out height))
            {
                if(double.TryParse(parameter.ToString(), out param))
                {
                    return height / param;
                }
                else
                {
                    return height / 2;
                }
            }
            return 25;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
