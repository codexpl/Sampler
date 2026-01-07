using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.Services.AppConfiguration
{

    public static class AppConfiguration
    {

        private static string _readDirectory = @"M:\FLSTUDIO\SAMPLE PACKS\Vision Noisia Sample Pack Vol.3 WAV-FANTASTiC\Vision Noisia Sample Pack Vol.3 WAV-FANTASTiC\ONE_SHOTS";
        private static string _writeDirectory = @"M:\DAVINCI PROJECT";


        public static string getReadDirectory() => _readDirectory;
        public static void setReadDirectory(string readDirectory) { _readDirectory = readDirectory; }
        public static string getWriteDirectory() => _writeDirectory;
        public static void setWriteDirectory(string writeDirectory) { _writeDirectory = writeDirectory; }
    }
}
