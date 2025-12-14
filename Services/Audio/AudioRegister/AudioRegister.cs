using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.Audio {



        public partial class AudioRegister{


                public WaveHeader       Header   { get; set; } = new WaveHeader();
                public List<Frame24>    Frames  { get; set; } = new List<Frame24>();

                public int              LengthInFrames()     => Header.Subchunk2Size / Header.BlockAlign;
                public float            LengthInSeconds()    => (float)LengthInFrames() / Header.SampleRate;


                public AudioRegister() {
                    this.Header = WaveHeaderParser.GetWaveHeader(0);
                    this.Frames = new List<Frame24>();
                }

                public AudioRegister( byte[] wavFileBytes) {
                            if(  !ImportHeader( wavFileBytes ) )    throw new ArgumentException("Invalid WAV file data.");           
                            ImportFrames( wavFileBytes );
                            if( Frames.Count != LengthInFrames() )  throw new InvalidOperationException("Frame count does not match header information.");
                    }


                private bool    ImportHeader( byte[] wavFileBytes ) {
                    WaveHeader header = WaveHeaderParser.Parse( wavFileBytes );
                    if( !header.IsValid() )   return false;
                    this.Header = header;
                    return true;
                }

                private void    ImportFrames( byte[] wavFileBytes ) {
                    int dataStartIndex = wavFileBytes.Length - Header.Subchunk2Size;
                    int totalFrames = Header.Subchunk2Size / Header.BlockAlign;
                    Frames = new List<Frame24>( totalFrames );
                    for( int i = 0; i < totalFrames; i++ ) {
                        int frameStart = dataStartIndex + i * Header.BlockAlign;
                        int leftSample = BitConverter.ToInt32( new byte[] { wavFileBytes[frameStart], wavFileBytes[frameStart + 1], wavFileBytes[frameStart + 2], 0x00 }, 0 );
                        int rightSample = BitConverter.ToInt32( new byte[] { wavFileBytes[frameStart + 3], wavFileBytes[frameStart + 4], wavFileBytes[frameStart + 5], 0x00 }, 0 );
                        Frames.Add( new Frame24( leftSample, rightSample ) );
                    }
                }

                private byte[]  ExportHeader() => WaveHeaderParser.Serialize( this.Header );
        
                private byte[]  ExportFrames() {
                    byte[] audioData = new byte[ Header.Subchunk2Size ];
                    for( int i = 0; i < Frames.Count; i++ ) {
                        int frameStart = i * Header.BlockAlign;
                        Frame24 frame = Frames[i];
                        audioData[frameStart]     = (byte)( frame.Lvalue()        & 0xFF );
                        audioData[frameStart + 1] = (byte)((frame.Lvalue() >> 8)  & 0xFF );
                        audioData[frameStart + 2] = (byte)((frame.Lvalue() >> 16) & 0xFF );
                        audioData[frameStart + 3] = (byte)( frame.Rvalue()        & 0xFF );
                        audioData[frameStart + 4] = (byte)((frame.Rvalue() >> 8)  & 0xFF );
                        audioData[frameStart + 5] = (byte)((frame.Rvalue() >> 16) & 0xFF );
                    }
                    return audioData;
            }

                public byte[]   ExportWavFile() {
                    byte[] headerBytes = ExportHeader();
                    byte[] frameBytes = ExportFrames();
                    byte[] wavFileBytes = new byte[ headerBytes.Length + frameBytes.Length ];
                    Array.Copy( headerBytes, 0, wavFileBytes, 0, headerBytes.Length );
                    Array.Copy( frameBytes, 0, wavFileBytes, headerBytes.Length, frameBytes.Length );
                    return wavFileBytes;
            }

                public int      GetLeftSamleValue( int index ) {
                    if( index < 0 || index >= Frames.Count )   throw new IndexOutOfRangeException("Frame index out of range.");
                    Frame24 frame = Frames[index];
                    return frame.Lvalue();
                }

                public void     SetLeftSampleValue( int index, int value ) {
                    if( index < 0 || index >= Frames.Count )   throw new IndexOutOfRangeException("Frame index out of range.");
                    Frame24 frame = Frames[index];
                    frame.Lvalue( value );
                }

                public int      GetRightSampleValue( int index ) {
                    if( index < 0 || index >= Frames.Count )   throw new IndexOutOfRangeException("Frame index out of range.");
                    Frame24 frame = Frames[index];
                    return frame.Rvalue();
                }

                public void     SetRightSampleValue( int index, int value ) {
                    if( index < 0 || index >= Frames.Count )   throw new IndexOutOfRangeException("Frame index out of range.");
                    Frame24 frame = Frames[index];
                    frame.Rvalue( value );
                }

            }


            public class Frame24 {
          
                public Sample24   LChannel    { get; set; } = new Sample24();
                public Sample24   RChannel    { get; set; } = new Sample24();
                public Frame24( int leftValue = 0, int rightValue = 0 ) {
                        LChannel.Write( leftValue );
                        RChannel.Write( rightValue );
                }

                public int      Lvalue()      => LChannel.Read();
                public int      Rvalue()      => RChannel.Read();

                public void     Lvalue( int value )   => LChannel.Write( value );
                public void     Rvalue( int value )   => RChannel.Write( value );
            }


            public class Sample24 {
                private byte[]      _data   { get; set; } = new byte[3];

                public Sample24( int value = 0 )  => Write( value );


                public void     Write( int value ) {
                        _data[0] = (byte)(value & 0xFF);
                        _data[1] = (byte)((value >> 8) & 0xFF);
                        _data[2] = (byte)((value >> 16) & 0xFF);
                }

                public int      Read() {
                        int value = _data[0] | (_data[1] << 8) | (_data[2] << 16);
                        if ((value & 0x800000) != 0)   { value |= unchecked((int)0xFF000000); }
                        return value;
                }
            }
}
