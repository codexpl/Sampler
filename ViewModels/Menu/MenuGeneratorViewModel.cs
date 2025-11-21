using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Sampler.Helpers;
using Sampler.Models;
using Sampler.Services.Audio;
using Sampler.Services.Audio.BufferPcm24;

namespace Sampler.ViewModels.Menu {
    public  class MenuGeneratorViewModel:INotifyPropertyChanged {

            public event PropertyChangedEventHandler? PropertyChanged;
            protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            public      ViewModel       _viewModel       { get; set; }
            public      ICommand        SineWaveCommand   { get; set; }

            public MenuGeneratorViewModel( ViewModel viewModel ) {
                _viewModel           = viewModel;
                SineWaveCommand      = new RelayCommand(GenerateSineWave);
            }


            private void GenerateSineWave() {
                _viewModel.LogService.Append( "[INFO] Generating Sine Wave..." );
                _viewModel.Buffer.CreateSineWave( 44100, 440.0 ); // 1 second of A4
            }

    }
}
