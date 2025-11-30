using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {

    
    public partial class Editor {

            public const int    LChannel                =   0;
            public const int    RChannel                =   1;


            private const int StartPointLeftOffset      =   -6; // dla 24-bit stereo
            private const int StartPointRightOffset     =   -3;






            /// <summary>
            ///     zakresy kodu błędów uzywane do odrozniania zwracanych wartości 
            ///         czy sa danymi czy kodami błędów 
            /// </summary>
            public const int MinErrorCode               =   -9;
            public const int MaxErrorCode               =   -1;


            /// <summary>
            ///     Success                   - brak błędu   
            /// </summary>
            public const int Success                    =   0;

            /// <summary>
            /// Error – ogólny błąd (niezdefiniowany, niesklasyfikowany).
            /// </summary>
            public const int Error                      =   -1;

            /// <summary>
            ///     OutOfRangeError     – podany numer próbki nie istnieje
            ///     
            /// </summary>
            public const int OutOfRangeError            =   -2;     

            /// <summary>
            ///     InvalidChannelError – podany kanał jest spoza zakresu 
            ///     
            /// </summary>
            public const int InvalidChannelError        =   -3;

            /// <summary>
            ///     NullBufferError     – brak danych w buforze (Buffer == null lub Buffer.Bytes == null)
            ///     
            /// </summary>
            public const int NullBufferError            =   -4;

            /// <summary>
            ///     HeaderError         – niepoprawny lub niepełny nagłówek WaveHeader
            ///     
            /// </summary>
            public const int HeaderError                =   -5;

            /// <summary>
            ///     Zarezerwowane kody błędu na przyszłe potrzeby .
            /// </summary>
            public const int ReservedForFutureA          =   -6;

            public const int ReservedForFutureB          =   -7;

            public const int ReservedForFutureC          =   -8;

            /// <summary>
            ///     wartosc graniczna możliwości programu - danych wejsciowych nie może być więcej niż 
            ///       int.MaxValue 
            /// </summary>
            public const int MaximumSampleSizeLimit     =    -9;

    }
}
