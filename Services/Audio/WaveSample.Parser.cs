using Sampler.Services.Audio;


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {

    public partial class WaveSampler {



                public byte[]       ToWaveFile24()
                {
                    // Aktualizacja rozmiarów
                    Header.Subchunk1Size = 16; // PCM
                    Header.Subchunk2Size = Edit.Buffer.Bytes.Length;
                    Header.ChunkSize = 36 + Header.Subchunk2Size;

                    using (var ms = new MemoryStream())
                    using (var bw = new BinaryWriter(ms))
                    {
                        // RIFF Header
                        bw.Write(Encoding.ASCII.GetBytes("RIFF"));
                        bw.Write(Header.ChunkSize);
                        bw.Write(Encoding.ASCII.GetBytes("WAVE"));

                        // fmt subchunk
                        bw.Write(Encoding.ASCII.GetBytes("fmt "));
                        bw.Write(Header.Subchunk1Size);
                        bw.Write((ushort)Header.FormatAudio);   // PCM = 1
                        bw.Write((short)Header.NumChannels);
                        bw.Write(Header.SampleRate);
                        bw.Write(Header.ByteRate);
                        bw.Write((short)Header.BlockAlign);
                        bw.Write((short)Header.BitsPerSample);

                        // data subchunk
                        bw.Write(Encoding.ASCII.GetBytes("data"));
                        bw.Write(Header.Subchunk2Size);
                        bw.Write(Edit.Buffer.Bytes);
                        return ms.ToArray();
                    }
                }
    }
}
