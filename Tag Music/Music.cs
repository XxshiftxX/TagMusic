using System;
using System.Collections.Generic;

using TagLib;

namespace Tag_Music
{
    public class Music
    {
        private readonly List<string> tags = new List<string>();
        private string name;

        public String Name
        {
            get => name;
            private set => name = value;
        }
        public IEnumerable<string> Tags
        {
            get => tags;
        }
        public IEnumerable<string> SimpleTags
        {
            get
            {
                if(tags.Count < 4)
                {
                    return tags;
                }
                string[] result = new string[4];
                for(int i = 0; i < 3; i++)
                {
                    result[i] = tags[i];
                }
                result[3] = "...";
                return result;
            }
        }

        public Music(string name, params string[] tags)
        {
            Name = name;

            foreach (var tag in tags)
            {
                this.tags.Add(tag);
            }
        }
    }
    //public class Music
    //{
    //    public readonly string FilePath;
    //    public readonly List<string> Tags = new List<string>();

    //    private File musicFile;

    //    public string name;

    //    public string Name
    //    {
    //        get
    //        {
    //            return name;
    //            /*
    //            if (musicFile.Tag.Title == null)
    //                return FilePath.Split('\\')[FilePath.Split('\\').Length - 1];

    //            return musicFile.Tag.Title;
    //            */
    //        }
    //        // 밑 코드는 추후 없앨 것
    //        private set => name = value;
    //    }

    //    public string Artist
    //    {
    //        get
    //        {
    //            if (musicFile.Tag.AlbumArtists.Length < 1)
    //                return "Unknown Artist";

    //            return string.Join(", ", musicFile.Tag.AlbumArtists);
    //        }
    //    }

    //    public TimeSpan TotalTime { get => musicFile.Properties.Duration; }
    //    private string TotalTimeString => $"{(int)TotalTime.TotalSeconds / 60}:{TotalTime.TotalSeconds % 60}";
    //    public string TagString
    //    {
    //        get
    //        {
    //            if (Tags.Count <= 0)
    //                return string.Empty;

    //            string temp = string.Empty;
    //            for(int i = 0; i < Tags.Count; i++)
    //            {
    //                if (i > 2)
    //                {
    //                    temp += "...";
    //                    break;
    //                }
    //                temp += "#" + Tags[i] + " ";
    //            }
    //            return temp;
    //        }
    //    }
    //    /*
    //    public Music(string path)
    //    {
    //        FilePath = path;
    //        musicFile = File.Create(path);
    //    }
    //    */
    //    public Music(string name, params string[] tags)
    //    {
    //        Name = name;

    //        foreach(var tag in tags)
    //        {
    //            Tags.Add(tag);
    //        }
    //    }
    //}
}
