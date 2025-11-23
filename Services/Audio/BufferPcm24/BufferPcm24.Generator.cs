using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio.BufferPcm24 {
    public partial class BufferPcm24 {



            public void CreateZigzag(int samples)  {
                int totalBytes = samples * 3 * 2; // 3 bytes per sample, 2 channels
                _buffer = new byte[totalBytes];

                int maxAmplitude = 8388607;
                int minAmplitude = -8388608;
                int amplitude = minAmplitude;
                int step = maxAmplitude >> 4; // np. 524288

                bool ascending = true;

                for (int i = 0; i < samples; i++)  {
                    int offset = i * 6;

                    // Zapisz do obu kanałów
                    Write24Bit(offset, amplitude);       // Left
                    Write24Bit(offset + 3, amplitude);   // Right

                    // Zigzag: zmiana kierunku przy granicy
                    if (ascending)  {
                        amplitude += step;
                        if (amplitude >= maxAmplitude)  {
                            amplitude = maxAmplitude;
                            ascending = false;
                        }
                    }
                    else {
                        amplitude -= step;
                        if (amplitude <= minAmplitude)  {
                            amplitude = minAmplitude;
                            ascending = true;
                        }
                    }
                }
            }


            public void SineTest(int sampleCount, double frequency)
            {
                int sampleRate = _waveFormat.SampleRate;
                int totalBytes = sampleCount * 3 * 2; // 3 bajty na próbkę, 2 kanały
                _buffer = new byte[totalBytes];

                double amplitude = 8388607; // max dla 24-bit signed
                double twoPiF = 2 * Math.PI * frequency;

                for (int i = 0; i < sampleCount; i++)
                {
                    double t = (double)i / sampleRate;
                    int leftValue  = (int)(Math.Sin(twoPiF * t) * amplitude);   // Lewy kanał
                    int rightValue = (int)(-Math.Sin(twoPiF * t) * amplitude);  // Prawy kanał (anty-faza)

                    int offset = i * 6;
                    Write24Bit(offset, leftValue);       // Left
                    Write24Bit(offset + 3, rightValue);  // Right
                }
            }

            public void CreateSineWave(int samples, double frequency)
            {
                    int sampleRate = _waveFormat.SampleRate;
                    int totalBytes = samples * 3 * 2; // 3 bytes per sample, 2 channels
                    _buffer = new byte[totalBytes];

                    double amplitude = 8388607; // max for 24-bit signed
                    double twoPiF = 2 * Math.PI * frequency;

                    for (int i = 0; i < samples; i++)   {
                        double t = (double)i / sampleRate;
                        int sampleValue = (int)(Math.Sin(twoPiF * t) * amplitude);

                        int offset = i * 6;
                        Write24Bit(offset, sampleValue);       // Left
                        Write24Bit(offset + 3, sampleValue);   // Right
                    }
            }



            public void CreateSineWaveWithPhase(int samples, double frequency, double phaseRadians)
            {
                int sampleRate = _waveFormat.SampleRate;
                int totalBytes = samples * 3 * 2; // 3 bytes per sample, 2 channels
                _buffer = new byte[totalBytes];

                double amplitude = 8388607; // max for 24-bit signed
                double twoPiF = 2 * Math.PI * frequency;

                for (int i = 0; i < samples; i++)
                {
                    double t = (double)i / sampleRate;
                    double angle = twoPiF * t + phaseRadians;
                    int sampleValue = (int)(Math.Sin(angle) * amplitude);

                    int offset = i * 6;
                    Write24Bit(offset, sampleValue);       // Left
                    Write24Bit(offset + 3, sampleValue);   // Right
                }
            }



            public void CreatePulseTrain(int samples, int interval)  {
                int totalBytes = samples * 3 * 2; // 3 bytes per sample, 2 channels
                _buffer = new byte[totalBytes];

                int maxAmplitude = 8388607;

                for (int i = 0; i < samples; i++) {
                    int offset = i * 6;

                    if (i % interval == 0) {
                        // Impuls w obu kanałach
                        Write24Bit(offset, maxAmplitude);       // Left
                        Write24Bit(offset + 3, maxAmplitude);   // Right
                    }
                    else  {
                        // Cisza
                        Write24Bit(offset, 0);
                        Write24Bit(offset + 3, 0);
                    }
                }
            }

    }
}
