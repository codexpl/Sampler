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


            private readonly    MenuEditorViewModel _menuEditorViewModel;
            public FadeViewModel( MenuEditorViewModel menuEditorViewModel )    {   
                _menuEditorViewModel = menuEditorViewModel;
                FadeInCommand  = new RelayCommand(FadeIn);
                FadeOutCommand = new RelayCommand(FadeOut);
            }

            private void FadeIn() {
                _menuEditorViewModel.ViewModel.Sampler.Edit.ApplyFade(true);
                _menuEditorViewModel.ViewModel.Sampler.Edit.Buffer.Play();
            }

            private void FadeOut()  {
                _menuEditorViewModel.ViewModel.Sampler.Edit.ApplyFade(false);
                _menuEditorViewModel.ViewModel.Sampler.Edit.Buffer.Play();
            }
    }
}
