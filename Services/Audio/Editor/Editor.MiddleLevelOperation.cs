using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Editor {
        
            public int GetLeftSampleValue(int sampleNr)                 => GetSampleValue(sampleNr, LChannel );
            public int GetRightSampleValue( int sampleNr )              => GetSampleValue(sampleNr, RChannel );
            public int SetLeftSampleValue( int sampleNr, int value )    => SetSampleValue( value, sampleNr, LChannel );
            public int SetRightSampleValue ( int sampleNr, int value )  => SetSampleValue( value, sampleNr, RChannel );


    }
}
