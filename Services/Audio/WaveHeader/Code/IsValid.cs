using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class WaveHeader {




            /// <summary>
            ///     szybkie sprawdzenie czy nagłówek WAV jest poprawny.    
            /// </summary>
            /// <returns></returns>
            public bool IsValid() =>
                ChunkID.Equals("RIFF", StringComparison.OrdinalIgnoreCase) &&
                Format.Equals("WAVE", StringComparison.OrdinalIgnoreCase) &&
                Subchunk1ID.Equals("fmt ", StringComparison.OrdinalIgnoreCase) &&
                Subchunk2ID.Equals("data", StringComparison.OrdinalIgnoreCase);
    }
}
