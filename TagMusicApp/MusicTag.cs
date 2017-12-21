using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagMusicApp
{
    class MusicTag
    {
        private readonly string _tagName;

        public readonly List<Music> Musics = new List<Music>();

        public string TagName => _tagName;

        public MusicTag(string name)
        {
            _tagName = name;
        }

        public void AddMusic(Music music)
        {
            bool isContain = false;
            foreach(Music m in Musics)
            {
                if(m.FilePath == music.FilePath)
                {
                    isContain = true;
                    break;
                }
            }
            
            if(!isContain)
            {
                Musics.Add(music);
            }
        }

        public void RemoveMusic(Music music)
        {
            bool isContain = false;
            foreach (Music m in Musics)
            {
                if (m.FilePath == music.FilePath)
                {
                    isContain = true;
                    break;
                }
            }

            if (isContain)
            {
                Musics.Remove(music);
            }
        }
    }
}
