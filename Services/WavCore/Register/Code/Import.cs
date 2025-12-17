using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Register {


                private bool    ImportWavFile( byte[] wavFileBytes ) {

                    if( wavFileBytes == null || wavFileBytes.Length < 44 )   return false;   //     wstepne sprawdzenie poprawnosci pliku wav
                    WaveHeader tmpHeader = WaveHeaderParser.Parse( wavFileBytes );           //     tu myk sprawdzajacy na fałszywym headerze zanim zostanie nadpisany prawdziwy
                    if( !tmpHeader.IsValid() )    return false;                              //     jezeli nieprawidłowy plik wav -> wyjscie .
                    Header = tmpHeader;                                                      //     nadpisanie prawdziwego headera. w  tym momencie wiadomo że wav jest poprawny


                    int dataStartIndex = wavFileBytes.Length - Header.Subchunk2Size;
                    int totalFrames = Header.Subchunk2Size / Header.BlockAlign;
                    Frames = new List<Frame24>( totalFrames );
                    for( int i = 0; i < totalFrames; i++ ) {
                        int frameStart = dataStartIndex + i * Header.BlockAlign;
                        int leftSample = BitConverter.ToInt32( new byte[] { wavFileBytes[frameStart], wavFileBytes[frameStart + 1], wavFileBytes[frameStart + 2], 0x00 }, 0 );
                        int rightSample = BitConverter.ToInt32( new byte[] { wavFileBytes[frameStart + 3], wavFileBytes[frameStart + 4], wavFileBytes[frameStart + 5], 0x00 }, 0 );
                        Frames.Add( new Frame24( leftSample, rightSample ) );
                    }
                    return true;
                }
    }
}
