using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {


    public  enum ObjectStatus : uint {
            SUCCESS     =   0b0000_0000,
            WARNING     =   0b0000_0001,
            ERROR       =   0b0000_0010,
            UNKNOWN     =   0b0000_0100
    }
}
