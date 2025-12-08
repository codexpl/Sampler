using Sampler.ViewModels.Menu;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sampler.ViewModels
{
    public class ToMonoViewModel:BaseViewModel
    {
        private readonly    MenuEditorViewModel _menuEditorViewModel;
        public ICommand     ToMonoCommand       { get; } 

        public ToMonoViewModel(MenuEditorViewModel menuEditorViewModel)
        {
            _menuEditorViewModel = menuEditorViewModel;
            ToMonoCommand = new Helpers.RelayCommand(ToMono);
        }

        private void ToMono()
        {
            int result = _menuEditorViewModel.ViewModel.Sampler.Edit.ToMono();
            _menuEditorViewModel.ViewModel.LogService.Append($"[INFO]  Converting to Mono... {result} samples");
        }
    }
}
