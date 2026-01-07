using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.ViewModels {
    public partial class MixViewModel : BaseViewModel
    {

        // efektem kodu jest zmienna TemplateOffset  - ustawiony knob 
        public int TemplateOffsetFrames => TemplateOffset;

        private double _knobOffset;
        public double KnobOffset
        {
            get => _knobOffset;
            set
            {
                _knobOffset = Math.Max(0, Math.Min(1, value));
                OnPropertyChanged();
                UpdateTemplateOffset();
            }
        }

        public int MaxOffsetSamples => _viewModel.Corx.RegisterA.LengthInFrames() - 1;

        private int _templateOffset;
        public int TemplateOffset
        {
            get => _templateOffset;
            set
            {
                _templateOffset = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TemplateOffsetFrames));
            }
        }

        private void UpdateTemplateOffset()
        {
            int offsetSamples = (int)(KnobOffset * MaxOffsetSamples);
            TemplateOffset = offsetSamples;
        }
    }
}
