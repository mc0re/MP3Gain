using System;
using System.IO;
using System.Text;


namespace Mp3GainLib
{
    /// <summary>
    /// ID3 v1 tags are located in the last 128 bytes of the file.
    /// Their sizes and locations are fixed.
    /// We do not parse the tags, as there is no need for this.
    /// </summary>
    public class Id3v1
    {
        private const int Id3V1Size = 128;

        private const string Id3V1Magic = "TAG";


        public static bool ReadTags(Stream strm, out GainTags tags)
        {
            tags = null;

            strm.Seek(0, SeekOrigin.End);
            if (strm.Position < Id3V1Size)
            {
                return false;
            }

            strm.Seek(-Id3V1Size, SeekOrigin.End);
            var offset = strm.Position;
            var tagBytes = new byte[Id3V1Size];

            if (strm.Read(tagBytes, 0, Id3V1Size) != Id3V1Size)
            {
                // As we've checked the size before, this might mean reading error.
                return false;
            }

            var magic = Encoding.ASCII.GetString(tagBytes, 0, Id3V1Magic.Length);
            if (magic != Id3V1Magic)
            {
                return false;
            }

            tags = new GainTags(TagTypes.Id3v1, 0, offset, tagBytes);
            return true;
        }
    }
}