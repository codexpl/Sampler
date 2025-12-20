using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Register {




        public void LTrim( int framesCount ) {
            List<Frame24> newFrames = Frames.Take( framesCount ).ToList();
            Frames = newFrames;
            this.HeaderUpdate();
        }


        public void  RTrim( int framesCount ) {
            int startIndex = Frames.Count - framesCount;
            List<Frame24> newFrames = Frames.Skip(startIndex ).ToList();
            Frames = newFrames;
            this.HeaderUpdate();
        }


        public void Trim( int startIndex, int size ) {
            List<Frame24> newFrames = Frames.Skip( startIndex ).Take( size ).ToList();
            Frames = newFrames;
            this.HeaderUpdate();
        }
    }
}
