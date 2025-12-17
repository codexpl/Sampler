using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Core {


        public void Append() {
            foreach( var frame in RegisterB.Frames )  RegisterA.Frames.Add( frame );
            GetHeaderA().Subchunk2Size = RegisterA.Frames.Count * GetHeaderA().BlockAlign;
            GetHeaderA().ChunkSize = 36 + GetHeaderA().Subchunk2Size;
        }
    }
}
