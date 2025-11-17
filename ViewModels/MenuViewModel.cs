using CSCore;
using CSCore.Codecs.WAV;
using CSCore.SoundOut;

using Microsoft.VisualBasic;

using Sampler.Helpers;
using Sampler.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Sampler.Services.Audio;
using Sampler.Services.Audio.BufferPcm24;




namespace Sampler.ViewModels {
 public class MenuViewModel:INotifyPropertyChanged {

            public event PropertyChangedEventHandler? PropertyChanged;
            protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); 




            private readonly LogService                         _logService;

            #region ICommands 
                    public ICommand OpenCommand { get; set; }
                    public ICommand PlayCommand { get; set; }
                    public ICommand EfectxCommand { get; set; } 
            #endregion



            private     WaveSample                              _waveSample      = new();

            private     BufferPcm24                             _buffer          = new( new byte[0] );





            public MenuViewModel( LogService logService ) {
                            this._logService = logService;
                            this.OpenCommand = new RelayCommand(Open);
                            this.PlayCommand = new RelayCommand(Play);
                            this.EfectxCommand = new RelayCommand( Efectx );
            }




            private void Efectx() {

            }

            private void Play() => this._buffer.Play();


            private void Open() {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog {   Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*"  };
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                    _waveSample = new WaveSample( File.ReadAllBytes( filePath ) );
                    this._buffer = new BufferPcm24( _waveSample.GetAudioData() );
                }
            }


    }
}

