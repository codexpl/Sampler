using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio
{
            public static class WaveHeaderParser  {


            public static WaveHeader GetWaveHeader(int sizeInFramesAudio = 0)
            {
                const int numChannels   = 2;     // stereo
                const int sampleRate    = 44100; // Hz
                const int bitsPerSample = 24;    // 24-bit

                int blockAlign = numChannels * bitsPerSample / 8; // 6 bajtów na ramkę
                int byteRate   = sampleRate * blockAlign;         // 264600 bajtów/s
                int subchunk2Size = sizeInFramesAudio * blockAlign;
                int chunkSize     = 36 + subchunk2Size;

                return new WaveHeader
                {
                    ChunkID       = "RIFF",
                    ChunkSize     = chunkSize,
                    Format        = "WAVE",
                    Subchunk1ID   = "fmt ",
                    Subchunk1Size = 16,              // PCM
                    FormatAudio   = 1,               // PCM
                    NumChannels   = (short)numChannels,
                    SampleRate    = sampleRate,
                    ByteRate      = 264600,
                    BlockAlign    = 6,
                    BitsPerSample = (short)bitsPerSample,
                    Subchunk2ID   = "data",
                    Subchunk2Size = subchunk2Size
                };
            }



            public static WaveHeader Parse(byte[] data)  {
                        using var reader = new BinaryReader(new MemoryStream(data));

                        // RIFF chunk
                        var riff        = new string(reader.ReadChars(4)); // "RIFF"
                        var chunkSize      = reader.ReadInt32();
                        var format      = new string(reader.ReadChars(4)); // "WAVE"

                        // fmt subchunk
                        var subchunk1ID     = new string(reader.ReadChars(4)); // "fmt "
                        var subchunk1Size     = reader.ReadUInt32();
                        var formatAudio      = reader.ReadUInt16();
                        var numChannels      = reader.ReadInt16();
                        var sampleRate         = reader.ReadInt32();
                        var byteRate          = reader.ReadUInt32();
                        var blockAlign      = reader.ReadUInt16();
                        var bitsPerSample    = reader.ReadInt16();

                        // data subchunk
                        var subchunk2ID   = new string(reader.ReadChars(4)); // "data"
                        var subchunk2Size = reader.ReadInt32();

                        return new WaveHeader
                        {
                            ChunkID       = riff,
                            ChunkSize     = chunkSize,
                            Format        = format,
                            Subchunk1ID   = subchunk1ID,
                            Subchunk1Size = subchunk1Size,
                            FormatAudio   = formatAudio,
                            NumChannels   = numChannels,
                            SampleRate    = sampleRate,
                            ByteRate      = byteRate,
                            BlockAlign    = blockAlign,
                            BitsPerSample = bitsPerSample,
                            Subchunk2ID   = subchunk2ID,
                            Subchunk2Size = subchunk2Size
                        };
            }


            public static byte[]     Serialize(WaveHeader header)
            {
                using var ms = new MemoryStream();
                using var writer = new BinaryWriter(ms);

                // RIFF chunk
                writer.Write(header.ChunkID.ToCharArray());   // "RIFF"
                writer.Write(header.ChunkSize);
                writer.Write(header.Format.ToCharArray());    // "WAVE"

                // fmt subchunk
                writer.Write(header.Subchunk1ID.ToCharArray());   // "fmt "
                writer.Write(header.Subchunk1Size);
                writer.Write(header.FormatAudio);
                writer.Write(header.NumChannels);
                writer.Write(header.SampleRate);
                writer.Write(header.ByteRate);
                writer.Write(header.BlockAlign);
                writer.Write(header.BitsPerSample);

                // data subchunk
                writer.Write(header.Subchunk2ID.ToCharArray());   // "data"
                writer.Write(header.Subchunk2Size);

                return ms.ToArray();
            }



    }
}
