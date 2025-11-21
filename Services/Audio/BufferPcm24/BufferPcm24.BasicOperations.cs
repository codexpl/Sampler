using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio.BufferPcm24 {
    public partial class BufferPcm24 {



            private int     Read24Bit(int offset) {
                int value = this._buffer[offset] | (this._buffer[offset + 1] << 8) | (this._buffer[offset + 2] << 16);
                if ((value & 0x800000) != 0)   { value |= unchecked((int)0xFF000000); }
                return value;
            }


            private void    Write24Bit( int offset, int value ) {
                this._buffer[offset]     = (byte)(value & 0xFF);
                this._buffer[offset + 1] = (byte)((value >> 8) & 0xFF);
                this._buffer[offset + 2] = (byte)((value >> 16) & 0xFF);
            }


            private void    invert24Bit( int offset ) {
                int value = Read24Bit( offset );
                int inverted = -value;
                Write24Bit( offset, inverted );
            }
    }
}
