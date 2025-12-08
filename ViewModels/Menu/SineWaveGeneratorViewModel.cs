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

            private int _frequency = 100;
            public int Frequency {
                get => _frequency;
                set {
                    if ( _frequency != value ) {
                        _frequency = value;
                        OnPropertyChanged();
                        _menuEditorViewModel.ViewModel.LogService.Append($"[INFO] Frequency changed to { _frequency }");
                    }
                }
            }

            private int  _duration = 44100; //  sample points
            public  int  Duration
            {
                get => _duration;
                set
                {
                    if (_duration != value)
                    {
                        _duration = value;
                        OnPropertyChanged();
                        _menuEditorViewModel.ViewModel.LogService.Append($"[INFO] Duration changed to {_duration}");
                    }
                }
            }



        private readonly MenuEditorViewModel _menuEditorViewModel;
            public SineWaveGeneratorViewModel( MenuEditorViewModel menuEditorViewModel) {
                _menuEditorViewModel = menuEditorViewModel;
                GenerateSineCommand = new Helpers.RelayCommand(GenerateSineWave);
                _menuEditorViewModel.ViewModel.LogService.Append("[INFO]SineWaveGeneratorViewModel initialized.");
            }

            private void GenerateSineWave()  {
                _menuEditorViewModel.ViewModel.Sampler.Edit.CreateSineWave( Frequency, Duration );
                _menuEditorViewModel.ViewModel.Sampler.Edit.Buffer.Play();
                _menuEditorViewModel.ViewModel.LogService.Append("[INFO]GenerateSineWave command executed.");
            }
    }
}
