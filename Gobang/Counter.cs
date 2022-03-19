using System;
using System.Collections.Generic;
using System.Text;

namespace IFN563
{
    public class Counter
    {

        private int currentcount;
        public int currentCount
        {
            get
            { return currentcount; }
            set
            {
                if (value <= 0)
                    currentcount = 0;
                else
                    currentcount = value;
            }
        }
        public int Increase()
        {
            return ++currentCount;
        }
        public int Reduce()
        {
            return --currentCount;
        }
    }
}
