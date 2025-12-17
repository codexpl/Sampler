using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {



        public partial class Register{

        

                
                public WaveHeader       Header   { get; set; } = new WaveHeader();
                public List<Frame24>    Frames  { get; set; } = new List<Frame24>();


                public Register() {
                    this.Header = WaveHeaderParser.GetWaveHeader(0);      // default header with 0 data size
                    this.Frames = new List<Frame24>();                    // empty frame list
                }


                public Register( byte[] wavFileBytes): this() { ImportWavFile( wavFileBytes ); }


                public int              LengthInFrames()     => Header.Subchunk2Size / Header.BlockAlign;
                public float            LengthInSeconds()    => (float)LengthInFrames() / Header.SampleRate;


                // wykonać po każdej zmianie Frames
                private void            HeaderUpdate() {
                            Header.Subchunk2Size = Frames.Count * Header.BlockAlign;
                            Header.ChunkSize = 36 + Header.Subchunk2Size;
                        }

    }

}
