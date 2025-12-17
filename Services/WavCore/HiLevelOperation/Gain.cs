using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio
{
    public partial class SoundRegister
    {


                /// <summary>
                ///     Zastosowanie stałego wzmocnienia (gain) na całym buforze stereo.
                ///     Gain = 1.0 → brak zmian
                ///     Gain < 1.0 → ściszenie
                ///     Gain > 1.0 → wzmocnienie
                /// </summary>
                /// <param name="gain">Współczynnik wzmocnienia</param>
                public void ApplyGain(float gain) {
            /*
                                int sampleCount = GetCurrentSampleCounter();

                                for (int i = 1; i <= sampleCount; i++)   {
                                    // Lewy kanał
                                    int left = GetLeftSampleValue(i);
                                    if (!IsErrorCode(left))
                                    {
                                        int scaled = (int)(left * gain);
                                        if (scaled > BufferPcm24.Max24Bit) scaled = BufferPcm24.Max24Bit;
                                        if (scaled < BufferPcm24.Min24Bit) scaled = BufferPcm24.Min24Bit;
                                        SetLeftSampleValue(i, scaled);
                                    }

                                    // Prawy kanał
                                    int right = GetRightSampleValue(i);
                                    if (!IsErrorCode(right))     {
                                        int scaled = (int)(right * gain);
                                        if (scaled > BufferPcm24.Max24Bit) scaled = BufferPcm24.Max24Bit;
                                        if (scaled < BufferPcm24.Min24Bit) scaled = BufferPcm24.Min24Bit;
                                        SetRightSampleValue(i, scaled);
                                    }
                                }
            */
        }



        /// <summary>
        ///     Oblicza maksymalny gain, który można zastosować
        ///     bez ryzyka przesterowania (clipping).
        /// </summary>
        /// <returns>Maksymalny bezpieczny współczynnik gain</returns>
        private float GetSafeGain()   {
/*return _gain;
                    int sampleCount = GetCurrentSampleCounter();
                    int maxSignal = 0;

                    for (int i = 1; i <= sampleCount; i++)  {
                        int left = GetLeftSampleValue(i);
                        if (!IsErrorCode(left))    {
                            int abs = Math.Abs(left);
                            if (abs > maxSignal) maxSignal = abs;
                        }

                        int right = GetRightSampleValue(i);
                        if (!IsErrorCode(right)) {
                            int abs = Math.Abs(right);
                            if (abs > maxSignal) maxSignal = abs;
                        }
                    }

                    if (maxSignal == 0) return float.PositiveInfinity;
                    return (float)BufferPcm24.Max24Bit / maxSignal;
*/return 1.0f;
        }
    }
}
