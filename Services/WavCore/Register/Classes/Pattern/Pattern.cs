using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.WavCore.Register.Classes.Pattern {
    public class Pattern
    {
        /// <summary>
        /// Znormalizowany sygnał wzorca (np. 128 punktów, zakres -1..1).
        /// </summary>
        public float[]  Normalized { get; }

        /// <summary>
        /// Gradient sygnału (różnice kolejnych punktów, długość Normalized.Length - 1).
        /// </summary>
        public float[]  Gradient { get; }

        /// <summary>
        /// Oryginalna długość wzorca w próbkach (przed resamplingiem).
        /// </summary>
        public int      OriginalLength { get; }

        /// <summary>
        /// Opcjonalna nazwa wzorca (np. "klik", "szlaczek", "uderzenie").
        /// </summary>
        public string?  Name { get; }

        public Pattern(float[] normalized, float[] gradient, int originalLength, string? name = null)
        {
            Normalized      = normalized;
            Gradient        = gradient;
            OriginalLength  = originalLength;
            Name            = name;
        }
    }

}

