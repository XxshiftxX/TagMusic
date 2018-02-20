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
using System.Windows.Shapes;

namespace TagMusicApp
{
    /// <summary>
    /// TagInfoDialogue.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TagInfoDialogue : Window
    {
        Music currentMusic;
        public TagInfoDialogue(Music music)
        {
            currentMusic = music;
            InitializeComponent();

            songNameField.Text = currentMusic.Name;
            foreach(string tag in currentMusic.Tags)
            {
                TagList.Items.Add("#"+tag.Replace(' ', '_'));
            }
        }
    }
}
