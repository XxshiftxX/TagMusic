using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagMusicApp
{
    public class Music
    {
        public readonly string FilePath;
        public readonly List<string> Tags = new List<string>();

        public string name;
        public string artist;
        public TimeSpan totalTime;

        public string Name => name;
        public string Artist => artist;
        public TimeSpan TotalTime => totalTime;
        public string TotalTimeString => $"{(int)TotalTime.TotalSeconds / 60}:{TotalTime.TotalSeconds % 60}";
    }
}
