using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3GainLib;


namespace Mp3GainLibTest
{
    [TestClass]
    public class Id3v2ReaderShould
    {
        #region Negative tests

        [TestMethod]
        public void FindNoTagsInEmptyFile()
        {
            var raw = new byte[] { };

            using (var strm = new MemoryStream(raw))
            {
                Assert.IsFalse(Id3v2.ReadTags(strm, out var id3v2));
                Assert.IsNull(id3v2);
            }
        }


        [TestMethod]
        public void FindNoTagsWhenWrongHeader()
        {
            // ID2
            var raw = new byte[] { 73, 68, 50, 0, 0, 0, 0, 0, 0, 0 };

            using (var strm = new MemoryStream(raw))
            {
                Assert.IsFalse(Id3v2.ReadTags(strm, out var id3v2));
                Assert.IsNull(id3v2);
            }
        }


        [TestMethod]
        public void FindNoTagsWhenWrongVersion()
        {
            // ID3 v 2.5
            var raw = new byte[] { 73, 68, 51, 5, 0, 0, 0, 0, 0, 0 };

            using (var strm = new MemoryStream(raw))
            {
                Assert.IsFalse(Id3v2.ReadTags(strm, out var id3v2));
                Assert.IsNull(id3v2);
            }
        }


        [TestMethod]
        public void FindNoTagsWhenWrongFlagsInV22()
        {
            // ID3 v 2.2
            var raw = new byte[] { 73, 68, 51, 2, 0, 1, 0, 0, 0, 0 };

            using (var strm = new MemoryStream(raw))
            {
                Assert.IsFalse(Id3v2.ReadTags(strm, out var id3v2));
                Assert.IsNull(id3v2);
            }
        }


        [TestMethod]
        public void FindNoTagsWhenWrongFlagsInV23()
        {
            // ID3 v 2.3
            var raw = new byte[] { 73, 68, 51, 3, 0, 1, 0, 0, 0, 0 };

            using (var strm = new MemoryStream(raw))
            {
                Assert.IsFalse(Id3v2.ReadTags(strm, out var id3v2));
                Assert.IsNull(id3v2);
            }
        }


        [TestMethod]
        public void FindNoTagsWhenWrongFlagsInV24()
        {
            // ID3 v 2.4
            var raw = new byte[] { 73, 68, 51, 4, 0, 1, 0, 0, 0, 0 };

            using (var strm = new MemoryStream(raw))
            {
                Assert.IsFalse(Id3v2.ReadTags(strm, out var id3v2));
                Assert.IsNull(id3v2);
            }
        }


        [TestMethod]
        public void FindNoTagsWhenMalformattedLength()
        {
            // ID3 v 2.3
            var raw = new byte[] { 73, 68, 51, 4, 0, 16, 128, 0, 0, 0 };

            using (var strm = new MemoryStream(raw))
            {
                Assert.IsFalse(Id3v2.ReadTags(strm, out var id3v2));
                Assert.IsNull(id3v2);
            }
        }


        [TestMethod]
        public void FindNoTagsWhenNotEnoughData()
        {
            // ID3 v 2.3, length = 5
            var raw = new byte[] { 73, 68, 51, 4, 0, 16, 0, 0, 0, 5 };

            using (var strm = new MemoryStream(raw))
            {
                Assert.IsFalse(Id3v2.ReadTags(strm, out var id3v2));
                Assert.IsNull(id3v2);
            }
        }

        #endregion


        #region Positive tests

        [TestMethod]
        public void ReadTagsFromFile()
        {
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var name = names.Where(n => n.Contains("Loud.mp3")).First();

            using (var strm = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                Assert.IsTrue(Id3v2.ReadTags(strm, out var id3v2));
                Assert.IsNotNull(id3v2);
                Assert.AreEqual(0, id3v2.OffsetInFile);
                Assert.AreEqual(4196, id3v2.Raw.Length);
                Assert.AreEqual(TagTypes.Id3v2, id3v2.Type);
                Assert.AreEqual(768, id3v2.Version);
            }
        }

        #endregion
    }
}
