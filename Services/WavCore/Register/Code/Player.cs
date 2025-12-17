using CSCore;
using CSCore.SoundOut;

using Sampler.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Register {

            private   WaveFormat        _waveFormat     = new WaveFormat( 44100, 24, 2 );
            private   ISoundOut?        _soundOut       = new CSCore.SoundOut.DirectSoundOut();
            private   MemoryStream?     _stream;


        public void Play( int SampleRate = 44100, int BitPerSample = 24, int Channels = 2 ) {

                _soundOut?.Stop();
                _soundOut?.Dispose();
                _stream?.Dispose();

                _waveFormat = new WaveFormat( SampleRate, BitPerSample, Channels );
                _stream = new MemoryStream( this.ExportFrames() );
                var source = new SimpleWaveSource( _stream, _waveFormat );

                _soundOut = new WasapiOut();
                _soundOut.Initialize(source);
                _soundOut.Play();
            }


            public void Stop() { 
                _soundOut?.Stop();
                _soundOut?.Dispose();
                _soundOut = null;

                _stream?.Dispose();
                _stream = null;
            }
    }
}
