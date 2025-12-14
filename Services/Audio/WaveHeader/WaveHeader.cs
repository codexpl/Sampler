using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class WaveHeader  {

                public const    int     NUM_CHANNELS_DEFAULT      = 2;     // stereo
                public const    int     SAMPLE_RATE_DEFAULT       = 44100; // Hz
                public const    int     BYTE_RATE_DEFAULT         = 264600; // bajtów/s
                public const    int     BLOCK_ALIGN_DEFAULT       = 6;     // bajtów na ramkę
                public const    short   BITS_PER_SAMPLE_DEFAULT   = 24;    // 24-bit


                public WaveHeader(int framesAudio = 41000)  => SetSize( framesAudio = 0 );



                public void SetSize(int sizeInFramesAudio = 0 )  {


                int blockAlign    = NUM_CHANNELS_DEFAULT * BITS_PER_SAMPLE_DEFAULT / 8; // 6 bajtów na ramkę
                int byteRate      = SAMPLE_RATE_DEFAULT * blockAlign;         // 264600 bajtów/s
                int subchunk2Size = sizeInFramesAudio * blockAlign;
                int chunkSize     = 36 + subchunk2Size;

                    ChunkID       = "RIFF";
                    ChunkSize     = chunkSize;
                    Format        = "WAVE";
                    Subchunk1ID   = "fmt ";
                    Subchunk1Size = 16;              // PCM
                    FormatAudio   = 1;               // PCM
                    NumChannels   = (short)NUM_CHANNELS_DEFAULT;
                    SampleRate    = SAMPLE_RATE_DEFAULT;
                    ByteRate      = 264600;
                    BlockAlign    = 6;
                    BitsPerSample = (short)BITS_PER_SAMPLE_DEFAULT;
                    Subchunk2ID   = "data";
                    Subchunk2Size = subchunk2Size;

                }

    }
}
