using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Mp3GainLib
{
    /// <summary>
    /// ID3 v2 tags are located in the beginning of the file.
    /// We do not parse the tags, as there is no need for this.
    /// </summary>
    public class Id3v2
    {
        #region Constants

        private const byte Version22 = 2;

        private const byte Version23 = 3;

        private const byte Version24 = 4;

        private const int HeaderSize = 10;

        private const string Magic = "ID3";

        private const byte HighestBitMask = 0x80;

        private const int WrongInteger = -1;

        #endregion


        #region API

        /// <summary>
        /// Id3v2 tags start with "ID3" at the current position.
        /// </summary>
        /// <param name="strm">Input stream</param>
        /// <returns>Whether the tags were found</returns>
        public static bool ReadTags(Stream strm, out GainTags tags)
        {
            tags = ReadTagsHeader(strm);
            return tags != null;
        }

        #endregion


        #region Utility

        /// <summary>
        /// Id3v2 tags start with "ID3" at the current position.
        /// </summary>
        /// <param name="strm"></param>
        /// <returns>null if the tags were not found</returns>
        private static GainTags ReadTagsHeader(Stream strm)
        {
            var offset = strm.Position;
            var hdr = GetHeader(strm);
            if (hdr is null)
            {
                // ID3 not found
                return null;
            }

            var versionMinor = hdr[3];
            if (!(new[] { Version22, Version23, Version24 }).Contains(versionMinor))
            {
                // Unknown sub-version
                return null;
            }

            // Unsupported flags?
            Id3v2Flags flags = (Id3v2Flags) hdr[5];
            switch (versionMinor)
            {
                case Version22 when (flags & ~Id3v2Flags.Unsync) != 0:
                    return null;

                case Version23 when (flags & ~(Id3v2Flags.Unsync | Id3v2Flags.ExtHdr | Id3v2Flags.Expr)) != 0:
                    return null;

                case Version24 when (flags & ~(Id3v2Flags.Unsync | Id3v2Flags.ExtHdr | Id3v2Flags.Expr | Id3v2Flags.Footer)) != 0:
                    return null;
            }

            var dlen = GetInt7bit(hdr.Skip(6));
            if (dlen == WrongInteger)
            {
                // Malformatted length
                return null;
            }

            var blockLen = HeaderSize + dlen + (flags.HasFlag(Id3v2Flags.Footer) ? HeaderSize : 0);
            var raw = new byte[blockLen];

            strm.Seek(-HeaderSize, SeekOrigin.Current);
            if (strm.Read(raw, 0, blockLen) != blockLen)
            {
                // Not enough data
                return null;
            }

            return new GainTags(
                TagTypes.Id3v2, (versionMinor << 8) + hdr[4], offset, raw);
        }


        private static byte[] GetHeader(Stream strm)
        {
            var hdr = new byte[HeaderSize];
            if (strm.Read(hdr, 0, HeaderSize) != HeaderSize)
            {
                // Not enough data
                return null;
            }

            if (Encoding.ASCII.GetString(hdr, 0, Magic.Length) != Magic)
            {
                // Wrong magic bytes, not ID3v2
                return null;
            }

            return hdr;
        }


        private static int GetInt7bit(IEnumerable<byte> bytes)
        {
            var p = bytes.Take(4).ToArray();
            if (p.Length != 4)
                return WrongInteger;

            if (p.Any(v => (v & HighestBitMask) != 0))
                return WrongInteger;

            return (p[0] << 21) + (p[1] << 14) + (p[2] << 7) + p[3];
        }

        #endregion
    }
}
