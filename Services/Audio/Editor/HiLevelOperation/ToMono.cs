using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio
{
    public partial class Editor {



        /// <summary>
        ///     Przerabia aktualny dźwięk stereo na mono poprzez uśrednienie wartości próbek lewego i prawego kanału.
        ///      wynik jest zapisywany nadal na obu kanałach - stereo ale z uśrednionymi wartością próbek.
        /// </summary>
        /// <returns>
        ///     zwraca ilość przerobionych próbek.
        /// </returns>
        public int ToMono( ) {

                int totalSamples    =   GetCurrentSampleCounter();
                int Lvalue          =   0;
                int Rvalue          =   0;
                int AverageValue    =   0;
                for ( int i = 1; i <= totalSamples; i++ ) {
                    Lvalue = GetLeftSampleValue(i);
                    Rvalue = GetRightSampleValue(i);
                    AverageValue = GetAverage( Lvalue, Rvalue );
                    SetLeftSampleValue( i, AverageValue );
                    SetRightSampleValue( i, AverageValue );
                }
                return totalSamples;
            }

            private int GetAverage( int valuex, int valuey ) => ( valuex + valuey ) / 2;
            
    }
}
