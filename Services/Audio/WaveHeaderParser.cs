using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio
{
            public static class WaveHeaderParser  {

                    public static WaveHeader Parse(byte[] data)
                    {
                        using var reader = new BinaryReader(new MemoryStream(data));

                        // RIFF chunk
                        var riff = new string(reader.ReadChars(4)); // "RIFF"
                        var chunkSize = reader.ReadInt32();
                        var format = new string(reader.ReadChars(4)); // "WAVE"

                        // fmt subchunk
                        var subchunk1ID   = new string(reader.ReadChars(4)); // "fmt "
                        var subchunk1Size = reader.ReadUInt32();
                        var formatAudio   = reader.ReadUInt16();
                        var numChannels   = reader.ReadInt16();
                        var sampleRate    = reader.ReadInt32();
                        var byteRate      = reader.ReadUInt32();
                        var blockAlign    = reader.ReadUInt16();
                        var bitsPerSample = reader.ReadInt16();

                        // data subchunk
                        var subchunk2ID   = new string(reader.ReadChars(4)); // "data"
                        var subchunk2Size = reader.ReadInt32();

                        return new WaveHeader
                        {
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

    }
}
