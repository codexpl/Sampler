using Sampler.ViewModels.Menu;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sampler.ViewModels  {
    class GainViewModel:BaseViewModel  {

            public ICommand ApplyGainCommand { get; set; }

            private readonly    ViewModel _viewModel;
            private double      _gainFactor = 1.0;
            public  double      GainFactor { 
                get => _gainFactor;
                set {
                    if ( _gainFactor != value ) {
                        _gainFactor = value;
                        OnPropertyChanged();
                        _viewModel.LogService.Append($"[INFO] GainFactor changed to { _gainFactor }");
                    }
                }
            }

            public GainViewModel( ViewModel viewModel )  {   
                _viewModel = viewModel;
                ApplyGainCommand = new Helpers.RelayCommand(ApplyGain);
                _viewModel.LogService.Append("[INFO]GainViewModel initialized.");
            }


            private void ApplyGain()   {
                //_viewModel.SampleR.RegisterA.ApplyGain( (float) GainFactor );
                _viewModel.SampleR.RegisterA.Play();
                _viewModel.LogService.Append("[INFO]ApplyGain command executed.");
            }
    }
}
