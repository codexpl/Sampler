using Sampler.ViewModels.Menu;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sampler.ViewModels
{
    class GainViewModel:BaseViewModel
    {

            public ICommand ApplyGainCommand { get; set; }

            private readonly    MenuEditorViewModel _menuEditorViewModel;
            private double      _gainFactor = 1.0;
            public  double      GainFactor { 
                get => _gainFactor;
                set {
                    if ( _gainFactor != value ) {
                        _gainFactor = value;
                        OnPropertyChanged();
                        _menuEditorViewModel.ViewModel.LogService.Append($"[INFO] GainFactor changed to { _gainFactor }");
                    }
                }
            }
            public GainViewModel( MenuEditorViewModel menuEditorViewModel ) 
            {   
                _menuEditorViewModel = menuEditorViewModel;
                _menuEditorViewModel.ViewModel.LogService.Append("[INFO]GainViewModel initialized.");

                ApplyGainCommand = new Helpers.RelayCommand(ApplyGain);
            }


            private void ApplyGain()
            {
                _menuEditorViewModel.ViewModel.WaveSmpl.Editor.ApplyGain( (float) GainFactor );
                _menuEditorViewModel.ViewModel.WaveSmpl.Buffer.Play();
                _menuEditorViewModel.ViewModel.LogService.Append("[INFO]ApplyGain command executed.");
            }
    }
}
