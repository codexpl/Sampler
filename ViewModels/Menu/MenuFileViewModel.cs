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




namespace Sampler.ViewModels.Menu {
 public class MenuFileViewModel:INotifyPropertyChanged {

            public event PropertyChangedEventHandler? PropertyChanged;
            protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); 




                    public      ViewModel           _viewModel      { get; set; }

                    public      ICommand            OpenCommand     { get; set; }
                     
    

            public MenuFileViewModel( ViewModel viewModel ) {          
                    _viewModel     = viewModel;
                    OpenCommand    = new RelayCommand(Open);                
                    _viewModel.LogService.Append( "[INFO] MenuViewModel initialized." ); 
            }




            private void Open() {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog {   Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*"  };
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                    _viewModel.WaveSmpl = new WaveSample( File.ReadAllBytes( filePath ) );
                    _viewModel.Buffer = new BufferPcm24( _viewModel.WaveSmpl.GetAudioData() );
                }
                _viewModel.LogService.Append( "[INFO] Opened file.  buffer bytes lenth = " + _viewModel.Buffer.Bytes.Length );
            }


    }
}

