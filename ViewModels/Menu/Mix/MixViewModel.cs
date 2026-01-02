using Sampler.Services.AppConfiguration;
using Sampler.Services.Audio;
using Sampler.Services.WavCore.Register.Classes.Pattern;
using Sampler.ViewModels.Menu;
using Sampler.Views;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sampler.ViewModels
{
    public partial class MixViewModel:BaseViewModel
    {

        private readonly            ViewModel                           _viewModel;

        #region ICommands_  


        public ICommand LoadSoundACommand { get; }
        public Helpers.RelayCommand PlayDestinationCommand { get; }    // uwaga: konkretny typ dla RaiseCanExecuteChanged

        public ICommand LoadSoundBCommand { get; }
        public Helpers.RelayCommand PlaySourceCommand { get; }         // uwaga: konkretny typ dla RaiseCanExecuteChanged


        public ICommand MixCommand { get; }
        public ICommand PlayMixCommand { get; }
        #endregion




        #region IsSourceLoaded   
        private bool _isSourceLoaded = false;
        public bool IsSourceLoaded {
            get => _isSourceLoaded;
            set {
                _isSourceLoaded = value;
                OnPropertyChanged(nameof(IsSourceLoaded));
                PlaySourceCommand.RaiseCanExecuteChanged();
            }
        } 
        #endregion


        #region IsDestinationLoaded  
        private bool _isDestinationLoaded = false;
        public bool IsDestinationLoaded {
            get => _isDestinationLoaded;
            set {
                _isDestinationLoaded = value;
                OnPropertyChanged(nameof(IsDestinationLoaded));
                PlayDestinationCommand.RaiseCanExecuteChanged();
            }
        } 
        #endregion



        public MixViewModel(ViewModel viewModel) {
            _viewModel = viewModel;

            LoadSoundACommand       = new Helpers.RelayCommand( _loadA );
            PlayDestinationCommand  = new Helpers.RelayCommand( PlayDestination, () => IsDestinationLoaded );

            LoadSoundBCommand       = new Helpers.RelayCommand( _loadB );
            PlaySourceCommand       = new Helpers.RelayCommand( PlaySource, () => IsSourceLoaded );

            MixCommand              = new Helpers.RelayCommand( Mix );
            PlayMixCommand          = new Helpers.RelayCommand( PlayDestination, () => IsDestinationLoaded );
        }



        private void    Mix()  {
            
            var slice = _viewModel.Corx.RegisterA.Frames.Skip( (int) KnobOffset * _viewModel.Corx.RegisterA.LengthInFrames() ).Take( 500 ).ToList();
            _viewModel.Corx.RegisterA.Frames = slice;
            _viewModel.Corx.RegisterA.HeaderUpdate();
            //Pattern kick = PatternExtractor.FromFrames( slice, _viewModel.Corx.RegisterA.Header.SampleRate, "kick" );
            //var hits =_viewModel.Corx.RegisterA.FindPattern( kick, kick.OriginalLength, 0.2f );

            //_viewModel.LogService.Append( "[INFO] wzorzec znaleziono  " + hits.Count + " razy w rejestrze A" );

            // konwersja patternu do ramek
            //var patternFrames = PatternExtractor.FloatToFrames(kick.Normalized);

            // nadpisanie wszystkich trafień
            //foreach (var hit in hits) { _viewModel.Corx.RegisterA.ReplaceFrames(hit.StartFrame, patternFrames); }
        }

        private void    PlaySource()       => _viewModel.Corx.RegisterB.Play();
        private void    PlayDestination()  => _viewModel.Corx.RegisterA.Play();


        private void    _loadA()  {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*" };
                if ( Directory.Exists( AppConfiguration.getReadDirectory() ) ) openFileDialog.InitialDirectory = AppConfiguration.getReadDirectory();
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                    _viewModel.Corx.LoadA( File.ReadAllBytes( filePath ) );
                }           
               IsDestinationLoaded = true;
                _viewModel.LogService.Append( "[INFO] _loadA  " + IsDestinationLoaded );
        }
        private void    _loadB()  {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*" };
                if ( Directory.Exists( AppConfiguration.getReadDirectory() ) ) openFileDialog.InitialDirectory = AppConfiguration.getReadDirectory();
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                    _viewModel.Corx.LoadB( File.ReadAllBytes( filePath ) );
                }           
                 _viewModel.LogService.Append( "[INFO] _loadB  " + IsDestinationLoaded );
                IsSourceLoaded = true;
        }


    }
}
