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
                return pos >= Musics.Count;
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

        public MusicList CombineLists(MusicList list1, MusicList list2)
        {
            MusicList list = new MusicList();

            foreach (Music m in list1)
            {
                list.AddMusic(m);
            }
            foreach (Music m in list2)
            {
                list.AddMusic(m);
            }

            return list;
        }

        public MusicList ExcludeLists(MusicList list1, MusicList list2)
        {
            MusicList list = new MusicList();

            foreach (Music m in list1)
            {
                list.AddMusic(m);
            }
            foreach (Music m in list2)
            {
                list.RemoveMusic(m);
            }

            return list;
        }
    }
}
