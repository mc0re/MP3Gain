namespace Mp3GainLib
{
    public class Mp3Tag<T>
    {
        /// <summary>
        /// Type of the tag container.
        /// </summary>
        public TagTypes Type { get; set; }


        /// <summary>
        /// Offset into the original file.
        /// </summary>
        public long Offset { get; set; }


        /// <summary>
        /// Parsed value.
        /// </summary>
        public T Value { get; set; }
    }
}