using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3GainLib;


namespace Mp3GainLibTest
{
    [TestClass]
    public class TagHandlerShould
    {
        [TestMethod]
        public void GetGainTagsInTheBeginning()
        {
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var name = names.Where(n => n.Contains("Loud.mp3")).First();

            using (var strm = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                var tags = TagHandler.Read(strm);
                var id3v2 = tags.Single(t => t.Type == TagTypes.Id3v2);
                Assert.IsNotNull(id3v2);
            }
        }
    }
}
