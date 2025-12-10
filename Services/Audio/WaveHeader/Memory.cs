using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class WaveHeader {

            public int      ChunkSize           { get; set; }
            public string   Format              { get; set; }   =   string.Empty;
            public string   Subchunk1ID         { get; set; }   =   string.Empty;
            public uint     Subchunk1Size       { get; set; }
            public ushort   FormatAudio         { get; set; }
            public short    NumChannels         { get; set; }
            public int      SampleRate          { get; set; }
            public uint     ByteRate            { get; set; }
            public ushort   BlockAlign          { get; set; }
            public short    BitsPerSample       { get; set; }
            public string   Subchunk2ID         { get; set; }   =   string.Empty;
            public int      Subchunk2Size       { get; set; }
    }
}
