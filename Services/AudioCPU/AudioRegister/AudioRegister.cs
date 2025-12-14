using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {



        public partial class AudioRegister{

        

                public WaveHeader       Header   { get; set; } = new WaveHeader();
                public List<Frame24>    Frames  { get; set; } = new List<Frame24>();

                public int              LengthInFrames()     => Header.Subchunk2Size / Header.BlockAlign;
                public float            LengthInSeconds()    => (float)LengthInFrames() / Header.SampleRate;


                public AudioRegister() {
                    this.Header = WaveHeaderParser.GetWaveHeader(0);
                    this.Frames = new List<Frame24>();
                }

                public AudioRegister( byte[] wavFileBytes) {
                            if(  !ImportHeader( wavFileBytes ) )    throw new ArgumentException("Invalid WAV file data.");           
                            ImportFrames( wavFileBytes );
                            if( Frames.Count != LengthInFrames() )  throw new InvalidOperationException("Frame count does not match header information.");
                    }
            }

}
