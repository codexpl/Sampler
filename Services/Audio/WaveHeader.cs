using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public class WaveHeader  {


            /// <summary>
            ///     Mapa nagłówka WAV.
            ///     
            ///     Offset 0–3   : "RIFF"
            ///     Offset 4–7   : ChunkSize
            ///     Offset 8–11  : "WAVE"
            ///     Offset 12–15 : "fmt "
            ///     Offset 16–19 : Subchunk1Size
            ///     Offset 20–21 : AudioFormat
            ///     Offset 22–23 : NumChannels
            ///     Offset 24–27 : SampleRate
            ///     Offset 28–31 : ByteRate
            ///     Offset 32–33 : BlockAlign
            ///     Offset 34–35 : BitsPerSample
            ///     Offset 36–39 : "data"
            ///     Offset 40–43 : Subchunk2Size
            ///     
            /// </summary>
  
            public int      ChunkSize           { get; set; }
            public string   Format              { get; set; }   =   string.Empty;
            public string   Subchunk1ID         { get; set; }   =   string.Empty;
            public uint     Subchunk1Size       { get; set; }
            public ushort   FormatAudio         { get; set; }
            public short    NumChannels         { get; set; }
            public int      SampleRate          { get; set; }
            public uint     ByteRate            { get; set; }
            public ushort   BlockAlign          { get; set; }
            public short    BitsPerSample       { get; set; }
            public string   Subchunk2ID         { get; set; }   =   string.Empty;
            public int      Subchunk2Size       { get; set; }
        



            /// <summary>
            ///     szybkie sprawdzenie czy nagłówek WAV jest poprawny.    
            /// </summary>
            /// <returns></returns>
            public bool IsValid() =>
                Format.Equals("WAVE", StringComparison.OrdinalIgnoreCase) &&
                Subchunk1ID.Equals("fmt ", StringComparison.OrdinalIgnoreCase) &&
                Subchunk2ID.Equals("data", StringComparison.OrdinalIgnoreCase);




            /// <summary>
            /// ToString override, zwraca czytelny opis nagłówka WAV.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
                {
                    string sFormatAudio     =   _getWavFormatDescription(FormatAudio);

                    return $"Rozmiar pliku: . . . . . . . . {ChunkSize}      bajtów\n" +
                           $"Format:. . . . . . . . . . . . {Format}\n" +
                           $"Subchunk1ID: . . . . . . . . . {Subchunk1ID}\n" +
                           $"Subchunk1Size: . . . . . . . . {Subchunk1Size}\n" + 
                           $"FormatAudio: . . . . . . . . . {sFormatAudio}\n" +
                           $"Liczba kanałów:. . . . . . . . {NumChannels}\n" +
                           $"Częstotliwość próbkowania: . . {SampleRate} Hz\n" +
                           $"Przepływność:  . . . . . . . . {ByteRate}   bajtów/s\n" +
                           $"Blok wyrównania: . . . . . . . {BlockAlign} bajtów\n" +
                           $"Rozdzielczość: . . . . . . . . {BitsPerSample} bitów\n" +
                           $"Subchunk2ID ( Data label ):. . {Subchunk2ID}\n" +
                           $"Rozmiar danych audio:. . . . . {Subchunk2Size} bajtów";
                }


            /// <summary>
            /// Funkcja pomocnicza dla ToString(), zwraca opis formatu WAV na podstawie kodu formatu.
            /// </summary>
            /// <param name="formatCode"></param>
            /// <returns></returns>
            private string _getWavFormatDescription(ushort formatCode)   {
                    return formatCode switch
                    {
                        0x0001 => "PCM",
                        0x0002 => "ADPCM",
                        0x0003 => "IEEE Float",
                        0x0006 => "A-Law",
                        0x0007 => "Mu-Law",
                        0xFFFE => "Extensible",
                        _      => "Unknown compressed Format"
                    };
            }

    }
}
