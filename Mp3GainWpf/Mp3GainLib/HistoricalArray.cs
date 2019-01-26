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
        protected double[] mData;


        /// <summary>
        /// Length of the data array (optimization).
        /// </summary>
        private int mDataLength;


        /// <summary>
        /// Historical data.
        /// </summary>
        private List<double> mPrevious;


        /// <summary>
        /// Length of historical data (optimization).
        /// </summary>
        private int mPreviousLength;

        #endregion


        #region Properties

        public double this[int index]
        {
            get
            {
                if (index < 0 && index >= -mPreviousLength)
                    return mPrevious[mPreviousLength + index];
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

        public HistoricalArray(int size)
        {
            mData = new double[size];
            mDataLength = size;
        }


        public HistoricalArray(double[] data)
        {
            mData = data;
            mDataLength = data.Length;
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
        public void Shift(int start, int size)
        {
            mPrevious = mData.Skip(start).Take(size).ToList();
            mPreviousLength = size;
        }

        #endregion
    }
}
