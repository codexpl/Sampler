using Sampler.Helpers;
using Sampler.ViewModels.Menu;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sampler.ViewModels
{
    class FadeViewModel:BaseViewModel
    {

            public ICommand FadeInCommand           { get; }
            public ICommand FadeOutCommand          { get; }


            private readonly    ViewModel   _viewModel;
            public FadeViewModel( ViewModel viewModel )    {   
                _viewModel = viewModel;
                FadeInCommand  = new RelayCommand(FadeIn);
                FadeOutCommand = new RelayCommand(FadeOut);
            }

            private void FadeIn() {
                //_viewModel.Corx.RegisterA.ApplyFade(true);
                _viewModel.Corx.RegisterA.Play();
            }

            private void FadeOut()  {
                //_viewModel.Corx.RegisterA.ApplyFade(false);
                _viewModel.Corx.RegisterA.Play();
            }
    }
}
