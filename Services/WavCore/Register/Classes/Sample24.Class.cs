using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
            public class Sample24 {
                private byte[]      _data   { get; set; } = new byte[3];

                public Sample24( int value = 0 )  => Write( value );


                public void     Write( int value ) {
                        _data[0] = (byte)(value & 0xFF);
                        _data[1] = (byte)(value >> 8 & 0xFF);
                        _data[2] = (byte)(value >> 16 & 0xFF);
                }

                public int      Read() {
                        int value = _data[0] | _data[1] << 8 | _data[2] << 16;
                        if ((value & 0x800000) != 0)   { value |= unchecked((int)0xFF000000); }
                        return value;
                }
            }
}
