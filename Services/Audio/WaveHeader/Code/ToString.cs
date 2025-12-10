using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
    public partial class WaveHeader {

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
