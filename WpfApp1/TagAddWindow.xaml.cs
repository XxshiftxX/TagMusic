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

namespace WpfApp1
{
    /// <summary>
    /// TagAddWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TagAddWindow : Window
    {
        string tagName;

        public TagAddWindow()
        {
            InitializeComponent();

            TagNameField.TextChanged += TagNameField_TextChanged;
            TagNameField.KeyDown += PressEnter;

            TagNameField.Focus();
        }

        public string TagName
        {
            get { return tagName; }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            tagName = TagNameField.Text;
            DialogResult = true;
            Close();
        }

        private void PressEnter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                AddButton_Click(null, null);
            }
        }

        private void TagNameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach(KeyValuePair<string, MusicTag> temp in MainWindow.allTagList)
            {
                if(temp.Value.Name.StartsWith(TagNameField.Text))
                {
                    int stringCount = TagNameField.Text.Length;
                    TagNameField.Text = temp.Value.Name;
                    TagNameField.Focus();
                    TagNameField.Select(stringCount, TagNameField.Text.Length);
                }
            }
        }
    }
}
