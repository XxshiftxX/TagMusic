using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TagLib;
using CSCore;

namespace TagMusicApp
{
    public class Music
    {
        // 변수
        public readonly string FilePath;
        public readonly List<string> Tags = new List<string>();

        private File musicFile;

        // 프로퍼티
        public string Name
        {
            get
            {
                if (musicFile.Tag.Title == null)
                    return FilePath.Split('\\')[FilePath.Split('\\').Length - 1];

                return musicFile.Tag.Title;
            }
        }
        public string Artist
        {
            get
            {
                if (musicFile.Tag.AlbumArtists.Length < 1)
                    return "Unknown Artist";

                return string.Join(", ", musicFile.Tag.AlbumArtists);
            }
        }
        public TimeSpan TotalTime { get => musicFile.Properties.Duration; }
        private string TotalTimeString => $"{(int)TotalTime.TotalSeconds / 60}:{TotalTime.TotalSeconds % 60}";
        public string TagString
        {
            get
            {
                if (Tags.Count <= 0)
                    return string.Empty;

                string temp = string.Empty;
                for(int i = 0; i < Tags.Count; i++)
                {
                    if (i > 2)
                    {
                        temp += "...";
                        break;
                    }
                    temp += "#" + Tags[i] + " ";
                }
                return temp;
            }
        }

        // 생성자
        public Music(string path)
        {
            FilePath = path;
            musicFile = File.Create(path);
        }
    }
}
