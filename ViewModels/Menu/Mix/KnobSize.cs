using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.ViewModels {
    public partial class MixViewModel : BaseViewModel
    {

        // efektem kodu jest zmienna TemplateSize = ustawiony knob w probkach 
        public int TemplateSizeFrames => TemplateSize;

        public int MaxSizeSamples =>
            Math.Max(0, _viewModel.Corx.RegisterA.LengthInFrames() - TemplateOffset);

        private int _templateSize;
        public int TemplateSize
        {
            get => _templateSize;
            set
            {
                _templateSize = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TemplateSizeFrames));
            }
        }

        private double _knobSize;
        public double KnobSize
        {
            get => _knobSize;
            set
            {
                _knobSize = Math.Max(0, Math.Min(1, value));
                OnPropertyChanged();
                UpdateTemplateSize();
            }
        }



        private void UpdateTemplateSize()
        {
            int sizeSamples = (int)(KnobSize * MaxSizeSamples);
            TemplateSize = sizeSamples;
        }
    }
}
