using Sampler.Services.AppConfiguration;
using Sampler.Services.Audio;
using Sampler.Services.WavCore.Register.Classes.Pattern;
using Sampler.ViewModels.Menu;
using Sampler.Views;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sampler.ViewModels
{
    public partial class MixViewModel:BaseViewModel
    {

        private readonly            ViewModel                           _viewModel;
        private string              LAST_FILE_IO_NAME                   = "Ready";

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
            PlayDestinationCommand  = new Helpers.RelayCommand( PlayA, () => IsDestinationLoaded );

            LoadSoundBCommand       = new Helpers.RelayCommand( _loadB );
            PlaySourceCommand       = new Helpers.RelayCommand( PlaySource, () => IsSourceLoaded );

            MixCommand              = new Helpers.RelayCommand( Mix );
            PlayMixCommand          = new Helpers.RelayCommand( PlayA, () => IsDestinationLoaded );
        }



        private void    Mix()  {

            Register oryginal = new Register( _viewModel.Corx.RegisterB );
            var slice = _viewModel.Corx.RegisterB.Frames.Skip( TemplateOffset ).Take( TemplateSize ).ToList();
            Pattern kick = PatternExtractor.FromFrames( slice, _viewModel.Corx.RegisterA.Header.SampleRate, "kick" );
            var hits =_viewModel.Corx.RegisterB.FindPattern( kick, kick.OriginalLength, 0.2f );

            _viewModel.LogService.Append( "[INFO] wzorzec znaleziono  " + hits.Count + " razy w rejestrze B" );

            //konwersja patternu do ramek
            var patternFrames = PatternExtractor.FloatToFrames(kick.Normalized);

            // nadpisanie wszystkich trafień
            foreach (var hit in hits) { _viewModel.Corx.RegisterB.ReplaceFrames(hit.StartFrame, patternFrames); }
            _viewModel.Corx.RegisterB.Play();
            _viewModel.Corx.RegisterB = oryginal;
        }

        private void    PlaySource()       => _viewModel.Corx.RegisterB.Play();
        private void PlayA()
        {
            Task.Run(() =>
            {
                if (TemplateOffset != 0 || TemplateSize != 0)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() => { _viewModel.LogService.Append("[INFO] PlayA with TemplateOffset " + TemplateOffset + " TemplateSize " + TemplateSize); });
                    _viewModel.Corx.RegisterB = _viewModel.Corx.RegisterA;

                    List<Frame24> slice = _viewModel.Corx.RegisterA.Frames
                        .Skip(TemplateOffset)
                        .Take(TemplateSize)
                        .ToList();

                    _viewModel.Corx.RegisterA.Frames = slice;
                    _viewModel.Corx.RegisterA.HeaderUpdate();
                    _viewModel.Corx.RegisterA.Play();

                    System.Windows.Application.Current.Dispatcher.Invoke ( () => { _viewModel.LogService.Append("[INFO] Restoring RegisterA after play"); } );
                    _viewModel.Corx.RegisterA = _viewModel.Corx.RegisterB;
                }
                else
                {
                    _viewModel.Corx.RegisterA.Play();
                }
            });
        }

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
            string fname = "not loaded register b";
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
