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
    public class MixViewModel:BaseViewModel
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



        private void Mix()  {
            
            var slice = _viewModel.Corx.RegisterA.Frames.Skip( 10000 ).Take( 500 ).ToList();
            Pattern kick = PatternExtractor.FromFrames( slice, _viewModel.Corx.RegisterA.Header.SampleRate, "kick" );
            var hits =_viewModel.Corx.RegisterB.FindPattern( kick, kick.OriginalLength, 0.2f );

            _viewModel.LogService.Append( "[INFO] wzorzec znaleziono  " + hits.Count + " razy w rejestrze B" );

            // konwersja patternu do ramek
            var patternFrames = PatternExtractor.FloatToFrames(kick.Normalized);

            // nadpisanie wszystkich trafień
            foreach (var hit in hits) { _viewModel.Corx.RegisterB.ReplaceFrames(hit.StartFrame, patternFrames); }
        }

        private void    PlaySource()       => _viewModel.Corx.RegisterB.Play();
        private void    PlayDestination()  => _viewModel.Corx.RegisterA.Play();


        private void    _loadA()  {
            _viewModel.LogService.Append( "[INFO] Bits Per Sample: IsDestinationLoaded = " + IsDestinationLoaded );
            var openFileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*" };
                if ( Directory.Exists( AppConfiguration.getReadDirectory() ) ) openFileDialog.InitialDirectory = AppConfiguration.getReadDirectory();
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                    _viewModel.Corx.LoadA( (byte[])File.ReadAllBytes( filePath ) );
                }           
                _viewModel.LogService.Append( "[WARNING] Opened file. Buffer.Bytes.Length = " + + _viewModel.Corx.RegisterB.LengthInFrames() + " frames" );
               IsDestinationLoaded = true;
                _viewModel.LogService.Append( "[INFO] Sample Rate: " + _viewModel.Corx.RegisterB.Header.SampleRate + " Hz" );
                _viewModel.LogService.Append( "[INFO] Bits Per Sample: IsDestinationLoaded = " + IsDestinationLoaded );
        }
        private void    _loadB()  {
            _viewModel.LogService.Append( "[INFO] Bits Per Sample: IsSourceLoaded = " + IsSourceLoaded );
            var openFileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*" };
                if ( Directory.Exists( AppConfiguration.getReadDirectory() ) ) openFileDialog.InitialDirectory = AppConfiguration.getReadDirectory();
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                    _viewModel.Corx.LoadB( (byte[])File.ReadAllBytes( filePath ) );
                }           
                _viewModel.LogService.Append( "[ERROR] Opened file. Length = " + _viewModel.Corx.RegisterA.LengthInFrames() + " frames" );
                IsSourceLoaded = true;
        }


    }
}
