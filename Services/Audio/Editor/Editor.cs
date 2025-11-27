using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sampler.Services.Audio;

namespace Sampler.Services.Audio.WaveSample.Editor {
    public partial class Editor {


            public WaveHeader       WaveHeader  { get; set; }
            public BufferPcm24      Buffer      { get; set; }

            public Editor( WaveHeader waveHeader, BufferPcm24 buffer) {
                this.WaveHeader = waveHeader;
                this.Buffer = buffer;
            }
    }
}
