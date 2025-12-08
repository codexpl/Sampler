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
        private readonly    MenuViewModel _menuEditorViewModel;

        public ICommand     LoadXCommand            { get; }
        public ICommand     LoadYCommand            { get; }
        public ICommand     MixCommand              { get; } 

        private  WaveSampler        _waveX;
        private  WaveSampler        _waveY;

        public MixViewModel(MenuViewModel menuEditorViewModel)
        {
            _menuEditorViewModel = menuEditorViewModel;
            LoadXCommand = new Helpers.RelayCommand(Loadx);
            LoadYCommand = new Helpers.RelayCommand(Loady);
            MixCommand = new Helpers.RelayCommand(Mix);
        }
        private void Mix()
        {
            // int result = _menuEditorViewModel.ViewModel.Sampler.Edit.Mix();
            // _menuEditorViewModel.ViewModel.LogService.Append($"[INFO]  Mixing Stereo to Mono... {result} samples");
            _menuEditorViewModel.ViewModel.LogService.Append("[INFO]  Mixing audio files...");
        }

        private void Loadx()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*" };
                if ( Directory.Exists( AppConfiguration.getReadDirectory() ) ) openFileDialog.InitialDirectory = AppConfiguration.getReadDirectory();
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                    _waveX = new WaveSampler( (byte[]) File.ReadAllBytes( filePath ) );
                }           
                _menuEditorViewModel.ViewModel.LogService.Append( "[INFO] Opened file. Buffer.Bytes.Length = " + _waveX.Edit.Buffer.Bytes.Length );
                _waveX.Edit.Buffer.Play();
        }

        private void Loady()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "WAV Files (*.wav)|*.wav|All Files (*.*)|*.*" };
                if ( Directory.Exists( AppConfiguration.getReadDirectory() ) ) openFileDialog.InitialDirectory = AppConfiguration.getReadDirectory();
                if (openFileDialog.ShowDialog() == true) {
                    var filePath = openFileDialog.FileName;
                    _waveY = new WaveSampler( (byte[]) File.ReadAllBytes( filePath ) );
                }           
                _menuEditorViewModel.ViewModel.LogService.Append( "[INFO] Opened file. Buffer.Bytes.Length = " + _waveY.Edit.Buffer.Bytes.Length );
                _waveY.Edit.Buffer.Play();
        }
    }
}
