using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Models {
        public class Fragment
        {
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
            public int Length => EndIndex - StartIndex;

            public Fragment(int startIndex, int endIndex)
            {
                StartIndex = startIndex;
                EndIndex = endIndex;
            }

            public bool             Contains(int index)         => index >= StartIndex && index < EndIndex;
            public bool             Overlaps(Fragment other)    => !(other.EndIndex <= StartIndex || other.StartIndex >= EndIndex);
            public override string  ToString() => $"[{StartIndex}..{EndIndex})";
        }
}
