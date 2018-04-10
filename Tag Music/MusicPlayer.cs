using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.Codecs;
using CSCore.SoundOut;
using System.Threading;

namespace Tag_Music
{
    public static class MusicPlayer
    {
        private static bool isPlaying = false;

        private static MMDevice _device;
        private static ISoundOut _soundOut = null;
        private static IWaveSource _soundSource = null;

        static MusicPlayer()
        {
            _device = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)[0];
        }

        public static void PlayNew(string filePath)
        {
            _soundSource = CodecFactory.Instance.GetCodec(filePath);
            _soundOut = new WasapiOut() { Latency = 100, Device = _device };
            _soundOut.Initialize(_soundSource);

            _soundOut.Play();
        }

        public static void Play()
        {
            if(isPlaying)
            {
                _soundOut.Play();
            }
            else
            {
                _soundOut.Pause();
            }
        }
    }
}
