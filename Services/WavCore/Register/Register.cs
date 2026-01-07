using Sampler.Services.WavCore.Register.Classes.Pattern;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sampler.Services.Audio {



        public partial class Register{

        

                public bool             IsLoaded()              => Frames.Count > 0 && Header.IsValid();
                public WaveHeader       Header   { get; set; }  = new WaveHeader();
                public List<Frame24>    Frames   { get; set; }  = new List<Frame24>();


                public Register() =>    SetSilence( WaveHeader.SAMPLE_RATE_DEFAULT );


                public Register( byte[] wavFileBytes) { if( !ImportWavFile( wavFileBytes ) )  SetSilence( WaveHeader.SAMPLE_RATE_DEFAULT ) ; }


                public Register(Register other)
                {
                    Header = new WaveHeader(other.Header);

                    Frames = new List<Frame24>(other.Frames.Count);
                    foreach (var f in other.Frames)
                        Frames.Add(new Frame24(f));

                    HeaderUpdate();
                }




                public int              LastIndex()          => Frames.Count - 1;   // ostatni dostepny index

                public int              LengthInFrames()     => Header.Subchunk2Size / Header.BlockAlign;
                public float            LengthInSeconds()    => (float)LengthInFrames() / Header.SampleRate;


                // wykonać po każdej zmianie Frames / przed każdym eksportem
                public void             HeaderUpdate() {
                            Header.Subchunk2Size = Frames.Count * Header.BlockAlign;
                            Header.ChunkSize = 36 + Header.Subchunk2Size;
                        }

        }

}
