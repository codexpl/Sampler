using Sampler.Services.Audio;


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {

    public partial class WaveSampler {


                public void LoadFromBytes(byte[] data) {
                    byte[]    bytesAudio;
                    _status = ObjectStatus.UNKNOWN;
                    _statusMessage = string.Empty;

                    if(data == null || data.Length < 44) {
                        _status = ObjectStatus.ERROR;
                        _statusMessage = "Dane WAV są zbyt krótkie lub puste.";
                        return;
                    }

                    try {
                        _oryginalData = data;
                        ParseFromBytes(data);

                        // Szukamy chunku 'data' i kopiujemy dane audio
                        int offset = 12;
                        while(offset + 8 <= data.Length) {
                            string chunkId = Encoding.ASCII.GetString(data, offset, 4);
                            int chunkSize = BitConverter.ToInt32(data, offset + 4);

                            if(chunkId == "data") {
                                bytesAudio = new byte[chunkSize];
                                Array.Copy(data, offset + 8, bytesAudio, 0, chunkSize);
                                AudioData = new BufferPcm24(bytesAudio);                       // ---------- BUFFERPCM24 INITIALIZE ---------------
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



                public void ParseFromBytes(byte[] data) {
                    if(data == null || data.Length < 12)
                        throw new ArgumentException("Dane WAV są zbyt krótkie lub puste.");

                    Header.FileSize    = BitConverter.ToInt32(data, 4) + 8;
                    Header.Format      = Encoding.ASCII.GetString(data, 8, 4);

                    int offset = 12; // początek chunków po "RIFF" i "WAVE"

                    while(offset + 8 <= data.Length) {
                        string chunkId  = Encoding.ASCII.GetString(data, offset, 4);
                        int chunkSize   = BitConverter.ToInt32(data, offset + 4);

                        if(chunkId == "fmt ") {
                            Header.Subchunk1ID     = chunkId;
                            Header.Subchunk1Size   = (uint)chunkSize;
                            Header.FormatAudio     = BitConverter.ToUInt16(data, offset + 8);
                            Header.NumChannels     = BitConverter.ToInt16(data, offset + 10);
                            Header.SampleRate      = BitConverter.ToInt32(data, offset + 12);
                            Header.ByteRate        = BitConverter.ToUInt32(data, offset + 16);
                            Header.BlockAlign      = BitConverter.ToUInt16(data, offset + 20);
                            Header.BitsPerSample   = BitConverter.ToInt16(data, offset + 22);
                        } else if(chunkId == "data") {
                            Header.Subchunk2ID = chunkId;
                            Header.Subchunk2IDsize = chunkSize;
                            // dane audio można przekazać dalej, np. do innej klasy
                            break;
                        }

                        offset += 8 + chunkSize;
                        if(chunkSize % 2 != 0) offset++; // padding
                    }

                    if(string.IsNullOrEmpty(Header.Subchunk2ID))
                        throw new InvalidDataException("Nie znaleziono chunku 'data' w pliku WAV.");
                }



                public byte[]       ToWaveFile24()
                {
                    // Aktualizacja rozmiarów
                    Header.Subchunk1Size = 16; // PCM
                    Header.Subchunk2IDsize = AudioData.Bytes.Length;
                    Header.FileSize = 36 + Header.Subchunk2IDsize;

                    using (var ms = new MemoryStream())
                    using (var bw = new BinaryWriter(ms))
                    {
                        // RIFF header
                        bw.Write(Encoding.ASCII.GetBytes("RIFF"));
                        bw.Write(Header.FileSize);
                        bw.Write(Encoding.ASCII.GetBytes("WAVE"));

                        // fmt subchunk
                        bw.Write(Encoding.ASCII.GetBytes("fmt "));
                        bw.Write(Header.Subchunk1Size);
                        bw.Write((ushort)Header.FormatAudio);   // PCM = 1
                        bw.Write((short)Header.NumChannels);
                        bw.Write(Header.SampleRate);
                        bw.Write(Header.ByteRate);
                        bw.Write((short)Header.BlockAlign);
                        bw.Write((short)Header.BitsPerSample);

                        // data subchunk
                        bw.Write(Encoding.ASCII.GetBytes("data"));
                        bw.Write(Header.Subchunk2IDsize);
                        bw.Write(AudioData.Bytes);
                        return ms.ToArray();
                    }
                }
    }
}
