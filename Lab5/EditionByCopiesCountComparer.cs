using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    public class EditionByCopiesCountComparer : IComparer<Edition>
    {
        public int Compare(Edition x, Edition y)
        {
            return x.CopiesCount.CompareTo(y.CopiesCount);
        }
    }
}
