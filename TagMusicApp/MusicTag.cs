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

        public MusicTag(string name)
        {
            _tagName = name;
        }
    }
}
