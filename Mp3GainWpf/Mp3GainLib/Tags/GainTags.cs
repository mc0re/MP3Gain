namespace Mp3GainLib
{
    public class GainTags
    {
        #region General tag block information

        public TagTypes Type { get; internal set; }


        public int Version { get; internal set; }


        public long BlockOffset { get; set; }


        public int BlockLength { get; internal set; }

        #endregion


        public Mp3Tag<double> TrackGain { get; set; }


        public Mp3Tag<double> TrackPeak { get; set; }


        public Mp3Tag<double> AlbumGain { get; set; }


        public Mp3Tag<double> AlbumPeak { get; set; }


        public Mp3Tag<double> MinMaxGain { get; set; }


        public Mp3Tag<double> AlbumMinMaxGain { get; set; }
    }
}
