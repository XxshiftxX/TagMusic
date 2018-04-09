using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag_Music
{
    public static class MusicListBuilder
    {
        public static IEnumerable<Music> GetMusiclist(IEnumerable<Music> list, string[] sumTag, string[] subTag)
        {
            IEnumerable<Music> result = list;
            result = result.Where(x =>
            {
                foreach (string tag in x.Tags)
                {
                    if (sumTag.Contains(tag)) return true;
                }
                return false;
            });
            foreach (var tag in subTag)
            {
                result = from music in result
                         where !music.Tags.Contains(tag)
                         select music;
            }
            return result;
        }
    }
}
