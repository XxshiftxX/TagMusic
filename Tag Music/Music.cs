using System;
using System.Collections.Generic;

using TagLib;

namespace Tag_Music
{
    public class Music
    {
        public readonly string FilePath;
        public readonly List<string> Tags = new List<string>();

        private File musicFile;

        private string name = null;

        public string Name
        {
            get
            {
                if (name != null)
                    return name;

                if (musicFile.Tag.Title == null)
                    return System.IO.Path.GetFileName(FilePath);

                return musicFile.Tag.Title;
            }

            set
            {
                name = value;
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

        public Music(string path)
        {
            FilePath = path;
            musicFile = File.Create(path);
        }

        // For Test
        public Music(string name, params string[] tags)
        {
            Name = name;

            foreach(var tag in tags)
            {
                Tags.Add(tag);
            }
        }
    }
}
