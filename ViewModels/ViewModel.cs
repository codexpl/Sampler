using Sampler.Helpers;
using Sampler.Services.Audio.BufferPcm24;
using Sampler.ViewModels.Menu;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sampler.ViewModels {
    public  class ViewModel {

        public      LogService              LogService         { get; private set; }
        public      BufferPcm24             Buffer             { get; set; }
        public      WaveSample              WaveSmpl           { get; set; } = new WaveSample();

        public      MenuFileViewModel       MenuFile           { get; set; }
        public      MenuPlayerViewViewModel MenuPlayer         { get; set; }
        public      MenuGeneratorViewModel  MenuGenerator      { get; set; }


        public ViewModel( RichTextBox richTextBox ) {
            this.Buffer             = new BufferPcm24(Array.Empty<byte>());
            this.LogService         = new LogService( richTextBox );
            this.MenuFile           = new MenuFileViewModel( (ViewModel) this );
            this.MenuPlayer         = new MenuPlayerViewViewModel( (ViewModel) this );
            this.MenuGenerator      = new MenuGeneratorViewModel( (ViewModel) this );
            LogService.Append( "[INFO] ViewModel initialized." );
        }
    }
}
