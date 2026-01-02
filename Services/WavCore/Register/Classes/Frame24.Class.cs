using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {

            
            public class Frame24 {
          
                public const int  MAX_VALUE   =  8388607;   // 2^23 - 1
                public Sample24   LChannel    { get; set; } = new Sample24();
                public Sample24   RChannel    { get; set; } = new Sample24();
                public Frame24( int leftValue = 0, int rightValue = 0 ) {
                        LChannel.Write( leftValue );
                        RChannel.Write( rightValue );
                }

                public int      Lvalue()      => LChannel.Read();
                public int      Rvalue()      => RChannel.Read();

                public void     Lvalue( int value )   => LChannel.Write( value );
                public void     Rvalue( int value )   => RChannel.Write( value );
            }
}
