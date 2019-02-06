using System.IO;


namespace Mp3GainLib
{
    public class TagHandler
    {
        public static GainTags Read(Stream file)
        {
            var res = Id3v2.ReadTags(file);

            if (res is null && Id3v2.Find(file))
            {
                res = Id3v2.ReadTags(file);
            }

            if (res is null && Id3v1.Find(file))
            {
                res = Id3v1.ReadTags(file);
            }

            if (res is null && Apev2.Find(file))
            {
                res = Apev2.ReadTags(file);
            }

            return res;
        }
    }
}
