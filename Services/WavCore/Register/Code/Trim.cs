using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Register {




        public bool LTrim( int lastFrameIndex ) {
            if( lastFrameIndex < 0 || lastFrameIndex > Frames.Count )   throw new IndexOutOfRangeException("Frame index out of range."); return false;
            int totalFrames = Frames.Count;
            int framesToKeep = lastFrameIndex;
            int framesToRemove = totalFrames - framesToKeep;
            // Update Frames list
            Frames = Frames.Take( framesToKeep ).ToList();
            this.HeaderUpdate();
            return true;
        }
    }
}
