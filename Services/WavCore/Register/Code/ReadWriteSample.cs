using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Register {

                public int      GetLeftSamleValue( int index ) {
                    if( index < 0 || index >= Frames.Count )   throw new IndexOutOfRangeException("Frame index out of range.");
                    Frame24 frame = Frames[index];
                    return frame.Lvalue();
                }

                public void     SetLeftSampleValue( int index, int value ) {
                    if( index < 0 || index >= Frames.Count )   throw new IndexOutOfRangeException("Frame index out of range.");
                    Frame24 frame = Frames[index];
                    frame.Lvalue( value );
                }

                public int      GetRightSampleValue( int index ) {
                    if( index < 0 || index >= Frames.Count )   throw new IndexOutOfRangeException("Frame index out of range.");
                    Frame24 frame = Frames[index];
                    return frame.Rvalue();
                }

                public void     SetRightSampleValue( int index, int value ) {
                    if( index < 0 || index >= Frames.Count )   throw new IndexOutOfRangeException("Frame index out of range.");
                    Frame24 frame = Frames[index];
                    frame.Rvalue( value );
                }

                public Frame24  GetFrame( int index ) {
                    if( index < 0 || index >= Frames.Count )   throw new IndexOutOfRangeException("Frame index out of range.");
                    return Frames[index];
                }

                public void     SetFrame( Frame24 frame, int index ) {
                    if( frame == null ) throw new ArgumentNullException(nameof(frame));
                    Frames[index] = frame;
                }
             
    }
}
