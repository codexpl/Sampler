using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Core {

            public  int ScanAndEdit( ) {
                    int  changedCounter = 0;
                    for( int i = 0; i < RegisterA.LengthInFrames(); i++ ) {
                            Frame24 frame = RegisterA.GetFrame(i);
                            if ( HasConditions( frame ) )  {
                                frame = Modyficate(  frame );
                                RegisterA.SetFrame( frame, i );
                                changedCounter++;
                            }
                    }
                    return changedCounter;
            }

            public bool HasConditions( Frame24 frame  ) {
                int leftValue  = frame.Lvalue();
                int rightValue = frame.Rvalue();
                if( leftValue != 0 && rightValue != 0 ) return true;
                return false;
            }

            public Frame24 Modyficate ( Frame24 inFrame ) {
                Frame24 outFrame = inFrame;
                return outFrame;
            }
    }
}
