using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicChords
{
    public static class CollectionExtensions
    {
        public static uint BinarySearch<T>(this T[] table, T value)
            where T : IComparable<T>
        {
            uint min = 0, max = (uint)table.Length;
            while (min < max)
            {
                uint mid = (min + max) / 2;
                T midItem = table[mid];
                int comparison = midItem.CompareTo(value);

                if (comparison == 0)
                {
                    return mid;
                }
                else if (comparison > 0)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return min;
        }
    }
}
