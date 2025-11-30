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
    public  class ViewModel:INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));





        public ICommand ShowFileCommand         { get; }
        public ICommand ShowPlayerCommand       { get; }
        public ICommand ShowGeneratorCommand    { get; }


        public      LogService               LogService         { get; private set; }
        public      WaveSampler              WaveSmpl           { get; set; } = new WaveSampler();


        private     object                  _currentMenu;    
        public object CurrentMenu {
            get => _currentMenu;
            set
            {
                _currentMenu = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentMenu)));
            }   
        }


        public      MenuFileViewModel       MenuFile           { get; set; }
        public      MenuPlayerViewViewModel MenuPlayer         { get; set; }
        public      MenuGeneratorViewModel  MenuGenerator      { get; set; }


        public ViewModel( RichTextBox richTextBox ) {
            this.LogService         = new LogService( richTextBox );
            this.MenuFile           = new MenuFileViewModel( (ViewModel) this );
            this.MenuPlayer         = new MenuPlayerViewViewModel( (ViewModel) this );
            this.MenuGenerator      = new MenuGeneratorViewModel( (ViewModel) this );

            this.CurrentMenu        = this.MenuFile;
            ShowFileCommand         = new RelayCommand  ( ()    => CurrentMenu = this.MenuFile );
            ShowPlayerCommand       = new RelayCommand  ( ()    => CurrentMenu = this.MenuPlayer );
            ShowGeneratorCommand    = new RelayCommand  ( ()    => CurrentMenu = this.MenuGenerator );
        }
    }
}
