using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32.SafeHandles;

using Sampler.Services.Audio;

namespace Sampler.Services.Audio {
    public partial class Editor {

            private     int         _sizeOfSample       =   0;  
            public      bool         IsSuccess         { get; private set; }




            public WaveHeader       Header      { get; set; }
            public BufferPcm24      Buffer      { get; set; }




            public Editor( WaveHeader waveHeader, BufferPcm24 buffer) {
                this.Header = waveHeader;
                this.Buffer = buffer;

                this._sizeOfSample = Header.BitsPerSample / 8;
                IsSuccess = true;
            }










            
    }
}
