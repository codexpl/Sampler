using CSCore;
using CSCore.Codecs.WAV;
using CSCore.SoundOut;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sampler.Models;

namespace Sampler.Services.Audio {
    public partial class BufferPcm24 {

                
            private   WaveFormat        _waveFormat = new WaveFormat( 44100, 24, 2 );
            private   ISoundOut         _soundOut = new CSCore.SoundOut.DirectSoundOut();


            public void Play() {
                var stream = new MemoryStream( this._buffer );
                var source = new SimpleWaveSource( stream, _waveFormat );
                _soundOut?.Stop();
                _soundOut = new WasapiOut();
                _soundOut.Initialize( source );
                _soundOut.Play();
            }

    }
}
