﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3GainLib;


namespace Mp3GainLibTest
{
    [TestClass]
    public class ApeV2ReaderShould
    {
        #region Negative tests

        [TestMethod]
        public void FindNoTagsInEmptyFile()
        {
            var raw = new byte[] { };

            using (var strm = new MemoryStream(raw))
            {
                Assert.IsFalse(ApeV2.ReadTags(strm, out var ape));
                Assert.IsNull(ape);
            }
        }


        [TestMethod]
        public void FindNoTagsWhenNotEnoughData()
        {
            var raw = new byte[] { 73, 68, 50, 0 };

            using (var strm = new MemoryStream(raw))
            {
                Assert.IsFalse(ApeV2.ReadTags(strm, out var ape));
                Assert.IsNull(ape);
            }
        }


        [TestMethod]
        public void FindNoTagsWhenNoHeader()
        {
            var raw = new byte[128];
            Array.Copy(new byte[] { 73, 68, 51 }, 0, raw, 0, 3);

            using (var strm = new MemoryStream(raw))
            {
                Assert.IsFalse(ApeV2.ReadTags(strm, out var ape));
                Assert.IsNull(ape);
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
                Assert.IsTrue(ApeV2.ReadTags(strm, out var tags));
                Assert.IsNotNull(tags);
                Assert.AreEqual(TagTypes.Id3v1, tags.Type);
                Assert.AreEqual(226704, tags.OffsetInFile);
                Assert.AreEqual(128, tags.Raw.Length);
            }
        }

        #endregion
    }
}
