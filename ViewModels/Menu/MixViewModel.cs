using Sampler.Services.AppConfiguration;
using Sampler.Services.Audio;
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
        public ICommand LoadSourceCommand { get; }
        public Helpers.RelayCommand PlaySourceCommand { get; }    // uwaga: konkretny typ dla RaiseCanExecuteChanged


        public ICommand LoadDestinationCommand { get; }
        public Helpers.RelayCommand PlayDestinationCommand { get; }    // uwaga: konkretny typ dla RaiseCanExecuteChanged

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
            LoadSourceCommand       = new Helpers.RelayCommand(LoadSrc);
            PlaySourceCommand       = new Helpers.RelayCommand(PlaySource, () => IsSourceLoaded);


            LoadDestinationCommand  = new Helpers.RelayCommand(LoadDst);
            PlayDestinationCommand  = new Helpers.RelayCommand( PlayDestination, () => IsDestinationLoaded );

            MixCommand              = new Helpers.RelayCommand(Mix);
            PlayMixCommand          = new Helpers.RelayCommand( PlayDestination, () => IsDestinationLoaded );
        }



        private void Mix()
        {
            
            _viewModel.LogService.Append("[INFO]  Appended audio files... at now is " + _viewModel.SampleR.RegisterA.LengthInFrames() + " frames" );
        }

        private void PlaySource()       => _viewModel.SampleR.RegisterB.Play();
        private void PlayDestination()  => _viewModel.SampleR.RegisterA.Play();

        private void LoadSrc()  {
            _viewModel.LogService.Append( "[INFO] Bits Per Sample: IsSourceLoaded = " + IsSourceLoaded );
            var openFileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*" };
                if ( Directory.Exists( AppConfiguration.getReadDirectory() ) ) openFileDialog.InitialDirectory = AppConfiguration.getReadDirectory();
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                _viewModel.SampleR.LoadB( (byte[])File.ReadAllBytes( filePath ) );
                }           
                _viewModel.LogService.Append( "[INFO] Opened file. Buffer.Bytes.Length = " + _viewModel.SampleR.RegisterA.LengthInFrames() + " frames" );
                IsSourceLoaded = true;
                _viewModel.LogService.Append( "[INFO] Bits Per Sample: IsSourceLoaded = " + IsSourceLoaded );
        }

        private void LoadDst()
        {
            _viewModel.LogService.Append( "[INFO] Bits Per Sample: IsDestinationLoaded = " + IsDestinationLoaded );
            var openFileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*" };
                if ( Directory.Exists( AppConfiguration.getReadDirectory() ) ) openFileDialog.InitialDirectory = AppConfiguration.getReadDirectory();
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                _viewModel.SampleR.LoadA( (byte[])File.ReadAllBytes( filePath ) );
                }           
                _viewModel.LogService.Append( "[INFO] Opened file. Buffer.Bytes.Length = " + + _viewModel.SampleR.RegisterB.LengthInFrames() + " frames" );
               IsDestinationLoaded = true;
                _viewModel.LogService.Append( "[INFO] Sample Rate: " + _viewModel.SampleR.RegisterB.Header.SampleRate + " Hz" );
                _viewModel.LogService.Append( "[INFO] Bits Per Sample: IsDestinationLoaded = " + IsDestinationLoaded );
        }
    }
}
