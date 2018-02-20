using Microsoft.VisualStudio.TestTools.UnitTesting;

using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.Codecs;
using CSCore.SoundOut;
using System.Threading;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private MMDevice _device;
        private ISoundOut _soundOut = null;
        private IWaveSource _soundSource = null;

        const string music1 = @"D:\Music\아이마스 컴플리머시기\02. ...In The Name of. ...LOVE.mp3";
        const string music2 = @"D:\Music\아이마스 컴플리머시기\03. VIVID Imagination.mp3";
        const string music3 = @"D:\Music\아이마스 컴플리머시기\05. POKER POKER.mp3";

        [TestMethod]
        public void Test()
        {
            // Init
            _device = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)[0];

            SetMusic(music1);
            _soundOut.Play();
            Thread.Sleep(3000);

            SetMusic(music2);
            _soundOut.Play();
            Thread.Sleep(3000);

            SetMusic(music1);
            _soundOut.Play();
            Thread.Sleep(3000);
        }
        void SetMusic(string m)
        {
            if (_soundOut != null)
            {
                _soundOut.Dispose();
                _soundOut = null;
            }

            _soundSource = CodecFactory.Instance.GetCodec(m);

            _soundOut = new WasapiOut() { Device = _device, Latency = 100 };
            _soundOut.Initialize(_soundSource);
        }
    }
}
