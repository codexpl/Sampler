using Sampler.Helpers;
using Sampler.Services.AppConfiguration;
using Sampler.Services.Audio;
using Sampler.ViewModels.Menu;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sampler.ViewModels {
    public  class ViewModel:BaseViewModel {


        public ICommand ShowEditorCommand       { get; }


        public      LogService               LogService         { get; private set; }
        public      WaveSampler              Sampler            { get; set; } = new WaveSampler();





        private     object                  _currentMenu;    
        public object CurrentMenu {
            get => _currentMenu;
            set
            {
                _currentMenu = value;
                OnPropertyChanged(nameof(CurrentMenu));
            }   
        }



        public      MenuViewModel           Menu         { get; set; }


        public ViewModel( LogService logService ) {

            this.LogService         = logService;
            this.Menu               = new MenuViewModel( (ViewModel) this );

            this.CurrentMenu        = this.Menu;
            ShowEditorCommand       = new RelayCommand  ( ()    => CurrentMenu = this.Menu );
        }
    }
}
