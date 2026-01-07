using Sampler.Services.WavCore.Register.Classes.Pattern;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio
{
    public static class PatternExtractor
    {
        private const int TARGET_POINTS = 128;

        public static Pattern FromFrames(List<Frame24> frames, int sampleRate, string? name = null)
        {
            // 1. Zamiana na floaty (-1..1)
            float[] raw = FramesToFloat(frames);

            // 2. Usunięcie DC offset
            float[] dc = RemoveDC(raw);

            // 3. Normalizacja amplitudy
            float[] norm = NormalizeAmplitude(dc);

            // 4. Resampling do stałej długości
            float[] resampled = Resample(norm, TARGET_POINTS);

            // 5. Gradient
            float[] gradient = ComputeGradient(resampled);

            return new Pattern(resampled, gradient, raw.Length, name);
        }



        public static float[] FramesToFloat(List<Frame24> frames)   {
            float max = Frame24.MAX_VALUE; // Twój 24-bit max
            float[] arr = new float[frames.Count];
            int  average    =   0;
            for (int i = 0; i < frames.Count; i++)     {
                    average = (frames[i].Lvalue() + frames[i].Rvalue())/ 2;
                    arr[i] = average / max;
            }
            return arr;
        }

        public static float[] RemoveDC(float[] data)  {
            float avg = data.Average();
            float[] result = new float[data.Length];
            for (int i = 0; i < data.Length; i++)    result[i] = data[i] - avg;
            return result;
        }

        public static float[] NormalizeAmplitude(float[] data)  {
            float max = data.Max(x => Math.Abs(x));
            if (max == 0) return data;
            float[] result = new float[data.Length];
            for (int i = 0; i < data.Length; i++)   result[i] = data[i] / max;
            return result;
        }

        public static float[] Resample(float[] data, int target)  {
            float[] result = new float[target];
            float step = (float)(data.Length - 1) / (target - 1);
            for (int i = 0; i < target; i++)
            {
                float pos = i * step;
                int p0 = (int)pos;
                int p1 = Math.Min(p0 + 1, data.Length - 1);
                float t = pos - p0;

                result[i] = data[p0] * (1 - t) + data[p1] * t;
            }
            return result;
        }

        public static float[] ComputeGradient(float[] data)  {
            float[] grad = new float[data.Length - 1];
            for (int i = 0; i < grad.Length; i++)  grad[i] = data[i + 1] - data[i];
            return grad;
        }

        // FUNKCJE ODWROTNE  NA BAZIE TYCH CO POWYŻEJ LUB ICH WYNIKÓW 
        public static List<Frame24> FloatToFrames(float[] data)   {
            float max = Frame24.MAX_VALUE;
            var frames = new List<Frame24>(data.Length);

            for (int i = 0; i < data.Length; i++)    {
                int sample = (int)(data[i] * max);
                frames.Add(new Frame24(sample, sample)); // stereo L=R
            }
            return frames;
        }


    }
}
