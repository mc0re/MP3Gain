using System;
using System.Collections.Generic;
using System.Linq;


namespace Mp3GainLib
{
    /// <summary>
    /// An array with possibility to access previous data by negative indices.
    /// </summary>
    public class HistoricalArray : IArray
    {
        #region Fields

        /// <summary>
        /// If any data is needed before the start of the buffer or after its end, it's 0.
        /// </summary>
        private double[] mData;


        /// <summary>
        /// Length of the data array (optimization).
        /// </summary>
        private int mDataLength;


        /// <summary>
        /// Historical data.
        /// </summary>
        private readonly double[] mPreviousData;


        /// <summary>
        /// Length of historical data (optimization).
        /// </summary>
        private readonly int mPreviousLength;

        #endregion


        #region Properties

        public double this[int index]
        {
            get
            {
                if (index < 0 && index >= -mPreviousLength)
                    return mPreviousData[mPreviousLength + index];
                else if (index >= 0 && index < mDataLength)
                    return mData[index];
                else
                    return 0;
            }
            set
            {
                if (index >= 0 && index < mDataLength)
                    mData[index] = value;
            }
        }


        /// <summary>
        /// Direct read-only access to data (optimization).
        /// </summary>
        public IReadOnlyList<double> Underlying => mData;

        #endregion


        #region Init and clean-up

        public HistoricalArray(int dataSize, int historySize) :
            this(new double[dataSize], historySize)
        {
        }


        public HistoricalArray(double[] data, int historySize)
        {
            mData = data;
            mDataLength = data.Length;
            mPreviousData = new double[historySize];
            mPreviousLength = historySize;
        }

        #endregion


        #region API

        public void SetData(double[] data)
        {
            mData = data;
            mDataLength = data.Length;
        }


        /// <summary>
        /// Pack up part of the current data as historical data.
        /// </summary>
        public void Shift(int start)
        {
            if (start >= 0)
            {
                Array.Copy(mData, start, mPreviousData, 0, mPreviousLength);
            }
            else
            {
                // We're only talking about a few samples, so a loop is okay
                for (var i = 0; i < mPreviousLength; i++)
                    mPreviousData[i] = this[start + i];
            }
        }

        #endregion
    }
}
