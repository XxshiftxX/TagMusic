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
        public MusicList CurrentMusicList = null;
        public bool isRepeat = false;

        private MMDevice _device;
        private ISoundOut _soundOut = null;
        private IWaveSource _soundSource = null;
        private int CurrentMusicIndex = 0;

        public MusicPlayer()
        {
            if(instance != null)
            {
                throw new Exception("인스턴스가 이미 존재합니다");
            }

            instance = this;
            _device = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)[0];
        }

        public void SetMusic()
        {
            if (CurrentMusic == null)
            {
                if (CurrentMusicList == null)
                    return;

                CurrentMusic = CurrentMusicList.First();
            }

            CleanPlayback();

            _soundSource = CodecFactory.Instance.GetCodec(CurrentMusic.FilePath);

            _soundOut = new WasapiOut() { Device = _device, Latency = 100 };
            _soundOut.Initialize(_soundSource);
            _soundOut.Stopped += _soundOut_Stopped;

            _soundOut.Play();
        }

        public void SetMusic(Music music)
        {
            CurrentMusic = music;
            SetMusic();
        }

        public void SetMusic(MusicList list, int index)
        {
            CurrentMusicList = list;
            CurrentMusicIndex = index;
            CurrentMusic = CurrentMusicList[index];
            SetMusic();
        }

        public void PlayMusic() => _soundOut.Play();

        public void PauseMusic() => _soundOut.Pause();

        public void StopMusic() => _soundOut.Stop();

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

        private void _soundOut_Stopped(object sender, PlaybackStoppedEventArgs e)
        {
            if (_soundSource.Position >= _soundSource.Length)
            {
                if (CurrentMusicIndex >= CurrentMusicList.Count - 1 && isRepeat)
                {
                    CurrentMusicIndex = 0;
                    SetMusic(CurrentMusicList, CurrentMusicIndex);
                }
                else
                {
                    SetMusic(CurrentMusicList, ++CurrentMusicIndex);
                }
            }
        }
    }
}
