using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.ViewModels {
     public partial class MixViewModel:BaseViewModel
     {
        public int TemplateSizeFrames { get { return TemplateSize; } }
        public int MaxTemplateSize { get { return _viewModel.Corx.RegisterA.LengthInFrames() - TemplateOffset; }  }
        private int _templateSize;
        public int TemplateSize{ get => _templateSize; 
            set {
                _templateSize = value; 
                OnPropertyChanged();
            }
        }

        private double _knobSize;
        public double KnobSize { 
            get => _knobSize; 
            set { 
                _knobSize = value;
                OnPropertyChanged(nameof(KnobSize));
                UpdateTemplateSize();
            } 
        }

        private void GenerateTemplateSize(int sizeSamples) {
            // tutaj robisz miks, przesunięcie, generowanie waveformu itd.
            _viewModel.LogService.Append( "[INFO] Generating template at offset (samples): " + sizeSamples );
        }
        
                private void UpdateTemplateSize() {
                     //KnobOffset = 0..1
                    // mapowanie na offset w próbkach
                    int sizeSamples = (int)(KnobOffset * MaxTemplateSize);
                    TemplateOffset = sizeSamples;
                    // możesz od razu generować template
                    GenerateTemplate(sizeSamples);
                } 


    }
}
