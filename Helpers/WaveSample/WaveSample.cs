using Sampler.Services.Audio.BufferPcm24;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Helpers.WaveSample {



    public partial class WaveSample {

        #region DIAGNOSTYCZNE ___ 
            private             byte[] _oryginalData    =   Array.Empty<byte>();

            private             ObjectStatus _status    =   ObjectStatus.UNKNOWN;
            public              ObjectStatus Status     =>  _status;

            private string      _statusMessage          =   string.Empty;
            public string       StatusMessage           =>  _statusMessage; 
        #endregion



            public      WaveHeader          Header          =  new WaveHeader();
            public      BufferPcm24         AudioData       =  null!;



        /// <summary>   
        ///     Constructor pustego obiektu.
        /// </summary>
        public WaveSample() {
                    _status                =   ObjectStatus.UNKNOWN;
                    _oryginalData          =   Array.Empty<byte>();
        }



        /// <summary>
        ///     Constructor klasy WaveSample, przyjmuje tablicę bajtów z odczytanego pliku wav. 
        /// </summary>
        /// <param name="data"> odczytany plik wav </param>
        public WaveSample(byte[] data) {    
                    if ( data.Length < 44 || Encoding.ASCII.GetString( data, 0, 4) != "RIFF") {
                        _status = ObjectStatus.ERROR;
                        _statusMessage =    GetType().Name  + "\n nieprawidłowe dane wejsciowe. spodziewano danych pliku WAV";
                        return;
                    }
                    _oryginalData = (byte[]) data.Clone();
                    LoadFromBytes( _oryginalData );
                    if ( Header.Subchunk2IDsize + 44 != _oryginalData.Length ) {
                        _status = ObjectStatus.WARNING;
                        _statusMessage =    GetType().Name  + 
                                    "\n nieprawidłowe dane wejsciowe. rozmiar danych audio nie zgadza się z wartością pola Subchunk2IDsize";
                        return;
                    }
        }


        public WaveHeader   GetHeader()          => Header;

    }
}


