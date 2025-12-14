using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {



    public partial class Sampler {

        #region DIAGNOSTYCZNE ___ 
            public ObjectStatus Status                  { get;  private set; }      =  ObjectStatus.UNKNOWN;
            public string       StatusMessage           =>  Status.ToString(); 
        #endregion




            public      AudioRegister              RegisterA           =  null!;
            public      AudioRegister              RegisterB           =  null!;



            /// <summary>   
            ///     Constructor pustego obiektu.
            /// </summary>
            public Sampler() {
                    RegisterA                   =   new AudioRegister();
                    RegisterB                   =   new AudioRegister();
                    Status                      =   ObjectStatus.SUCCESS;
            }



            /// <summary>
            ///     Constructor klasy Sampler, przyjmuje tablicę bajtów z odczytanego pliku wav. 
            /// </summary>
            /// <param name="data"> odczytany plik wav </param>
            public Sampler(byte[] data) {    
                  LoadD( data );
                  LoadS( data );
                  Status                     =   ObjectStatus.SUCCESS;
            }

            




            /// <summary>
            ///     File wav data loader do edycji destruktywnej.
            /// </summary>
            /// <param name="data">  dane pliku WAV , 24 bit stereo 44100Hz </param>
            /// <returns> true jezeli gotowy do użycia i prawidłowy </returns>
            public void LoadD( byte[] data ) => RegisterA =   new AudioRegister( data );  


            /// <summary>
            ///     File wav data loader do edycji niedestruktywnej.
            /// </summary>
            /// <param name="data">  dane pliku WAV , 24 bit stereo 44100Hz </param>
            /// <returns> true jezeli gotowy do użycia i prawidłowy </returns>
            public void LoadS( byte[] data ) => RegisterB =   new AudioRegister( data );
              
            


            public WaveHeader   GetHeaderA()          => RegisterA.Header;
            public WaveHeader   GetHeaderB()          => RegisterB.Header;

    }
}


