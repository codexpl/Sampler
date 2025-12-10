using Sampler.ViewModels.Menu;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sampler.ViewModels
{
    class SineWaveGeneratorViewModel:BaseViewModel
    {

            public ICommand GenerateSineCommand { get; set; }

            private int     _frequency = 100;
            public int      Frequency {
                get => _frequency;
                set {
                    if ( _frequency != value ) {
                        _frequency = value;
                        OnPropertyChanged();
                        _viewModel.LogService.Append($"[INFO] Frequency changed to { _frequency }");
                    }
                }
            }



            private int     _duration = 44100; //  sample points
            public  int     Duration
            {
                get => _duration;
                set
                {
                    if (_duration != value)
                    {
                        _duration = value;
                        OnPropertyChanged();
                        _viewModel.LogService.Append($"[INFO] Duration changed to {_duration}");
                    }
                }
            }



            private readonly ViewModel      _viewModel;
            public SineWaveGeneratorViewModel( ViewModel viewModel ) {
                _viewModel = viewModel;
                GenerateSineCommand = new Helpers.RelayCommand(GenerateSineWave);
                _viewModel.LogService.Append("[INFO]SineWaveGeneratorViewModel initialized.");
            }

            private void GenerateSineWave()  {
                if( _viewModel.waveDst == null ) {
                    _viewModel.LogService.Append("[ERROR]Cannot generate sine wave: No audio file loaded.");
                    return;
                }
                _viewModel.waveDst.Edit.CreateSineWave( Frequency, Duration ); 
                _viewModel.waveDst.Edit.Buffer.Play();
                _viewModel.LogService.Append("[INFO]GenerateSineWave command executed.");
            }
    }
}
