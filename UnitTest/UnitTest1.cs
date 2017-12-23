using System;
using TagMusicApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        /*
             * A, B     : tag1
             * B, C, D  : tag2
             * A, D, E  : tag3
             * C        : tag4
             */
        MusicTag tag1 = new MusicTag("Tag1");
        MusicTag tag2 = new MusicTag("Tag2");
        MusicTag tag3 = new MusicTag("Tag3");
        MusicTag tag4 = new MusicTag("Tag4");
        Music musicA = new Music();
        Music musicB = new Music();
        Music musicC = new Music();
        Music musicD = new Music();
        Music musicE = new Music();

        [TestMethod]
        public void 중복테스트()
        {
            tag1.AddMusic(musicA);
            tag1.AddMusic(musicA);
            Assert.AreEqual(tag1.Musics.Count, 1);
        }

        public void 추가테스트()
        {

        }
    }
}
