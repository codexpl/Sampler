using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {



    public partial class Core {

            #region DIAGNOSTYCZNE ___ 
                public ObjectStatus Status                  { get;  private set; }      =  ObjectStatus.UNKNOWN;
                public string       StatusMessage           =>  Status.ToString(); 
            #endregion



            private     Stack<Register>       _stack              =  new Stack<Register>();
            public      Register              RegisterA           =  null!;
            public      Register              RegisterB           =  null!;



            /// <summary>   
            ///     Constructor pustego obiektu.
            /// </summary>
            public Core() {
                    RegisterA                   =   new Register();
                    RegisterB                   =   new Register();
                    Status                      =   RegisterA.Header.IsValid() && RegisterB.Header.IsValid() ? ObjectStatus.SUCCESS : ObjectStatus.ERROR;
            }



            /// <summary>
            ///     Constructor klasy Core, przyjmuje tablicę bajtów z odczytanego pliku wav. 
            /// </summary>
            /// <param name="data"> odczytany plik wav </param>
            public Core(byte[] data) {    
                  LoadA( data );
                  LoadB( data );
                  Status                        =   RegisterA.Header.IsValid() && RegisterB.Header.IsValid() ? ObjectStatus.SUCCESS : ObjectStatus.ERROR;
            }

            


            public void Play() => RegisterA.Play();
            public void Stop() => RegisterA.Stop();


            /// <summary>
            ///     File wav data loader do edycji destruktywnej.
            /// </summary>
            /// <param name="data">  dane pliku WAV , 24 bit stereo 44100Hz </param>
            /// <returns> true jezeli gotowy do użycia i prawidłowy </returns>
            public void LoadA( byte[] data ) => RegisterA =   new Register( data );  


            /// <summary>
            ///     File wav data loader do edycji niedestruktywnej.
            /// </summary>
            /// <param name="data">  dane pliku WAV , 24 bit stereo 44100Hz </param>
            /// <returns> true jezeli gotowy do użycia i prawidłowy </returns>
            public void LoadB( byte[] data ) => RegisterB =   new Register( data );
              
            


            public WaveHeader   GetHeaderA()          => RegisterA.Header;
            public WaveHeader   GetHeaderB()          => RegisterB.Header;

    }
}



