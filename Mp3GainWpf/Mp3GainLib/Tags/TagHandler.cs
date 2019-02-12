using System.Collections.Generic;
using System.IO;


namespace Mp3GainLib
{
    public class TagHandler
    {
        public static IList<GainTags> Read(Stream file)
        {
            var res = new List<GainTags>();

            if (Id3v2.ReadTags(file, out var id3v2))
            {
                res.Add(id3v2);
            }

            if (Id3v1.ReadTags(file, out var id3v1))
            {
                res.Add(id3v1);
            }

            if (ApeV2.ReadTags(file, out var apev2))
            {
                res.Add(apev2);
            }

            return res;
        }
    }
}
