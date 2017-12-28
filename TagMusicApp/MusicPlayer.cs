using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.Codecs;
using CSCore.SoundOut;

namespace TagMusicApp
{
    public class MusicPlayer
    {
        public static MusicPlayer instance;

        public Music CurrentMusic = null;

        private MMDevice _device;
        private ISoundOut _soundOut = null;
        private IWaveSource _soundSource = null;

        public MusicPlayer()
        {
            if(instance != null)
            {

            }

            instance = this;
            _device = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)[0];
        }

        public void SetMusic(Music music)
        {
            CurrentMusic = music;

            CleanPlayback();

            _soundSource = CodecFactory.Instance.GetCodec(CurrentMusic.FilePath);

            _soundOut = new WasapiOut() { Device = _device, Latency = 100 };
            _soundOut.Initialize(_soundSource);
        }

        public void PlayMusic()
        {
            _soundOut.Play();
        }

        public void PauseMusic()
        {
            _soundOut.Pause();
        }

        public void StopMusic()
        {
            _soundOut.Stop();
        }

        private void CleanPlayback()
        {
            if(_soundOut != null)
            {
                _soundOut.Dispose();
                _soundOut = null;
            }
            if(_soundSource != null)
            {
                _soundSource.Dispose();
                _soundSource = null;
            }
        }
    }
}
