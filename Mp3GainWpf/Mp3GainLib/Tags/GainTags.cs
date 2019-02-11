namespace Mp3GainLib
{
    public class GainTags
    {
        #region General tag block information

        /// <summary>
        /// Tag type.
        /// </summary>
        public TagTypes Type { get; private set; }


        /// <summary>
        /// Tag version (for information).
        /// </summary>
        public int Version { get; private set; }


        /// <summary>
        /// Where in the file does the block start.
        /// </summary>
        public long OffsetInFile { get; private set; }


        /// <summary>
        /// Raw tags data, including all headers and footers.
        /// </summary>
        public byte[] Raw { get; private set; }

        #endregion


        #region Parsed information (if APEv2)

        public Mp3Tag<double> TrackGain { get; set; }


        public Mp3Tag<double> TrackPeak { get; set; }


        public Mp3Tag<double> AlbumGain { get; set; }


        public Mp3Tag<double> AlbumPeak { get; set; }


        public Mp3Tag<double> MinMaxGain { get; set; }


        public Mp3Tag<double> AlbumMinMaxGain { get; set; }

        #endregion


        #region Init and clean-up

        /// <summary>
        /// Constructor.
        /// </summary>
        public GainTags(TagTypes tagType, int version, long offset, byte[] bytes)
        {
            Type = tagType;
            Version = version;
            OffsetInFile = offset;
            Raw = bytes;
        }

        #endregion
    }
}
