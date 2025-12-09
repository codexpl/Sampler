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
        private readonly    ViewModel           _viewModel;
        public ICommand     ToMonoCommand       { get; } 

        public ToMonoViewModel( ViewModel viewModel)
        {
            _viewModel = viewModel;
            ToMonoCommand = new Helpers.RelayCommand(ToMono);
        }

        private void ToMono()
        {
            int result = _viewModel.waveDst.Edit.ToMono();
            _viewModel.LogService.Append($"[INFO]  Converting to Mono... {result} samples");
        }
    }
}
