using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {


    public struct PatternMatchResult {
        public int      StartFrame { get; }
        public float    Score { get; }
        public PatternMatchResult(int startFrame, float score)   {
                StartFrame = startFrame;
                Score = score;
        }
    }
}
