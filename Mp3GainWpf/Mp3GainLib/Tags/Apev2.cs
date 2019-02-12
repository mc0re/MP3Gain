using System;
using System.IO;


namespace Mp3GainLib
{
    public class ApeV2
    {
        public static bool ReadTags(Stream strm, out GainTags tags)
        {
            tags = null;
            return false;
        }
    }
}