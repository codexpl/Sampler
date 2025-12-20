using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public static class MathPercent {


        public static float ToPercent(this int value, int total) {
            if (total == 0) return 0;
            return (float)value / total * 100;
        }
    }
}
