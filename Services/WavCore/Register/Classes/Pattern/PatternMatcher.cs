using Sampler.Services.WavCore.Register.Classes.Pattern;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio
{




        public static class PatternMatcher    {
                private const int TARGET_POINTS = 128;

                /// <summary>
                /// Cosine similarity [-1..1]
                /// </summary>
                public static float Similarity(float[] a, float[] b)   {
                    if (a.Length != b.Length)     throw new ArgumentException("Vectors must have the same length.");

                    float dot = 0;
                    float lenA = 0;
                    float lenB = 0;

                    for (int i = 0; i < a.Length; i++)     {
                        dot  += a[i] * b[i];
                        lenA += a[i] * a[i];
                        lenB += b[i] * b[i];
                    }

                    float denom = MathF.Sqrt(lenA) * MathF.Sqrt(lenB);
                    if (denom == 0)     return 0;
                    return dot / denom;
                }


                /// <summary>
                /// Wyszukuje wzorzec w rejestrze.
                /// </summary>
                public static List<PatternMatchResult> FindMatches( Register register, Pattern pattern, int windowFrames, float minScore = 0.8f, int stepFrames = 1) {
            var results = new List<PatternMatchResult>();

            if (windowFrames <= 0 || register.Frames.Count < windowFrames)  return results;

            for ( int start = 0; start <= register.Frames.Count - windowFrames; start += stepFrames )    {
                // 1. Wytnij okno
                var slice = register.Frames
                    .Skip(start)
                    .Take(windowFrames)
                    .ToList();

                // 2. Pipeline DSP (ten sam co PatternExtractor)
                float[] raw       = PatternExtractor.FramesToFloat(slice);
                float[] dc        = PatternExtractor.RemoveDC(raw);
                float[] norm      = PatternExtractor.NormalizeAmplitude(dc);
                float[] resampled = PatternExtractor.Resample(norm, TARGET_POINTS);
                float[] gradient  = PatternExtractor.ComputeGradient(resampled);

                // 3. Porównanie gradientów
                float score = Similarity(pattern.Gradient, gradient);
                if (score >= minScore) results.Add(new PatternMatchResult(start, score));
            }

            return results;
        }


        }


        public static class RegisterPatternExtensions {
                public static List<PatternMatchResult> FindPattern ( this Register register, Pattern pattern, int windowFrames, float minScore = 0.8f, int stepFrames = 1) {
                        return PatternMatcher.FindMatches(register, pattern, windowFrames, minScore, stepFrames);
                    }
        }

        public static class RegisterEditExtensions {
            public static void ReplaceFrames( this Register register, int startFrame, List<Frame24> newFrames ) {
                for (int i = 0; i < newFrames.Count; i++)  {
                    int idx = startFrame + i;
                    if (idx >= register.Frames.Count) break;
                    register.Frames[idx] = newFrames[i];
                }
            }
        }



}
