using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Helpers {




    public  enum ObjectStatus : uint {
            SUCCESS     =   0b0000_0000,
            WARNING     =   0b0000_0001,
            ERROR       =   0b0000_0010,
            UNKNOWN     =   0b0000_0100
    }




    public class WaveHeader  {
  
            public int      FileSize            { get; set; }
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
            public int      Subchunk2IDsize     { get; set; }
        


            /// <summary>
            /// ToString override, zwraca czytelny opis nagłówka WAV.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
                {
                    string sFormatAudio     =   _getWavFormatDescription(FormatAudio);

                    return $"Rozmiar pliku: . . . . . . . . {FileSize}      bajtów\n" +
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
                           $"Rozmiar danych audio:. . . . . {Subchunk2IDsize} bajtów";
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







    public class WaveSample {

        #region DIAGNOSTYCZNE ___ 
            private byte[] _oryginalData    = Array.Empty<byte>();

            private ObjectStatus _status    = ObjectStatus.UNKNOWN;
            public ObjectStatus Status      => _status;

            private string _statusMessage   = string.Empty;
            public string StatusMessage     => _statusMessage; 
        #endregion



            private     WaveHeader          _header         =  new WaveHeader();
            private     byte []             _audioData      =  Array.Empty<byte>();



        /// <summary>   
        ///     Constructor pustego obiektu.
        /// </summary>
        public WaveSample() {
                    this._status                =   ObjectStatus.UNKNOWN;
                    this._oryginalData          =   Array.Empty<byte>();
        }



        /// <summary>
        ///     Constructor klasy WaveSample, przyjmuje tablicę bajtów z odczytanego pliku wav. 
        /// </summary>
        /// <param name="data"> odczytany plik wav </param>
        public WaveSample(byte[] data) {             
                    if ( data.Length < 44 || Encoding.ASCII.GetString( data, 0, 4) != "RIFF") {
                        this._status = ObjectStatus.ERROR;
                        this._statusMessage =    this.GetType().Name  + "\n nieprawidłowe dane wejsciowe. spodziewano danych pliku WAV";
                        return;
                    }
                    this._oryginalData = (byte[]) data.Clone();
                    LoadFromBytes( _oryginalData );
                    if ( this._header.Subchunk2IDsize + 44 != this._oryginalData.Length ) {
                        this._status = ObjectStatus.WARNING;
                        this._statusMessage =    this.GetType().Name  + 
                                    "\n nieprawidłowe dane wejsciowe. rozmiar danych audio nie zgadza się z wartością pola Subchunk2IDsize";
                        return;
                    }
        }





        #region Parser   ___ 
        public void ParseFromBytes(byte[] data) {
            if(data == null || data.Length < 12)
                throw new ArgumentException("Dane WAV są zbyt krótkie lub puste.");

            _header.FileSize = BitConverter.ToInt32(data, 4) + 8;
            _header.Format = Encoding.ASCII.GetString(data, 8, 4);

            int offset = 12; // początek chunków po "RIFF" i "WAVE"

            while(offset + 8 <= data.Length) {
                string chunkId = Encoding.ASCII.GetString(data, offset, 4);
                int chunkSize = BitConverter.ToInt32(data, offset + 4);

                if(chunkId == "fmt ") {
                    _header.Subchunk1ID     = chunkId;
                    _header.Subchunk1Size   = (uint)chunkSize;
                    _header.FormatAudio     = BitConverter.ToUInt16(data, offset + 8);
                    _header.NumChannels     = BitConverter.ToInt16(data, offset + 10);
                    _header.SampleRate      = BitConverter.ToInt32(data, offset + 12);
                    _header.ByteRate        = BitConverter.ToUInt32(data, offset + 16);
                    _header.BlockAlign      = BitConverter.ToUInt16(data, offset + 20);
                    _header.BitsPerSample   = BitConverter.ToInt16(data, offset + 22);
                } else if(chunkId == "data") {
                    _header.Subchunk2ID = chunkId;
                    _header.Subchunk2IDsize = chunkSize;
                    // dane audio można przekazać dalej, np. do innej klasy
                    break;
                }

                offset += 8 + chunkSize;
                if(chunkSize % 2 != 0) offset++; // padding
            }

            if(string.IsNullOrEmpty(_header.Subchunk2ID))
                throw new InvalidDataException("Nie znaleziono chunku 'data' w pliku WAV.");
        }


        public void LoadFromBytes(byte[] data) {
            _status = ObjectStatus.UNKNOWN;
            _statusMessage = string.Empty;

            if(data == null || data.Length < 44) {
                _status = ObjectStatus.ERROR;
                _statusMessage = "Dane WAV są zbyt krótkie lub puste.";
                return;
            }

            try {
                _oryginalData = data;
                this.ParseFromBytes(data);

                // Szukamy chunku 'data' i kopiujemy dane audio
                int offset = 12;
                while(offset + 8 <= data.Length) {
                    string chunkId = Encoding.ASCII.GetString(data, offset, 4);
                    int chunkSize = BitConverter.ToInt32(data, offset + 4);

                    if(chunkId == "data") {
                        _audioData = new byte[chunkSize];
                        Array.Copy(data, offset + 8, _audioData, 0, chunkSize);
                        _status = ObjectStatus.SUCCESS;
                        _statusMessage = "Plik WAV został poprawnie załadowany.";
                        return;
                    }

                    offset += 8 + chunkSize;
                    if(chunkSize % 2 != 0) offset++;
                }

                _status = ObjectStatus.WARNING;
                _statusMessage = "Nie znaleziono chunku 'data' — brak danych audio.";
            } catch(Exception ex) {
                _status = ObjectStatus.ERROR;
                _statusMessage = $"Błąd podczas parsowania WAV: {ex.Message}";
            }
        }

        #endregion


        public WaveHeader   GetHeader()          => _header;
        public double       GetDurationSeconds() => (double)_header.Subchunk2IDsize / _header.ByteRate;
        public int          GetSampleCount()     => _header.Subchunk2IDsize / _header.BlockAlign;
        public byte[]       GetAudioData()       => _audioData;
        public byte[]       GetOriginalData()    => _oryginalData;


        public int GetSample(int index, int channel)
        {
            int sampleSize = _header.BitsPerSample / 8;
            int channels = _header.NumChannels;
            int lastSampleIndex = GetSampleCount() - 1;

            if (index < 0 || index > lastSampleIndex || channel < 0 || channel >= channels)
                return 0xFFFFFFF;

            int offset = (index * channels + channel) * sampleSize;

            int sample = (_audioData[offset + 2] << 16) |
                         (_audioData[offset + 1] << 8) |
                         _audioData[offset];

            if ((sample & 0x800000) != 0)
                sample |= unchecked((int)0xFF000000);

            return sample;
        }



    }
}


