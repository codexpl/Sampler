using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class AudioRegister {

                private byte[]  ExportHeader() => WaveHeaderParser.Serialize( this.Header );
        
                private byte[]  ExportFrames() {
                    byte[] audioData = new byte[ Header.Subchunk2Size ];
                    for( int i = 0; i < Frames.Count; i++ ) {
                        int frameStart = i * Header.BlockAlign;
                        Frame24 frame = Frames[i];
                        audioData[frameStart]     = (byte)( frame.Lvalue()        & 0xFF );
                        audioData[frameStart + 1] = (byte)((frame.Lvalue() >> 8)  & 0xFF );
                        audioData[frameStart + 2] = (byte)((frame.Lvalue() >> 16) & 0xFF );
                        audioData[frameStart + 3] = (byte)( frame.Rvalue()        & 0xFF );
                        audioData[frameStart + 4] = (byte)((frame.Rvalue() >> 8)  & 0xFF );
                        audioData[frameStart + 5] = (byte)((frame.Rvalue() >> 16) & 0xFF );
                    }
                    return audioData;
            }

                public byte[]   ExportWavFile() {
                    byte[] headerBytes = ExportHeader();
                    byte[] frameBytes = ExportFrames();
                    byte[] wavFileBytes = new byte[ headerBytes.Length + frameBytes.Length ];
                    Array.Copy( headerBytes, 0, wavFileBytes, 0, headerBytes.Length );
                    Array.Copy( frameBytes, 0, wavFileBytes, headerBytes.Length, frameBytes.Length );
                    return wavFileBytes;
            }
    }
}
