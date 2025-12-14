using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class AudioRegister {



                private bool    ImportHeader( byte[] wavFileBytes ) {
                    WaveHeader header = WaveHeaderParser.Parse( wavFileBytes );
                    if( !header.IsValid() )   return false;
                    this.Header = header;
                    return true;
                }


                private void    ImportFrames( byte[] wavFileBytes ) {
                    int dataStartIndex = wavFileBytes.Length - Header.Subchunk2Size;
                    int totalFrames = Header.Subchunk2Size / Header.BlockAlign;
                    Frames = new List<Frame24>( totalFrames );
                    for( int i = 0; i < totalFrames; i++ ) {
                        int frameStart = dataStartIndex + i * Header.BlockAlign;
                        int leftSample = BitConverter.ToInt32( new byte[] { wavFileBytes[frameStart], wavFileBytes[frameStart + 1], wavFileBytes[frameStart + 2], 0x00 }, 0 );
                        int rightSample = BitConverter.ToInt32( new byte[] { wavFileBytes[frameStart + 3], wavFileBytes[frameStart + 4], wavFileBytes[frameStart + 5], 0x00 }, 0 );
                        Frames.Add( new Frame24( leftSample, rightSample ) );
                    }
                }
    }
}
