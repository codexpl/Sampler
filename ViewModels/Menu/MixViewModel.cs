using Sampler.Services.AppConfiguration;
using Sampler.Services.Audio;
using Sampler.ViewModels.Menu;

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
        private readonly    ViewModel               _viewModel;

        public ICommand     LoadSourceCommand                   { get; }
        public ICommand     LoadDestinationCommand              { get; }
        public ICommand     MixCommand                          { get; } 



        public MixViewModel(ViewModel viewModel) {
            _viewModel = viewModel;
            LoadSourceCommand       = new Helpers.RelayCommand(LoadSrc);
            LoadDestinationCommand  = new Helpers.RelayCommand(LoadDst);
            MixCommand              = new Helpers.RelayCommand(Mix);
        }
        private void Mix()
        {
            int srcSamples = _viewModel.waveSrc.Edit.GetCurrentSampleCounter();
            int dstSamples = _viewModel.waveDst.Edit.GetCurrentSampleCounter();
            int minSamples = Math.Min( srcSamples, dstSamples );
            for ( int i = 1; i <= minSamples; i++ ) {
              int  Lsrc = _viewModel.waveSrc.Edit.GetLeftSampleValue( i );
              int  Rsrc = _viewModel.waveSrc.Edit.GetRightSampleValue( i );
              int  Ldst = _viewModel.waveDst.Edit.GetLeftSampleValue( i );
              int  Rdst = _viewModel.waveDst.Edit.GetRightSampleValue( i );
              int  Lmix = AverageSamples( Lsrc, Ldst );
              int  Rmix = AverageSamples( Rsrc, Rdst );
              _viewModel.waveDst.Edit.SetLeftSampleValue( i, Lmix );
              _viewModel.waveDst.Edit.SetRightSampleValue( i, Rmix );
            }
            _viewModel.waveDst.Edit.Buffer.Play();
            _viewModel.LogService.Append("[INFO]  Mixing audio files...");
        }

        private int AverageSamples( int sampleA, int sampleB ) => ( sampleA + sampleB ) / 2;

        private void LoadSrc()  {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*" };
                if ( Directory.Exists( AppConfiguration.getReadDirectory() ) ) openFileDialog.InitialDirectory = AppConfiguration.getReadDirectory();
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                    _viewModel.waveSrc = new WaveSampler( (byte[]) File.ReadAllBytes( filePath ) );
                }           
                _viewModel.LogService.Append( "[INFO] Opened file. Buffer.Bytes.Length = " + _viewModel.waveSrc.Edit.Buffer.Bytes.Length );
                _viewModel.waveSrc.Edit.Buffer.Play();
        }

        private void LoadDst()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*" };
                if ( Directory.Exists( AppConfiguration.getReadDirectory() ) ) openFileDialog.InitialDirectory = AppConfiguration.getReadDirectory();
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                    _viewModel.waveDst = new WaveSampler( (byte[]) File.ReadAllBytes( filePath ) );
                }           
                _viewModel.LogService.Append( "[INFO] Opened file. Buffer.Bytes.Length = " + _viewModel.waveDst.Edit.Buffer.Bytes.Length );
                _viewModel.waveDst.Edit.Buffer.Play();
        }
    }
}
