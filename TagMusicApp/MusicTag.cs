using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagMusicApp
{
    public class MusicTag : MusicList
    {
        private readonly string _tagName;

        public string TagName => _tagName;

        public MusicTag(string name)
        {
            _tagName = name;
        }

        public override bool AddMusic(Music music)
        {
            bool isContain = base.AddMusic(music);

            if (!isContain)
            {
                music.Tags.Add(TagName);
            }

            return isContain;
        }

        public override bool RemoveMusic(Music music)
        {
            bool isContain = base.RemoveMusic(music);

            if (isContain)
            {
                music.Tags.Remove(TagName);
            }

            return isContain;
        }
    }
}
