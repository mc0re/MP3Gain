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
        public void AnalyzeOneWindowOfSilence()
        {
            var analyzer = new GainAnalyzer(8000);

            var res = analyzer.AnalyzeSamples(
                Enumerable.Range(0, 400).Select(a => 0.9).ToArray(), null);
            Assert.AreEqual(AnalyzerResults.Ok, res);

            var gain = analyzer.GetTitleGain();
            Assert.AreEqual(64.82, gain);
        }


        [TestMethod]
        public void AnalyzeLargeWindowOfSilence()
        {
            var analyzer = new GainAnalyzer(8000);

            var res = analyzer.AnalyzeSamples(
                Enumerable.Range(0, 40000).Select(a => 0.0).ToArray(), null);
            Assert.AreEqual(AnalyzerResults.Ok, res);

            var gain = analyzer.GetTitleGain();
            Assert.AreEqual(64.82, gain);
        }


        [TestMethod]
        public void AnalyzeFewWindowsOfSilence()
        {
            var analyzer = new GainAnalyzer(8000);

            for (var i = 0; i < 10; i++)
            {
                var res = analyzer.AnalyzeSamples(
                    Enumerable.Range(0, 400).Select(a => 0.0).ToArray(), null);
                Assert.AreEqual(AnalyzerResults.Ok, res);
            }

            var gain = analyzer.GetTitleGain();
            Assert.AreEqual(64.82, gain);
        }


        [TestMethod]
        public void AnalyzeMaxVolume()
        {
            var analyzer = new GainAnalyzer(8000);

            var res = analyzer.AnalyzeSamples(
                Enumerable.Range(0, 400).Select(a => 32767.0).ToArray(), null);
            Assert.AreEqual(AnalyzerResults.Ok, res);

            var gain = analyzer.GetTitleGain();
            Assert.AreEqual(2.82, gain, 0.00001);
        }


        [TestMethod]
        public void AnalyzeWhiteNoise()
        {
            var analyzer = new GainAnalyzer(8000);
            var rnd = new Random();

            var res = analyzer.AnalyzeSamples(
                Enumerable.Range(0, 1000).Select(a => (double) rnd.Next(-32768, 32767)).ToArray(), null);
            Assert.AreEqual(AnalyzerResults.Ok, res);

            var gain = analyzer.GetTitleGain();
            Assert.AreEqual(-16.18, gain, 0.01);
        }
    }
}
