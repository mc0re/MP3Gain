using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3GainLib;


namespace Mp3GainLibTest
{
    [TestClass]
    public class GainAnalyzerShould
    {
        [TestMethod]
        public void RejectInvalidFrequency()
        {
            Assert.ThrowsException<ArgumentException>(() => new GainAnalyzer(6000));
        }


        [TestMethod]
        public void AnalyzeZeroSamples()
        {
            var g = new GainAnalyzer(8000);
            var res = g.AnalyzeSamples(new double[] {}, null);
            Assert.AreEqual(AnalyzerResults.Ok, res);
        }


        [TestMethod]
        public void AnalyzeTooFewSamples()
        {
            var analyzer = new GainAnalyzer(8000);
            var res = analyzer.AnalyzeSamples(new[] { 0.5, 0.4, 0.3 }, null);
            Assert.AreEqual(AnalyzerResults.Ok, res);

            Assert.ThrowsException<ArgumentException>(() => analyzer.GetTitleGain());
        }


        [TestMethod]
        public void AnalyzeOneWindowOfWhiteNoise()
        {
            var analyzer = new GainAnalyzer(8000);
            var rnd = new Random();

            var res = analyzer.AnalyzeSamples(
                Enumerable.Range(0, 400).Select(a => rnd.NextDouble()).ToArray(), null);
            Assert.AreEqual(AnalyzerResults.Ok, res);

            var gain = analyzer.GetTitleGain();
            Assert.AreEqual(64.82, gain);
        }


        [TestMethod]
        public void AnalyzeLargeWindowOfWhiteNoise()
        {
            var analyzer = new GainAnalyzer(8000);
            var rnd = new Random();

            var res = analyzer.AnalyzeSamples(
                Enumerable.Range(0, 40000).Select(a => rnd.NextDouble()).ToArray(), null);
            Assert.AreEqual(AnalyzerResults.Ok, res);

            var gain = analyzer.GetTitleGain();
            Assert.AreEqual(64.82, gain);
        }


        [TestMethod]
        public void AnalyzeFewWindowsOfWhiteNoise()
        {
            var analyzer = new GainAnalyzer(8000);
            var rnd = new Random();

            for (var i = 0; i < 10; i++)
            {
                var res = analyzer.AnalyzeSamples(
                    Enumerable.Range(0, 400).Select(a => rnd.NextDouble()).ToArray(), null);
                Assert.AreEqual(AnalyzerResults.Ok, res);
            }

            var gain = analyzer.GetTitleGain();
            Assert.AreEqual(64.82, gain);
        }


        [TestMethod]
        public void AnalyzeReducedNoise()
        {
            var analyzer = new GainAnalyzer(8000);
            var rnd = new Random();

            for (var i = 0; i < 10; i++)
            {
                var res = analyzer.AnalyzeSamples(
                    Enumerable.Range(0, 400).Select(a => rnd.NextDouble() * 9093).ToArray(), null);
                Assert.AreEqual(AnalyzerResults.Ok, res);
            }

            var gain = analyzer.GetTitleGain();
            Assert.AreEqual(0.82, gain, 0.00001);
        }
    }
}
