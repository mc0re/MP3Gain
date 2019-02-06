using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3GainLib;

namespace Mp3GainLibTest
{
    [TestClass]
    public class TagHandlerShould
    {
        [TestMethod]
        public void GetGainTags()
        {
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var name = names.Where(n => n.Contains("Loud.mp3")).First();
            using (var strm = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                var tags = TagHandler.Read(strm);
            }
        }
    }
}
