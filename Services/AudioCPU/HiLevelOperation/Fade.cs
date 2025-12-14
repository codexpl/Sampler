using Sampler.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio
{
    public partial class SoundRegister
    {

            public void ApplyFade(bool fadeIn)
            {
            /*
                            int sampleRate = Header.SampleRate;
                            int totalSamples = Buffer.Bytes.Length / 6; // stereo, 3 bajty na kanał

                            for (int i = 0; i < totalSamples; i++)
                            {
                                // współczynnik rosnący (Fade In) lub malejący (Fade Out)
                                float factor = fadeIn
                                    ? (float)i / totalSamples
                                    : 1f - (float)i / totalSamples;

                                int offset = i * 6;

                                // odczyt próbek
                                int left  = Buffer.Read24Bit(offset);
                                int right = Buffer.Read24Bit(offset + 3);

                                // zapis z uwzględnieniem współczynnika
                                Buffer.Write24Bit(offset,     (int)(left  * factor));
                                Buffer.Write24Bit(offset + 3, (int)(right * factor));
                            }
            */
        }

    }
}
