using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.ViewModels {
    public partial class MixViewModel:BaseViewModel  {


                public int TemplateOffsetFrames => TemplateOffset / (44100 / 24);

                private double _knobOffset;
                public double KnobOffset {
                    get => _knobOffset;
                    set {
                        _knobOffset = value;
                        OnPropertyChanged();
                        UpdateTemplateOffset();
                    }
                }

                public  int MaxOffsetSamples { get{ return _viewModel.Corx.RegisterA.LengthInFrames() -500; } } 
                private int _templateOffset;
                public  int TemplateOffset {
                    get => _templateOffset;
                    set {
                        _templateOffset = value;
                        OnPropertyChanged();
                    }
                }



                private void GenerateTemplate(int offsetSamples) {
                    // tutaj robisz miks, przesunięcie, generowanie waveformu itd.
                    _viewModel.LogService.Append( "[INFO] Generating template at offset (samples): " + offsetSamples );
                }


                private void UpdateTemplateOffset() {
                    // KnobOffset = 0..1
                    // mapowanie na offset w próbkach
                    int offsetSamples = (int)(KnobOffset * MaxOffsetSamples);
                    TemplateOffset = offsetSamples;
                    // możesz od razu generować template
                    GenerateTemplate(offsetSamples);
                } 

    }
}
