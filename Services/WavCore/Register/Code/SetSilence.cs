using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Register {

                private void SetSilence( int lengthInFrames) {
                    this.Header = WaveHeaderParser.GetWaveHeader(lengthInFrames * Header.BlockAlign);
                    this.Frames = new List<Frame24>( lengthInFrames );
                    for( int i = 0; i < lengthInFrames; i++ ) {
                        this.Frames.Add( new Frame24( 0, 0 ) );
                    }
                    this.HeaderUpdate();
                }
    }
}
