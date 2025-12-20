using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {
public partial class Register
{
    /// <summary>
    /// Importuje dane WAV do Register (Header + Frames).
    /// Obsługuje PCM 16-bit, PCM 24-bit packed (3B) i 24-bit w 32-bit kontenerze (4B).
    /// </summary>
    /// <param name="wavFileBytes">Pełna zawartość pliku WAV.</param>
    /// <returns>true jeśli zaimportowano poprawnie; false jeśli format nieobsługiwany lub uszkodzony.</returns>
    private bool ImportWavFile(byte[] wavFileBytes)
    {
        try
        {
            // 1) Parsujemy nagłówek
            Header = WaveHeaderParser.Parse(wavFileBytes);

            // Podstawowa walidacja
            if (Header.FormatAudio != 1)          // PCM only
                return false;

            if (Header.NumChannels != 1 && Header.NumChannels != 2)
                return false;

            if (Header.BitsPerSample != 16 && Header.BitsPerSample != 24)
                return false;

            if (Header.Subchunk2Size <= 0)
                return false;

            // 2) Obliczamy ilość ramek na podstawie PRAWDZIWEGO BlockAlign
            int bytesPerFrame = Header.BlockAlign; // z pliku
            int totalFrames   = Header.Subchunk2Size / bytesPerFrame;

            // Standardowy WAV PCM ma nagłówek 44 bajty
            // (RIFF + fmt + data bez dodatkowych chunków)
            // Twój parser też czyta dokładnie 44 bajty,
            // więc dane audio zaczynają się od offsetu 44.
            const int dataOffset = 44;

            // 3) Czyścimy i przygotowujemy listę ramek
            Frames = new List<Frame24>(totalFrames);

            bool isStereo     = Header.NumChannels == 2;
            bool isPacked24   = Header.BitsPerSample == 24 && Header.BlockAlign == Header.NumChannels * 3;
            bool isAligned32  = Header.BitsPerSample == 24 && Header.BlockAlign == Header.NumChannels * 4;
            bool isPcm16      = Header.BitsPerSample == 16 && Header.BlockAlign == Header.NumChannels * 2;

            if (!isPacked24 && !isAligned32 && !isPcm16)
            {
                // Na razie obsługujemy tylko te trzy przypadki
                return false;
            }

            // 4) Główna pętla po ramkach
            for (int frameIndex = 0; frameIndex < totalFrames; frameIndex++)
            {
                int frameByteOffset = dataOffset + frameIndex * bytesPerFrame;

                int leftValue24;
                int rightValue24;

                if (isPcm16)
                {
                    // 16-bit → konwersja do 24-bit przestrzeni (shift << 8)
                    // mono: L=R; stereo: osobno
                    short left16, right16;

                    if (isStereo)
                    {
                        left16  = (short)(wavFileBytes[frameByteOffset + 0]
                                        | wavFileBytes[frameByteOffset + 1] << 8);

                        right16 = (short)(wavFileBytes[frameByteOffset + 2]
                                        | wavFileBytes[frameByteOffset + 3] << 8);
                    }
                    else
                    {
                        left16  = (short)(wavFileBytes[frameByteOffset + 0]
                                        | wavFileBytes[frameByteOffset + 1] << 8);
                        right16 = left16;
                    }

                    leftValue24  = left16 << 8;
                    rightValue24 = right16 << 8;
                }
                else if (isPacked24)
                {
                    // 24-bit packed (3B na kanał)
                    if (isStereo)
                    {
                        int l0 = wavFileBytes[frameByteOffset + 0];
                        int l1 = wavFileBytes[frameByteOffset + 1];
                        int l2 = wavFileBytes[frameByteOffset + 2];

                        int r0 = wavFileBytes[frameByteOffset + 3];
                        int r1 = wavFileBytes[frameByteOffset + 4];
                        int r2 = wavFileBytes[frameByteOffset + 5];

                        leftValue24  = Pack24ToInt(l0, l1, l2);
                        rightValue24 = Pack24ToInt(r0, r1, r2);
                    }
                    else
                    {
                        int l0 = wavFileBytes[frameByteOffset + 0];
                        int l1 = wavFileBytes[frameByteOffset + 1];
                        int l2 = wavFileBytes[frameByteOffset + 2];

                        leftValue24  = Pack24ToInt(l0, l1, l2);
                        rightValue24 = leftValue24;
                    }
                }
                else // isAligned32 (24-bit w 32-bit kontenerze, 4B na kanał)
                {
                    if (isStereo)
                    {
                        int l0 = wavFileBytes[frameByteOffset + 0];
                        int l1 = wavFileBytes[frameByteOffset + 1];
                        int l2 = wavFileBytes[frameByteOffset + 2];
                        // byte [frameByteOffset + 3] → padding, ignorujemy

                        int r0 = wavFileBytes[frameByteOffset + 4];
                        int r1 = wavFileBytes[frameByteOffset + 5];
                        int r2 = wavFileBytes[frameByteOffset + 6];
                        // byte [frameByteOffset + 7] → padding

                        leftValue24  = Pack24ToInt(l0, l1, l2);
                        rightValue24 = Pack24ToInt(r0, r1, r2);
                    }
                    else
                    {
                        int l0 = wavFileBytes[frameByteOffset + 0];
                        int l1 = wavFileBytes[frameByteOffset + 1];
                        int l2 = wavFileBytes[frameByteOffset + 2];
                        // [frameByteOffset + 3] → padding

                        leftValue24  = Pack24ToInt(l0, l1, l2);
                        rightValue24 = leftValue24;
                    }
                }

                Frames.Add(new Frame24(leftValue24, rightValue24));
            }

            // 5) Po imporcie aktualizujemy nagłówek pod aktualną liczbę ramek
            Header.Subchunk2Size = Frames.Count * Header.BlockAlign;
            Header.ChunkSize     = 36 + Header.Subchunk2Size;

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Składa 3 bajty little-endian w signed 24-bit int (z sign-extension do 32-bit).
    /// </summary>
    private static int Pack24ToInt(int b0, int b1, int b2)
    {
        int value = b0 | (b1 << 8) | (b2 << 16);
        // sign-extension, jak w Sample24.Read()
        if ((value & 0x800000) != 0)
        {
            value |= unchecked((int)0xFF000000);
        }
        return value;
    }
}
}
