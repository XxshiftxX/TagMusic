using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.ComponentModel;

using TagLib;

namespace WpfApp1
{
    public class MusicList : ObservableCollection<Music>
    {
        public MusicList()
        {

        }

        public MusicList(Music music)
        {
            Add(music);
        }

        public MusicList(MusicTag tag)
        {
            AddTag(tag);
        }

        public void AddTag(MusicTag tag)
        {
            bool containCheck = false;
            foreach (Music music in tag)
            {
                containCheck = false;
                foreach (Music checkingMusic in this)
                {
                    if (checkingMusic.Path == music.Path)
                    {
                        containCheck = true;
                        break;
                    }
                }

                if (!containCheck)
                    Add(music);
            }
        }

        public void SubTag(MusicTag tag)
        {
            foreach (Music music in tag)
            {
                foreach (Music checkingMusic in this)
                {
                    if (checkingMusic.Path == music.Path)
                    {
                        Remove(checkingMusic);
                        break;
                    }
                }
            }
        }
    }

    public class MusicTag : ObservableCollection<Music> { }

    public class MusicTagList : Dictionary<string, MusicTag> { }

    public class Music : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string strName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(strName));
            }
        }

        string path;
        File file;
        List<string> tags = new List<string>();

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
        public List<string> Tag
        {
            get
            {
                return Tag;
            }
        }
    }
}
