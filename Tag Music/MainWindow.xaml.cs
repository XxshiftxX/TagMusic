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

namespace Tag_Music
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Music[] list = new Music[10] 
            {
                new Music("Heal me", "*luna", "j_edm", "vocaloid", "JPN"),
                new Music("Tell me", "*luna", "j_edm", "vocaloid", "JPN"),
                new Music("인도어계라면 트랙 메이커", "Yunomi", "Nicamoq", "j_edm", "JPN"),
                new Music("소소고금", "Reol", "Utaite", "JPN"),
                new Music("우주를 줄게", "볼빨간사춘기", "KOR"),
                new Music("아름다운 빛깔을 입고서", "신데마스", "아이마스", "슈오미_슈코", "코바야카와_사에", "JPN"),
                new Music("푸른 일번성", "신데마스", "아이마스", "슈오미_슈코", "JPN"),
                new Music("Lemon", "요네즈_켄시", "Utaite", "JPN"),
                new Music("아모르 파티", "김연자", "KOR"),
                new Music("마음 플로트", "Yunomi", "Nicamoq", "j_edm", "JPN")
            };

            var processed = MusicListBuilder.GetMusiclist(list, new string[] { "JPN", "볼빨간사춘기" }, new string[] { "코바야카와_사에" }).Select(x => x.name);
            foreach(string s in processed)
                System.Diagnostics.Debug.WriteLine(s);
        }
    }
}
