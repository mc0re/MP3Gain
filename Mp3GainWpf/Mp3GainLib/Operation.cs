using System;

namespace Mp3GainLib
{
    public class Operation
    {
        #region Properties

        /// <summary>
        /// Input samples.
        /// </summary>
        public readonly HistoricalArray Input;


        /// <summary>
        /// After the first filter.
        /// </summary>
        public readonly HistoricalArray Filtered;


        /// <summary>
        /// Output samples (after the second filter).
        /// </summary>
        public readonly HistoricalArray Output;

        #endregion


        #region Init and clean-up

        public Operation(int size)
        {
            Input = new HistoricalArray(0);
            Filtered = new HistoricalArray(size);
            Output = new HistoricalArray(size);
        }

        #endregion


        #region API

        public void SetInput(double[] samples)
        {
            Input.SetData(samples);
        }

        #endregion
    }
}
