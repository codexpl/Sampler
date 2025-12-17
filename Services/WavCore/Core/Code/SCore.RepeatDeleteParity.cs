using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Core {

        public void DeleteParity() {
            for (int i = RegisterA.Frames.Count - 1; i >= 0; i--)  
                if (i % 2 == 0)  {
                    RegisterA.Frames.RemoveAt(i);
                    GetHeaderA().Subchunk2Size = RegisterA.Frames.Count * GetHeaderA().BlockAlign;
                    GetHeaderA().ChunkSize = 36 + GetHeaderA().Subchunk2Size;
                }
        }

        public void RepeateParity() {
            List<Frame24> newFrames = new List<Frame24>();
            foreach (var frame in RegisterA.Frames) {
                newFrames.Add(frame);
                newFrames.Add(frame);
            }
            RegisterA.Frames = newFrames;
            GetHeaderA().Subchunk2Size = RegisterA.Frames.Count * GetHeaderA().BlockAlign;
            GetHeaderA().ChunkSize = 36 + GetHeaderA().Subchunk2Size;
        }
    }
}
