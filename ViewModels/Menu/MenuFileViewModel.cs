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
using Sampler.Services.AppConfiguration;




namespace Sampler.ViewModels.Menu {
 public class MenuFileViewModel:INotifyPropertyChanged {

                    public event PropertyChangedEventHandler? PropertyChanged;
                    protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); 


                    public      ViewModel           VModel          { get; set; }
                    public      ICommand            OpenCommand     { get; set; }
                    public      ICommand            SaveCommand     { get; set; }



            public MenuFileViewModel( ViewModel viewModel ) {          
                    VModel     = viewModel;
                    OpenCommand    = new RelayCommand(Open);       
                    SaveCommand    = new RelayCommand(Save);             
            }




            private void Open() {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog {   Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*"  };
                if ( Directory.Exists( AppConfiguration.getReadDirectory() ) ) openFileDialog.InitialDirectory = AppConfiguration.getReadDirectory();
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                    VModel.WaveSmpl = new WaveSampler( (byte[]) File.ReadAllBytes( filePath ) );
                }           
                VModel.LogService.Append( "[INFO] Opened file. Buffer.Bytes.Length = " + VModel.WaveSmpl.Buffer.Bytes.Length );
            }

            private void Save() {
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog {   Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*"  };
                if( Directory.Exists( AppConfiguration.getWriteDirectory() ) ) saveFileDialog.InitialDirectory = AppConfiguration.getWriteDirectory();
                if (saveFileDialog.ShowDialog() == true) {
                    var filePath = saveFileDialog.FileName;
                    File.WriteAllBytes( filePath, VModel.WaveSmpl.ToWaveFile24() );
                }
                VModel.LogService.Append( "[INFO] Saved file." );
            }


        }
}

