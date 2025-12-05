using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Sampler.Services.Audio
{

    public partial class Editor  {
        public void CreateSineWave( int amplitude, int duration )
        {
              
        }
    }
}

/*
public float[] GenerateSineWave(int sampleRate, double frequency, double durationSeconds)
{
    int sampleCount = (int)(sampleRate * durationSeconds);
    float[] buffer = new float[sampleCount];

    for (int n = 0; n < sampleCount; n++)
    {
        buffer[n] = (float)Math.Sin(2 * Math.PI * frequency * n / sampleRate);
    }

    return buffer;
}
*/