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




            private readonly WaveSampler        _sampler;




            public Editor( WaveSampler sampler ) {
                
                this._sampler       =   sampler;
                this._sizeOfSample  =   _sampler.Header.BitsPerSample / 8;
                IsSuccess = true;
            }

            
    }
}
