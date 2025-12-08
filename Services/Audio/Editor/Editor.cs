using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32.SafeHandles;

using Sampler.Services.Audio;

namespace Sampler.Services.Audio {
    public partial class Editor {

            private     int          _sizeOfSample     =   0;  
            public      bool         IsSuccess         { get; private set; }





            public WaveHeader         Header         =   new WaveHeader();
            public BufferPcm24        Buffer         =   new BufferPcm24( new byte[0] );              





            public Editor( WaveHeader header, byte[] audioData ) {
                
                this.Header         =   header;
                this.Buffer         =   new BufferPcm24( audioData );
                this._sizeOfSample  =   Header.BitsPerSample / 8;
                IsSuccess = true;
            }
  
    }
}
