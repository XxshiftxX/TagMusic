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
    /// AllMusic.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AllMusic : Page
    {
        MusicList musics = new MusicList();

        public AllMusic()
        {
            InitializeComponent();

            Music ranko = new Music(@"D:\Music\[2012-2017] Cinderella Girls [Console Games]\01. CINDERELLA MASTER Solo\[2012.08.08] CINDERELLA MASTER 006 Kanzaki Ranko\01. Tsubomi Yume Miru Rapsodia ~Alma no Michibiki~.mp3");
            Music rika = new Music(@"D:\Music\[2012-2017] Cinderella Girls [Console Games]\01. CINDERELLA MASTER Solo\[2012.04.18] CINDERELLA MASTER 005 Jougasaki Rika\01. DOKIDOKI Rhythm.mp3");
            Music mika = new Music(@"D:\Music\[2012-2017] Cinderella Girls [Console Games]\01. CINDERELLA MASTER Solo\[2012.08.08] CINDERELLA MASTER 009 Jougasaki Mika\01. TOKIMEKI Escalate.mp3");
            Music star = new Music(@"D:\Music\임시음원\【アイドルマスター ミリオンライブ！】 流星群.mp3");
            Music dear = new Music(@"D:\Music\임시음원\바바 코노미 (馬場このみ) - dear....mp3");
            Music tonarini = new Music(@"D:\Music\임시음원\아즈사 곁에.mp3");

            musics.Add(ranko);
            musics.Add(rika);
            musics.Add(mika);
            musics.Add(star);
            musics.Add(dear);

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

            TagInfoDialogue d = new TagInfoDialogue(tonarini);
            d.ShowDialog();

            MusicListBox.ItemsSource = cinderella.CombineLists(million);
        }

        private void MusicListItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            MusicPlayer.instance.SetMusic((MusicList)MusicListBox.ItemsSource, MusicListBox.SelectedIndex);
        }
    }
}
