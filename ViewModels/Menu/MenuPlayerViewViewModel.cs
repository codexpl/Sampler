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

namespace Sampler.ViewModels.Menu {
    public  class MenuPlayerViewViewModel:INotifyPropertyChanged {

            public event PropertyChangedEventHandler? PropertyChanged;
            protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); 

            public      ViewModel                              VModel           { get; set; }
            public      ICommand                               PlayCommand      { get; set; }

            public MenuPlayerViewViewModel( ViewModel viewModel ) {
                VModel           = viewModel;
                PlayCommand        = new RelayCommand(Play);
            }

            private void Play() {
                VModel.LogService.Append( $"[INFO] Play command executed. buffer size {VModel.Sampler.Edit.Buffer.Bytes.Length} bytes" );
                VModel.Sampler.Edit.Buffer.Play();
            }
    }
}
