using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagMusicApp
{
    public class MusicList : IEnumerable
    {
        public readonly List<Music> Musics = new List<Music>();
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MusicListEnumerator(Musics);
        }

        public class MusicListEnumerator : IEnumerator
        {
            List<Music> Musics;
            int pos = -1;

            public MusicListEnumerator(List<Music> list)
            {
                Musics = list;
            }

            public object Current => Musics[pos];

            public bool MoveNext()
            {
                pos++;
                return pos >= Musics.Count + 1;
            }

            public void Reset()
            {
                pos = -1;
            }
        }

        public virtual bool AddMusic(Music music)
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

            if (!isContain)
            {
                Musics.Add(music);
            }
            return isContain;
        }

        public virtual bool RemoveMusic(Music music)
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

            return isContain;
        }

        public MusicList CombineLists(MusicList list)
        {
            MusicList result = new MusicList();

            foreach (Music m in list.Musics)
            {
                result.AddMusic(m);
            }
            foreach (Music m in Musics)
            {
                result.AddMusic(m);
            }

            return result;
        }

        public MusicList ExcludeLists(MusicList list)
        {
            MusicList result = new MusicList();

            foreach (Music m in Musics)
            {
                result.AddMusic(m);
            }
            foreach (Music m in list.Musics)
            {
                result.RemoveMusic(m);
            }

            return result;
        }
    }
}
