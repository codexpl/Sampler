using CSCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Sampler.Services.Audio
{

    public partial class SoundRegister  {
        public void CreateSineWave( int frequency, int samples ) {
            /*
                                int sampleRate = Header.SampleRate;
                                Header.SetSize( samples );
                                SetBufferSize( samples );

                                double amplitude = 8388607; // max value for 24 bit
                                double twoPiF = 2 * Math.PI * frequency;

                                for (int i = 0; i < samples; i++)   {
                                    double t = (double)i / sampleRate;
                                    int sampleValue = (int)(Math.Sin(twoPiF * t) * amplitude);

                                    int offset = i * 6;
                                    Buffer.Write24Bit(offset, sampleValue);       // Left
                                    Buffer.Write24Bit(offset + 3, sampleValue);   // Right
                                }
            */
        }
    }
}
