using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Register {

                private void   Clear() {
                    this.Header = WaveHeaderParser.GetWaveHeader(0);      // default header with 0 data size
                    this.Frames.Clear();                                  // empty frame list
                    this.HeaderUpdate();
                }
    }
}
