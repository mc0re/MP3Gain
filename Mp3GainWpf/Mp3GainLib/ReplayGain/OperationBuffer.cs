namespace Mp3GainLib
{
    public class OperationBuffer
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

        public OperationBuffer(int size, int historySize)
        {
            Input = new HistoricalArray(0, historySize);
            Filtered = new HistoricalArray(size, historySize);
            Output = new HistoricalArray(size, historySize);
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
