using Sampler.Services.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {



    public partial class WaveSampler {

        #region DIAGNOSTYCZNE ___ 
            private             byte[] _oryginalData    =   Array.Empty<byte>();

            private             ObjectStatus _status    =   ObjectStatus.UNKNOWN;
            public              ObjectStatus Status     =>  _status;

            private string      _statusMessage          =   string.Empty;
            public string       StatusMessage           =>  _statusMessage; 
        #endregion



            public      WaveHeader          Header          =  new WaveHeader();
            public      BufferPcm24         AudioData       =  null!;
            public      Editor              Editor          =  null!;



        /// <summary>   
        ///     Constructor pustego obiektu.
        /// </summary>
        public WaveSampler() {
                    _status                =   ObjectStatus.UNKNOWN;
                    _oryginalData          =   Array.Empty<byte>();
        }



        /// <summary>
        ///     Constructor klasy WaveSampler, przyjmuje tablicę bajtów z odczytanego pliku wav. 
        /// </summary>
        /// <param name="data"> odczytany plik wav </param>
        public WaveSampler(byte[] data) {    
                    if ( data.Length < 44 || Encoding.ASCII.GetString( data, 0, 4) != "RIFF") {
                        _status = ObjectStatus.ERROR;
                        _statusMessage =    GetType().Name  + "\n nieprawidłowe dane wejsciowe. spodziewano danych pliku WAV";
                        return;
                    }
                    _oryginalData = (byte[]) data.Clone();
                    LoadFromBytes( _oryginalData );
                    this.Editor =   new Editor( Header, AudioData );
                    if ( Header.Subchunk2Size + 44 != _oryginalData.Length ) {
                        _status = ObjectStatus.WARNING;
                        _statusMessage =    GetType().Name  + 
                                    "\n nieprawidłowe dane wejsciowe. rozmiar danych audio nie zgadza się z wartością pola Subchunk2Size";
                        return;
                    }
        }


        public WaveHeader   GetHeader()          => Header;

    }
}


