using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagMusicApp
{
    public class MusicList : List<Music>
    {
        public virtual bool AddMusic(Music music)
        {
            bool isContain = false;
            foreach (Music m in this)
            {
                if (m.FilePath == music.FilePath)
                {
                    isContain = true;
                    break;
                }
            }

            if (!isContain)
            {
                Add(music);
            }
            return isContain;
        }

        public virtual bool RemoveMusic(Music music)
        {
            bool isContain = false;
            foreach (Music m in this)
            {
                if (m.FilePath == music.FilePath)
                {
                    isContain = true;
                    break;
                }
            }

            if (isContain)
            {
                Remove(music);
            }

            return isContain;
        }

        public MusicList CombineLists(MusicList list)
        {
            MusicList result = new MusicList();

            foreach (Music m in list)
            {
                result.AddMusic(m);
            }
            foreach (Music m in this)
            {
                result.AddMusic(m);
            }

            return result;
        }

        public MusicList ExcludeLists(MusicList list)
        {
            MusicList result = new MusicList();

            foreach (Music m in this)
            {
                result.AddMusic(m);
            }
            foreach (Music m in list)
            {
                result.RemoveMusic(m);
            }

            return result;
        }
    }
}
