using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;
using CSCore.Codecs.WAV;
using CSCore.SoundOut;

namespace Sampler.Services.Audio {
    public partial class BufferPcm24 {

            private   byte[]          _buffer   = new byte[0];
            public    byte[]           Bytes    => _buffer;



            public void Clear()          { _buffer = new byte[0]; }
            public BufferPcm24( byte[] buffer )     { _buffer = buffer; }

    }
}
