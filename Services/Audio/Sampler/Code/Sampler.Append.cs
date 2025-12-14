using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Sampler {


        public void Append() {
            /*
                        int lengthA = RegisterA.Buffer.Bytes.Length;
                        int lengthB = RegisterB.Buffer.Bytes.Length;
                        byte[] newBuffer = new byte[lengthA + lengthB];
                        Array.Copy(RegisterA.Buffer.Bytes, 0, newBuffer, 0, lengthA);
                        Array.Copy(RegisterB.Buffer.Bytes, 0, newBuffer, lengthA, lengthB);
                        RegisterA.Buffer = new BufferPcm24(newBuffer);

                        RegisterA.Header.ChunkSize       =   36 + newBuffer.Length;
                        RegisterA.Header.Subchunk2Size   =   newBuffer.Length;
            */
        }




    }
}
