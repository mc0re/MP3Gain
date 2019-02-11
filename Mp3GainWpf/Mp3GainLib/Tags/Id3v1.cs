using System;
using System.IO;


namespace Mp3GainLib
{
    internal class Id3v1
    {
        public static bool ReadTags(Stream strm, out GainTags tags)
        {
            tags = null;
            return false;
        }
    }
}