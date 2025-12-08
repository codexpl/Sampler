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
            public      Editor              Edit          =  null!;



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

                  this.Header = WaveHeaderParser.Parse(data);
                  if( ! Header.IsValid() )   { 
                        _status = ObjectStatus.ERROR;
                        _statusMessage =    nameof(WaveSampler)   + "\n nieprawidłowe dane wejsciowe. spodziewano danych pliku WAV";
                        return;
                    }

                  _oryginalData           = (byte[]) data.Clone();
                  int   startDataIndex    = data.Length - Header.Subchunk2Size;
                  byte[] audioData        = new byte[Header.Subchunk2Size];
                  Array.Copy(data, startDataIndex, audioData, 0, Header.Subchunk2Size);

                  this.Edit =   new Editor( Header, audioData );            
        }


        public WaveHeader   GetHeader()          => Header;

    }
}


