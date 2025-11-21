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
    public  class MenuPlayerViewViewModel:INotifyPropertyChanged {

            public event PropertyChangedEventHandler? PropertyChanged;
            protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); 

            public      ViewModel                              _viewModel       { get; set; }
            public      ICommand                                PlayCommand     { get; set; }

            public MenuPlayerViewViewModel( ViewModel viewModel ) {
                _viewModel           = viewModel;
                PlayCommand        = new RelayCommand(Play);
            }

            private void Play() {
                _viewModel.LogService.Append( $"[INFO] Play command executed. buffer size {_viewModel.Buffer.GetBufferLength()} bytes" );
                _viewModel.Buffer.Play();
            }
    }
}
