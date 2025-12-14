using Sampler.Helpers;
using Sampler.Services.AppConfiguration;
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
        public ICommand PlayCommand             { get; }
        public ICommand StopCommand             { get; }

        public ICommand PauseCommand            { get; }
        public ICommand RewindCommand           { get; }
        public ICommand ForwardCommand          { get; }

        public      LogService                      LogService         { get; private set; }
        public      Services.Audio.Sampler          SampleR            { get; set; } = new Services.Audio.Sampler();






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
            this.SampleR            = new Services.Audio.Sampler();
            this.Menu               = new MenuViewModel( (ViewModel) this );

            PlayCommand             = new RelayCommand(Play);
            StopCommand             = new RelayCommand(Stop);


            this.CurrentMenu        = this.Menu;
            ShowEditorCommand       = new RelayCommand  ( ()    => CurrentMenu = this.Menu );

            LogService.Append("[ERROR] ViewModel initialized.  SampleR.StatusMessage = " + SampleR.StatusMessage );
        }

        private void Play() => SampleR.RegisterA.Play();
        private void Stop() {
            SampleR.RegisterB.Stop();
            SampleR.RegisterA.Stop();
        }

    }
}
