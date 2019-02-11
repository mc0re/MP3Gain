using System.IO;


namespace Mp3GainLib
{
    public interface ITagReader
    {
        bool ReadTags(Stream strm, out GainTags tags);
    }
}
