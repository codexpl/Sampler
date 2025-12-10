using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class Editor {


            public void     ClearBuffer()   =>  Buffer = new BufferPcm24( new byte[0] );
            public void     CreateBuffer( int sizeInSamples )  {
                    int  bufferSize = sizeInSamples * _SIZEOF_SAMPLE * Header.NumChannels;
                    Buffer = new BufferPcm24( new byte[ bufferSize ] );
                    for ( int i = 0; i < bufferSize; i++ )  Buffer.Bytes[i] = 0;
            }   





            /// <summary>
            ///     zwraca aktualna ilośc probek w Buffer.
            /// </summary>
            /// <returns></returns>
            public  int     GetCurrentSampleCounter() =>  Buffer.Bytes.Length / ( this._SIZEOF_SAMPLE * Header.NumChannels ); 


            /// <summary>
            ///     sprawdzanie czy istnieje probka o podanym numerze 
            /// </summary>
            /// <param name="sampleNr"></param>
            /// <returns></returns>
            public  bool    IsInRange( int sampleNr )   =>   ( (sampleNr > 0) && ( sampleNr <= GetCurrentSampleCounter() ) )?true:false; 


            /// <summary>
            ///     sprawqdzenie czy podana wartasc jest kodem błędu .
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public bool     IsErrorCode(int value) => value >= MinErrorCode && value <= MaxErrorCode;



            /// <summary>
            ///     zwraca bazowy index probki .
            ///     przed użyciem - musi być sprawdzenie za pomocą IsRange(int) !!!
            /// </summary>
            /// <param name="sampleNr"></param>
            /// <returns></returns>
            private int     _getStartPoint(int sampleNr)  => (sampleNr - 1) * Header.NumChannels * _SIZEOF_SAMPLE;
    



            /// <summary>
            ///         Konwersja podanego numeru próbki na jej indeks w buforze.
            /// </summary>
            /// <param name="sampleNr">Numer próbki (indeksowany od 1).</param>
            /// <param name="channel">Kanał (np. LChannel / RChannel).</param>
            /// <returns>
            /// >= 0: indeks próbki w buforze
            /// -1: OutOfRangeError – podany numer próbki nie istnieje
            /// -2: InvalidChannelError – kanał spoza zakresu
            /// -3: NullBufferError – brak danych w buforze
            /// -4: HeaderError – niepoprawny nagłówek
            /// </returns>
            public int SampleNrToIndex(int sampleNr, int channel = LChannel)
            {
                if ( Buffer == null || Buffer.Bytes == null)                                                return NullBufferError;

                if (Header == null || Header.NumChannels <= 0 )                                             return HeaderError;

                if (!IsInRange(sampleNr))                                                                   return OutOfRangeError;

                if (channel < LChannel || channel > RChannel)                                               return InvalidChannelError;

                int startPoint = _getStartPoint(sampleNr);

                if( channel == LChannel )   return startPoint + StartPointLeftOffset;
                                            return startPoint + StartPointRightOffset;
            }



            public int GetSampleValue(int sampleNr, int channel = LChannel )  {
                int index = SampleNrToIndex(sampleNr, channel);
                if ( IsErrorCode(index) ) {  this.IsSuccess = false; return index;  }
                IsSuccess = true;
                return this.Buffer.Read24Bit(index);
            }


            public int SetSampleValue( int newValue, int sampleNr, int channel = LChannel ) {
                int index = SampleNrToIndex( sampleNr, channel);
                if( IsErrorCode(index)) { this.IsSuccess = false;  return index; }
                this.IsSuccess = true;
                this.Buffer.Write24Bit(index, newValue);
                return Success; 
            }
    }
}
