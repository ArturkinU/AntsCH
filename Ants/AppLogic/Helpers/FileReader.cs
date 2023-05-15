using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Ants
{
    public static class FileReader
    {
        public static List<AntPoint> ReadFile(string FilePath)
        {
            var file = File.ReadLines(FilePath);
            List<AntPoint> points = new List<AntPoint>();

            
            return points;
        }
    }
}