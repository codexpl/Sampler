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

            public      ViewModel       VModel              { get; set; }
            public      ICommand        GenerateCommand     { get; set; }

            private int _frequency;
            public int Frequency
            {
                get => _frequency;
                set { _frequency = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Frequency))); }
            }

            private int _duration;
            public int Duration
            {
                get => _duration;
                set { _duration = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Duration))); }
            }



            public MenuGeneratorViewModel( ViewModel viewModel ) {
                VModel                  = viewModel;
                GenerateCommand         = new RelayCommand(GenerateSineWave);
                Frequency               =   400;
                Duration                =   44100;
            }


            private void GenerateSineWave() {
                VModel.LogService.Append( "[INFO] Generating Sine Wave..." );
                VModel.WaveSmpl.AudioData.SineTest( Duration, Frequency ); // 1 second of A4
            }

    }
}
